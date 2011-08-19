using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;


namespace WebApplication1.Directory
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         if (!Page.IsPostBack)
            {
                PopulateRootLevel();
            }
        }
        private void PopulateRootLevel()
        {

            DataTable dt = new DataTable();
            TreeDevice.SelectMethod = "GetAll";  
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
            DataView dv = (DataView)TreeDevice.Select();
            dt = dv.Table; 
            PopulateNodes(dt, parentNode.ChildNodes);
        } 
        private void PopulateNodes( DataTable dt ,  TreeNodeCollection nodes )
        {
            foreach (DataRow dr in dt.Rows)
            {
             TreeNode tn = new TreeNode();
             tn.Text = dr["Text"].ToString();
             tn.Value = dr["Id"].ToString();
             nodes.Add(tn);
 
            //If node has child nodes, then enable on-demand populating
             tn.PopulateOnDemand = (int.Parse(dr["childnodecount"].ToString())  > 0);
                  
            }
        }
        protected void TreeNodePopulate(Object sender, TreeNodeEventArgs e)
            {
                PopulateSubLevel(int.Parse(e.Node.Value), e.Node);
            }

        protected void Select_Change(Object sender, EventArgs e)
        {
            //Msg.Text = TreeView1.Nodes.Count.ToString();    
            //            Msg.Text = "You selected: " + TreeViewUpdate.SelectedNode.Value;

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
            ModalPopupExtender1.Show();
            UpdateButton.Visible = true;
            InsertButton.Visible = false;
            DeleteButton.Visible = true;
        }
        protected void OnRowChanged(object sender, EventArgs e)
        {
            /*SparesObjectDataSourceOneRow.SelectParameters["ID_Spares"].DefaultValue =
            SparesGridView.SelectedValue.ToString();
            SparesObjectDataSourceOneRow.DataBind();

            UpdateSparse.ChangeMode(DetailsViewMode.Edit);
            */
            ModalPopupExtender1.Show();
        }
        protected void DataSource_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            string ID_NewDevice;
            ID_NewDevice = Convert.ToString(e.ReturnValue);
        }
        protected void DataSource_OnUpdated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            /*            DeviceGridView.DataBind();
                        if ((int)e.ReturnValue == 0)
                            Msg.Text = "Employee was not updated. Please try again.";
            */
            ModalPopupExtender1.Show();
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