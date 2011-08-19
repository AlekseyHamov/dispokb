using System;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Samples.AspNet.ObjectDataPerson
{
    //
    //  Northwind Employee Data Factory
    //

    public class PersonData
    {

        private string _connectionString;


        public PersonData()
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

            string sqlCmd = "SELECT p.ID_Person, p.NamePerson, p.ID_Unit, u.NameUnit FROM Person as p " +
                            " Left join Unit as u on u.ID_Unit=p.ID_Unit ";

            if (sortColumns.Trim() == "")
                sqlCmd += "ORDER BY ID_Person";
            else
                sqlCmd += "ORDER BY " + sortColumns;

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);

            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                da.Fill(ds, startRecord, maxRecords, "Person");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables["Person"];
        }


        public int SelectCount()
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Person", conn);

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
                    case "id_person":
                        break;
                    case "nameperson":
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
        public DataTable GetOneRecord(int ID_Person)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da =
              new SqlDataAdapter("SELECT ID_Person, NamePerson, ID_Unit " +
                                 "  FROM Person WHERE ID_Person = @ID_Person", conn);
            da.SelectCommand.Parameters.Add("@ID_Person", SqlDbType.Int).Value = ID_Person;

            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                da.Fill(ds, "Person");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables["Person"];
        }

        // Select Person Unit.
        public DataTable GetUnit()
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da =
              new SqlDataAdapter("SELECT Distinct Unit " +
                                 "  FROM Person ", conn);

            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                da.Fill(ds, "Person");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables["Person"];
        }

        // Delete the Otdelen by ID_Person.

        public int DeleteRecord(int ID_Person)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM Person WHERE ID_Person = @ID_Person", conn);
            cmd.Parameters.Add("@ID_Person", SqlDbType.Int).Value = ID_Person;
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

        public int UpdateRecord(int ID_Person, string NamePerson, int ID_Unit)
        {
            if (String.IsNullOrEmpty(NamePerson))
                throw new ArgumentException("Необходимо заполнять поле Наименование комнаты");
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE Person " +
                                                "  SET NamePerson=@NamePerson, ID_Unit=@ID_Unit " +
                                                 "  WHERE ID_Person=@ID_Person", conn);

            cmd.Parameters.Add("@NamePerson", SqlDbType.VarChar, 50).Value = NamePerson;
            cmd.Parameters.Add("@ID_Person", SqlDbType.Int).Value = ID_Person;
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

        public int InsertRecord(string NamePerson,int ID_Unit)

        {
            if (String.IsNullOrEmpty(NamePerson))
                throw new ArgumentException("NamePerson cannot be null or an empty string.");

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO Person " +
                                                "  (NamePerson,ID_Unit) " +
                                                "  Values(@NamePerson,@ID_Unit); " +
                                                "SELECT @ID_Person = SCOPE_IDENTITY()", conn);

            cmd.Parameters.Add("@NamePerson", SqlDbType.VarChar, 50).Value = NamePerson;
            cmd.Parameters.Add("@ID_Unit", SqlDbType.Int).Value = ID_Unit;
            SqlParameter p = cmd.Parameters.Add("@ID_Person", SqlDbType.Int);
            p.Direction = ParameterDirection.Output;

            int newID_Person = 0;

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();

                newID_Person = (int)p.Value;
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return newID_Person;
        }
           
        //
        // Methods that support Optimistic Concurrency checks.
        //

        // Delete the Otdelen by ID_Person.

        public int DeleteRecord(string NamePerson, int original_ID, string original_NamePerson)
        {
            if (String.IsNullOrEmpty(original_NamePerson))
                throw new ArgumentException("FirstName cannot be null or an empty string.");

            string sqlCmd = "DELETE FROM Person WHERE ID_Person = @original_ID " +
                            " AND NamePerson = @original_NamePerson ";

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);

            cmd.Parameters.Add("@NamePerson", SqlDbType.VarChar, 50).Value = NamePerson;
            cmd.Parameters.Add("@original_ID", SqlDbType.Int).Value = original_ID;
            cmd.Parameters.Add("@original_NamePerson", SqlDbType.VarChar, 10).Value = original_NamePerson;

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
        // Update the Employee by original ID_Person.

        public int UpdateRecord(string NamePerson, int ID_Building, int ID_Unit, string original_NamePerson, int original_ID_Person, int original_ID_Unit)
        {
            if (String.IsNullOrEmpty(NamePerson))
                throw new ArgumentException("Необходимо заполнить поле Наименование комнаты.");
            if (ID_Building == null)
                throw new ArgumentException("Необходимо выбрать Корпус");

            string sqlCmd = "UPDATE Person " +
                            "  SET NamePerson = @NamePerson, ID_Unit=@ID_Unit  " +
                            "  WHERE ID_Person = @original_ID_Person " +
                            " AND NamePerson = @original_NamePerson";

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);

            cmd.Parameters.Add("@NamePerson", SqlDbType.VarChar, 50).Value = NamePerson;
            cmd.Parameters.Add("@original_ID_Person", SqlDbType.Int).Value = original_ID_Person;
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
        
    }
}