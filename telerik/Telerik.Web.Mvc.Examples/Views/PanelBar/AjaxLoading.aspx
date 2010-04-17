<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Examples.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content contentPlaceHolderID="MainContent" runat="server">

	<% Html.Telerik().PanelBar()
			.Name("PanelBar")
            .HtmlAttributes(new { style = "width: 300px" })
			.Items(parent => {
                parent.Add()
                    .Text("Pure ASP.NET MVC components")
                    /* this panel will be visible initially - no need to get it with ajax */
                    .Content(() =>
                    {%>
						<p>
						    Telerik Extensions for ASP.NET MVC is built from the ground up to fully embrace
						    the values of the ASP.NET MVC framework - lightweight rendering, clean HTML,
						    clear separation of concerns, easy testability - while helping make you more
						    productive building MVC views.
					    </p>
					<%});

				parent.Add()
                    .Text("Completely Open Source")
					.LoadContentFrom("AjaxView_OpenSource", "PanelBar")
                    .Expanded(true);
				
                parent.Add()
                    .Text("Exceptional Performance")
                    .LoadContentFrom("AjaxView_ExceptionalPerformance", "PanelBar");

                parent.Add()
                    .Text("Based on jQuery")
                    .LoadContentFrom("AjaxView_BasedOnJQuery", "PanelBar");

                parent.Add()
                    .Text("Wide Cross-browser support")
                    .LoadContentFrom("AjaxView_CrossBrowser", "PanelBar");
			})
			.Render();
	%>
				
</asp:Content>

<asp:Content contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #PanelBar .t-content p
        {
            margin: 0;
            padding: 1em;
        }
        
    </style>
</asp:Content>
