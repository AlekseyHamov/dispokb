using System;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Samples.AspNet.ObjectDataBuilding
{
    //
    //  Northwind Employee Data Factory
    //

    public class BuildingData
    {

        private string _connectionString;


        public BuildingData()
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

        public DataTable GetAllBuilding(string sortColumns, int startRecord, int maxRecords)
        {
            VerifySortColumns(sortColumns);

            string sqlCmd = "SELECT ID_Building, NameBuilding, MapMain FROM Building  ";

            if (sortColumns.Trim() == "")
                sqlCmd += "ORDER BY ID_Building";
            else
                sqlCmd += "ORDER BY " + sortColumns;

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);

            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                da.Fill(ds, startRecord, maxRecords, "Building");
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

        public DataTable GetBuildingTempGrid(int ID_Building)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            string sqlCmd = "SELECT b.ID_Building, b.NameBuilding, b.MapMain, fr.id as ID_FilesRelation, fr.ID_Files, f.fileName, f.fileType    " +
                "  FROM Building as b  "+
                " Left join FilesRelation as fr on fr.ID_Table = b.ID_Building and fr.NameTable = 'Building'" +
                " Left join files as f on f.ID = fr.ID_Files " +
                                 "WHERE b.ID_Building = @ID_Building";

            SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);
            da.SelectCommand.Parameters.Add("@ID_Building", SqlDbType.Int).Value = ID_Building;

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
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Building", conn);

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
                    case "id_building":
                        break;
                    case "namebuilding":
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
        public DataTable GetBuilding(int ID_Building)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da =
              new SqlDataAdapter("SELECT ID_Building, NameBuilding,MapMain " +
                                 "  FROM Building WHERE ID_Building = @ID_Building", conn);
            da.SelectCommand.Parameters.Add("@ID_Building", SqlDbType.Int).Value = ID_Building;

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

        // Delete the Otdelen by ID_Building.

        public int DeleteBuilding(int ID_Building)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM Building WHERE ID_Building = @ID_Building", conn);
            cmd.Parameters.Add("@ID_Building", SqlDbType.Int).Value = ID_Building;
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

        public int UpdateBuilding(int ID_Building, string NameBuilding)
        {
            if (String.IsNullOrEmpty(NameBuilding))
                throw new ArgumentException("FirstName cannot be null or an empty string.");

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE Building " +
                                                "  SET NameBuilding=@NameBuilding" +
                                                 "  WHERE ID_Building=@ID_Building", conn);

            cmd.Parameters.Add("@NameBuilding", SqlDbType.VarChar, 50).Value = NameBuilding;
            cmd.Parameters.Add("@ID_Building", SqlDbType.Int).Value = ID_Building;

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

        public int InsertBuilding(string NameBuilding)

        {
            if (String.IsNullOrEmpty(NameBuilding))
                throw new ArgumentException("NameBuilding cannot be null or an empty string.");

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO Building " +
                                                "  (NameBuilding) " +
                                                "  Values(@NameBuilding); " +
                                                "SELECT @ID_Building = SCOPE_IDENTITY()", conn);

            cmd.Parameters.Add("@NameBuilding", SqlDbType.VarChar, 50).Value = NameBuilding;
            SqlParameter p = cmd.Parameters.Add("@ID_Building", SqlDbType.Int);
            p.Direction = ParameterDirection.Output;

            int newID_Building = 0;

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();

                newID_Building = (int)p.Value;
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return newID_Building;
        }
           
        //
        // Methods that support Optimistic Concurrency checks.
        //

        // Delete the Otdelen by ID_Building.

        public int DeleteBuilding(string NameBuilding, int original_ID, string original_NameBuilding)
        {
            if (String.IsNullOrEmpty(original_NameBuilding))
                throw new ArgumentException("FirstName cannot be null or an empty string.");

            string sqlCmd = "DELETE FROM Building WHERE ID_Building = @original_ID " +
                            " AND NameBuilding = @original_NameBuilding ";

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);

            cmd.Parameters.Add("@NameBuilding", SqlDbType.VarChar, 50).Value = NameBuilding;
            cmd.Parameters.Add("@original_ID", SqlDbType.Int).Value = original_ID;
            cmd.Parameters.Add("@original_NameBuilding", SqlDbType.VarChar, 10).Value = original_NameBuilding;

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
        // Update the Employee by original ID_Building.

        public int UpdateBuilding(string NameBuilding, string original_NameBuilding, int original_ID_Building)
        {
            if (String.IsNullOrEmpty(NameBuilding))
                throw new ArgumentException("FirstName cannot be null or an empty string.");

            string sqlCmd = "UPDATE Building " +
                            "  SET NameBuilding = @NameBuilding" +
                            "  WHERE ID_Building = @original_ID_Building " +
                            " AND NameBuilding = @original_NameBuilding";

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);

            cmd.Parameters.Add("@NameBuilding", SqlDbType.VarChar, 50).Value = NameBuilding;
            cmd.Parameters.Add("@original_ID_Building", SqlDbType.Int).Value = original_ID_Building;
            cmd.Parameters.Add("@original_NameBuilding", SqlDbType.VarChar, 50).Value = original_NameBuilding;

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