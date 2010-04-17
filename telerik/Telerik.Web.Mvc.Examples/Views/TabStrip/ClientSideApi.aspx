<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">
<% Html.Telerik().TabStrip()
        .Name("TabStrip")
		.Items(tabstrip =>
		{
            tabstrip.Add()
                    .Text("Based on jQuery")
                    .Content(() =>
                    {%>
                        <p>
                            The client-side code of the Telerik Extensions for ASP.NET MVC is based on
                            the open source and Microsoft-supported jQuery JavaScript library.
                            By using jQuery, the Telerik Extensions minimize their client-side footprint
                            and draw on the power of jQuery for advanced visual effects as well as for an easy
                            and reliable way to work with HTML elements.
                        </p>
                    <%});

            tabstrip.Add()
                    .Text("Cross-browser Support")
                    .Content(() =>
                    {%>
                        <p>
                            Telerik is renowned for making its product consistent across all major browsers:
                            Internet Explorer, Firefox, Safari, Opera and Google Chrome.
                            If an update or a new browser is coming,
                            the Telerik web components will be the first to support it!
                        </p>
                    <%});
            
            tabstrip.Add()
                    .Text("Exceptional Performance")
                    .Content(() =>
                    {%>
                        <p>
                            You can achieve unprecedented performance for your web application
                            with the lightweight, semantically rendered Extensions
                            that completely leverage the ASP.NET MVC model of no postbacks,
                            no ViewState, and no page life cycle.
                            Additional performance gains are delivered through the Extensions’
                            Web Asset Managers, which enable you to optimize the delivery of
                            CSS and JavaScript to your pages. You combine, cache, and compress
                            resources resulting in fewer requests that your page must make to load,
                            improving page load time performance.
                        </p>
                    <%});

			tabstrip.Add()
                    .Text("Completely Open-Source")
                    .Content(() =>
                    {%>
                        <p>
                            The Telerik Extensions for ASP.NET MVC are licensed under the widely
                            adopted GPLv2. In fact, the complete source for the Extensions is
                            available on CodePlex. For those that need dedicated support or need 
                            to use the Extensions in an environment where open source software is 
                            hard to get approved, Telerik provides a commercial license with support included.
                        </p>
                    <%});
		})
        .HtmlAttributes(new { style = "float: left; width: 605px;" })
		.Render(); %>
	
<% using (Html.Configurator("Client API").Begin()) { %>
    <p>
        <label for="itemIndex">Item index:</label>
        <%= Html.TextBox("itemIndex", "0", new { style = "width: 40px" })%> <br />
        
        <button class="t-button t-state-default" onclick="Select()">Select</button><br />
        
        <button class="t-button t-state-default" onclick="Enable()">Enable</button> /
        <button class="t-button t-state-default" onclick="Disable()">Disable</button>
    </p>
<% } %>
        
	<script type="text/javascript">
		
	    function Select() {
	        var tabstrip = $("#TabStrip").data("tTabStrip");
	        var item = $("li", tabstrip.element)[$("#itemIndex").val()];

	        tabstrip.select(item);
	    }

	    function Enable() {
	        var tabstrip = $("#TabStrip").data("tTabStrip");
	        var item = $("li", tabstrip.element)[$("#itemIndex").val()];

	        tabstrip.enable(item);
	    }

	    function Disable() {
	        var tabstrip = $("#TabStrip").data("tTabStrip");
	        var item = $("li", tabstrip.element)[$("#itemIndex").val()];

	        tabstrip.disable(item);
	    }
	</script>
			
</asp:content>

<asp:Content contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
		
	    .example .configurator
	    {
	        width: 200px;
	        height: 140px;
	        float: right;
	        margin: 0;
	    }
	    
	    .configurator p
	    {
	        margin: 0;
	        padding: 1em 0;
	    }
	    
	    .configurator .t-button
	    {
	        display: inline-block;
	        *display: inline;
	        zoom: 1;
	        margin-top: 1em;
	    }
    </style>
</asp:Content>