<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<FirstLookModelView>" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">
    

    <%= Html.Telerik().DatePicker()
            .Name("DatePicker")
            .HtmlAttributes(new { style = "margin-bottom: 230px" })
    %>
    
    <% using (Html.Configurator("Try entering...").Begin()) { %>
        <ul class="humanReadableExamples">
            <li><a href="#"><%= DateTime.Today.ToShortDateString() %></a></li>
            <li><a href="#">today</a></li>
            <li><a href="#">tomorrow</a></li>
        </ul>
        <ul class="humanReadableExamples">
            <li><a href="#">next Tuesday</a></li>
            <li><a href="#">last March</a></li>
        </ul>
    <% } %>
    
    <% Html.Telerik().ScriptRegistrar().OnDocumentReady(() => {%>
        $('.humanReadableExamples a').click(function(e) {
            e.preventDefault();
            
            setTimeout($.proxy(function() {
                $('#DatePicker-input')
                .val($(this).text())
                .focus();
            }, this), 0);
        });
    <%}); %>
    
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
	        width: 300px;
	        float: left;
	        margin: 0 0 0 15em;
	        display: inline;
	    }
	    
	    .right-aligned
	    {
	        width: 82px;
	        text-align: right;
	        padding-right: 5px;
	    }
	    
	    .configurator p
	    {
	        margin: 0;
	        padding: .4em 0;
	    }
	    
	    .configurator .humanReadableExamples
	    {
	        float: left; width: 120px;
	        padding-left: 20px;
	    }
    </style>
</asp:content>
