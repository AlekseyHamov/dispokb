<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="Person.aspx.cs" Inherits="WebApplication1.Directory.Person" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script runat="server">
</script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
      <h3>Справочник Сотрудников</h3> 
      <asp:Label id="Msg" runat="server" ForeColor="Red" />
      <asp:ObjectDataSource 
        ID="PersonObjectDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataPerson.PersonData" 
        SortParameterName="SortColumns"
        EnablePaging="true"
        SelectCountMethod="SelectCount"
        DeleteMethod="DeleteRecord"
        StartRowIndexParameterName="StartRecord"
        MaximumRowsParameterName="MaxRecords" 
        SelectMethod="GetAll" 
        InsertMethod="InsertRecord" 
        UpdateMethod="UpdateRecord" 
        OnInserted="DetailsObjectDataSource_OnInserted"
        OnUpdated="DetailsObjectDataSource_OnUpdated"
        OnDeleted="DetailsObjectDataSource_OnDeleted" >
        <InsertParameters>
            <asp:ControlParameter ControlID="TextBox2" Name="NamePerson" 
             PropertyName="Text" />
            <asp:ControlParameter ControlID="RadioButtonUnit" Name="ID_Unit" 
             PropertyName="SelectedValue" />
        </InsertParameters>
        <deleteparameters>
            <asp:controlparameter name="ID_Person" controlid="GridView" propertyname="SelectedValue" />
        </deleteparameters>
        <updateparameters>
            <asp:controlparameter name="ID_Person" controlid="GridView" propertyname="SelectedValue" />
            <asp:ControlParameter ControlID="TextBox2" Name="NamePerson"  
                PropertyName="Text" />
            <asp:ControlParameter ControlID="RadioButtonUnit" Name="ID_Unit" 
             PropertyName="SelectedValue" />
        </updateparameters>
      </asp:ObjectDataSource>
      <asp:ObjectDataSource 
        ID="PersonObjectDataSourceOneRow" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataPerson.PersonData" 
        ConflictDetection="CompareAllValues"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetOneRecord" 
        InsertMethod="InsertRecord" 
        UpdateMethod="UpdateRecord" 
        DeleteMethod="DeleteRecord"
        OnInserted="DetailsObjectDataSource_OnInserted"
        OnUpdated="DetailsObjectDataSource_OnUpdated"
        OnDeleted="DetailsObjectDataSource_OnDeleted" >
        <SelectParameters>
            <asp:ControlParameter ControlID="GridView" DefaultValue="" 
                Name="ID_Person" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters> 
        <InsertParameters>
            <asp:ControlParameter ControlID="TextBox2" Name="NamePerson" 
             PropertyName="Text" />
            <asp:ControlParameter ControlID="RadioButtonUnit" Name="ID_Unit" 
             PropertyName="SelectedValue" />
        </InsertParameters>
        <deleteparameters>
            <asp:controlparameter name="ID_Person" controlid="GridView" propertyname="SelectedValue" />
        </deleteparameters>
        <updateparameters>
            <asp:controlparameter name="ID_Person" controlid="GridView" propertyname="SelectedValue" />
            <asp:ControlParameter ControlID="TextBox2" Name="NamePerson"  
                PropertyName="Text" />
            <asp:ControlParameter ControlID="RadioButtonUnit" Name="ID_Unit" 
             PropertyName="SelectedValue" />
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
        SelectMethod="GetAll" >
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
            <asp:Parameter Name="NameTable"  DefaultValue = "Person" />
            <asp:ControlParameter Name= "ID_Table" ControlID="GridView" PropertyName="SelectedValue" DefaultValue="0" />  
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
            <asp:Parameter Name= "NameTable" DefaultValue="Person" />  
            <asp:ControlParameter Name= "ID_Table" ControlID="GridView" PropertyName="SelectedValue" DefaultValue="0" />  
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

      <div style="float:left; width:400px">
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
                <asp:RadioButtonList ID="FiltrRadioUnit" runat="server" 
                    DataSourceID="UnitObjectDataSource" 
                    DataTextField="NameUnit" DataValueField="ID_Unit" RepeatDirection="Horizontal"
                    >
                </asp:RadioButtonList>
                <BR>
                <asp:Button runat="server" id="FiltrButton" Text="Поиск" OnCommand="button_filtr" />
                <BR>
                <asp:CheckBox ID="FilterClear" runat="server" AutoPostBack="true" Text="Без фильтра" OnCheckedChanged="button_filtr" />
            </td>
        </tr>
      </table>
      <table cellspacing="10">
        <tr>
          <td valign="top">
            <asp:GridView ID="GridView" 
              DataSourceID="PersonObjectDataSource" 
              AutoGenerateColumns="False"
              AllowSorting="True"
              AllowPaging="True"
              PageSize="18"
              DataKeyNames="ID_Person"
              OnSelectedIndexChanged="GridView_OnSelectedIndexChanged"
              RunAt="server">
              <HeaderStyle backcolor="lightblue" forecolor="black"/>
              <Columns>                
                <asp:ButtonField
                                 CommandName="Select" ButtonType="Image" 
                      ImageUrl="~/Image/edit.png" HeaderText="Ред.">  
                  <ControlStyle Height="15px" Width="15px" />

                </asp:ButtonField>
                <asp:BoundField DataField="ID_Person" HeaderText="Номер п/п" 
                      SortExpression="ID_Person" Visible="False" />
                <asp:BoundField 
                        DataField="NamePerson"
                        HeaderText="Наименование" 
                      SortExpression="NamePerson" />
                <asp:BoundField 
                        DataField="NameUnit"
                        HeaderText="Подраз." 
                      SortExpression="NameUnit" />
                <asp:ButtonField
                        CommandName="Delete" ButtonType="Image" 
                        ImageUrl="~/Image/deletion.png" HeaderText="Удалить"
                        >  
                    <ControlStyle Height="15px" Width="15px" />
                </asp:ButtonField>
              </Columns>                
            </asp:GridView>            
            <asp:Button ID="btnEditCustomer" Text = "добавить" runat="server" />
          </td>
        </tr>
        <tr>
          <td>
             <asp:Panel ID="UpdateOtdelenPanel" runat="server"  
                  BackColor="#D9F2FF" BorderStyle="Double">
                  <table>
                    <tr >
                        <td align="left" >
                          <asp:Label ID="Label3" runat="server" Text="Редактирование данных сотрудника"></asp:Label>
                        </td> 
                        <td align="right">
                          <asp:ImageButton ID="editBox_OK" runat="server" ImageUrl= "~/Image/Close.ico" Width="20" Height = "20" ImageAlign="Left"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" >
                            <asp:Label ID="Label1" runat="server" Text="ФИО сотрудника"></asp:Label>
                            <asp:TextBox ID="TextBox2"  runat="server"></asp:TextBox>
                        </td>
                        <td >
                            
                        </td>
                   </tr>
                    <tr>
                 <td align="right" style="text-align: left" >
                       <asp:Label ID="LableUnit" Text="Подразделение" runat="server" 
                           style="text-align: left"></asp:Label>   
                 </td>
                  <td align="left" >
                        &nbsp;</td>
                 </tr>
                    <tr>
                        <td align="right" style="text-align: left" >
                            <asp:RadioButtonList ID="RadioButtonUnit" runat="server" 
                                DataSourceID="UnitObjectDataSource" 
                                DataTextField="NameUnit" DataValueField="ID_Unit"
                                >
                            </asp:RadioButtonList>
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
                            <asp:Label ID="D" runat="server" Visible="false" Text="Привязать к месту на карте" ></asp:Label>
                            <br />
                            <asp:Button ID="MapRelation" Text="Привязать" ToolTip="Привязать к месту на карте" 
                                        runat="server" Visible="false" onclick="MapRelation_Click" >
                            </asp:Button>

                      </div>
                  </div>
                    <asp:Button ID="UpdateButton" runat="server" Text="Обновить" CommandName="Update" 
                            OnCommand="CommandBtn_Click" Visible="false"/>
                    <asp:Button ID="InsertButton" runat="server" Text="Добавить" CommandName="Insert" 
                            OnCommand="CommandBtn_Click"/>
                    <asp:Button ID="DeleteButton" runat="server" Text="Удалить" CommandName="Delete" 
                            OnCommand="CommandBtn_Click" Visible="false"/>
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

             <asp:DetailsView ID="UpdatePan" AutoGenerateRows="true" DataSourceID="PersonObjectDataSourceOneRow"  Visible="false" runat="server" /> 
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
              Справочник Сотрудников. Его необходимо запонлнять одним из первых. На странице доступна возможность фильтрации 
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
