<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OKB_ALL.aspx.cs" Inherits="WebApplication1.Map_Image.OKB_ALL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<asp:Label id="Msg" runat="server" ForeColor="Red" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script runat="server">
    void ImageButton_Click(object sender, ImageClickEventArgs e)
    {
        Msg.Text = "You clicked the ImageButton control at the coordinates: (" +
                      e.X.ToString() + ", " + e.Y.ToString() + ")";
        X.Text = e.X.ToString();
        Y.Text = e.Y.ToString();
    }
</script>
      <asp:ObjectDataSource 
        ID="ImageObjectDataSource" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataImage.ImageData" 
        InsertMethod="AddEmployee"
        SelectMethod ="TestGetSqlBytes">
      </asp:ObjectDataSource>
        <asp:Label id="lblMessage" runat="server" />
        <asp:FileUpload ID="MyFile" runat="server" />
        <asp:Button id="btnUpload" OnClick="btnUpload_Click" Text="Upload!" runat="server" />
        <asp:Button id="btnLoad" OnClick="btnLoad_Click" Text="Load!" runat="server" />
      <div style="width:200; height:200">
      <asp:TextBox ID="X" runat="server"></asp:TextBox>  
      <asp:TextBox ID="Y" runat="server"></asp:TextBox>
          
      <asp:ImageButton ID="ghhjhjg" ImageUrl="~/Map_Image/OKB_ALL.jpg"
       width="900"
       onclick="ImageButton_Click"
       runat="server"  > 
       </asp:ImageButton> 

      <asp:ImageMap ID="Buttons" ImageUrl="~/Map_Image/OKB_ALL.jpg"
       onclick="ButtonsMap_Clicked"
       runat="server"
       width="900"
       alternatetext="Карта больницы"
       hotspotmode="NotSet"  >
       <asp:RectangleHotSpot
          hotspotmode="Navigate"
          NavigateUrl="http://www.contoso.com"
          alternatetext="Главный корпус блок 'АБ'"
          left="400"
          top="130"
          right="480"
          bottom="350">
       </asp:RectangleHotSpot>    
       <asp:RectangleHotSpot
          hotspotmode="Navigate"
          NavigateUrl="http://www.contoso.com"
          alternatetext="Главный корпус блок 'В'"
          left="400"
          top="350"
          right="480"
          bottom="450"
           > 
      </asp:RectangleHotSpot>
       <asp:RectangleHotSpot
          hotspotmode="Navigate"
          NavigateUrl="http://www.contoso.com"
          alternatetext="Главный корпус блок 'Г'"
          left="317"
          top="323"
          right="419"
          bottom="358"
          > 
      </asp:RectangleHotSpot>
       <asp:RectangleHotSpot
          hotspotmode="Navigate"
          NavigateUrl="http://www.contoso.com"
          alternatetext="Главный корпус блок 'Д'"
          left="335"
          top="180"
          right="370"
          bottom="323"
           > 
       </asp:RectangleHotSpot>
       <asp:RectangleHotSpot
          hotspotmode="Navigate"
          NavigateUrl="http://www.contoso.com"
          alternatetext="Гараж"
          left="65"
          top="25"
          right="175"
          bottom="70"
           > 
      </asp:RectangleHotSpot>
      <asp:PolygonHotSpot 
        HotSpotMode="Navigate"
        AlternateText="Радиология"
        Coordinates="230,40,230,65,380,65,376,41, 300,37, 304,13, 338,15, 335,37"
        >
        </asp:PolygonHotSpot> 
       </asp:ImageMap> 
      </div>
      </asp:Content>
