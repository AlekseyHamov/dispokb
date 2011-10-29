using System;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Samples.AspNet.ObjectDataRoom
{
    //
    //  Northwind Employee Data Factory
    //

    public class RoomData
    {

        private string _connectionString;
        
        public RoomData()
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

            string sqlCmd = "SELECT r.ID_Room, r.NameRoom, r.ID_Otdelen, r.Num, o.NameOtdelen, b.NameBuilding, r.ID_Building, r.ID_Otdelen " +
                            " FROM Room as r " +
                            " left join otdelen as o on r.ID_Otdelen=o.ID_Otdelen " +
                            " left join Building as b on r.ID_Building=b.ID_Building ";

            if (sortColumns.Trim() == "")
                sqlCmd += "ORDER BY ID_Room";
            else
                sqlCmd += "ORDER BY " + sortColumns;

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);

            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                da.Fill(ds, startRecord, maxRecords, "Room");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables["Room"];
        }


        public DataTable GetAllNameNum(string sortColumns, int startRecord, int maxRecords)
        {
            VerifySortColumns(sortColumns);

            string sqlCmd = "SELECT r.ID_Room,(r.NameRoom+'  '+r.Num) as NameRoom , r.ID_Otdelen, r.Num, o.NameOtdelen, b.NameBuilding  FROM Room as r " + 
                            " left join otdelen as o on r.ID_Otdelen=o.ID_Otdelen "+
                            " left join Building as b on r.ID_Building=b.ID_Building ";

            if (sortColumns.Trim() == "")
                sqlCmd += "ORDER BY ID_Room";
            else
                sqlCmd += "ORDER BY " + sortColumns;

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);

            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                da.Fill(ds, startRecord, maxRecords, "Room");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables["Room"];
        }


        public int SelectCount()
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Room", conn);

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
                    case "id_room":
                        break;
                    case "nameroom":
                        break;
                    case "num":
                        break;
                    case "namebuilding":
                        break;
                    case "nameotdelen":
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
        public DataTable GetOneRecord(int ID_Room)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da =
              new SqlDataAdapter("SELECT r.ID_Room, r.NameRoom, r.Num, r.ID_Otdelen, r.ID_Building  FROM Room as r " +
                            " left join otdelen as o on r.ID_Otdelen=o.ID_Otdelen " +
                            " left join Building as b on r.ID_Building=b.ID_Building Where r.ID_Room=@ID_Room" , conn);
            da.SelectCommand.Parameters.Add("@ID_Room", SqlDbType.Int).Value = ID_Room;

            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                da.Fill(ds, "Room");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables["Room"];
        }

        // Delete the Otdelen by ID_Room.

        public int DeleteRecord(int ID_Room)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM Room WHERE ID_Room = @ID_Room", conn);
            cmd.Parameters.Add("@ID_Room", SqlDbType.Int).Value = ID_Room;
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

        public int UpdateRecord(int ID_Room, int ID_Building, int ID_Otdelen, string NameRoom,string Num)
        {
            if (String.IsNullOrEmpty(NameRoom))
                throw new ArgumentException("Необходимо заполнять поле Наименование комнаты");
            if (ID_Building == null)
                throw new ArgumentException("Необходимо заполнять выбирать Корпус/блок");

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE Room " +
                                                "  SET NameRoom=@NameRoom,ID_Building=@ID_Building, Num=@Num, ID_Otdelen=@ID_Otdelen " +
                                                 "  WHERE ID_Room=@ID_Room", conn);

            cmd.Parameters.Add("@NameRoom", SqlDbType.VarChar, 50).Value = NameRoom;
            cmd.Parameters.Add("@Num", SqlDbType.VarChar, 10).Value = Num;
            cmd.Parameters.Add("@ID_Room", SqlDbType.Int).Value = ID_Room;
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

        public int InsertRecord(string NameRoom, int ID_Building, int ID_Otdelen, string Num)

        {
            if (String.IsNullOrEmpty(NameRoom))
                throw new ArgumentException("NameRoom cannot be null or an empty string.");

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO Room " +
                                                "  (NameRoom, ID_Building, ID_Otdelen, Num) " +
                                                "  Values(@NameRoom, @ID_Building,@ID_Otdelen , @Num); " +
                                                "SELECT @ID_Room = SCOPE_IDENTITY()", conn);

            cmd.Parameters.Add("@NameRoom", SqlDbType.VarChar, 50).Value = NameRoom;
            cmd.Parameters.Add("@ID_Building", SqlDbType.Int).Value = ID_Building;
            cmd.Parameters.Add("@ID_Otdelen", SqlDbType.Int).Value = ID_Otdelen;
            cmd.Parameters.Add("@Num", SqlDbType.VarChar, 10).Value = Num;
            SqlParameter p = cmd.Parameters.Add("@ID_Room", SqlDbType.Int);
            p.Direction = ParameterDirection.Output;

            int newID_Room = 0;

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();

                newID_Room = (int)p.Value;
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return newID_Room;
        }
           
        //
        // Methods that support Optimistic Concurrency checks.
        //

        // Delete the Otdelen by ID_Room.

        public int DeleteRecord(string NameRoom, int original_ID, string original_NameRoom)
        {
            if (String.IsNullOrEmpty(original_NameRoom))
                throw new ArgumentException("FirstName cannot be null or an empty string.");

            string sqlCmd = "DELETE FROM Room WHERE ID_Room = @original_ID " +
                            " AND NameRoom = @original_NameRoom ";

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);

            cmd.Parameters.Add("@NameRoom", SqlDbType.VarChar, 50).Value = NameRoom;
            cmd.Parameters.Add("@original_ID", SqlDbType.Int).Value = original_ID;
            cmd.Parameters.Add("@original_NameRoom", SqlDbType.VarChar, 10).Value = original_NameRoom;

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

        public int UpdateRecord(string NameRoom, int ID_Building, int original_ID_Building, string original_NameRoom, int original_ID_Room)
        {
            if (String.IsNullOrEmpty(NameRoom))
                throw new ArgumentException("Необходимо заполнить поле Наименование комнаты.");
            if (ID_Building == null)
                throw new ArgumentException("Необходимо выбрать Корпус");

            string sqlCmd = "UPDATE Room " +
                            "  SET NameRoom = @NameRoom, ID_Building = @ID_Building   " +
                            "  WHERE ID_Room = @original_ID_Room " +
                            " AND ID_Building = @original_ID_Building" +
                            " AND NameRoom = @original_NameRoom";

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);

            cmd.Parameters.Add("@NameRoom", SqlDbType.VarChar, 50).Value = NameRoom;
            cmd.Parameters.Add("@original_ID_Room", SqlDbType.Int).Value = original_ID_Room;
            cmd.Parameters.Add("@original_NameRoom", SqlDbType.VarChar, 50).Value = original_NameRoom;

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