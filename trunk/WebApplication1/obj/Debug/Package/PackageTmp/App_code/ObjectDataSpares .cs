using System;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Samples.AspNet.ObjectDataSpares
{
    //
    //  Northwind Employee Data Factory
    //

    public class SparesData
    {

        private string _connectionString;


        public SparesData()
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

        public DataTable GetAll(string sortColumns, int startRecord, int maxRecords)
        {
            VerifySortColumns(sortColumns);

            string sqlCmd = "SELECT ID_Spares, NameSpares, Description, ID_Device  FROM Spares  ";
 
            if (sortColumns.Trim() == "")
                sqlCmd += "ORDER BY ID_Spares";
            else
                sqlCmd += "ORDER BY " + sortColumns;

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);

            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                da.Fill(ds, startRecord, maxRecords, "Spares");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables["Spares"];
        }

        public DataTable GetForDevice(string sortColumns, int startRecord, int maxRecords, int ID_Device)
        {
            VerifySortColumns(sortColumns);

            string sqlCmd = "SELECT ID_Spares, NameSpares, Description, ID_Device  FROM Spares Where ID_Device=@ID_Device  ";
           
            
            if (sortColumns.Trim() == "")
                sqlCmd += "ORDER BY ID_Spares";
            else
                sqlCmd += "ORDER BY " + sortColumns;

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);

            da.SelectCommand.Parameters.Add("@ID_Device", SqlDbType.Int).Value = ID_Device;

            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                da.Fill(ds, startRecord, maxRecords, "Spares");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables["Spares"];
        }

        public int SelectCount(int ID_Device)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Spares where ID_Device=@ID_Device", conn);

            cmd.Parameters.Add("@ID_Device", SqlDbType.Int).Value = ID_Device;
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
                    case "id_spares":
                        break;
                    case "namespares":
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
        public DataTable GetOneRecord(int ID_Spares)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da =
              new SqlDataAdapter("SELECT ID_Spares, NameSpares, Description, ID_Device " +
                                 "  FROM Spares WHERE ID_Spares = @ID_Spares", conn);
            da.SelectCommand.Parameters.Add("@ID_Spares", SqlDbType.Int).Value = ID_Spares;

            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                da.Fill(ds, "Spares");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables["Spares"];
        }

        // Delete the Otdelen by ID_Room.

        public int DeleteRecord(int ID_Spares)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM Spares WHERE ID_Spares = @ID_Spares", conn);
            cmd.Parameters.Add("@ID_Spares", SqlDbType.Int).Value = ID_Spares;
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

        public int UpdateRecord1(int ID_Spares, string NameSpares, string Description, int ID_Device)
        {
            if (String.IsNullOrEmpty(NameSpares))
                throw new ArgumentException("Необходимо заполнять поле Наименование комнаты");

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE Spares " +
                                                "  SET NameSpares=@NameSpares, Description=@Description, ID_Devcei=@ID_Device " +
                                                 "  WHERE ID_Spares=@ID_Spares ", conn);

            cmd.Parameters.Add("@NameSpares", SqlDbType.VarChar, 50).Value = NameSpares;
            cmd.Parameters.Add("@Description", SqlDbType.VarChar, 50).Value = Description;
            cmd.Parameters.Add("@ID_Device", SqlDbType.Int).Value = ID_Device;
            cmd.Parameters.Add("@ID_Spares", SqlDbType.Int).Value = ID_Spares;
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

        public int InsertRecord(string NameSpares, string Description, int ID_Device)

        {
            if (String.IsNullOrEmpty(NameSpares))
                throw new ArgumentException("NameRoom cannot be null or an empty string.");

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO Spares " +
                                                "  (NameSpares,Description, ID_Device) " +
                                                "  Values(@NameSpares, @Description, @ID_Device); " +
                                                "SELECT @ID_Spares = SCOPE_IDENTITY()", conn);

            cmd.Parameters.Add("@NameSpares", SqlDbType.VarChar, 50).Value = NameSpares;
            cmd.Parameters.Add("@ID_Device", SqlDbType.Int).Value = ID_Device;
            cmd.Parameters.Add("@Description", SqlDbType.VarChar, 150).Value = Description;
            SqlParameter p = cmd.Parameters.Add("@ID_Spares", SqlDbType.Int);
            p.Direction = ParameterDirection.Output;

            int newID_Spares = 0;

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();

                newID_Spares = (int)p.Value;
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return newID_Spares;
        }
           
        //
        // Methods that support Optimistic Concurrency checks.
        //

        // Delete the Otdelen by ID_Room.

        public int DeleteRecord(int ID_Spares,int original_ID_Spares,string original_NameSpares,string original_Description)
        {
//            if (String.IsNullOrEmpty(original_NameSpares))
//                throw new ArgumentException("FirstName cannot be null or an empty string.");

            string sqlCmd = "DELETE FROM Spares WHERE ID_Spares = @original_ID_Spares " +
                            " AND NameSpares = @original_NameSpares ";

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);

            cmd.Parameters.Add("@original_NameSpares", SqlDbType.VarChar, 50).Value = original_NameSpares;
            cmd.Parameters.Add("@original_ID_Spares", SqlDbType.Int).Value = original_ID_Spares;
            cmd.Parameters.Add("@original_Description", SqlDbType.VarChar, 150).Value = original_Description;

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
        // Update the Employee by original ID_Room.

        public int UpdateRecord(int ID_Sapres,int ID_Device,string NameSpares,string Description,string original_NameSpares,string original_Description, 
            int original_ID_Spares)
        {
            if (String.IsNullOrEmpty(NameSpares))
                throw new ArgumentException("Необходимо заполнить поле Наименование комнаты.");

            string sqlCmd = "UPDATE Spares " +
                            "  SET NameSpares = @NameSpares , Description=@Description  " +
                            "  WHERE ID_Spares = @original_ID_Spares " +
                            " AND NameSpares = @original_NameSpares";

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);

            cmd.Parameters.Add("@NameSpares", SqlDbType.VarChar, 50).Value = NameSpares;
            cmd.Parameters.Add("@Description", SqlDbType.VarChar, 200).Value = Description;
            cmd.Parameters.Add("@ID_Device", SqlDbType.Int).Value = ID_Device;
            cmd.Parameters.Add("@original_ID_Spares", SqlDbType.Int).Value = original_ID_Spares;
            cmd.Parameters.Add("@original_NameSpares", SqlDbType.VarChar, 50).Value = original_NameSpares;
            cmd.Parameters.Add("@original_Description", SqlDbType.VarChar, 200).Value = original_Description;

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