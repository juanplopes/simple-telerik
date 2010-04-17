<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTest.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Filtering</h2>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid1")
            .Columns(columns =>
            {
                columns.Bound(c => c.Name);
                columns.Bound(c => c.BirthDate.Date).Format("{0:d}");
                columns.Bound(c => c.BirthDate.Day);
                columns.Bound(c => c.Active);
                columns.Bound(c => c.BirthDate.DayOfWeek).Filterable(false);
                columns.Bound(c => c.BirthDate.DayOfYear);
                columns.Bound(c => c.Gender);
            })
            .Ajax(settings => { })
            .Pageable()
            .Filterable()
    %>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid2")
            .Columns(columns =>
            {
                columns.Bound(c => c.Name);
                columns.Bound(c => c.BirthDate.Date).Format("{0:d}");
                columns.Bound(c => c.BirthDate.Day);
                columns.Bound(c => c.Active);
                columns.Bound(c => c.BirthDate.DayOfWeek).Filterable(false);
                columns.Bound(c => c.BirthDate.DayOfYear);
                columns.Bound(c => c.Gender);
            })
            .Ajax(settings => { })
            .Pageable()
            .Filterable(filtering => filtering.Filters(filters =>
                {
                    filters.Add(c => c.BirthDate.DayOfYear).IsGreaterThanOrEqualTo(1).And().IsLessThanOrEqualTo(1);
                    filters.Add(c => c.Name).Contains("Customer1").And().EndsWith("Customer1");
                    filters.Add(c => c.Active).IsEqualTo(false);
                    filters.Add(c => c.Gender).IsNotEqualTo(Telerik.Web.Mvc.JavaScriptTest.Gender.Male).And().IsNotEqualTo(Telerik.Web.Mvc.JavaScriptTest.Gender.Male);
                }))
    %>
        <script type="text/javascript">
    
        function getGrid(selector) {
            return $(selector || "#Grid1").data("tGrid");
        }

        function setUp() {
            getGrid().ajaxRequest = function() {};
            getGrid('').ajaxRequest = function() {};
        }
        function tearDown() {
            var grid = getGrid();
            $.each(grid.columns, function() { this.filters = null });
            $('.t-grid-filter', grid.element).removeClass('t-active-filter');
            grid.filterBy = null;
        }
        
        function test_single_filter_expr_for_one_column() {
            var grid = getGrid();
            
            grid.columns[0].filters = [
            {
                operator: "eq",
                value:"10"
            }];
            
            assertEquals("Name~eq~'10'", grid.filterExpr())
        }
        
        function test_filter_expr_for_bool_column() {
            var grid = getGrid();
            
            grid.columns[3].filters = [
            {
                operator: "eq",
                value:true
            }];
            
            assertEquals("Active~eq~true", grid.filterExpr())
        }

        function test_filter_expr_for_enum_column() {
            var grid = getGrid();

            grid.columns[grid.columns.length - 1].filters = [
            {
                operator: "eq",
                value: 'Female'
            }];

            assertEquals("Gender~eq~'Female'", grid.filterExpr())
        }
        
        function test_single_filter_expr_for_one_column_when_operator_is_function() {
            var grid = getGrid();
            
            grid.columns[0].filters = [
            {
                operator: "startswith",
                value:"10"
            }];
            
            assertEquals("startswith(Name,'10')", grid.filterExpr())
        }
        
        function test_two_filters_for_one_column() {
            var grid = getGrid();
            
            grid.columns[0].filters = [{
                operator: "startswith",
                value:"10"
            },
            {
                operator: "eq",
                value:"10"
            }];
            
            assertEquals("startswith(Name,'10')~and~Name~eq~'10'", grid.filterExpr())
        }
        
        function test_encode_number() {
            var grid = getGrid();
            var encoded = grid.encodeFilterValue(2, 10);
            assertEquals(10, encoded);
        }
        
        function test_encode_string() {
            var grid = getGrid();
            var encoded = grid.encodeFilterValue(0, "test");
            assertEquals("'test'", encoded);
        }
        
        function test_escape_quotes_in_string() {
            var grid = getGrid();
            var encoded = grid.encodeFilterValue(0, "t'est");
            assertEquals("'t''est'", encoded);
        }
        
        function test_encode_date() {
            var grid = getGrid();
            
            var encoded = grid.encodeFilterValue(1, '10/11/2000');
            assertEquals("datetime'2000-10-11T12-00-00'", encoded);
        }
        
        function test_validate_number_fails_when_string() {
            var grid = getGrid();
            assertFalse(grid.isValidFilterValue(grid.columns[2], "string"));
        }
        
        function test_validate_number_succeeds_when_whole_number() {
            var grid = getGrid();
            assertTrue(grid.isValidFilterValue(grid.columns[2], "1"));
        }
        
        function test_validate_number_succeeds_when_decimal_number() {
            var grid = getGrid();
            assertTrue(grid.isValidFilterValue(grid.columns[2], "3.14"));
        }
        
        function test_validate_number_succeeds_when_decimal_number_without_whole_part() {
            var grid = getGrid();
            assertTrue(grid.isValidFilterValue(grid.columns[2], ".14"));
        }
        
        function test_validate_number_succeeds_when_positive_number() {
            var grid = getGrid();
            assertTrue(grid.isValidFilterValue(grid.columns[2], "+1"));
        }
        
        function test_validate_number_succeeds_when_negative_number() {
            var grid = getGrid();
            assertTrue(grid.isValidFilterValue(grid.columns[2], "-1"));
        }
        
        function test_validate_string_succeeds_always() {
            var grid = getGrid();
            assertTrue(grid.isValidFilterValue(grid.columns[0], "anything"));
        }

        function test_filtering_applies_filtered_css() {
            var grid = getGrid();
            grid.columns[0].filters = [{ operator: 'eq', value: 'test'}]
            grid.filter(grid.filterExpr());
            assertTrue($('th:contains("Name")', grid.element).find('.t-grid-filter').hasClass('t-active-filter'));
        }

        function test_filtering_applies_filtered_css_to_one_column_only() {
            var grid = getGrid();
            grid.columns[0].filters = [ {operator:'eq', value:'test' }]
            grid.filter(grid.filterExpr());
            assertEquals(1, $('.t-active-filter', grid.element).length);
        }

        function test_filtering_applies_filtered_css_with_columns_which_are_not_filterable() {
            var grid = getGrid();
            grid.columns[5].filters = [{ operator: 'eq', value: 'test'}]
            grid.filter(grid.filterExpr());
            assertTrue($('th:contains("Day Of Year")', grid.element).find('.t-grid-filter').hasClass('t-active-filter'));
        }

        function test_clearing_boolean_filter_unchecks_radio_buttons() {
            var grid = getGrid();
            $('th:contains("Active") .t-filter', grid.element).click();
            $('.t-filter-options input[type="radio"]:first', grid.element).click();
            $('.t-filter-options .t-clear-button', grid.element).click();
            assertEquals(0, $('.t-filter-options input:checked', grid.element).length);
        }


        function test_column_format_propagates_to_datepicker() {
            var grid = getGrid();
            $('th:contains("Date") .t-filter', grid.element).click();

            var datePicker = $('input[id^=Grid1BirthDate]', grid.element).parent().data('tDatePicker');

            assertEquals('d', datePicker.format);
        }

        function test_column_filter_values_serialized_for_enum() {
            var column = getGrid().columns[getGrid().columns.length - 1];

            assertEquals('Enum', column.type);
            assertEquals(0, column.values.Female);
            assertEquals(1, column.values.Male);
        }

        function test_enum_filter_ui() {
            var grid = getGrid();
            $('th:contains("Gender")', grid.element).find('.t-grid-filter').click();

            assertEquals(4, $('.t-filter-options:last select', grid.element).length);
            assertEquals(grid.localization.filterSelectValue, $('.t-filter-options:last select:eq(1) option:eq(0)', grid.element).text());
            assertEquals('Female', $('.t-filter-options:last select:eq(1) option:eq(1)', grid.element).text());
            assertEquals('Male', $('.t-filter-options:last select:eq(1) option:eq(2)', grid.element).text());
            assertEquals(grid.localization.filterSelectValue, $('.t-filter-options:last select:eq(3) option:eq(0)', grid.element).text());
            assertEquals('Female', $('.t-filter-options:last select:eq(3) option:eq(1)', grid.element).text());
            assertEquals('Male', $('.t-filter-options:last select:eq(3) option:eq(2)', grid.element).text());
        }

        function test_ajax_style_date_parsing() {
            assertEquals("datetime'2010-02-10T12-00-00'", getGrid().encodeFilterValue(1, "\/Date(1265752800000)\/"));
        }

        function test_filter_ui_prefilled_with_filter_values_for_numeric_filter() {
            var grid = getGrid('#Grid2');
            $('th:contains("Day Of Year")', grid.element).find('.t-grid-filter').click();

            assertEquals('ge', $('.t-filter-options:last select:eq(0)', grid.element).val());
            assertEquals('1', $('.t-filter-options:last input:eq(0)', grid.element).val());

            assertEquals('le', $('.t-filter-options:last select:eq(1)', grid.element).val());
            assertEquals('1', $('.t-filter-options:last input:eq(1)', grid.element).val());
        }

        function test_filter_ui_prefilled_with_filter_values_for_string_filter() {
            var grid = getGrid('#Grid2');
            $('th:contains(Name)', grid.element).find('.t-grid-filter').click();

            assertEquals('substringof', $('.t-filter-options:last select:eq(0)', grid.element).val());
            assertEquals('Customer1', $('.t-filter-options:last input:eq(0)', grid.element).val());

            assertEquals('endswith', $('.t-filter-options:last select:eq(1)', grid.element).val());
            assertEquals('Customer1', $('.t-filter-options:last input:eq(1)', grid.element).val());
        }

        function test_filter_ui_prefilled_with_filter_values_for_boolean_filter() {
            var grid = getGrid('#Grid2');
            $('th:contains(Active)', grid.element).find('.t-grid-filter').click();

            assertEquals('false', $('.t-filter-options:last :checked', grid.element).val());
        }

        function test_filter_ui_prefilled_with_filter_values_for_enum_filter() {
            var grid = getGrid('#Grid2');
            $('th:contains(Gender)', grid.element).find('.t-grid-filter').click();

            assertEquals('ne', $('.t-filter-options:last select:eq(0)', grid.element).val());
            assertEquals('1', $('.t-filter-options:last select:eq(1)', grid.element).val());
            assertEquals('ne', $('.t-filter-options:last select:eq(2)', grid.element).val());
            assertEquals('1', $('.t-filter-options:last select:eq(3)', grid.element).val());
        }
        
        </script>
    
</asp:Content>
