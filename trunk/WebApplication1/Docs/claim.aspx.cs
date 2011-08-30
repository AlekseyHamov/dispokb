using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
namespace WebApplication1.Directory
{
    public partial class Claim : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Msg.Text = "";
            InsertButton.Visible = true; 
            if (IsPostBack == false)
            {
                TimeBox.Text = DateTime.Now.ToLongTimeString();
                DateClaimBox.Text = DateTime.Now.Date.ToString("yyyy.MM.dd");
            }
            if (IsPostBack == false && DateTime.Now.Hour < 8)
            {
                DateEnd.Text = DateTime.Now.ToString("yyyy.MM.dd 08:00:00");
                DateTime DateBegin1 = new DateTime();
                DateBegin1 = DateTime.Now.AddDays(-1);
                DateBegin.Text = DateBegin1.ToString("yyyy.MM.dd 08:00:00");
            }
            else if (IsPostBack == false && DateTime.Now.Hour >= 8)
            {
                DateTime DateEnd1 = new DateTime();
                DateEnd1 = DateTime.Now.AddDays(1);
                DateEnd.Text = DateEnd1.ToString("yyyy.MM.dd 08:00:00");
                DateBegin.Text = DateTime.Now.ToString("yyyy.MM.dd 08:00:00"); ;
            }
            DataGrid  listclaim = new DataGrid();
            listclaim.DataSourceID = "ClaimObjectDataSource";
            listclaim.AutoGenerateColumns = true;
            listclaim.Visible = false;
            Panel1.Controls.Add(listclaim);
            listclaim.DataBind();
            for (int i = 0; i < listclaim.Items.Count; i++)
            {
                ListItem itemBuilding = new ListItem();
                Boolean notrow = false;
                for (int j = 0; j <= BuildingList.Items.Count; j++)
                    {
                        try
                        {
                            if (BuildingList.Items[j].Value == listclaim.Items[i].Cells[2].Text)
                            {
                                notrow = true;
                            }
                        }
                        catch (Exception) { } 
                    }
                    if (notrow == false)
                    {
                        itemBuilding.Value = listclaim.Items[i].Cells[2].Text;
                        itemBuilding.Text = listclaim.Items[i].Cells[8].Text;
                        BuildingList.Items.Add(itemBuilding);
                    }
                    ListItem itemOtdelen = new ListItem();
                    notrow = false;
                    for (int j = 0; j <= OtdelenList.Items.Count; j++)
                    {
                        try
                        {
                            if (OtdelenList.Items[j].Value == listclaim.Items[i].Cells[1].Text)
                            {
                                notrow = true;
                            }
                        }
                        catch (Exception) { }
                    }
                    if (notrow == false)
                    {
                        itemOtdelen.Value = listclaim.Items[i].Cells[1].Text;
                        itemOtdelen.Text = listclaim.Items[i].Cells[9].Text;
                        OtdelenList.Items.Add(itemOtdelen);
                    }
                    ListItem itemRoom = new ListItem();
                    notrow = false;
                    for (int j = 0; j <= RoomList.Items.Count; j++)
                    {
                        try
                        {
                            if (RoomList.Items[j].Value == listclaim.Items[i].Cells[3].Text)
                            {
                                notrow = true;
                            }
                        }
                        catch (Exception) { }
                    }
                    if (notrow == false)
                    {
                        itemRoom.Value = listclaim.Items[i].Cells[3].Text;
                        itemRoom.Text = listclaim.Items[i].Cells[10].Text;
                        RoomList.Items.Add(itemRoom);
                    }
                    ListItem itemUnit = new ListItem();
                    notrow = false;
                    for (int j = 0; j <= RadioButtonUnit.Items.Count; j++)
                    {
                        try
                        {
                            if (RadioButtonUnit.Items[j].Value == listclaim.Items[i].Cells[5].Text)
                            {
                                notrow = true;
                            }
                        }
                        catch (Exception) { }
                    }
                    if (notrow == false)
                    {
                        itemUnit.Value = listclaim.Items[i].Cells[5].Text;
                        itemUnit.Text = listclaim.Items[i].Cells[12].Text;
                        RadioButtonUnit.Items.Add(itemUnit);
                    }
                    ListItem itemPerson = new ListItem();
                    notrow = false;
                    for (int j = 0; j <= PersonList.Items.Count; j++)
                    {
                        try
                        {
                            if (PersonList.Items[j].Value == listclaim.Items[i].Cells[4].Text)
                            {
                                notrow = true;
                            }
                        }
                        catch (Exception) { }
                    }
                    if (notrow == false)
                    {
                        itemPerson.Value = listclaim.Items[i].Cells[4].Text;
                        itemPerson.Text = listclaim.Items[i].Cells[11].Text;
                        PersonList.Items.Add(itemPerson);
                    }
            }
        }
        protected void CommandBtn_Click(Object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Update":
                    DateClaimBox.Text = DateClaimBox.Text + " " + DateTime.Now.ToLongTimeString(); ;
                    ClaimObjectDataSource.Update();
                    InsertButton.Visible = true;   
                    DateClaimBox.Text = DateTime.Now.Date.ToString("dd.MM.yyyy");
                    break;
                case "Insert":
                    DateClaimBox.Text = DateClaimBox.Text + " " + DateTime.Now.ToLongTimeString(); ;
                    ClaimObjectDataSource.Insert();
                    DateClaimBox.Text = DateTime.Now.Date.ToString("dd.MM.yyyy");
                    break;
                case "Delete":
                    ClaimObjectDataSource.Delete();
                    break;
                case "VisibleButoonAdd":
                    Msg.Text = "Command name not recogized.";
                    break;
                default:
                    Msg.Text = "Command name not recogized.";
                    break;
            }

        }
        protected void CommandBtn_Click1(Object sender, EventArgs e)
        {
            ClaimObjectDataSource.Update();
        }
        protected void button_filter_Close(object sender, EventArgs e)
        {
            FiltrTable.Visible = false;
            OtdelenList.DataBind();
            RoomList.DataBind();
            PersonList.DataBind();
        }
        protected void Update_filter_Building(object sender, EventArgs e)
        {
            OtdelenObjectDataSource.FilterExpression = "ID_Building={0}";
            OtdelenObjectDataSource.FilterParameters.Clear();
            OtdelenObjectDataSource.FilterParameters.Add(new ControlParameter("ID_Building", "DropDownListBuildingUpdate", "SelectedValue"));
            ModalPopupExtender1.Show();
        }
        protected void Update_filter_Otdelen(object sender, EventArgs e)
        {
            RoomObjectDataSource.FilterExpression = "ID_Otdelen={0}";
            RoomObjectDataSource.FilterParameters.Clear();
            RoomObjectDataSource.FilterParameters.Add(new ControlParameter("ID_Otdelen", "DropDownListOtdelenUpdate", "SelectedValue"));
            ModalPopupExtender1.Show();
        }
        protected void Update_filter_Person(object sender, EventArgs e)
        {
            PersonObjectDataSource.FilterExpression = "ID_Unit = {0}";
            PersonObjectDataSource.FilterParameters.Clear();
            PersonObjectDataSource.FilterParameters.Add("ID_Unit", RadioButtonUnitUpdate.SelectedValue);
            RoomDeviceListDataSource.FilterExpression = "ID_Unit = {0}";
            RoomDeviceListDataSource.FilterParameters.Clear();
            RoomDeviceListDataSource.FilterParameters.Add("ID_Unit", RadioButtonUnitUpdate.SelectedValue);

            ModalPopupExtender1.Show();
        }
        protected void Update_filter_Room(object sender, EventArgs e)
        {
            RoomDeviceListDataSource.SelectMethod = "GetOneRecordTest";
            RoomDeviceListDataSource.SelectParameters.Clear();
            RoomDeviceListDataSource.SelectParameters.Add("ID_Room", DropDownListRoomUpdate.SelectedValue.ToString());
            RoomDeviceListDataSource.SelectParameters.Add("ID_Unit", "0");

            if (RadioButtonUnitUpdate.SelectedValue != "")
            {
                RoomDeviceListDataSource.FilterExpression = "ID_Unit = {0}";
                RoomDeviceListDataSource.FilterParameters.Clear();
                RoomDeviceListDataSource.FilterParameters.Add("ID_Unit", RadioButtonUnitUpdate.SelectedValue);
            }
            string ID_Device = "0";
            for (int i = 0; i < CheckBoxDevice.Items.Count; i++)
            {
                if (CheckBoxDevice.Items[i].Selected == true)
                {
                    ID_Device += ", " + CheckBoxDevice.Items[i].Value;
                }
            }
            try
            {
                SparesObjectDataSource.FilterParameters.Clear();
                SparesObjectDataSource.FilterParameters.Add("ID_Device", ID_Device);
            }
            catch (FormatException) { }
            ModalPopupExtender1.Show();
        }
        protected void Filter_device(object sender, EventArgs e)
        {
            string ID_Device = "0";
            for (int i = 0; i < CheckBoxDevice.Items.Count; i++)
            {
                if (CheckBoxDevice.Items[i].Selected == true)
                {
                    ID_Device += ", " + CheckBoxDevice.Items[i].Value;
                }
            }
            try
            {
                SparesObjectDataSource.FilterParameters.Clear();
                SparesObjectDataSource.FilterParameters.Add("ID_Device", ID_Device);
            }
            catch (FormatException) { }

            ModalPopupExtender1.Show();
        }
        protected void filter_building(object sender, EventArgs e)
        {
            if (CheckBoxBuilding.Checked.ToString() == "True")
            {
                ClaimObjectDataSource.FilterExpression = "ID_Building={0}";
                ClaimObjectDataSource.FilterParameters.Clear();
                ClaimObjectDataSource.FilterParameters.Add(new ControlParameter("ID_Building", "BuildingList", "SelectedValue"));
            }            
        }
        protected void filter_otdelen(object sender, EventArgs e)
        {
            if (CheckBoxOtdelen.Checked.ToString() == "True")
            {
                ClaimObjectDataSource.FilterParameters.Clear();
                ClaimObjectDataSource.FilterExpression = " ID_Otdelen={0}";
                ClaimObjectDataSource.FilterParameters.Add(new ControlParameter("ID_Otdelen", "OtdelenList", "SelectedValue"));
            }
        }
        protected void filter_room(object sender, EventArgs e)
        {
            if (CheckBoxRoom.Checked.ToString() == "True")
            {
                ClaimObjectDataSource.FilterExpression = " ID_Room={0}";
                ClaimObjectDataSource.FilterParameters.Clear();
                ClaimObjectDataSource.FilterParameters.Add(new ControlParameter("ID_Room", "RoomList", "SelectedValue"));
            }
        }
        protected void filter_unit(object sender, EventArgs e)
        {
            if (RadioButtonUnit.SelectedValue.ToString() == "Все")
            {
                ClaimObjectDataSource.FilterExpression = " ID_Person={0}";
                ClaimObjectDataSource.FilterParameters.Clear();
                ClaimObjectDataSource.FilterParameters.Add(new ControlParameter("ID_Person", "PersonList", "SelectedValue"));
            }
            if (RadioButtonUnit.SelectedIndex > -1 && CheckBoxPerson.Checked == false)
            {
                ClaimObjectDataSource.FilterExpression = "ID_Unit = {0}";
                ClaimObjectDataSource.FilterParameters.Clear();
                ClaimObjectDataSource.FilterParameters.Add("ID_Unit", RadioButtonUnit.SelectedValue);
            }
        }
        protected void filter_person(object sender, EventArgs e)
        {
            Msg.Text = "1";
            if (PersonList.SelectedIndex > -1 && CheckBoxPerson.Checked == true)
            {
                ClaimObjectDataSource.FilterExpression = " ID_Person={0}";
                ClaimObjectDataSource.FilterParameters.Clear();
                ClaimObjectDataSource.FilterParameters.Add(new ControlParameter("ID_Person", "PersonList", "SelectedValue"));
                Msg.Text += Msg.Text + " 2 ";
            }
        }
        protected void button_filter(object sender, EventArgs e)
        {
            FiltrTable.Visible = true;
            AllFiltr.Checked = false;

            if (AllFiltr.Checked == false)
            {
                if (CheckBoxBuilding.Checked == false || CheckBoxOtdelen.Checked == false ||
                     CheckBoxRoom.Checked == false && ClaimObjectDataSource.FilterParameters.Count != 0)
                {
                    ClaimObjectDataSource.FilterParameters.Clear();
                }
                if (CheckBoxBuilding.Checked.ToString() == "True" || CheckBoxOtdelen.Checked.ToString() == "True" ||
                     CheckBoxRoom.Checked.ToString() == "True")
                {
                }
            }
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
            ClaimOneRow.DataBind();
            InsertButton.Visible = false;
            UpdateButton.Visible = true;
            StatusRadioButtonList.Visible = true;
            DeleteButton.Visible = true;
            try
            {
                if (DropDownListBuildingUpdate.Items.Count != 0)
                {
                    TextBox2.Text  = ClaimOneRow.Rows[6].Cells[1].Text;
                }
                if (DropDownListBuildingUpdate.Items.Count != 0)
                {
                    DropDownListBuildingUpdate.SelectedValue = ClaimOneRow.Rows[2].Cells[1].Text;
                }
                if (DropDownListOtdelenUpdate.Items.Count != 0)
                {
                    DropDownListOtdelenUpdate.SelectedValue = ClaimOneRow.Rows[1].Cells[1].Text;
                }
                if (DropDownListRoomUpdate.Items.Count != 0)
                {
                    DropDownListRoomUpdate.SelectedValue = ClaimOneRow.Rows[3].Cells[1].Text;
                }
                if (DropDownListPersonUpdate.Items.Count != 0)
                {
                    DropDownListPersonUpdate.SelectedValue = ClaimOneRow.Rows[4].Cells[1].Text;
                }
                if (RadioButtonUnitUpdate.Items.Count != 0)
                {
                    RadioButtonUnitUpdate.SelectedValue = ClaimOneRow.Rows[5].Cells[1].Text;
                }
            }
            catch { }
            ListClaimSpares.DataBind();
            CheckBoxDevice.DataBind();
            for (int i = 0; i < CheckBoxDevice.Items.Count; i++)
            {
                for (int j = 0; j < ListClaimSpares.Rows.Count; j++)
                {
                    if (ListClaimSpares.Rows[j].Cells[3].Text == CheckBoxDevice.Items[i].Value)
                    {
                        if (Convert.ToInt32(ListClaimSpares.Rows[j].Cells[3].Text) != 0)
                        {
                            CheckBoxDevice.Items[i].Selected = true;
                        }
                    }
                }
            }
            string ID_Device = "0";
            for (int i = 0; i < CheckBoxDevice.Items.Count; i++)
            {
                if (CheckBoxDevice.Items[i].Selected == true)
                {
                    ID_Device += ", " + CheckBoxDevice.Items[i].Value;
                }
            }
            try
            {
                SparesObjectDataSource.FilterParameters.Clear();
                SparesObjectDataSource.FilterParameters.Add("ID_Device", ID_Device);
            }
            catch (FormatException) { }
            CheckBoxSpares.DataBind();
            for (int i = 0; i < CheckBoxSpares.Items.Count; i++)
            {
                for (int j = 0; j < ListClaimSpares.Rows.Count; j++)
                {
                    if (ListClaimSpares.Rows[j].Cells[2].Text == CheckBoxSpares.Items[i].Value)
                    {
                        if (Convert.ToInt32(ListClaimSpares.Rows[j].Cells[2].Text) != 0)
                        {
                            CheckBoxSpares.Items[i].Selected = true;
                        }
                    }
                }
            }

            ModalPopupExtender1.Show();
        }
        protected void DetailsObjectDataSource_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            string ID_Claim = "0";
            ID_Claim = e.ReturnValue.ToString();
            ClaimSparesListDataSource.InsertMethod = "InsertRecord";
            ClaimSparesListDataSource.InsertParameters.Clear();
            ClaimSparesListDataSource.InsertParameters.Add("ID_Claim", ID_Claim);
            ClaimSparesListDataSource.InsertParameters.Add("ID_Spares", null);
            for (int i = 0; i < CheckBoxSpares.Items.Count; i++)
            {
                if (CheckBoxSpares.Items[i].Selected == true)
                {
                    ClaimSparesListDataSource.InsertParameters["ID_Spares"].DefaultValue = CheckBoxSpares.Items[i].Value;
                    ClaimSparesListDataSource.Insert();
                }
            }
        }
        protected void DetailsObjectDataSource_OnUpdated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            ClaimSparesListDataSource.DeleteMethod = "DeleteRecord";
            ClaimSparesListDataSource.DeleteParameters.Add("ID_ClaimSparesList", "0");
            ClaimSparesListDataSource.InsertMethod = "InsertRecord";
            ClaimSparesListDataSource.InsertParameters.Clear();
            try
            {
                ClaimSparesListDataSource.InsertParameters.Add("ID_Claim", GridView.SelectedValue.ToString());
            }
            catch (Exception)
            { Msg.Text = "Вновь создаваемая запись, невозможно обновить"; }
            ClaimSparesListDataSource.InsertParameters.Add("ID_Spares", "0");
            for (int i = 0; i < CheckBoxDevice.Items.Count; i++)
            {
                for (int j = 0; j < ListClaimSpares.Rows.Count; j++)
                {
                    if (ListClaimSpares.Rows[j].Cells[3].Text == CheckBoxDevice.Items[i].Value && CheckBoxDevice.Items[i].Selected == false)
                    {
                        ClaimSparesListDataSource.DeleteParameters["ID_ClaimSparesList"].DefaultValue = ListClaimSpares.Rows[j].Cells[0].Text;
                        ClaimSparesListDataSource.Delete();   // удаляем запись
                    }
                }
            }

            for (int i = 0; i < CheckBoxSpares.Items.Count; i++)
            {
                bool k = new bool();
                k = false;
                for (int j = 0; j < ListClaimSpares.Rows.Count; j++)
                {
                    if (ListClaimSpares.Rows[j].Cells[2].Text == CheckBoxSpares.Items[i].Value && CheckBoxSpares.Items[i].Selected == false)
                    {
                        ClaimSparesListDataSource.DeleteParameters["ID_ClaimSparesList"].DefaultValue = ListClaimSpares.Rows[j].Cells[0].Text;
                        ClaimSparesListDataSource.Delete();   // удаляем запись
                    }
                    if (ListClaimSpares.Rows[j].Cells[2].Text == CheckBoxSpares.Items[i].Value && CheckBoxSpares.Items[i].Selected == true)
                    {
                        k = true;
                    }
                }
                if (CheckBoxSpares.Items[i].Selected == true && k == false)
                {
                    ClaimSparesListDataSource.InsertParameters["ID_Spares"].DefaultValue = CheckBoxSpares.Items[i].Value;
                    ClaimSparesListDataSource.Insert();
                }
            }
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