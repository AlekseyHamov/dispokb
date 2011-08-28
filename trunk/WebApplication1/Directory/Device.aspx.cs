using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            ModalPopupExtender1.Show();
            DivUpdatePanel.Visible = true;
            UpdateButton.Visible = true;
            InsertButton.Visible = false;
            DeleteButton.Visible = true;
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
            ModalPopupExtender1.Show();
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
    }
}