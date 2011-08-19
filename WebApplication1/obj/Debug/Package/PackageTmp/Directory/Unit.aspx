<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="Unit.aspx.cs" Inherits="WebApplication1.Directory.Unit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script runat="server">

private void CommandBtn_Click(Object sender, CommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Update":
                UnitObjectDataSource.Update();
                break;
            case "Insert":
                UnitObjectDataSource.Insert();
                break;
            case "Delete":
                UnitObjectDataSource.Delete();  
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
    
  void DetailsView_ItemInserted(Object sender, DetailsViewInsertedEventArgs e)
  {
      UnitGridView.DataBind();
  }
  
  void DetailsView_ItemUpdated(Object sender, DetailsViewUpdatedEventArgs e)
  {
      UnitGridView.DataBind();
  }
  
  void DetailsView_ItemDeleted(Object sender, DetailsViewDeletedEventArgs e)
  {
      UnitGridView.DataBind();
  }
  void GridView_OnSelectedIndexChanged(object sender, EventArgs e)
  {
      //OtdelenDetailsObjectDataSource.SelectParameters["ID_Otdelen"].DefaultValue =
      //OtdelenGridView.SelectedDataKey.Value.ToString();
      GridViewRow row = UnitGridView.SelectedRow;
      TextBox2.Text = row.Cells[2].Text ;

      ModalPopupExtender1.Show();
      //ImageUpload.FileBytes;
      //ImageUpload.FileContent. 
      //UpdatePanel.Width = Convert.ToInt16(TextBox1.Width.Value.ToString()) + Convert.ToInt16(TextBox2.Width.Value.ToString()) + 20;
       
  }
  void GridViewBuilding_OnSelectedIndexChanged(object sender, EventArgs e)
  {
//      int index = BuildingGridView.SelectedIndex;
      
//      Msg.Text = "The primary key value of the selected row is " +
//      BuildingGridView.DataKeys[index].Value.ToString() + ".";

      ModalPopupExtender1.Show();
  }
  void DataSource_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
  {
     // UnitObjectDataSource.SelectParameters["ID_Unit"].DefaultValue =
     // e.ReturnValue.ToString();
    //  UnitGridView.DataBind();
      
      
        /*kmj  FileUpload upload = UpdatePanel.FindControl("FileUpload") as FileUpload;
          using (System.IO.BinaryReader reader = new System.IO.BinaryReader(upload.PostedFile.InputStream))
          {
              byte[] bytes = new byte[upload.PostedFile.ContentLength];
              reader.Read(bytes, 0, bytes.Length);
              e.["imgData"] = bytes;
          }*/
 }


  void DataSource_OnUpdated(object sender, ObjectDataSourceStatusEventArgs e)
  {
      UnitGridView.DataBind();
      if ((int)e.ReturnValue == 0)
          Msg.Text = "Employee was not updated. Please try again.";
  }

  void DataSource_OnDeleted(object sender, ObjectDataSourceStatusEventArgs e)
  {
      if ((int)e.ReturnValue == 0)
          Msg.Text = "Employee was not deleted. Please try again.";
      
  }
             
  void Page_Load()
  {
    Msg.Text = "";        
  }

</script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
      <h3>Справочник Комнат</h3> 
      <asp:Label id="Msg" runat="server" ForeColor="Red" />
      <asp:ObjectDataSource 
        ID="UnitObjectDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataUnit.UnitData" 
        SortParameterName="SortColumns"
        EnablePaging="true"
        SelectCountMethod="SelectCount"
        DeleteMethod="DeleteRecord"
        StartRowIndexParameterName="StartRecord"
        MaximumRowsParameterName="MaxRecords" 
        SelectMethod="GetAll" 
        InsertMethod="InsertRecord" 
        UpdateMethod="UpdateRecord" 
         
        OnInserted="DataSource_OnInserted"
        OnUpdated="DataSource_OnUpdated"
        OnDeleted="DataSource_OnDeleted" >
        <InsertParameters>
              <asp:ControlParameter ControlID="TextBox2" Name="NameUnit" 
                  PropertyName="Text" />
        </InsertParameters>
        <deleteparameters>
            <asp:controlparameter name="ID_Unit" controlid="UnitGridView" propertyname="SelectedValue" />
        </deleteparameters>
        <updateparameters>
            <asp:controlparameter name="ID_Unit" controlid="UnitGridView" propertyname="SelectedValue" />
            <asp:ControlParameter ControlID="TextBox2" Name="NameUnit" 
                PropertyName="Text" />
        </updateparameters>
      </asp:ObjectDataSource>

      <br />

      <table cellspacing="10">
        <tr>
          <td valign="top">
            <asp:GridView ID="UnitGridView" 
              DataSourceID="UnitObjectDataSource" 
              AutoGenerateColumns="False"
              AllowSorting="True"
              AllowPaging="True"
              PageSize="18"
              DataKeyNames="ID_Unit"
              OnSelectedIndexChanged="GridView_OnSelectedIndexChanged"
              RunAt="server">

              <HeaderStyle backcolor="lightblue" forecolor="black"/>

              <Columns>                
                <asp:ButtonField
                                 CommandName="Select" ButtonType="Image" 
                      ImageUrl="~/Image/edit.png" FooterText="Проба">  
                  <ControlStyle Height="15px" Width="15px" />

                </asp:ButtonField>

                <asp:BoundField DataField="ID_Unit" HeaderText="Номер п/п" 
                      SortExpression="ID_Unit" Visible="False" />
                <asp:BoundField 
                        DataField="NameUnit"
                        HeaderText="Наименование" 
                      SortExpression="NameUnit" />
              </Columns>                
            </asp:GridView>            
            <asp:Button ID="btnEditCustomer"  Text = "добавить" runat="server" />
          </td>
        </tr>
        <tr>
          <td>
             <asp:Panel ID="UpdatePanel" runat="server"  
                  BackColor="#D9F2FF" BorderStyle="Double">
                  <table>
                    <tr >
                        <td align="left" >
                          <asp:Label ID="Label3" runat="server" Text="Редактирование подразделений"></asp:Label>
                        </td> 
                        <td align="right">
                          <asp:ImageButton ID="editBox_OK" runat="server" ImageUrl= "~/Image/Close.ico" Width="20" Height = "20"  />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="style1" >
                            Наименование подразделения</td>
                        <td align="left" class="style1">
                            <asp:TextBox ID="TextBox2"  runat="server" Width="160px"></asp:TextBox>
                        </td>
                   </tr>
                   <tr>
                        <td align="right" >
                            &nbsp;</td>
                         <td align="left">
                             &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    </tr>
                    
                  </table>
                          <asp:Button ID="Button1" runat="server" Text="Обновить" CommandName="Update" 
                                 OnCommand="CommandBtn_Click"/>
                          <asp:Button ID="Button2" runat="server" Text="Добавить" CommandName="Insert" 
                                 OnCommand="CommandBtn_Click"/>
                          <asp:Button ID="Button3" runat="server" Text="Удалить" CommandName="Delete" 
                                 OnCommand="CommandBtn_Click"/>
             </asp:Panel>
            <asp:ModalPopupExtender ID="ModalPopupExtender1"
                    runat="server"  
                    PopupControlID="UpdatePanel"
                    TargetControlID="btnEditCustomer"
                    OkControlID="editBox_OK"
                    BackgroundCssClass = "modalBackground" 
                    PopupDragHandleControlID = "Редактирование записи" Drag="True"
                    />
              <asp:DragPanelExtender ID="UpdatePanel_DragPanelExtender" runat="server" 
                  DragHandleID="UpdatePanel" Enabled="True" 
                  TargetControlID="UpdatePanel">
              </asp:DragPanelExtender>
          </td>
          <td>
              &nbsp;</td>
        </tr>
      </table>
    </asp:Content>
