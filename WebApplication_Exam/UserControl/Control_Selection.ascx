<%@ Control Language="C#" CodeBehind="Control_Selection.ascx.cs" Inherits="WebApplication_Exam.UserControl.Control_Selection" %>
<%@ Register Src="~/UserControl/Control_GridList.ascx" TagName="Grid" TagPrefix="Uc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            var Current_Source_ControlID;
            var Current_Target_PanelID;

            function ShowSelection(Source_ControlID, Target_PanelID, Window_Title, Window_Height, Window_Width) {
                ClearFlags();
                Current_Source_ControlID = Source_ControlID;
                Current_Target_PanelID = Target_PanelID;

                var RadWindow = $find("<%= RadWindow_Selection.ClientID %>");
                RadWindow.setSize(Window_Width, Window_Height);
                RadWindow.set_title(Window_Title);

                var Panel = $find('<%= this.RadWindow_RadAjaxPanel_Selection.ClientID %>');
                Panel.ajaxRequest('SelectStart,' + Source_ControlID);
            }

            function RadWindow_Selection_OnClose(Sender, Args) {
                if (Args.get_argument() == '1') {
                    var Hid = document.getElementById('<%= this.RadWindow_Selection_Hid_ControlID.ClientID %>');
                    Hid.value = Current_Source_ControlID;
                    var Panel = $find(Current_Target_PanelID);
                    Panel.ajaxRequest('Selection');
                    ClearFlags();
                }
            }

            function RadWindow_RadAjaxPanel_Selection_AjaxEnd(Sender, Args) {
                var RadWindow = $find("<%= RadWindow_Selection.ClientID %>");
                RadWindow.show();
            }

            function RadWindow_Selection_Accept() {
                var RadWindow = $find("<%= RadWindow_Selection.ClientID %>");
                RadWindow.close('1');
            }

            function RadWindow_Selection_Close() {
                var RadWindow = $find("<%= RadWindow_Selection.ClientID %>");
                RadWindow.close();
            }

            function ClearFlags() {
                var Hid = document.getElementById('<%= this.RadWindow_Selection_Hid_ControlID.ClientID %>');
                Hid.value = '';
                Current_Source_ControlID = '';
                Current_Target_PanelID = '';
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="RadWindow_Selection" runat="server" 
        Width="500px" Height="450px" 
        Modal="True" Skin="Hay" Title="Selection" 
        OnClientClose="RadWindow_Selection_OnClose"
        Behaviors="Close, Move" KeepInScreenBounds="True">
        <ContentTemplate>
            <telerik:RadAjaxPanel ID="RadWindow_RadAjaxPanel_Selection" runat="server" ClientEvents-OnResponseEnd="RadWindow_RadAjaxPanel_Selection_AjaxEnd">
                <table>
                    <tr>
                        <td>
                            <Uc:Grid runat="server" ID="Grid_Selection" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="RadWindow_Selection_Btn_Ok" runat="server" Text="Ok" Width="80px" OnClientClick="RadWindow_Selection_Accept(); return false;" />
                            &nbsp;
                            <asp:Button ID="RadWindow_Selection_Btn_Cancel" runat="server" Text="Cancel" Width="80px" OnClientClick="RadWindow_Selection_Close(); return false;" />
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="RadWindow_Selection_Hid_ControlID" runat="server"></asp:HiddenField>
            </telerik:RadAjaxPanel>
        </ContentTemplate>
    </telerik:RadWindow>
</div>
<br />
<br />
