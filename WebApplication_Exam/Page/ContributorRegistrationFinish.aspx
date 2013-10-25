<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" CodeBehind="ContributorRegistrationFinish.aspx.cs" Inherits="WebApplication_Exam.Page.ContributorRegistrationFinish" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Title" runat="server">
    Registration Successful
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder_ModuleTitle" ID="Content_Title">
    Registration Successful
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_Module" runat="server">
    <br />
    <div>
        You will be informed by an administrator via the email that you entered.
        <br />
        <br />
        <a href="<%= this.ResolveUrl("~/Page/Default.aspx") %>">Back to Main</a>
    </div>
</asp:Content>
