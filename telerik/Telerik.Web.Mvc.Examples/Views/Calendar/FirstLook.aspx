<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content contentPlaceHolderID="MainContent" runat="server">
   
    <%= Html.Telerik().Calendar()
            .Name("Calendar")
            .Value((DateTime)ViewData["selectedDate"])
            .MinDate((DateTime)ViewData["minDate"])
            .MaxDate((DateTime)ViewData["maxDate"])
    %>

    <% using (Html.Configurator("The calendar should...")
                  .PostTo("FirstLook", "Calendar")
                  .Begin())
       { %>
	    <ul id="calendar-options">
		    <li>-
			    <label for="minDate-input">show dates between</label>
                <%= Html.Telerik().DatePicker()
                        .Name("minDate")
                        .Value((DateTime)ViewData["minDate"])
                %>
                <label for="maxDate-input">and</label>
                <%= Html.Telerik().DatePicker()
                        .Name("maxDate")
                        .Value((DateTime)ViewData["maxDate"])
                %>
		    </li>
		    <li>-
			    <label for="selectedDate-input">have pre-selected</label>
                <%= Html.Telerik().DatePicker()
                        .Name("selectedDate")
                        .Value((DateTime)ViewData["selectedDate"])
                %>
		    </li>
	    </ul>
        <button type="submit" class="t-button t-state-default">Apply</button>
    <% } %>
</asp:Content>

<asp:Content contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #Calendar
        {
            float: left;
            margin-bottom: 30px;
        }
        
        .example .configurator
        {
            width: 400px;
            float: left;
            margin: 0 0 0 10em;
            display: inline;
        }
        
        #calendar-options li
        {
            padding-bottom: 4px;
        }
    </style>
</asp:Content>