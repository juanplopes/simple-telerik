<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">
        
	<script type="text/javascript">
	    function onSelect(e) {
			var item = $(e.item);
			$console.log('OnSelect :: ' + item.find('> .t-link').text());
		}

		function onClose(e) {
		    var item = $(e.item);
		    $console.log('OnClose :: ' + item.find('> .t-link').text());
		}
		
		function onOpen(e) {
		    var item = $(e.item);
		    $console.log('OnOpen :: ' + item.find('> .t-link').text());
		}

		function onLoad(e) {
		    $console.log('Menu loaded');
		}
	</script>
	
	<%= Html.Telerik().Menu()
            .Name("Menu")
            .HtmlAttributes(new { style = "margin-bottom: 80px;" })
			.ClientEvents(events => events
					.OnLoad("onLoad")
					.OnSelect("onSelect")
                    .OnOpen("onOpen")
                    .OnClose("onClose")
			)
			.Items(menu =>
			{
				menu.Add().Text("UI Components")
					.Items(item =>
					{
						item.Add().Text("ASP.NET WebForms");
						item.Add().Text("Silverlight");
						item.Add().Text("ASP.NET MVC");
						item.Add().Text("WinForms");
						item.Add().Text("WPF");
					});

				menu.Add().Text("Data")
					.Items(item =>
					{
						item.Add().Text("OpenAccess ORM");
						item.Add().Text("Reporting");
					});

				menu.Add().Text("TFS Tools")
					.Items(item =>
					{
						item.Add().Text("Work Item Manager");
						item.Add().Text("Project Dashboard");
					});

				menu.Add().Text("Automated Testing")
					.Items(item =>
					{
						item.Add().Text("Web Testing Tools");
					});

				menu.Add().Text("ASP.NET CMS")
					.Items(item =>
					{
						item.Add().Text("Sitefinity CMS");
					});
			})
	    %>
 
    <% Html.RenderPartial("EventLog"); %>
			
</asp:content>