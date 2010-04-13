<%@ Page Title="CollapseDelay Tests" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Expand / Collapse Tests</h2>
    <% Html.Telerik().TreeView()
            .Name("myTreeView")
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
                    .Expanded(false);
                panelbar.Add().Text("Item 3")
                    .Items(item =>
                    {
                        item.Add().Text("Child Item 3.1");
                        item.Add().Text("Child Item 3.2");
                    })
                    .Expanded(false);
                panelbar.Add().Text("Item 4")
                    .Items(item =>
                    {
                        item.Add().Text("Child Item 4.1");
                        item.Add().Text("Child Item 4.2");
                    })
                    .Expanded(true);
                panelbar.Add().Text("Item 4")
                    .Items(item =>
                    {
                        item.Add().Text("Child Item 4.1");
                        item.Add().Text("Child Item 4.2");
                    })
                    .Expanded(true);
            }
            )
            .Effects(fx => fx.Toggle())
            .Render(); %>

    <script type="text/javascript">
        var treeView;
        
        function getTreeView(selector) {
            return $(selector || "#myTreeView").data("tTreeView");
        }
        
        function setUp() {
            treeView = getTreeView();
        }
        
        function test_clicking_collapsed_item_not_expand_if_it_is_disabled() {

            var item = $("ul li:nth-child(1)", treeView.element);

            item.find('.t-plus')
                .toggleClass('t-plus-disabled', true)
                .trigger('click');

            assertEquals("none", item.find('.t-group').css("display"));
        }

        function test_clicking_plus_icon_should_toggle_minus() {
            var item = $("ul li:nth-child(2)", treeView.element);

            item.find('.t-plus')
                .trigger('click');

            assertTrue(item.find('.t-icon').hasClass('t-minus'));
        }

        function test_clicking_plus_items_should_expand_them() {
            var item = $("ul li:nth-child(3)", treeView.element);

            item.find('.t-plus')
                .trigger('click');

            assertEquals("block", item.find('.t-group').css("display"));
        }

        function test_clicking_minus_icon_should_toggle_plus() {
            var item = $("ul li:nth-child(4)", treeView.element);

            item.find('.t-minus')
                .trigger('click');

            assertTrue(item.find('.t-icon').hasClass('t-plus'));
        }

        function test_clicking_minus_items_should_collaspe_them() {
            var item = $("ul li:nth-child(5)", treeView.element);

            item.find('.t-minus')
                .trigger('click');

            assertEquals("none", item.find('.t-group').css("display"));
        }
        
        
    </script>

</asp:Content>
