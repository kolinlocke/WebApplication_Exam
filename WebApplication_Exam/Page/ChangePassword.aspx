<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" CodeBehind="ChangePassword.aspx.cs" Inherits="WebApplication_Exam.Page.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Title" runat="server">
    Change Password
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_ModuleTitle" runat="server">
    Change Password
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_Module" runat="server">
    <div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="UserDetails" ShowSummary="true" DisplayMode="List" CssClass="infobox" />
    </div>
    <div>
        <table style="width: 100%">
            <tr>
                <td>
                    Password:
                </td>
                <td>
                    <asp:TextBox ID="Txt_Password" runat="server" Width="200px" MaxLength="50" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="Validator_Password"
                        ControlToValidate="Txt_Password"
                        ValidationGroup="UserDetails"
                        ErrorMessage="New Password is required."
                        Text="*">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Confirm Password:
                </td>
                <td>
                    <asp:TextBox ID="Txt_ConfirmPassword" runat="server" Width="200px" MaxLength="50" TextMode="Password"></asp:TextBox>
                    <asp:CompareValidator runat="server" ID="Validator_ConfirmPassword" 
                        ControlToValidate="Txt_ConfirmPassword" 
                        ControlToCompare="Txt_Password"
                        ValidationGroup="UserDetails"
                        ErrorMessage="Password and Confirm Password fields must match."
                        Text="*">
                    </asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table style="width: 100%">
            <tr>
                <td align="right">
                    <asp:Button ID="Btn_Save" runat="server" Text="Change Password" Width="150px" ValidationGroup="UserDetails" />
                    <input type="button" value="Go back" onclick="location.href = 'Default.aspx';" style="width: 150px;" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
