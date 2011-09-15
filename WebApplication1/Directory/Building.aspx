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
<div style="display:inline">
<div style="float:left; width:300px"> 
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
        ID="ImageObjectDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataImage.ImageData" 
        InsertMethod="AddEmployee"
        SelectMethod ="TestGetSqlBytes">
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
              DataKeyNames="ID_Building"
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
                  BackColor="#D9F2FF" BorderStyle="Double">
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
                   <tr>
                        <td align="right" >
                             Изображение</td>
                         <td align="left">
                             <asp:FileUpload ID="ImageFile" runat="server" />
                        </td>
                    </tr>
                  </table>
                  <div id="ImageDiv" runat="server" style="overflow-x:scroll; text-align:left;" ></div>
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
        </tr>
      </table>
</div>
<div style="width:350px;float:left">
      Справочник Корпусов-блоков-строений является базовым. Его необходимо запонлнять одним из первых. 
      В дальнейшем в приложении он используется как справочник для удобства поискаи фильтрации информациии,
      для получения укрупненной статистики и построения отчетов.
</div> 
</div>
    </asp:Content>
