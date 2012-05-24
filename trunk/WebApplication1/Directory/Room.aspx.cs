using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace WebApplication1.Directory
{
    public partial class Room : System.Web.UI.Page
    {
        protected void Test(object sender, EventArgs e)
        {
             
        }
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
                    RoomObjectDataSource.Update();
                    UpdateButton.Visible = false;
                    InsertButton.Visible = true;
                    DeleteButton.Visible = false;
                    break;
                case "Insert":
                    RoomObjectDataSource.Insert();
                    break;
                case "Delete":
                    RoomObjectDataSource.Delete();
                    break;
                case "SelectBuildingID":
                    Msg.Text = "Нажата кнопка Выбора";
                    ModalPopupExtender1.Show();
                    break;
                default:
                    Msg.Text = "Command name not recogized.";
                    break;

            }
        }
        protected void Update_Device_In_Room()
        {
            string Dl = "Datalist Items Checked:<br />";

            foreach (ListViewItem dli in CheckBoxImage.Items)
            {
              CheckBox chk = (CheckBox)dli.FindControl("IMGCHECK");
              if (chk.Checked)
              {
                  Dl += (chk.Text + chk.ToolTip.ToString()+" __________ "+CheckBoxImage.DataKeys[dli.DataItemIndex].Values[5] + "<br />");
              }
            }
            Msg.Text = Dl;

        }
        protected void button_filtr(object sender, EventArgs e)
        {
            if (FilterClear.Checked == false)
            {
                if (CheckBuilding.Checked == true)
                {
                    RoomObjectDataSource.FilterExpression = "ID_Building={0}";
                    RoomObjectDataSource.FilterParameters.Clear();
                    RoomObjectDataSource.FilterParameters.Add(new ControlParameter("ID_Building", "FiltrBuilding", "SelectedValue"));
                    OtdelenObjectDataSource.FilterExpression = "ID_Building={0}";
                    OtdelenObjectDataSource.FilterParameters.Clear();
                    OtdelenObjectDataSource.FilterParameters.Add(new ControlParameter("ID_Building", "FiltrBuilding", "SelectedValue"));
                }
                if(CheckOtdelen.Checked==true)
                {
                RoomObjectDataSource.FilterExpression = "ID_Otdelen={0}";
                RoomObjectDataSource.FilterParameters.Clear();
                RoomObjectDataSource.FilterParameters.Add(new ControlParameter("ID_Otdelen", "FiltrOtdelen", "SelectedValue"));
                }
                
            }
            else
            {
                RoomObjectDataSource.FilterParameters.Clear();
            }
        }
        protected void DetailsView_ItemInserted(Object sender, DetailsViewInsertedEventArgs e)
        {
            RoomGridView.DataBind();
        }
        protected void DetailsView_ItemUpdated(Object sender, DetailsViewUpdatedEventArgs e)
        {
            RoomGridView.DataBind();
        }
        protected void DetailsView_ItemDeleted(Object sender, DetailsViewDeletedEventArgs e)
        {
            RoomGridView.DataBind();
        }
        protected void GridView_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            RoomObjectDataSourceOneRow.SelectParameters["ID_Room"].DefaultValue = RoomGridView.SelectedValue.ToString();
            UpdatePan.DataBind();
            TextBox2.Text = UpdatePan.Rows[1].Cells[1].Text;
            TextBoxNum.Text = UpdatePan.Rows[2].Cells[1].Text;
            OtdelenList.SelectedValue = UpdatePan.Rows[3].Cells[1].Text;
            BuildingList.SelectedValue = UpdatePan.Rows[4].Cells[1].Text;

            RoomDeviceListDataSource.SelectMethod = "GetOneRecordTest";
            RoomDeviceListDataSource.SelectParameters.Clear();
            RoomDeviceListDataSource.SelectParameters.Add("ID_Room", RoomGridView.SelectedValue.ToString());
            RoomDeviceListDataSource.SelectParameters.Add("ID_Unit", "0");

            Load_Image();
            ImageChecked.DataBind();
            string photoFilePath = Server.MapPath("../Image_Data/");
            for (int i = 0; i < CheckBoxImage.Items.Count; i++)
            {
                if (!File.Exists(photoFilePath + CheckBoxImage.DataKeys[i].Values[2].ToString() + "_" + CheckBoxImage.DataKeys[i].Values[1].ToString() + "." + CheckBoxImage.DataKeys[i].Values[3].ToString()))
                {
                    ImageFilesObjectDataSource.SelectMethod = "TestGetSqlBytes";
                    ImageFilesObjectDataSource.SelectParameters.Clear();
                    ImageFilesObjectDataSource.SelectParameters.Add("documentID", CheckBoxImage.DataKeys[i].Values[1].ToString());
                    ImageFilesObjectDataSource.SelectParameters.Add("filePath", photoFilePath);
                    ImageFilesObjectDataSource.Select();
                }
            }

            // начало рисунков к чекбоксу
/*            
            try 
            {
                foreach (ListItem item in CheckBoxImage1.Items)
                {
                    item.Text = string.Format("<img width=20px src = \"{0}\" /> {1} , {2}", this.GetImageUrl(), item.Text, item.Value);
                }
            }
            catch (Exception ex)
            {
                this.Msg.Text = ex.ToString();
            }
*/
            // конец рисунков к чекбоксу

            ModalPopupExtender1.Show();
            UpdateButton.Visible = true;
            InsertButton.Visible = false;
            DeleteButton.Visible = true;
            AddImge.Visible = true;
        }

        private string GetImageUrl()
        { return string.Format("/Image_Data/{0}.jpeg", "Device_1_74"); }
        protected void BuildingList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            OtdelenObjectDataSource.FilterExpression = "ID_Building={0}";
            OtdelenObjectDataSource.FilterParameters.Clear();
            OtdelenObjectDataSource.FilterParameters.Add(new ControlParameter("ID_Building", "BuildingList", "SelectedValue"));

            ModalPopupExtender1.Show();
        }
        protected void Filter_device(object sender, EventArgs e)
        {
            RoomDeviceListDataSource.SelectMethod = "GetOneRecordTest";
            //RoomDeviceListDataSource.SelectParameters.Clear();
            if (RoomGridView.SelectedValue != null)
            {
               // RoomDeviceListDataSource.SelectParameters.Add("ID_Room", RoomGridView.SelectedValue.ToString());
            }
            else
            {
                //RoomDeviceListDataSource.SelectParameters.Add("ID_Room", "0");
            }
            GridView ListDevice = new GridView();
            ListDevice.DataSourceID = "RoomDeviceListDataSource";
            ListDevice.AutoGenerateColumns = true;
            ListDevice.Visible = false;
            UpdatePanel.Controls.Add(ListDevice);
            ListDevice.DataBind();
            for (int i = 0; i < CheckBoxImage.Items.Count; i++)
            {
                for (int j = 0; j < ListDevice.Rows.Count; j++)
                {
                    if (ListDevice.Rows[j].Cells[0].Text == CheckBoxImage.DataKeys[i].Values[5])
                    {
                        if (ListDevice.Rows[j].Cells[1].Text != "")
                        {
                           // CheckBoxImage.Items[i].Selected = true;
                        }
                    }
                }
            }
            ModalPopupExtender1.Show();
        }
        protected void DataSource_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            string ID_RoomNew;
            ID_RoomNew = Convert.ToString(e.ReturnValue);
            //RoomDeviceListDataSource.InsertParameters.Clear();
            RoomDeviceListDataSource.InsertParameters["ID_Room"].DefaultValue = ID_RoomNew;
            RoomDeviceListDataSource.InsertParameters["ID_Device"].DefaultValue= "";
            RoomDeviceListDataSource.InsertParameters["CountDevice"].DefaultValue = "";
            int i = 0;
            foreach (ListViewItem dli in CheckBoxImage.Items)
            {
                CheckBox chk = (CheckBox)dli.FindControl("IMGCHECK");
                TextBox tcd = (TextBox)dli.FindControl("CountDevice");
                if (chk.Checked )
                {
                    RoomDeviceListDataSource.InsertParameters["ID_Device"].DefaultValue = CheckBoxImage.DataKeys[i].Values[5].ToString();
                    RoomDeviceListDataSource.InsertParameters["CountDevice"].DefaultValue = tcd.Text;
                    RoomDeviceListDataSource.Insert();
                }
                i += 1;
            }
        }
        protected void DataSource_OnUpdated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            RoomGridView.DataBind();
            if ((int)e.ReturnValue == 0)
                Msg.Text = "Employee was not updated. Please try again.";
            RoomDeviceListDataSource.DeleteParameters.Clear();
            RoomDeviceListDataSource.DeleteParameters.Add("ID_Room", RoomGridView.SelectedValue.ToString());
            RoomDeviceListDataSource.DeleteParameters.Add("ID_Device", "");
            //RoomDeviceListDataSource.InsertParameters.Clear();
            RoomDeviceListDataSource.InsertParameters["ID_Room"].DefaultValue= RoomGridView.SelectedValue.ToString();
            RoomDeviceListDataSource.InsertParameters["ID_Device"].DefaultValue= "";
            RoomDeviceListDataSource.SelectMethod = "GetOneRecordTest";
            //RoomDeviceListDataSource.SelectParameters.Clear();
            //RoomDeviceListDataSource.SelectParameters.Add("ID_Room", RoomGridView.SelectedValue.ToString());

            GridView ListDevice = new GridView();
            ListDevice.DataSourceID = "RoomDeviceListDataSource";
            ListDevice.AutoGenerateColumns = true;
            ListDevice.Visible = false ;
            UpdatePanel.Controls.Add(ListDevice);
            ListDevice.DataBind();
            Boolean CheckBoxDeviceBoolean = new Boolean();
            int i = 0;
            foreach (ListViewItem dli in CheckBoxImage.Items)
            {
                CheckBox chk = (CheckBox)dli.FindControl("IMGCHECK");
                CheckBoxDeviceBoolean = false;
                for (int j = 0; j < ListDevice.Rows.Count; j++)
                {
                    if (ListDevice.Rows[j].Cells[0].Text == CheckBoxImage.DataKeys[i].Values[5].ToString())
                    {
                        if (Convert.ToInt32(ListDevice.Rows[j].Cells[2].Text) != 0)
                        {
                            CheckBoxDeviceBoolean = true;
                        }
                    }
                }
                if (chk.Checked && CheckBoxDeviceBoolean == false)
                {
                    RoomDeviceListDataSource.InsertParameters["ID_Device"].DefaultValue = CheckBoxImage.DataKeys[i].Values[5].ToString();
                    TextBox tcd = (TextBox)dli.FindControl("CountDevice");
                    RoomDeviceListDataSource.InsertParameters["CountDevice"].DefaultValue = tcd.Text.ToString();   
                    RoomDeviceListDataSource.Insert();
                }
                if (!chk.Checked && CheckBoxDeviceBoolean == true )
                {
                    RoomDeviceListDataSource.DeleteParameters["ID_Device"].DefaultValue = CheckBoxImage.DataKeys[i].Values[5].ToString();
                    RoomDeviceListDataSource.Delete();
                }
                if (chk.Checked && CheckBoxDeviceBoolean == true )
                {
                    RoomDeviceListDataSource.UpdateParameters["ID_Device"].DefaultValue = CheckBoxImage.DataKeys[i].Values[5].ToString();
                    TextBox tcd = (TextBox)dli.FindControl("CountDevice");
                    RoomDeviceListDataSource.UpdateParameters["CountDevice"].DefaultValue = tcd.Text;  
                    RoomDeviceListDataSource.Update();
                }
                i += 1;
            }
        }
        protected void DataSource_OnDeleted(object sender, ObjectDataSourceStatusEventArgs e)
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
            string ID_Table = RoomGridView.SelectedValue.ToString();
            string strFileName = ImageFile.PostedFile.ContentType;
            strFileName = System.IO.Path.GetFileName(strFileName);
            ImageFile.PostedFile.SaveAs(Server.MapPath("../Image_Data/")+"temp." + strFileName);
            string photoFilePath = Server.MapPath("../Image_Data/") + "temp." + strFileName;
            ImageObjectDataSource.InsertParameters.Clear();
            ImageObjectDataSource.InsertParameters.Add("ID_Table", ID_Table);
            ImageObjectDataSource.InsertParameters.Add("fileType", strFileName);
            ImageObjectDataSource.InsertParameters.Add("photoFilePath", photoFilePath);
            ImageObjectDataSource.InsertParameters.Add("NameTable", "Room");
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
            ObjectDataTempGrig.SelectParameters.Add("NameTable", "Otdelen");
            ObjectDataTempGrig.SelectParameters.Add("ID_Table", RoomGridView.DataKeys[RoomGridView.SelectedIndex].Values[1].ToString());

            MapText.Text = RoomGridView.Rows[RoomGridView.SelectedIndex].Cells[2].Text;
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
*/            ModalImageMaping.Show();
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
            ImageFilesObjectDataSource.InsertParameters.Add("ID_UrlTable", RoomGridView.SelectedValue.ToString());
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
            int wwiii = 0;
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

        protected void MapPage_Click(object sender, ImageMapEventArgs e)
        {

        }
        // мульти виев
        protected void NextButton_Command(object sender, EventArgs e)
        {
            // Determine which button was clicked
            // and set the ActiveViewIndex property to
            // the view selected by the user.
            if (MultiViewDevice.ActiveViewIndex > -1 & MultiViewDevice.ActiveViewIndex < 1)
            {
                // Increment the ActiveViewIndex property 
                // by one to advance to the next view.
                MultiViewDevice.ActiveViewIndex += 1;
            }
            else if (MultiViewDevice.ActiveViewIndex == 1)
            {
                // This is the final view.
                // The user wants to save the survey results.
                // Insert code here to save survey results.
                // Disable the navigation buttons.
                Page2Back.Enabled = false;
                Page2Next.Enabled = false;
            }
            else
            {
                throw new Exception("An error occurred.");
            }
            ModalPopupExtender1.Show(); 
        }
        protected void BackButton_Command(object sender, EventArgs e)
        {
          /*  if (MultiViewDevice.ActiveViewIndex > 0 & MultiViewDevice.ActiveViewIndex <= 2)
            {
                // Decrement the ActiveViewIndex property
                // by one to return to the previous view.
                MultiViewDevice.ActiveViewIndex -= 1;
            }
            else 
            */
            if (MultiViewDevice.ActiveViewIndex == 1)
            {
                // This is the final view.
                // The user wants to restart the survey.
                // Return to the first view.
                MultiViewDevice.ActiveViewIndex = 0;
            }
            else
            {
                throw new Exception("An error occurred.");
            }
            ModalPopupExtender1.Show();
        }

    }
}