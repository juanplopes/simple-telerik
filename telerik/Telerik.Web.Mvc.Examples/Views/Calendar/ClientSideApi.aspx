<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">
    <%= Html.Telerik().Calendar()
             .Name("Calendar")
    %>
        
	<script type="text/javascript">
	    function updateValue() {
	        var value = $('#valuePicker').data("tDatePicker").value();
	        $("#Calendar").data("tCalendar").value(value);
	    }	    
	</script>
	
    <% using (Html.Configurator("Client API").Begin()) { %>
        <%= Html.Telerik().DatePicker()
                .Name("valuePicker")
                .Value(DateTime.Today) 
        %>
        <button onclick="updateValue()" class="t-button t-state-default">Select</button>
    <% } %>
			
</asp:content>

<asp:content contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        
        #Calendar
        {
            float: left;
            margin-bottom: 1.3em;
        }
		
	    .example .configurator
	    {
	        width: 300px;
	        float: left;
	        margin: 0 0 0 10em;
	        display: inline;
	    }
	    
	    .example .configurator-legend
	    {
	        margin-bottom: .6em;
	    }
	    
	    .example .configurator .t-button
	    {
	        display: inline-block;
	        *display: inline;
	        zoom: 1;
	        margin: 1em 0 1.5em 1em;
	    }
    </style>
</asp:content>