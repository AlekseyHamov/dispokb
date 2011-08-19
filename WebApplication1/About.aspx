<%@ Page Title="Диспетчер" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="About.aspx.cs" Inherits="WebApplication1.About" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<script type="text/javascript">

    function OpenD() {

        var r = window.showModalDialog("Web_Test.aspx", null, "dialogWidth:450px;menubar:0;dialogHeight:450px");
    }

</script>

    <h2>
        О программе
    </h2>
    <p>
        Приложение диспетчерская предназначено для оформления заявок и мониторинга их выполения. 
        Для сбора информации о потребности в материалах.
	Create Modal Dialog Box
	<input type="button" value="Push To Create" onclick="OpenD()"/>
    </p>
</asp:Content>
