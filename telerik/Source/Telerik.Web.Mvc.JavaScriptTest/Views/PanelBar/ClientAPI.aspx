<%@ Page Title="ClientAPI tests" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <h2>Client API Tests</h2>
    
	<% Html.Telerik().PanelBar()
            .Name("myPanelBar")
            .Items(panelBarItem =>
            {
                panelBarItem.Add().Text("Item 1")
                    .Items(item =>
                    {
                        item.Add().Text("Child Item 1.1");
                        item.Add().Text("Child Item 1.2");
                        item.Add().Text("Child Item 1.3");
                        item.Add().Text("Child Item 1.4");
                    });
                panelBarItem.Add().Text("Item 2")
                                  .ImageHtmlAttributes(new { alt = "testImage", height = "10px", width = "10px" })
                                  .ImageUrl(Url.Content("~/Content/Images/telerik.png"));

				panelBarItem.Add().Text("Item 3")
					.Items(item =>
					{
						item.Add().Text("Child Item 3.1");
						item.Add().Text("Child Item 3.2");
						item.Add().Text("Child Item 3.3");
						item.Add().Text("Child Item 3.4");
					})
					.Enabled(false);
                panelBarItem.Add().Text("Item 4")
                    .Items(item =>
                    {
                        item.Add().Text("Child Item 4.1")
                            .Items(subitem =>
                            {
                                subitem.Add().Text("Grand Child Item 4.1.1");
                                subitem.Add().Text("Grand Child Item 4.1.2");
                            });

                        item.Add().Text("Child Item 4.2");
                    }).Enabled(false);
                panelBarItem.Add().Text("Item 5")
                    .Items(item =>
                    {
                        item.Add().Text("Child Item 5.1");
                    });
            }
            ).ClientEvents(events =>
            {
                events.OnExpand("Expand");
                events.OnCollapse("Collapse");
                events.OnSelect("Select");
                events.OnLoad("Load");
            })
            .Effects(effects => effects.Toggle())
            .Render(); %>
    
   <script type="text/javascript">

       var isRaised;
        
        function getRootItem(index) {
			return $('#myPanelBar').find('.t-header').parent().eq(index)
        }

        function getPanelBar() {
            return $("#myPanelBar").data("tPanelBar");
        }

        function test_clicking_should_raise_onSelect_event() {

            var item = getRootItem(1);

            isRaised = false;

            item.find('> .t-link').trigger('click');

            assertTrue(isRaised);
        }

        function test_collapse_should_raise_onCollapse_event() {

            var panel = getPanelBar();

            isRaised = false;

            var item = getRootItem(0);
            
            item.find('> .t-link').trigger('click');

            assertTrue(isRaised);
        }
        
        function test_expand_should_raise_onExpand_event() {

            var panel = getPanelBar();
            
            isRaised = false;

            var item = getRootItem(0);

            item.find('> .t-link').trigger('click');

            assertTrue(isRaised);
        }

        function test_expand_should_not_expand_item_is_disabled() {
            var panel = getPanelBar();

            var item = getRootItem(3);

            panel.expand(item);

            assertEquals("none", item.find('> .t-group').css("display"));
        }

        function test_disable_method_should_collapse_group() {
            var panel = getPanelBar();

            var item = getRootItem(3);

            panel.disable(item);

            assertEquals("none", item.find('> .t-group').css("display"));
        }        

        function test_disable_method_should_disable_disabled_item() {
            var panel = getPanelBar();

            var item = getRootItem(2);

            panel.disable(item);

            assertTrue(item.hasClass('t-state-disabled'));
        }

        function test_enable_method_should_enable_disabled_item() {
            var panel = getPanelBar();

            var item = getRootItem(3);

            panel.enable(item);

            assertTrue(item.hasClass('t-state-default'));
        }

        function test_collapse_method_should_collapse_last_item() {
        
            var panel = getPanelBar();

            var item = getRootItem(4);

            panel.collapse(item);

            assertEquals("none", item.find('> .t-group').css("display"));
        }

        function test_expand_method_should_expand_last_item() {
        
            var panel = getPanelBar();

            var item = getRootItem(4);

            panel.expand(item);

            assertEquals("block", item.find('> .t-group').css("display"));
        }

        //handlers
        function Expand(sender, args) {
            isRaised = true;
        }

        function Collapse(sender, args) {
            isRaised = true;
        }

        function Select(sender, args) {
            isRaised = true;
        }

        function Load(sender, args) {
            isRaised = true;
        }
   </script>

</asp:Content>
