<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function onChange(e) {
            $console.log('OnChange :: to ' + formatDate(e.date) + ' from ' + formatDate(e.previousDate));
        }

        function onLoad(e) {
            $console.log('Calendar loaded');
        }
	    
	    function formatDate(date) {
	        return $.telerik.formatString('{0:dd/MM/yyyy}', date);
	    }
    </script>

    <%= Html.Telerik().Calendar()
            .Name("Calendar")
            .ClientEvents(events => events
                    .OnLoad("onLoad")
                    .OnChange("onChange")
            )
            .HtmlAttributes(new { style = "margin: 2.3em 0; float: left;" })
    %>
    	
    <% Html.RenderPartial("EventLog"); %>

</asp:content>

<asp:content contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .event-log-wrap
        {
            float: right;
            width: 468px;
            margin-left: 10em;
        }
    </style>
</asp:content>
