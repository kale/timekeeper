<%@ Page Language="C#" MasterPageFile="~/10/Site.master" AutoEventWireup="true" CodeFile="ProjectMembers.aspx.cs"
    Inherits="ProjectMembers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="dataentry">
        Project:<br />
        <asp:DropDownList ID="ProjectNameDropDownList" runat="server" DataSourceID="ProjectsDataSource"
            DataTextField="Name" DataValueField="ProjectID">
        </asp:DropDownList><br />
        User:<br />
        <asp:DropDownList ID="UserNameDropDownList" runat="server" DataSourceID="UsersDataSource"
            DataTextField="DisplayName" DataValueField="UserID">
        </asp:DropDownList><br />
        Position:<br />
        <asp:TextBox ID="PositionTextBox" runat="server"></asp:TextBox><br />
        <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="SubmitButton_Click" /><br />
        <br />
        <asp:TextBox ID="MessageTextBox" runat="server" BorderStyle="None" ReadOnly="True"
            BackColor="Transparent" Width="100%" Height="50px"></asp:TextBox></div>
    <div id="gridview">
        <h2>
            Project Members</h2>
        <asp:GridView ID="ProjectMembersGridView" runat="server" AutoGenerateColumns="False"
            DataKeyNames="ProjectMemberID" DataSourceID="ProjectMembersDataSource">
            <Columns>
                <asp:CommandField ShowEditButton="True" HeaderText="" ButtonType="Image" EditImageUrl="images/icon-edit.gif"
                    UpdateImageUrl="images/icon-save.gif" CancelImageUrl="images/icon-cancel.gif"
                    ItemStyle-Width="10px" />
                <asp:CommandField ShowDeleteButton="True" HeaderText="" DeleteImageUrl="images/icon-delete.gif"
                    ButtonType="Image" ItemStyle-Width="35px" />
                <asp:BoundField DataField="UserName" HeaderText="User Name" ReadOnly="True" SortExpression="UserName" />
                <asp:BoundField DataField="ProjectName" HeaderText="Project Name" ReadOnly="True"
                    SortExpression="ProjectName" />
                <asp:BoundField DataField="Position" HeaderText="Position" SortExpression="Position" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ProjectMembersDataSource" runat="server" DeleteMethod="DeleteProjectMember"
            InsertMethod="AddProjectMember" SelectMethod="GetProjectMembers" TypeName="ProjectMembersBLL"
            UpdateMethod="UpdateProjectMember">
            <DeleteParameters>
                <asp:Parameter Name="projectMemberID" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="projectID" Type="Int32" />
                <asp:Parameter Name="userID" Type="Int32" />
                <asp:Parameter Name="position" Type="String" />
                <asp:Parameter Name="projectMemberID" Type="Int32" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="projectID" Type="Int32" />
                <asp:Parameter Name="userID" Type="Int32" />
                <asp:Parameter Name="position" Type="String" />
            </InsertParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ProjectsDataSource" runat="server" DeleteMethod="DeleteProject"
            InsertMethod="AddProject" OldValuesParameterFormatString="original_{0}" SelectMethod="GetProjects"
            TypeName="ProjectsBLL" UpdateMethod="UpdateProject">
            <DeleteParameters>
                <asp:Parameter Name="projectID" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="teamsiteURL" Type="String" />
                <asp:Parameter Name="estimatedTime" Type="Decimal" />
                <asp:Parameter Name="active" Type="Boolean" />
                <asp:Parameter Name="projectID" Type="Int32" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="teamsiteURL" Type="String" />
                <asp:Parameter Name="estimatedTime" Type="Decimal" />
                <asp:Parameter Name="active" Type="Boolean" />
            </InsertParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="UsersDataSource" runat="server" DeleteMethod="Delete" InsertMethod="Insert"
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetUsers" TypeName="TimeKeeperTableAdapters.UsersTableAdapter"
            UpdateMethod="Update">
            <DeleteParameters>
                <asp:Parameter Name="Original_UserID" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="AccountName" Type="String" />
                <asp:Parameter Name="DisplayName" Type="String" />
                <asp:Parameter Name="Email" Type="String" />
                <asp:Parameter Name="DefaultView" Type="Int32" />
                <asp:Parameter Name="Role" Type="String" />
                <asp:Parameter Name="Active" Type="Boolean" />
                <asp:Parameter Name="Original_UserID" Type="Int32" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="AccountName" Type="String" />
                <asp:Parameter Name="DisplayName" Type="String" />
                <asp:Parameter Name="Email" Type="String" />
                <asp:Parameter Name="DefaultView" Type="Int32" />
                <asp:Parameter Name="Role" Type="String" />
                <asp:Parameter Name="Active" Type="Boolean" />
            </InsertParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>
