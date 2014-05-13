<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Details.Master" CodeBehind="User_Details.aspx.cs" Inherits="WebApplication_Exam.Page.User_Details" %>
<%@ Register Src="~/UserControl/Control_GridList.ascx" TagName="GridList" TagPrefix="Uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Title" runat="server">
    Exam | <%= this.Lbl_Title.Text %>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder_ModuleTitle" ID="Content_Title">
    <asp:Label ID="Lbl_Title" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_Module" runat="server">
    <script type="text/javascript">
        function Grid_OnCommand(Sender, Args) {
            var Command = Args.get_commandName();
            var ItemIndex = Args.get_commandArgument();
            switch (Command) {
                case "IsActive":
                    RequireSave();
                    break;
            }
        }
    </script>
    <div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="UserDetails" ShowSummary="true" DisplayMode="List" CssClass="infobox" />
    </div>
    <div>
        <table style="width: 100%">
            <tr>
                <td style="width: 100px;">
                    User Name:
                </td>
                <td>
                    <asp:TextBox ID="Txt_UserName" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                    <asp:HiddenField ID="Hid_Username" runat="server" />
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Password:
                </td>
                <td>
                    <asp:TextBox ID="Txt_Password" runat="server" Width="200px" MaxLength="50" TextMode="Password"></asp:TextBox>
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Confirm Password:
                </td>
                <td>
                    <asp:TextBox ID="Txt_ConfirmPassword" runat="server" Width="200px" MaxLength="50"
                        TextMode="Password"></asp:TextBox>
                    <asp:Label ID="Label3" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div>
        <Uc:GridList runat="server" ID="Grid_Details" />
    </div>
</asp:Content>
