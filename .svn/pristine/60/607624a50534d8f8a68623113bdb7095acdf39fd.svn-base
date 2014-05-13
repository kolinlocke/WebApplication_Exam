<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Control_GridList.ascx.cs" Inherits="WebApplication_Exam.UserControl.Control_GridList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">

        function RadAjaxPanel1_OnResponseEnd(Sender, Args) {
            var TxtSelected = document.getElementById('<%= this.Hid_Selected.ClientID %>').value;
            var ArrSelected = TxtSelected.split(',');
            var Table = $find('<%= this.Grid.ClientID %>').get_masterTableView();
            var Items = Table.get_dataItems();
            
            for (Selected in ArrSelected) {
                for (Ct = 0; Ct <= (Items.length - 1); Ct++) {
                    var Key = Items[Ct].getDataKeyValue('TmpKey');
                    if (Key == Selected) {
                        Items[Ct].set_selected(true);
                        break;
                    }
                }
            }
        }

    </script>
</telerik:RadScriptBlock>
<div>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadGrid ID="Grid" runat="server" Skin="Hay" AllowSorting="True" AutoGenerateColumns="False" AllowPaging="True">
            <MasterTableView Width="100%">
                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                    <HeaderStyle Width="20px" />
                </RowIndicatorColumn>
                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                    <HeaderStyle Width="20px" />
                </ExpandCollapseColumn>
                <EditFormSettings>
                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                    </EditColumn>
                </EditFormSettings>
                <PagerStyle AlwaysVisible="True" />
            </MasterTableView>
            <FilterMenu EnableImageSprites="False">
            </FilterMenu>
            <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Hay">
            </HeaderContextMenu>
        </telerik:RadGrid>
        <asp:HiddenField ID="Hid_Selected" runat="server" />
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Hay" Transparency="50">
    </telerik:RadAjaxLoadingPanel>
</div>
