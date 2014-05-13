<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" CodeBehind="Default.aspx.cs" Inherits="WebApplication_Exam.Page.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Title" runat="server">
    Exam | Home
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder_ModuleTitle" ID="Content_Title">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_Module" runat="server">
    <ul>
        <li><a href="<%= this.ResolveUrl("~/Page/Login.aspx") %>">User Login</a></li>
        <%-- 
        <li><a href="<%= this.ResolveUrl("~/Page/Login.aspx?Type=1") %>">Contributor Login</a></li>
        --%>
        <li><a href="<%= this.ResolveUrl("~/Page/ContributorRegistration.aspx") %>">Contributor Registration</a></li>
        <%--
        <li><a href="<%= this.ResolveUrl("~/Page/ExamStart.aspx") %>">Applicant Exam</a></li>
        --%>
    </ul>
</asp:Content>