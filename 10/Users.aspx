<%@ Page Language="C#" MasterPageFile="~/10/Site.master" AutoEventWireup="true" CodeFile="Users.aspx.cs"
    Inherits="Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="dataentry">
        Account Name (CAC\username):<br />
        <asp:TextBox ID="AccountNameTextBox" runat="server"></asp:TextBox><br />
        Display Name:<br />
        <asp:TextBox ID="DisplayNameTextBox" runat="server"></asp:TextBox><br />
        E-mail Address:<br />
        <asp:TextBox ID="EmailTextBox" runat="server"></asp:TextBox><br />
        Default View:<br />
        <asp:DropDownList ID="DefaultViewDropDownList" runat="server">
            <asp:ListItem Selected="True" Value="0">By Today</asp:ListItem>
            <asp:ListItem Value="1">By Project</asp:ListItem>
            <asp:ListItem Value="2">All Tasks</asp:ListItem>
        </asp:DropDownList><br />
        Role:<br />
        <asp:CheckBoxList ID="RoleList" runat="server">
        </asp:CheckBoxList><br />
        Manager:<br />
        <asp:DropDownList ID="ManagerIDDropDownList" runat="server" DataSourceID="UsersDataSource"
            DataTextField="DisplayName" DataValueField="UserID">
        </asp:DropDownList><br />
        Section:<br />
        <asp:DropDownList ID="SectionIDDropDownList" runat="server" DataSourceID="SectionsDataSource"
            DataTextField="Name" DataValueField="SectionID">
        </asp:DropDownList><br />
        Active:<br />
        <asp:CheckBox ID="ActiveCheckBox" runat="server" Checked="true" /><br />
        <asp:Button ID="SubmitButton" runat="server" CausesValidation="True" CommandName="Insert"
            Text="Submit" OnClick="SubmitButton_Click"></asp:Button>
        <br />
    </div>
    <div id="gridview">
        <h2>
            Users</h2>
        <div id="informationbar">
            <asp:Label ID="InfoBarLabel" runat="server" BorderStyle="None"></asp:Label>
        </div>
        <br />
        <asp:GridView ID="UsersGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID"
            DataSourceID="UsersDataSource" OnRowDataBound="UsersGridView_RowDataBound" OnRowCommand="UsersGridView_RowCommand">
            <Columns>
                <asp:CommandField ShowEditButton="True" ButtonType="Image" EditImageUrl="images/icon-edit.gif"
                    UpdateImageUrl="images/icon-save.gif" CancelImageUrl="images/icon-cancel.gif">
                    <ItemStyle Width="10px" />
                </asp:CommandField>
                <asp:CommandField ShowDeleteButton="True" DeleteImageUrl="images/icon-delete.gif"
                    ButtonType="Image">
                    <ItemStyle Width="35px" />
                </asp:CommandField>
                <asp:BoundField DataField="UserID" HeaderText="UserID" SortExpression="UserID" />
                <asp:BoundField DataField="DisplayName" HeaderText="Name" SortExpression="DisplayName" />
                <asp:BoundField DataField="AccountName" HeaderText="Account Name" SortExpression="AccountName" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
                <asp:BoundField DataField="ManagerID" HeaderText="ManagerID" SortExpression="ManagerID" />
                <asp:BoundField DataField="SectionID" HeaderText="SectionID" SortExpression="SectionID" />
                <asp:ButtonField CommandName="AddManagerRole" HeaderText="Manager?" ButtonType="Image"
                    ImageUrl="~/10/images/add.png" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="UsersDataSource" runat="server" DeleteMethod="DeleteUser"
            InsertMethod="AddUser" OldValuesParameterFormatString="{0}" SelectMethod="GetUsers"
            TypeName="UsersBLL" UpdateMethod="UpdateUser">
            <DeleteParameters>
                <asp:Parameter Name="userID" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="accountName" Type="String" />
                <asp:Parameter Name="displayName" Type="String" />
                <asp:Parameter Name="email" Type="String" />
                <asp:Parameter Name="defaultView" Type="Int32" />
                <asp:Parameter Name="role" Type="String" />
                <asp:Parameter Name="active" Type="Boolean" />
                <asp:Parameter Name="managerID" Type="Int32" />
                <asp:Parameter Name="sectionID" Type="Int32" />
                <asp:Parameter Name="userID" Type="Int32" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="accountName" Type="String" />
                <asp:Parameter Name="displayName" Type="String" />
                <asp:Parameter Name="email" Type="String" />
                <asp:Parameter Name="defaultView" Type="Int32" />
                <asp:Parameter Name="role" Type="String" />
                <asp:Parameter Name="active" Type="Boolean" />
                <asp:Parameter Name="managerID" Type="Int32" />
                <asp:Parameter Name="sectionID" Type="Int32" />
            </InsertParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="SectionsDataSource" runat="server" SelectMethod="GetSections"
            TypeName="SectionsBLL"></asp:ObjectDataSource>
    </div>
</asp:Content>
