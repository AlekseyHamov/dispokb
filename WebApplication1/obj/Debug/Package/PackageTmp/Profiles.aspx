<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profiles.aspx.cs" Inherits="WebApplication1.Profiles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<script runat="server">
void SetPostalCode_Click(object sender, System.EventArgs e)
{
    Profile.PostalCode = Server.HtmlEncode(textPostalCode.Text);    
    labelPostalCode.Text = Profile.PostalCode;
}
void AddURL_Click(object sender, System.EventArgs e)
{
    String urlString = Server.HtmlEncode(textFavoriteURL.Text);
    if (Profile.FavoriteURLs == null)
    {
        Profile.FavoriteURLs = new
            System.Collections.Specialized.StringCollection();
    }
    Profile.FavoriteURLs.Add(urlString); 
    DisplayFavoriteURLs();
}

void DisplayFavoriteURLs()
{
    listFavoriteURLs.DataSource = Profile.FavoriteURLs;
    listFavoriteURLs.DataBind();
}

void Page_Load(object sender, System.EventArgs e)
{
    labelPostalCode.Text = Profile.PostalCode;
    DisplayFavoriteURLs();
}
</script>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TextBox ID="textPostalCode" runat="server"></asp:TextBox>
    <asp:Button ID="SetPostalCode" runat="server" Text="Укажите почтовый индекс" OnClick="SetPostalCode_Click" />
    <asp:Label ID="labelPostalCode" runat="server" Text="(пусто)"></asp:Label>
    
    <br />
    
    <asp:TextBox ID="textFavoriteURL" runat="server"></asp:TextBox>
    <asp:Button ID="AddURL" runat="server" Text="Добавьте URL-адрес" OnClick="AddURL_Click" />
    <asp:ListBox ID="listFavoriteURLs" runat="server"></asp:ListBox>

<asp:Login ID="Login1" runat="server">
</asp:Login>
<asp:LoginName ID="LoginName1" runat="server" />

</asp:Content>
