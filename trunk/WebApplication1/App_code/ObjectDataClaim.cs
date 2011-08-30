using System;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Samples.AspNet.ObjectDataClaim
{
    //
    //  Northwind Employee Data Factory
    //

    public class ClaimData
    {

        private string _connectionString;


        public ClaimData()
        {
            Initialize();
        }


        public void Initialize()
        {
            // Initialize data source. Use "Northwind" connection string from configuration.

            if (ConfigurationManager.ConnectionStrings["ApplicationServices"] == null ||
                ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString.Trim() == "")
            {
                throw new Exception("A connection string named 'DispOKBConnectionString1' with a valid connection string " +
                                    "must exist in the <connectionStrings> configuration section for the application.");
            }

            _connectionString =
              ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        }


        // Select all employees.

        public DataTable GetAll(string sortColumns, int startRecord, int maxRecords, DateTime DateBegin, DateTime DateEnd)
        {
            VerifySortColumns(sortColumns);

            string sqlCmd = "SELECT c.ID_Claim, c.ID_Otdelen , c.ID_Building, c.ID_Room, c.ID_Person, p.ID_Unit, c.Note, c.DateClaim, " +
                            " b.NameBuilding, o.NameOtdelen as NameOtdelen, ( r.NameRoom + ' / '+ r.Num)as NameRoom, p.NamePerson, u.NameUnit " +
                            " FROM Claim as c " +
                            " Left join Otdelen as o on o.ID_Otdelen=c.ID_Otdelen " +
                            " Left join Person as p on p.ID_Person=c.ID_Person " +
                            " Left join Room as r on r.ID_Room=c.ID_Room " + 
                            " Left join Building as b on b.ID_Building=c.ID_Building " +
                            " Left join Unit as u on u.ID_Unit=p.ID_Unit " +
                            " Where 1=1 AND c.DateClaim between @DateBegin and @DateEnd";
            
            if (sortColumns.Trim() == "")
                sqlCmd += " ORDER BY ID_Claim";
            else
                sqlCmd += " ORDER BY " + sortColumns;

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);
            da.SelectCommand.Parameters.Add("@DateBegin", SqlDbType.DateTime).Value = DateBegin;
            da.SelectCommand.Parameters.Add("@DateEnd", SqlDbType.DateTime).Value = DateEnd; 
            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                da.Fill(ds, startRecord, maxRecords, "Claim");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables["Claim"];
        }

        public int SelectCount(DateTime DateEnd, DateTime DateBegin)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Claim", conn);

            int result = 0;

            try
            {
                conn.Open();

                result = (int)cmd.ExecuteScalar();
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
        

        //////////
        // Verify that only valid columns are specified in the sort expression to avoid a SQL Injection attack.

        private void VerifySortColumns(string sortColumns)
        {
            if (sortColumns.ToLowerInvariant().EndsWith(" desc"))
                sortColumns = sortColumns.Substring(0, sortColumns.Length - 5);

            string[] columnNames = sortColumns.Split(',');

            foreach (string columnName in columnNames)
            {
                switch (columnName.Trim().ToLowerInvariant())
                {
                    case "ID_Claim":
                        break;
                    case "note":
                        break;
                    case "datetime":
                        break;
                    case "nameotdelen":
                        break;
                    case "nameroom":
                        break;
                    case "namebuilding":
                        break;
                    case "nameperson":
                        break;
                    case "":
                        break;
                    default:
                        throw new ArgumentException("SortColumns contains an invalid column name.");
                        break;
                }
            }
        }

        public DataTable GetAllFiltr(DateTime DateEnd, DateTime DateBegin)
        {
            string sqlCmd = "SELECT Distinct c.ID_Building, b.NameBuilding,"+
                            " c.ID_Otdelen, o.NameOtdelen, " +
                            " c.ID_Room, r.NameRoom, " +
                            " p.ID_Unit, u.NameUnit, " +
                            " c.ID_Person, p.NamePerson " +
                            " FROM Claim as c " +
                            " Left join Otdelen as o on o.ID_Otdelen=c.ID_Otdelen " +
                            " Left join Person as p on p.ID_Person=c.ID_Person " +
                            " Left join Building as b on b.ID_Building=c.ID_Building "+
                            " Left join Room as r on r.ID_Room=c.ID_Room "+
                            " Left join Unit as u on u.ID_Unit=p.ID_Unit " +
                            " Where c.DateClaim between @DateBegin and @DateEnd ";

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);
            da.SelectCommand.Parameters.Add("@DateBegin", SqlDbType.DateTime).Value = DateBegin;
            da.SelectCommand.Parameters.Add("@DateEnd", SqlDbType.DateTime).Value = DateEnd; 
            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                da.Fill(ds, "Claim");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables["Claim"];
        }

        // Select an Otdelen.
        public DataTable GetOneRecord(int ID_Claim)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da =
              new SqlDataAdapter("SELECT c.ID_Claim, c.ID_Otdelen , c.ID_Building, c.ID_Room, c.ID_Person, p.ID_Unit, c.Note, c.DateClaim, " +
                            " (b.NameBuilding +' / '+ o.NameOtdelen) as NameOtdelen, p.NamePerson " +
                            " FROM Claim as c " +
                            " Left join Otdelen as o on o.ID_Otdelen=c.ID_Otdelen " +
                            " Left join Person as p on p.ID_Person=c.ID_Person " + 
                            " Left join Building as b on b.ID_Building=c.ID_Building "+
                            " Where c.ID_Claim=@ID_Claim ", conn);
            da.SelectCommand.Parameters.Add("@ID_Claim", SqlDbType.Int).Value = ID_Claim;

            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                da.Fill(ds, "Claim");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables["Claim"];
        }

        // Delete the Otdelen by ID_Claim.

        public int DeleteRecord(int ID_Claim)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM Claim WHERE ID_Claim = @ID_Claim", conn);
            cmd.Parameters.Add("@ID_Claim", SqlDbType.Int).Value = ID_Claim;
            int result = 0;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return result;
        }


        // Update the Otdelen by original ID_Otdelen.

        public int UpdateRecord(int ID_Claim, string Note, string DateClaim, int ID_Building, int ID_Otdelen, int ID_Room,
                                int ID_Person, int Status)
        {
            if (String.IsNullOrEmpty(Note))
                throw new ArgumentException("Необходимо заполнять поле Наименование комнаты");
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE Claim " +
                                                "  SET Note=@Note , DateClaimChange=@DateClaim" +
                                                ", ID_Building=@ID_Building, ID_Otdelen=@ID_Otdelen, ID_Room=@ID_Room " +
                                                ", ID_Person=@ID_Person , Status=@Status" +
                                                "  WHERE ID_Claim=@ID_Claim", conn);

            cmd.Parameters.Add("@Note", SqlDbType.VarChar, 50).Value = Note;
            cmd.Parameters.Add("@DateClaim", SqlDbType.DateTime).Value = DateClaim;
            cmd.Parameters.Add("@ID_Building", SqlDbType.Int).Value = ID_Building;
            cmd.Parameters.Add("@ID_Otdelen", SqlDbType.Int).Value = ID_Otdelen;
            cmd.Parameters.Add("@ID_Room", SqlDbType.Int).Value = ID_Room;
            cmd.Parameters.Add("@ID_Person", SqlDbType.Int).Value = ID_Person;
            cmd.Parameters.Add("@ID_Claim", SqlDbType.Int).Value = ID_Claim;
            cmd.Parameters.Add("@Status", SqlDbType.Int).Value = Status;
            int result = 0;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        // Insert an Otdelen.

        public int InsertRecord(string Note, string DateClaim, int ID_Building, int ID_Otdelen, int ID_Room,
                                int ID_Person, int Status)

        {
            if (String.IsNullOrEmpty(Note))
                throw new ArgumentException("NamePerson cannot be null or an empty string.");
            
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO Claim " +
                                                "  (Note,DateClaim, ID_Building, ID_Otdelen, ID_Room, ID_Person, Status ) " +
                                                "  Values(@Note,@DateClaim,@ID_Building, @ID_Otdelen, @ID_Room, @ID_Person, @Status ); " +
                                                "SELECT @ID_Claim = SCOPE_IDENTITY()", conn);

            cmd.Parameters.Add("@Note", SqlDbType.NVarChar, 500).Value = Note;
            cmd.Parameters.Add("@DateClaim", SqlDbType.DateTime).Value = DateClaim;
            cmd.Parameters.Add("@ID_Building", SqlDbType.Int).Value = ID_Building;
            cmd.Parameters.Add("@ID_Otdelen", SqlDbType.Int).Value = ID_Otdelen;
            cmd.Parameters.Add("@ID_Room", SqlDbType.Int).Value = ID_Room;
            cmd.Parameters.Add("@ID_Person", SqlDbType.Int).Value = ID_Person;
            cmd.Parameters.Add("@Status", SqlDbType.Int).Value = Status;
            SqlParameter p = cmd.Parameters.Add("@ID_Claim", SqlDbType.Int);
            p.Direction = ParameterDirection.Output;

            int newID_Claim = 0;
            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();

                newID_Claim = (int)p.Value;
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return newID_Claim;
        }
           
        //
        // Methods that support Optimistic Concurrency checks.
        //

        // Delete the Otdelen by ID_Claim.

        public int DeleteRecord(string Note, int original_ID, string original_Note)
        {
            if (String.IsNullOrEmpty(original_Note))
                throw new ArgumentException("FirstName cannot be null or an empty string.");

            string sqlCmd = "DELETE FROM Claim WHERE ID_Claim = @original_ID " +
                            " AND Note = @original_Note ";

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);

            cmd.Parameters.Add("@Note", SqlDbType.VarChar, 50).Value = Note;
            cmd.Parameters.Add("@original_ID", SqlDbType.Int).Value = original_ID;
            cmd.Parameters.Add("@original_Note", SqlDbType.VarChar, 10).Value = original_Note;

            int result = 0;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
        // Update the Employee by original ID_Claim.

        public int UpdateRecord(string Note, string original_Note, int original_ID_Claim, string DateClaim, string original_DateClaim
            , int ID_Building, int original_ID_Building, int ID_Otdelen, int original_ID_Otdelen
            , int ID_Room, int original_ID_Room, int ID_Person, int original_ID_Person
            , int Status, int original_Status)
        {
            if (String.IsNullOrEmpty(Note))
                throw new ArgumentException("Необходимо заполнить описание заявки.");

            string sqlCmd = "UPDATE Claim " +
                            "  SET Note = @Note , DateClaim=@DateClaim" +
                            ", ID_Building=@ID_Building, ID_Otdelen=@ID_Otdelen, ID_Room=@ID_Room " +
                            ", ID_Person=@ID_Person,Status=@Status  " +
                            "  WHERE ID_Claim = @original_ID_Claim ";

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);

            cmd.Parameters.Add("@Note", SqlDbType.NVarChar, 500).Value = Note;
            cmd.Parameters.Add("@DateClaim", SqlDbType.DateTime).Value = DateClaim;
            cmd.Parameters.Add("@ID_Building", SqlDbType.Int).Value = ID_Building;
            cmd.Parameters.Add("@ID_Otdelen", SqlDbType.Int).Value = ID_Otdelen;
            cmd.Parameters.Add("@ID_Room", SqlDbType.Int).Value = ID_Room;
            cmd.Parameters.Add("@ID_Person", SqlDbType.Int).Value = ID_Person;
            cmd.Parameters.Add("@Status", SqlDbType.Int).Value = Status;
            cmd.Parameters.Add("@original_ID_Claim", SqlDbType.Int).Value = original_ID_Claim;
  
            int result = 0;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
        
    }
}