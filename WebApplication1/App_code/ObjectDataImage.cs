using System;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Drawing;
using System.Data.SqlTypes;
using System.Drawing.Imaging;

namespace Samples.AspNet.ObjectDataImage
{
    //
    //  Northwind Employee Data Factory
    //

    public class ImageData
    {

        private string _connectionString;
        public ImageData()
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

        public int AddEmployee(string fileType, string photoFilePath, string ID_Table, string NameTable)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
                        
            {
                SqlCommand addEmp = new SqlCommand(
                    "INSERT INTO files (fileType,fileName, fileData) " +
                    "Values(@fileType,@fileName, 0x0);" +
                    "SELECT @Identity = SCOPE_IDENTITY();" +
                    "SELECT @Pointer = TEXTPTR(fileData) FROM files WHERE ID = @Identity;"+
                    "INSERT INTO FilesRelation (ID_Files, ID_Table, NameTable) " +
                    "Values(@Identity, @ID_Table, @NameTable);",
                    connection);

                addEmp.Parameters.Add("@fileType", SqlDbType.NVarChar, 10).Value = fileType;
                addEmp.Parameters.Add("@fileName", SqlDbType.NVarChar, 20).Value = NameTable + "_" + ID_Table;
                addEmp.Parameters.Add("@ID_Table", SqlDbType.NVarChar, 20).Value = ID_Table;
                addEmp.Parameters.Add("@NameTable", SqlDbType.NVarChar, 20).Value = NameTable;

                SqlParameter idParm = addEmp.Parameters.Add("@Identity", SqlDbType.Int);
                idParm.Direction = ParameterDirection.Output;
                SqlParameter ptrParm = addEmp.Parameters.Add("@Pointer", SqlDbType.Binary, 16);
                ptrParm.Direction = ParameterDirection.Output;

//                addEmp.Parameters.Add("@fileName", SqlDbType.NVarChar, 20).Value = NameTable + "_" + (int)idParm.Value;

                connection.Open();

                addEmp.ExecuteNonQuery();

                int newEmpID = (int)idParm.Value;

                StorePhoto(photoFilePath, (byte[])ptrParm.Value, connection);
                return newEmpID;
            }
        }

        public static void StorePhoto(string fileName, byte[] pointer, SqlConnection connection)
        {
            // The size of the "chunks" of the image.
            int bufferLen = 4096;

            SqlCommand appendToPhoto = new SqlCommand(
                "UPDATETEXT files.fileData @Pointer @Offset 0 @Bytes",
                connection);

            SqlParameter ptrParm = appendToPhoto.Parameters.Add(
                "@Pointer", SqlDbType.Binary, 16);
            ptrParm.Value = pointer;
            SqlParameter photoParm = appendToPhoto.Parameters.Add(
                "@Bytes", SqlDbType.Image, bufferLen);
            SqlParameter offsetParm = appendToPhoto.Parameters.Add(
                "@Offset", SqlDbType.Int);
            offsetParm.Value = 0;

            // Read the image in and write it to the database 128 (bufferLen) bytes at a time.
            // Tune bufferLen for best performance. Larger values write faster, but
            // use more system resources.
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            byte[] buffer = br.ReadBytes(bufferLen);
            int offset_ctr = 0;

            while (buffer.Length > 0)
            {
                photoParm.Value = buffer;
                appendToPhoto.ExecuteNonQuery();
                offset_ctr += bufferLen;
                offsetParm.Value = offset_ctr;
                buffer = br.ReadBytes(bufferLen);
            }

            br.Close();
            fs.Close();
        }
        public void TestGetSqlBytes(int documentID, string filePath)
        {
            // Assumes GetConnectionString returns a valid connection string.
            SqlConnection connection =
                       new SqlConnection(_connectionString);
            {
                SqlCommand command = connection.CreateCommand();
                SqlDataReader reader = null;
                try
                {
                    // Setup the command
                    command.CommandText =
                        "SELECT fileType,fileName, fileData "
                        + "FROM files "
                        + "WHERE ID=@ID";
                    command.CommandType = CommandType.Text;

                    // Declare the parameter
                    SqlParameter paramID = new SqlParameter("@ID", SqlDbType.Int);
                    paramID.Value = documentID;
                    command.Parameters.Add(paramID);
                    connection.Open();

                    string photoName = null;

                    reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // Get the name of the file.
                            photoName = reader.GetString(1) + "_" + documentID + "." + reader.GetString(0);

                            // Ensure that the column isn't null
                            if (reader.IsDBNull(2))
                            {
                                Console.WriteLine("{0} is unavailable.", photoName);
                            }
                            else
                            {
                                SqlBytes bytes = reader.GetSqlBytes(2);
                                using (Bitmap productImage = new Bitmap(bytes.Stream))
                                {
                                    String fileName = filePath + photoName;
                                     // Save in gif format.
                                    productImage.Save(fileName, ImageFormat.Gif);
                                    Console.WriteLine("Successfully created {0}.", fileName);
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No records returned.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    if (reader != null)
                        reader.Dispose();
                }
            }
        }
        public DataTable FileRelationList(int ID_Table, string NameTable)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da =
              new SqlDataAdapter("Select fr.ID, fr.ID_files, fr.ID_Table, fr.NameTable, f.fileName, f.fileType "+ 
                                  " from FilesRelation as fr" +
                                  " Left join Files as f on f.ID=fr.ID_files "+
                                  " Where fr.ID_table = @ID_Table and fr.NameTable =@NameTable  ", conn);
            da.SelectCommand.Parameters.Add("@ID_Table", SqlDbType.Int).Value = ID_Table;
            da.SelectCommand.Parameters.Add("@NameTable", SqlDbType.NVarChar, 20).Value = NameTable;

            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                da.Fill(ds, "FileRelation");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables["FileRelation"];
        }
        public int DeleteImage(int ID_files, int ID, string fileName, string fileType)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM Files WHERE ID = @ID_files; Delete From FilesRelation where ID_files = @ID_files", conn);

            cmd.Parameters.Add("@ID_files", SqlDbType.Int).Value = ID_files;
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
        public int AddFileCoordinate(string Coordinate, int ID_Files, string AlternateText, int ID_UrlTable, string NameUrlTable)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand(
                    "INSERT INTO FileCoordinate (ID_Files,Coordinate, AlternateText, ID_UrlTable, NameUrlTable) " +
                    "Values(@ID_Files,@Coordinate, @AlternateText, @ID_UrlTable, @NameUrlTable);" +
                    "SELECT @ID = SCOPE_IDENTITY()",
                    connection);

                cmd.Parameters.Add("@Coordinate", SqlDbType.NVarChar, 500).Value = Coordinate;
                cmd.Parameters.Add("@ID_Files", SqlDbType.NVarChar, 20).Value = ID_Files;
                cmd.Parameters.Add("@AlternateText", SqlDbType.NVarChar, 70).Value = AlternateText;
                cmd.Parameters.Add("@ID_UrlTable", SqlDbType.NVarChar, 20).Value = ID_UrlTable;
                cmd.Parameters.Add("@NameUrlTable", SqlDbType.NVarChar, 30).Value = NameUrlTable;

                SqlParameter p = cmd.Parameters.Add("@ID", SqlDbType.Int);
                p.Direction = ParameterDirection.Output;

                int newID = 0;
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    newID = (int)p.Value;
                }
                catch (SqlException e)
                {
                    // Handle exception.
                }
                finally
                {
                    connection.Close();
                }

                return newID;
            }
        public DataTable FileCoordinate(int ID_files)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlDataAdapter da =
              new SqlDataAdapter("Select ID, ID_files,Coordinate, AlternateText,ID_UrlTable,NameUrlTable  " +
                                  " from FileCoordinate " +
                                  " Where ID_files = @ID_files ", conn);
            da.SelectCommand.Parameters.Add("@ID_files", SqlDbType.Int).Value = ID_files;

            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                da.Fill(ds, "FileCoordinate");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables["FileCoordinate"];
        }

        public DataTable GetTempGrid(string ID_Table, string NameTable)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            
            string sqlCmd = "SELECT fr.id as ID_FilesRelation, fr.ID_Files, f.fileName, fr.ID_Table , f.fileType , b.MapMain    " +
                "  FROM  FilesRelation as fr " +
                " Left join files as f on f.ID = fr.ID_Files " +
                " Left join Building as b on fr.ID_Table = b.ID_Building and fr.NameTable = 'Building' ";
            if (ID_Table == null)
            {
                sqlCmd += " WHERE  fr.NameTable = @NameTable";
            }
            else
            {
                sqlCmd += " WHERE  fr.NameTable = @NameTable and fr.ID_Table = @ID_Table";
            }
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);
            if (ID_Table != null)
            {
                da.SelectCommand.Parameters.Add("@ID_Table", SqlDbType.Int).Value = ID_Table;
            }
            da.SelectCommand.Parameters.Add("@NameTable", SqlDbType.NVarChar, 50).Value = NameTable;

            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                da.Fill(ds, "TempGrid");
            }
            catch (SqlException e)
            {
                // Handle exception.
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables["TempGrid"];
        }


    }
}