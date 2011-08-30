using System;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Samples.AspNet.ObjectDataRoomDeviceList
{
    public class RoomDeviceListData
    {
        private string _connectionString;
        public RoomDeviceListData()
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
            string sqlCmd = "SELECT  rdl.ID_Room_Device_list,rdl.ID_Room, rdl.ID_device, d.ID_unit "+
                            " FROM Room_Device_list as rdl "+
                            " left join Device as d on d.ID_Device=rdl.ID_device ";
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
 
        public DataTable GetOneRecordTest(int ID_Room, int ID_Unit)
        {
            string sqlCmd = "SELECT d.ID_Device, r.ID_Room_Device_List, r.ID_Room, d.NameDevice,ID_Unit FROM Device as d " +
            " Left join Room_Device_List as r on r.ID_Device = d.ID_Device WHERE 1=1  ";

            if (ID_Room != 0)
            {
                sqlCmd += " and  r.ID_Room = @ID_Room ";
            }
            if (ID_Unit != 0)
            {
                sqlCmd += " and d.ID_Unit = @ID_Unit " ; 
            }

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);
            
            da.SelectCommand.Parameters.Add("@ID_Room", SqlDbType.Int).Value = ID_Room;
            da.SelectCommand.Parameters.Add("@ID_Unit", SqlDbType.Int).Value = ID_Unit;

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

        public int DeleteRecord(int ID_Room,int ID_Device)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM Room_Device_List WHERE ID_Room = @ID_Room and ID_Device = @ID_Device ", conn);
            cmd.Parameters.Add("@ID_Room", SqlDbType.Int).Value = ID_Room;
            cmd.Parameters.Add("@ID_Device", SqlDbType.Int).Value = ID_Device;
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

        public int InsertRecord(int ID_Device, int ID_Room)

        {

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO Room_Device_List " +
                                                "  (ID_Device,ID_Room ) " +
                                                "  Values(@ID_Device, @ID_Room); " +
                                                "SELECT @ID_Room_Device_List = SCOPE_IDENTITY()", conn);

            cmd.Parameters.Add("@ID_Device", SqlDbType.Int).Value = ID_Device;
            cmd.Parameters.Add("@ID_Room", SqlDbType.Int).Value = ID_Room;
            SqlParameter p = cmd.Parameters.Add("@ID_Room_Device_List", SqlDbType.Int);
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

        public int DeleteRecord(int original_ID,
                                int original_ID_Device, int original_ID_Room)
        {

            string sqlCmd = "DELETE FROM Room_Device_List WHERE ID_Room_Device_List = @original_ID ";

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