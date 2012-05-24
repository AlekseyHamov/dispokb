using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace WebApplication1.Directory
{
    public partial class Device : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PopulateRootLevel();
            }
            Msg.Text = "";
        }

        private void PopulateRootLevel()
        {

            DataTable dt = new DataTable();
            TreeDevice.SelectMethod = "GetAll";
            TreeDevice.SelectParameters.Clear();
            TreeDevice.SelectParameters.Add("ID_Unit", FiltrRadioUnit.SelectedValue);   
            DataView dv = (DataView)TreeDevice.Select();

            dt = dv.Table;

            PopulateNodes(dt, TreeView1.Nodes);
        }
        private void PopulateSubLevel(int parent_id, TreeNode parentNode)
        {
            DataTable dt = new DataTable();
            TreeDevice.SelectMethod = "GetAllParent";
            TreeDevice.SelectParameters.Clear();
            TreeDevice.SelectParameters.Add("Parent_ID", parent_id.ToString());
            TreeDevice.SelectParameters.Add("ID_Unit", FiltrRadioUnit.SelectedValue);
            DataView dv = (DataView)TreeDevice.Select();
            dt = dv.Table;
            PopulateNodes(dt, parentNode.ChildNodes);
        }
        private void PopulateNodes(DataTable dt, TreeNodeCollection nodes)
        {
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode tn = new TreeNode();
                tn.Text = dr["Text"].ToString();
                tn.Value = dr["Id"].ToString();
                nodes.Add(tn);
                //If node has child nodes, then enable on-demand populating
                tn.PopulateOnDemand = (int.Parse(dr["childnodecount"].ToString()) > 0);
            }
        }
        protected void TreeNodePopulate(Object sender, TreeNodeEventArgs e)
        {
            PopulateSubLevel(int.Parse(e.Node.Value), e.Node);
        }

        protected void Select_Change(Object sender, EventArgs e)
        {
            string str_ID = "";
            if(TreeView1.SelectedNode.ChildNodes.Count==0)
            { str_ID=TreeView1.SelectedValue; }
            else
            {
            for (int i = 0; i < TreeView1.SelectedNode.ChildNodes.Count ; i++)
            { 
                if (i==0)
                {
                    str_ID += TreeView1.SelectedNode.ChildNodes[i].Value;  
                }else if(i>0)
                {
                    str_ID += ", " + TreeView1.SelectedNode.ChildNodes[i].Value;
                }
            }
            }
            str_ID += ", " + TreeView1.SelectedValue.ToString();
            DeviceObjectDataSource.SelectParameters["str_ID"].DefaultValue = str_ID;
            GridDevice.DataBind();
        }
        protected void Select_Change1(Object sender, TreeNodeEventArgs e)
        {
            string str_ID = "";
            e.Node.Selected = true;
            for (int i = 0; i < TreeView1.SelectedNode.ChildNodes.Count; i++)
            {
                if (i == 0)
                {
                    str_ID += TreeView1.SelectedNode.ChildNodes[i].Value;
                }
                else if (i > 0)
                {
                    str_ID += ", " + TreeView1.SelectedNode.ChildNodes[i].Value;
                }
            }
            str_ID += ", " + TreeView1.SelectedValue.ToString();
            DeviceObjectDataSource.SelectParameters["str_ID"].DefaultValue = str_ID;
            GridDevice.DataBind();
        }

        private void PopulateRootLevel_Update()
        {
            DataTable dt = new DataTable();
            TreeDevice.SelectMethod = "GetAll";
            TreeDevice.SelectParameters.Clear();
            TreeDevice.SelectParameters.Add("ID_Unit", FiltrRadioUnit.SelectedValue);   
            DataView dv = (DataView)TreeDevice.Select();
            dt = dv.Table;
            PopulateNodes_Update(dt, TreeViewUpdate.Nodes);
        }
        private void PopulateSubLevel_Update(int parent_id, TreeNode parentNode)
        {
            DataTable dt = new DataTable();
            TreeDevice.SelectMethod = "GetAllParent";
            TreeDevice.SelectParameters.Clear();
            TreeDevice.SelectParameters.Add("Parent_ID", parent_id.ToString());
            TreeDevice.SelectParameters.Add("ID_Unit", FiltrRadioUnit.SelectedValue);
            DataView dv = (DataView)TreeDevice.Select();
            dt = dv.Table;
            PopulateNodes_Update(dt, parentNode.ChildNodes);
        }
        private void PopulateNodes_Update(DataTable dt, TreeNodeCollection nodes)
        {
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode tn = new TreeNode();
                tn.Text = dr["Text"].ToString();
                tn.Value = dr["Id"].ToString();
                nodes.Add(tn);
                //If node has child nodes, then enable on-demand populating
                tn.PopulateOnDemand = (int.Parse(dr["childnodecount"].ToString()) > 0);
            }
        }
        protected void TreeNodePopulate_Update(Object sender, TreeNodeEventArgs e)
        {
            PopulateSubLevel_Update(int.Parse(e.Node.Value), e.Node);
        }

        private void Selected_Unit()
        {
                for (int i = 0; i < FiltrRadioUnit.Items.Count; i++)
                {
                    if (FiltrRadioUnit.Items[i].Selected == true)
                    {
                        RadioButtonUnit.Items[i].Selected = true;
                    }
                }
        }
        protected void Button_Click_Insert(object sender, EventArgs e)
        {
            if (TreeView1.Nodes.Count == 0)
            { PopulateRootLevel(); }
            Selected_Unit();
        }
        protected void Unit_Click_Select(Object sender, EventArgs e)
        {
            Msg.Text = FiltrRadioUnit.SelectedValue;
            DeviceObjectDataSource.SelectParameters["ID_Unit"].DefaultValue = FiltrRadioUnit.SelectedValue;
            TreeView1.Nodes.Clear(); 
            PopulateRootLevel();
//            GridDevice.DataBind(); 
        }
        protected void Button_Click(object sender, EventArgs e)
        {
            Msg.Text = TreeViewUpdate.Nodes.Count.ToString(); 
            if (TreeViewUpdate.CheckedNodes.Count > 0)
            {
                // Clear the message label.
                Msg.Text = "You selected:";
                
                // Iterate through the CheckedNodes collection and display the selected nodes.
                foreach (TreeNode node in TreeViewUpdate.CheckedNodes)
                {
                    Msg.Text += node.Text ;
                }
            }
            else
            {
                Msg.Text += "  No items selected.";
            }
            ModalPopupExtender1.Show();
        }

        protected void CommandBtn_Click(Object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Update":
                    DeviceObjectDataSource.Update();
                    UpdateButton.Visible = false;
                    InsertButton.Visible = true;
                    DeleteButton.Visible = false;
                    break;
                case "Insert":
                    DeviceObjectDataSource.Insert();
                    break;
                case "Delete":
                    DeviceObjectDataSource.Delete();
                    break;
                case "SelectBuildingID":
                    Msg.Text = "Нажаата кнопка Выбора";
                    ModalPopupExtender1.Show();
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
                DeviceObjectDataSource.FilterExpression = "ID_Unit={0}";
                DeviceObjectDataSource.FilterParameters.Clear();
                DeviceObjectDataSource.FilterParameters.Add(new ControlParameter("ID_Unit", "FiltrRadioUnit", "SelectedValue"));
            }
            else
            {
                DeviceObjectDataSource.FilterParameters.Clear();
            }
        }
        protected void DetailsView_ItemInserted(Object sender, DetailsViewInsertedEventArgs e)
        {
            //DeviceGridView.DataBind();
        }
        protected void DetailsView_ItemUpdated(Object sender, DetailsViewUpdatedEventArgs e)
        {
    //        DeviceGridView.DataBind();
        }
        protected void DetailsView_ItemDeleted(Object sender, DetailsViewDeletedEventArgs e)
        {
            //DeviceGridView.DataBind();
        }

        protected void GridView_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            CheckDeviceObjectDataSource.SelectParameters["ID_Device_Spares"].DefaultValue = GridDevice.SelectedValue.ToString();    
            CheckBoxParent.DataBind();
            Selected_Unit();

            for (int i = 0; i < CheckBoxParent.Items.Count; i++)
            { CheckBoxParent.Items[i].Selected = true; }

            TextBox2.Text = GridDevice.Rows[GridDevice.SelectedIndex].Cells[2].Text;
            Description.Text = GridDevice.Rows[GridDevice.SelectedIndex].Cells[3].Text;
            if (TreeViewUpdate.Nodes.Count != TreeView1.Nodes.Count   )
            {
                TreeViewUpdate.Nodes.Clear(); 
                PopulateRootLevel_Update();
            }
            else
            {
                for (int i = 0; i < TreeViewUpdate.Nodes.Count; i++)
                {
                    if (TreeViewUpdate.Nodes[i].Checked == true)
                    {
                        TreeViewUpdate.Nodes[i].Checked = false;
                    }
                }
            }
            Load_Image();
            ModalPopupExtender1.Show();
            DivUpdatePanel.Visible = true;
            UpdateButton.Visible = true;
            InsertButton.Visible = false;
            DeleteButton.Visible = true;
            AddImge.Visible = true;
        }

        protected void OnRowChanged(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();
        }
        protected void DataSource_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            string ID_NewDevice;
            ID_NewDevice = Convert.ToString(e.ReturnValue);
            if (TreeViewUpdate.CheckedNodes.Count > 0)
            {
                Msg.Text = "Цикл TreeViewUpdate.CheckedNodes.Count";
                for (int i = 0; i < TreeViewUpdate.CheckedNodes.Count; i++ )
                {
                    if (i == 0)
                    {
                        Msg.Text = "Update";
                        TreeDeviceObjectDataSource.UpdateMethod = "UpdateRecord_Device_list";
                        TreeDeviceObjectDataSource.UpdateParameters.Clear();
                        TreeDeviceObjectDataSource.UpdateParameters.Add("ID_Device", TreeViewUpdate.CheckedNodes[i].Value);
                        TreeDeviceObjectDataSource.UpdateParameters.Add("ID_NewDevice", ID_NewDevice);
                        TreeDeviceObjectDataSource.Update();
                    }
                    else
                    {
                        Msg.Text = "iNSEERT";
                        TreeDeviceObjectDataSource.InsertMethod = "InsertRecord_Device_list";
                        TreeDeviceObjectDataSource.InsertParameters.Clear();
                        TreeDeviceObjectDataSource.InsertParameters.Add("ID_Device", TreeViewUpdate.CheckedNodes[i].Value);
                        TreeDeviceObjectDataSource.InsertParameters.Add("ID_NewDevice", ID_NewDevice);
                        TreeDeviceObjectDataSource.Insert();
                    }
                }
            }
        }
        protected void DataSource_OnUpdated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            string ID_NewDevice;
            ID_NewDevice = GridDevice.SelectedValue.ToString();
            if (TreeViewUpdate.CheckedNodes.Count > 0)
            {
//                Msg.Text = "Цикл TreeViewUpdate.CheckedNodes.Count";
                for (int i = 0; i < TreeViewUpdate.CheckedNodes.Count; i++)
                {
                    Msg.Text += " if count";
                    if (CheckBoxParent.Items.Count==0 )
                    {
//                        Msg.Text += " UpdateU";
                        TreeDeviceObjectDataSource.UpdateMethod = "UpdateRecord_Device_list";
                        TreeDeviceObjectDataSource.UpdateParameters.Clear();
                        TreeDeviceObjectDataSource.UpdateParameters.Add("ID_NewDevice", TreeViewUpdate.CheckedNodes[i].Value);
                        TreeDeviceObjectDataSource.UpdateParameters.Add("ID_Device", ID_NewDevice);
                        TreeDeviceObjectDataSource.Update();
                    }
                    else if (CheckBoxParent.Items.Count != 0)
                    {
             
//                        Msg.Text += "iNSEERTU";
                        TreeDeviceObjectDataSource.InsertMethod = "InsertRecord_Device_list";
                        TreeDeviceObjectDataSource.InsertParameters.Clear();
                        TreeDeviceObjectDataSource.InsertParameters.Add("ID_NewDevice", TreeViewUpdate.CheckedNodes[i].Value);
                        TreeDeviceObjectDataSource.InsertParameters.Add("ID_Device", ID_NewDevice);
                        TreeDeviceObjectDataSource.Insert();
                    }
                }
            }
            // удаление по крыжикам чекбокса
            for (int j = 0; j < CheckBoxParent.Items.Count; j++)
            {
                if (CheckBoxParent.Items[j].Selected == false)
                {
                    TreeDeviceObjectDataSource.DeleteMethod = "DeleteRecord_Device_list";
                    TreeDeviceObjectDataSource.DeleteParameters.Clear();
                    TreeDeviceObjectDataSource.DeleteParameters.Add("ID_Device", CheckBoxParent.Items[j].Value);
                    TreeDeviceObjectDataSource.DeleteParameters.Add("ID_NewDevice", ID_NewDevice);
                    TreeDeviceObjectDataSource.Delete();
//                    Msg.Text += " Delete";
                }
            }
//            ModalPopupExtender1.Show();
            TreeView1.Nodes.Clear();   
            PopulateRootLevel();
        }
        protected void DataSource_OnDeleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if ((int)e.ReturnValue == 0)
                Msg.Text = "Employee was not deleted. Please try again.";
        }

        protected void UpdateSparse_ItemChanged(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();
        }

        protected void UpdateSparse_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
    //        SparesGridView.DataBind();
        }
        protected void UpdateSparse_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            for (int i = 0; i < e.Values.Count; i++)
            {
                if (e.Values[i] != null)
                {
                    e.Values[i] = Server.HtmlEncode(e.Values[i].ToString());
                }
            }
        }
        protected void UpdateSparse_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
        {
      //      SparesGridView.DataBind();
        }
        protected void UpdateSparse_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            for (int i = 0; i < e.NewValues.Count; i++)
            {
                if (e.NewValues[i] != null)
                {
                    e.NewValues[i] = Server.HtmlEncode(e.NewValues[i].ToString());
                }
            }
        }
        protected void UpdateSparse_ItemDeleted(object sender, DetailsViewDeletedEventArgs e)
        {
        //    SparesGridView.DataBind();
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
            string ID_Table = GridDevice.SelectedValue.ToString();
            string strFileName = ImageFile.PostedFile.ContentType;
            strFileName = System.IO.Path.GetFileName(strFileName);
            ImageFile.PostedFile.SaveAs(Server.MapPath("../Image_Data/")+"temp." + strFileName);
            string photoFilePath = Server.MapPath("../Image_Data/") + "temp." + strFileName;
            ImageObjectDataSource.InsertParameters.Clear();
            ImageObjectDataSource.InsertParameters.Add("ID_Table", ID_Table);
            ImageObjectDataSource.InsertParameters.Add("fileType", strFileName);
            ImageObjectDataSource.InsertParameters.Add("photoFilePath", photoFilePath);
            ImageObjectDataSource.InsertParameters.Add("NameTable", "Device");
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
            ObjectDataTempGrig.SelectParameters.Add("NameTable", "Device");
            ObjectDataTempGrig.SelectParameters.Add("ID_Table", GridDevice.DataKeys[GridDevice.SelectedIndex].Values[1].ToString());

            MapText.Text = GridDevice.Rows[GridDevice.SelectedIndex].Cells[2].Text;
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
            ImageFilesObjectDataSource.InsertParameters.Add("ID_UrlTable", GridDevice.SelectedValue.ToString());
            ImageFilesObjectDataSource.InsertParameters.Add("NameUrlTable", "Device");
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