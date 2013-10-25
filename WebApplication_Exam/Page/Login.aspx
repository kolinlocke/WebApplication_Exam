<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" CodeBehind="Login.aspx.cs" Inherits="WebApplication_Exam.Page.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Title" runat="server">
    User Login
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder_ModuleTitle" ID="Content_Title">
    User Login
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_Module" runat="server">
    <table style="margin: 0 auto;">
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="80px">
                            <strong>Username:</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="Txt_Username" Width="200px" runat="server" MaxLength="50" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="Txt_Username"
                                ErrorMessage="User Name is required." ValidationGroup="Login" runat="server">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <strong>Password: </strong>
                        </td>
                        <td>
                            <asp:TextBox ID="Txt_Password" Width="200px" runat="server" MaxLength="15" TextMode="Password" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="Txt_Password"
                                ErrorMessage="Password is required." ValidationGroup="Login" runat="server">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2" class="clsButton">
                            <asp:Button ID="Btn_Login" Text="Login" CausesValidation="True" ValidationGroup="Login"
                                Width="80px" runat="server" />
                            <input type="button" value="Go back" onclick="location.href = 'Default.aspx';" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel_Msg" runat="server" Visible="false">
                                <strong>
                                    <asp:Label ID="Lbl_Msg" runat="server" Visible="false" ForeColor="Red" />
                                </strong>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
