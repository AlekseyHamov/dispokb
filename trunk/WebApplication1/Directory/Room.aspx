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
        DeleteMethod="DeleteRecord"
        UpdateMethod="UpdateRecord">
        <DeleteParameters>
            <asp:Parameter Name="ID_Device" DefaultValue="0" />
            <asp:ControlParameter ControlID="RoomGridView" Name="ID_Room"
                  PropertyName="SelectedValue" />
        </DeleteParameters>  
        <UpdateParameters>
            <asp:Parameter Name="ID_Device" DefaultValue="0" />
            <asp:ControlParameter ControlID="RoomGridView" Name="ID_Room"
                PropertyName="SelectedValue" />
            <asp:Parameter Name="CountDevice" DefaultValue="0" />            
        </UpdateParameters>  
        <InsertParameters>
            <asp:Parameter Name="ID_Device" DefaultValue="0" />
            <asp:ControlParameter ControlID="RoomGridView" Name="ID_Room"
                PropertyName="SelectedValue" />
            <asp:Parameter Name="CountDevice" DefaultValue="0" />            
        </InsertParameters>  
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

      <asp:ObjectDataSource 
        ID="ImageChecked" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataDevice.DeviceData" 
        SelectMethod="GetAllImage">
        <SelectParameters>
            <asp:ControlParameter ControlID="RadioButtonUnit" Name="ID_Unit" PropertyName="SelectedValue"/>
            <asp:Parameter Name="str_ID" DefaultValue=""/>
            <asp:ControlParameter Name="ID_Room" controlid="RoomGridView" propertyname="SelectedValue" DefaultValue="0"/>
        </SelectParameters> 
      </asp:ObjectDataSource>

      <asp:ObjectDataSource 
        ID="ImageObjectDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataImage.ImageData" 
        InsertMethod="AddEmployee"
        DeleteMethod="DeleteImage"
        SelectMethod="FileRelationList"
        OnDeleted = "ImageDataSource_OnDeleted" >
        <SelectParameters>
            <asp:Parameter Name="NameTable"  DefaultValue = "Room" />
            <asp:ControlParameter Name= "ID_Table" ControlID="RoomGridView" PropertyName="SelectedValue" DefaultValue="0" />  
        </SelectParameters> 
        <DeleteParameters>
            <asp:ControlParameter Name= "ID_files" ControlID="LWImage" PropertyName="SelectedValue" DefaultValue="0" />  
        </DeleteParameters> 
      </asp:ObjectDataSource>
      <asp:ObjectDataSource 
        ID="ObjectDataTempGrig" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataImage.ImageData" 
        SelectMethod="GetTempGrid" >
        <SelectParameters>
            <asp:Parameter Name= "NameTable" DefaultValue="Otdelen" />  
            <asp:ControlParameter Name= "ID_Table" ControlID="RoomGridView" PropertyName="SelectedValue" DefaultValue="0" />  
        </SelectParameters> 
      </asp:ObjectDataSource>
      <asp:ObjectDataSource 
        ID="ImageFilesObjectDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataImage.ImageData" 
        InsertMethod="AddEmployee"
        >
      </asp:ObjectDataSource>
      <asp:ObjectDataSource 
        ID="FileCoordimateDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataImage.ImageData" 
        SelectMethod="FileCoordinate" 
        >
        <SelectParameters>
            <asp:ControlParameter Name= "ID_files" ControlID="LWImage" PropertyName="SelectedValue" DefaultValue="0" />  
        </SelectParameters> 
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
            <asp:GridView ID="RoomGridView" 
              DataSourceID="RoomObjectDataSource" 
              AutoGenerateColumns="False"
              AllowSorting="True"
              AllowPaging="True"
              PageSize="18"
              DataKeyNames="ID_Room,ID_Otdelen"
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
            <asp:Panel ID="UpdatePanel" runat="server" BackColor="#D9F2FF" BorderStyle="Double" >
               <div>
                <div style="float:left" >
                    <asp:Label ID="Label3" runat="server" Text="Редактирование комнат"></asp:Label>
                </div>
                <div style="float:right" >
                    <asp:ImageButton ID="editBox_OK" runat="server" ImageUrl= "~/Image/Close.ico" Width="20" Height = "20" />
                </div>
            </div>
            <br />
            <div> 
                <caption>
                    Наименование комнаты
                    <asp:TextBox ID="TextBox2" runat="server" Width="160px"></asp:TextBox>
                </caption>
            </div>
            <div>
                <caption>
                    Номер комнаты
                    <asp:TextBox ID="TextBoxNum" runat="server" Width="160px"></asp:TextBox>
                </caption>
            </div> 
            <div>
                <asp:Label ID="LabelNameBuilging" runat="server" Text="Здание корпус: "></asp:Label>
                <asp:DropDownList ID="BuildingList" runat="server"
                DataSourceID="BuildingObjectDataSource" DataTextField="NameBuilding" 
                DataValueField="ID_Building" OnSelectedIndexChanged ="BuildingList_OnSelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>   
            </div>
            <div>
                <caption>
                    Отделение
                    <asp:DropDownList ID="OtdelenList" runat="server" 
                        DataSourceID="OtdelenObjectDataSource" DataTextField="NameOtdelen" 
                        DataValueField="ID_Otdelen">
                    </asp:DropDownList>
                </caption>
            </div>
            <div>
                Этаж
                <asp:Label ID="TextBoxFloor" runat="server" Width="160px"></asp:Label>
            </div>
            <table>
            <tr>
            <td>
                <div>
                    <div id="Div1" runat="server" style="height:220px; width:150px; overflow:auto; float:left ">  
                        <asp:RadioButtonList ID="RadioButtonUnit" runat="server" 
                            DataSourceID="UnitObjectDataSource" DataValueField ="ID_Unit" 
                            DataTextField="NameUnit" OnSelectedIndexChanged = "Filter_device" AutoPostBack="true" >
                        </asp:RadioButtonList>
                    </div>
                    <asp:MultiView ID="MultiViewDevice" runat="server" ActiveViewIndex="0" >
                        <asp:View ID="View1" runat="server">
                                <table>
                                    <tr>
                                    <td>
                                        <div id="HeaderDeviceScrol" runat="server" style="height:220px; width:350px; overflow-x:scroll; float:left " >
                                        <asp:ListView  ID="CheckBoxImage" runat="server" 
                                            RepeatDirection="Horizontal"
                                            DataKeyNames="ID,ID_files,fileName,fileType,NameDevice, ID_Device, roomdevice,roomdevicecount" 
                                            DataSourceID="ImageChecked" GroupItemCount="3" >
                                            <GroupTemplate>
                                                <tr>
                                                  <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                                                </tr>
                                            </GroupTemplate>
                                            <LayoutTemplate>
                                                <table>
                                                    <asp:PlaceHolder ID="groupPlaceholder" runat="server" />
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <td style="border-style:ridge; border-width:thin;  "     >
                                                    <asp:ImageMap ID="IMG" runat="server" Height="50" 
                                                      ImageUrl='<%#Eval("fileType", "~/Image_Data/"+Eval("fileName")+"_"+Eval("ID_files")+"."+Eval("fileType")) %>'  ToolTip='<%#Eval("NameDevice") %>' />
                                                    <br />
                                                    <asp:CheckBox ID="IMGCHECK" runat="server" Checked='<%# Eval("roomdevice") %>' ToolTip="Выбрать" />
                                                    <br />
                                                    <asp:TextBox ID="CountDevice" runat="server" Width="17" Text='<%# Eval("roomdevicecount") %>' ToolTip="Колличество"  />
                                                </td>
                                            </ItemTemplate>
                                        </asp:ListView >
                                        </div>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td>
                                        <asp:Button id="Page2Back"
                                            Text = "Previous"
                                            OnClick="BackButton_Command"
                                            runat="Server" Visible="false">
                                        </asp:Button> 
                                        <asp:Button id="Page2Next"
                                            Text = "Next"
                                            OnClick="NextButton_Command"
                                            runat="Server" Visible="false">
                                        </asp:Button>
                                    </td>
                                    </tr>
                                </table>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </td>
            </tr>
            <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            Дополнительно
                         </td>
                        <td>
                            <img  alt="" src="../Image/Downarrow.png"  style = " width :10px; height :10px;"
                          onclick= "document.getElementById('ImageHide').style.display=''" />
                        </td>
                        <td>
                            <img  alt="" src="../Image/Uparrow.png" style = " width :10px; height :10px;" 
                          onclick= "document.getElementById('ImageHide').style.display='none'" />
                        </td>
                    </tr>
                    <tr ID="ImageHide" style="display:none ">
                        <td>
                            <div>
                                <div style="float:left">
                                    <asp:GridView ID="LWImage" runat="server" AutoGenerateColumns="false" 
                                        DataKeyNames="ID,ID_files,fileName,fileType" 
                                        DataSourceID="ImageObjectDataSource" Height="144px" 
                                        OnSelectedIndexChanged="LWImage_SelectedIndexChanged">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Image" CommandName="Select" FooterText="Проба" 
                                                HeaderText="Ред." ImageUrl="~/Image/edit.png">
                                            <ControlStyle Height="15px" Width="15px" />
                                            </asp:ButtonField>
                                            <asp:TemplateField HeaderText="Карта.">
                                                <ItemTemplate>
                                                    <asp:ImageMap ID="IMG" runat="server" Height="50" 
                                                        ImageUrl='<%#Eval("fileType", "~/Image_Data/"+Eval("fileName")+"_"+Eval("ID_files")+"."+Eval("fileType")) %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:ButtonField ButtonType="Image" CommandName="Delete" HeaderText="Удалить" 
                                                ImageUrl="~/Image/deletion.png">
                                            <ControlStyle Height="15px" Width="15px" />
                                            </asp:ButtonField>
                                        </Columns>
                                    </asp:GridView>
                                </div>                                
                                <div>
                                    Изображение
                                    <asp:FileUpload ID="ImageFile" runat="server" />
                                    <br />
                                    <asp:Button ID="AddImge" runat="server" OnClick="Image_OnInserted" 
                                        Text="Добавить изображение" 
                                        ToolTip="Добавляет изображение к существующей записи" Visible="false" />
                                    <br />
                                    <br />
                                    Привязать к месту на карте
                                    <br />
                                    <asp:Button ID="MapRelation" runat="server" onclick="MapRelation_Click" 
                                        Text="Привязать" ToolTip="Привязать к месту на карте" Visible="true" />
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            </tr>
            <tr>
            <td>
                <div>
                    <div style="float:right">
                        <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                            OnCommand="CommandBtn_Click" Text="Обновить" Visible="false" />
                        <asp:Button ID="InsertButton" runat="server" CommandName="Insert" 
                            OnCommand="CommandBtn_Click" Text="Добавить" />
                        <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" 
                            OnCommand="CommandBtn_Click" Text="Удалить" Visible="false" />
                    </div>
                </div>
            </td>
            </tr>
            </table>
            </asp:Panel>
            <asp:Panel runat="server" ID="ImageMapingPanel" BackColor="#ffffff">
                <asp:GridView ID="TempGrid" runat="server" >
                </asp:GridView>
                <div style="text-align:right" >
                    <asp:ImageButton runat="server" id="CloseImageMapingPanel" ImageUrl="~/Image/Close.ico"   Height="15px" Width="15px"/>
                </div>
                <div id="ImageBM" runat="server" style="overflow:auto ; width:auto">
                    <asp:ImageButton runat="server" id="ImgButOne" Width="500" onclick="ImageButton_Click" Visible="false" />
                    <asp:ImageMap runat="server" id="ImgMapOne" Width="500" >
                    </asp:ImageMap>
                <div style="float:right" >
                    <asp:GridView ID="ImageChildren" runat="server" AutoGenerateColumns="false"
                                DataSourceID="FileCoordimateDataSource" 
                                DataKeyNames="ID,ID_files,Coordinate,AlternateText" >
                        <Columns>
                            <asp:ButtonField HeaderText = "Ред."
                                                CommandName="Select" ButtonType="Image" 
                                    ImageUrl="~/Image/edit.png" FooterText="Проба">  
                                <ControlStyle Height="15px" Width="15px" />
                            </asp:ButtonField>
                            <asp:TemplateField  HeaderText = "Ссылка на"> 
                                <ItemTemplate>
                                <asp:Label ID="MapText" runat="server" Text='<%#Eval("AlternateText") %>' />
                                </ItemTemplate> 
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView> 
                </div>
                </div>
                <div>
                    <asp:CheckBox ID="ChangeMap" runat="server" AutoPostBack="true" Checked="false" 
                        oncheckedchanged="ChangeMap_CheckedChanged" Text="Задать ссылки по карте" />
                    <a ID="UnitMap" runat="server" visible="false">
                    <br />
                    <asp:TextBox ID="MapText" runat="server" Text="Описание выделенного участка" />
                    <br />
                    <asp:Button ID="DelCoordinate" runat="server" OnClick="DelCoordinate_Click" 
                        Text="Записать область" />
                    <br />
                    <asp:Label ID="OX" runat="server" ToolTip="Координата по оси Х"></asp:Label>
                    <br />
                    <asp:Label ID="OY" runat="server" ToolTip="Координата по оси Y"></asp:Label>
                    <br />
                    <asp:Label ID="Coordin" runat="server" ToolTip="Координ"></asp:Label>
                    </a>
                </div>
            </asp:Panel>
            <asp:DetailsView Visible="False" ID="UpdatePan" AutoGenerateRows="true" 
                              DataSourceID="RoomObjectDataSourceOneRow" runat="server" >
            </asp:DetailsView>
            <asp:Label runat="server" id="aliona" />
            <asp:ModalPopupExtender ID="ModalPopupExtender1"
                    runat="server"  
                    PopupControlID="UpdatePanel"
                    TargetControlID="btnEditCustomer"
                    OkControlID="editBox_OK"
                    BackgroundCssClass = "modalBackground" 
                    PopupDragHandleControlID = "Редактирование записи" Drag="True"
                    />
            <asp:ModalPopupExtender runat="server" ID = "ModalImageMaping"
                PopupControlID = "ImageMapingPanel"
                TargetControlID ="aliona"
                OkControlID = "CloseImageMapingPanel"
                BackgroundCssClass = "modalBackground" 
                PopupDragHandleControlID = "Ссылки на рисунке" Drag="True"/>
      </div>
      <div>
              Справочник Комнат. Его необходимо запонлнять одним из первых. На странице доступна возможность фильтрации 
              по заданиям и отделениям. В справочнике необходимо выполнять привязку к установленному в помещении оборудованию и 
              приборам, для удобства выбора оборудования сиписок устройств фильтруется по службам.
              В дальнейшем справочник используется для удобства поискаи фильтрации информациии,
              для получения укрупненной статистики и построения отчетов.
      </div>
      <div style="display:inline" >
            <asp:ImageMap ID="MapPage" runat="server" Visible="false" Width="500" 
                onclick="MapPage_Click" />
            <div id="DivRightPage" runat="server" style="float:right" >
            </div>
      </div>
    </asp:Content>
