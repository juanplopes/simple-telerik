<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">
        
	<script type="text/javascript">
	    function onSelect(e) {
			var item = $(e.item);
			$console.log('OnSelect :: ' + item.find('> .t-link').text());

        }
        function onLoad(e) {
            $console.log('TabStrip loaded');
        }
	</script>
	
	<%= Html.Telerik().TabStrip()
            .Name("TabStrip")
			.ClientEvents(events => events
					.OnLoad("onLoad")
					.OnSelect("onSelect")
			)
			.Items(tabstrip =>
			{
				tabstrip.Add().Text("UI Components");
				tabstrip.Add().Text("Data");
				tabstrip.Add().Text("TFS Tools");
				tabstrip.Add().Text("Automated Testing");
				tabstrip.Add().Text("ASP.NET CMS");
			})
	%>

    <% Html.RenderPartial("EventLog"); %>
			
</asp:content>

<asp:content contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .event-log-wrap
        {
            margin-top: 3em;
        }
    </style>
</asp:content>