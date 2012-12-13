<%@ Page Language="C#" MasterPageFile="~/10/Site.master" AutoEventWireup="true" CodeFile="UpdatePanelAnimation.aspx.cs"
    Inherits="UpdatePanelAnimation_UpdatePanelAnimation" Title="UpdatePanelAnimation Sample" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true" />
    <div class="demoarea">
        <div class="demoheading">
            UpdatePanelAnimation Demonstration</div>
        <div style="margin-bottom: 10px;">
            <div style="border: dashed 1px #222222;">
                <div id="up_container" style="background-color: #40669A;">
                    <asp:UpdatePanel ID="update" runat="server">
                        <ContentTemplate>
                            <div id="background" style="text-align: center; vertical-align: middle; line-height: 44px;
                                padding: 12px; height: 44px; color: #FFFFFF;">
                                <asp:Label ID="lblUpdate" runat="server" Style="padding: 5px; font-size: 14px; font-weight: bold;">Hello World!</asp:Label>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnUpdate" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <input type="checkbox" id="effect_fade" checked="checked" visible="false" /><label
            for="effect_fade">Fade</label><br />
        <input type="checkbox" id="effect_color" checked="checked" visible="false" /><label
            for="effect_color">Color Background</label><br />
        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
        <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" runat="server" TargetControlID="update">
            <Animations>
                    <OnUpdating>
                        <Sequence>
                            <Parallel duration="0">
                                <EnableAction AnimationTarget="effect_fade" Enabled="false" />
                            </Parallel>
                            
                            <StyleAction Attribute="overflow" Value="hidden" />
                            <Parallel duration=".25" Fps="30">
                                <Condition ConditionScript="$get('effect_fade').checked">
                                    <FadeOut AnimationTarget="up_container" minimumOpacity=".2" />
                                </Condition>
                                <Condition ConditionScript="$get('effect_color').checked">
                                    <Color AnimationTarget="up_container" EndValue="#FF0000" StartValue="#40669A" Property="style" PropertyKey="backgroundColor"/>
                                </Condition>
                            </Parallel>
                        </Sequence>
                    </OnUpdating>
                    <OnUpdated>
                        <Sequence>
                            <Parallel duration=".25" Fps="30">
                                <Condition ConditionScript="$get('effect_fade').checked">
                                    <FadeIn AnimationTarget="up_container" minimumOpacity=".2" />
                                </Condition>
                                <Condition ConditionScript="$get('effect_color').checked">
                                    <Color AnimationTarget="up_container" StartValue="#FF0000" EndValue="#40669A" Property="style" PropertyKey="backgroundColor"/>
                                </Condition>
                            </Parallel>
                            
                            <Parallel duration="0">
                                <EnableAction AnimationTarget="btnUpdate" Enabled="true" />
                            </Parallel>
                            
                        </Sequence>
                    </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
    <div class="demobottom">
    </div>
</asp:Content>
