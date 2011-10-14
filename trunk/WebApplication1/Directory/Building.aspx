<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="Building.aspx.cs" Inherits="WebApplication1.Directory.Building" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script runat="server">
</script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
      <h3>Справочник Корпусов-блоков-строений</h3> 
      <asp:Label id="Msg" runat="server" ForeColor="Red" />
<div style="display:inline" >
<div style="width:30%; float:left"  > 
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
        UpdateMethod="UpdateBuilding" 
        OnInserted="BuildingDataSource_OnInserted"
        OnUpdated="BuildingDataSource_OnUpdated"
        OnDeleted="BuildingDataSource_OnDeleted" >
        <InsertParameters>
              <asp:ControlParameter ControlID="TextBox2" Name="NameBuilding" 
                  PropertyName="Text" />
          </InsertParameters>
        <deleteparameters>
            <asp:controlparameter name="ID_Building" controlid="BuildingGridView" propertyname="SelectedValue" />
        </deleteparameters>
        <updateparameters>
            <asp:controlparameter name="ID_Building" controlid="BuildingGridView" propertyname="SelectedValue" />
            <asp:ControlParameter ControlID="TextBox2" Name="NameBuilding" 
                PropertyName="Text" />
        </updateparameters>
      </asp:ObjectDataSource>
      <asp:ObjectDataSource 
        ID="ObjectDataTempGrig" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataBuilding.BuildingData" 
        SelectMethod="GetBuildingTempGrid" >
        <SelectParameters>
            <asp:ControlParameter Name= "ID_Building" ControlID="BuildingGridView" PropertyName="SelectedValue" DefaultValue="0" />  
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
            <asp:Parameter Name="NameTable"  DefaultValue = "Building" />
            <asp:ControlParameter Name= "ID_Table" ControlID="BuildingGridView" PropertyName="SelectedValue" DefaultValue="0" />  
        </SelectParameters> 
        <DeleteParameters>
            <asp:ControlParameter Name= "ID_files" ControlID="LWImage" PropertyName="SelectedValue" DefaultValue="0" />  
        </DeleteParameters> 
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
      <table cellspacing="10">
        <tr>
          <td valign="top">
            <asp:GridView ID="BuildingGridView" 
              DataSourceID="BuildingObjectDataSource" 
              AutoGenerateColumns="False"
              AllowSorting="True"
              AllowPaging="True"
              PageSize="18"
              DataKeyNames="ID_Building,MapMain"
              OnSelectedIndexChanged="GridView_OnSelectedIndexChanged"
              RunAt="server" >  
              <HeaderStyle backcolor="lightblue" forecolor="black"/>
              <Columns>                
                <asp:ButtonField HeaderText = "Ред."
                                 CommandName="Select" ButtonType="Image" 
                      ImageUrl="~/Image/edit.png" FooterText="Проба">  
                  <ControlStyle Height="15px" Width="15px" />
                </asp:ButtonField>
                <asp:BoundField DataField="ID_Building" HeaderText="Номер п/п" 
                      SortExpression="ID_Building" Visible="False" />
                <asp:BoundField 
                        DataField="NameBuilding"
                        HeaderText="Наименование" 
                      SortExpression="NameBuilding" />
                <asp:ButtonField
                        CommandName="Delete" ButtonType="Image" 
                        ImageUrl="~/Image/deletion.png" HeaderText="Удалить"
                        >  
                    <ControlStyle Height="15px" Width="15px" />
                </asp:ButtonField>
              </Columns>                
            </asp:GridView>    
          </td>
        </tr>
        <tr>
          <td>
           <asp:Button ID="btnEditCustomer" runat="server" Text="Добавить" />
          </td>
        </tr>
        <tr>
          <td>
                  <asp:Panel ID="UpdatePanel" runat="server"  
                  BackColor="#ffffff">
                  <table>
                    <tr >
                        <td align="left" >
                          <asp:Label ID="Label3" runat="server" Text="Редактирование корпусов блоков"></asp:Label>
                        </td> 
                        <td align="right">
                          <asp:ImageButton ID="editBox_OK" runat="server" ImageUrl= "~/Image/Close.ico" Width="20" Height = "20"  />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" >
                            <asp:Label ID="Label1" runat="server" Text="Наименование корпуса"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="TextBox2"  runat="server" Width="160px"></asp:TextBox>
                        </td>
                   </tr>
                  </table>
                  <div id="ImageDiv" runat="server" style="overflow-y:scroll; text-align:left; height:150px;">
                      <div style="float:left"> 
                        <asp:GridView ID = "LWImage" runat="server" 
                                    AutoGenerateColumns="false"
                                    DataSourceID="ImageObjectDataSource" 
                                    DataKeyNames="ID,ID_files,fileName,fileType"
                                    OnSelectedIndexChanged="LWImage_SelectedIndexChanged" Height="144px"
                        >
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
                  <asp:Label ID="Label4" runat="server" Text="100"></asp:Label>
                  <asp:Button ID="Plus" runat="server" OnCommand = "Plus_Minus_Click" CommandName = "Plus" Text="Plus" />
                  <asp:Button ID="Minus" runat="server" OnCommand = "Plus_Minus_Click" CommandName = "Minus" Text="Minus" />  
                  <p style="display:inline; float:right">
                          <asp:Button ID="UpdateButton" runat="server" Text="Обновить" CommandName="Update" 
                                 OnCommand="CommandBtn_Click" Visible="false"/>
                          <asp:Button ID="InsertButton" runat="server" Text="Добавить" CommandName="Insert" 
                                 OnCommand="CommandBtn_Click" />
                          <asp:Button ID="DeleteButton" runat="server" Text="Удалить" CommandName="Delete" 
                                 OnCommand="CommandBtn_Click" Visible="false"/>
                  </p>
                  </asp:Panel>
            <asp:Panel runat="server" ID="ImageMapingPanel" BackColor="#ffffff">
                <div style="text-align:right" >
                    <asp:ImageButton runat="server" id="CloseImageMapingPanel" ImageUrl="~/Image/Close.ico"   Height="15px" Width="15px"/>
                </div>
                <div>
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
                    />
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
        </tr>
      </table>
</div>
<div style="width:100%">
      Справочник Корпусов-блоков-строений является базовым. Его необходимо запонлнять одним из первых. 
      В дальнейшем в приложении он используется как справочник для удобства поискаи фильтрации информациии,
      для получения укрупненной статистики и построения отчетов.
</div>
<br />
<div style="display:inline" >
    <asp:ImageMap ID="MapPage" runat="server" Visible="false" Width="500" />
    <div id="DivRightPage" runat="server" style="float:right" >
    </div>
</div>
</div>
    </asp:Content>
