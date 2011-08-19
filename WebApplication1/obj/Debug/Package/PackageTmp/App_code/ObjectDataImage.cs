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

        public int AddEmployee(string fileType, string photoFilePath)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
                        
            {
                SqlCommand addEmp = new SqlCommand(
                    "INSERT INTO files (fileType, fileData) " +
                    "Values(@fileType, 0x0);" +
                    "SELECT @Identity = SCOPE_IDENTITY();" +
                    "SELECT @Pointer = TEXTPTR(fileData) FROM files WHERE ID = @Identity",
                    connection);

                addEmp.Parameters.Add("@fileType", SqlDbType.NVarChar, 20).Value = fileType;

                SqlParameter idParm = addEmp.Parameters.Add("@Identity", SqlDbType.Int);
                idParm.Direction = ParameterDirection.Output;
                SqlParameter ptrParm = addEmp.Parameters.Add("@Pointer", SqlDbType.Binary, 16);
                ptrParm.Direction = ParameterDirection.Output;

                connection.Open();

                addEmp.ExecuteNonQuery();

                int newEmpID = (int)idParm.Value;

                StorePhoto(photoFilePath, (byte[])ptrParm.Value, connection);

                return newEmpID;
            }
        }

        public static void StorePhoto(string fileName, byte[] pointer,
            SqlConnection connection)
        {
            // The size of the "chunks" of the image.
            int bufferLen = 128;

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
                        "SELECT fileType, fileData "
                        + "FROM files "
                        + "WHERE ID=@ID";
                    command.CommandType = CommandType.Text;

                    // Declare the parameter
                    SqlParameter paramID =
                        new SqlParameter("@ID", SqlDbType.Int);
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
                            photoName = reader.GetString(0);

                            // Ensure that the column isn't null
                            if (reader.IsDBNull(1))
                            {
                                Console.WriteLine("{0} is unavailable.", photoName);
                            }
                            else
                            {
                                SqlBytes bytes = reader.GetSqlBytes(1);
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
    }
}