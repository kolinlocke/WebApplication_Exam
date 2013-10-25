<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Details.master" CodeBehind="Question_Details.aspx.cs" Inherits="WebApplication_Exam.Page.Question_Details" ValidateRequest="false" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UserControl/Control_GridList.ascx" TagName="GridList" TagPrefix="Uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Title" runat="server">
    Exam | <%= this.Lbl_Title.Text %>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder_ModuleTitle" ID="Content_Title">
    <asp:Label runat="server" ID="Lbl_Title"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_Module" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function Grid_Command(Sender, Args) {
                var Command = Args.get_commandName();
                var ItemIndex = Args.get_commandArgument();
                var Panel = null;

                switch (Command) {
                    case "Edit":
                        {
                            Panel = $find("<%= RadAjaxPanel_AnswerWindow.ClientID %>");
                            Panel.ajaxRequest(ItemIndex);
                            break;
                        }
                    case "Delete":
                        {
                            Panel = $find("<%= RadAjaxPanel1.ClientID %>");
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
                var RadEditor = $find("<%= RadWindow1_RadEditor_Answer.ClientID %>");
                RadEditor.setSize(RadEditor.get_element().style.width, RadEditor.get_element().style.height);
                RadWindow.show();
            }

            function RadWindow_Accept() {
                RequireSave();
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
                    Panel.ajaxRequest('Dialog_Answer');
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="RadWindow1" runat="server" Width="500px" Height="460px" 
        Modal="True" Skin="Hay" OnClientClose="RadWindow_OnClientClose" Title="New Answer" Behaviors="Close, Move" KeepInScreenBounds="True">
        <ContentTemplate>
            <telerik:RadAjaxPanel ID="RadAjaxPanel_AnswerWindow" runat="server" ClientEvents-OnResponseEnd="RadAjaxPanel_AnswerWindow_AjaxEnd">
                <table>
                    <tr>
                        <td style="width: 100px">
                            Answer:
                        </td>
                        <td>
                            <telerik:RadEditor ID="RadWindow1_RadEditor_Answer" runat="server" Height="250px" Width="350px" EnableResize="False" ContentAreaMode="Div">
                                <Tools>
                                    <telerik:EditorToolGroup Tag="MainToolbar">
                                        <telerik:EditorTool Name="FindAndReplace" />
                                        <telerik:EditorSeparator />
                                        <telerik:EditorSplitButton Name="Undo">
                                        </telerik:EditorSplitButton>
                                        <telerik:EditorSplitButton Name="Redo">
                                        </telerik:EditorSplitButton>
                                        <telerik:EditorSeparator />
                                        <telerik:EditorTool Name="Cut" />
                                        <telerik:EditorTool Name="Copy" />
                                        <telerik:EditorTool Name="Paste" ShortCut="CTRL+V" />
                                    </telerik:EditorToolGroup>
                                    <telerik:EditorToolGroup Tag="Formatting">
                                        <telerik:EditorTool Name="Bold" />
                                        <telerik:EditorTool Name="Italic" />
                                        <telerik:EditorTool Name="Underline" />
                                        <telerik:EditorSeparator />
                                        <telerik:EditorSplitButton Name="ForeColor">
                                        </telerik:EditorSplitButton>
                                        <telerik:EditorSplitButton Name="BackColor">
                                        </telerik:EditorSplitButton>
                                        <telerik:EditorSeparator />
                                        <telerik:EditorDropDown Name="FontName">
                                        </telerik:EditorDropDown>
                                        <telerik:EditorDropDown Name="RealFontSize">
                                        </telerik:EditorDropDown>
                                    </telerik:EditorToolGroup>
                                </Tools>
                                <Content>
                                </Content>
                            </telerik:RadEditor>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Is this the correct Answer?
                        </td>
                        <td>
                            <asp:CheckBox ID="RadWindow1_Chk_IsAnswer" runat="server"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td title='The choices will be in random order during the test. If this is checked, it will be fixed in its current slot.'>
                            Is this choice has<br />a fixed location?
                        </td>
                        <td>
                            <asp:CheckBox ID="RadWindow1_Chk_IsFixed" runat="server"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Order Index:
                        </td>
                        <td>
                            <asp:TextBox ID="RadWindow1_Txt_OrderIndex" runat="server" Width="40px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:Button ID="RadWindow1_Btn_Ok" runat="server" Text="Ok" Width="80px" OnClientClick="RadWindow_Accept(); return false;" />
                            <asp:Button ID="RadWindow1_Btn_Cancel" runat="server" Text="Cancel" Width="80px" OnClientClick="RadWindow_Close(); return false;" />
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="RadWindow1_Hid_TmpKey" runat="server"></asp:HiddenField>
            </telerik:RadAjaxPanel>
        </ContentTemplate>
    </telerik:RadWindow>
    <div>
        <fieldset>
            <legend>
                Question Details 
            </legend>
            <table style="width: 100%">
                <tr>
                    <td style="width: 100px;">
                        Question:
                    </td>
                    <td>
                        <telerik:RadEditor ID="RadEditor_Question" runat="server" Height="200px" Width="500px" EnableResize="False" ContentAreaMode="Div">
                            <Tools>
                                <telerik:EditorToolGroup Tag="MainToolbar">
                                    <telerik:EditorTool Name="FindAndReplace" />
                                    <telerik:EditorSeparator />
                                    <telerik:EditorSplitButton Name="Undo">
                                    </telerik:EditorSplitButton>
                                    <telerik:EditorSplitButton Name="Redo">
                                    </telerik:EditorSplitButton>
                                    <telerik:EditorSeparator />
                                    <telerik:EditorTool Name="Cut" />
                                    <telerik:EditorTool Name="Copy" />
                                    <telerik:EditorTool Name="Paste" ShortCut="CTRL+V" />
                                </telerik:EditorToolGroup>
                                <telerik:EditorToolGroup Tag="Formatting">
                                    <telerik:EditorTool Name="Bold" />
                                    <telerik:EditorTool Name="Italic" />
                                    <telerik:EditorTool Name="Underline" />
                                    <telerik:EditorSeparator />
                                    <telerik:EditorSplitButton Name="ForeColor">
                                    </telerik:EditorSplitButton>
                                    <telerik:EditorSplitButton Name="BackColor">
                                    </telerik:EditorSplitButton>
                                    <telerik:EditorSeparator />
                                    <telerik:EditorDropDown Name="FontName">
                                    </telerik:EditorDropDown>
                                    <telerik:EditorDropDown Name="RealFontSize">
                                    </telerik:EditorDropDown>
                                </telerik:EditorToolGroup>
                            </Tools>
                            <Content>
                            </Content>
                        </telerik:RadEditor>
                    </td>
                </tr>
                <tr>
                    <td>
                        Category:
                    </td>
                    <td>
                        <asp:DropDownList ID="Cbo_Category" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Type:
                    </td>
                    <td>
                        <asp:DropDownList ID="Cbo_QuestionType" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <%--
                <tr>
                    <td colspan ="2">
                        <asp:CheckBox ID="Chk_IsMultipleAnswer" runat="server" Text="Has Multiple Answers?" />
                    </td>
                </tr>
                --%>
            </table>
        </fieldset>
        <br />
        <table style="width: 100%">
            <tr>
                <td align="right">
                    <asp:Button ID="Btn_New" runat="server" Text="+ New Choice" />
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%">
                        <Uc:GridList ID="Grid_Answers" runat="server" />
                    </telerik:RadAjaxPanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="ContentPlaceHolder_Button">
    <asp:Button ID="Btn_Approve" runat="server" Text="Approve" Width="80px" />
</asp:Content>

