<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="Otdelen.aspx.cs" Inherits="WebApplication1.Directory.Otdelen" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script runat="server">


</script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
      <h3>Справочник отделений</h3> 
      <asp:Label id="Msg" runat="server" ForeColor="Red" />
      <asp:ObjectDataSource 
        ID="OtdelenObjectDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataOtdelen.OtdelenData" 
        SortParameterName="SortColumns"
        EnablePaging="true"
        SelectCountMethod="SelectCount"
        DeleteMethod="DeleteOtdelen"
        StartRowIndexParameterName="StartRecord"
        MaximumRowsParameterName="MaxRecords" 
        SelectMethod="GetAllOtdelen" 
        InsertMethod="InsertOtdelen" 
        UpdateMethod="UpdateOtdelen" 
        OnInserted="DetailsObjectDataSource_OnInserted"
        OnUpdated="DetailsObjectDataSource_OnUpdated"
        OnDeleted="DetailsObjectDataSource_OnDeleted" 
        >
        <InsertParameters>
            <asp:ControlParameter ControlID="TextBox2" Name="NameOtdelen" 
             PropertyName="Text" />
            <asp:controlparameter name="ID_Building" controlid="BuildingList" propertyname="SelectedValue" />
            <asp:ControlParameter ControlID="Floor" Name="Floor"  propertyName="Text" />
        </InsertParameters>
        <deleteparameters>
            <asp:controlparameter name="ID_Otdelen" controlid="OtdelenGridView" propertyname="SelectedValue" />
        </deleteparameters>
        <updateparameters>
            <asp:controlparameter name="ID_Otdelen" controlid="OtdelenGridView" propertyname="SelectedValue" />
            <asp:controlparameter name="ID_Building" controlid="BuildingList" propertyname="SelectedItem.value" />
            <asp:ControlParameter ControlID="TextBox2" Name="NameOtdelen"  
                PropertyName="Text" />
            <asp:ControlParameter ControlID="Floor" Name="Floor"     PropertyName="Text" />
        </updateparameters>
      </asp:ObjectDataSource>
      <asp:ObjectDataSource 
        ID="OtdelenObjectDataSourceOneRow" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataOtdelen.OtdelenData" 
        ConflictDetection="CompareAllValues"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetOtdelen" 
        InsertMethod="InsertOtdelen" 
        UpdateMethod="UpdateOtdelen" 
        DeleteMethod="DeleteOtdelen"
        OnInserted="DetailsObjectDataSource_OnInserted"
        OnUpdated="DetailsObjectDataSource_OnUpdated"
        OnDeleted="DetailsObjectDataSource_OnDeleted" >
        <SelectParameters>
            <asp:ControlParameter ControlID="OtdelenGridView" DefaultValue="" 
                Name="ID_Otdelen" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters> 
        <InsertParameters>
            <asp:ControlParameter ControlID="TextBox2" Name="NameOtdelen" 
             PropertyName="Text" />
            <asp:controlparameter name="ID_Building" controlid="BuildingList" propertyname="SelectedValue" />
            <asp:ControlParameter ControlID="Floor" Name="Floor"  propertyName="Text" />
        </InsertParameters>
        <deleteparameters>
            <asp:controlparameter name="ID_Otdelen" controlid="OtdelenGridView" propertyname="SelectedValue" />
        </deleteparameters>
        <updateparameters>
            <asp:controlparameter name="ID_Otdelen" controlid="OtdelenGridView" propertyname="SelectedValue" />
            <asp:controlparameter name="ID_Building" controlid="BuildingList" propertyname="SelectedItem.value" />
            <asp:ControlParameter ControlID="TextBox2" Name="NameOtdelen"  
                PropertyName="Text" />
            <asp:ControlParameter ControlID="Floor" Name="Floor"     PropertyName="Text" />
        </updateparameters>
      </asp:ObjectDataSource>
      <asp:ObjectDataSource 
        ID="BuildingObjectDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataBuilding.BuildingData" 
        SortParameterName="SortColumns"
        EnablePaging="true"
        SelectCountMethod="SelectCout"
        DeleteMethod="DeleteBuilding"
        StartRowIndexParameterName="StartRecord"
        MaximumRowsParameterName="MaxRecords" 
        SelectMethod="GetAllBuilding">
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
            <asp:Parameter Name="NameTable"  DefaultValue = "Otdelen" />
            <asp:ControlParameter Name= "ID_Table" ControlID="OtdelenGridView" PropertyName="SelectedValue" DefaultValue="0" />  
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
            <asp:Parameter Name="NameTable" DefaultValue="Building" />  
            <asp:ControlParameter Name= "ID_Table" ControlID="OtdelenGridView" PropertyName="SelectedValue" DefaultValue="0" />  
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

      <div style="float:left; width:450px">
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
                <asp:DropDownList ID="FiltrBuilding" runat="server" 
        DataSourceID="BuildingObjectDataSource"
        DataTextField="NameBuilding" DataValueField="ID_Building" />
                <asp:Button id="Button5" OnCommand="button_filtr" AutoPostBack="true" runat="server" text="Поиск"/>
                <br />
                <asp:CheckBox ID="FilterClear" runat="server" Text="Без фильтра" Checked="true"  />
                </td>
        </tr>
      </table>
      <table cellspacing="10">
        <tr>
          <td valign="top">
            <asp:GridView ID="OtdelenGridView" 
              DataSourceID="OtdelenObjectDataSource" 
              AutoGenerateColumns="False"
              AllowSorting="True"
              AllowPaging="True"
              PageSize="18"
              DataKeyNames="ID_Otdelen,ID_Building,NameBuilding,Floor"
              OnSelectedIndexChanged="GridView_OnSelectedIndexChanged"
              RunAt="server">
              <HeaderStyle backcolor="lightblue" forecolor="black"/>
              <Columns>                
                <asp:ButtonField CommandName="Select" ButtonType="Image" 
                      ImageUrl="~/Image/edit.png" HeaderText="Ред.">  
                  <ControlStyle Height="15px" Width="15px"  />
                </asp:ButtonField>
                <asp:BoundField DataField="ID_Otdelen" HeaderText="Номер п/п" 
                      SortExpression="ID_Otdelen" Visible="False" />
                <asp:BoundField 
                        DataField="NameOtdelen"
                        HeaderText="Наименование" 
                      SortExpression="NameOtdelen" />
                <asp:BoundField DataField="NameBuilding" 
                  HeaderText="Корпус строение" SortExpression="NameBuilding" />
                <asp:BoundField DataField="Floor" HeaderText="Этаж" SortExpression="Floor">
                  <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>
                <asp:ButtonField
                        CommandName="Delete" ButtonType="Image" 
                        ImageUrl="~/Image/deletion.png" HeaderText="Удалить"
                        >  
                    <ControlStyle Height="15px" Width="15px" />
                </asp:ButtonField>
              </Columns>                
            </asp:GridView>            
            <asp:Button ID="btnEditCustomer" Text = "Добавить" runat="server"/>
          </td>
        </tr>
        <tr>
          <td>
            <asp:Panel ID="UpdateOtdelenPanel" runat="server"  
                  BackColor="#D9F2FF" BorderStyle="Double">
                  <div style="display:inline" >
                    <div style="text-align:left; float:left">
                          <asp:Label ID="Label3" runat="server" Text="Редактирование оттделений"></asp:Label>
                    </div>
                    <div style="text-align:right">
                          <asp:ImageButton ID="editBox_OK" runat="server" ImageUrl= "~/Image/Close.ico" Width="20" Height = "20"  />
                    </div>
                  </div>
                  <table>
                    <tr>
                        <td align="right" >
                            <asp:Label ID="Label1" runat="server" Text="Наименование отделения"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="TextBox2"  runat="server" Width="160px"></asp:TextBox>
                        </td>
                   </tr>
                    <tr>
                        <td align="right" >
                            <asp:Label ID="Label2" runat="server" Text="Здание корпус"></asp:Label>
                         </td>
                         <td align="left">
                             <asp:DropDownList ID="BuildingList" runat="server" 
                              DataSourceID="BuildingObjectDataSource" 
                              DataTextField="NameBuilding" DataValueField="ID_Building" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" >
                            <asp:Label ID="Label4" runat="server" Text="Этаж"></asp:Label>
                         </td>
                         <td align="left">
                             <asp:TextBox ID="Floor"  runat="server" Width="160px"></asp:TextBox>
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

                  <p style="display:inline; float:right" >
                    <asp:Button ID="UpdateButton" runat="server" Text="Обновить" CommandName="Update" 
                            OnCommand="CommandBtn_Click" Visible="false"/>
                    <asp:Button ID="InsertButton" runat="server" Text="Добавить" CommandName="Insert" 
                            OnCommand="CommandBtn_Click"/>
                    <asp:Button ID="DeleteButton" runat="server" Text="Удалить" CommandName="Delete" 
                            OnCommand="CommandBtn_Click" Visible="false"/>
                  </p>
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
            <asp:DetailsView ID="UpdatePan" AutoGenerateRows="true" 
                  DataSourceID="OtdelenObjectDataSourceOneRow" runat="server" 
                  Visible="False" />
            <asp:Label runat="server" id="aliona" />
            <asp:ModalPopupExtender ID="ModalPopupExtender1"
                    runat="server"  
                    PopupControlID="UpdateOtdelenPanel"
                    TargetControlID = "btnEditCustomer"  
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
            <asp:DragPanelExtender ID="UpdateOtdelenPanel_DragPanelExtender" runat="server" 
                  DragHandleID="UpdateOtdelenPanel" Enabled="True" 
                  TargetControlID="UpdateOtdelenPanel">
              </asp:DragPanelExtender>
          </td>
        </tr>
      </table>
      </div>
      <div>
              Справочник отделений. Его необходимо запонлнять одним из первых. 
              В дальнейшем в приложении он используется как справочник для удобства поискаи фильтрации информациии,
              для получения укрупненной статистики и построения отчетов.
      </div>
      <br />
      <div style="display:inline" >
            <asp:ImageMap ID="MapPage" runat="server" Visible="false" Width="500" />
            <div id="DivRightPage" runat="server" style="float:right" >
            </div>
      </div>
</asp:Content>
