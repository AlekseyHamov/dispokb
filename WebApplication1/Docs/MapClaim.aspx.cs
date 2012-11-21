using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

namespace WebApplication1.Directory
{
    public partial class MapClaim : System.Web.UI.Page
    {
        ArrayList arrayList = new ArrayList();

        protected void Page_Load(object sender, EventArgs e)
        {
            MapPage.Visible = true;
            TempGrid.DataSourceID = "ObjectDataTempGrig";
            TempGrid.DataKeyNames = new string[] { "ID_Table", "ID_Files", "fileName", "fileType", "MapMain" };
            TempGrid.DataBind();
                    for (int i = 0; i < TempGrid.Rows.Count; i++)
                    {
                        FileCoordimateDataSource.SelectParameters.Clear();
                        FileCoordimateDataSource.SelectParameters.Add("ID_files", TempGrid.DataKeys[i].Values[1].ToString());
                    }
                    string photoFilePath = Server.MapPath("../Image_Data/");
                    for (int i = 0; i < TempGrid.Rows.Count; i++)
                    {
                        if (TempGrid.DataKeys[i].Values[4].ToString() == "True")
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
                    //поле для HotSpotsGrid
                    BoundField PageImageGrid = new BoundField();
                    PageImageGrid.HeaderText = "Связанные элементы";
                    PageImageGrid.DataField = "AlternateText";
                    PageImageGrid.Visible = true;
                    // для заполения ссылок и их названий 
                    GridView HotSpotsGrid = new GridView();
                    HotSpotsGrid.DataSourceID = "FileCoordimateDataSource";
                    HotSpotsGrid.AutoGenerateColumns = false;
                    HotSpotsGrid.DataKeyNames = new string[] { "ID", "ID_files", "Coordinate", "AlternateText", "ID_UrlTable" };
                    HotSpotsGrid.Columns.Add(PageImageGrid);
                    HotSpotsGrid.Visible = false;
                    HotSpotsGrid.Width = 40;
                    DivRightPage.Controls.Add(HotSpotsGrid);

                    HotSpotsGrid.DataBind();

                    for (int i = 0; i < HotSpotsGrid.Rows.Count; i++)
                    {
                        PolygonHotSpot Ph = new PolygonHotSpot();
                        Ph.AlternateText = HotSpotsGrid.DataKeys[i].Values[3].ToString();
                        Ph.Coordinates = HotSpotsGrid.DataKeys[i].Values[2].ToString();
                        Ph.PostBackValue = HotSpotsGrid.DataKeys[i].Values[4].ToString();
                        MapPage.HotSpots.Add(Ph);
                    }

        }
        // Вызов по ссылке на карте
        protected void VoteMap_Clicked(Object sender, ImageMapEventArgs e)
        {
            switch (Name_Url.Text.ToString())
            {
            case "Otdelen":
                Url_Map(e.PostBackValue.ToString(), "Room");
                DeviceMain_room(e.PostBackValue.ToString());
                Name_Url.Text = "Room";
                break;
            case"Building_child":
                Url_Map(e.PostBackValue.ToString(), "Otdelen");
                Name_Url.Text = "Otdelen";
                break;
            case "Building":
                Url_Map(e.PostBackValue.ToString(), "Building");
                Name_Url.Text = "Building_child";
                break;
            }

            if (IDLinkClaim.Rows.Count > 0)
            {
                for (int i = 0; i < IDLinkClaim.Rows.Count; i++)
                {
                    arrayList.Add(new Item() { code = i+1, value = IDLinkClaim.Rows[i].Cells[1].Text.ToString(), id = IDLinkClaim.Rows[i].Cells[2].Text.ToString() });
                }
            }
            arrayList.Add(new Item() { code = IDLinkClaim.Rows.Count+1, value = "value1", id = e.PostBackValue.ToString() });
            IDLinkClaim.DataSource = arrayList;
            IDLinkClaim.DataBind();
        }
        protected class Item
        {
            public int code { get; set; }
            public string value { get; set; }
            public string id { get; set; }
        }
        public void Url_Map(string ID_Table, string Name_Table)
        {
            MapPage.Visible = true;
            ObjectDataTempGrig.SelectParameters.Clear();
            ObjectDataTempGrig.SelectParameters.Add("NameTable", Name_Table.ToString());
            ObjectDataTempGrig.SelectParameters.Add("ID_Table", ID_Table.ToString());
            TempGrid.DataSourceID = "ObjectDataTempGrig";
            TempGrid.DataKeyNames = new string[] { "ID_Table", "ID_Files", "fileName", "fileType", "MapMain" };
            TempGrid.DataBind();
            for (int i = 0; i < TempGrid.Rows.Count; i++)
            {
                FileCoordimateDataSource.SelectParameters.Clear();
                FileCoordimateDataSource.SelectParameters.Add("ID_files", TempGrid.DataKeys[i].Values[1].ToString());
            }
            string photoFilePath = Server.MapPath("../Image_Data/");
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
            //поле для HotSpotsGrid
            BoundField PageImageGrid = new BoundField();
            PageImageGrid.HeaderText = "Связанные элементы";
            PageImageGrid.DataField = "AlternateText";
            PageImageGrid.Visible = false;
            // для заполения ссылок и их названий 
            GridView HotSpotsGrid = new GridView();
            HotSpotsGrid.DataSourceID = "FileCoordimateDataSource";
            HotSpotsGrid.AutoGenerateColumns = false;
            HotSpotsGrid.DataKeyNames = new string[] { "ID", "ID_files", "Coordinate", "AlternateText", "ID_UrlTable" };
            HotSpotsGrid.Columns.Add(PageImageGrid);
            HotSpotsGrid.Width = 40;
            DivRightPage.Controls.Add(HotSpotsGrid);

            HotSpotsGrid.DataBind();
            MapPage.HotSpots.Clear();
            for (int i = 0; i < HotSpotsGrid.Rows.Count; i++)
            {
                PolygonHotSpot Ph = new PolygonHotSpot();
                Ph.AlternateText = HotSpotsGrid.DataKeys[i].Values[3].ToString();
                Ph.Coordinates = HotSpotsGrid.DataKeys[i].Values[2].ToString();
                Ph.PostBackValue = HotSpotsGrid.DataKeys[i].Values[4].ToString();
                MapPage.HotSpots.Add(Ph);
            }
        }
        // Заполнение устройств привязвязанных к комнате
        public void DeviceMain_room(string ID_Room)
        {
            ImageChecked.SelectParameters["ID_Room"].DefaultValue = ID_Room;
            CheckBoxImage.DataBind(); 
        }
        // Заполнение деревьв дочерних к выбранным устройствам
        public void DeviceCildren_room(object sender, EventArgs e)
        {
            int i = 0;

            foreach (ListViewItem dli in CheckBoxImage.Items)
            {
                CheckBox chk = (CheckBox)dli.FindControl("IMGCHECK");
                if (chk.Checked)
                {   
                    //string nametree =  ;
                    /*
                    TreeView TreeView1 = new TreeView();
                    TreeView1.ID = "TreeView1_"+i;
                    TreeView1.Attributes.Add("TreeNodePopulate", "TreeNodePopulate");
                    TreeView1.EnableViewState = true;
                    TreeView1.EnableClientScript = true;  

                    DataTable dt = new DataTable();
                    TreeDevice.SelectMethod = "GetAllParent";
                    TreeDevice.SelectParameters.Clear();
                    TreeDevice.SelectParameters.Add("Parent_ID", CheckBoxImage.DataKeys[i].Values[5].ToString());
                    TreeDevice.SelectParameters.Add("ID_Unit", "");
                    DataView dv = (DataView)TreeDevice.Select();
                    dt = dv.Table;
                    //PopulateNodes(dt, TreeView1.Nodes);
                     */
                    //PopulateRootLevel(i);
                    
                    //CheckBoxImage.Items[i].FindControl("TDControl").Controls.Add(TreeView1);

                    TreeDevice.SelectMethod = "GetAllParent";
                    TreeDevice.SelectParameters.Clear();
                    TreeDevice.SelectParameters.Add("Parent_ID", CheckBoxImage.DataKeys[i].Values[5].ToString());
                    TreeDevice.SelectParameters.Add("ID_Unit", "");

                    CheckBoxList treew = new CheckBoxList();
                    treew.DataSourceID = "TreeDevice";
                    treew.DataTextField = "Text";
                    treew.DataValueField = "ID";
                    CheckBoxImage.Items[i].FindControl("TDControlFoter").Controls.Add(treew); 
                }
                i += 1;

            }
        }

        private void PopulateRootLevel(int i)
        {

            DataTable dt = new DataTable();
            TreeDevice.SelectMethod = "GetAll";
            TreeDevice.SelectParameters.Clear();
            TreeDevice.SelectParameters.Add("ID_Unit", "");
            DataView dv = (DataView)TreeDevice.Select();

            dt = dv.Table;

            TreeView TreeView1 = new TreeView();
            TreeView1.ID = "TreeView1";
            TreeView1.Attributes.Add("TreeNodePopulate", "TreeNodePopulate");
            TreeView1.EnableViewState = true;
            TreeView1.EnableClientScript = true;
            CheckBoxImage.Items[i].FindControl("TDControl").Controls.Add(TreeView1);

            PopulateNodes(dt, TreeView1.Nodes);
        }
        private void PopulateNodes(DataTable dt, TreeNodeCollection nodes)
        {
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode tn = new TreeNode();
                tn.Text = dr["Text"].ToString();
                tn.Value = dr["Id"].ToString();
                nodes.Add(tn);
                //If node has child nodes, then enable on-demand populating€
                tn.PopulateOnDemand = (int.Parse(dr["childnodecount"].ToString()) > 0);
            }
        }
        protected void TreeNodePopulate(Object sender, TreeNodeEventArgs e)
        {
            PopulateSubLevel(int.Parse(e.Node.Value), e.Node);
        }
        private void PopulateSubLevel(int parent_id, TreeNode parentNode)
        {
            DataTable dt = new DataTable();
            TreeDevice.SelectMethod = "GetAllParent";
            TreeDevice.SelectParameters.Clear();
            TreeDevice.SelectParameters.Add("Parent_ID", parent_id.ToString());
            TreeDevice.SelectParameters.Add("ID_Unit", "");
            DataView dv = (DataView)TreeDevice.Select();
            dt = dv.Table;
            PopulateNodes(dt, parentNode.ChildNodes);
        }
    }
}