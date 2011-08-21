using System;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Samples.AspNet.ObjectDataTreeDevice
{
    //
    //  Northwind Employee Data Factory
    //

    public class TreeDeviceData
    {
        private string _connectionString;
        public TreeDeviceData()
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
        public DataTable GetAll(string ID_Unit )
        {

            string sqlCmd = "select Id, Text, (select count(*) FROM View_Dv_list WHERE Parent_Id=sc.id) childnodecount FROM View_Dv_list sc where Parent_Id IS NULL ";
            try
            {
                if (ID_Unit.Trim() != "")
                { sqlCmd += " and ID_Unit = " + ID_Unit + " "; }
            }
            catch { }
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);

//            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
//                da.Fill(ds, "Device");
                da.Fill(dt);
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            //return ds.Tables["Device"];
            return dt;
        }
        public DataTable GetAllParent(string ID_Unit, int Parent_ID)
        {

            string sqlCmd = "select Id, Text, (select count(*) FROM View_Dv_list WHERE Parent_Id=sc.id) childnodecount "+
                " FROM View_Dv_list sc where Parent_Id = @Parent_Id";
            try
            {
                if (ID_Unit.Trim() != "")
                { sqlCmd += " and ID_Unit = " + ID_Unit + " "; }
            }
            catch { }

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);
            da.SelectCommand.Parameters.Add("@Parent_ID", SqlDbType.Int).Value = Parent_ID;

            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                da.Fill(dt);
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
    }
}