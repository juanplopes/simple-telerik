<%@ Page Title="Ajax Loading Of Content Tests" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Ajax Loading Of Content Tests</h2>
    <% Html.Telerik().PanelBar()
            .Name("myPanelBar")
            .Items(parent =>
            {
                parent.Add().Text("Item 0")
                            .Content(() =>
                            {%>
                                <p>
                                    Lipsum!!!</p>
                            <%})
                            .Expanded(true);
                
                parent.Add().Text("Item 1")
                            .Content(() =>
                            {%>
                                <p>
                                    Lipsum!!!</p>
                            <%})
                            .Expanded(true);

                parent.Add().Text("Item 2")
                            .Content(() =>
                            {%>
                                <p>
                                    Lipsum!!!</p>
                            <%})
                            .Expanded(false);
                parent.Add().Text("Item 3")
                            .Content(() =>
                                        {%>
                                <p>
                                    Lipsum!!!</p>
                                <%})
                            .Expanded(false);

                parent.Add().Text("Item 4")
                            .LoadContentFrom(Url.Action("AjaxView1", "PanelBar"));

                parent.Add().Text("Item 5")
                            .LoadContentFrom(Url.Action("AjaxView2", "PanelBar"));
                parent.Add().Text("Item 6")
                            .Content(() =>
                            {%>
                                <p>
                                    Lipsum!!!</p>
                            <%});
            }) 
            .Effects(effects=>effects.Toggle())
            .Render(); %>

    <script type="text/javascript">

        function getRootItem(index) {
            return $('#myPanelBar').find('.t-header').eq(index)
        }

        function test_clicking_expanded_content_items_should_collapse_them() {
        
            var item = getRootItem(0);

            item.trigger('click');
            
            assertEquals('none', item.parent().find('.t-content').css("display"));
        }

        function test_clicking_expanded_content_items_should_toggle_arrow() {
            var item = getRootItem(1);

            item.trigger('click');

            assertTrue(item.parent().find('.t-icon').hasClass('t-arrow-down'));
        }

        function test_clicking_collapsed_content_items_should_expand_them() {
            var item = getRootItem(2);

            item.trigger('click');

            assertEquals("block", item.parent().find('.t-content').css("display"));
        }

        function test_clicking_collapsed_content_items_should_toggle_arrow() {
            var item = getRootItem(3);

            item.trigger('click');

            assertTrue(item.parent().find('.t-icon').hasClass('t-arrow-up'));
        }

        function test_clicking_should_make_item_active() {

            var item = getRootItem(6);
            
            item.trigger('click');

            assertTrue(item.parent().hasClass('t-state-active'));
        }

    </script>

</asp:Content>
