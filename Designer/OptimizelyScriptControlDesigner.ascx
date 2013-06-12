<%@ Control Language="C#" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sitefinity" %>
<sitefinity:ResourceLinks ID="resourcesLinks" runat="server">
    <sitefinity:ResourceFile Name="OptimizelyScriptControl.Resources.WidgetStyles.css" Static="True" />
</sitefinity:ResourceLinks>

<div class="sfClearfix">
    <div class="sfModeSelector">
        <h2 class="sfStep">Please paste the Optimizely script code
        </h2>
        <div id="main-container">
            <span class="k-in">
                <telerik:RadTextBox runat="server" ID="UrlTextBox" TextMode="Multiline" Skin="MetroTouch" width="320px" EmptyMessage="e.g. www.someaddress.js"></telerik:RadTextBox>
                <span runat="server" class="w-icon w-question" id="lbl"></span>
            </span>
            <br />
            <asp:Label runat="server" ID="ErrorLabel" Text="" CssClass="errorZone hidden"></asp:Label>
        </div>

        <telerik:RadToolTip id="radToolTipEvents" runat="server" TargetControlID="lbl" RelativeTo="Element" Skin="MetroTouch" RenderInPageRoot="true">
            <div class="appointment-tooltip">
                <p>You can paste a valid script tag or just the "src" attribute value of it, i.e. //cdn.optimizely.com/js/123456789.js</p>
            </div>
        </telerik:RadToolTip>
    </div>
</div>