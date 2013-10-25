<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" CodeBehind="Question_DetailsEx2.aspx.cs" Inherits="WebApplication_Exam.Page.Question_DetailsEx2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ContentPlaceHolderID="Head" runat="server">
    <title> Exam | <%= String.IsNullOrEmpty(Request.QueryString["ID"]) ? "New Question" : "Question Details: ID #" + Request.QueryString["ID"] %> </title>
</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 id="page-title"> <%= String.IsNullOrEmpty(Request.QueryString["ID"]) ? "New Question" : "Question Details: ID #" + Request.QueryString["ID"] %> </h1>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function Grid_Command(Sender, Args) {
                var Command = Args.get_commandName();
                var ItemIndex = Args.get_commandArgument();

                switch (Command) {
                    case "Edit":
                        {
                            var Panel = $find("<%= RadAjaxPanel_AnswerWindow.ClientID %>");
                            Panel.ajaxRequest(ItemIndex);
                            break;
                        }
                    case "Delete":
                        {
                            var Panel = $find("<%= RadAjaxPanel1.ClientID %>");
                            Panel.ajaxRequest('Delete,' + ItemIndex);
                            break;
                        }
                }

                Args.set_cancel(true);
            }

            function Btn_New_Click() {
                var Panel = $find("<%= RadAjaxPanel_AnswerWindow.ClientID %>");
                Panel.ajaxRequest('');
            }

            function RadAjaxPanel_AnswerWindow_AjaxEnd(Sender, Args) {
                var RadWindow = $find("<%= RadWindow1.ClientID %>");
                RadWindow.show();
            }

            function RadWindow_Accept() {
                var RadWindow = $find("<%= RadWindow1.ClientID %>");
                RadWindow.close('1');
            }

            function RadWindow_Close() {
                var RadWindow = $find("<%= RadWindow1.ClientID %>");
                RadWindow.close();
            }

            function RadWindow_OnClientClose(Sender, Args) {
                if (Args.get_argument() == '1') {
                    var Panel = $find("<%= RadAjaxPanel1.ClientID %>");
                    var Cmb = $find("<%= RadWindow1_RadComboBox1.ClientID %>");
                    var Text = Cmb.get_text();
                    Panel.ajaxRequest('Dialog_Answer,' + Text, '');
                }
            }
        </script>
    </telerik:RadCodeBlock>

    <telerik:RadWindow ID="RadWindow1" runat="server" Modal="True" Width="500px" Height="200px"
        Skin="Hay" OnClientClose="RadWindow_OnClientClose" Behaviors="Close" Title="New Answer">
        <ContentTemplate>
            <telerik:RadAjaxPanel ID="RadAjaxPanel_AnswerWindow" runat="server" ClientEvents-OnResponseEnd="RadAjaxPanel_AnswerWindow_AjaxEnd">
                <table>
                    <tr>
                        <td style="width: 100px">
                            Answer:
                        </td>
                        <td>
                            <telerik:RadComboBox ID="RadWindow1_RadComboBox1" runat="server" Width="350px" AllowCustomText="True" EnableLoadOnDemand="True" Skin="Hay">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Is Answer?
                        </td>
                        <td>
                            <asp:CheckBox ID="RadWindow1_Chk_IsAnswer" runat="server"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:Button ID="RadWindow1_Btn_Ok" runat="server" Text="Ok" Width="80px" OnClientClick="RadWindow_Accept(); return false;" />
                            <asp:Button ID="RadWindow1_Btn_Cancel" runat="server" Text="Cancel" Width="80px"
                                OnClientClick="RadWindow_Close(); return false;" />
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="RadWindow1_Hid_TmpKey" runat="server"></asp:HiddenField>
            </telerik:RadAjaxPanel>
        </ContentTemplate>
    </telerik:RadWindow>

    <div>
        <div>
            <asp:Panel ID="Panel1" runat="server" Visible="False" CssClass="infobox">
                <asp:Label ID="Lbl_EventMsg" CssClass="ClsEventMsg" runat="server"></asp:Label>
                
                <br /> <br />
            </asp:Panel>
        </div>

        <br />

        <div>
            <asp:Panel ID="Panel_Details" runat="server">
                <fieldset>
                    <legend>Question Details </legend>

                    <table style="width: 100%">
                        <tr>
                            <td style="width: 100px;">
                                Question:
                            </td>
                            <td>
                                <asp:TextBox ID="Txt_Question" runat="server" Width="500px" Height="60px" MaxLength="200" TextMode="MultiLine">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan ="2">
                                <asp:CheckBox ID="Chk_IsMultipleAnswer" runat="server" Text="Has Multiple Answers?" />
                            </td>
                        </tr>
                    </table>
                </fieldset>

                <br />

                <table style="width: 100%">
                    <tr>
                        <td align="right">
                            <asp:Button ID="Btn_New" runat="server" Text="+ New Answer" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%">
                                <telerik:RadGrid ID="RadGrid1" runat="server" Skin="Hay">
                                </telerik:RadGrid>
                            </telerik:RadAjaxPanel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>

        <br /> 

        <div class="infobox"> REMINDER: Click <strong>Save</strong> when you are done adding/editing answers. </div>
        
        <br />

        <div style="float: left;">
            <asp:Button ID="Btn_Save" runat="server" Text="Save" Width="80px" />
            <input type="button" onclick="location.href = 'Question.aspx';" value="Go back to Questions List" style="width: 200px;" />

            <br />

            <asp:Button ID="Btn_Approve" runat="server" Text="Approve" Width="80px" Enabled="False" />
        </div>

        <div style="float: right;">
            <asp:Button ID="Btn_Delete" runat="server" Text="Delete Question" OnClick="btnDelete_Click" />
        </div>
    </div>

    <br />
</asp:Content>