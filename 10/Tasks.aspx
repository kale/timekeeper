<%@ Page Language="C#" MasterPageFile="~/10/Site.master" AutoEventWireup="true" CodeFile="Tasks.aspx.cs" Inherits="Tasks" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls" TagPrefix="BDP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true" />
    <div id="dataentry">
        <asp:UpdatePanel runat="server" ID="DataEntryPanel">
            <ContentTemplate>
                <asp:ValidationSummary ID="ValidationSummary" runat="server" DisplayMode="List" HeaderText="<b>Errors on the form, please make sure you...</b>"
                    ValidationGroup="NewTask" Width="250px" CssClass="FormError" ForeColor="#000000" />
                <br />
                Date:<br />
                <span style="vertical-align: middle;">
                    <BDP:BDPLite ID="DateTextBox" runat="server" DateFormat="d" />
                    <asp:RequiredFieldValidator ID="DateRequiredFieldValidator" runat="server" ControlToValidate="DateTextBox"
                        ErrorMessage="Enter a date" ValidationGroup="NewTask">*</asp:RequiredFieldValidator><br />
                </span>Time spent on task:<br />
                <span style="vertical-align: middle;">
                    <asp:TextBox ID="TimeTextBox" runat="server" Width="2em"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="TimeRequiredRegularExpressionValidator" runat="server"
                        ControlToValidate="TimeTextBox" ValidationExpression="\d*\.?(?:25|5[0]?|75|00)?"
                        ErrorMessage="Enter a valid time format" Display="dynamic" ValidationGroup="NewTask">*</asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="TimeRequiredFieldValidator" runat="server" ControlToValidate="TimeTextBox"
                        ErrorMessage="Enter a time value" Display="dynamic" ValidationGroup="NewTask">*</asp:RequiredFieldValidator>
                </span>
                <br />
                Description of work done:<br />
                <span style="vertical-align: top; white-space: nowrap">
                    <asp:TextBox ID="WorkDoneTextBox" runat="server" Height="60px" TextMode="MultiLine"
                        Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="WorkDoneRequiredFieldValidator" runat="server" ControlToValidate="WorkDoneTextBox"
                        ErrorMessage="Enter the work done you completed" ValidationGroup="NewTask">*</asp:RequiredFieldValidator>
                </span><span style="vertical-align: middle; white-space: nowrap">
                    <br />
                    Category:<br />
                    <asp:DropDownList ID="CategoryDropDown" runat="server" DataSourceID="CategoriesDataSource"
                        DataTextField="Name" DataValueField="CategoryID" OnSelectedIndexChanged="CategoryDropDown_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <br />
                    <asp:ObjectDataSource ID="CategoriesDataSource" runat="server" OldValuesParameterFormatString="{0}"
                        SelectMethod="GetCategories" TypeName="CategoriesBLL"></asp:ObjectDataSource>
                    Project:<br />
                    <asp:DropDownList ID="ProjectDropDownList" runat="server" DataSourceID="ProjectMembersDataSource"
                        DataTextField="ProjectName" DataValueField="ProjectID" OnDataBound="ProjectDropDownList_DataBound"
                        AppendDataBoundItems="true" OnSelectedIndexChanged="ProjectDropDownList_SelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Text="(Select a Project...)" Value="None" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="ProjectRequiredFieldValidator" runat="server" ControlToValidate="ProjectDropDownList"
                        ErrorMessage="Enter a project if you select the Project category" ValidationGroup="NewTask"
                        InitialValue="None">*</asp:RequiredFieldValidator>
                    <br />
                    Phase:<br />
                    <asp:DropDownList ID="PhaseDropDown" runat="server" DataSourceID="PhasesDataSource"
                        DataTextField="Name" DataValueField="PhaseID" AppendDataBoundItems="true">
                        <asp:ListItem Text="(Select a Phase...)" Value="None" />
                    </asp:DropDownList><asp:RequiredFieldValidator ID="PhaseRequiredFieldValidator" runat="server"
                        ControlToValidate="PhaseDropDown" ErrorMessage="Enter a phase if you select a project"
                        ValidationGroup="NewTask" InitialValue="None">*</asp:RequiredFieldValidator>
                    <asp:ObjectDataSource ID="PhasesDataSource" runat="server" OldValuesParameterFormatString="{0}"
                        SelectMethod="GetPhases" TypeName="PhasesBLL"></asp:ObjectDataSource>
                    <br />
                    Effort Type:<br />
                    <asp:DropDownList ID="ServiceDropDownList" runat="server" DataSourceID="ServicesDataSource"
                        DataTextField="Name" DataValueField="ServiceID">
                        <asp:ListItem Text="(Select an Effort Type...)" Value="None" />
                    </asp:DropDownList>
                    <br />
                    Asset:<br />
                    <asp:DropDownList ID="AssetDropDown" runat="server" DataSourceID="AssetsDataSource"
                        DataTextField="Name" DataValueField="AssetID" AppendDataBoundItems="true">
                        <asp:ListItem Text="(Select an Asset...)" Value="None" />
                    </asp:DropDownList><asp:ObjectDataSource ID="AssetsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetAssets" TypeName="AssetsBLL"></asp:ObjectDataSource>
                    <br />
                    WO#:<br />
                    <asp:TextBox ID="WOTextBox" runat="server" Width="75px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="WOTextBox"
                        ValidationExpression="\d*" ErrorMessage="Incorrect WO format" Display="dynamic"
                        ValidationGroup="NewTask">*</asp:RegularExpressionValidator>
                    <br />
                    RFC#:<br />
                    <asp:TextBox ID="RFCTextBox" runat="server" Width="75px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="RFCTextBox"
                        ValidationExpression="\d*" ErrorMessage="Incorrect RFC format" Display="dynamic"
                        ValidationGroup="NewTask">*</asp:RegularExpressionValidator>
                </span>
                <br />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="CategoryDropDown" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ProjectDropDownList" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="SubmitButton_Click"
            ValidationGroup="NewTask" />
    </div>
    <div id="gridview">
        <h2>
            Tasks</h2>
        <asp:UpdatePanel ID="InfobarUpdatePanel" runat="server">
            <ContentTemplate>
                <asp:Panel runat="server" ID="ViewPanel" Width="237px">
                    <span style="vertical-align: bottom;">View: &nbsp;
                        <asp:LinkButton ID="TodayLB" runat="server" OnClick="TodayLB_Click">Today</asp:LinkButton>
                        |
                        <asp:LinkButton ID="YesterdayLB" runat="server" OnClick="YesterdayLB_Click">Yesterday</asp:LinkButton>
                        |
                        <asp:LinkButton ID="PickDayLB" runat="server" OnClick="PickDayLB_Click">By Date</asp:LinkButton>
                        |
                        <asp:LinkButton ID="AllTasksLB" runat="server" OnClick="AllTasksLB_Click">All Tasks</asp:LinkButton></span></asp:Panel>
                <br />
                <div id="informationbar">
                    <asp:Label ID="StatsLabel" runat="server" BorderStyle="None"></asp:Label>
                </div>
                <div id="errorbar">
                    <asp:Label ID="ExceptionDetails" runat="server" BorderStyle="None" Visible="false"
                        EnableViewState="false"></asp:Label></div>
                <div id="timebar">
                    <asp:ImageButton ID="timebarIB" runat="server" Enabled="false" /></div>
                <asp:Panel ID="PickDayPanel" runat="server">
                    <asp:Calendar ID="PickDayCalendar" runat="server" OnSelectionChanged="PickDayCalendar_SelectionChanged"
                        OnDayRender="PickDayCalendar_DayRender"></asp:Calendar>
                    <br />
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="SubmitButton" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanelGridView" runat="server">
            <ContentTemplate>
                <asp:GridView ID="TasksGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="TaskID"
                    DataSourceID="TasksDataSource" AllowSorting="True" BorderWidth="0px" BorderStyle="None"
                    Width="100%" CellPadding="2" PageSize="25" AllowPaging="True" OnRowUpdated="TasksGridView_RowUpdated"
                    OnSelectedIndexChanged="TasksGridView_SelectedIndexChanged">
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="TaskID" DataNavigateUrlFormatString="EditTask.aspx?t={0}"
                            Text="View/Edit">
                            <ItemStyle Width="75px" />
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="TaskID" Visible="False"></asp:BoundField>
                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" DataFormatString="{0:d}"
                            HtmlEncode="False">
                            <ItemStyle Width="75px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Time" HeaderText="Time" SortExpression="Time" DataFormatString="{0} hr"
                            HtmlEncode="False">
                            <ItemStyle Width="75px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CategoryName" HeaderText="Category" SortExpression="CategoryName">
                            <ItemStyle Width="75px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ProjectName" HeaderText="Project" SortExpression="ProjectName"
                            NullDisplayText="N/A">
                            <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ServiceName" HeaderText="Effort" ReadOnly="True" SortExpression="ServiceName">
                            <ItemStyle Width="75px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AssetName" HeaderText="Asset" NullDisplayText="N/A" SortExpression="AssetName">
                            <ItemStyle Width="75px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="WorkDone" HeaderText="Work Done" SortExpression="WorkDone">
                            <ItemStyle Width="300px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                <asp:Label ID="NotesLabel" runat="server" Width="99%" Height="95%"></asp:Label>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="SubmitButton" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="TodayLB" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="YesterdayLB" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="AllTasksLB" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <asp:ObjectDataSource ID="ProjectMembersDataSource" runat="server" OldValuesParameterFormatString="{0}"
        SelectMethod="GetProjectMembersByUserID" TypeName="ProjectMembersBLL">
        <SelectParameters>
            <asp:SessionParameter Name="userID" SessionField="userID" Size="4" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ServicesDataSource" runat="server" OldValuesParameterFormatString="{0}"
        SelectMethod="GetServicesBySectionID" TypeName="ServicesBLL">
        <SelectParameters>
            <asp:SessionParameter Name="sectionID" SessionField="sectionID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="TasksDataSource" runat="server" InsertMethod="AddTask"
        SelectMethod="GetTasksByUserID" TypeName="TasksBLL">
        <SelectParameters>
            <asp:SessionParameter Name="userID" SessionField="userID" Size="4" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="date" Type="DateTime" />
            <asp:Parameter Name="time" Type="Decimal" />
            <asp:Parameter Name="workDone" Type="String" />
            <asp:Parameter Name="notes" Type="String" />
            <asp:Parameter Name="serviceID" Type="Int32" />
            <asp:Parameter Name="projectID" Type="Int32" />
            <asp:Parameter Name="userID" Type="Int32" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
