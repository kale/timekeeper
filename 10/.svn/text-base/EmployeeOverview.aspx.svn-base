<%@ Page Language="C#" MasterPageFile="~/10/Site.master" AutoEventWireup="true" CodeFile="EmployeeOverview.aspx.cs"
    Inherits="Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="dataentry">
        View by Day/Week/Month:<br />
        <asp:Calendar ID="ReportCalendar" runat="server" Height="180px" Width="200px" OnDayRender="ReportCalendar_DayRender"
            OnSelectionChanged="ReportCalendar_SelectionChanged"></asp:Calendar>
        <br />
        <div class="listheader">
            My Reports:</div>
        <div class="simplelist">
            <ul>
                <li>
                    <asp:LinkButton ID="AllTasksLB" runat="server" OnClick="AllTasksLB_Click">All Tasks</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="AllNonProjectsLB" runat="server" OnClick="AllNonProjectsLB_Click">All Non-Projects</asp:LinkButton></li>
            </ul>
        </div>
        By Project:<br />
        <asp:DropDownList ID="ProjectsDropDownList" runat="server" AutoPostBack="True" DataSourceID="ProjectsDropDownSqlDataSource"
            DataTextField="ProjectName" DataValueField="ProjectID" OnSelectedIndexChanged="ProjectsDropDownList_SelectedIndexChanged"
            AppendDataBoundItems="true">
            <asp:ListItem Text="(Select a Project...)" Value="None" />
        </asp:DropDownList><br />
        By Effort Type:<br />
        <asp:DropDownList ID="ServicesDropDownList" runat="server" AutoPostBack="True" DataSourceID="ServicesDataSource"
            DataTextField="Name" DataValueField="ServiceID" OnSelectedIndexChanged="ServicesDropDownList_SelectedIndexChanged"
            AppendDataBoundItems="true">
            <asp:ListItem Text="(Select an Effort...)" Value="None" />
        </asp:DropDownList>
        <br />
        <asp:Label runat="server" ID="MyEmployeeLabel">
            <div class="listheader">
                My Employee Reports:</div>
            <div class="simplelist">
                <asp:BulletedList ID="DirectReportsBulletedList" runat="server" DataSourceID="DirectReportsDataSource"
                    DataTextField="DisplayName" DataValueField="UserID" DisplayMode="LinkButton"
                    OnClick="DirectReportsBulletedList_Click">
                </asp:BulletedList>
            </div>
        </asp:Label>
        <div class="listheader">
            Other Reports:</div>
        <div class="simplelist">
            <ul>
                <li>
                    <asp:LinkButton ID="DetailedReportsLinkButton" runat="server" OnClick="DetailedReportsLinkButton_Click">Detailed Reports</asp:LinkButton></li>
            </ul>
        </div>
    </div>
    <div id="gridview">
        <div id="informationbar">
            <asp:Label ID="Output" runat="server"></asp:Label>
        </div>
        <asp:MultiView ID="MainMultiView" runat="server" Visible="true">
            <asp:View ID="ByUserView" runat="server">
                <asp:GridView ID="ByUserGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="TaskID"
                    PageSize="50" PagerSettings-PageButtonCount="10" OnSorting="GridView_Sorting">
                    <Columns>
                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" DataFormatString="{0:d}"
                            HtmlEncode="False" />
                        <asp:BoundField DataField="Time" HeaderText="Time" SortExpression="Time" />
                        <asp:BoundField DataField="CategoryName" HeaderText="Category" ReadOnly="True" SortExpression="CategoryName" />
                        <asp:BoundField DataField="ProjectName" HeaderText="Project" ReadOnly="True" SortExpression="ProjectName"
                            NullDisplayText="N/A" />
                        <asp:BoundField DataField="ServiceName" HeaderText="Effort Type" ReadOnly="True"
                            SortExpression="ServiceName" />
                        <asp:BoundField DataField="WorkDone" HeaderText="Work Done" />
                    </Columns>
                </asp:GridView>
            </asp:View>
            <asp:View ID="ByProjectView" runat="server">
                <asp:GridView ID="ByProjectGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="TaskID"
                    OnSorting="GridView_Sorting">
                    <Columns>
                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" DataFormatString="{0:d}"
                            HtmlEncode="False" />
                        <asp:BoundField DataField="Time" HeaderText="Time" SortExpression="Time" />
                        <asp:BoundField DataField="CategoryName" HeaderText="Category" ReadOnly="True" SortExpression="CategoryName" />
                        <asp:BoundField DataField="ServiceName" HeaderText="Effort Type" ReadOnly="True"
                            SortExpression="ServiceName" />
                        <asp:BoundField DataField="UserName" HeaderText="User Name" ReadOnly="True" SortExpression="UserName" />
                        <asp:BoundField DataField="WorkDone" HeaderText="Work Done" />
                    </Columns>
                </asp:GridView>
            </asp:View>
            <asp:View ID="ByServiceView" runat="server">
                <asp:GridView ID="ByServiceGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="TaskID"
                    OnSorting="GridView_Sorting">
                    <Columns>
                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" DataFormatString="{0:d}"
                            HtmlEncode="False" />
                        <asp:BoundField DataField="Time" HeaderText="Time" SortExpression="Time" />
                        <asp:BoundField DataField="CategoryName" HeaderText="Category" ReadOnly="True" SortExpression="CategoryName" />
                        <asp:BoundField DataField="ProjectName" HeaderText="Project" ReadOnly="True" SortExpression="ProjectName"
                            NullDisplayText="N/A" />
                        <asp:BoundField DataField="WorkDone" HeaderText="Work Done" />
                    </Columns>
                </asp:GridView>
            </asp:View>
            <asp:View ID="ByDetailedReports" runat="server">
                <br />
                <iframe id="iFrameView" src="http://cacsql1/Reports/Pages/Folder.aspx?ItemPath=%2fTimeKeeper&ViewMode=List"
                    runat="server" width="100%" height="100%" frameborder="0"></iframe>
            </asp:View>
        </asp:MultiView>
    </div>
    <asp:ObjectDataSource ID="TasksDataSource" runat="server" OldValuesParameterFormatString="{0}"
        SelectMethod="GetTasksByUserID" TypeName="TasksBLL"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ProjectsDataSource" runat="server" DeleteMethod="DeleteProject"
        InsertMethod="AddProject" OldValuesParameterFormatString="{0}" SelectMethod="GetProjects"
        TypeName="ProjectsBLL" UpdateMethod="UpdateProject"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="DirectReportsDataSource" runat="server" OldValuesParameterFormatString="{0}"
        SelectMethod="GetUsersByDirectReportsOf" TypeName="UsersBLL"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ServicesDataSource" runat="server" DeleteMethod="DeleteService"
        InsertMethod="AddService" OldValuesParameterFormatString="{0}" SelectMethod="GetServicesBySectionID"
        TypeName="ServicesBLL" UpdateMethod="UpdateService">
        <SelectParameters>
            <asp:SessionParameter Name="SectionID" SessionField="sectionID" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:SqlDataSource ID="ProjectsDropDownSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:TimeKeeperConnectionString %>"
        SelectCommand="SELECT DISTINCT ProjectID, (SELECT Name FROM Projects WHERE (ProjectID = Tasks.ProjectID)) AS ProjectName FROM Tasks WHERE ((UserID = @UserID) AND (ProjectID > 0)) ORDER BY ProjectName">
        <SelectParameters>
            <asp:SessionParameter Name="UserID" SessionField="userID" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
