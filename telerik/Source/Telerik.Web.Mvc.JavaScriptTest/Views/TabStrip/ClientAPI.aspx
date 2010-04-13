<%@ Page Title="ClientAPI tests" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <h2>Client API Tests</h2>
    
	<% Html.Telerik().TabStrip()
            .Name("TabStrip")
            .Items(items =>
            {
                items.Add().Text("Item 1");
                items.Add().Text("Item 2");

                items.Add().Text("Item 3");
                items.Add().Text("Item 4")
                    .Enabled(false);
                items.Add().Text("Item 5");
                items.Add().Text("Item 6")
                    .Content(() => 
                    {
                        %>
                            <p>Content</p>
                        <%
                    });
                 items.Add().Text("Item 7")
                    .Content(() => 
                    {
                        %>
                            <p>Content</p>
                        <%
                    });
            }
            ).ClientEvents(events =>
            {
                events.OnSelect("Select");
                events.OnLoad("Load");
            })
            .Render(); %>
    
   <script type="text/javascript">

       var isRaised;
        
        function getRootItem(index) {
            return $('#TabStrip').find('.t-item').eq(index)
        }
        
        function getTabStrip() {
            return $("#TabStrip").data("tTabStrip");
        }

        function test_loaded_component_should_raise_onLoad_event() {
            assertTrue(isRaised);
        }
    
        function test_clicking_should_raise_onSelect_event() {

            var item = getRootItem(2);

            isRaised = false;

            item.find('> .t-link').trigger('click');

            assertTrue(isRaised);
        }

        function test_clicking_first_item_should_select_it() {
            var item = getRootItem(0);

            item.find('> .t-link').trigger('click');

            assertTrue(item.hasClass('t-state-active'));
        }

        function test_select_method_should_select_second_item() {
            var tabstrip = getTabStrip();
            var item = getRootItem(1);

            tabstrip.select(item);

            assertTrue(item.hasClass('t-state-active'));
        
        }

        function test_disable_method_should_disable_item() {
            var tabstrip = getTabStrip();

            var item = getRootItem(4);

            tabstrip.disable(item);

            assertTrue(item.hasClass('t-state-disabled'));
        }

        function test_enable_method_should_enable_disabled_item() {
            var tabstrip = getTabStrip();

            var item = getRootItem(3);

            tabstrip.enable(item);

            assertTrue(item.hasClass('t-state-default'));
        }

        function test_select_method_should_show_content() {
            var tabstrip = getTabStrip();

            var item = getRootItem(5);
            tabstrip.select(item);

            var content = tabstrip.getContentElement($('#TabStrip').find('.t-content'), 5);
            assertTrue(content.hasClass('t-state-active'));
        }

        var argsCheck = false;
        function test_click_should_raise_select_event_and_pass_corresponding_content() {
            argsCheck = true;

            var item = getRootItem(6);

            isRaised = false;

            item.find('> .t-link').trigger('click');

            assertTrue(isRaised);
        }
        
        //handlers
        function Select(e) {
            if (argsCheck) {
                if (e.contentElement) {
                    isRaised = true;
                } else {
                    isRaised = false;
                }
                argsCheck = false;
            } else {
                isRaised = true;
            }
        }

        function Load(e) {
            isRaised = true;
        }
   </script>

</asp:Content>
