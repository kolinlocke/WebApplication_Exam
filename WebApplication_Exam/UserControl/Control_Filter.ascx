<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Control_Filter.ascx.cs" Inherits="WebApplication_Exam.UserControl.Control_Filter" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div>
    <fieldset>
        <legend>
            Filters
        </legend>
        <table border="0" cellpadding="0" cellspacing="5">
            <tr>
                <td width="155px">
                    <asp:DropDownList ID="Cbo_Filter" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td width="205px">
                    <asp:TextBox ID="Txt_Filter" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td width="85px">
                    <asp:Button ID="Btn_FilterNew" runat="server" Text="Search" Width="80px" />
                </td>
                <td width="85px">
                    <asp:Button ID="Btn_FilterAdd" runat="server" Text="+Search" Width="80px" />
                </td>
                <td>
                    <asp:Button ID="Btn_FilterClear" runat="server" Text="Clear" Width="80px" />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <telerik:RadAjaxPanel ID="RadAjaxPanel_FilterLabel" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                        <asp:Label ID="Lbl_Filter" runat="server"></asp:Label>
                        <asp:HiddenField ID="Hid_IsFilter" runat="server" />
                    </telerik:RadAjaxPanel>
                    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Hay" Transparency="50">
                    </telerik:RadAjaxLoadingPanel>
                </td>
            </tr>
        </table>
    </fieldset>
</div>

