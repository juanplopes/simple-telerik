<%@ Page Title="CollapseDelay Tests" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        CollapseDelay Tests</h2>
    <% Html.Telerik().Menu()
            .Name("myMenu")
            .OpenOnClick(true)
            .Items(menu =>
            {
                menu.Add().Text("Item 1")
                    .Items(item =>
                    {
                        item.Add().Text("Child Item 1.1");
                        item.Add().Text("Child Item 1.2");
                        item.Add().Text("Child Item 1.3");
                        item.Add().Text("Child Item 1.4");
                    });
                menu.Add().Text("Item 2")
                    .Items(item =>
                    {
                        item.Add().Text("Child Item 2.1");
                        item.Add().Text("Child Item 2.2");
                        item.Add().Text("Child Item 2.3");
                        item.Add().Text("Child Item 2.4");
                    });
                menu.Add().Text("Item 4")
                    .Items(item =>
                    {
                        item.Add().Text("Child Item 4.1")
                            .Items(subitem =>
                            {
                                subitem.Add().Text("Grand Child Item 4.1.1");
                                subitem.Add().Text("Grand Child Item 4.1.2");
                            });

                        item.Add().Text("Child Item 4.2");
                    });
                menu.Add().Text("Item 5");
            }
            )
            .Render(); %>

    <script type="text/javascript">

        function getMenu(selector) {
            return $(selector || "#myMenu").data("tMenu");
        }

        var open;
        var close;
        var menu;

        function setUp() {
            menu = getMenu();
            open = menu.open;
            close = menu.close;
        }

        function tearDown() {
            menu.clicked = false;
            menu.open = open;
            menu.close = close;
        }


        function test_open_on_click_is_serialized() {
            assertTrue(menu.openOnClick);
        }

        function test_hovering_root_item_does_not_open_it() {
            var opend = false;

            menu.open = function() { opend = true }
            menu.mouseenter({}, $("li:first", menu.element)[0]);

            assertFalse(opend);
        }

        function test_clicking_root_item_should_open_it() {
            var opend = false;
            menu.open = function() { opend = true }
            menu.click({preventDefault:function(){}}, $("li:first", menu.element)[0]);
            assertTrue(opend);
            assertTrue(menu.clicked);
        }

        function test_leaving_opened_item_does_not_close_it() {
            var opend = false;
            menu.clicked = true;
            menu.open = function() { opend = true }

            menu.mouseleave({}, $("li:first", menu.element)[0]);

            assertFalse(opend);
        }

        function test_leaving_opened_and_hovering_a_sibling_closes_it_and_opens_the_sibling() {
            var opened = false;
            menu.clicked = true;
            menu.open = function() { opend = true }

            var element = $("li:first", menu.element)[0];
            menu.mouseenter({ relatedTarget: element, indexOf: function() { }, type:'mouseenter' }, element.nextSibling);

            assertTrue(opend);
        }

        function test_clicking_the_document_closes_the_open_item() {
            var closed = false;
            menu.clicked = true;
            menu.close = function() { closed = true }
            menu.documentClick({ target: document.body}, document);
            assertTrue(closed);
        }
	
    </script>

</asp:Content>
