<%@ Page Language="C#" MasterPageFile="~/10/Site.master" AutoEventWireup="true" CodeFile="ModalPopup.aspx.cs"
    Inherits="ModalPopup_ModalPopup" Title="ModalPopup Sample" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
    <div id="fullview">
        <h2>
            Effort Types for
            <asp:Label ID="SectionLabel" runat="server"></asp:Label></h2>
        <div class="demoarea">
            <br />
            <asp:LinkButton ID="LinkButton1" runat="server" Text="Click here to add an Effort type"></asp:LinkButton>

            <script type="text/javascript">
                function onYes() {
                    var postBack = new Sys.WebForms.PostBackAction();
                        postBack.set_target('OkButton');
                        postBack.set_eventArgument('');
                        
                        postBack.performAction();
                }
    
                function onNo() {
                    //no postback necessary
                    //$('SectionLabel').innerText = 'Action canceled';
                }
            </script>

            <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Style="display: none">
                <h3>
                    Add Effort Type</h3>
                Name:<br />
                <span style="vertical-align: middle;">
                    <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>'>
                    </asp:TextBox><asp:RequiredFieldValidator ID="NameRequiredFieldValidator" runat="server"
                        ErrorMessage="RequiredFieldValidator" ControlToValidate="NameTextBox" ValidationGroup="NewService"> *</asp:RequiredFieldValidator></span><br />
                Description:<br />
                <span style="vertical-align: middle;">
                    <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>'
                        Height="100px" TextMode="MultiLine" Width="200px">
                    </asp:TextBox><asp:RequiredFieldValidator ID="DescriptionRequiredFieldValidator"
                        runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="DescriptionTextBox"
                        ValidationGroup="NewService"> *</asp:RequiredFieldValidator></span><br />
                Examples:<br />
                <span style="vertical-align: middle;">
                    <asp:TextBox ID="ExamplesTextBox" runat="server" Text='<%# Bind("Examples") %>' Height="100px"
                        TextMode="MultiLine" Width="200px">
                    </asp:TextBox><asp:RequiredFieldValidator ID="ExamplesRequiredFieldValidator" runat="server"
                        ErrorMessage="RequiredFieldValidator" ControlToValidate="ExamplesTextBox" ValidationGroup="NewService"> *</asp:RequiredFieldValidator></span><br />

                <p style="text-align: center;">
                <asp:Button ID="OkButton" runat="server" CausesValidation="True" CommandName="Insert"
                    Text="Create" ValidationGroup="NewService"></asp:Button>
                <asp:Button ID="CancelButton" runat="server" Text="Cancel"></asp:Button>
                </p>
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="LinkButton1"
                PopupControlID="Panel1" BackgroundCssClass="modalBackground" OkControlID="OkButton"
                OnOkScript="onOk()" CancelControlID="CancelButton" OnCancelScript="onNo()" />
        </div>
        <p>
            Please note that core effort types are in <strong>bold</strong>.&nbsp; These effort
            types are common to all sections.</p>
        <asp:GridView ID="ServicesGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="ServiceID"
            DataSourceID="ServicesDataSource" OnRowDataBound="ServicesGridView_RowDataBound">
            <Columns>
                <asp:CommandField ShowEditButton="True" ButtonType="Image" EditImageUrl="images/icon-edit.gif"
                    UpdateImageUrl="images/icon-save.gif" CancelImageUrl="images/icon-cancel.gif">
                    <ItemStyle Width="10px" />
                </asp:CommandField>
                <asp:CommandField ShowDeleteButton="True" DeleteImageUrl="images/icon-delete.gif"
                    ButtonType="Image">
                    <ItemStyle Width="35px" />
                </asp:CommandField>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ItemStyle-Width="150px" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                <asp:BoundField DataField="Examples" HeaderText="Examples" SortExpression="Examples" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ServicesDataSource" runat="server" InsertMethod="AddService"
            SelectMethod="GetServicesBySectionID" TypeName="ServicesBLL" DeleteMethod="DeleteService"
            UpdateMethod="UpdateService">
            <InsertParameters>
                <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="description" Type="String" />
                <asp:Parameter Name="examples" Type="String" />
                <asp:SessionParameter Name="sectionID" SessionField="sectionID" Type="Int32" />
            </InsertParameters>
            <SelectParameters>
                <asp:SessionParameter Name="sectionID" SessionField="sectionID" Type="Int32" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="serviceID" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="description" Type="String" />
                <asp:Parameter Name="examples" Type="String" />
                <asp:Parameter Name="serviceID" Type="Int32" />
            </UpdateParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>
