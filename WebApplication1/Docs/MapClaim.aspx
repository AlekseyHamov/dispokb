<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Culture="auto" UICulture="auto"  
    CodeBehind="MapClaim.aspx.cs" Inherits="WebApplication1.Directory.MapClaim" %>
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
      <br />
      <asp:ObjectDataSource 
        ID="FileCoordimateDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataImage.ImageData" 
        SelectMethod="FileCoordinate" 
        >
        <SelectParameters>
            <asp:Parameter Name= "ID_files" DefaultValue="0" />  
        </SelectParameters> 
      </asp:ObjectDataSource>
      <asp:ObjectDataSource 
        ID="ObjectDataTempGrig" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataImage.ImageData" 
        SelectMethod="GetTempGrid" >
        <SelectParameters>
            <asp:Parameter Name= "NameTable" DefaultValue="Building" />  
            <asp:Parameter Name= "ID_Table" DefaultValue="" />  
        </SelectParameters> 
      </asp:ObjectDataSource>
      <asp:ObjectDataSource 
        ID="ImageFilesObjectDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataImage.ImageData" 
        InsertMethod="AddEmployee"
        >
      </asp:ObjectDataSource>
      <div style="overflow:scroll; width=500" >
        <asp:ImageMap ID="MapPage" 
            runat="server" Visible="false" width="500"  hotspotmode="PostBack"
            onclick="VoteMap_Clicked"/>
      </div>
      <div id="DivRightPage" runat="server" style="float:right" >
      </div>
      d<br />
      <div id="service" runat="server" visible="true">
          <asp:TextBox ID="Name_Url" runat="server" Text="Building" Visible="false"></asp:TextBox>
          <asp:GridView ID="TempGrid" runat="server" Visible="false" >
          </asp:GridView>
          <asp:GridView ID="IDLinkClaim" runat="server">
          </asp:GridView> 
      </div> 
</asp:Content> 