<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function onChange(e) {
            $console.log('OnChange :: to ' + formatDate(e.date) + ' from ' + formatDate(e.previousDate));
        }

        function onLoad(e) {
            $console.log('DatePicker loaded');
        }

		function OnOpen(e) {
		    $console.log('OnOpen :: pop-up calendar is opened.');
		}

		function OnClose(e) {
		    $console.log('OnClose :: pop-up calendar is closed.');
		}
	    
	    function formatDate(date) {
	        return $.telerik.formatString('{0:dd/MM/yyyy}', date);
	    }
    </script>

    <%= Html.Telerik().DatePicker()
            .Name("DatePicker")
            .ClientEvents(events => events
                    .OnLoad("onLoad")
                    .OnChange("onChange")
                    .OnOpen("OnOpen")
                    .OnClose("OnClose")
            )
            .HtmlAttributes(new { style = "margin-bottom: 1.3em" })
    %>
    	
    <% Html.RenderPartial("EventLog"); %>

</asp:Content>