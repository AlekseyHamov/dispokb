using System;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Samples.AspNet.ObjectDataClaimSparesList
{
    public class ClaimSparesListData
    {
        private string _connectionString;
        public ClaimSparesListData()
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
            string sqlCmd = "SELECT ID_Room_Device_list, ID_Room,ID_device  FROM ClaimSparesList  ";
            if (sortColumns.Trim() == "")
                sqlCmd += "ORDER BY ID_Device";
            else
                sqlCmd += "ORDER BY " + sortColumns;
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                da.Fill(ds, startRecord, maxRecords, "Room_Device_list");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }
            return ds.Tables["Room_Device_list"];
        }

        public int SelectCount()
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Room_Device_list", conn);
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
                    case "id_device":
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
        public DataTable GetOneRecord(int ID_Device,int ID_Room)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da =
            new SqlDataAdapter("SELECT ID_Room_Device_List, ID_Device, ID_Room FROM Room_Device_List WHERE ID_Room = @ID_Room and ID_Device = @ID_Device", conn);
            da.SelectCommand.Parameters.Add("@ID_Device", SqlDbType.Int).Value = ID_Device;
            da.SelectCommand.Parameters.Add("@ID_Room", SqlDbType.Int).Value = ID_Room;

            DataSet ds = new DataSet();

            try
            {
                conn.Open();
                da.Fill(ds, "Room_Device_List");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }
            return ds.Tables["Room_Device_List"];
        }
 
        public DataTable GetOneClaimRecord(int ID_Claim)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da =
            new SqlDataAdapter("SELECT Cl.ID_ClaimSparesList, Cl.ID_Claim, Cl.ID_Spares, D.ID_Device FROM ClaimSparesList as Cl " +
                " Left join Spares as S on Cl.ID_Spares=S.ID_Spares "+
                " Left join Device as D on S.ID_Device=D.ID_Device Where ID_Claim=@ID_Claim", conn);
            da.SelectCommand.Parameters.Add("@ID_Claim", SqlDbType.Int).Value = ID_Claim;

            DataSet ds = new DataSet();

            try
            {
                conn.Open();
                da.Fill(ds, "Room_Device_List");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }
            return ds.Tables["Room_Device_List"];
        }

        // Delete the Otdelen by ID_Room.

        public int DeleteRecord(int ID_ClaimSparesList)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM ClaimSparesList WHERE ID_ClaimSparesList = @ID_ClaimSparesList ", conn);
            cmd.Parameters.Add("@ID_ClaimSparesList", SqlDbType.Int).Value = ID_ClaimSparesList;

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

        public int UpdateRecord(int ID_Device, int ID_Room_Device_List, int ID_Room)
        {

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE Room_Device_List " +
                                                "  SET ID_Device=@ID_Device, ID_Room=@ID_Room " +
                                                 "  WHERE ID_Room_Device_List=@ID_Room_Device_List ", conn);

            cmd.Parameters.Add("@ID_Device", SqlDbType.Int).Value = ID_Device;
            cmd.Parameters.Add("@ID_Room", SqlDbType.Int).Value = ID_Room;
            cmd.Parameters.Add("@ID_Room_Device_List", SqlDbType.Int).Value = ID_Room_Device_List;
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

        public int InsertRecord(int ID_Claim, int ID_Spares)

        {

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO ClaimSparesList " +
                                                "  (ID_Claim,ID_Spares ) " +
                                                "  Values(@ID_Claim, @ID_Spares); " +
                                                "SELECT @ID_ClaimSparesList = SCOPE_IDENTITY()", conn);

            cmd.Parameters.Add("@ID_Claim", SqlDbType.Int).Value = ID_Claim;
            cmd.Parameters.Add("@ID_Spares", SqlDbType.Int).Value = ID_Spares;
            SqlParameter p = cmd.Parameters.Add("@ID_ClaimSparesList", SqlDbType.Int);
            p.Direction = ParameterDirection.Output;

            int newID_Device = 0;

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();

                newID_Device = (int)p.Value;
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return newID_Device;
        }
           
        //
        // Methods that support Optimistic Concurrency checks.
        //

        // Delete the Otdelen by ID_Room.

        public int DeleteRecord(int original_ID, int original_ID_Device, int original_ID_Claim)
        {
            string sqlCmd = "DELETE FROM ClaimSparesList WHERE ID_ClaimSparesList = @original_ID ";

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);

            cmd.Parameters.Add("@original_ID", SqlDbType.Int).Value = original_ID;

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

        public int UpdateRecord(int ID_Device, int ID_Room,int ID_Room_Device_List
                                ,int original_ID_Room_Device_List, int original_ID_Device, int original_ID_Room)
        {

            string sqlCmd = "UPDATE Room_Device_List " +
                            "  SET ID_Room = @ID_Room , ID_Device=@ID_Device  " +
                            "  WHERE ID_Room_Device_List = @ID_Room_Device_List ";

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);

            cmd.Parameters.Add("@ID_Room", SqlDbType.Int).Value = ID_Room;
            cmd.Parameters.Add("@ID_Device", SqlDbType.Int).Value = ID_Device;
            cmd.Parameters.Add("@original_ID_Room_Device_List", SqlDbType.Int).Value = original_ID_Room_Device_List;
            cmd.Parameters.Add("@original_ID_Device", SqlDbType.Int).Value = original_ID_Device;
            cmd.Parameters.Add("@original_ID_Room", SqlDbType.Int).Value = original_ID_Room;

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