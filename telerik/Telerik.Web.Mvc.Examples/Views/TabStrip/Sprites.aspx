<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content contentPlaceHolderID="HeadContent" runat="server">
	<style type="text/css">
		#TabStrip .t-sprite
		{
			background-image: url('<%= ResolveUrl("~/Content/Common/sprite.png") %>');
		}
		
		.icon-mvc            { background-position: 0 0; }
		.icon-mvc-grid       { background-position: 0 -16px; }
		.icon-mvc-menu       { background-position: 0 -32px; }
		.icon-mvc-panelbar   { background-position: 0 -48px; }
		.icon-mvc-tabstrip   { background-position: 0 -64px; }
	</style>
</asp:Content>

<asp:Content contentPlaceHolderID="MainContent" runat="server">

    <% 
        Html.Telerik().TabStrip()
            .Name("TabStrip")
			.Items(tabstrip => {
                tabstrip.Add()
                    .Text("Overview")
                    .SpriteCssClasses("icon-mvc");

                tabstrip.Add()
                    .Text("Grid")
                    .SpriteCssClasses("icon-mvc-grid");

                tabstrip.Add()
                    .Text("Menu")
                    .SpriteCssClasses("icon-mvc-menu");

                tabstrip.Add()
                    .Text("PanelBar")
                    .SpriteCssClasses("icon-mvc-panelbar");

                tabstrip.Add()
                    .Text("TabStrip")
                    .SpriteCssClasses("icon-mvc-tabstrip");
			})
            .Render();
    %>

</asp:Content>
