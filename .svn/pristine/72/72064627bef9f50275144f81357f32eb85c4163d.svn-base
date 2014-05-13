<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" CodeBehind="ExamReport.aspx.cs" Inherits="WebApplication_Exam.Page.ExamReport" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UserControl/Control_GridList.ascx" TagName="GridList" TagPrefix="Uc" %>
<%@ Register Src="~/UserControl/Control_Filter.ascx"  TagName="Filter" TagPrefix="Uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Title" runat="server">
    Exam Report
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder_ModuleTitle" ID="Content_Title">
    Exam Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_Module" runat="server">
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
    </telerik:RadStyleSheetManager>
    <div>
        <Uc:Filter ID="ListFilter" runat="server" />
        <Uc:GridList ID="ReportGrid" runat="server" />
    </div>
    <br />
    <br />
    <div style="text-align: right;">
        <input type="button" value="Go back" onclick="location.href = 'Default.aspx';" style="width: 120px;" />
    </div>
</asp:Content>
