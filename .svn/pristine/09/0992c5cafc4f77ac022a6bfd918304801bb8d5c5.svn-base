<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" CodeBehind="ExamStart.aspx.cs" Inherits="WebApplication_Exam.Page.ExamStart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Title" runat="server">
    Applicant Exam
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder_ModuleTitle" ID="Content_Title">
    Applicant Exam
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_Module" runat="server">
    <div>
        <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="ExamStart" ShowSummary="true" DisplayMode="List" CssClass="infobox" runat="server" />
    </div>
    <br />
    <br />
    <table align="center">
        <tr>
            <td style="width: 120px;">
                Name:
            </td>
            <td>
                <asp:TextBox runat="server" ID="Txt_Name" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="Req_Name" runat="server" ControlToValidate="Txt_Name"
                    ValidationGroup="ExamStart" ErrorMessage="Please type in your name.">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Email:
            </td>
            <td>
                <asp:TextBox runat="server" ID="Txt_Email" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="Req_Email" runat="server" ControlToValidate="Txt_Email"
                    ValidationGroup="ExamStart" ErrorMessage="Please enter your email address for us to send your test results.">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                    ControlToValidate="Txt_Email" ErrorMessage="Enter a valid email address." ValidationGroup="ExamStart"
                    SetFocusOnError="true">*</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                Category:
            </td>
            <td>
                <asp:DropDownList ID="Cbo_Category" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button runat="server" Text="Click to start a new exam session" ID="Btn_Start" ValidationGroup="ExamStart" Width="250px" />
                <br />
                <br />
                <asp:LinkButton runat="server" Text="Go Back" ID="Btn_Back" />
            </td>
        </tr>
    </table>
</asp:Content>
