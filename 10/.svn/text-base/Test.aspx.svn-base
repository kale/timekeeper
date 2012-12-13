<%@ Page Language="C#" MasterPageFile="~/10/Site.master" AutoEventWireup="true" CodeFile="Test.aspx.cs"
    Inherits="Test" Title="TEST PAGE" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <script type="text/javascript">
    function onYes() {
        var postBack = new Sys.WebForms.PostBackAction();
            postBack.set_target('YesButton.ClientID');
            postBack.set_eventArgument('');
            
            postBack.performAction();
    }
    
    function onNo() {
        //no postback necessary
        $('OutputLabel.ClientID').innerText = 'Action canceled';
    }
    
    function postbackFromJS(sender, e) {
            var postBack = new Sys.WebForms.PostBackAction();
            postBack.set_target(sender);     
            postBack.set_eventArgument(e);
            postBack.performAction();
        }
    </script>

    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
    <div id="fullview">
        <asp:Label ID="OutputLabel" runat="server"></asp:Label>
        <br /><br />
        <asp:Button ID="DeleteButton" runat="server" Text="Delete" />
        <asp:Panel ID="ConfirmtionPanel" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="modalPopup-text">
                Are you sure you want to delete this item?<br />
                <br />
                <asp:Button ID="YesButton" runat="server" Text="Yes" OnClick="YesButton_Click" />&nbsp;&nbsp;
                <asp:Button ID="NoButton" runat="server" Text="No" />
            </div>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="DeleteButton"
            PopupControlID="ConfirmtionPanel" OkControlID="YesButton" CancelControlID="NoButton"
             BackgroundCssClass="modalBackground" />
    </div>
</asp:Content>
