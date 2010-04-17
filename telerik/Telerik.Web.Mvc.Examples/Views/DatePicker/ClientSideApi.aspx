<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">

    <%= Html.Telerik().DatePicker()
            .Name("DatePicker") %>

	<script type="text/javascript">

        function updateValue() {
            var value = $('#valuePicker').data("tDatePicker").value();
	        $("#DatePicker").data("tDatePicker").value(value);
	    }

	    function getValue() {
	        alert($("#DatePicker").data("tDatePicker").value());
	    }

	    function showCalendar(e) {
	        $("#DatePicker").data("tDatePicker").showPopup();
	        
	        // prevent the click from bubbling, thus, closing the popup
	        if (e.stopPropagation) e.stopPropagation();
	        e.cancelBubble = true; 
	    }

	    function hideCalendar() {
	        $("#DatePicker").data("tDatePicker").hidePopup();
	    }
	</script>

    <% using (Html.Configurator("Client API").Begin()) { %>
        <ul>
            <li>
                <%= Html.Telerik().DatePicker()
                        .Name("valuePicker")
                        .Value(DateTime.Today) 
                %>
                <button class="t-button t-state-default" onclick="updateValue()">Select</button>
            </li>
            
            <li>
                <button class="t-button t-state-default" onclick="getValue()" style="width: 100px;">Get Value</button>
            </li>
            <li>
                <button class="t-button t-state-default" onclick="showCalendar(event)">Open</button> /
                <button class="t-button t-state-default" onclick="hideCalendar()">Close</button>
            </li>
        </ul>
    <% } %>
			
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
	    
	    .configurator .t-button
	    {
	        margin: 0 0 1em;
	        display: inline-block;
	        *display: inline;
	        zoom: 1;
	    }
	    
	    #DatePicker
	    {
	        float: left;
	        margin-bottom: 160px;
	    }
	    
    </style>
</asp:Content>