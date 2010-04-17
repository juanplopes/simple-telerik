<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content contentPlaceHolderID="MainContent" runat="server">

<%= Html.Telerik().Menu()
		.Name("Menu")
        .HtmlAttributes(new { style = "width: 300px; float: left;" })
        .Effects(fx =>
        {
            if (ViewData["animation"].ToString() == "slide")
            {
                fx.Slide();
            }
            else if (ViewData["animation"].ToString() == "expand")
            {
                fx.Expand();
            }
            else
            {
                /* activate only toggle, so that the items show */
                fx.Toggle();
            }

            if ((bool)ViewData["enableOpacityAnimation"])
                fx.Opacity();

            fx.OpenDuration((int)ViewData["openDuration"])
              .CloseDuration((int)ViewData["closeDuration"]);
        })
        .Items(menu =>
        {
            menu.Add().Text("Item 1")
                .Items(item =>
                {
                    item.Add().Text("Child item 1.1");
                    item.Add().Text("Child item 1.2");
                    item.Add().Text("Child item 1.3");
                    item.Add().Text("Child item 1.4");
                    
                    item.Add().Text("Child item 1.5")
                        .Items(child =>
                        {
                            child.Add().Text("Child item 1.5.1");
                            child.Add().Text("Child item 1.5.2");
                            child.Add().Text("Child item 1.5.3");
                            child.Add().Text("Child item 1.5.4");
                            child.Add().Text("Child item 1.5.5");
                        });
                });

            menu.Add().Text("Item 2")
                .Items(item =>
                {
                    item.Add().Text("Child item 2.1");
                    item.Add().Text("Child item 2.2");
                    item.Add().Text("Child item 2.3");
                    item.Add().Text("Child item 2.4");
                    item.Add().Text("Child item 2.5");
                });

            menu.Add().Text("Item 3")
                .Items(item =>
                {
                    item.Add().Text("Child item 3.1")
                        .Items(child =>
                        {
                            child.Add().Text("Child item 3.1.1");
                            child.Add().Text("Child item 3.1.2");
                            child.Add().Text("Child item 3.1.3");
                            child.Add().Text("Child item 3.1.4");
                            child.Add().Text("Child item 3.1.5");
                        });
                    
                    item.Add().Text("Child item 3.2");
                    item.Add().Text("Child item 3.3");
                    item.Add().Text("Child item 3.4");
                    item.Add().Text("Child item 3.5");
                });

        })
%>

<% using (Html.Configurator("Animate with...")
              .PostTo("AnimationEffects", "Menu")
              .Begin())
   { %>
    <ul>
         <li>
            <%= Html.RadioButton("animation", "toggle", new { id = "toggle" }) %>
            <label for="toggle"><strong>toggle</strong> animation</label>
            <br />
            <%= Html.RadioButton("animation", "slide", new { id = "slide" }) %>
            <label for="toggle"><strong>slide</strong> animation</label>
            <br />
            <%= Html.RadioButton("animation", "expand", new { id = "expand" })%>
            <label for="toggle"><strong>expand</strong> animation</label>
            <br />
            <%= Html.CheckBox(
                    "enableOpacityAnimation",
                    (bool)ViewData["enableOpacityAnimation"],
                    "&nbsp;<strong>opacity</strong> animation")%>
        </li>
        <li>
            <ul>
                <li>
                    <label for="openDuration">open for</label>
                    <%= Html.Telerik().NumericTextBox()
                            .Name("openDuration")
                            .DecimalDigits(0)
                            .NumberGroupSeparator("")
                            .MinValue(0).MaxValue(10000)
                            .Value(Convert.ToDouble(ViewData["openDuration"]))
                    %> ms
                </li>
                <li>
                    <label for="closeDuration">close for</label>
                    <%= Html.Telerik().NumericTextBox()
                            .Name("closeDuration")
                            .DecimalDigits(0)
                            .NumberGroupSeparator("")
                            .MinValue(0).MaxValue(10000)
                            .Value(Convert.ToDouble(ViewData["closeDuration"]))
                    %> ms
                </li>
            </ul>
        </li>
    </ul>
    
    <button class="t-button t-state-default" type="submit">Apply</button>
<% } %>

<% Html.Telerik().ScriptRegistrar().OnDocumentReady(() => {%>
	/* client-side validation */
    $('.configurator button').click(function(e) {
        $('.configurator :text').each(function () {
            if ($(this).hasClass('t-state-error')) {
                alert("TextBox `" + this.name + "` has an invalid param!");
                e.preventDefault();
            }
        });
    });
<%}); %>
	
</asp:Content>


<asp:Content contentPlaceHolderID="HeadContent" runat="server">
	<style type="text/css">
	    .example .configurator
	    {
	        width: 300px;
	        float: left;
	        margin: 0 0 0 10em;
	        display: inline;
	    }
	    
	    .configurator li
		{
		    padding: 3px 0;
		}
	    
		.configurator input[type=text]
		{
			width: 50px;
		}
		
		.configurator ul ul
		{
		    padding-left: 24px;
		    margin: 0;
		}
		
		.configurator ul ul label
		{
		    width: 48px;
		    margin: 0;
		}
	</style>
</asp:Content>