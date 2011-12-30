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
        public TextBox Coordinats ; 
        protected void Page_Load(object sender, EventArgs e)
        {
            Msg.Text = "";
            Coordinats = new TextBox();
            UpdateButton.Visible = false;
            InsertButton.Visible = true;
            DeleteButton.Visible = false;
            
            MapPage.Visible = true;

            if (!Page.IsPostBack)
            {
                string photoFilePath = Server.MapPath("../Image_Data/");
                for (int j = 0; j < BuildingGridView.Rows.Count; j++)
                {
                    if (BuildingGridView.DataKeys[j].Values[1].ToString() == "True")
                    {
                        ObjectDataTempGrig.SelectParameters.Clear();
                        ObjectDataTempGrig.SelectParameters.Add("NameTable", "Building");
                        ObjectDataTempGrig.SelectParameters.Add("ID_Table", BuildingGridView.DataKeys[j].Values[0].ToString());
                        break;
                    }
                }
                TempGrid.DataSourceID = "ObjectDataTempGrig";
                TempGrid.DataKeyNames = new string[] { "ID_Table", "ID_Files", "fileName", "fileType", "MapMain" };
                TempGrid.DataBind();

                BoundField PageImageGrid = new BoundField();
                PageImageGrid.HeaderText = "Связанные элементы";
                PageImageGrid.DataField = "AlternateText";
                PageImageGrid.Visible = false;

                GridView HotSpotsGrid = new GridView();
                HotSpotsGrid.DataSourceID = "FileCoordimateDataSource";
                HotSpotsGrid.AutoGenerateColumns = false;
                HotSpotsGrid.DataKeyNames = new string[] { "ID", "ID_files", "Coordinate", "AlternateText" };
                HotSpotsGrid.Columns.Add(PageImageGrid);
                HotSpotsGrid.Width = 40;
                DivRightPage.Controls.Add(HotSpotsGrid);

                HotSpotsGrid.DataBind();

                for (int i = 0; i < HotSpotsGrid.Rows.Count; i++)
                {
                    PolygonHotSpot Ph = new PolygonHotSpot();
                    Ph.AlternateText = HotSpotsGrid.DataKeys[i].Values[3].ToString();
                    Ph.Coordinates = HotSpotsGrid.DataKeys[i].Values[2].ToString();
                    MapPage.HotSpots.Add(Ph);
                }

                for (int i = 0; i < TempGrid.Rows.Count; i++)
                {
                    if (!File.Exists(photoFilePath + TempGrid.DataKeys[i].Values[2].ToString() + "_" + TempGrid.DataKeys[i].Values[1].ToString() + "." + TempGrid.DataKeys[i].Values[3].ToString()))
                    {
                        ImageFilesObjectDataSource.SelectMethod = "TestGetSqlBytes";
                        ImageFilesObjectDataSource.SelectParameters.Clear();
                        ImageFilesObjectDataSource.SelectParameters.Add("documentID", TempGrid.DataKeys[i].Values[1].ToString());
                        ImageFilesObjectDataSource.SelectParameters.Add("filePath", photoFilePath);
                        ImageFilesObjectDataSource.Select();
                    }
                    MapPage.ImageUrl = "~/Image_Data/" + TempGrid.DataKeys[i].Values[2].ToString() + "_" + TempGrid.DataKeys[i].Values[1].ToString() + "." + TempGrid.DataKeys[i].Values[3].ToString();
                    break;
                }

            }
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
            ModalPopupExtender1.Show();
            GridViewRow row = BuildingGridView.SelectedRow;
            TextBox2.Text = row.Cells[2].Text;
            Load_Image();
            AddImge.Visible = true;
            UpdateButton.Visible = true;
            InsertButton.Visible = false;
            DeleteButton.Visible = true;
        }
        protected void BuildingDataSource_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {   
            string ID_Building = e.ReturnValue.ToString();
            string strFileName = ImageFile.PostedFile.ContentType;
            strFileName = System.IO.Path.GetFileName(strFileName);
            ImageFile.PostedFile.SaveAs(Server.MapPath("../Image_Data/")+"temp." + strFileName);
            string photoFilePath = Server.MapPath("../Image_Data/") + "temp." + strFileName;
            ImageObjectDataSource.InsertParameters.Clear();
            ImageObjectDataSource.InsertParameters.Add("ID_Table", ID_Building);
            ImageObjectDataSource.InsertParameters.Add("fileType", strFileName);
            ImageObjectDataSource.InsertParameters.Add("photoFilePath", photoFilePath);
            ImageObjectDataSource.InsertParameters.Add("NameTable", "Building");
            ImageObjectDataSource.Insert();
        }
        protected void Image_OnInserted(Object sender, EventArgs e)
        {
            string ID_Building = BuildingGridView.SelectedValue.ToString(); 
            string strFileName = ImageFile.PostedFile.ContentType; 
            strFileName = System.IO.Path.GetFileName(strFileName);
            ImageFile.PostedFile.SaveAs(Server.MapPath("../Image_Data/")+"temp." + strFileName);
            string photoFilePath = Server.MapPath("../Image_Data/") + "temp." + strFileName;
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
        protected void ImageDataSource_OnDeleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if ((int)e.ReturnValue == 0)
                Msg.Text = "Employee was not deleted. Please try again.";

        }
        protected void btnUpload_Click(Object sender, EventArgs e)
        {
            string strFileName = ImageFile.PostedFile.FileName;
            strFileName = System.IO.Path.GetFileName(strFileName);
            ImageFile.PostedFile.SaveAs(Server.MapPath("../Image_Data/")+"temp." + strFileName);
            string photoFilePath = Server.MapPath("../Image_Data/") + "temp." + strFileName;
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
            MapPage.Visible = false;
            for (int i = 0; i < LWImage.Rows.Count; i++)
            {
                if (!File.Exists(photoFilePath + LWImage.DataKeys[i].Values[2].ToString() + "_" + LWImage.DataKeys[i].Values[1].ToString() + "." + LWImage.DataKeys[i].Values[3].ToString()))
                { 
                ImageFilesObjectDataSource.SelectMethod = "TestGetSqlBytes";
                ImageFilesObjectDataSource.SelectParameters.Clear();
                ImageFilesObjectDataSource.SelectParameters.Add("documentID", LWImage.DataKeys[i].Values[1].ToString());
                ImageFilesObjectDataSource.SelectParameters.Add("filePath", photoFilePath);
                ImageFilesObjectDataSource.Select();
                }
                MapPage.Visible = true;
                FileCoordimateDataSource.SelectParameters.Clear();
                FileCoordimateDataSource.SelectParameters.Add("ID_files", LWImage.DataKeys[0].Values[1].ToString());
                MapPage.ImageUrl = "~/Image_Data/" + LWImage.DataKeys[0].Values[2].ToString() + "_" + LWImage.DataKeys[0].Values[1].ToString() + "." + LWImage.DataKeys[0].Values[3].ToString();
            }
            MapPage.HotSpots.Clear();

            ImageChildren.DataBind();
            ImgMapOne.HotSpots.Clear();
            for (int i = 0; i < ImageChildren.Rows.Count; i++)
            {
                PolygonHotSpot Ph = new PolygonHotSpot();
                Ph.AlternateText = ImageChildren.DataKeys[i].Values[3].ToString();
                Ph.Coordinates = ImageChildren.DataKeys[i].Values[2].ToString();
                MapPage.HotSpots.Add(Ph);
            }
        }
        protected void LWImage_SelectedIndexChanged(Object sender, EventArgs e)
        {
            ImgButOne.ImageUrl = "~/Image_Data/" + LWImage.DataKeys[LWImage.SelectedIndex].Values[2].ToString() + "_" + LWImage.DataKeys[LWImage.SelectedIndex].Values[1].ToString() + "." + LWImage.DataKeys[LWImage.SelectedIndex].Values[3].ToString();
            ImgMapOne.ImageUrl = "~/Image_Data/" + LWImage.DataKeys[LWImage.SelectedIndex].Values[2].ToString() + "_" + LWImage.DataKeys[LWImage.SelectedIndex].Values[1].ToString() + "." + LWImage.DataKeys[LWImage.SelectedIndex].Values[3].ToString();
            FileCoordimateDataSource.SelectParameters.Clear();
            FileCoordimateDataSource.SelectParameters.Add("ID_files", LWImage.DataKeys[LWImage.SelectedIndex].Values[1].ToString());
            ImageChildren.DataBind();
            ImgMapOne.HotSpots.Clear();
            for (int i = 0; i < ImageChildren.Rows.Count; i++)
            {
                PolygonHotSpot Ph = new PolygonHotSpot();
                Ph.AlternateText = ImageChildren.DataKeys[i].Values[3].ToString();
                Ph.Coordinates = ImageChildren.DataKeys[i].Values[2].ToString();
                ImgMapOne.HotSpots.Add(Ph);
            }
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
            ChangeMap.Visible = false;

            ImageFilesObjectDataSource.InsertMethod = "AddFileCoordinate";
            ImageFilesObjectDataSource.InsertParameters.Clear();
            ImageFilesObjectDataSource.InsertParameters.Add("Coordinate", Coordin.Text);
            for (int j = 0; j < BuildingGridView.Rows.Count; j++)
            {
                if (BuildingGridView.DataKeys[j].Values[1].ToString() == "True")
                {
                    ImageFilesObjectDataSource.InsertParameters.Add("ID_Files", TempGrid.DataKeys[0].Values[1].ToString());
                }
            }
            ImageFilesObjectDataSource.InsertParameters.Add("AlternateText", MapText.Text.ToString());
            ImageFilesObjectDataSource.InsertParameters.Add("ID_UrlTable", BuildingGridView.SelectedValue.ToString());
            ImageFilesObjectDataSource.InsertParameters.Add("NameUrlTable", "Building");
            ImageFilesObjectDataSource.Insert();
            ImageChildren.DataBind();
            ImgMapOne.HotSpots.Clear();
            for (int i = 0; i < ImageChildren.Rows.Count; i++)
            {
                PolygonHotSpot Ph = new PolygonHotSpot();
                Ph.AlternateText = ImageChildren.DataKeys[i].Values[3].ToString();
                Ph.Coordinates = ImageChildren.DataKeys[i].Values[2].ToString();
                ImgMapOne.HotSpots.Add(Ph);
            }
            Coordin.Text = "";
            ChangeMap.Checked = false;
            ImgButOne.Visible = false;
            ImgMapOne.Visible = true;
            UnitMap.Visible = false;
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
        protected void ChangeMap_CheckedChanged(object sender, EventArgs e)
        {
            Coordin.Text = "";
            if (ChangeMap.Checked == false)
            {
                ImgButOne.Visible = false;
                ImgMapOne.Visible = true;
                UnitMap.Visible = false;
            }
            else
            {
                ImgButOne.Visible = true;
                ImgMapOne.Visible = false;
                UnitMap.Visible = true;
            }
            ModalImageMaping.Show();
        }
        protected void MapRelation_Click(object sender, EventArgs e)
        {
            string photoFilePath = Server.MapPath("../Image_Data/");

            for (int j = 0; j < BuildingGridView.Rows.Count; j++)
            {
                if (BuildingGridView.DataKeys[j].Values[1].ToString() == "True")
                {
                    ObjectDataTempGrig.SelectParameters.Clear();
                    ObjectDataTempGrig.SelectParameters.Add("NameTable", "Building");
                    ObjectDataTempGrig.SelectParameters.Add("ID_Table", BuildingGridView.DataKeys[j].Values[0].ToString());
                    break;
                }
            }
            MapText.Text = BuildingGridView.Rows[BuildingGridView.SelectedIndex].Cells[2].Text;
            TempGrid.DataSourceID = "ObjectDataTempGrig";
            TempGrid.DataKeyNames = new string[]{"ID_Table", "ID_Files", "fileName", "fileType" ,"MapMain"};
            TempGrid.DataBind();
            for (int i = 0; i < TempGrid.Rows.Count; i++)
            {
                    if (!File.Exists(photoFilePath + TempGrid.DataKeys[i].Values[2].ToString() + "_" + TempGrid.DataKeys[i].Values[1].ToString() + "." + TempGrid.DataKeys[i].Values[3].ToString()))
                    {
                        ImageFilesObjectDataSource.SelectMethod = "TestGetSqlBytes";
                        ImageFilesObjectDataSource.SelectParameters.Clear();
                        ImageFilesObjectDataSource.SelectParameters.Add("documentID", TempGrid.DataKeys[i].Values[1].ToString());
                        ImageFilesObjectDataSource.SelectParameters.Add("filePath", photoFilePath);
                        ImageFilesObjectDataSource.Select();
                    }
                    ImgButOne.ImageUrl = "~/Image_Data/" + TempGrid.DataKeys[i].Values[2].ToString() + "_" + TempGrid.DataKeys[i].Values[1].ToString() + "." + TempGrid.DataKeys[i].Values[3].ToString();
                    ImgMapOne.ImageUrl = "~/Image_Data/" + TempGrid.DataKeys[i].Values[2].ToString() + "_" + TempGrid.DataKeys[i].Values[1].ToString() + "." + TempGrid.DataKeys[i].Values[3].ToString();
                    break;
            }
            ModalImageMaping.Show();
        }
    }
}