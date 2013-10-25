<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" CodeBehind="ContributorRegistration.aspx.cs" Inherits="WebApplication_Exam.Page.ContributorRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Title" runat="server">
    Contributor Registration
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder_ModuleTitle" ID="Content_Title">
    Contributor Registration
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_Module" runat="server">
    <div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Registration" DisplayMode="List" CssClass="infobox" ShowSummary="true" />
    </div>
    <div>
        <table style="margin: 0 auto;">
            <tr>
                <td style="width: 100px;">
                    User Name:
                </td>
                <td>
                    <asp:TextBox ID="Txt_UserName" runat="server" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="Req_Username" runat="server" ControlToValidate="Txt_UserName"
                        ErrorMessage="Username field is required." 
                        ValidationGroup="Registration">*
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegEx_Username" runat="server" ControlToValidate="Txt_UserName"
                        ValidationExpression="^[A-Za-z_.][A-Za-z\d_.]*$"
                        ErrorMessage="User Name must only contain alphanumeric characters and can include an underscore."
                        ValidationGroup="Registration">*
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Email:
                </td>
                <td>
                    <asp:TextBox ID="Txt_Email" runat="server" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="Req_Email" runat="server" ControlToValidate="Txt_Email"
                        ValidationGroup="Registration" ErrorMessage="Please enter a valid e-mail address.">*
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegEx_Email" runat="server" ControlToValidate="Txt_Email"
                        ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{2,3})(\]?)$"
                        ErrorMessage="Email address must be in the correct format." 
                        ValidationGroup="Registration">*
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button ID="Btn_Register" runat="server" Text="Register" Width="80px" ValidationGroup="Registration" />
                    <asp:Button ID="Btn_Back" runat="server" Text="Back" Width="80px" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
