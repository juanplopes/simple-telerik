<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTest.Customer>>" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTest" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Paging</h2>
    <%= Html.Telerik().Grid<Telerik.Web.Mvc.JavaScriptTest.Customer>()
            .Name("Grid_DefaultPager")
            .Columns(columns =>
            {
                columns.Bound(c => c.Name);
                columns.Bound(c => c.BirthDate.Day);
            })
            .Ajax(settings => { })
			.BindTo((List<Customer>)ViewData["moreData"])
            .Pageable(pager => pager.PageSize(1))				   
    %>
    <%= Html.Telerik().Grid<Telerik.Web.Mvc.JavaScriptTest.Customer>()
            .Name("Grid_NextPrevAndInputPager")
            .Columns(columns =>
            {
                columns.Bound(c => c.Name);
                columns.Bound(c => c.BirthDate.Day);
            })
            .Ajax(delegate{})
			.BindTo((List<Customer>)ViewData["lessData"])
			.Pageable(pager => pager.PageSize(1).Style(GridPagerStyles.NextPreviousAndInput))
    %>
    <%= Html.Telerik().Grid<Telerik.Web.Mvc.JavaScriptTest.Customer>()
            .Name("Grid_UpdatePager")
            .Columns(columns =>
            {
                columns.Bound(c => c.Name);
                columns.Bound(c => c.BirthDate.Day);
            })
            .Ajax(settings => { })
            .BindTo((List<Customer>)ViewData["moreData"])
			.Pageable()
    %>

    <script type="text/javascript">
        var gridElement;

        function setUp() {
            gridElement = document.createElement("div");
            gridElement.id = "tempGrid";
        }

        function getGrid(selector) {
            return $(selector).data("tGrid");
        }

        function test_grid_object_is_initialized() {
            var grid = getGrid("#Grid_DefaultPager");
            assertNotNull(grid);
            assertNotUndefined(grid);
        }

        function test_grid_colum_names_are_initialized() {
            var grid = getGrid("#Grid_DefaultPager");
            assertEquals("Name", grid.columns[0].member);
            assertEquals("BirthDate.Day", grid.columns[1].member);
        }

        function test_createColumnMappings_maps_data_fields_to_column() {
            var grid = createGrid(gridElement, { columns: [
                { member: "Name" },
                { member: "Id" }
            ]
            
            });

            var dataItem = { Id: 1, Name: "John" };
            grid.createColumnMappings(dataItem);
            var nameMapping = grid.columns[0].display;
            assertNotUndefined(nameMapping);
            assertEquals(dataItem.Name, nameMapping(dataItem));
        }

        function test_createColumnMappings_maps_fields_of_nested_objects() {
            var grid =createGrid(gridElement, { columns: [
	            { member: "NestedObject.Id" }
	        ]
            
            });

            var dataItem = { NestedObject: { Id: 1} };

            grid.createColumnMappings(dataItem);
            var nestedObjectMapping = grid.columns[0].display;
            assertEquals(dataItem.NestedObject.Id, nestedObjectMapping(dataItem));
        }

        function test_bind_populates_from_data() {
            var grid = getGrid("#Grid_DefaultPager");
            var data = [{ Name: "Test", BirthDate: { Day: 1}}];

            grid.dataBind(data);
            assertEquals("Test", $("#Grid_DefaultPager tbody tr:nth-child(1) td:nth-child(1)").text());
            assertEquals("1", $("#Grid_DefaultPager tbody tr:nth-child(1) td:nth-child(2)").text());
        }

        function test_page_size_serialized() {
            var grid = getGrid("#Grid_DefaultPager");
            assertEquals(1, grid.pageSize);
        }

        function test_update_pager_next_disabled_on_last_page() {
            var grid = getGrid("#Grid_NextPrevAndInputPager");
            grid.currentPage = 2;
            grid.updatePager(2);
            assertTrue($("#Grid_NextPrevAndInputPager .t-arrow-next").parent().hasClass("t-state-disabled"));
        }

        function test_total_pages_when_page_size_divides_total_without_remainder() {
            var grid =createGrid(gridElement, { pageSize: 10 });
            grid.total = 20;
            assertEquals(2, grid.totalPages());
        }

        function test_total_pages_when_page_size_divides_total_with_remainder() {
            var grid =createGrid(gridElement, { pageSize: 10 });
            grid.total = 19;
            assertEquals(2, grid.totalPages());
        }

        function test_total_when_total_is_less_than_page_size() {
            var grid =createGrid(gridElement, { pageSize: 10 });
            grid.total = 9;
            assertEquals(1, grid.totalPages());
        }

        function test_total_when_total_is_zero() {
            var grid =createGrid(gridElement, { pageSize: 10 });
            grid.total = 0;
            assertEquals(0, grid.totalPages());
        }

        function test_update_default_pager_sets_the_text() {
            var grid = getGrid("#Grid_NextPrevAndInputPager");
            grid.currentPage = 2;
            grid.updatePager();

            assertEquals("2", $("#Grid_NextPrevAndInputPager .t-pager input[type=text]").val());
        }

        function test_clicking_next_increments_current_page() {

            var grid = getGrid("#Grid_DefaultPager");
            grid.ajaxRequest = function() { };
            $("#Grid_DefaultPager .t-arrow-next").parent().trigger("click");
            assertEquals(2, grid.currentPage);
        }

        function test_clicking_last_set_the_page_to_last() {
            var grid = getGrid("#Grid_NextPrevAndInputPager");
            grid.ajaxRequest = function() { };
            $("#Grid_NextPrevAndInputPager .t-arrow-last").parent().trigger("click");
            assertEquals(2, grid.currentPage);
        }

        function test_current_page_is_one_by_default() {
            var grid = getGrid("#Grid_DefaultPager");
            assertEquals(1, grid.currentPage);
        }

        function test_update_pager_last_button_disabled_on_last_page() {
            var grid = getGrid("#Grid_NextPrevAndInputPager");
            grid.currentPage = 2;
            grid.updatePager(2);
            assertTrue($("#Grid_NextPrevAndInputPager .t-arrow-last").parent().hasClass("t-state-disabled"));
        }

        function test_update_pager_prev_button_enabled_on_last_page() {
            var grid = getGrid("#Grid_DefaultPager");
            grid.currentPage = 2;
            grid.updatePager(2);
            assertFalse($("#Grid_DefaultPager .t-arrow-prev").parent().hasClass("t-state-disabled"));
        }

        function test_update_pager_first_button_enabled_on_last_page() {
            var grid = getGrid("#Grid_DefaultPager");
            grid.currentPage = 2;
            grid.updatePager(2);
            assertFalse($("#Grid_DefaultPager .t-arrow-first").parent().hasClass("t-state-disabled"));
        }

        function test_clicking_prev_goes_to_previous_page() {
            var grid = getGrid("#Grid_DefaultPager");
            grid.ajaxRequest = function() { };
            grid.currentPage = 2;
            grid.updatePager(2);

            $("#Grid_DefaultPager .t-arrow-prev").parent().trigger("click");
            assertEquals(1, grid.currentPage);
        }

        function test_clicking_first_goes_to_first_page() {
            var grid = getGrid("#Grid_DefaultPager");
            grid.ajaxRequest = function() { };
            grid.currentPage = 2;
            grid.updatePager(2);

            $("#Grid_DefaultPager .t-arrow-first").parent().trigger("click");
            assertEquals(1, grid.currentPage);
        }

        function test_firstItemInPage_first_page() {
            var grid =createGrid(gridElement, { currentPage: 1, pageSize: 10, total: 20 });
            assertEquals(1, grid.firstItemInPage());
        }

        function test_firstItemInPage_when_total_is_0() {
            var grid = createGrid(gridElement, { currentPage: 1, pageSize: 10, total: 0 });
            assertEquals(0, grid.firstItemInPage());
        }
        function test_firstItemInPage_second_page() {
            var grid =createGrid(gridElement, { currentPage: 2, pageSize: 10, total: 20 });
            assertEquals(11, grid.firstItemInPage());
        }

        function test_lastItemInPage_first_page() {
            var grid =createGrid(gridElement, { currentPage: 1, pageSize: 10, total: 20 });
            assertEquals(10, grid.lastItemInPage());
        }

        function test_lastItemInPage_last_page() {
            var grid =createGrid(gridElement, { currentPage: 2, pageSize: 10, total: 20 });
            assertEquals(20, grid.lastItemInPage());
        }

        function test_last_itemInPage_last_page_page_size_divides_total_with_remainder() {
            var grid =createGrid(gridElement, { currentPage: 2, pageSize: 10, total: 19 });
            assertEquals(19, grid.lastItemInPage());
        }

        function test_sanitizePage_returns_the_value_if_valid_integer_below_total_pages() {
            var grid =createGrid(gridElement, { currentPage: 2, pageSize: 10, total: 19 });
            assertEquals(1, grid.sanitizePage("1"));
        }

        function test_sanitizePage_returns_one_when_the_value_is_not_a_number() {
            var grid =createGrid(gridElement, { currentPage: 2, pageSize: 10, total: 19 });
            assertEquals(1, grid.sanitizePage("something"));
        }

        function test_sanitizePage_returns_one_when_the_value_is_a_negative_number_or_zero() {
            var grid =createGrid(gridElement, { currentPage: 2, pageSize: 10, total: 19 });
            assertEquals(1, grid.sanitizePage("-1"));
            assertEquals(1, grid.sanitizePage("0"));
        }

        function test_sanitizePage_returns_whole_fraction_when_the_value_is_floating_point() {
            var grid =createGrid(gridElement, { currentPage: 2, pageSize: 10, total: 19 });
            assertEquals(2, grid.sanitizePage("2.5"));
        }

        function test_sanitizePage_returns_total_pages_if_number_is_bigger_than_total_pages() {
            var grid =createGrid(gridElement, { currentPage: 2, pageSize: 10, total: 19 });
            assertEquals(2, grid.sanitizePage("3"));
        }

        function test_pressing_enter_in_pager_text_box_pages() {
            var grid = getGrid("#Grid_NextPrevAndInputPager");
            grid.ajaxRequest = function() { };
            $("#Grid_NextPrevAndInputPager .t-pager input").val("2").trigger({ type: "keydown", keyCode: 13 });
            assertEquals(2, grid.currentPage);
        }

        function test_query_string_parameter_names_are_set_by_default() {
            var grid = getGrid("#Grid_DefaultPager");
            assertEquals("page", grid.queryString.page);
            assertEquals("size", grid.queryString.size);
            assertEquals("orderBy", grid.queryString.orderBy);
        }

        function test_numeric_page_buttons_decreased_when_total_is_less_than_initial() {
            var grid = getGrid("#Grid_UpdatePager");

            grid.total = 10;
            grid.dataBind([]);

            assertEquals(1, $(".t-numeric", grid.element).children().length);
        }

        function test_numeric_pager_when_current_page_is_less_than_total_number_of_numeric_buttons() {
            var grid = getGrid("#Grid_UpdatePager");
            var pager = $(".t-pager .t-numeric", grid.element);


            grid.numericPager(pager[0], 1, 10);
            assertEquals(10, pager.children().length);
            assertEquals('t-state-active', pager.children().eq(0).attr("class"));
        }

        function test_numeric_pager_when_current_page_is_on_the_second_set_of_numeric_page_buttons() {
            var grid = getGrid("#Grid_UpdatePager");
            var pager = $(".t-pager .t-numeric", grid.element);
            grid.numericPager(pager[0], 11, 21);
            assertEquals(12, pager.children().length);
            assertEquals('t-state-active', pager.children().eq(1).attr("class"));
        }

        function test_numeric_pager_when_current_page_is_on_the_third_set_of_numeric_page_buttons() {
            var grid = getGrid("#Grid_UpdatePager");
            var pager = $(".t-pager .t-numeric", grid.element);
            grid.numericPager(pager[0], 21, 31);
            assertEquals(12, pager.children().length);
            assertEquals('t-state-active', pager.children().eq(1).attr("class"));
        }

        function tearDown() {
            gridElement = null;

            $("#Grid_DefaultPager .t-arrow-next").parent().removeClass("t-state-disabled");
            $("#Grid_DefaultPager .t-arrow-last").parent().removeClass("t-state-disabled");
            $("#Grid_DefaultPager .t-arrow-prev").parent().add("t-state-disabled");
            $("#Grid_DefaultPager .t-arrow-first").parent().add("t-state-disabled");
            $("#Grid_DefaultPager .t-pager .t-state-active").removeClass("t-state-active");
            $("#Grid_DefaultPager .t-pager .t-link").eq(0).addClass("t-state-active");

            $("#Grid_NextPrevAndInputPager .t-arrow-next").parent().removeClass("t-state-disabled");
            $("#Grid_NextPrevAndInputPager .t-arrow-last").parent().removeClass("t-state-disabled");
            $("#Grid_NextPrevAndInputPager .t-arrow-prev").parent().add("t-state-disabled");
            $("#Grid_NextPrevAndInputPager .t-arrow-first").parent().add("t-state-disabled");

            $("#Grid_NextPrevAndInputPager .t-pager input[type=text]").val(1);
            $("#Grid_NextPrevAndInputPager .t-status-text").text("Displaying items 1 - 1 of 2");

            getGrid("#Grid_DefaultPager").currentPage = 1;
            getGrid("#Grid_NextPrevAndInputPager").currentPage = 1;
        }
        
        function createGrid(gridElement, options)
        {
             options = $.extend({}, $.fn.tGrid.defaults, options);
             return new $.telerik.grid(gridElement, options);
        }        
    </script>

    <div id="dummyGrid">
    </div>
</asp:Content>
