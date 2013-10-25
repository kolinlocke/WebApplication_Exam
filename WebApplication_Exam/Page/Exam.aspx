<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" CodeBehind="Exam.aspx.cs" Inherits="WebApplication_Exam.Page.Exam" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Title" runat="server">
    <%= this.mTitle %>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder_ModuleTitle" ID="Content_Title">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_Module" runat="server">
    <div>
        <asp:Panel runat="server" ID="Panel_Title" Visible="False">
            <h2 class="ClsExamTitle">
                Exam Report of 
                <asp:Label runat="server" ID="Lbl_Applicant">
                </asp:Label>
            </h2>
            <span class="ClsExamTitle">
                Taken on 
                <asp:Label runat="server" ID="Lbl_DateTaken">
                </asp:Label>
            </span>
            <br />
            <br />
        </asp:Panel>
    </div>
    <div class="panel">
        <asp:Panel runat="server" ID="Panel_Questions">
        </asp:Panel>
    </div>
    <div class="panel">
        <asp:Label runat="server" ID="Lbl_Page">
        </asp:Label>
        <br />
        <table>
            <tr>
                <td>
                    <asp:LinkButton runat="server" ID="Btn_First">First</asp:LinkButton>
                    &nbsp;
                    <asp:LinkButton runat="server" ID="Btn_Previous">Previous</asp:LinkButton>
                    &nbsp;
                    <asp:LinkButton runat="server" ID="Btn_Next">Next</asp:LinkButton>
                    &nbsp;
                    <asp:LinkButton runat="server" ID="Btn_Last">Last</asp:LinkButton>
                </td>
                <td align="right" width="100px">
                    <asp:DropDownList runat="server" ID="Cbo_Page" AutoPostBack="True" Width="80px">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <asp:Button runat="server" Text="Submit" ID="Btn_Submit" Width="80px" />
        <br />
        <asp:Panel ID="Panel_Report" runat="server" Visible="False">
            <asp:Button runat="server" Text="Export" ID="Btn_Export" Width="160px" />
            <br />
            <asp:Button runat="server" Text="Back To Exam Reports" ID="Btn_BackToExamReport" Width="160px" />
        </asp:Panel>
    </div>
</asp:Content>
