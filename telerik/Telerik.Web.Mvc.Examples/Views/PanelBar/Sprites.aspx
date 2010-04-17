<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content contentPlaceHolderID="HeadContent" runat="server">
	<style type="text/css">
		#PanelBar .t-sprite
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

    <% 
        Html.Telerik().PanelBar()
            .Name("PanelBar")
            .HtmlAttributes(new { style = "width: 300px;" })
			.Items(panelbar => {
				panelbar.Add()
					.Text("Telerik Extensions for ASP.NET MVC")
					.SpriteCssClasses("icon-mvc")
					.Items(parent =>
					{
						parent.Add().Text("Grid")
							.SpriteCssClasses("icon-mvc-grid");

						parent.Add().Text("Menu")
							.SpriteCssClasses("icon-mvc-menu");

						parent.Add().Text("PanelBar")
							.SpriteCssClasses("icon-mvc-panelbar");

						parent.Add().Text("TabStrip")
							.SpriteCssClasses("icon-mvc-tabstrip");
					})
					.Expanded(true);

				panelbar.Add()
					.Text("RadControls for ASP.NET AJAX")
					.SpriteCssClasses("icon-ajax")
					.Items(parent =>
					{
						parent.Add().Text("Grid")
							.SpriteCssClasses("icon-ajax-grid");

						parent.Add().Text("Editor")
							.SpriteCssClasses("icon-ajax-editor");

						parent.Add().Text("Scheduler")
							.SpriteCssClasses("icon-ajax-scheduler");
					})
					.Expanded(true);

				panelbar.Add()
					.Text("RadControls for Silverlight")
					.SpriteCssClasses("icon-sl")
					.Items(parent =>
					{
						parent.Add().Text("Grid")
							.SpriteCssClasses("icon-sl-grid");

						parent.Add().Text("Chart")
							.SpriteCssClasses("icon-sl-charting");

						parent.Add().Text("Scheduler")
							.SpriteCssClasses("icon-sl-scheduler");

						parent.Add().Text("Docking")
							.SpriteCssClasses("icon-sl-docking");
					})
					.Expanded(true);
					
			})
            .Render();
    %>

</asp:Content>
