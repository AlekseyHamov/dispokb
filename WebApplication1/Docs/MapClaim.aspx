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
      <br />
    <asp:ObjectDataSource ID="ImageChecked" runat="server" 
        SelectMethod="GetDeviceForRoomMaping" 
        TypeName="Samples.AspNet.ObjectDataDevice.DeviceData">
        <SelectParameters>
            <asp:Parameter Name="ID_Unit" DefaultValue=""/>
            <asp:Parameter Name="str_ID" DefaultValue=""/>
            <asp:Parameter Name="ID_Room" DefaultValue="0"/>
        </SelectParameters>
    </asp:ObjectDataSource>
      <asp:ObjectDataSource 
        ID="TreeDevice" 
        runat="server" 
        TypeName="Samples.AspNet.ObjectDataTreeDevice.TreeDeviceData"
        >
      </asp:ObjectDataSource>

      <table>
          <tr style="border-style:double;" >
              <td>
               <div style="overflow:scroll;float:left;" >
                    <asp:ImageMap ID="MapPage" 
                        runat="server" Visible="false" width="500"  hotspotmode="PostBack"
                        onclick="VoteMap_Clicked"/>
                </div>
                <div id="HeaderDeviceScrol" runat="server" style=" overflow-x:scroll;"  >
                <asp:ListView  ID="CheckBoxImage" runat="server" 
                    RepeatDirection="Horizontal"
                    DataKeyNames="ID,ID_files,fileName,fileType,NameDevice, ID_Device, roomdevice,roomdevicecount" 
                    DataSourceID="ImageChecked" GroupItemCount="3" >
                    <GroupTemplate>
                        <tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                        </tr>
                    </GroupTemplate>
                    <LayoutTemplate>
                        <table>
                            <asp:PlaceHolder ID="groupPlaceholder" runat="server" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <td  style="border-style:ridge; border-width:thin; "     >
                            <asp:ImageMap ID="IMG" runat="server" Height="50" 
                                ImageUrl='<%#Eval("fileType", "~/Image_Data/"+Eval("fileName")+"_"+Eval("ID_files")+"."+Eval("fileType")) %>'  ToolTip='<%#Eval("NameDevice") %>' />
                            <br />
                            <asp:CheckBox ID="IMGCHECK" runat="server" ToolTip="Выбрать" />
                            <br />
                            <asp:Label ID="Label1" runat="server" Text="Кол-во=" ToolTip="Колличество"  />
                            <asp:Label ID="CountDevice" runat="server" Width="17" Text='<%# Eval("roomdevicecount") %>' ToolTip="Колличество"  />
                            <asp:Panel id="TDControl" runat="server" /> 
                        </td>
                    </ItemTemplate>
                </asp:ListView >
                <asp:TreeView ID="TreeView2" OnTreeNodePopulate="TreeNodePopulate" runat="server" >
                    <Nodes>
                        <asp:TreeNode Text="Новый узел" Value="Новый узел">
                            <asp:TreeNode Text="Новый узел" Value="Новый узел"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="Новый узел" Value="Новый узел">
                            <asp:TreeNode Text="Новый узел" Value="Новый узел">
                                <asp:TreeNode Text="Новый узел" Value="Новый узел">
                                    <asp:TreeNode Text="Новый узел" Value="Новый узел"></asp:TreeNode>
                                </asp:TreeNode>
                            </asp:TreeNode>
                        </asp:TreeNode>
                    </Nodes>
                    </asp:TreeView>
                <asp:Button ID="reteeee" OnClick="DeviceCildren_room"   Text="fhghhg" runat="server" />
                </div>
              </td>
            </tr>      
            <tr>
                <td>
                  <div id="DivRightPage" runat="server" style=" width:100% ; float:right ;  " >
                  </div>
                </td>
            </tr>
      </table>
      <div id="service" runat="server" visible="false">
          <asp:TextBox ID="Name_Url" runat="server" Text="Building" Visible="false"></asp:TextBox>
          <asp:GridView ID="TempGrid" runat="server" Visible="false" >
          </asp:GridView>
          <asp:GridView ID="IDLinkClaim" runat="server" Visible="false">
          </asp:GridView> 
      </div> 
</asp:Content> 