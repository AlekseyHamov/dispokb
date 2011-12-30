using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO; 

namespace WebApplication1.Directory
{
    public partial class Person : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Msg.Text = "";
        }
        protected void CommandBtn_Click(Object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Update":
                    PersonObjectDataSource.Update();
                    UpdateButton.Visible = false;
                    InsertButton.Visible = true;
                    DeleteButton.Visible = false;
                    break;
                case "Insert":
                    PersonObjectDataSource.Insert();
                    break;
                case "Delete":
                    PersonObjectDataSource.Delete();
                    break;
                default:
                    Msg.Text = "Command name not recogized.";
                    break;
            }
        }
        protected void button_filtr(object sender, EventArgs e)
        {
            if (FilterClear.Checked == false)
            {
                PersonObjectDataSource.FilterExpression = "ID_Unit={0}";
                PersonObjectDataSource.FilterParameters.Clear();
                PersonObjectDataSource.FilterParameters.Add(new ControlParameter("ID_Unit", "FiltrRadioUnit", "SelectedValue"));
            }
            else
            {
                PersonObjectDataSource.FilterParameters.Clear();
            }
        }
        protected void Insert_Unit(Object sender, EventArgs e)
        {
            UnitObjectDataSource.Insert();
            ModalPopupExtender1.Show();
        }
        protected void DataSource_OnInserted_Unit(object sender, ObjectDataSourceStatusEventArgs e)
        {

        }
        protected void DetailsView_ItemInserted(Object sender, DetailsViewInsertedEventArgs e)
        {
            GridView.DataBind();
        }
        protected void DetailsView_ItemUpdated(Object sender, DetailsViewUpdatedEventArgs e)
        {
            GridView.DataBind();
        }
        protected void DetailsView_ItemDeleted(Object sender, DetailsViewDeletedEventArgs e)
        {
            GridView.DataBind();
        }
        protected void GridView_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePan.DataBind();
            TextBox2.Text = UpdatePan.Rows[1].Cells[1].Text;
            try
            {
                int VUnit;
                VUnit = Convert.ToInt16(UpdatePan.Rows[2].Cells[1].Text);
                RadioButtonUnit.SelectedValue = UpdatePan.Rows[2].Cells[1].Text;
            }
            catch (FormatException)
            {
                Msg.Text = "Поле подразделение ранее было пустым";
            }
            Load_Image();
            ModalPopupExtender1.Show();
            UpdateButton.Visible = true;
            InsertButton.Visible = false;
            DeleteButton.Visible = true;
            AddImge.Visible = true;
        }
        protected void DetailsObjectDataSource_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            // OtdelenObjectDataSource.SelectParameters["ID_Otdelen"].DefaultValue =
            // e.ReturnValue.ToString();
            //OtdelenGridView.DataBind();


            /*kmj  FileUpload upload = UpdateOtdelenPanel.FindControl("FileUpload") as FileUpload;
              using (System.IO.BinaryReader reader = new System.IO.BinaryReader(upload.PostedFile.InputStream))
              {
                  byte[] bytes = new byte[upload.PostedFile.ContentLength];
                  reader.Read(bytes, 0, bytes.Length);
                  e.["imgData"] = bytes;
              }*/
        }
        protected void DetailsObjectDataSource_OnUpdated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            GridView.DataBind();
            if ((int)e.ReturnValue == 0)
                Msg.Text = "Employee was not updated. Please try again.";

        }
        protected void DetailsObjectDataSource_OnDeleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if ((int)e.ReturnValue == 0)
                Msg.Text = "Employee was not deleted. Please try again.";

        }
        // работа с картинками
        protected void LWImage_SelectedIndexChanged(Object sender, EventArgs e)
        {
            ImgButOne.ImageUrl = "~/Image_Data/" + LWImage.DataKeys[LWImage.SelectedIndex].Values[2].ToString() + "_" + LWImage.DataKeys[LWImage.SelectedIndex].Values[1].ToString() + "." + LWImage.DataKeys[LWImage.SelectedIndex].Values[3].ToString();
            ImgMapOne.ImageUrl = "~/Image_Data/" + LWImage.DataKeys[LWImage.SelectedIndex].Values[2].ToString() + "_" + LWImage.DataKeys[LWImage.SelectedIndex].Values[1].ToString() + "." + LWImage.DataKeys[LWImage.SelectedIndex].Values[3].ToString();
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
        protected void Image_OnInserted(Object sender, EventArgs e)
        {
            string ID_Table = GridView.SelectedValue.ToString();
            string strFileName = ImageFile.PostedFile.ContentType;
            strFileName = System.IO.Path.GetFileName(strFileName);
            ImageFile.PostedFile.SaveAs(Server.MapPath("../Image_Data/")+"temp." + strFileName);
            string photoFilePath = Server.MapPath("../Image_Data/") + "temp." + strFileName;
            ImageObjectDataSource.InsertParameters.Clear();
            ImageObjectDataSource.InsertParameters.Add("ID_Table", ID_Table);
            ImageObjectDataSource.InsertParameters.Add("fileType", strFileName);
            ImageObjectDataSource.InsertParameters.Add("photoFilePath", photoFilePath);
            ImageObjectDataSource.InsertParameters.Add("NameTable", "Person");
            ImageObjectDataSource.Insert();
        }
        protected void ImageDataSource_OnDeleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if ((int)e.ReturnValue == 0)
                Msg.Text = "Employee was not deleted. Please try again.";

        }
        protected void MapRelation_Click(object sender, EventArgs e)
        {
            string photoFilePath = Server.MapPath("../Image_Data/");
            ObjectDataTempGrig.SelectParameters.Clear();
            ObjectDataTempGrig.SelectParameters.Add("NameTable", "Person");
            ObjectDataTempGrig.SelectParameters.Add("ID_Table", GridView.DataKeys[GridView.SelectedIndex].Values[1].ToString());

            MapText.Text = GridView.Rows[GridView.SelectedIndex].Cells[2].Text;
            //      GridView TempGrid = new GridView();
            TempGrid.DataSourceID = "ObjectDataTempGrig";
            TempGrid.DataKeyNames = new string[] { "ID_Table", "ID_Files", "fileName", "fileType" };
            //        ImageMapingPanel.Controls.Add(TempGrid);
            TempGrid.Visible = false;
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
            ChangeMap.Visible = true;
            /*            Image img =
                            Image.FromFile(Server.MapPath(ImgMapOne.ImageUrl));
                        if (img.Height > img.Width)
                        {
                            ImageBM.Style.Add("height", "500px");
                        }
                        else { ImageBM.Style.Add("height", "auto"); }
            */
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
        protected void DelCoordinate_Click(Object sender, EventArgs e)
        {
            ChangeMap.Visible = false;

            ImageFilesObjectDataSource.InsertMethod = "AddFileCoordinate";
            ImageFilesObjectDataSource.InsertParameters.Clear();
            ImageFilesObjectDataSource.InsertParameters.Add("Coordinate", Coordin.Text);

            ImageFilesObjectDataSource.InsertParameters.Add("ID_Files", TempGrid.DataKeys[0].Values[1].ToString());

            ImageFilesObjectDataSource.InsertParameters.Add("AlternateText", MapText.Text.ToString());
            ImageFilesObjectDataSource.InsertParameters.Add("ID_UrlTable", GridView.SelectedValue.ToString());
            ImageFilesObjectDataSource.InsertParameters.Add("NameUrlTable", "Otdelen");
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

    }
}