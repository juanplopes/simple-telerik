<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">
<% Html.Telerik().TreeView()
        .Name("TreeView")
        .HtmlAttributes(new { style = "width: 300px; float: left; margin-bottom: 30px;" })
        .Items(treeViewItem =>
		{
			treeViewItem.Add().Text("UI Components")
				.Items(item =>
				{
					item.Add().Text("ASP.NET WebForms");
					item.Add().Text("Silverlight");
					item.Add().Text("ASP.NET MVC");
					item.Add().Text("WinForms");
					item.Add().Text("WPF");
				})
				.Expanded(true);

			treeViewItem.Add().Text("Data")
				.Items(item =>
				{
					item.Add().Text("OpenAccess ORM");
					item.Add().Text("Reporting");
				});

			treeViewItem.Add().Text("TFS Tools")
				.Items(item =>
				{
					item.Add().Text("Work Item Manager");
					item.Add().Text("Project Dashboard");
				});

			treeViewItem.Add().Text("Automated Testing")
				.Items(item =>
				{
					item.Add().Text("Web Testing Tools");
				});

			treeViewItem.Add().Text("ASP.NET CMS")
				.Items(item =>
				{
					item.Add().Text("Sitefinity CMS");
				});
		})
		.Render(); %>
	
<% using (Html.Configurator("Client API").Begin()) { %>
    <p>
        <label for="itemIndex">Item index:</label>
        <%= Html.TextBox("itemIndex", "0", new { style = "width: 40px" })%>
    </p>
    
    <p>
        <button onclick="ExpandItem()">Expand</button> / <button onclick="CollpaseItem()">Collapse</button>
    </p>
    
    <p>
        <button onclick="EnableItem()">Enable</button> / <button onclick="DisableItem()">Disable</button>
    </p>
<% } %>
        
	<script type="text/javascript">
		
	    function ExpandItem() {
	        var treeView = $("#TreeView").data("tTreeView");
	        var item = $("> ul > li", treeView.element)[$("#itemIndex").val()];
	        
	        treeView.expand(item);
	    }

	    function CollpaseItem() {
	        var treeView = $("#TreeView").data("tTreeView");
	        var item = $("> ul > li", treeView.element)[$("#itemIndex").val()];
	        
	        treeView.collapse(item);
	    }

	    function EnableItem() {
	        var treeView = $("#TreeView").data("tTreeView");
	        var item = $("> ul > li", treeView.element)[$("#itemIndex").val()];
	        
	        treeView.enable(item);
	    }

	    function DisableItem() {
	        var treeView = $("#TreeView").data("tTreeView");
	        var item = $("> ul > li", treeView.element)[$("#itemIndex").val()];

	        treeView.disable(item);
	    }
	</script>
			
</asp:content>

<asp:Content contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
		
	    .example .configurator
	    {
	        width: 300px;
	        float: left;
	        margin: 0 0 0 10em;
	        display: inline;
	    }
	    
	    .configurator p
	    {
	        margin: 0;
	        padding: .4em 0;
	    }
    </style>
</asp:Content>