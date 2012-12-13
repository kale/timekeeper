<%@ Page Language="C#" MasterPageFile="~/10/Site.master" AutoEventWireup="true" CodeFile="Watch.aspx.cs" Inherits="Spy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:GridView ID="SpyGridView" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="TaskID" DataSourceID="ObjectDataSource1" PageSize="250">
        <Columns>
            <asp:BoundField DataField="TaskID" HeaderText="TaskID" InsertVisible="False" ReadOnly="True"
                SortExpression="TaskID" />
            <asp:BoundField DataField="UserName" HeaderText="User" ReadOnly="True" SortExpression="UserName" ItemStyle-Width="100px" />
            <asp:BoundField DataField="Date" DataFormatString="{0:d}" HeaderText="Date" HtmlEncode="False"
                SortExpression="Date" ItemStyle-Width="75px" />
            <asp:BoundField DataField="Time" HeaderText="Time" SortExpression="Time" ItemStyle-Width="50px" />
            <asp:BoundField DataField="WorkDone" HeaderText="Work Done" SortExpression="WorkDone" />
            <asp:BoundField DataField="CategoryName" HeaderText="Category" ReadOnly="True" SortExpression="CategoryName" />
            <asp:BoundField DataField="ServiceName" HeaderText="Service" ReadOnly="True" SortExpression="ServiceName" />
            <asp:BoundField DataField="ProjectName" HeaderText="Project" ReadOnly="True" SortExpression="ProjectName" />
            <asp:BoundField DataField="WONum" HeaderText="WO#" SortExpression="WONum" />
            <asp:BoundField DataField="RFCNum" HeaderText="RFC#" SortExpression="RFCNum" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTasks" TypeName="TasksBLL"></asp:ObjectDataSource>
</asp:Content>

