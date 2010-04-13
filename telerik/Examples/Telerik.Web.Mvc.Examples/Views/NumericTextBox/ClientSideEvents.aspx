<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">
        
	<script type="text/javascript">
	    function onChange(e) {
	        
			$console.log('OnChange :: oldValue: ' + e.oldValue + ', newValue: ' + e.newValue);
        }
        function onLoad(e) {
            $console.log('NumericTextBox loaded');
        }
	</script>
	
    <%= Html.Telerik().NumericTextBox()
            .Name("NumericTextBox")
            .HtmlAttributes(new { style = "margin-bottom: 1.3em" })
            .MinValue(-100)
            .MaxValue(100)
            .ClientEvents(events => events
                .OnLoad("onLoad")    
                .OnChange("onChange"))
    %>
    
    <% Html.RenderPartial("EventLog"); %>
			
</asp:content>