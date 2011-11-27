using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

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
    }
}