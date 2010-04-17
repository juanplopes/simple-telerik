<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTest.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Binding</h2>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid1")
            .Columns(columns =>columns.Bound(c => c.Name))
            .Ajax(settings => { })
            .Pageable(pager => pager.PageSize(10))
    %>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid2")
            .Columns(columns =>
            {
                columns.Bound(c => c.Name).HtmlAttributes(new { dir="rtl" });
                columns.Bound(c => c.Name);
            })
            .Ajax(settings => { })
            .Pageable(pager => pager.PageSize(10))
    %>

    <script type="text/javascript">
        
        function tearDown() {
            var grid = getGrid();
            
            $("tbody tr", grid.element).remove();
            for (var i = 0; i < 10; i++)
                $("<tr><td/></tr>").appendTo($("tbody", grid.element));
        }

        function getGrid(selector) {
            return $(selector || "#Grid1").data("tGrid");
        }

        function test_should_removes_rows_when_data_length_is_less_than_page_size() {
            var grid = getGrid();
            grid.dataBind([{}, {}]);

            assertEquals(2, $("tbody tr", grid.element).length);
        }

        function test_should_create_rows_up_to_page_size_when_they_dont_exist() {
            var grid = getGrid();
            $("tbody tr", grid.element).remove();
            grid.dataBind([{}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}]);
            assertEquals(10, $("tbody tr", grid.element).length);
        }
        
        function test_should_bind_columns_with_same_name() {
            var grid = getGrid("#Grid2");

            grid.dataBind([{ Name: "Test"}]);
            
            assertEquals("Test", $("tbody tr td:last", grid.element).html());
            assertEquals("Test", $("tbody tr:last td:first", grid.element).html());
        }
        
        
        function test_should_apply_alt_style() {
            var grid = getGrid();
            $("tbody tr", grid.element).remove();
            grid.dataBind([{}, {}]);
            assertEquals(2, $("tbody tr", grid.element).length);
            assertEquals("t-alt", $("tbody tr:nth-child(2)", grid.element).attr("class"));
        }
        
        function test_should_serialize_attributes() {
            var grid = getGrid("#Grid2");
            
            assertEquals(' dir="rtl"', grid.columns[0].attr);
        }
        
        function test_should_apply_column_html_attributes() {
            var grid = getGrid('#Grid2');
            $('tbody tr', grid.element).remove();
            grid.dataBind([{}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}]);
            assertEquals('rtl', $('#Grid2 tbody tr:first td:first').attr('dir'));
        }

        function test_rebind_multiple_arguments() {
            var grid = getGrid('#Grid2');
            grid.ajaxRequest = function() { }
            grid.rebind({a:1,b:2});
            assertEquals(location.href.replace(/(http:\/\/.*?\/)/, "/") + '&a=1&b=2', grid.ajax.selectUrl);
        }
    </script>

</asp:Content>
