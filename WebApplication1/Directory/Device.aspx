<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="Device.aspx.cs" Inherits="WebApplication1.Directory.Device" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script runat="server">
</script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
      <h3>Справочник Устройств</h3> 
      <asp:Label id="Msg" runat="server" ForeColor="Red" />
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
        <SelectParameters>
            <asp:Parameter Name="str_ID" DefaultValue="" />  
            <asp:ControlParameter Name="ID_Unit" ControlID="FiltrRadioUnit" PropertyName="SelectedValue" DefaultValue="" />  
        </SelectParameters> 
        <InsertParameters>
              <asp:ControlParameter ControlID="TextBox2" Name="NameDevice" 
                  PropertyName="Text" />
              <asp:ControlParameter ControlID="Description" Name="Description" 
                  PropertyName="Text" />
              <asp:ControlParameter ControlID="RadioButtonUnit" Name="ID_Unit" 
                  PropertyName="SelectedValue" />
        </InsertParameters>
        <deleteparameters>
            <asp:controlparameter name="ID_Device" controlid="GridDevice" propertyname="SelectedValue" />
        </deleteparameters>
        <updateparameters>
            <asp:controlparameter name="ID_Device" controlid="GridDevice" propertyname="SelectedValue" />
            <asp:ControlParameter ControlID="TextBox2" Name="NameDevice" 
                  PropertyName="Text" />
              <asp:ControlParameter ControlID="Description" Name="Description" 
                  PropertyName="Text" />
              <asp:ControlParameter ControlID="RadioButtonUnit" Name="ID_Unit" 
                  PropertyName="Text" />
        </updateparameters>
      </asp:ObjectDataSource>
      <asp:ObjectDataSource 
        ID="TreeDeviceObjectDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataDevice.DeviceData" >
      </asp:ObjectDataSource>
      <asp:ObjectDataSource 
        ID="CheckDeviceObjectDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataDevice.DeviceData" 
        SelectMethod ="GetForCheck">
        <SelectParameters>
            <asp:controlparameter name="ID_Device_Spares" controlid="GridDevice" propertyname="SelectedValue" />
        </SelectParameters> 
      </asp:ObjectDataSource>
      <asp:ObjectDataSource 
        ID="UnitObjectDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataUnit.UnitData" 
        SortParameterName="SortColumns"
        EnablePaging="true"
        SelectCountMethod="SelectCount"
        StartRowIndexParameterName="StartRecord"
        MaximumRowsParameterName="MaxRecords" 
        SelectMethod="GetAll" 
        InsertMethod="InsertRecord" 
        OnInserted="DataSource_OnInserted">
        <InsertParameters>
              <asp:ControlParameter ControlID="TextBox2" Name="NameUnit" 
                  PropertyName="Text" />
        </InsertParameters>
      </asp:ObjectDataSource>
      <div style="float:left; width:700px">
        <table >
        <tr>
            <td>Фильтр</td>
            <td>
                <img  alt="" src="../Image/Downarrow.png"  style = " width :10px; height :10px;"
                onclick= "document.getElementById('Filtr').style.display=''" />
            </td>
            <td>
                <img  alt="" src="../Image/Uparrow.png" style = " width :10px; height :10px;" 
                onclick= "document.getElementById('Filtr').style.display='none'" />
            </td>
        </tr>
        <tr id="Filtr" style = "display:none " >
        <td>
            <BR>
            <asp:Button runat="server" id="FiltrButton" Text="Поиск" OnCommand="button_filtr" />
            <BR>
            <asp:CheckBox ID="FilterClear" runat="server" AutoPostBack="true" Text="Без фильтра" OnCheckedChanged="button_filtr" />
        </td>
        </tr>
        </table>
          <div style="float:left" >
            <asp:TreeView 
            ID="TreeView1"
            ExpandDepth="0" 
            runat="server"
            OnTreeNodePopulate ="TreeNodePopulate"
            OnSelectedNodeChanged= "Select_Change"
            OnTreeNodeExpanded="Select_Change1"
            OnTreeNodeCollapsed="Select_Change1" 
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
            <asp:GridView ID="GridDevice" runat="server" 
                  DataSourceID="DeviceObjectDataSource" 
                  AutoGenerateColumns="False"
                  AllowSorting="True"
                  AllowPaging="True"
                  PageSize="18"
                  DataKeyNames="ID_Device"
                  OnSelectedIndexChanged="GridView_OnSelectedIndexChanged">
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
                            <asp:BoundField 
                                    DataField="Description"
                                    HeaderText="Описание" />
                            <asp:BoundField 
                                    DataField="Unit"
                                    Visible="false"/>
                            <asp:ButtonField
                                    CommandName="Delete" ButtonType="Image" 
                                    ImageUrl="~/Image/deletion.png" HeaderText="Удалить"
                                    >  
                                <ControlStyle Height="15px" Width="15px" />
                            </asp:ButtonField>
                          </Columns>                
                </asp:GridView>
          </div>
            <asp:RadioButtonList ID="FiltrRadioUnit" runat="server" 
                DataSourceID="UnitObjectDataSource" 
                DataTextField="NameUnit" 
                DataValueField="ID_Unit" 
                RepeatDirection="Horizontal"
                OnSelectedIndexChanged="Unit_Click_Select" 
                AutoPostBack="true" >
            </asp:RadioButtonList>
            <br>  
            <asp:Button ID="btnEditCustomer" Text="Добавить" runat="server"/>
            <asp:Button id="Submit"
            Text="Select Items"
            OnUnload="Button_Click"  
            runat="server"/>
        <table>
        <tr>
          <td>
              <asp:Panel ID="UpdatePanel" runat="server"  
                  BackColor="#D9F2FF" BorderStyle="Double" OnLoad="Button_Click_Insert"  >
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
                            <asp:TextBox ID="TextBox2" runat="server" Width="160px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" Text="Описание" runat="server"/> 
                        </td> 
                        <td align="left">
                            <asp:TextBox ID="Description"  runat="server" Height="28px" 
                                style="margin-left: 0px" TextMode="MultiLine" Width="155px"></asp:TextBox>
                        </td>
                   </tr>
                </table>
                    <div runat="server" id="DivUpdatePanel" visible="true" style="height:350px; width:auto; overflow:auto; float:left" >   
                        <asp:TreeView 
                        ID="TreeViewUpdate" 
                        ExpandDepth="0" 
                        runat="server"
                        OnTreeNodePopulate ="TreeNodePopulate_Update"
                        EnableViewState="true" 
                        ShowCheckBoxes="All" >
                        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" 
                         HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="1px" />
                        <ParentNodeStyle Font-Bold="False" />
                        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" 
                         HorizontalPadding="0px" VerticalPadding="0px" />
                        </asp:TreeView>
                    </div>
                    <div style="height:350px; width:auto; overflow:auto">
                            <asp:CheckBoxList ID="CheckBoxParent" runat="server" DataSourceID="CheckDeviceObjectDataSource"
                                DataTextField="NameDevice" DataValueField="Device">
                            </asp:CheckBoxList>
                    </div>
            <asp:RadioButtonList ID="RadioButtonUnit" AppendDataBoundItems="true" runat="server"
                DataSourceID = "UnitObjectDataSource" DataTextField="NameUnit" DataValueField="ID_Unit" 
                RepeatDirection="Horizontal" Enabled="false">
            </asp:RadioButtonList>
            <asp:Button ID="UpdateButton" runat="server" Text="Обновить" CommandName="Update" 
                    OnCommand="CommandBtn_Click" Visible="false"/>
            <asp:Button ID="InsertButton" runat="server" Text="Добавить" CommandName="Insert" 
                    OnCommand="CommandBtn_Click"/>
            <asp:Button ID="DeleteButton" runat="server" Text="Удалить" CommandName="Delete" 
                    OnCommand="CommandBtn_Click" Visible="false"/>
            <asp:Button ID="Button1" runat="server" Text="gfdjhgdjk" onclick="Button_Click"
                    />
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
          </td>
          <td>
              &nbsp;</td>
        </tr>
      </table>
      </div>
      <div>
            Справочник Устройств. Его необходимо запонлнять одним из первых. На странице доступна возможность фильтрации 
            по подразделениям. В справочнике необходимо выполнять привязку к подразделению.
            В дальнейшем справочник используется для удобства поискаи фильтрации информациии,
            для получения укрупненной статистики и построения отчетов.
      </div> 
    </asp:Content>
