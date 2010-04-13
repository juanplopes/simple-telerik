<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content contentPlaceHolderID="HeadContent" runat="server">
	<style type="text/css">
		#Menu .t-sprite
		{
			background-image: url('<%= ResolveUrl("~/Content/Common/sprite.png") %>');
		}
		
		.icon-mvc            { background-position: 0 0; }
		.icon-mvc-grid       { background-position: 0 -16px; }
		.icon-mvc-menu       { background-position: 0 -32px; }
		.icon-mvc-panelbar   { background-position: 0 -48px; }
		.icon-mvc-tabstrip   { background-position: 0 -64px; }
		
		.icon-ajax           { background-position: -16px 0; }
		.icon-ajax-grid      { background-position: -16px -16px; }
		.icon-ajax-editor    { background-position: -16px -32px; }
		.icon-ajax-scheduler { background-position: -16px -48px; }
		
		.icon-sl             { background-position: -32px 0; }
		.icon-sl-grid        { background-position: -32px -16px; }
		.icon-sl-charting    { background-position: -32px -32px; }
		.icon-sl-scheduler   { background-position: -32px -48px; }
		.icon-sl-docking     { background-position: -32px -64px; }
	</style>
</asp:Content>

<asp:Content contentPlaceHolderID="MainContent" runat="server">

<%= Html.Telerik().Menu()
        .Name("Menu")
		.Items(menu => {
			menu.Add()
				.Text("Telerik Extensions for ASP.NET MVC")
				.SpriteCssClasses("icon-mvc")
				.Items(item =>
				{
					item.Add().Text("Grid")
						.SpriteCssClasses("icon-mvc-grid");

					item.Add().Text("Menu")
						.SpriteCssClasses("icon-mvc-menu");

					item.Add().Text("PanelBar")
						.SpriteCssClasses("icon-mvc-panelbar");

					item.Add().Text("TabStrip")
						.SpriteCssClasses("icon-mvc-tabstrip");
				});

			menu.Add()
				.Text("RadControls for ASP.NET AJAX")
				.SpriteCssClasses("icon-ajax")
				.Items(item =>
				{
					item.Add().Text("Grid")
						.SpriteCssClasses("icon-ajax-grid");

					item.Add().Text("Editor")
						.SpriteCssClasses("icon-ajax-editor");

					item.Add().Text("Scheduler")
						.SpriteCssClasses("icon-ajax-scheduler");
				});

			menu.Add()
				.Text("RadControls for Silverlight")
				.SpriteCssClasses("icon-sl")
				.Items(item =>
				{
					item.Add().Text("Grid")
						.SpriteCssClasses("icon-sl-grid");

					item.Add().Text("Chart")
						.SpriteCssClasses("icon-sl-charting");

					item.Add().Text("Scheduler")
						.SpriteCssClasses("icon-sl-scheduler");

					item.Add().Text("Docking")
						.SpriteCssClasses("icon-sl-docking");
				});

        })
%>
</asp:Content>
