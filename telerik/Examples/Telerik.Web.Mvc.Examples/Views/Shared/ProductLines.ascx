<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.Telerik().Menu()
	.Name("product-lines")
	.Theme("demos")
	.Items(menu =>
	{
		menu.Add()
			.Text("RadControls for ASP.NET AJAX")
            .Url("http://demos.telerik.com/aspnet-ajax")
			.HtmlAttributes(new { id = "productAjaxSuite" });
		
		menu.Add()
			.Text("RadControls for Silverlight")
			.Url("http://demos.telerik.com/silverlight")
			.HtmlAttributes(new { id = "productSlSuite" });
		
		menu.Add()
			.Text("RadControls for WPF")
			.Url("http://demos.telerik.com/wpf")
			.HtmlAttributes(new { id = "productWpfSuite" });
		
		menu.Add()
			.Text("Telerik OpenAccess ORM")
			.Url("http://demos.telerik.com/orm")
			.HtmlAttributes(new { id = "productORM" });
		
		menu.Add()
			.Text("Telerik Reporting")
			.Url("http://demos.telerik.com/reporting")
			.HtmlAttributes(new { id = "productReporting" });

        menu.Add()
            .Text("ALL PRODUCTS")
            .HtmlAttributes(new { id = "all-products" })
            .Content(() =>
            {%>
<div class="column">
    <strong>UI Components</strong>
    <ul>
        <li><a href="http://www.telerik.com/products/aspnet-ajax.aspx"><span class="ajax"></span>ASP.NET AJAX</a></li>
        <li><a href="http://www.telerik.com/products/aspnet-mvc.aspx"><span class="mvc"></span>ASP.NET MVC</a></li>
        <li><a href="http://www.telerik.com/products/silverlight.aspx"><span class="silverlight"></span>Silverlight</a></li>
        <li><a href="http://www.telerik.com/products/winforms.aspx"><span class="winforms"></span>WinForms</a></li>
        <li><a href="http://www.telerik.com/products/wpf.aspx"><span class="wpf"></span>WPF</a></li>
    </ul>
    <strong class="not-first-of-type">Productivity</strong>
    <ul>
        <li><a href="http://www.telerik.com/products/justcode.aspx"><span class="jc"></span>JustCode</a></li>
    </ul>
</div>
<div class="column">
    <strong>Data</strong>
    <ul>
        <li><a href="http://www.telerik.com/products/orm.aspx"><span class="orm"></span>OpenAccess ORM</a></li>
        <li><a href="http://www.telerik.com/products/reporting.aspx"><span class="reporting"></span>Reporting</a></li>
    </ul>
    <strong class="not-first-of-type">Automated Testing</strong>
    <ul>
        <li><a href="http://www.telerik.com/products/web-testing-tools.aspx"><span class="webuites"></span>WebUI Test Studio</a></li>
    </ul>
</div>
<div class="last column">
    <strong>TFS Tools</strong>
    <ul>
        <li><a href="http://www.telerik.com/products/tfsmanager-and-tfsdashboard.aspx"><span class="tfs"></span>Work Item Manager</a></li>
        <li><a href="http://www.telerik.com/products/tfsmanager-and-tfsdashboard.aspx"><span class="tfs"></span>Project Dashboard</a></li>
    </ul>
    <strong class="not-first-of-type">Content Management</strong>
    <ul>
        <li><a href="http://www.telerik.com/products/sitefinity.aspx"><span class="sitefinity"></span>Sitefinity .NET CMS</a></li>
    </ul>
</div>
            <%});
        
	})
    .ClientEvents(events => events.OnLoad("productMenuLoad"))
    .Render(); %>