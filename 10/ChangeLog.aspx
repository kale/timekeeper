<%@ Page Language="C#" MasterPageFile="~/10/Site.master" AutoEventWireup="true" CodeFile="ChangeLog.aspx.cs"
	Inherits="ChangeLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
	<div id="fullview">
		<h2>
			ChangeLog</h2>
		<span>
			<asp:Button ID="UpdateButton" runat="server" OnClick="UpdateButton_Click" Text="Update" />
			<asp:Button ID="SaveButton" runat="server" OnClick="SaveButton_Click" Text="Save" />
			<br />
			<asp:TextBox ID="ChangeLogTextBox" runat="server" TextMode="MultiLine" Width="98%"
				Rows="40" ReadOnly="true"></asp:TextBox>
		</span>
	</div>
</asp:Content>
