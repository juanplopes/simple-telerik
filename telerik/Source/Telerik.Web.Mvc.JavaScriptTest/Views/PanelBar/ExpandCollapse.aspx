<%@ Page Title="CollapseDelay Tests" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Expand / Collapse Tests</h2>
    <% Html.Telerik().PanelBar()
            .Name("myPanelBar")
            .Items(panelbar =>
            {
                panelbar.Add().Text("Item 1")
                    .Items(item =>
                    {
                        item.Add().Text("Child Item 1.1");
                        item.Add().Text("Child Item 1.2");
                        item.Add().Text("Child Item 1.3");
                        item.Add().Text("Child Item 1.4");
                    })
                    .Expanded(false);
                panelbar.Add().Text("Item 2")
                    .Items(item =>
                    {
                        item.Add().Text("Child Item 3.1");
                        item.Add().Text("Child Item 3.2");
                        item.Add().Text("Child Item 3.3");
                        item.Add().Text("Child Item 3.4");
                    })
                    .Expanded(true);
                panelbar.Add().Text("Item 3")
                    .Items(item =>
                    {
                        item.Add().Text("Child Item 3.1");
                        item.Add().Text("Child Item 3.2");
                    });
                panelbar.Add().Text("Item 4")
                    .Items(item =>
                    {
                        item.Add().Text("Child Item 4.1");
                        item.Add().Text("Child Item 4.2");
                    });
                panelbar.Add().Text("Item 5")
                    .Items(item =>
                    {
                        item.Add().Text("Child Item 5.1")
                            .Items(childitem =>
                            {
                                childitem.Add().Text("Grand Child Item 5.1.1");
                                childitem.Add().Text("Grand Child Item 5.1.2");
                            });
                    });
                panelbar.Add().Text("Item 6")
                    .Items(item =>
                    {
                        item.Add().Text("Child Item 6.1");
                    })
                    .Expanded(true);

                panelbar.Add().Text("Item 7")
                    .Items(item =>
                    {
                        item.Add().Text("Child Item 7.1");
                    });
            }
            )
            .Effects(fx => fx.Toggle())
            .Render(); %>

    <script type="text/javascript">

        function getRootItem(index) {
            return $('#myPanelBar').find('.t-header').parent().eq(index)
        }

        function test_clicking_collapsed_item_not_expand_if_it_is_disabled() {
        
            var item = getRootItem(0);
            
            item
                .toggleClass('t-state-default', false)
				.toggleClass('t-state-disabled', true);

            item.find('> .t-link').trigger('click');
            
            assertEquals("none", item.find('.t-group').css("display"));
        }

        function test_clicking_expanded_items_should_toggle_arrow() {
            var item = getRootItem(1);

            item.find('> .t-link').trigger('click');

            assertTrue(item.find('.t-icon').hasClass('t-arrow-down'));
        }

        function test_clicking_collapsed_items_should_expand_them() {
            var item = getRootItem(2);

            item.find('> .t-link').trigger('click');

            assertEquals("block", item.find('.t-group').css("display"));
        }

        function test_clicking_collapsed_items_should_toggle_arrow() {
            var item = getRootItem(3);

            item.find('> .t-link').trigger('click');

            assertTrue(item.find('.t-icon').hasClass('t-arrow-up'));
        }

        function test_clicking_collapsed_items_should_not_expand_child_groups() {
            var item = getRootItem(4);

            item.find('> .t-link').trigger('click');

            assertEquals("none", item.find('.t-group .t-group').css("display"));
        }

        function test_clicking_child_group_items_should_not_collapse_root_group() {
            var item = getRootItem(5);

            item.find('.t-item').trigger('click');

            assertEquals("block", item.find('.t-group').css("display"));
        }

        function test_clicking_arrows_toggles_child_groups() {
            var item = getRootItem(6);

            item.find('> .t-link > .t-icon').trigger('click');

            assertEquals("block", item.find('.t-group').css("display"));
        }
    </script>

</asp:Content>
