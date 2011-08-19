<%@ Page Title="Домашняя страница" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<script runat="server">
    protected void Button_Click(object sender, EventArgs e)
    {
//        Msg.Text = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
        Msg.Text = Environment.CurrentDirectory;
    }
    protected void Button_Upload_Click(object sender, EventArgs e)
    {
        string imageType = UploadedFile.PostedFile.ContentType;
        byte[] imageData = new byte[UploadedFile.PostedFile.InputStream.Length + 1];

        UploadedFile.PostedFile.InputStream.Read(imageData, 0, imageData.Length);

        System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection("Data Source=(Local);Initial Catalog=DispOKB;User ID=sa;Password=20");
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("INSERT INTO files (fileData,fileType) VALUES (@imgData,@imgType);SELECT @@IDENTITY", cn);

        System.Data.SqlClient.SqlParameter para1 = new System.Data.SqlClient.SqlParameter("@imgData", System.Data.SqlDbType.Image);
        para1.Value = imageData;
        cmd.Parameters.Add(para1);


        System.Data.SqlClient.SqlParameter para2 = new System.Data.SqlClient.SqlParameter("@imgType", System.Data.SqlDbType.VarChar, 50);
        para2.Value = imageType;
        cmd.Parameters.Add(para2);


        cn.Open();
        cmd.ExecuteNonQuery();
        int theID = System.Convert.ToInt32(cmd.ExecuteScalar());
        cn.Close();
        lit_status.Text = "The file has been uploaded to the database. Click <a href=\"Default.aspx?id=" + theID + "\">here</a> to see it.";
    }
</script>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
        Добро пожаловать!
      <asp:Label id="Msg" runat="server" ForeColor="Red" />

          <asp:Literal ID="lit_status" runat="server" /><br />
          <asp:FileUpload ID="UploadedFile" runat="server" /><br />
          <asp:Button ID="Button_Upload" runat="server" Text="Upload File" 
                    onclick="Button_Upload_Click" />
          <asp:Button ID="Button1" runat="server" Text="123" 
                    onclick="Button_Click" />

    </h2>
</asp:Content> 
