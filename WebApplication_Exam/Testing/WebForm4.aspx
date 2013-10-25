<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" CodeBehind="WebForm4.aspx.cs" Inherits="WebApplication_Exam.Testing.WebForm4" %>
<%@ Register Src="~/UserControl/Control_Selection.ascx" TagName="Selection" TagPrefix="Uc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_ModuleTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_Module" runat="server">
    <Uc:Selection runat="server" ID="Selection" />
    <asp:Button ID="Btn_Select" runat="server" Text="Select" Width="80px" />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Height="200px" Width="300px">
    </telerik:RadAjaxPanel>
</asp:Content>
