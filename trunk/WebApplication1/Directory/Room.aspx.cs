using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Directory
{
    public partial class Room : System.Web.UI.Page
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

            GridView ListDevice = new GridView();
            ListDevice.DataSourceID = "RoomDeviceListDataSource";
            ListDevice.AutoGenerateColumns = true;
            ListDevice.Visible = false;
            UpdatePanel.Controls.Add(ListDevice);
            ListDevice.DataBind();
            for (int i = 0; i < CheckBoxDevice.Items.Count; i++)
            {
                for (int j = 0; j < ListDevice.Rows.Count; j++)
                {
                    if (ListDevice.Rows[j].Cells[0].Text == CheckBoxDevice.Items[i].Value)
                    {
                        if (ListDevice.Rows[j].Cells[1].Text != "")
                        {
                            CheckBoxDevice.Items[i].Selected = true;
                        }
                    }
                }
            }
            ModalPopupExtender1.Show();
            UpdateButton.Visible = true;
            InsertButton.Visible = false;
            DeleteButton.Visible = true;
        }
        protected void BuildingList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            OtdelenObjectDataSource.FilterExpression = "ID_Building={0}";
            OtdelenObjectDataSource.FilterParameters.Clear();
            OtdelenObjectDataSource.FilterParameters.Add(new ControlParameter("ID_Building", "BuildingList", "SelectedValue"));

            ModalPopupExtender1.Show();
        }
        protected void Filter_device(object sender, EventArgs e)
        {
            CheckBoxDevice.DataBind();
            RoomDeviceListDataSource.SelectMethod = "GetOneRecordTest";
            RoomDeviceListDataSource.SelectParameters.Clear();
            if (RoomGridView.SelectedValue != null)
            {
                RoomDeviceListDataSource.SelectParameters.Add("ID_Room", RoomGridView.SelectedValue.ToString());
            }
            else
            {
                RoomDeviceListDataSource.SelectParameters.Add("ID_Room", "0");
            }
            GridView ListDevice = new GridView();
            ListDevice.DataSourceID = "RoomDeviceListDataSource";
            ListDevice.AutoGenerateColumns = true;
            ListDevice.Visible = false;
            UpdatePanel.Controls.Add(ListDevice);
            ListDevice.DataBind();
            for (int i = 0; i < CheckBoxDevice.Items.Count; i++)
            {
                for (int j = 0; j < ListDevice.Rows.Count; j++)
                {
                    if (ListDevice.Rows[j].Cells[0].Text == CheckBoxDevice.Items[i].Value)
                    {
                        if (ListDevice.Rows[j].Cells[1].Text != "")
                        {
                            CheckBoxDevice.Items[i].Selected = true;
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
            RoomDeviceListDataSource.InsertParameters.Clear();
            RoomDeviceListDataSource.InsertParameters.Add("ID_Room", ID_RoomNew);
            RoomDeviceListDataSource.InsertParameters.Add("ID_Device", "");
            for (int i = 0; i < CheckBoxDevice.Items.Count; i++)
            {
                if (CheckBoxDevice.Items[i].Selected)
                {
                    RoomDeviceListDataSource.InsertParameters["ID_Device"].DefaultValue = CheckBoxDevice.Items[i].Value;
                    RoomDeviceListDataSource.Insert();
                }
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
            RoomDeviceListDataSource.InsertParameters.Clear();
            RoomDeviceListDataSource.InsertParameters.Add("ID_Room", RoomGridView.SelectedValue.ToString());
            RoomDeviceListDataSource.InsertParameters.Add("ID_Device", "");
            RoomDeviceListDataSource.SelectMethod = "GetOneRecordTest";
            RoomDeviceListDataSource.SelectParameters.Clear();
            RoomDeviceListDataSource.SelectParameters.Add("ID_Room", RoomGridView.SelectedValue.ToString());

            GridView ListDevice = new GridView();
            ListDevice.DataSourceID = "RoomDeviceListDataSource";
            ListDevice.AutoGenerateColumns = true;
            ListDevice.Visible = false;
            UpdatePanel.Controls.Add(ListDevice);
            ListDevice.DataBind();
            Boolean CheckBoxDeviceBoolean = new Boolean();
            for (int i = 0; i < CheckBoxDevice.Items.Count; i++)
            {
                for (int j = 0; j < ListDevice.Rows.Count; j++)
                {
                    CheckBoxDeviceBoolean = false;
                    if (ListDevice.Rows[j].Cells[0].Text == CheckBoxDevice.Items[i].Value)
                    {
                        if (Convert.ToInt32(ListDevice.Rows[j].Cells[2].Text) != 0)
                        {
                            CheckBoxDeviceBoolean = true;
                        }
                    }
                }
                if (CheckBoxDeviceBoolean == true && CheckBoxDevice.Items[i].Selected == false)
                {
                    RoomDeviceListDataSource.DeleteParameters["ID_Device"].DefaultValue = CheckBoxDevice.Items[i].Value;
                    RoomDeviceListDataSource.Delete();
                }
                if (CheckBoxDeviceBoolean == false && CheckBoxDevice.Items[i].Selected == true)
                {
                    RoomDeviceListDataSource.InsertParameters["ID_Device"].DefaultValue = CheckBoxDevice.Items[i].Value;
                    RoomDeviceListDataSource.Insert();
                }
            }
        }
        protected void DataSource_OnDeleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if ((int)e.ReturnValue == 0)
                Msg.Text = "Employee was not deleted. Please try again.";

        }
    }
}