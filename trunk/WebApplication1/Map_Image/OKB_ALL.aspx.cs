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
            if (!IsPostBack)
            {
                DataSet dataSet = new DataSet();
                dataSet.Tables.Add("Table");
                dataSet.Tables[0].Columns.Add("ID", typeof(string));
                dataSet.Tables[0].Columns.Add("Parent_ID", typeof(string));
                dataSet.Tables[0].Columns.Add("Text", typeof(string));
                DataRow row = dataSet.Tables[0].NewRow();
                for (int i = 0; i < Gr.Rows.Count; i++)
                {
                    Msg.Text = "1";
                    row = dataSet.Tables[0].NewRow();
                    row["ID"] = Gr.Rows[i].Cells[0].Text;
                    if (Gr.Rows[i].Cells[1].Text == "&nbsp;")
                    {
                        row["Parent_ID"] = null;
                    }else {
                    row["Parent_ID"] = Gr.Rows[i].Cells[1].Text;
                    }
                    row["Text"] = Gr.Rows[i].Cells[2].Text;
                    dataSet.Tables[0].Rows.Add(row);
                }

                // You can use this:
                TreeView1.DataSource = new HierarchicalDataSet("View_Dv_list", "ID", "Parent_ID");
                
                // OR you can use the extensions for TreeView if you are using .NET 3.5
                //TreeView1.SetDataSourceFromDataSet(dataSet, "ID", "ParentID");

                // OR This line, will load the tree starting from the parent record of value = 1
                //TreeView1.DataSource = new HierarchicalDataSet(dataSet, "ID", "ParentID", 1);

                TreeView1.DataBind();
                TreeView1.CollapseAll();
                Gr.Visible = false;
            }

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
            string photoFilePath = Server.MapPath("../Image_Data/") + strFileName;
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