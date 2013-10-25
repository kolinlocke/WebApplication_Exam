<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Details.master" CodeBehind="Rights_Details.aspx.cs" Inherits="WebApplication_Exam.Page.Rights_Details" %>
<%@ Register Src="~/UserControl/Control_GridList.ascx" TagName="GridList" TagPrefix="Uc" %>
<%@ Register Src="~/UserControl/Control_Filter.ascx" TagName="Filter" TagPrefix="Uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Title" runat="server">
   Exam | <%= this.Lbl_Title.Text %> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_ModuleTitle" runat="server">
    <asp:Label ID="Lbl_Title" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_Module" runat="server">
    <script type="text/javascript">
        function Grid_OnCommand(Sender, Args) {
            var Command = Args.get_commandName();
            var ItemIndex = Args.get_commandArgument();
            switch (Command) {
                case "IsAllowed":
                    RequireSave();
                    break;
            }
        }

        function Btn_SetAll(IsTrue) {
            var Panel = $find('<%= this.Grid_Details.pAjaxPanel.ClientID %>');
            Panel.ajaxRequest('SetIsAllowed,' + IsTrue);
        }

    </script>
    <div>
        <table>
            <tr>
                <td style="width: 100px;">
                    Role Name:
                </td>
                <td>
                    <asp:TextBox ID="Txt_Name" runat="server" Width="200px" MaxLength="50">
                    </asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div>
        <Uc:Filter runat="server" ID="Filter_Details" />
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:Button ID="Btn_SetAll" runat="server" Text="Set All" OnClientClick="Btn_SetAll(true); return false;" Width="100px"/>
                    <asp:Button ID="Btn_UnSetAll" runat="server" Text="Unset All" OnClientClick="Btn_SetAll(false); return false;" Width="100px"/>
                </td>
            </tr>
        </table>
        <Uc:GridList runat="server" ID="Grid_Details" />
    </div>
</asp:Content>
