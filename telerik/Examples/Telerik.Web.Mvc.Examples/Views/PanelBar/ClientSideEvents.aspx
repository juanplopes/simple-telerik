<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">
        
	<script type="text/javascript">
		function onSelect(e) {
			var item = $(e.item);
			$console.log('OnSelect :: ' + item.find('> .t-link').text());
		}
		
		function onCollapse(e) {
		    var item = $(e.item);
			$console.log('OnCollapse :: ' + item.find('> .t-link').text());
		}
		
		function onExpand(e) {
		    var item = $(e.item);
			$console.log('OnExpand :: ' + item.find('> .t-link').text());
        }

        function onLoad(e) {
            $console.log('PanelBar loaded');
        }
	</script>
	
	<%= Html.Telerik().PanelBar()
            .Name("PanelBar")
            .HtmlAttributes(new { style = "width: 300px; float: left; margin-bottom: 30px;" })
			.ClientEvents(events => events
					.OnLoad("onLoad")
					.OnSelect("onSelect")
					.OnCollapse("onCollapse")
					.OnExpand("onExpand")
			)
			.Items(panelbar =>
			{
				panelbar.Add().Text("UI Components")
					.Items(item =>
					{
						item.Add().Text("ASP.NET WebForms");
						item.Add().Text("Silverlight");
						item.Add().Text("ASP.NET MVC");
						item.Add().Text("WinForms");
						item.Add().Text("WPF");
					})
					.Expanded(true);

				panelbar.Add().Text("Data")
					.Items(item =>
					{
						item.Add().Text("OpenAccess ORM");
						item.Add().Text("Reporting");
					});

				panelbar.Add().Text("TFS Tools")
					.Items(item =>
					{
						item.Add().Text("Work Item Manager");
						item.Add().Text("Project Dashboard");
					});

				panelbar.Add().Text("Automated Testing")
					.Items(item =>
					{
						item.Add().Text("Web Testing Tools");
					});

				panelbar.Add().Text("ASP.NET CMS")
					.Items(item =>
					{
						item.Add().Text("Sitefinity CMS");
					});
			})
	    %>
 
    <% Html.RenderPartial("EventLog"); %>
			
</asp:content>

<asp:content contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .event-log-wrap
        {
            float: left;
            display: inline;
            width: 468px;
            margin-left: 10em;
        }
    </style>
</asp:content>