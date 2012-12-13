<%@ Page Language="C#" MasterPageFile="~/10/Site.master" AutoEventWireup="true" CodeFile="MyEmployees.aspx.cs"
    Inherits="Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="dataentry">
        <div class="listheader">
            Select Employee:</div>
        <asp:DropDownList ID="DirectReportsDropDown" runat="server" DataSourceID="DirectReportsDataSource"
            DataTextField="DisplayName" DataValueField="UserID" AppendDataBoundItems="true"
            AutoPostBack="true" OnSelectedIndexChanged="DirectReportsDropDown_SelectedIndexChanged">
            <asp:ListItem Text="All Users" Value="AllUsers"></asp:ListItem>
        </asp:DropDownList><br />
        <br />
        View by Day/Week/Month:<br />
        <asp:Calendar ID="ReportCalendar" runat="server" Height="180px" Width="200px" OnDayRender="ReportCalendar_DayRender"
            OnSelectionChanged="ReportCalendar_SelectionChanged"></asp:Calendar>
        <br />
        <div class="listheader">
            Other Views:</div>
        <div class="simplelist">
            <ul>
                <li>
                    <asp:LinkButton ID="AllTasksLB" runat="server" OnClick="AllTasksLB_Click">All Tasks</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="AllNonProjectsLB" runat="server" OnClick="AllNonProjectsLB_Click">All Non-Projects</asp:LinkButton></li>
            </ul>
        </div>
    </div>
    <div id="gridview">
        <div id="informationbar">
            <asp:Label ID="Output" runat="server"></asp:Label>
        </div>
        <asp:GridView ID="ByUserGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="TaskID"
            PageSize="50" PagerSettings-PageButtonCount="10" OnSorting="ByUserGridView_Sorting">
            <Columns>
                <asp:BoundField DataField="UserName" HeaderText="Name" ReadOnly="True" SortExpression="UserName" />
                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" DataFormatString="{0:d}"
                    HtmlEncode="False" />
                <asp:BoundField DataField="Time" HeaderText="Time" SortExpression="Time" />
                <asp:BoundField DataField="CategoryName" HeaderText="Category" ReadOnly="True" SortExpression="CategoryName" />
                <asp:BoundField DataField="ProjectName" HeaderText="Project" ReadOnly="True" SortExpression="ProjectName" />
                <asp:BoundField DataField="PhaseName" HeaderText="Phase" ReadOnly="True" SortExpression="PhaseName" />
                <asp:BoundField DataField="ServiceName" HeaderText="Effort" ReadOnly="True" SortExpression="ServiceName" />
                <asp:BoundField DataField="WONum" HeaderText="WO" SortExpression="WONum" />
                <asp:BoundField DataField="RFCNum" HeaderText="RFC" SortExpression="RFCNum" />
                <asp:BoundField DataField="AssetName" HeaderText="Asset" ReadOnly="True" SortExpression="AssetName" />
                <asp:BoundField DataField="WorkDone" HeaderText="Work Done" SortExpression="WorkDone" />
            </Columns>
        </asp:GridView>
        &nbsp;
    </div>
    <asp:ObjectDataSource ID="DirectReportsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetUsersByDirectReportsOf" TypeName="UsersBLL">
        <SelectParameters>
            <asp:SessionParameter Name="userID" SessionField="userID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
