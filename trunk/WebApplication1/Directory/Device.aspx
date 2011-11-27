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
      
      <asp:ObjectDataSource 
        ID="ImageObjectDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataImage.ImageData" 
        InsertMethod="AddEmployee"
        DeleteMethod="DeleteImage"
        SelectMethod="FileRelationList"
        OnDeleted = "ImageDataSource_OnDeleted" >
        <SelectParameters>
            <asp:Parameter Name="NameTable"  DefaultValue = "Device" />
            <asp:ControlParameter Name= "ID_Table" ControlID="GridDevice" PropertyName="SelectedValue" DefaultValue="0" />  
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
            <asp:Parameter Name= "NameTable" DefaultValue="Device" />  
            <asp:ControlParameter Name= "ID_Table" ControlID="GridDevice" PropertyName="SelectedValue" DefaultValue="0" />  
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
                  DataKeyNames="ID_Device,Parent_ID"
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
                <div>
                    <div runat="server" id="DivUpdatePanel" visible="true" style="height:200px; width:auto; overflow:auto; float:left" >   
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
                    <div style="height:200px; width:auto; overflow:auto">
                            <asp:CheckBoxList ID="CheckBoxParent" runat="server" DataSourceID="CheckDeviceObjectDataSource"
                                DataTextField="NameDevice" DataValueField="Device">
                            </asp:CheckBoxList>
                    </div>
                      <div id="ImageDiv" runat="server" style="overflow-y:scroll; text-align:left;">
                      <div style="float:left"> 
                        <asp:GridView ID = "LWImage" runat="server" 
                                    AutoGenerateColumns="false"
                                    DataSourceID="ImageObjectDataSource" 
                                    DataKeyNames="ID,ID_files,fileName,fileType"
                                    OnSelectedIndexChanged="LWImage_SelectedIndexChanged">
                        <Columns>                
                            <asp:ButtonField HeaderText = "Ред."
                                                CommandName="Select" ButtonType="Image" 
                                    ImageUrl="~/Image/edit.png" FooterText="Проба">  
                                <ControlStyle Height="15px" Width="15px" />
                            </asp:ButtonField>
                            <asp:TemplateField  HeaderText = "Карта."> 
                                <ItemTemplate>
                                <asp:ImageMap ID="IMG" runat="server" 
                                    ImageUrl='<%#Eval("fileType", "~/Image_Data/"+Eval("fileName")+"_"+Eval("ID_files")+"."+Eval("fileType")) %>' 
                                    Height = "50"/>
                                </ItemTemplate> 
                            </asp:TemplateField>
                            <asp:ButtonField
                            CommandName="Delete" ButtonType="Image" 
                            ImageUrl="~/Image/deletion.png" HeaderText="Удалить"
                            >  
                               <ControlStyle Height="15px" Width="15px" />
                            </asp:ButtonField>
                        </Columns> 
                        </asp:GridView>
                      </div>
                      </div>
                      <div> 
                            Изображение
                            <asp:FileUpload ID="ImageFile" runat="server" />
                            <br />
                            <asp:Button ID="AddImge" Text="Добавить изображение" ToolTip="Добавляет изображение к существующей записи" 
                                    runat="server" Visible="false" OnClick="Image_OnInserted">
                            </asp:Button>
                            <br />
                            <br />
                            Привязать к месту на карте
                            <br />
                            <asp:Button ID="MapRelation" Text="Привязать" ToolTip="Привязать к месту на карте" 
                                        runat="server" Visible="true" onclick="MapRelation_Click" >
                            </asp:Button>
                      </div>
                </div>

            <asp:RadioButtonList ID="RadioButtonUnit" AppendDataBoundItems="true" runat="server"
                DataSourceID = "UnitObjectDataSource" DataTextField="NameUnit" DataValueField="ID_Unit" 
                RepeatDirection="Horizontal" Enabled="true">
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
            <asp:Label runat="server" id="aliona" />
            <asp:ModalPopupExtender ID="ModalPopupExtender1"
                    runat="server"  
                    PopupControlID="UpdatePanel"
                    TargetControlID="btnEditCustomer"
                    OkControlID="editBox_OK"
                    BackgroundCssClass = "modalBackground" 
                    PopupDragHandleControlID = "Редактирование записи" Drag="True"
                    DynamicServicePath=""
                    Enabled="true"/>
            <asp:ModalPopupExtender runat="server" ID = "ModalImageMaping"
                PopupControlID = "ImageMapingPanel"
                TargetControlID ="aliona"
                OkControlID = "CloseImageMapingPanel"
                BackgroundCssClass = "modalBackground" 
                PopupDragHandleControlID = "Ссылки на рисунке" Drag="True"/>

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
      <div style="display:inline" >
            <asp:ImageMap ID="MapPage" runat="server" Visible="false" Width="500" />
            <div id="DivRightPage" runat="server" style="float:right" >
            </div>
      </div>
    </asp:Content>
