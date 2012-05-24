<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="CountDevice.aspx.cs" Inherits="WebApplication1.Directory.CountDevice" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script runat="server">


</script>
<asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
</asp:ScriptManager>
      <h3>Справочник отделений</h3> 
      <asp:Label id="Msg" runat="server" ForeColor="Red" />

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
                <br />
                </td>
        </tr>
      </table>

      </div>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" InteractiveDeviceInfos="(Коллекция)" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        <LocalReport ReportPath="Reports\Report1.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
      <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="SELECT * FROM [Device]"></asp:SqlDataSource>
      <div>
              Справочник отделений. Его необходимо запонлнять одним из первых. 
              В дальнейшем в приложении он используется как справочник для удобства поискаи фильтрации информациии,
              для получения укрупненной статистики и построения отчетов.
      </div>
</asp:Content>
