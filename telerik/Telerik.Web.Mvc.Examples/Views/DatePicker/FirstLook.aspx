<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<FirstLookModelView>" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">

    <%= Html.Telerik().DatePicker()
            .Name("DatePicker")
            .MinDate(Model.MinDate.Value)
            .MaxDate(Model.MaxDate.Value)
            .Value(Model.SelectedDate.Value)
            .ShowButton(Model.ShowButton.Value)
    %>
    
    
    <% using (Html.Configurator("The date picker should...")
                  .PostTo("FirstLook", "DatePicker")
                  .Begin())
       { %>
	    <ul>
		    <li>
                <%= Html.CheckBox("ShowButton", Model.ShowButton.Value)%>
                <label for="ShowButton">show a popup button</label>
		    </li>
		    <li>
			    <label for="MaxDate-text">show dates between</label>
                <%= Html.Telerik().DatePicker()
                        .Name("MinDate")
                        .Value(Model.MinDate.Value)
                        .MinDate(new DateTime(999, 1, 1))
                %>
			    <label for="MinDate-text">and</label>
                <%= Html.Telerik().DatePicker()
                        .Name("MaxDate")
                        .Value(Model.MaxDate.Value)
                        .MaxDate(new DateTime(2999, 12, 31))
                %>
		    </li>
		    <li>
			    <label for="SelectedDate-text">have pre-selected</label>
                <%= Html.Telerik().DatePicker()
                        .Name("SelectedDate")
                        .Value(Model.SelectedDate.Value)
                %>
		    </li>
	    </ul>
        <button type="submit" class="t-button t-state-default">Apply</button>
    <% } %>

</asp:content>

<asp:content contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        
        #DatePicker
        {
            margin-bottom: 230px;
            float: left;
        }
		
	    .example .configurator
	    {
	        width: 400px;
	        float: left;
	        margin: 0 0 0 15em;
	        display: inline;
	    }
	    
	    .configurator p
	    {
	        margin: 0;
	        padding: .4em 0;
	    }
    </style>
</asp:content>