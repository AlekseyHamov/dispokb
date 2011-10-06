using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections;
using AjaxControlToolkit;

namespace WebApplication1.Directory
{
    public partial class Building : System.Web.UI.Page
    {
        /*protected void Page_Init()
        { 
            ImageButtons.Click +=  new ImageClickEventHandler(ShowImageMapingPanel); 
        }*/
        protected void Page_Load(object sender, EventArgs e)
        {
            Msg.Text = "";

            UpdateButton.Visible = false;
            InsertButton.Visible = true;
            DeleteButton.Visible = false;
        }
        protected void CommandBtn_Click(Object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Update":
                    BuildingObjectDataSource.Update();
                    InsertButton.Visible = true;
                    break;
                case "Insert":
                    BuildingObjectDataSource.Insert();
                    break;
                case "Delete":
                    BuildingObjectDataSource.Delete();
                    break;
                default:
                    Msg.Text = "Command name not recogized.";
                    break;
            }
        }
        protected void DetailsView_ItemInserted(Object sender, DetailsViewInsertedEventArgs e)
        {
            BuildingGridView.DataBind();
        }
        protected void _DetailsView_ItemUpdated(Object sender, DetailsViewUpdatedEventArgs e)
        {
            BuildingGridView.DataBind();
        }
        protected void DetailsView_ItemDeleted(Object sender, DetailsViewDeletedEventArgs e)
        {
            BuildingGridView.DataBind();
        }
        protected void GridView_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = BuildingGridView.SelectedRow;
            TextBox2.Text = row.Cells[2].Text;
            Load_Image();
            ModalPopupExtender1.Show();
            UpdateButton.Visible = true;
            InsertButton.Visible = false;
            DeleteButton.Visible = true;
        }
        protected void BuildingDataSource_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            Msg.Text = e.ReturnValue.ToString();
            string ID_Building = e.ReturnValue.ToString();
            string strFileName = ImageFile.PostedFile.ContentType ;
            strFileName = System.IO.Path.GetFileName(strFileName);
            ImageFile.PostedFile.SaveAs(Server.MapPath("../Image_Data/") + strFileName);
            string photoFilePath = Server.MapPath("../Image_Data/") + strFileName;
            ImageObjectDataSource.InsertParameters.Clear();
            ImageObjectDataSource.InsertParameters.Add("ID_Table", ID_Building);
            ImageObjectDataSource.InsertParameters.Add("fileType", strFileName);
            ImageObjectDataSource.InsertParameters.Add("photoFilePath", photoFilePath);
            ImageObjectDataSource.InsertParameters.Add("NameTable", "Building");
            ImageObjectDataSource.Insert();
        } 
        protected void BuildingDataSource_OnUpdated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            BuildingGridView.DataBind();
            if ((int)e.ReturnValue == 0)
                Msg.Text = "Employee was not updated. Please try again.";
        }
        protected void BuildingDataSource_OnDeleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if ((int)e.ReturnValue == 0)
                Msg.Text = "Employee was not deleted. Please try again.";

        }
        protected void btnUpload_Click(Object sender, EventArgs e)
        {
            string strFileName = ImageFile.PostedFile.FileName;
            strFileName = System.IO.Path.GetFileName(strFileName);
            ImageFile.PostedFile.SaveAs(Server.MapPath("../Image_Data/") + strFileName);
            string photoFilePath = Server.MapPath("../Image_Data/") + strFileName;
            ImageObjectDataSource.InsertParameters.Clear();
            ImageObjectDataSource.InsertParameters.Add("fileType", strFileName);
            ImageObjectDataSource.InsertParameters.Add("photoFilePath", photoFilePath);
            ImageObjectDataSource.Insert();
         //   Msg.Text = "Your file: " + strFileName + " has been uploaded successfully !";
        }
        protected void btnLoad_Click(Object sender, EventArgs e)
        {
            string photoFilePath = Server.MapPath("../Image_Data/");
            ImageObjectDataSource.SelectParameters.Clear();
            ImageObjectDataSource.SelectParameters.Add("documentID", "21");
            ImageObjectDataSource.SelectParameters.Add("filePath", photoFilePath);
            ImageObjectDataSource.Select();
        }
        private void Load_Image()
        {
            string photoFilePath = Server.MapPath("../Image_Data/");
            LWImage.DataBind();
            for (int i = 0; i < LWImage.Rows.Count; i++)
            {
                if (!File.Exists(photoFilePath + LWImage.DataKeys[i].Values[2].ToString() + "." + LWImage.DataKeys[i].Values[3].ToString()))
                { 
                ImageFilesObjectDataSource.SelectMethod = "TestGetSqlBytes";
                ImageFilesObjectDataSource.SelectParameters.Clear();
                ImageFilesObjectDataSource.SelectParameters.Add("documentID", LWImage.DataKeys[i].Values[1].ToString());
                ImageFilesObjectDataSource.SelectParameters.Add("filePath", photoFilePath);
                ImageFilesObjectDataSource.Select();
                }
            }
        }
        protected void LWImage_SelectedIndexChanged(Object sender, EventArgs e)
        {
            ImgButOne.ImageUrl = "~/Image_Data/" + LWImage.DataKeys[LWImage.SelectedIndex].Values[2].ToString() + "." + LWImage.DataKeys[LWImage.SelectedIndex].Values[3].ToString();
            ImgMapOne.ImageUrl = "~/Image_Data/" + LWImage.DataKeys[LWImage.SelectedIndex].Values[2].ToString() + "." + LWImage.DataKeys[LWImage.SelectedIndex].Values[3].ToString();
            ModalImageMaping.Show();
        }
        protected void ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            Msg.Text = "You clicked the ImageButton control at the coordinates: (" +
                          e.X.ToString() + ", " + e.Y.ToString() + ")";
            OX.Text = e.X.ToString();
            OY.Text = e.Y.ToString();
            if (Coordin.Text == "")
            {
                Coordin.Text = OX.Text + "," + OY.Text;
            }
            else 
            {
                Coordin.Text += "," + OX.Text + "," + OY.Text;
            }

            ModalImageMaping.Show();
        }
        protected void DelCoordinate_Click(Object sender, EventArgs e)
        {
            PolygonHotSpot Ph = new PolygonHotSpot();
            Ph.AlternateText = "Тест";
            Ph.Coordinates = Coordin.Text;
            ImgMapOne.HotSpots.Add(Ph);
            ImgButOne.Visible = false;
            ImgMapOne.Visible = true;
            ModalImageMaping.Show();
        }

        private void Load_Images(string ID_Building)
        {
            ImageObjectDataSource.SelectMethod = "FileRelationList";
            ImageObjectDataSource.SelectParameters.Clear();
            ImageObjectDataSource.SelectParameters.Add("ID_Table", ID_Building);
            ImageObjectDataSource.SelectParameters.Add("NameTable", "Building");
            
            GridView LWImage = new GridView();
            LWImage.DataSourceID = "ImageObjectDataSource";
            LWImage.AutoGenerateColumns = true;
            UpdatePanel.Controls.Add(LWImage);
            LWImage.Visible = false; 
            LWImage.DataBind();

            string photoFilePath = Server.MapPath("../Image_Data/");
            string NameFife = "";
            for (int i = 0; i < LWImage.Rows.Count; i++)
            {
                ImageMap ImageSecond = new ImageMap();
                ImageSecond.ID = LWImage.Rows[i].Cells[4].Text.ToString();
                NameFife = "~/Image_Data/" + LWImage.Rows[i].Cells[4].Text.ToString() + "." + LWImage.Rows[i].Cells[5].Text.ToString();
                ImageSecond.ImageUrl = NameFife;
                ImageSecond.Width = Convert.ToInt16(Label4.Text.ToString());
                ImageSecond.ImageAlign = ImageAlign.Left;
                ImageDiv.Controls.Add(ImageSecond);
            }
        }
        protected void Plus_Minus_Click(Object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Plus":
                    ModalPopupExtender1.Show();
                    int  t = Convert.ToInt16(Label4.Text.ToString());
                    t = t + (t * 30 / 100);
                    Label4.Text = t.ToString();
                    //Load_Image(BuildingGridView.SelectedValue.ToString());
                    if (t > 300 && ImageDiv.Style.Value == "overflow-x:scroll; text-align:left;")
                    {
                        ImageDiv.Style.Value = "overflow-x:scroll; text-align:left;width:420px;height:300px"; 
                    }
                    break;
                case "Minus":
                    ModalPopupExtender1.Show();
                    int  tt = Convert.ToInt16(Label4.Text.ToString());
                    tt = tt - (tt * 30 / 100);
                    Label4.Text = tt.ToString() ;
                    //Load_Image(BuildingGridView.SelectedValue.ToString());
                    if (tt < 300 && ImageDiv.Style.Value == "overflow-x:scroll; text-align:left;width:420px;height:300px")
                    {
                        ImageDiv.Style.Value = "overflow-x:scroll; text-align:left;";
                    }
                    break;
                default:
                    Msg.Text = "Command name not recogized.";
                    break;
            }
        }
    }
}