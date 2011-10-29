using System;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Samples.AspNet.ObjectDataDevice
{
    //
    //  Northwind Employee Data Factory
    //

    public class DeviceData
    {

        private string _connectionString;
        public DeviceData()
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

        public DataTable GetAll(string str_ID, string ID_Unit, string sortColumns, int startRecord, int maxRecords)
        {
            VerifySortColumns(sortColumns);

            string sqlCmd = " SELECT distinct Device.ID_Device,Device_list.Device as Parent_ID, Device.NameDevice, Device.Description, Device.ID_Unit, Unit.NameUnit, Device.CheckLog " +
                " FROM Device "+
                " Left join Unit on Unit.ID_Unit=Device.ID_Unit " +
                " Left join Device_list on Device_list.Device_Spares=Device.ID_Device ";

            sqlCmd += " where 1=1 "; 
            try
            {
                if (str_ID.Trim() != "")
                { sqlCmd += " and Device.ID_Device in ( " + str_ID + " ) "; }
            }
            catch { }
            try
            {
                if (ID_Unit.Trim() != "")
                { sqlCmd += " and Device.ID_Unit=" + ID_Unit + " "; }
            }
            catch { }

            if (sortColumns.Trim() == "")
                sqlCmd += "ORDER BY Device.ID_Device DESC ";
            else
                sqlCmd += "ORDER BY " + sortColumns;

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);

            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                da.Fill(ds, startRecord, maxRecords, "Device");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables["Device"];
            
        }
        public DataTable GetView()
        {
            string sqlCmd = " Select ID, Parent_ID, Text from dbo.View_Dv_list";

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);

            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                da.Fill(ds, "Device");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables["Device"];

        }
        public DataTable GetForCheck(int ID_Device_Spares)
        {
            string sqlCmd = " Select dv_l.ID_DV_List, dv.NameDevice, dv_l.Device from Device_list as dv_l "+
                            "Left Join Device as dv on dv.ID_Device=dv_l.Device "+
                            "Left Join Device as dv_s on dv_s.ID_Device=dv_l.Device_Spares "+
                            "Where dv_l.Device is not null and dv_l.Device_Spares = @ID_Device_Spares ";

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);
            da.SelectCommand.Parameters.Add("@ID_Device_Spares", SqlDbType.Int).Value = ID_Device_Spares;
            
            DataSet ds = new DataSet();

            try
            {
                conn.Open();
                da.Fill(ds, "Device");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables["Device"];

        }
        public int SelectCount(string str_ID, string ID_Unit)
        {

            string sqlCmd = "";
                sqlCmd = "SELECT COUNT(*) FROM Device ";
                sqlCmd += " where 1=1 ";
                try
                {
                    if (str_ID.Trim() != "")
                    { sqlCmd += " and Device.ID_Device in ( " + str_ID + " ) "; }
                }
                catch { }
                try
                {
                    if (ID_Unit.Trim() != "")
                    { sqlCmd += " and Device.ID_Unit=" + ID_Unit + " "; }
                }
                catch { }

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(sqlCmd , conn);

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
                    case "namedevice":
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
        public DataTable GetOneRecord(int ID_Device)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da =
              new SqlDataAdapter("SELECT ID_Device, NameDevice, Description, ID_Unit " +
                                 "  FROM Device WHERE ID_Device = @ID_Device", conn);
            da.SelectCommand.Parameters.Add("@ID_Device", SqlDbType.Int).Value = ID_Device;

            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                da.Fill(ds, "Device");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables["Device"];
        }

        // Delete the Otdelen by ID_Room.
        public int DeleteRecord(int ID_Device)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM Device WHERE ID_Device = @ID_Device", conn);
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
        public int UpdateRecord(int ID_Device, string NameDevice, string Description, int ID_Unit)
        {
            if (String.IsNullOrEmpty(NameDevice))
                throw new ArgumentException("Необходимо заполнять поле Наименование комнаты");

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE Device " +
                                                "  SET NameDevice=@NameDevice, Description=@Description, ID_Unit=@ID_Unit " +
                                                 "  WHERE ID_Device=@ID_Device ", conn);

            cmd.Parameters.Add("@NameDevice", SqlDbType.VarChar, 50).Value = NameDevice;
            cmd.Parameters.Add("@Description", SqlDbType.VarChar, 50).Value = Description;
            cmd.Parameters.Add("@ID_Unit", SqlDbType.Int).Value = ID_Unit;
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
        public int UpdateRecord_Device_list(int ID_Device, int ID_NewDevice)
        {
           // if (Int.IsNullOrEmpty(ID_NewDevice))
           //     throw new ArgumentException("Новая запись не создана");

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE Device_list " +
                                                "  SET Device=@ID_NewDevice " +
                                                 "  WHERE Device_Spares=@ID_Device ", conn);

            cmd.Parameters.Add("@ID_NewDevice", SqlDbType.Int).Value = ID_NewDevice;
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
        public int InsertRecord_Device_list(int ID_Device, int ID_NewDevice)
        {
            //if (String.IsNullOrEmpty(NameDevice))
            //    throw new ArgumentException("NameRoom cannot be null or an empty string.");

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO Device_list " +
                                                "  (Device,Device_Spares) " +
                                                "  Values(@ID_NewDevice, @ID_Device); " +
                                                "SELECT @ID_DV_List = SCOPE_IDENTITY()", conn);
            cmd.Parameters.Add("@ID_Device", SqlDbType.Int).Value = ID_Device;
            cmd.Parameters.Add("@ID_NewDevice", SqlDbType.Int).Value = ID_NewDevice;
            SqlParameter p = cmd.Parameters.Add("@ID_DV_List", SqlDbType.Int);
            p.Direction = ParameterDirection.Output;

            int newID_DV_List = 0;

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();

                newID_DV_List = (int)p.Value;
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return newID_DV_List;
        }
        public int DeleteRecord_Device_list(int ID_Device, int ID_NewDevice)
        {
            
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_Delete_Device_list";
            cmd.Parameters.Add("@Device_Spares", SqlDbType.Int).Value = ID_NewDevice;
            cmd.Parameters.Add("@Device", SqlDbType.Int).Value = ID_Device;
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
        public int InsertRecord(string NameDevice, string Description, int ID_Unit)

        {
            if (String.IsNullOrEmpty(NameDevice))
                throw new ArgumentException("NameRoom cannot be null or an empty string.");

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO Device " +
                                                "  (NameDevice,Description, ID_unit) " +
                                                "  Values(@NameDevice, @Description, @ID_Unit); " +
                                                "SELECT @ID_Device = SCOPE_IDENTITY()", conn);

            cmd.Parameters.Add("@NameDevice", SqlDbType.VarChar, 50).Value = NameDevice;
            cmd.Parameters.Add("@ID_Unit", SqlDbType.Int).Value = ID_Unit;
            cmd.Parameters.Add("@Description", SqlDbType.VarChar, 200).Value = Description;
            SqlParameter p = cmd.Parameters.Add("@ID_Device", SqlDbType.Int);
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

        public int DeleteRecord(string NameDevice, string Description, int original_ID,
                                string original_NameDevice, string original_Description)
        {
            if (String.IsNullOrEmpty(original_NameDevice))
                throw new ArgumentException("FirstName cannot be null or an empty string.");

            string sqlCmd = "DELETE FROM Device WHERE ID_Device = @original_ID " +
                            " AND NameDevice = @original_NameDevice ";

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);

            cmd.Parameters.Add("@NameDevice", SqlDbType.VarChar, 50).Value = NameDevice;
            cmd.Parameters.Add("@original_ID", SqlDbType.Int).Value = original_ID;
            cmd.Parameters.Add("@original_NameDevice", SqlDbType.VarChar, 10).Value = original_NameDevice;

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

        public int UpdateRecord(string NameDevice, string Description
                                , string original_NameDevice, int original_ID_Device, string original_Description)
        {
            if (String.IsNullOrEmpty(NameDevice))
                throw new ArgumentException("Необходимо заполнить поле Наименование комнаты.");

            string sqlCmd = "UPDATE Device " +
                            "  SET NameDevice = @NameDevice , Description=@Description  " +
                            "  WHERE ID_Device = @original_ID_Device " +
                            " AND NameDevice = @original_NameDevice";

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);

            cmd.Parameters.Add("@NameDevice", SqlDbType.VarChar, 50).Value = NameDevice;
            cmd.Parameters.Add("@Description", SqlDbType.VarChar, 200).Value = Description;
            cmd.Parameters.Add("@original_ID_Device", SqlDbType.Int).Value = original_ID_Device;
            cmd.Parameters.Add("@original_NameDevice", SqlDbType.VarChar, 50).Value = original_NameDevice;
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