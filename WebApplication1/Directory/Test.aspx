<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="Test.aspx.cs" Inherits="WebApplication1.Directory.Test" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script runat="server">
  
    </script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

             <br />
      
      <asp:ObjectDataSource 
        ID="TreeDevice" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataTreeDevice.TreeDeviceData"
        >
      </asp:ObjectDataSource>
      <asp:ObjectDataSource 
        ID="DeviceObjectDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataDevice.DeviceData" 
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
              <asp:ControlParameter ControlID="TextBox2" Name="NameDevice" 
                  PropertyName="Text" />
              <asp:ControlParameter ControlID="Description" Name="Description" 
                  PropertyName="Text" />
              <asp:ControlParameter ControlID="RadioButtonUnit" Name="ID_Unit" 
                  PropertyName="SelectedValue" />
        </InsertParameters>
        <deleteparameters>
            <asp:controlparameter name="ID_Device" controlid="TreeView1" propertyname="SelectedValue" />
        </deleteparameters>
        <updateparameters>
            <asp:controlparameter name="ID_Device" controlid="TreeView1" propertyname="SelectedValue" />
            <asp:ControlParameter ControlID="TextBox2" Name="NameDevice" 
                  PropertyName="Text" />
              <asp:ControlParameter ControlID="Description" Name="Description" 
                  PropertyName="Text" />
              <asp:ControlParameter ControlID="RadioButtonUnit" Name="ID_Unit" 
                  PropertyName="Text" />
        </updateparameters>
      </asp:ObjectDataSource>
             <div style="float:left">
             <asp:Label ID="Msg" runat="server" Text="" />
             <asp:TreeView 
                ID="TreeView1"
                ExpandDepth="0" 
                runat="server"
                ShowCheckBoxes="All" 
                OnTreeNodePopulate ="TreeNodePopulate"
                EnableViewState="true" >
             <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
             <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" 
                HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="1px" />
             <ParentNodeStyle Font-Bold="False" />
             <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" 
                HorizontalPadding="0px" VerticalPadding="0px" />
             </asp:TreeView>
             </div>
             <div>
                <asp:GridView ID="Device" runat="server" 
                  DataSourceID="DeviceObjectDataSource" 
                  AutoGenerateColumns="False"
                  AllowSorting="True"
                  AllowPaging="True"
                  PageSize="18"
                  DataKeyNames="ID_Device"
                  OnSelectedIndexChanged="GridView_OnSelectedIndexChanged"
                    >
                          <HeaderStyle backcolor="lightblue" forecolor="black"/>
                          <Columns>                
                            <asp:ButtonField
                                             CommandName="Select" ButtonType="Image" 
                                  ImageUrl="~/Image/edit.png" HeaderText="Ред.">  
                              <ControlStyle Height="15px" Width="15px" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="ID_Device" HeaderText="Номер п/п" 
                                  SortExpression="ID_Device" Visible="False" />
                            <asp:BoundField 
                                    DataField="NameDevice"
                                    HeaderText="Наименование" 
                                  SortExpression="NameDevice" />
                            <asp:ButtonField
                                    CommandName="Delete" ButtonType="Image" 
                                    ImageUrl="~/Image/deletion.png" HeaderText="Удалить"
                                    >  
                                <ControlStyle Height="15px" Width="15px" />
                            </asp:ButtonField>
                          </Columns>                
                </asp:GridView>
                <asp:Button ID="btnEditCustomer" Text="Добавить" runat="server" />
             </div>
             <asp:Panel ID="UpdatePanel" runat="server"  
                  BackColor="#D9F2FF" BorderStyle="Double">
                <p style="float:right">
                <asp:ImageButton ID="editBox_OK" runat="server" ImageUrl= "~/Image/Close.ico" Width="20" Height = "20"/>
                </p>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Labe4" Text="Редактирование устройств" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" Text="Наименоване"  runat="server"/> 
                        </td> 
                        <td align="left">
                            <asp:TextBox ID="TextBox2"  Text='<%# Eval("NameDevice") %>' runat="server" Width="160px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" Text="Описание" runat="server"/> 
                        </td> 
                        <td align="left">
                            <asp:TextBox ID="Description" Text='<%# Eval("Description") %>' runat="server" Height="93px" 
                                style="margin-left: 0px" TextMode="MultiLine" Width="155px"></asp:TextBox>
                        </td>
                   </tr>
                    <tr>
                        <td valign="top" >
                            &nbsp;</td>
                        <td valign="top">
                    </td>
                    </tr>
                </table>
            <asp:Button ID="UpdateButton" runat="server" Text="Обновить" CommandName="Update" 
                    OnCommand="CommandBtn_Click" Visible="false"/>
            <asp:Button ID="InsertButton" runat="server" Text="Добавить" CommandName="Insert" 
                    OnCommand="CommandBtn_Click"/>
            <asp:Button ID="DeleteButton" runat="server" Text="Удалить" CommandName="Delete" 
                    OnCommand="CommandBtn_Click" Visible="false"/>
            </asp:Panel>
             <asp:ModalPopupExtender ID="ModalPopupExtender1"
                    runat="server"  
                    PopupControlID="UpdatePanel"
                    TargetControlID="btnEditCustomer"
                    OkControlID="editBox_OK"
                    BackgroundCssClass = "modalBackground" 
                    PopupDragHandleControlID = "Редактирование записи" Drag="True"
                    DynamicServicePath=""
                    Enabled="true"/>
             <asp:DragPanelExtender ID="UpdatePanel_DragPanelExtender" runat="server" 
                  DragHandleID="UpdatePanel" Enabled="True" 
                  TargetControlID="UpdatePanel">
              </asp:DragPanelExtender>

</asp:Content>
