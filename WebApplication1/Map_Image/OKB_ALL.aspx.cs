using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Map_Image
{
    public partial class OKB_ALL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void ButtonsMap_Clicked(object sender, ImageMapEventArgs e)
        {
            // When a user clicks the "Background" hot spot,
            // display the hot spot's value.
            
            if (e.PostBackValue == "Background")
            {
               // string coordinates = Buttons.HotSpots[3].GetCoordinates();
              //  Msg.Text = "You selected the " + e.PostBackValue + "<br />" +
            //                              "The coordinates are " + coordinates;
            }
        }

        protected void btnUpload_Click(Object sender, EventArgs e)
        {
            string strFileName = MyFile.PostedFile.FileName;
            strFileName = System.IO.Path.GetFileName(strFileName);
            MyFile.PostedFile.SaveAs(Server.MapPath("../Image_Data/") + strFileName);
            string photoFilePath = Server.MapPath("../Image_Data/") + "temp." + strFileName;
            ImageObjectDataSource.InsertParameters.Clear();
            ImageObjectDataSource.InsertParameters.Add("fileType", strFileName);
            ImageObjectDataSource.InsertParameters.Add("photoFilePath", photoFilePath);  
            ImageObjectDataSource.Insert();
            lblMessage.Text = "Your file: " + strFileName + " has been uploaded successfully !";
        }
        protected void btnLoad_Click(Object sender, EventArgs e)
        {
            string photoFilePath = Server.MapPath("../Image_Data/");
            ImageObjectDataSource.SelectParameters.Clear();
            ImageObjectDataSource.SelectParameters.Add("documentID", "21");
            ImageObjectDataSource.SelectParameters.Add("filePath", photoFilePath);
            ImageObjectDataSource.Select(); 
        }
    }
}