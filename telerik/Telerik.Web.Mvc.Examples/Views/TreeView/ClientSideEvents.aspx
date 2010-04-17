<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">
	
		<%= Html.Telerik().TreeView()
                .Name("TreeView")
                .HtmlAttributes(new { style = "width: 300px; float: left; margin-bottom: 30px;" })
				.ClientEvents(events => events
						.OnLoad("onLoad")
						.OnSelect("onSelect")
						.OnCollapse("onCollapse")
						.OnExpand("onExpand")
                        
                        // drag & drop related
                        .OnNodeDragStart("onNodeDragStart")
                        .OnNodeDrop("onNodeDrop")
                        .OnNodeDropped("onNodeDropped")
                        .OnNodeDragCancelled("onNodeDragCancelled")
				)
		        .DragAndDrop(true)      
				.Items(treeView =>
				{
					treeView.Add().Text("UI Components")
						.Items(item =>
						{
							item.Add().Text("ASP.NET WebForms");
							item.Add().Text("Silverlight");
							item.Add().Text("ASP.NET MVC");
							item.Add().Text("WinForms");
							item.Add().Text("WPF");
						})
						.Expanded(true);

					treeView.Add().Text("Data")
						.Items(item =>
						{
							item.Add().Text("OpenAccess ORM");
							item.Add().Text("Reporting");
						});

					treeView.Add().Text("TFS Tools")
						.Items(item =>
						{
							item.Add().Text("Work Item Manager");
							item.Add().Text("Project Dashboard");
						});

					treeView.Add().Text("Automated Testing")
						.Items(item =>
						{
							item.Add().Text("Web Testing Tools");
						});

					treeView.Add().Text("ASP.NET CMS")
						.Items(item =>
						{
							item.Add().Text("Sitefinity CMS");
						});
				})
      %>
        
	<script type="text/javascript">
		function onSelect(e) {
			var item = $(e.item);
			$console.log('OnSelect :: ' + item.text());
		}
		
		function onCollapse(e) {
		    var item = $(e.item);
			$console.log('OnCollapse :: ' + item.text());
		}

		function onExpand(e) {
		    var item = $(e.item);
			$console.log('OnExpand :: ' + item.text());
        }

		function onNodeDragStart(e) {
		    var item = $(e.item);
			$console.log('OnNodeDragStart :: ' + item.text());
        }

		function onNodeDragCancelled(e) {
		    var item = $(e.item);
			$console.log('OnNodeDragCancelled :: ' + item.text());
        }

		function onNodeDrop(e) {
		    var item = $(e.item);
			$console.log('OnNodeDrop :: ' + item.text());
        }

		function onNodeDropped(e) {
			$console.log('OnNodeDropped :: `' + $(e.item).text() + '` ' + e.dropPosition + ' `' + $(e.destinationItem).text() + '`');
        }

        function onLoad(e) {
            $console.log('TreeView loaded');
        }
		
	</script>
 
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
