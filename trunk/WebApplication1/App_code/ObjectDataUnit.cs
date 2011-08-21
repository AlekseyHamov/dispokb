using System;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Samples.AspNet.ObjectDataUnit
{
    //
    //  Northwind Employee Data Factory
    //

    public class UnitData
    {

        private string _connectionString;


        public UnitData()
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

        public DataTable GetAll(string sortColumns, int startRecord, int maxRecords)
        {
            VerifySortColumns(sortColumns);

            string sqlCmd = "SELECT ID_Unit, NameUnit  FROM Unit ";

            if (sortColumns.Trim() == "")
                sqlCmd += "ORDER BY ID_Unit";
            else
                sqlCmd += "ORDER BY " + sortColumns;

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);

            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                da.Fill(ds, startRecord, maxRecords, "Unit");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables["Unit"];
        }


        public int SelectCount()
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Unit", conn);

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
                    case "id_unit":
                        break;
                    case "nameunit":
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
        public DataTable GetOneRecord(int ID_Unit)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da =
              new SqlDataAdapter("SELECT ID_Unit, NameUnit " +
                                 "  FROM Unit WHERE ID_Unit = @ID_Unit", conn);
            da.SelectCommand.Parameters.Add("@ID_Unit", SqlDbType.Int).Value = ID_Unit;

            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                da.Fill(ds, "Unit");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables["Unit"];
        }

        // Delete the Otdelen by ID_Unit.

        public int DeleteRecord(int ID_Unit)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM Unit WHERE ID_Unit = @ID_Unit", conn);
            cmd.Parameters.Add("@ID_Unit", SqlDbType.Int).Value = ID_Unit;
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

        public int UpdateRecord(int ID_Unit, string NameUnit)
        {
            if (String.IsNullOrEmpty(NameUnit))
                throw new ArgumentException("Необходимо заполнять поле Наименование комнаты");

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE Unit " +
                                                "  SET NameUnit=@NameUnit " +
                                                 "  WHERE ID_Unit=@ID_Unit", conn);

            cmd.Parameters.Add("@NameUnit", SqlDbType.VarChar, 50).Value = NameUnit;
            cmd.Parameters.Add("@ID_Unit", SqlDbType.Int).Value = ID_Unit;
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

        public int InsertRecord(string NameUnit)

        {
            if (String.IsNullOrEmpty(NameUnit))
                throw new ArgumentException("NameUnit cannot be null or an empty string.");

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO Unit " +
                                                "  (NameUnit) " +
                                                "  Values(@NameUnit); " +
                                                "SELECT @ID_Unit = SCOPE_IDENTITY()", conn);

            cmd.Parameters.Add("@NameUnit", SqlDbType.VarChar, 50).Value = NameUnit;
            SqlParameter p = cmd.Parameters.Add("@ID_Unit", SqlDbType.Int);
            p.Direction = ParameterDirection.Output;

            int newID_Unit = 0;

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();

                newID_Unit = (int)p.Value;
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return newID_Unit;
        }
           
        //
        // Methods that support Optimistic Concurrency checks.
        //

        // Delete the Otdelen by ID_Unit.

        public int DeleteRecord(string NameUnit, int original_ID, string original_NameUnit)
        {
            if (String.IsNullOrEmpty(original_NameUnit))
                throw new ArgumentException("FirstName cannot be null or an empty string.");

            string sqlCmd = "DELETE FROM Unit WHERE ID_Unit = @original_ID " +
                            " AND NameUnit = @original_NameUnit ";

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);

            cmd.Parameters.Add("@NameUnit", SqlDbType.VarChar, 50).Value = NameUnit;
            cmd.Parameters.Add("@original_ID", SqlDbType.Int).Value = original_ID;
            cmd.Parameters.Add("@original_NameUnit", SqlDbType.VarChar, 10).Value = original_NameUnit;

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
        // Update the Employee by original ID_Unit.

        public int UpdateRecord(string NameUnit, string original_NameUnit, int original_ID_Unit)
        {
            if (String.IsNullOrEmpty(NameUnit))
                throw new ArgumentException("Необходимо заполнить поле Наименование комнаты.");

            string sqlCmd = "UPDATE Unit " +
                            "  SET NameUnit = @NameUnit " +
                            "  WHERE ID_Unit = @original_ID_Unit " +
                            " AND NameUnit = @original_NameUnit";

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);

            cmd.Parameters.Add("@NameUnit", SqlDbType.VarChar, 50).Value = NameUnit;
            cmd.Parameters.Add("@original_ID_Unit", SqlDbType.Int).Value = original_ID_Unit;
            cmd.Parameters.Add("@original_NameUnit", SqlDbType.VarChar, 50).Value = original_NameUnit;

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