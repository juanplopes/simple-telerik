<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>


<asp:Content contentPlaceHolderID="MainContent" runat="server">

	<% Html.Telerik().Menu()
            .Name("Menu")
            .HtmlAttributes(new { @class = "t-menu-rtl" })
            .Items(menu =>
            {
                menu.Add()
                    .Text("ASP.NET MVC")
					.ImageUrl("~/Content/Common/Icons/Suites/mvc.png")
                    .Items(item =>
                    {
                        item.Add().Text("Grid").ImageUrl("~/Content/Common/Icons/Mvc/Grid.png");
                        item.Add().Text("Menu").ImageUrl("~/Content/Common/Icons/Mvc/Menu.png");
                        item.Add().Text("PanelBar").ImageUrl("~/Content/Common/Icons/Mvc/PanelBar.png");
                        item.Add().Text("TabStrip").ImageUrl("~/Content/Common/Icons/Mvc/TabStrip.png");
                    });

                menu.Add()
                    .Text("Silverlight")
                    .ImageUrl("~/Content/Common/Icons/Suites/sl.png")
                    .Items(item =>
                    {
                        item.Add().Text("GridView").ImageUrl("~/Content/Common/Icons/Sl/GridView.gif");
                        item.Add().Text("Scheduler").ImageUrl("~/Content/Common/Icons/Sl/Scheduler.gif");
                        item.Add().Text("Docking").ImageUrl("~/Content/Common/Icons/Sl/Docking.gif");
                        item.Add().Text("Chart").ImageUrl("~/Content/Common/Icons/Sl/Chart.gif");
                        item.Add().Text("... and 28 more!").Url("http://www.telerik.com/products/silverlight.aspx");
                    });

                menu.Add()
                    .Text("ASP.NET AJAX")
					.ImageUrl("~/Content/Common/Icons/Suites/ajax.png")
                    .Items(item =>
                    {
                        item.Add().Text("Grid").ImageUrl("~/Content/Common/Icons/Ajax/Grid.png");
                        item.Add().Text("Editor").ImageUrl("~/Content/Common/Icons/Ajax/Editor.png");
                        item.Add().Text("Scheduler").ImageUrl("~/Content/Common/Icons/Ajax/Scheduler.png");
                        item.Add().Text("... and 28 more!").Url("http://www.telerik.com/products/aspnet-ajax.aspx");

                    });

                menu.Add()
                    .Text("OpenAccess ORM")
					.ImageUrl("~/Content/Common/Icons/Suites/orm.png")
                    .Content(() =>
                    {%>
                        <img src="<%= ResolveUrl("~/Content/Common/orm-comparison.png") %>" alt="Telerik OpenAccess" />
                        <a id="buy" href="http://www.telerik.com/purchase/individual/orm.aspx">
                            Telerik OpenAccess ORM
                        </a>
                        <a id="express" href="http://www.telerik.com/community/license-agreement.aspx?pId=639">
                            Telerik OpenAccess ORM Express
                        </a>
                    <%});

                menu.Add()
                    .Text("Reporting")
					.ImageUrl("~/Content/Common/Icons/Suites/rep.png");

                menu.Add()
                    .Text("Sitefinity ASP.NET CMS")
					.ImageUrl("~/Content/Common/Icons/Suites/sitefinity.png");

                menu.Add()
					.HtmlAttributes(new { style = "border-left: 0;" })
                    .Text("Other products")
                    .Items(item =>
                    {
						item.Add().Text("Web Testing Tools").ImageUrl("~/Content/Common/Icons/Suites/test.png");
						item.Add().Text("WinForms UI Controls").ImageUrl("~/Content/Common/Icons/Suites/win.png");
						item.Add().Text("WPF UI Controls").ImageUrl("~/Content/Common/Icons/Suites/wpf.png");
                    });
            })
            .Render();
	%>

</asp:Content>

<asp:Content contentPlaceHolderID="HeadContent" runat="server">
	<style type="text/css">
	    #buy, #express
	    {
	        position: absolute;
	        width: 50%;
	        height: 100%;
	        text-indent: 9999px;
	        outline: 0;
	    }
	    
	    #buy { left: 0; }
	    #express { right: 0; }
	</style>
</asp:Content>