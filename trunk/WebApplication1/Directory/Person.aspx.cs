using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            ModalPopupExtender1.Show();
            UpdateButton.Visible = true;
            InsertButton.Visible = false;
            DeleteButton.Visible = true;
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
    }
}