<%@ Page Title="CollapseDelay Tests" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>ClientEvents tests</h2>
    
    <% Html.Telerik().TreeView()
            .Name("ClientSideTreeView")
            .ClientEvents(ce => ce.OnDataBinding("onDataBinding_ClientSideTreeView"))
            .Effects(fx => fx.Toggle())
            .Render(); %>

    <script type="text/javascript">
    
        function onDataBinding_ClientSideTreeView(e) {
	        var treeview = $('#ClientSideTreeView').data('tTreeView');
	        var jsonObject;

	        if (e.item == treeview.element) {
	            jsonObject = [
	                { Text: "LoadOnDemand", LoadOnDemand: true, Value: "abyss" }
	            ];

	            treeview.bindTo(jsonObject);
	        }
        }
        
        function test_clicking_load_on_demand_nodes_triggers_databinding_event() {
            var treeview = $("#ClientSideTreeView").data("tTreeView");
            var hasCalledDataBinding = false;
            var eventContainsItem = false;
            
            var nodeToClick = $(treeview.element)
                                    .find('.t-item')
                                    .filter(function() {
                                        return $(this).find('> div').text().indexOf("LoadOnDemand") >= 0;
                                    });
        
            $(treeview.element).bind('dataBinding', function(e) {
                hasCalledDataBinding = true;
                eventContainsItem = e.item == nodeToClick[0];
            });
            
            nodeToClick.find('.t-plus').click();
            
            assertTrue("DataBinding event should be fired when elements with LoadOnDemand are clicked.", hasCalledDataBinding);
            assertTrue("DataBinding event should contain item in event data", eventContainsItem);
        }
        
    </script>

</asp:Content>
