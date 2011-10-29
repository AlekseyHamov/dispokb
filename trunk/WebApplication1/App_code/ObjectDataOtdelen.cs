using System;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Samples.AspNet.ObjectDataOtdelen
{
    //
    //  Northwind Employee Data Factory
    //

    public class OtdelenData
    {

        private string _connectionString;


        public OtdelenData()
        {
            Initialize();
        }


        public void Initialize()
        {
            // Initialize data source. Use "Northwind" connection string from configuration.

            if (ConfigurationManager.ConnectionStrings["ApplicationServices"] == null ||
                ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString.Trim() == "")
            {
                throw new Exception("A connection string named 'ApplicationServices' with a valid connection string " +
                                    "must exist in the <connectionStrings> configuration section for the application.");
            }

            _connectionString =
              ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        }


        // Select all employees.

        public DataTable GetAllOtdelen(string sortColumns, int startRecord, int maxRecords)
        {
            VerifySortColumns(sortColumns);

            string sqlCmd = "SELECT ID_Otdelen, Otdelen.ID_Building, NameOtdelen, NameBuilding, [Floor] FROM Otdelen " +
                            "Left Join Building on otdelen.ID_Building=Building.ID_Building ";
            if (sortColumns.Trim() == "")
                sqlCmd += "ORDER BY ID_Otdelen";
            else
                sqlCmd += "ORDER BY " + sortColumns;

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);

            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                da.Fill(ds, startRecord, maxRecords, "Otdelen");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables["Otdelen"];
        }
        public DataTable GetTempGrid(int ID_Table)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            string sqlCmd = "SELECT fr.id as ID_FilesRelation, fr.ID_Files, f.fileName, fr.ID_Table , f.fileType    " +
                "  FROM  FilesRelation as fr " +
                " Left join files as f on f.ID = fr.ID_Files " +
                                 "WHERE  fr.NameTable = 'Building' and fr.ID_Table = @ID_Table";

            SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);
            da.SelectCommand.Parameters.Add("@ID_Table", SqlDbType.Int).Value = ID_Table;                                       

            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                da.Fill(ds, "Building");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables["Building"];
        }


        public int SelectCount()
        {
            string sqlCmd = "SELECT COUNT(*) FROM Otdelen " + 
                "Left Join Building on otdelen.ID_Building=Building.ID_Building ";

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);
            
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
                    case "id_otdelen":
                        break;
                    case "nameotdelen":
                        break;
                    case "namebuilding":
                        break;
                    case "floor":
                        break;
                    case "":
                        break;
                    default:
                        throw new ArgumentException("SortColumns contains an invalid column name.");
                        break;
                }
            }
        }
   
        // Select an Otdelen.
        public DataTable GetOtdelen(int ID_Otdelen)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da =
            new SqlDataAdapter("SELECT ID_Otdelen, NameOtdelen, ID_Building, Floor " +
                                 "  FROM Otdelen WHERE ID_Otdelen = @ID_Otdelen", conn);

            da.SelectCommand.Parameters.Add("@ID_Otdelen", SqlDbType.Int).Value = ID_Otdelen;

            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                da.Fill(ds, "Otdelen");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables["Otdelen"];
        }

        // Delete the Otdelen by ID_Otdelen.

        public int DeleteOtdelen(int ID_Otdelen)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM Otdelen WHERE ID_Otdelen = @ID_Otdelen", conn);
            cmd.Parameters.Add("@ID_Otdelen", SqlDbType.Int).Value = ID_Otdelen;
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

        public int UpdateOtdelen(int ID_Otdelen, int ID_Building, string NameOtdelen , string Floor)
        {
            if (String.IsNullOrEmpty(NameOtdelen))
                throw new ArgumentException("FirstName cannot be null or an empty string.");
            if (ID_Building == null)
                throw new ArgumentException("Необходимо заполнять выбирать Корпус/блок");

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE Otdelen " +
                                                "  SET NameOtdelen=@NameOtdelen , ID_Building = @ID_Building" +
                                                ", Floor = @Floor" +
                                                 "  WHERE ID_Otdelen=@ID_Otdelen", conn);

            cmd.Parameters.Add("@NameOtdelen", SqlDbType.VarChar, 50).Value = NameOtdelen;
            cmd.Parameters.Add("@Floor", SqlDbType.VarChar, 50).Value = Floor;
            cmd.Parameters.Add("@ID_Building", SqlDbType.Int).Value = ID_Building;
            cmd.Parameters.Add("@ID_Otdelen", SqlDbType.Int).Value = ID_Otdelen;

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

        public int InsertOtdelen(int ID_Building, string NameOtdelen, string Floor)

        {
            if (String.IsNullOrEmpty(NameOtdelen))
                throw new ArgumentException("NameOtdelen cannot be null or an empty string.");

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO Otdelen " +
                                                "  (NameOtdelen, ID_Building , Floor ) " +
                                                "  Values(@NameOtdelen, @ID_Building, @Floor); " +
                                                "SELECT @ID_Otdelen = SCOPE_IDENTITY()", conn);

            cmd.Parameters.Add("@NameOtdelen", SqlDbType.VarChar, 50).Value = NameOtdelen;
//            cmd.Parameters.Add("@imgData", System.Data.SqlDbType.Image).Value = imageData;
            cmd.Parameters.Add("@ID_Building", SqlDbType.Int).Value = ID_Building;
            cmd.Parameters.Add("@Floor", SqlDbType.Int).Value = Floor;
            SqlParameter p = cmd.Parameters.Add("@ID_Otdelen", SqlDbType.Int);
            p.Direction = ParameterDirection.Output;

            int newID_Otdelen = 0;
            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();

                newID_Otdelen = (int)p.Value;
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return newID_Otdelen;
        }
        //
        // Methods that support Optimistic Concurrency checks.
        //

        // Delete the Otdelen by ID_Otdelen.

        public int DeleteOtdelen(string NameOtdelen, int original_ID, string original_NameOtdelen)
        {
            if (String.IsNullOrEmpty(original_NameOtdelen))
                throw new ArgumentException("FirstName cannot be null or an empty string.");
          
            string sqlCmd = "DELETE FROM Otdelen WHERE ID_Otdelen = @original_ID " +
                            " AND NameOtdelen = @original_NameOtdelen ";

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);

            cmd.Parameters.Add("@NameOtdelen", SqlDbType.VarChar, 50).Value = NameOtdelen;
            cmd.Parameters.Add("@original_ID", SqlDbType.Int).Value = original_ID;
            cmd.Parameters.Add("@original_NameOtdelen", SqlDbType.VarChar, 10).Value = original_NameOtdelen;

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
        // Update the Employee by original ID_Otdelen.

        public int UpdateOtdelen(int ID_Building, int original_ID_Building, string NameOtdelen, string original_NameOtdelen, int original_ID_Otdelen, string Floor, string original_Floor)
        {
            if (String.IsNullOrEmpty(NameOtdelen))
                throw new ArgumentException("FirstName cannot be null or an empty string.");
            if (ID_Building == null)
                throw new ArgumentException("Необходимо заполнять выбирать Корпус/блок");

            string sqlCmd = "UPDATE Otdelen " +
                            "  SET NameOtdelen = @NameOtdelen, ID_Building = @ID_Building " +
                            ", Floor = @Floor " +
                            "  WHERE ID_Otdelen = @original_ID_Otdelen " +
                            " AND ID_Building = @original_ID_Building" +
                            " AND Floor = @original_ID_Floor" +
                            " AND NameOtdelen = @original_NameOtdelen";

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);

            cmd.Parameters.Add("@NameOtdelen", SqlDbType.VarChar, 50).Value = NameOtdelen;
            cmd.Parameters.Add("@Floor", SqlDbType.VarChar, 50).Value = Floor;
            cmd.Parameters.Add("@original_ID_Otdelen", SqlDbType.Int).Value = original_ID_Otdelen;
            cmd.Parameters.Add("@original_ID_Floor", SqlDbType.VarChar, 50).Value = original_Floor;
            cmd.Parameters.Add("@original_NameOtdelen", SqlDbType.VarChar, 50).Value = original_NameOtdelen;

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