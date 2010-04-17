<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTest.Customer>>" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTest" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Fast navigation</h2>
    
    <script type="text/javascript">
        var calendarObject;
        var $t;

        function setUp() {
            $t = $.telerik;
            calendarObject =
                $($.telerik.calendar.html(new $t.datetime(), new $t.datetime()))
                    .appendTo(document.body)
                    .tCalendar()
                    .data('tCalendar');
                    
            calendarObject.stopAnimation = true;
        }

        function test_goToView_updates_viewedMonth() {
            calendarObject.goToView(0, new $t.datetime(2010, 3, 1));
            
            assertEquals(2010, calendarObject.viewedMonth.year());
            assertEquals(3, calendarObject.viewedMonth.month());
        }
        
        function test_getFirstVisibleDay_honors_DST() {
            
            // 28. March will be skipped if the DST isn't honored
            assertTrue(28 == $t.datetime.firstVisibleDay(new $t.datetime(2010, 3, 1)).date());
        }

        function test_buildDateRows_honors_DST() {
            
            // DST on october 2009 will produce the 25th twice, if calcluations are wrong
            var html = $.telerik.calendar.views[0].body(new $t.datetime(2009, 9, 26),
                                                        calendarObject.minDate,
                                                        calendarObject.maxDate, 
                                                        new $t.datetime(2009, 9, 26));
            
            var temporaryDom =
                $(html)
                    .appendTo(document.body)
                    .find('.t-link')
                    .filter(function() {
                        return parseInt($(this).text(), 10) == 25; 
                    });
            
            assertTrue(temporaryDom.length == 1);
            
            temporaryDom.remove();
        }

        function test_buildDateRows_should_render_days_without_links_if_they_are_out_of_range() {
            var html = $.telerik.calendar.views[0].body(new $t.datetime(2009, 9, 26),
                                                        new $t.datetime(2009, 9, 10),
                                                        new $t.datetime(2009, 9, 30),
                                                        new $t.datetime(2009, 9, 26));

            var renderedDays = $('.t-link', html).length;
            
            assertTrue(renderedDays == 21);
        }

        function test_buildDateRows_should_render_days_with_URL_to_Action_if_dates_are_in_the_current_month() {

            var html = $.telerik.calendar.views[0].body(new $t.datetime(2009, 9, 26),
                                                        new $t.datetime(2009, 9, 10),
                                                        new $t.datetime(2009, 9, 30),
                                                        new $t.datetime(2009, 9, 26),
                                                        '/aspnet-mvc-beta/calendar/selectaction?date={0}',
                                                        { '2009': { '9': [15, 21, 22]} });

            var renderedDays = $('.t-link', html).filter(function(index) {
                return $(this).attr('href').indexOf('selectaction') != -1;
            })
            assertTrue(renderedDays.length == 3)
        }

        function test_buildDateRows_should_render_all_days_with_Url_to_Action_if_no_dates_passed() {
            var html = $.telerik.calendar.views[0].body(new $t.datetime(2009, 9, 26),
                                                        new $t.datetime(2009, 9, 10),
                                                        new $t.datetime(2009, 9, 30),
                                                        new $t.datetime(2009, 9, 26),
                                                        '/aspnet-mvc-beta/calendar/selectaction?date={0}');
            var renderedDays = $('.t-link', html);

            var days_with_URL = renderedDays.filter(function(index) {
                                    return $(this).attr('href') != '#';
                                })

            assertTrue(renderedDays.length == days_with_URL.length)
        }

        function test_buildDateRows_should_render_all_days_with_Url_and_t_action_link_class() {
            var html = $.telerik.calendar.views[0].body(new $t.datetime(2009, 9, 26),
                                                        new $t.datetime(2009, 9, 10),
                                                        new $t.datetime(2009, 9, 30),
                                                        new $t.datetime(2009, 9, 26),
                                                        '/aspnet-mvc-beta/calendar/selectaction?date={0}');
            var renderedDays = $('.t-link', html);

            var days_with_URL = $('.t-link', html).filter(function(index) {
                return $(this).attr('href') != '#';
            })

            assertTrue($(days_with_URL[0]).hasClass('t-action-link'))
        }

        function test_if_focusedDate_has_same_month_as_min_date_prev_arrow_should_be_disabled() {

            calendarObject.minDate = new $t.datetime(2009, 9, 10);
            calendarObject.selectedDate = new $t.datetime(2009, 9, 11);
            calendarObject.goToView(0, new $t.datetime(2009, 9, 1));

            assertTrue($('.t-nav-prev', calendarObject.element).hasClass('t-state-disabled'));
        }
        
        function test_if_focusedDate_has_same_month_as_min_date_prev_arrow_should_be_disabled_year_view() {

            calendarObject.minDate = new $t.datetime(2009, 9, 10);
            calendarObject.selectedDate = new $t.datetime(2009, 9, 11);
            calendarObject.goToView(1, new $t.datetime(2009, 9, 1));

            assertTrue($('.t-nav-prev', calendarObject.element).hasClass('t-state-disabled'));
        }
        
        function test_if_focusedDate_has_same_month_as_min_date_prev_arrow_should_be_disabled_decade_view() {

            calendarObject.minDate = new $t.datetime(2009, 9, 10);
            calendarObject.selectedDate = new $t.datetime(2009, 9, 11);
            calendarObject.goToView(2, new $t.datetime(2009, 9, 1));

            assertTrue($('.t-nav-prev', calendarObject.element).hasClass('t-state-disabled'));
        }
        
        function test_if_focusedDate_has_same_month_as_min_date_prev_arrow_should_be_disabled_century_view() {

            calendarObject.minDate = new $t.datetime(2009, 9, 10);
            calendarObject.selectedDate = new $t.datetime(2009, 9, 11);
            calendarObject.goToView(3, new $t.datetime(2009, 9, 1));

            assertTrue($('.t-nav-prev', calendarObject.element).hasClass('t-state-disabled'));
        }

        function test_if_focusedDate_has_same_month_as_max_date_next_arrow_should_be_disabled() {

            calendarObject.maxDate = new $t.datetime(2009, 9, 30);
            calendarObject.selectedDate = new $t.datetime(2009, 9, 11);
            calendarObject.goToView(0, new $t.datetime(2009, 9, 1));

            assertTrue($('.t-nav-next', calendarObject.element).hasClass('t-state-disabled'));
        }
        
        function test_if_focusedDate_has_same_month_as_max_date_next_arrow_should_be_disabled_year_view() {

            calendarObject.maxDate = new $t.datetime(2009, 9, 30);
            calendarObject.selectedDate = new $t.datetime(2009, 9, 11);
            calendarObject.goToView(1, new $t.datetime(2009, 9, 1));

            assertTrue($('.t-nav-next', calendarObject.element).hasClass('t-state-disabled'));
        }
        function test_if_focusedDate_has_same_month_as_max_date_next_arrow_should_be_disabled_decade_view() {

            calendarObject.maxDate = new $t.datetime(2009, 9, 30);
            calendarObject.selectedDate = new $t.datetime(2009, 9, 11);
            calendarObject.goToView(2, new $t.datetime(2009, 9, 1));

            assertTrue($('.t-nav-next', calendarObject.element).hasClass('t-state-disabled'));
        }
        function test_if_focusedDate_has_same_month_as_max_date_next_arrow_should_be_disabled_century_view() {

            calendarObject.maxDate = new $t.datetime(2009, 9, 30);
            calendarObject.selectedDate = new $t.datetime(2009, 9, 11);
            calendarObject.goToView(3, new $t.datetime(2009, 9, 1));

            assertTrue($('.t-nav-next', calendarObject.element).hasClass('t-state-disabled'));
        }

        function test_if_viewMonth_has_same_month_as_min_date_prev_arrow_should_be_disabled() {

            calendarObject.minDate = new $t.datetime(2009, 9, 10);
            calendarObject.selectedDate = new $t.datetime(2009, 9, 11);
            calendarObject.navigateVertically(0, new $t.datetime(2009, 9, 1), false, $('<a class="t-link"></a>'));
            
            assertTrue($('.t-nav-prev', calendarObject.element).hasClass('t-state-disabled'));
        }

        function test_if_viewMonth_has_same_month_as_min_date_prev_arrow_should_be_disabled_year_view() {

            calendarObject.minDate = new $t.datetime(2009, 9, 10);
            calendarObject.selectedDate = new $t.datetime(2009, 9, 11);
            calendarObject.navigateVertically(1, new $t.datetime(2009, 9, 1), false, $('<a class="t-link"></a>'));

            assertTrue($('.t-nav-prev', calendarObject.element).hasClass('t-state-disabled'));
        }

        function test_if_viewMonth_has_same_month_as_min_date_prev_arrow_should_be_disabled_decade_view() {

            calendarObject.minDate = new $t.datetime(2009, 9, 10);
            calendarObject.selectedDate = new $t.datetime(2009, 9, 11);
            calendarObject.navigateVertically(2, new $t.datetime(2009, 9, 1), false, $('<a class="t-link"></a>'));

            assertTrue($('.t-nav-prev', calendarObject.element).hasClass('t-state-disabled'));
        }

        function test_if_viewMonth_has_same_month_as_min_date_prev_arrow_should_be_disabled_century_view() {

            calendarObject.minDate = new $t.datetime(2009, 9, 10);
            calendarObject.selectedDate = new $t.datetime(2009, 9, 11);
            calendarObject.navigateVertically(3, new $t.datetime(2009, 9, 1), false, $('<a class="t-link"></a>'));

            assertTrue($('.t-nav-prev', calendarObject.element).hasClass('t-state-disabled'));
        }

        function test_if_viewMonth_has_same_month_as_max_date_next_arrow_should_be_disabled() {
            calendarObject.maxDate = new $t.datetime(2009, 9, 30);
            calendarObject.selectedDate = new $t.datetime(2009, 9, 11);
            calendarObject.navigateVertically(0, new $t.datetime(2009, 9, 1), false, $('<a class="t-link"></a>'));

            assertTrue($('.t-nav-next', calendarObject.element).hasClass('t-state-disabled'));
        }

        function test_if_viewMonth_has_same_month_as_max_date_next_arrow_should_be_disabled_year_view() {

            calendarObject.maxDate = new $t.datetime(2009, 9, 30);
            calendarObject.selectedDate = new $t.datetime(2009, 9, 11);
            calendarObject.navigateVertically(1, new $t.datetime(2009, 9, 1), false, $('<a class="t-link"></a>'));

            assertTrue($('.t-nav-next', calendarObject.element).hasClass('t-state-disabled'));
        }

        function test_if_viewMonth_has_same_month_as_max_date_next_arrow_should_be_disabled_decade_view() {

            calendarObject.maxDate = new $t.datetime(2009, 9, 30);
            calendarObject.selectedDate = new $t.datetime(2009, 9, 11);
            calendarObject.navigateVertically(2, new $t.datetime(2009, 9, 1), false, $('<a class="t-link"></a>'));

            assertTrue($('.t-nav-next', calendarObject.element).hasClass('t-state-disabled'));
        }

        function test_if_viewMonth_has_same_month_as_max_date_next_arrow_should_be_disabled_century_view() {

            calendarObject.maxDate = new $t.datetime(2009, 9, 30);
            calendarObject.selectedDate = new $t.datetime(2009, 9, 11);
            calendarObject.navigateVertically(3, new $t.datetime(2009, 9, 1), false, $('<a class="t-link"></a>'));

            assertTrue($('.t-nav-next', calendarObject.element).hasClass('t-state-disabled'));
        }
        
        function test_selected_date_should_render_with_selected_state() {
            var html = $.telerik.calendar.views[0].body(new $t.datetime(2009, 9, 26),
                                                        calendarObject.minDate,
                                                        calendarObject.maxDate, 
                                                        new $t.datetime(2009, 9, 26));

            assertTrue($(html).find('.t-state-selected').length == 1);
        }
        
        function test_date_selection_within_current_month_sets_selected_date_correctly() {
            calendarObject.goToView(0, new $t.datetime(2009, 10, 26));
            
            $(calendarObject.element).find('.t-content td:not(.t-other-month) .t-link').filter(function() {
                return $(this).text() == '3';
            }).eq(0).trigger('click');
            
            assertEquals(2009, calendarObject.selectedDate.year());
            assertEquals(10, calendarObject.selectedDate.month());
            assertEquals(3, calendarObject.selectedDate.date());
        }
        
        function test_date_selection_within_previous_month_sets_selected_date_correctly() {
            calendarObject.goToView(0, new $t.datetime(2009, 10, 26));
            
            $(calendarObject.element).find('.t-content td.t-other-month .t-link').filter(function() {
                return $(this).text() == '29';
            }).eq(0).trigger('click');
            
            assertEquals(2009, calendarObject.selectedDate.year());
            assertEquals(9, calendarObject.selectedDate.month());
            assertEquals(29, calendarObject.selectedDate.date());
        }
        
        function test_date_selection_within_next_month_sets_selected_date_correctly() {
            calendarObject.goToView(0, new $t.datetime(2009, 10, 26));
            
            $(calendarObject.element).find('.t-content td.t-other-month .t-link').filter(function() {
                return $(this).text() == '1';
            }).eq(0).trigger('click');
            
            assertEquals(2009, calendarObject.selectedDate.year());
            assertEquals(11, calendarObject.selectedDate.month());
            assertEquals(1, calendarObject.selectedDate.date());
        }
        
        function test_date_selection_within_next_month_of_next_year_sets_selected_date_correctly() {
            calendarObject.goToView(0, new $t.datetime(2009, 11, 26));
            
            $(calendarObject.element).find('.t-content td.t-other-month .t-link').filter(function() {
                return $(this).text() == '1';
            }).eq(0).trigger('click');

            assertEquals(2010, calendarObject.selectedDate.year());
            assertEquals(0, calendarObject.selectedDate.month());
            assertEquals(1, calendarObject.selectedDate.date());
        }
        
        function test_date_selection_within_previous_month_of_previous_year_sets_selected_date_correctly() {
            calendarObject.goToView(0, new $t.datetime(2009, 0, 1));
            
            $(calendarObject.element).find('.t-content td.t-other-month .t-link').filter(function() {
                return $(this).text() == '29';
            }).eq(0).trigger('click');

            assertEquals(2008, calendarObject.selectedDate.year());
            assertEquals(11, calendarObject.selectedDate.month());
            assertEquals(29, calendarObject.selectedDate.date());
        }

        function test_date_selection_does_not_overflow_when_selecting_previous_month() {
            calendarObject.goToView(0, new $t.datetime(2010, 1, 1));
            
            $(calendarObject.element).find('.t-content td.t-other-month .t-link').filter(function() {
                return $(this).text() == '31';
            }).eq(0).trigger('click');

            assertEquals(2010, calendarObject.selectedDate.year());
            assertEquals(0, calendarObject.selectedDate.month());
            assertEquals(31, calendarObject.selectedDate.date());
        }
        
        function test_date_selection_does_not_change_selected_date_when_cancelled() {
            calendarObject.value(new $t.datetime(1987, 3, 21));

            $(calendarObject.element).bind('change', function(e) {
                e.preventDefault();
            });
            
            $(calendarObject.element).find('.t-content td .t-link').filter(function() {
                return $(this).text() == '22';
            }).eq(0).trigger('click');

            assertEquals(1987, calendarObject.selectedDate.year());
            assertEquals(3, calendarObject.selectedDate.month());
            assertEquals(21, calendarObject.selectedDate.date());
        }
        
        //viewedMonth, selectedDate, minDate, maxDate
        function test_date_less_than_min_date_should_not_be_rendered() {

            var minDate = new $t.datetime(2000, 1, 1);

            var html = $.telerik.calendar.views[0].body(new $t.datetime(1999, 9, 26),
                                                        minDate,
                                                        calendarObject.maxDate, 
                                                        new $t.datetime(2009, 9, 26));

            assertTrue($(html).find('> .t-link').length == 0);
        }

        function test_date_bigger_than_max_date_should_not_be_rendered() {
            
            var maxDate = new Date(2010, 1, 1);

            var html = $.telerik.calendar.views[0].body(new $t.datetime(2019, 9, 26),
                                                        calendarObject.minDate,
                                                        maxDate,
                                                        new $t.datetime(2009, 9, 26));

            assertTrue($(html).find('> .t-link').length == 0);
        }


        function test_goToView_with_date_with_same_month_as_maxdate_should_disable_rightArrow() {

            calendarObject.maxDate = new $t.datetime(2000, 2, 30);
            calendarObject.selectedDate = new $t.datetime(2000, 2, 23);

            calendarObject.goToView(0, $t.datetime.firstDayOfMonth(new $t.datetime(2000, 2, 24)));

            assertTrue($(calendarObject.element).find('.t-nav-next').hasClass('t-state-disabled'));
        }

        function test_buildYearView_should_not_render_months_less_than_min_date() {
            var html = $.telerik.calendar.views[1].body(new $t.datetime(2009, 9, 1), new $t.datetime(2009, 9, 10), calendarObject.maxDate);
            
            assertTrue($('.t-link', html).length == 3);
        }

        function test_buildYearView_should_not_render_months_bigger_than_max_date() {
            var html = $.telerik.calendar.views[1].body(new $t.datetime(2009, 9, 1), calendarObject.minDate, new $t.datetime(2009, 9, 10));

            assertTrue($('.t-link', html).length == 10);
        }

        function test_buildDecadeView_should_not_render_months_less_than_min_date() {

            var html = $.telerik.calendar.views[2].body(new $t.datetime(2005, 9, 1), new $t.datetime(2002, 9, 10), calendarObject.maxDate);

            assertTrue($('.t-link', html).length == 9);
        }

        function test_buildDecadeView_should_not_render_months_bigger_than_max_date() {

            var html = $.telerik.calendar.views[2].body(new $t.datetime(2005, 9, 1), calendarObject.minDate, new $t.datetime(2008, 9, 10));

            assertTrue($('.t-link', html).length == 10);
        }

        function test_buildCenturyView_should_not_render_months_less_than_min_date() {

            var html = $.telerik.calendar.views[3].body(new $t.datetime(2005, 9, 1), new $t.datetime(2002, 9, 10), calendarObject.maxDate);

            assertTrue($('.t-link', html).length == 11);
        }

        function test_buildCenturyView_should_not_render_months_bigger_than_max_date() {

            var html = $.telerik.calendar.views[3].body(new $t.datetime(2005, 9, 1), calendarObject.minDate, new $t.datetime(2008, 9, 10));

            assertTrue($('.t-link', html).length == 2);
        }
        
        function tearDown() {
            $(calendarObject.element).remove();
            calendarObject = null;
        }
    </script>
    
    <% Html.Telerik().ScriptRegistrar()
           .Scripts(scripts => scripts
               .Add("telerik.common.js")
               .Add("telerik.calendar.js")); %>
</asp:Content>
