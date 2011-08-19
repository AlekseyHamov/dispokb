using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Directory
{
    public partial class Building : System.Web.UI.Page
    {
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
        protected void DetailsView_ItemUpdated(Object sender, DetailsViewUpdatedEventArgs e)
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
            ModalPopupExtender1.Show();
            UpdateButton.Visible = true;
            InsertButton.Visible = false;
            DeleteButton.Visible = true;

        }
        protected void BuildingDataSource_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            // BuildingObjectDataSource.SelectParameters["ID_Building"].DefaultValue =
            // e.ReturnValue.ToString();
            //  BuildingGridView.DataBind();


            /*kmj  FileUpload upload = UpdatePanel.FindControl("FileUpload") as FileUpload;
              using (System.IO.BinaryReader reader = new System.IO.BinaryReader(upload.PostedFile.InputStream))
              {
                  byte[] bytes = new byte[upload.PostedFile.ContentLength];
                  reader.Read(bytes, 0, bytes.Length);
                  e.["imgData"] = bytes;
              }*/
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
    }
}