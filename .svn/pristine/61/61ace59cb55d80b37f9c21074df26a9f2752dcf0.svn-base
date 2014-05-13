<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" CodeBehind="Question_Org.aspx.cs" Inherits="WebApplication_Exam.Page.Question_Org" %>
<%@ Register Src="~/UserControl/Control_GridList.ascx" TagName="GridList" TagPrefix="Uc" %>
<%@ Register Src="~/UserControl/Control_Filter.ascx"  TagName="Filter" TagPrefix="Uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Title" runat="server">
    Exam | Question List
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder_ModuleTitle" ID="Content_Title">
    Question List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_Module" runat="server">
    <script type="text/javascript">
        function Grid_OnDeleteCommand(Sender, Args) {
            var Command = Args.get_commandName();
            var ItemIndex = Args.get_commandArgument();
            var AjaxPanel = $find('<%= this.QuestionGrid.pAjaxPanel.ClientID %>');
            AjaxPanel.set_enableAJAX(true);

            switch (Command) {
                case "Delete":
                    if (!window.confirm("Are you sure you want to delete this question?")) {
                        Args.set_cancel(true);
                    }
                    else {
                        AjaxPanel.set_enableAJAX(false);
                    }
                    break;
            }
        }
    </script>
    <div class="section" align="right">
        <input type="button" value="+ Add New Question" onclick="location.href = 'Question_Details.aspx';"
            style="width: 150px;" />
    </div>
    <br />
    <div>
        <Uc:Filter ID="ListFilter" runat="server" />
        <Uc:GridList ID="QuestionGrid" runat="server" />
    </div>
    <br />
    <br />
    <div style="text-align: right;">
        <asp:Panel runat="server" ID="Panel_Back">
            <input type="button" value="Go back" onclick="location.href = 'Default.aspx';" style="width: 120px;" />
        </asp:Panel>
    </div>
</asp:Content>
