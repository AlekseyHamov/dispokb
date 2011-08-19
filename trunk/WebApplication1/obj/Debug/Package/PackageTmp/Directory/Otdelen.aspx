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
              DataKeyNames="ID_Otdelen"
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
                  <table>
                    <tr >
                        <td align="left" >
                          <asp:Label ID="Label3" runat="server" Text="Редактирование оттделений"></asp:Label>
                        </td> 
                        <td align="right">
                          <asp:ImageButton ID="editBox_OK" runat="server" ImageUrl= "~/Image/Close.ico" Width="20" Height = "20"  />
                        </td>
                    </tr>
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
                  <p style="display:inline; float:right" >
                    <asp:Button ID="UpdateButton" runat="server" Text="Обновить" CommandName="Update" 
                            OnCommand="CommandBtn_Click" Visible="false"/>
                    <asp:Button ID="InsertButton" runat="server" Text="Добавить" CommandName="Insert" 
                            OnCommand="CommandBtn_Click"/>
                    <asp:Button ID="DeleteButton" runat="server" Text="Удалить" CommandName="Delete" 
                            OnCommand="CommandBtn_Click" Visible="false"/>
                  </p>
             </asp:Panel>
            <asp:DetailsView ID="UpdatePan" AutoGenerateRows="true" 
                  DataSourceID="OtdelenObjectDataSourceOneRow" runat="server" 
                  Visible="False" />
            <asp:ModalPopupExtender ID="ModalPopupExtender1"
                    runat="server"  
                    PopupControlID="UpdateOtdelenPanel"
                    TargetControlID = "btnEditCustomer"  
                    OkControlID="editBox_OK"
                    BackgroundCssClass = "modalBackground" 
                    PopupDragHandleControlID = "Редактирование записи" Drag="True"
                    />
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
</asp:Content>
