<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="HR.aspx.cs" Inherits="WebApplication_Exam.Page.HR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Title" runat="server">
    Exam | HR Home
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_ModuleTitle" runat="server">
    Administration
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_Module" runat="server">
    <ul>
        <li>
            <a href='<%= this.ResolveUrl("~/Page/ExamStart.aspx") %>'>Applicant Exam</a> 
        </li>
        <li>
            <a href='<%= this.ResolveUrl("~/Page/ExamReport.aspx") %>'>Exam Report</a> 
        </li>
    </ul>
</asp:Content>
