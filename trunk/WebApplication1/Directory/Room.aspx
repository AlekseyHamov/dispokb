<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="Room.aspx.cs" Inherits="WebApplication1.Directory.Room" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
&nbsp;<script runat="server">
</script><asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
      <h3>Справочник Комнат</h3> 
      <asp:Label id="Msg" runat="server" ForeColor="Red" />
      <asp:ObjectDataSource 
        ID="RoomObjectDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataRoom.RoomData" 
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
              <asp:controlparameter name="ID_Building" controlid="BuildingList" propertyname="SelectedValue" />
              <asp:controlparameter name="ID_Otdelen" controlid="OtdelenList" propertyname="SelectedValue" />
              <asp:ControlParameter ControlID="TextBox2" Name="NameRoom" 
                  PropertyName="Text" />
              <asp:ControlParameter ControlID="TextBoxNum" Name="Num" 
                  PropertyName="Text" />
        </InsertParameters>
        <deleteparameters>
            <asp:controlparameter name="ID_Room" controlid="RoomGridView" propertyname="SelectedValue" />
        </deleteparameters>
        <updateparameters>
            <asp:controlparameter name="ID_Room" controlid="RoomGridView" propertyname="SelectedValue" />
            <asp:controlparameter name="ID_Building" controlid="BuildingList" propertyname="SelectedValue" />
            <asp:controlparameter name="ID_Otdelen" controlid="OtdelenList" propertyname="SelectedValue" />
            <asp:ControlParameter ControlID="TextBox2" Name="NameRoom" 
                PropertyName="Text" />
            <asp:ControlParameter ControlID="TextBoxNum" Name="Num" 
                  PropertyName="Text" />
        </updateparameters>
      </asp:ObjectDataSource>
      <asp:ObjectDataSource 
        ID="RoomObjectDataSourceOneRow" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataRoom.RoomData" 
        ConflictDetection="CompareAllValues"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetOneRecord" 
        InsertMethod="InsertRecord" 
        UpdateMethod="UpdateRecord" 
        DeleteMethod="DeleteRecord"
        OnInserted="DataSource_OnInserted"
        OnUpdated="DataSource_OnUpdated"
        OnDeleted="DataSource_OnDeleted" >
        <SelectParameters>
            <asp:ControlParameter ControlID="RoomGridView" DefaultValue="" 
                Name="ID_Room" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters> 
        <InsertParameters>
              <asp:controlparameter name="ID_Building" controlid="BuildingList" propertyname="SelectedValue" />
              <asp:controlparameter name="ID_Otdelen" controlid="OtdelenList" propertyname="SelectedValue" />
              <asp:ControlParameter ControlID="TextBox2" Name="NameRoom" 
                  PropertyName="Text" />
              <asp:ControlParameter ControlID="TextBoxNum" Name="Num" 
                  PropertyName="Text" />
            <asp:Parameter Name="newID_Room" Direction="Output" 
                           Type="Int32" DefaultValue="0" />
        </InsertParameters>
        <deleteparameters>
            <asp:controlparameter name="ID_Room" controlid="RoomGridView" propertyname="SelectedValue" />
        </deleteparameters>
        <updateparameters>
            <asp:controlparameter name="ID_Room" controlid="RoomGridView" propertyname="SelectedValue" />
            <asp:controlparameter name="ID_Building" controlid="BuildingList" propertyname="SelectedValue" />
            <asp:controlparameter name="ID_Otdelen" controlid="OtdelenList" propertyname="SelectedValue" />
            <asp:ControlParameter ControlID="TextBox2" Name="NameRoom" 
                PropertyName="Text" />
            <asp:ControlParameter ControlID="TextBoxNum" Name="Num" 
                  PropertyName="Text" />
        </updateparameters>
      </asp:ObjectDataSource>
      <asp:ObjectDataSource 
        ID="BuildingObjectDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataBuilding.BuildingData" 
        SortParameterName="SortColumns"
        EnablePaging="true"
        SelectCountMethod="SelectCount"
        DeleteMethod="DeleteBuilding"
        StartRowIndexParameterName="StartRecord"
        MaximumRowsParameterName="MaxRecords" 
        SelectMethod="GetAllBuilding" 
        InsertMethod="InsertBuilding" 
        UpdateMethod="UpdateBuilding" >
          <InsertParameters>
              <asp:ControlParameter ControlID="TextBox2" Name="NameBuilding" 
                  PropertyName="Text" />
          </InsertParameters>
        <deleteparameters>
            <asp:controlparameter name="ID_Building" controlid="BuildingList" propertyname="SelectedValue" />
        </deleteparameters>
        <updateparameters>
            <asp:controlparameter name="ID_Building" controlid="BuildingList" propertyname="SelectedValue" />
            <asp:ControlParameter ControlID="TextBox2" Name="NameBuilding" 
                PropertyName="Text" />
        </updateparameters>
      </asp:ObjectDataSource>
      <asp:ObjectDataSource 
        ID="OtdelenObjectDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataOtdelen.OtdelenData" 
        SortParameterName="SortColumns"
        EnablePaging="true"
        SelectCountMethod="SelectCount"
        StartRowIndexParameterName="StartRecord"
        MaximumRowsParameterName="MaxRecords" 
        SelectMethod="GetAllOtdelen"> 
      </asp:ObjectDataSource>
      <asp:ObjectDataSource 
        ID="PersonObjectDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataPerson.PersonData" 
        SelectMethod="GetUnit">
      </asp:ObjectDataSource>
      <asp:ObjectDataSource 
        ID="RoomDeviceListDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataRoomDeviceList.RoomDeviceListData" 
        SelectMethod="GetUnit"
        InsertMethod="InsertRecord"
        DeleteMethod="DeleteRecord">
        <DeleteParameters>
              <asp:ControlParameter ControlID="CheckBoxDevice" Name="ID_Device"
                  PropertyName="SelectedValue" />
              <asp:ControlParameter ControlID="RoomGridView" Name="ID_Room"
                  PropertyName="SelectedValue" />
        </DeleteParameters>  
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
        OnDeleted="DataSource_OnDeleted">
        <SelectParameters>
            <asp:ControlParameter ControlID="RadioButtonUnit" Name="ID_Unit" PropertyName="SelectedValue"/>
            <asp:Parameter Name="str_ID" DefaultValue=""/>
        </SelectParameters>  
        <InsertParameters>
              <asp:ControlParameter ControlID="TextBox2" Name="NameDevice"
                  PropertyName="Text" />
              <asp:ControlParameter ControlID="Description" Name="Description" 
                  PropertyName="Text" />
        </InsertParameters>
        <deleteparameters>
            <asp:controlparameter name="ID_Device" controlid="DeviceGridView" propertyname="SelectedValue" />
        </deleteparameters>
        <updateparameters>
            <asp:controlparameter name="ID_Device" controlid="DeviceGridView" propertyname="SelectedValue" />
            <asp:ControlParameter ControlID="TextBox2" Name="NameDevice" 
                  PropertyName="Text" />
              <asp:ControlParameter ControlID="Description" Name="Description" 
                  PropertyName="Text" />
        </updateparameters>
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
      <br />
      <div style="float:left; width:680px">
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
            <asp:CheckBox ID="CheckBuilding" runat="server"/>
                <asp:DropDownList ID="FiltrBuilding" runat="server" 
                    DataSourceID="BuildingObjectDataSource"
                    DataTextField="NameBuilding" DataValueField="ID_Building" />
            <asp:CheckBox ID="CheckOtdelen" runat="server"/>
                <asp:DropDownList ID="FiltrOtdelen" runat="server" 
                    DataSourceID="OtdelenObjectDataSource"
                    DataTextField="NameOtdelen" DataValueField="ID_Otdelen" 
                 />
                <asp:Button id="Button5" OnCommand="button_filtr" AutoPostBack="true" runat="server" text="Поиск"/>
                <br />
                <asp:CheckBox ID="FilterClear" runat="server" Text="Без фильтра" Checked="False"  />
                </td>
        </tr>
      </table>
      <table cellspacing="10">
        <tr>
          <td valign="top">
            <asp:GridView ID="RoomGridView" 
              DataSourceID="RoomObjectDataSource" 
              AutoGenerateColumns="False"
              AllowSorting="True"
              AllowPaging="True"
              PageSize="18"
              DataKeyNames="ID_Room"
              OnSelectedIndexChanged="GridView_OnSelectedIndexChanged"
              RunAt="server">
              <HeaderStyle backcolor="lightblue" forecolor="black"/>
              <Columns>                
                <asp:ButtonField
                                 CommandName="Select" ButtonType="Image" 
                      ImageUrl="~/Image/edit.png" FooterText="Проба">  
                  <ControlStyle Height="15px" Width="15px" />
                </asp:ButtonField>
                <asp:BoundField DataField="ID_Room" HeaderText="Номер п/п" 
                      SortExpression="ID_Room" 
                      Visible="False" />
                <asp:BoundField 
                        DataField="NameRoom"
                        HeaderText="Наименование" 
                      SortExpression="NameRoom" />
                <asp:BoundField 
                        DataField="Num"
                        HeaderText="Ном. комнаты"
                      SortExpression="Num" />
                <asp:BoundField 
                        DataField="NameBuilding"
                        HeaderText="Здание"
                      SortExpression="NameBuilding" />
                <asp:BoundField 
                        DataField="NameOtdelen"
                        HeaderText="Отделение"
                      SortExpression="NameOtdelen" />
                <asp:ButtonField
                        CommandName="Delete" ButtonType="Image" 
                        ImageUrl="~/Image/deletion.png" HeaderText="Удалить"
                        >  
                    <ControlStyle Height="15px" Width="15px" />
                </asp:ButtonField>
              </Columns>                
            </asp:GridView>            
            <asp:Button ID="btnEditCustomer"  Text = "добавить" runat="server"  />
          </td>
        </tr>
        <tr>
          <td>
             <asp:Panel ID="UpdatePanel" runat="server"  
                  BackColor="#D9F2FF" BorderStyle="Double" >
                  <table>
                    <tr >
                        <td align="left" >
                          <asp:Label ID="Label3" runat="server" Text="Редактирование комнат"></asp:Label>
                        </td> 
                        <td align="right">
                          <asp:ImageButton ID="editBox_OK" runat="server" ImageUrl= "~/Image/Close.ico" Width="20" Height = "20"  />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" >
                            Наименование комнаты</td>
                        <td align="left">
                            <asp:TextBox ID="TextBox2"  runat="server" Width="160px"></asp:TextBox>
                        </td>
                   </tr>
                   <tr>
                        <td align="right" >
                            Номер комнаты</td>
                         <td align="left">
                             <asp:TextBox ID="TextBoxNum" runat="server" Width="160px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelNameBuilging" runat="server" Text="Здание корпус: "></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="BuildingList" runat="server"
                            DataSourceID="BuildingObjectDataSource" DataTextField="NameBuilding" 
                            DataValueField="ID_Building" OnSelectedIndexChanged ="BuildingList_OnSelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>   
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Отделение</td>
                        <td>
                            <asp:DropDownList ID="OtdelenList"  runat="server" 
                                DataSourceID="OtdelenObjectDataSource" DataTextField="NameOtdelen" 
                                DataValueField="ID_Otdelen">
                            </asp:DropDownList>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            Этаж</td>
                        <td>
                            <asp:Label ID="TextBoxFloor" runat="server" Width="160px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                    <td>
                        <div id="Div1" runat="server" style="height:150px; width:auto; overflow:auto">  
                        <asp:RadioButtonList ID="RadioButtonUnit" runat="server" 
                            DataSourceID="UnitObjectDataSource" DataValueField ="ID_Unit" 
                            DataTextField="NameUnit" OnSelectedIndexChanged = "Filter_device" AutoPostBack="true" >
                        </asp:RadioButtonList>
                        </div>
                    </td>
                    <td>
                    <div runat="server" style="height:150px; overflow:auto; width:auto">    
                        <asp:CheckBoxList ID="CheckBoxDevice" runat="server" DataSourceID="DeviceObjectDataSource"
                         DataTextField="NameDevice" DataValueField="ID_Device" >
                        </asp:CheckBoxList>
                    </div>
                    </td>
                    </tr>
                  </table>
                  <p style="display:inline; float:right" >
                          <asp:Button ID="UpdateButton" runat="server" Text="Обновить" CommandName="Update" 
                                 OnCommand="CommandBtn_Click" Visible="false"/>
                          <asp:Button ID="InsertButton" runat="server" Text="Добавить" CommandName="Insert" 
                                 OnCommand="CommandBtn_Click"/>
                          <asp:Button ID="DeleteButton" runat="server" Text="Удалить" CommandName="Delete" 
                                 OnCommand="CommandBtn_Click" Visible="false"/>
                  </p>
             </asp:Panel>
             <asp:DetailsView Visible="False" ID="UpdatePan" AutoGenerateRows="true" 
                              DataSourceID="RoomObjectDataSourceOneRow" runat="server" >
            </asp:DetailsView>
            <asp:ModalPopupExtender ID="ModalPopupExtender1"
                    runat="server"  
                    PopupControlID="UpdatePanel"
                    TargetControlID="btnEditCustomer"
                    OkControlID="editBox_OK"
                    BackgroundCssClass = "modalBackground" 
                    PopupDragHandleControlID = "Редактирование записи" Drag="True"
                    />
          </td>
          <td>
              &nbsp;</td>
        </tr>
      </table>
      </div>
      <div>
              Справочник Комнат. Его необходимо запонлнять одним из первых. На странице доступна возможность фильтрации 
              по заданиям и отделениям. В справочнике необходимо выполнять привязку к установленному в помещении оборудованию и 
              приборам, для удобства выбора оборудования сиписок устройств фильтруется по службам.
              В дальнейшем справочник используется для удобства поискаи фильтрации информациии,
              для получения укрупненной статистики и построения отчетов.
      </div>
    </asp:Content>
