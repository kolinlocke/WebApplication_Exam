<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" CodeBehind="Configuration.aspx.cs" Inherits="WebApplication_Exam.Page.Configuration" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UserControl/Control_GridList.ascx" TagName="GridList" TagPrefix="Uc" %>
<%@ Register Src="~/UserControl/Control_Selection.ascx" TagName="Selection" TagPrefix="Uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Title" runat="server">
    Configuration
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder_ModuleTitle" ID="Content_Title">
    Configuration
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_Module" runat="server">
    <script type="text/javascript" src="../Scripts/Js_ModuleRequired.js"></script>
    <Uc:Selection runat="server" ID="Selection" />
    <asp:Panel ID="Panel_Details" runat="server">    
        <table>
            <tr>
                <td style="width: 200px">
                    Exam total no. of items:
                </td>
                <td>
                    <asp:TextBox ID="Txt_NoItemsTotal" runat="server" Width="80px">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    No. of items per page:
                </td>
                <td>
                    <asp:TextBox ID="Txt_NoItemsPerPage" runat="server" Width="80px">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Required no. of answer options:
                </td>
                <td>
                    <asp:TextBox ID="Txt_NoAnswersRequired" runat="server" Width="80px">
                    </asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <div>
            <table width="300px">
                <tr>
                    <td>
                        <em>
                            Contributor Default Roles
                        </em>
                    </td>
                    <td align="right">
                        <asp:LinkButton ID="Btn_AddContributorDefaultRole" runat="server" Text="Add Role" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <Uc:GridList ID="Grid_RightsIDs" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <br />
    <div>
        <table>
            <tr>
                <td>
                    <asp:Button ID="Btn_Save" runat="server" Text="Save" Width="80px" />
                    &nbsp;
                    <input type="button" value="Go back" onclick="location.href = 'Default.aspx';" style="width: 80px;" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
