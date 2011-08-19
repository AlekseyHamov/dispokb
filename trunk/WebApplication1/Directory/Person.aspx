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
                    <asp:Button ID="UpdateButton" runat="server" Text="Обновить" CommandName="Update" 
                            OnCommand="CommandBtn_Click" Visible="false"/>
                    <asp:Button ID="InsertButton" runat="server" Text="Добавить" CommandName="Insert" 
                            OnCommand="CommandBtn_Click"/>
                    <asp:Button ID="DeleteButton" runat="server" Text="Удалить" CommandName="Delete" 
                            OnCommand="CommandBtn_Click" Visible="false"/>
             </asp:Panel>
             <asp:DetailsView ID="UpdatePan" AutoGenerateRows="true" DataSourceID="PersonObjectDataSourceOneRow"  Visible="false" runat="server" /> 
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
              Справочник Сотрудников. Его необходимо запонлнять одним из первых. На странице доступна возможность фильтрации 
              по подразделениям. В справочнике необходимо выполнять привязку к подразделению.
              В дальнейшем справочник используется для удобства поискаи фильтрации информациии,
              для получения укрупненной статистики и построения отчетов.
      </div>
</asp:Content>
