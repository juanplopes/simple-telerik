<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<h2>Date parsing</h2>

<script type="text/javascript">
    var views = {
        Month: 0,
        Year: 1,
        Decade: 2,
        Century: 3
    }

    function getDatePicker() {
        return $('#DatePicker').data('tDatePicker');
    }

    function test_if_datepicker_focused_ctrl_and_down_arrow_change_centuryView_to_decadeView() {

        var date = new Date(2000, 6, 1);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');

        getDatePicker().showPopup();
        var calendar = configureCalendar(date, views.Century);
        
        input.trigger({ type: "keydown", keyCode: 40, ctrlKey: true });
        
        assertTrue(calendar.currentView.index == views.Decade);
    }

    function test_if_datepicker_focused_ctrl_and_down_arrow_change_decadeView_to_yearView() {

        var date = new Date(2000, 6, 1);
        var calendar = configureCalendar(date, views.Decade);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');
        
        getDatePicker().showPopup();
                
        input.trigger({ type: "keydown", keyCode: 40, ctrlKey: true });

        assertTrue(calendar.currentView.index == views.Year);
    }

    function test_if_datepicker_focused_ctrl_and_up_arrow_change_view_to_wider_range() {

        var date = new Date(2000, 6, 1);
        var calendar = configureCalendar(date, views.Month);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');
        
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 38, ctrlKey: true });

        assertTrue(calendar.currentView.index == views.Year);
    }

    function test_left_key_should_focus_previous_date() {
        var date = new Date(2000, 10, 10);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');
        
        configureCalendar(date, views.Month);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();
        
        input.trigger({ type: "keydown", keyCode: 37 });
        
        var element = $('.t-state-focus', getDatePicker().$calendar());

        assertTrue(getDatePicker().focusedDate.date() == 9);
    }

    function test_right_key_should_focus_next_date() {

        var date = new Date(2000, 10, 10);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');

        configureCalendar(date, views.Month);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();
               
        input.trigger({ type: "keydown", keyCode: 39 });

        var element = $('.t-state-focus', getDatePicker().$calendar());

        assertTrue(element.find('.t-link').html() == "11");
    }

    function test_down_key_should_focus_next_week_day() {

        var date = new Date(2000, 10, 10);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');
        
        configureCalendar(date, views.Month);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 40 });

        var element = $('.t-state-focus', getDatePicker().$calendar());

        assertTrue(element.find('.t-link').html() == "17");
    }

    function test_up_key_should_focus_previous_week_day() {

        var date = new Date(2000, 10, 10);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');
        
        configureCalendar(date, views.Month);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();
        
        input.trigger({ type: "keydown", keyCode: 38 });

        var element = $('.t-state-focus', getDatePicker().$calendar());

        assertTrue(element.find('.t-link').html() == "3");
    }

    function test_left_key_should_navigate_to_prev_month_day() {
        var date = new Date(2000, 10, 1);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');
        
        configureCalendar(date, views.Month);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 37 });

        assertTrue(getDatePicker().focusedDate.date() == 31 && getDatePicker().focusedDate.month() == 9);
    }

    function test_right_key_should_navigate_to_next_month_day() {
        var date = new Date(2000, 10, 30);

        var input = $('#DatePicker').find('> .t-input');
        input.val('');

        configureCalendar(date, views.Month);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();
        
        input.trigger({ type: "keydown", keyCode: 39 });

        assertTrue(getDatePicker().focusedDate.date() == 1 && getDatePicker().focusedDate.month() == 11);
    }

    function test_up_key_should_navigate_to_prev_week_and_navigate() {
        var date = new Date(2000, 10, 4);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');
        
        configureCalendar(date, views.Month);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 38 });

        assertTrue(getDatePicker().focusedDate.date() == 28 && getDatePicker().focusedDate.month() == 9);
    }

    function test_down_key_should_navigate_to_next_week_and_navigate() {
        
        var date = new Date(2000, 10, 28);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');
        
        configureCalendar(date, views.Month);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 40 });

        assertTrue(getDatePicker().focusedDate.date() == 5 && getDatePicker().focusedDate.month() == 11);
    }

    function test_left_key_should_navigate_to_prev_month() {
        var date = new Date(2000, 10, 1);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');
        
        configureCalendar(date, views.Year);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 37 });

        assertTrue(getDatePicker().focusedDate.month() == 9);
    }

    function test_right_key_should_navigate_to_next_month() {
        var date = new Date(2000, 10, 1);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');
        
        configureCalendar(date, views.Year);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 39 });

        assertTrue(getDatePicker().focusedDate.month() == 11);
    }

    function test_up_key_should_focus_july() {
        var date = new Date(2000, 10, 1);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');
        
        configureCalendar(date, views.Year);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 38 });

        assertTrue(getDatePicker().focusedDate.month() == 6);
    }

    function test_down_key_should_focus_november() {

        var date = new Date(2000, 6, 28);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');
        
        configureCalendar(date, views.Year);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 40 });

        assertTrue(getDatePicker().focusedDate.month() == 10);
    }

    function test_up_key_should_focus_prev_november() {
        var date = new Date(2000, 2, 1);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');
        
        configureCalendar(date, views.Year);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 38 });

        assertTrue(getDatePicker().focusedDate.month() == 10 && getDatePicker().focusedDate.year() == 1999);
    }

    function test_down_key_should_focus_next_march() {

        var date = new Date(2000, 10, 28);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');
        
        configureCalendar(date, views.Year);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 40 });

        assertTrue(getDatePicker().focusedDate.month() == 2 && getDatePicker().focusedDate.year() == 2001);
    }
    
    function test_left_key_should_navigate_to_prev_year() {
        var date = new Date(2000, 10, 1);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');        
        
        configureCalendar(date, views.Decade);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 37 });

        assertTrue(getDatePicker().focusedDate.year() == 1999);
    }

    function test_right_key_should_navigate_to_next_year() {
        var date = new Date(2000, 10, 1);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');
        
        configureCalendar(date, views.Decade);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 39 });

        assertTrue(getDatePicker().focusedDate.year() == 2001);
    }

    function test_up_key_should_focus_2000() {
        var date = new Date(2004, 10, 1);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');
        
        configureCalendar(date, views.Decade);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 38 });

        assertTrue(getDatePicker().focusedDate.year() == 2000);
    }

    function test_down_key_should_focus_2004() {

        var date = new Date(2000, 6, 28);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');
        
        configureCalendar(date, views.Decade);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 40 });

        assertTrue(getDatePicker().focusedDate.year() == 2004);
    }

    function test_up_key_should_focus_prev_1996() {
        var date = new Date(2000, 2, 1);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');
        
        configureCalendar(date, views.Decade);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 38 });

        assertTrue(getDatePicker().focusedDate.year() == 1996);
    }

    function test_down_key_should_focus_next_2014() {

        var date = new Date(2010, 10, 28);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');        
        
        configureCalendar(date, views.Decade);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 40 });

        assertTrue(getDatePicker().focusedDate.year() == 2014);
    }

    ///
    function test_left_key_should_navigate_to_prev_decade() {
        var date = new Date(2000, 10, 1);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');
        
        configureCalendar(date, views.Century);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 37 });

        assertTrue(getDatePicker().focusedDate.year() == 1990);
    }

    function test_right_key_should_navigate_to_next_decade() {
        var date = new Date(2000, 10, 1);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');

        configureCalendar(date, views.Century);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 39 });

        assertTrue(getDatePicker().focusedDate.year() == 2010);
    }

    function test_up_key_should_focus_2004_in_century_view() {
        var date = new Date(2044, 10, 1);

        var input = $('#DatePicker').find('> .t-input');
        input.val('');
        
        configureCalendar(date, views.Century);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 38 });

        assertTrue(getDatePicker().focusedDate.year() == 2004);
    }

    function test_down_key_should_focus_2044() {

        var date = new Date(2004, 6, 28);

        var input = $('#DatePicker').find('> .t-input');
        input.val('');

        configureCalendar(date, views.Century);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 40 });

        assertTrue(getDatePicker().focusedDate.year() == 2044);
    }

    function test_if_datepicker_focused_ctrl_and_left_arrow_should_navigate_to_passed_month() {

        var date = new Date(2000, 6, 1);
        var resultDate = new Date(2000, 5, 1);
        var calendar = configureCalendar(date, views.Month);
        var input = $('#DatePicker').find('> .t-input');

        input.val('');

        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 37, ctrlKey: true });

        assertTrue((calendar.viewedMonth.toDate() - resultDate) == 0);
    }

    function test_if_datepicker_focused_ctrl_and_left_arrow_should_navigate_to_passed_year() {

        var date = new Date(2000, 6, 1);
        var resultDate = new Date(1999, 6, 1);
        var calendar = configureCalendar(date, views.Year);
        var input = $('#DatePicker').find('> .t-input');

        input.val('');
        
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 37, ctrlKey: true });

        assertTrue((calendar.viewedMonth.toDate() - resultDate) == 0);
    }

    function test_if_datepicker_focused_ctrl_and_left_arrow_should_navigate_to_passed_decade() {

        var date = new Date(2000, 6, 1);
        var resultDate = new Date(1990, 6, 1);
        var calendar = configureCalendar(date, views.Decade);
        var input = $('#DatePicker').find('> .t-input');

        input.val('');

        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 37, ctrlKey: true });

        assertTrue((calendar.viewedMonth.toDate() - resultDate) == 0);
    }

    function test_if_datepicker_focused_ctrl_and_left_arrow_should_navigate_to_passed_century() {

        var date = new Date(2000, 6, 1);
        var resultDate = new Date(1900, 6, 1);
        var calendar = configureCalendar(date, views.Century);

        getDatePicker().showPopup();

        var input = $('#DatePicker').find('> .t-input');
        input.trigger({ type: "keydown", keyCode: 37, ctrlKey: true });

        assertTrue((calendar.viewedMonth.toDate() - resultDate) == 0);
    }

    function test_if_datepicker_focused_ctrl_and_right_arrow_should_navigate_to_future_month() {

        var date = new Date(2000, 6, 1);
        var resultDate = new Date(2000, 7, 1);
        var calendar = configureCalendar(date, views.Month);

        getDatePicker().showPopup();

        var input = $('#DatePicker').find('> .t-input');
        input.trigger({ type: "keydown", keyCode: 39, ctrlKey: true });

        assertTrue((calendar.viewedMonth.toDate() - resultDate) == 0);
    }

    function test_if_datepicker_focused_ctrl_and_right_arrow_should_navigate_to_passed_year() {

        var date = new Date(2000, 6, 1);
        var resultDate = new Date(2001, 6, 1);
        var calendar = configureCalendar(date, views.Year);

        getDatePicker().showPopup();

        var input = $('#DatePicker').find('> .t-input');
        input.trigger({ type: "keydown", keyCode: 39, ctrlKey: true });

        assertTrue((calendar.viewedMonth.toDate() - resultDate) == 0);
    }

    function test_if_datepicker_focused_ctrl_and_right_arrow_should_navigate_to_passed_decade() {

        var date = new Date(2000, 6, 1);
        var resultDate = new Date(2010, 6, 1);
        var calendar = configureCalendar(date, views.Decade);
        var input = $('#DatePicker').find('> .t-input');

        input.val('');
        
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 39, ctrlKey: true });

        assertTrue((calendar.viewedMonth.toDate() - resultDate) == 0);
    }

    function test_if_datepicker_focused_ctrl_and_right_arrow_should_navigate_to_passed_century() {

        var date = new Date(1999, 6, 1);
        var resultDate = new Date(2099, 6, 1);
        var calendar = configureCalendar(date, views.Century);
        var input = $('#DatePicker').find('> .t-input');

        input.val('');

        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 39, ctrlKey: true });

        assertTrue((calendar.viewedMonth.toDate() - resultDate) == 0);
    }

    function test_page_up_should_navigate_to_past() {

        var date = new Date(2004, 6, 28);

        var input = $('#DatePicker').find('> .t-input');

        input.val('');

        configureCalendar(date, views.Month);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 33 });

        assertTrue((getDatePicker().focusedDate.toDate() - new Date(2004, 5, 28)) == 0);
    }

    function test_page_down_should_navigate_to_future() {

        var date = new Date(2004, 6, 28);

        var input = $('#DatePicker').find('> .t-input');
        input.val('');

        configureCalendar(date, views.Month);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 34 });

        assertTrue((getDatePicker().focusedDate.toDate() - new Date(2004, 7, 28)) == 0);
    }

    function test_home_button_should_focus_first_day_of_month() {

        var date = new Date(2004, 6, 28);
        var input = $('#DatePicker').find('> .t-input');
        input.val('');
        
        configureCalendar(date, views.Month);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        
        input.trigger({ type: "keydown", keyCode: 36 });

        assertTrue((getDatePicker().focusedDate.toDate() - new Date(2004, 6, 1)) == 0);
    }

    function test_end_button_should_focus_last_day_of_month() {

        var date = new Date(2004, 6, 28);

        var input = $('#DatePicker').find('> .t-input');
        input.val('');
                
        configureCalendar(date, views.Month);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        input.trigger({ type: "keydown", keyCode: 35 });
        
        assertTrue((getDatePicker().focusedDate.toDate() - new Date(2004, 6, 31)) == 0);
    }

    function test_alt_and_down_arrow_should_open_calendar() {

        var date = new Date(2004, 6, 28);

        configureCalendar(date, views.Month);
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        
        var input = $('#DatePicker').find('> .t-input');
        input.trigger({ type: "keydown", keyCode: 40, altKey: true });

        assertTrue(getDatePicker().$calendar().is(':visible'));
    }

    function test_alt_and_down_arrow_should_parse_input_value() {
        var date = new Date(2004, 6, 28);

        configureCalendar(date, views.Month);
        
        getDatePicker().format = 'M/d/yyyy';
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().hidePopup();
        
        var input = $('#DatePicker').find('> .t-input');
        input.val('10/10/2000')
        input.trigger({ type: "keydown", keyCode: 40, altKey: true, shiftKey: true });

        assertTrue(isValidDate(2000, 10, 10, getDatePicker().focusedDate.toDate()));
    }

    function test_input_value_should_be_parsed_on_calendar_opening() {
        
        var date = new Date(2004, 6, 28);

        configureCalendar(date, views.Month);
        getDatePicker().format = 'M/d/yyyy';
        getDatePicker().focusedDate = new $.telerik.datetime(date);  

        var input = $('#DatePicker').find('> .t-input');
        input.val('10/10/2000')

        getDatePicker().showPopup();
        
        assertTrue(isValidDate(2000, 10, 10, getDatePicker().focusedDate.toDate()));
    }


    function test_should_parse_input_value_even_calendar_is_opened() {
        var date = new Date(2004, 6, 28);

        configureCalendar(date, views.Month);
        getDatePicker().format = 'M/d/yyyy';
        getDatePicker().focusedDate = new $.telerik.datetime(date);
        getDatePicker().showPopup();

        var input = $('#DatePicker').find('> .t-input');

        input.val("10/10/2000");
        
        input.trigger({ type: "keydown", keyCode: 48 }); //ASCII - 0
        input.trigger({ type: "keydown", keyCode: 13 }); //trigger enter to parse value.

        assertTrue(getDatePicker().focusedDate.year() == 2000);
    }

    function test_should_close_calendar_if_document_is_clicked() {
        getDatePicker().show();

        $(document.body).click();
        
        assertTrue(getDatePicker().$calendar().is(':visible'));
    }

    function configureCalendar(viewedMonth, currentView) {
        var calendar = getDatePicker().$calendar().data('tCalendar');
        
        if (viewedMonth) calendar.viewedMonth = new $.telerik.datetime(viewedMonth);
        calendar.currentView = $.telerik.calendar.views[currentView];
        calendar.stopAnimation = true;

        return calendar;
    }

    function isValidDate(year, month, day, date) {
        var isValid = true;

        if (year != date.getFullYear())
            isValid = false;
        else if (month != date.getMonth() + 1)
            isValid = false;
        else if (day != date.getDate())
            isValid = false;

        return isValid;
    }  
        
</script>

 <%= Html.Telerik().DatePicker().Name("DatePicker")
                   .MinDate(new DateTime(1600, 1,1))
                   .MaxDate(new DateTime(2400, 1, 1))
 %>
    
    <% Html.Telerik().ScriptRegistrar()
           .Scripts(scripts => scripts
               .Add("telerik.common.js")
               .Add("telerik.datepicker.js")
               .Add("telerik.calendar.js")); %>
</asp:Content>