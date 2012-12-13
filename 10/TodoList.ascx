<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TodoList.ascx.cs" Inherits="TodoList" %>
<asp:GridView ID="TodosGridView" runat="server" DataSourceID="TodosDataSource" AutoGenerateColumns="False"
    DataKeyNames="TodoID">
    <Columns>
        <asp:CheckBoxField DataField="Complete" HeaderText="" ReadOnly="False">
            <ItemStyle Width="5px" />
        </asp:CheckBoxField>
        <asp:BoundField DataField="TodoItem" HeaderText="Item" ReadOnly="True" SortExpression="TodoItem">
            <ItemStyle Width="200px" />
        </asp:BoundField>
        <asp:BoundField DataField="DueDate" HeaderText="Due Date" ReadOnly="True" SortExpression="DueDate"
            DataFormatString="{0:d}" HtmlEncode="false"></asp:BoundField>
    </Columns>
</asp:GridView>
<span style="vertical-align: bottom;">Item:
    <asp:TextBox ID="TodoItemTextBox" runat="server" Width="500px"></asp:TextBox>
    Due Date:
    <asp:TextBox ID="DueDateTextBox" runat="server" Width="65px"></asp:TextBox>
    Project:
    <asp:DropDownList ID="ProjectsDropDownList" runat="server" DataSourceID="ProjectMembersDataSource" DataTextField="ProjectName" DataValueField="ProjectID">
    </asp:DropDownList>&nbsp;
    <asp:LinkButton ID="SubmitLinkButton" runat="server" OnClick="SubmitLinkButton_Click">Submit</asp:LinkButton></span><br />
<asp:Label ID="EntryViewLabel" runat="server" Text="Label"></asp:Label>
<asp:ObjectDataSource ID="TodosDataSource" runat="server" SelectMethod="GetTodos"
    UpdateMethod="UpdateTodo" TypeName="TodosBLL"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="ProjectMembersDataSource" runat="server" SelectMethod="GetProjectMembersByUserID"
    TypeName="ProjectMembersBLL">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="" Name="userID" SessionField="userID" Size="4"
            Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
