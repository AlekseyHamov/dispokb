<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Culture="auto" UICulture="auto"  
    CodeBehind="claim.aspx.cs" Inherits="WebApplication1.Directory.Claim" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script runat="server">
</script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
      <h3>Справочник Заявок</h3> 
      <asp:Label id="Msg" runat="server" ForeColor="Red" />
      <p >  
      <asp:ObjectDataSource 
        ID="ClaimObjectDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataClaim.ClaimData" 
        SortParameterName="SortColumns"
        EnablePaging="true"
        SelectCountMethod="SelectCount"
        StartRowIndexParameterName="StartRecord"
        MaximumRowsParameterName="MaxRecords" 
        DeleteMethod="DeleteRecord"
        SelectMethod="GetAll" 
        InsertMethod="InsertRecord" 
        UpdateMethod="UpdateRecord" 
        OnInserted="DetailsObjectDataSource_OnInserted"
        OnUpdated="DetailsObjectDataSource_OnUpdated"
        OnDeleted="DetailsObjectDataSource_OnDeleted">
        <InsertParameters>
            <asp:ControlParameter ControlID="TextBox2" Name="Note"  
                PropertyName="Text" />
            <asp:ControlParameter ControlID="DateClaimBox" Name="DateClaim"  
                PropertyName="Text" />
            <asp:ControlParameter ControlID="DropDownListBuildingUpdate" Name="ID_Building"  
                PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="DropDownListOtdelenUpdate" Name="ID_Otdelen"  
                PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="DropDownListRoomUpdate" Name="ID_Room"  
                PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="DropDownListPersonUpdate" Name="ID_Person"  
                PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="StatusRadioButtonList" Name="Status"  
                PropertyName="SelectedValue" />
        </InsertParameters>
        <updateparameters>
            <asp:controlparameter name="ID_Claim" controlid="GridView" propertyname="SelectedValue" />
            <asp:ControlParameter ControlID="TextBox2" Name="Note"  
                PropertyName="Text" />
            <asp:ControlParameter ControlID="DateClaimBox" Name="DateClaim"  
                PropertyName="Text" />
            <asp:ControlParameter ControlID="DropDownListBuildingUpdate" Name="ID_Building"  
                PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="DropDownListOtdelenUpdate" Name="ID_Otdelen"  
                PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="DropDownListRoomUpdate" Name="ID_Room"  
                PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="DropDownListPersonUpdate" Name="ID_Person"  
                PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="StatusRadioButtonList" Name="Status"  
                PropertyName="SelectedValue" />
        </updateparameters>
        <deleteparameters>
            <asp:controlparameter name="ID_Claim" controlid="GridView" propertyname="SelectedValue" />
        </deleteparameters>
        <SelectParameters>
            <asp:controlparameter name="DateBegin" controlid="DateBegin" propertyname="Text" DbType="DateTime" />
            <asp:controlparameter name="DateEnd" controlid="DateEnd" propertyname="Text" DbType="DateTime"/>
        </SelectParameters> 
      </asp:ObjectDataSource>
      <asp:ObjectDataSource 
        ID="ClaimObjectDataSourceOneRow" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataClaim.ClaimData"
        SelectMethod="GetOneRecord" 
        >
        <SelectParameters>
            <asp:controlparameter name="ID_Claim" controlid="GridView" propertyname="SelectedValue" />
        </SelectParameters>
      </asp:ObjectDataSource>
      <asp:ObjectDataSource 
        ID="BuildingObjectDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataBuilding.BuildingData" 
        SortParameterName="SortColumns"
        EnablePaging="true"
        SelectCountMethod="SelectCout"
        StartRowIndexParameterName="StartRecord"
        MaximumRowsParameterName="MaxRecords" 
        SelectMethod="GetAllBuilding">
      </asp:ObjectDataSource>
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
        SelectMethod="GetAllOtdelen">
      </asp:ObjectDataSource>
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
        SelectMethod="GetAllNameNum">
      </asp:ObjectDataSource>
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
        SelectMethod="GetAll">
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
        SelectMethod="GetAll">
      </asp:ObjectDataSource>
      <asp:ObjectDataSource 
        ID="RoomDeviceListDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataRoomDeviceList.RoomDeviceListData" 
        InsertMethod="InsertRecord"
        DeleteMethod="DeleteRecord"
        SelectMethod="GetOneRecordTest" >
        <SelectParameters>
              <asp:ControlParameter ControlID="DropDownListRoomUpdate" Name="ID_Room"
                  PropertyName="SelectedValue" />
        </SelectParameters>
        <DeleteParameters>
              <asp:ControlParameter ControlID="CheckBoxDevice" Name="ID_Device"
                  PropertyName="SelectedValue" />
              <asp:ControlParameter ControlID="RoomGridView" Name="ID_Room"
                  PropertyName="SelectedValue" />
        </DeleteParameters>  
      </asp:ObjectDataSource>
      <asp:ObjectDataSource 
        ID="SparesObjectDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataSpares.SparesData" 
        SortParameterName="SortColumns"
        EnablePaging="true"
        SelectCountMethod="SelectCount"
        StartRowIndexParameterName="StartRecord"
        MaximumRowsParameterName="MaxRecords"
        SelectMethod="GetAll"
        FilterExpression="ID_Device in ({0})">
        <FilterParameters>
            <asp:ControlParameter ControlID="CheckBoxDevice" Name="ID_Device" PropertyName="Text" DefaultValue="0"/>
        </FilterParameters>  
      </asp:ObjectDataSource>
      <asp:ObjectDataSource
          ID="ClaimSparesListDataSource"
          runat="server"
          TypeName="Samples.AspNet.ObjectDataClaimSparesList.ClaimSparesListData"
          SelectMethod="GetOneClaimRecord"
          >
          <SelectParameters>
            <asp:ControlParameter ControlID="GridView" Name="ID_Claim" PropertyName="SelectedValue" DefaultValue="0"/>
          </SelectParameters>
      </asp:ObjectDataSource> 
      </p>

      <p>Фильтр
      <asp:ImageButton OnCommand="button_filter"   runat="server" alt="" ImageUrl="../Image/Downarrow.png"  style = " width :10px; height :10px;"
         />
      <asp:ImageButton  OnCommand="button_filter_Close"  runat="server" alt="" ImageUrl="../Image/Uparrow.png" style = " width :10px; height :10px;" 
       />
       
       <asp:TextBox ID="DateBegin" runat="server" Width="130px" MaxLength="1" 
       Style="text-align: justify"  ValidationGroup="MKE" > </asp:TextBox>
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/image/calendar.png"  CausesValidation="False" />
        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
              TargetControlID="DateBegin"  Mask="9999-99-99 99:99:99" 
        MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" CultureName="ru-RU" 
        OnInvalidCssClass="MaskedEditError" DisplayMoney="None" AcceptNegative="None"
        ErrorTooltipEnabled="True" ClearMaskOnLostFocus="false" />
        <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
        ControlToValidate="DateBegin" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
        Display="Static" TooltipMessage="Заполнить начальную дату" EmptyValueBlurredText="Empty" ValidationGroup="MKE" />
        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="DateBegin"
        PopupButtonID="ImageButton1" Format="yyyy.MM.dd. HH:mm:ss" />    

       <asp:TextBox ID="DateEnd" runat="server" Width="130px" MaxLength="1" 
       Style="text-align: justify"  ValidationGroup="MKE"> </asp:TextBox>
        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/image/calendar.png"  CausesValidation="False" />
        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
              TargetControlID="DateEnd" Mask="9999-99-99 99:99:99" 
        MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" CultureName="ru-RU"
        OnInvalidCssClass="MaskedEditError" DisplayMoney="None" AcceptNegative="None"
        ErrorTooltipEnabled="True" ClearMaskOnLostFocus="false" />
        <asp:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender1"
        ControlToValidate="DateEnd" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
        Display="Static" TooltipMessage="Заполнить конечную дату" EmptyValueBlurredText="Empty" ValidationGroup="MKE" />
        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="DateEnd"
        PopupButtonID="ImageButton2" Format="yyyy.MM.dd. HH:mm:ss" />    
      </p>
      <asp:Table id = "FiltrTable" runat="server" Visible="false" >
        <asp:tablerow>
            <asp:TableCell VerticalAlign="Top">
                <asp:CheckBox ID="CheckBoxBuilding"  OnCheckedChanged="filter_building" AutoPostBack="true" Text="Здан." runat="server" TextAlign="Right"/>
                <br />
                <asp:CheckBox ID="CheckBoxOtdelen" OnCheckedChanged="filter_otdelen" AutoPostBack="true" Text="Отдел." runat="server" TextAlign="Right"  />
                <br />
                <asp:CheckBox ID="CheckBoxRoom" OnCheckedChanged="filter_room" AutoPostBack="true" Text="Ком." runat="server" TextAlign="Right" />
            </asp:TableCell>
            <asp:TableCell VerticalAlign="Top">
                    <asp:DropDownList ID="BuildingList" runat="server" 
                        OnSelectedIndexChanged="filter_building"  AutoPostBack="true" >
                    </asp:DropDownList>
                    <br />
                    <asp:DropDownList ID="OtdelenList" runat="server" 
                        OnSelectedIndexChanged="filter_otdelen"  AutoPostBack="true" >
                    </asp:DropDownList>
                    <br />
                    <asp:DropDownList ID="RoomList" runat="server" 
                        OnSelectedIndexChanged="filter_room"  AutoPostBack="true" >
                    </asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell>
                <asp:RadioButtonList ID="RadioButtonUnit" runat="server"
                OnSelectedIndexChanged="filter_unit" AutoPostBack="true">
                        </asp:RadioButtonList>
            </asp:TableCell>
            <asp:TableCell VerticalAlign="Top" >
               <asp:CheckBox ID="CheckBoxPerson" Checked ="false" runat="server" />
               <asp:DropDownList ID="PersonList" runat="server" 
                        OnSelectedIndexChanged="filter_person" AutoPostBack="true">
                    </asp:DropDownList>
             </asp:TableCell>
        </asp:tablerow>
        <asp:tablerow>
            <asp:TableCell>
                <asp:CheckBox ID="AllFiltr" OnCheckedChanged="button_filter_Close" AutoPostBack="true" Checked="False" Text="Без фильтра" runat="server" TextAlign="Left" />
            </asp:TableCell>
            <asp:TableCell HorizontalAlign="center" >
                <asp:Button ID="Button4" runat="server" text="Поиск" OnCommand="button_filter"/>
            </asp:TableCell>
        </asp:tablerow>
      </asp:Table>

      <asp:GridView ID="GridView" 
              DataSourceID="ClaimObjectDataSource" 
              AutoGenerateColumns="False"
              AllowSorting="True"
              AllowPaging="True"
              PageSize="9"
              DataKeyNames="ID_Claim"
              OnSelectedIndexChanged="GridView_OnSelectedIndexChanged"
              RunAt="server" ToolTip="Список заявок">
              <HeaderStyle backcolor="lightblue" forecolor="black" />
              <Columns>                
                <asp:ButtonField 
                      CommandName="Select" ButtonType="Image" 
                      ImageUrl="~/Image/edit.png" HeaderText="Ред.">  
                  <ControlStyle Height="15px" Width="15px"/>
                </asp:ButtonField>
                <asp:BoundField DataField="ID_Claim" HeaderText="Номер п/п" 
                SortExpression="ID_Claim" Visible="False"/>
                <asp:BoundField 
                        DataField="Note"
                        HeaderText="Описание" 
                      SortExpression="Note"/>
                <asp:BoundField 
                        DataField="DateClaim"
                        HeaderText="Дата регистрации заявки" 
                      SortExpression="DateClaim" />
                <asp:BoundField 
                        DataField="NameOtdelen"
                        HeaderText="Отделение" 
                       SortExpression="NameOtdelen"/>
                <asp:BoundField 
                        DataField="NameRoom"
                        HeaderText="Комната" 
                       SortExpression="NameRoom"/>
                <asp:BoundField 
                        DataField="NameBuilding"
                        HeaderText="Здание корпус" 
                       SortExpression="NameBuilding"/>
                <asp:BoundField 
                        DataField="NamePerson"
                        HeaderText="Сотрудник"
                       SortExpression="NamePerson"
                       />
                <asp:ButtonField
                        CommandName="Delete" ButtonType="Image" 
                        ImageUrl="~/Image/deletion.png" HeaderText="Удалить"
                        >  
                    <ControlStyle Height="15px" Width="15px" />
                </asp:ButtonField>
            </Columns>                
            </asp:GridView>
      <asp:DetailsView ID="ClaimOneRow" runat="server" DataSourceID="ClaimObjectDataSourceOneRow" AutoGenerateRows="true" Visible="false">
      </asp:DetailsView>                
      <asp:Button ID="btnEditCustomer" Text = "добавить" runat="server" />
      <asp:Panel ID="UpdatePanel" runat="server"  
                  BackColor="#D9F2FF" BorderStyle="Double">
          <asp:table ID="TablePanel"  runat="server" GridLines="Both">
            <asp:tablerow>
                <asp:tablecell HorizontalAlign="left" >
                    <asp:Label ID="Label3" runat="server" Text="Редактирование заявки"></asp:Label>
                </asp:tablecell> 
                <asp:tablecell HorizontalAlign="right">
                    <asp:ImageButton ID="editBox_OK" runat="server" ImageUrl= "~/Image/Close.ico" Width="20" Height = "20"  />
                </asp:tablecell>
            </asp:tablerow>
            <asp:tablerow >
                <asp:tablecell HorizontalAlign="left" >
                    <asp:Label ID="Label1" runat="server" Text="Дата и время поступления заявки:"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:TextBox ID="DateClaimBox" runat="server" 
                    ToolTip="Время поступления заявки.Автоматически установлено текущее время">
                    </asp:TextBox>
                    &nbsp;
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                        TargetControlID="DateClaimBox" FirstDayOfWeek="Default" 
                        Format="dd.MM.yyyy" TodaysDateFormat="dd.MM.yyyy" 
                        PopupButtonID="CalendarButton" > 
                    </asp:CalendarExtender>
                    <asp:ImageButton ID="CalendarButton"  ImageUrl="~/Image/calendar.png" runat="server" />
                    &nbsp;
                    <asp:Label ID="TimeBox"   runat="server"></asp:Label>
                </asp:tablecell>
                <asp:tablecell>
                </asp:tablecell>
            </asp:tablerow>
            <asp:tablerow >
                <asp:tablecell>
                    <asp:DropDownList ID="DropDownListBuildingUpdate" runat="server" 
                        DataSourceID="BuildingObjectDataSource" DataTextField="NameBuilding" 
                        DataValueField="Id_Building" OnSelectedIndexChanged="Update_filter_Building" AutoPostBack="true">
                    </asp:DropDownList>           
                    <br />
                    <asp:DropDownList ID="DropDownListOtdelenUpdate" runat="server" 
                        DataSourceID="OtdelenObjectDataSource" DataTextField="NameOtdelen" 
                        DataValueField="Id_Otdelen" OnSelectedIndexChanged="Update_filter_Otdelen" AutoPostBack="true">
                    </asp:DropDownList>
                    <br />
                    <asp:DropDownList ID="DropDownListRoomUpdate" runat="server" 
                        DataSourceID="RoomObjectDataSource" DataTextField="NameRoom"  
                        DataValueField="ID_Room" OnSelectedIndexChanged="Update_filter_Room" AutoPostBack="true">
                    </asp:DropDownList>
                    &nbsp;&nbsp; 
                    <asp:RadioButtonList ID="RadioButtonUnitUpdate" runat="server" DataSourceID = "UnitObjectDataSource" 
                    DataValueField="ID_Unit"  DataTextField="NameUnit" RepeatDirection="Horizontal" 
                    OnSelectedIndexChanged="Update_filter_Person" AutoPostBack="true"  >
                        </asp:RadioButtonList>
                    <asp:DropDownList ID="DropDownListPersonUpdate" runat="server" 
                        DataSourceID="PersonObjectDataSource" DataTextField="NamePerson" 
                        DataValueField="ID_Person">
                    </asp:DropDownList>
               </asp:tablecell>
            </asp:tablerow>
            <asp:tablerow>
               <asp:tablecell>
               <table>
               <tr >
               <td>
               <div id="Div0" runat="server" style="height:150px; overflow:auto">
                    <asp:CheckBoxList ID="CheckBoxDevice" runat="server" DataSourceID="RoomDeviceListDataSource"
                        DataTextField="NameDevice" DataValueField="ID_Device" OnSelectedIndexChanged="Filter_device"
                        AutoPostBack="true">
                    </asp:CheckBoxList>
                </div>
               </td>
               <td>
               <div id="Div1" runat="server" style="height:150px; overflow:auto">     
                    <asp:CheckBoxList ID="CheckBoxSpares" runat="server" DataSourceID="SparesObjectDataSource"
                        DataTextField="NameSpares" DataValueField="ID_Spares">
                    </asp:CheckBoxList>
                </div> 
                </td>
                </tr>
                </table>
               </asp:tablecell>
            </asp:tablerow>
            <asp:tablerow >
                <asp:tablecell>
                    <asp:TextBox ID="TextBox2" runat="server" Height="75px" Width="484px" 
                        TextMode="MultiLine"></asp:TextBox>
                </asp:tablecell>
            </asp:tablerow>
            <asp:tablerow  HorizontalAlign="right"  >
                <asp:tablecell>
                <table>
                <tr>
                <td align="left">
                <asp:RadioButtonList ID="StatusRadioButtonList" runat="server" RepeatDirection="Horizontal"
                OnSelectedIndexChanged="CommandBtn_Click1" AutoPostBack="true" Visible="false" >
                   <asp:ListItem Text="Выполнено" Value="1" />
                   <asp:ListItem Text="Закупить" Value="2" />
                </asp:RadioButtonList> 
                </td>
                <td align="right"> 
                    <asp:Button ID="UpdateButton" runat="server" Text="Обновить" CommandName="Update" 
                            OnCommand="CommandBtn_Click" Visible="false"/>
                    <asp:Button ID="InsertButton" runat="server" Text="Добавить" CommandName="Insert" 
                            OnCommand="CommandBtn_Click"/>
                    <asp:Button ID="DeleteButton" runat="server" Text="Удалить" CommandName="Delete" 
                            OnCommand="CommandBtn_Click" Visible="false"/>
                </td>
                </tr>
                </table>
                </asp:tablecell>
            </asp:tablerow>
           </asp:table>
      </asp:Panel>
      <asp:GridView runat="server" ID="ListClaimSpares" DataSourceID="ClaimSparesListDataSource" AutoGenerateColumns="true"/> 
      <asp:ModalPopupExtender ID="ModalPopupExtender1"
                    runat="server"  
                    PopupControlID="UpdatePanel"
                    TargetControlID = "btnEditCustomer"  
                    OkControlID="editBox_OK"
                    BackgroundCssClass = "modalBackground" 
                    PopupDragHandleControlID = "Редактирование записи" Drag="True"
                    />
      <asp:DragPanelExtender ID="UpdatePanel_DragPanelExtender" runat="server" 
                  DragHandleID="UpdatePanel" Enabled="True" 
                  TargetControlID="UpdatePanel">
              </asp:DragPanelExtender>
    <asp:Panel ID="Panel1" runat="server">
    </asp:Panel>
</asp:Content>
