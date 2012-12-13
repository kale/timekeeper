<%@ Page Language="C#" MasterPageFile="~/10/Site.master" AutoEventWireup="true" CodeFile="Projects.aspx.cs"
    Inherits="Projects" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="fullview">
        <h2>
            Projects</h2>
        <div id="informationbar">
            <asp:Label ID="InfoLabel" runat="server">If you need to track time to a project you
                can use the plus buttons below to add or if you no longer need it use the minus
                button.<br />
                Can't find the PMO project you were looking for?
                <asp:LinkButton ID="PullLinkButton" runat="server" OnClick="PullLinkButton_Click">You can try pulling it from the PMO Project Board.</asp:LinkButton></asp:Label>
        </div>
        <asp:Label ID="PullOutputLabel" runat="server"></asp:Label><br />
        <asp:Label ID="ImportLogLabel" runat="server"></asp:Label><br />
        <asp:ObjectDataSource ID="ProjectsDataSource" runat="server" SelectMethod="GetProjectsByUserID"
            TypeName="ProjectsBLL" OldValuesParameterFormatString="original_{0}" DeleteMethod="DeleteProject" InsertMethod="AddProject" UpdateMethod="UpdateProject">
            <SelectParameters>
                <asp:SessionParameter Name="userID" SessionField="userID" Type="Int32" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="projectID" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="projectID" Type="Int32" />
                <asp:Parameter Name="projectNumber" Type="String" />
                <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="teamsiteURL" Type="String" />
                <asp:Parameter Name="active" Type="Boolean" />
                <asp:Parameter Name="phaseID" Type="Int32" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="projectID" Type="Int32" />
                <asp:Parameter Name="projectNumber" Type="String" />
                <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="teamsiteURL" Type="String" />
                <asp:Parameter Name="active" Type="Boolean" />
                <asp:Parameter Name="phaseID" Type="Int32" />
            </InsertParameters>
        </asp:ObjectDataSource>
        <asp:GridView ID="ProjectsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="ProjectID"
            DataSourceID="ProjectsDataSource" OnRowDataBound="ProjectsGridView_RowDataBound"
            EnableTheming="True" EditRowStyle-VerticalAlign="middle">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="ProjectID" DataNavigateUrlFormatString="ProjectMembers.aspx?AddPrj={0}"
                    Text="&lt;img src='images/add.png'&gt;">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:HyperLinkField>
                <asp:BoundField DataField="ProjectNumber" HeaderText="NSR/Project Number" SortExpression="ProjectNumber" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="PhaseName" HeaderText="Current Phase" SortExpression="PhaseName" />
                <asp:BoundField DataField="ProjectManager" HeaderText="Project Manager" SortExpression="ProjectManager" />
                <asp:BoundField DataField="TeamSiteURL" HeaderText="Team Site" SortExpression="TeamSiteURL"
                    DataFormatString="&lt;a href=&quot;{0}&quot;&gt;Team Site&lt;/a&gt;" HtmlEncode="False" />
            </Columns>
            <EditRowStyle VerticalAlign="Middle" />
        </asp:GridView>
    </div>
</asp:Content>
