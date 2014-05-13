<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" CodeBehind="ContributorApproval.aspx.cs" Inherits="WebApplication_Exam.Page.ContributorApproval" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UserControl/Control_GridList.ascx" TagPrefix="Uc" TagName="GridList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Title" runat="server">
    Contributor Approval
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder_ModuleTitle" ID="Content_Title">
    Contributor Approval
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_Module" runat="server">
    <div>
        <Uc:GridList runat="server" ID="GridList"></Uc:GridList>
    </div>
    <br />
    <div>
        <asp:Button ID="Btn_Approve" runat="server" Text="Approve Selected" Width="150px" />
    </div>
    <br />
    <br />
    <div style="text-align: right;">
        <input type="button" value="Go back" onclick="location.href = 'Default.aspx';" style="width: 120px;" />
    </div>
</asp:Content>
