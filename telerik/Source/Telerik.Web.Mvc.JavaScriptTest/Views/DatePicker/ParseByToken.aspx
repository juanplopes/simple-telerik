<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Date by token</h2>

    <script type="text/javascript">
        
        function getDatePicker() {
            return $('#DatePicker').data('tDatePicker');
        }
            
        function compareDates(expected, returned) {
            var isValid = true;

            if (expected.getFullYear() != returned.year())
                isValid = false;
            else if (expected.getMonth() != returned.month())
                isValid = false;
            else if (expected.getDate() != returned.date())
                isValid = false;

            return isValid;
        }

        function test_parseByToken_should_return_today_date_token_today() {
           
            var expectedDate = new Date();

            var returnedDate = getDatePicker().parse("today");

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_yesterday_date_token_yesterday() {
            var expectedDate = new Date();
            expectedDate.setDate(expectedDate.getDate() -1);

            var returnedDate = getDatePicker().parse("yesterday");

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_tomorrow_date_token_tomorrow() {
            var expectedDate = new Date();
            expectedDate.setDate(expectedDate.getDate() + 1);

            var returnedDate = getDatePicker().parse("tomorrow");

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_monday_of_current_week() {

            var tmpDate = new $.telerik.datetime(2009, 10, 27); //friday
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = getDatePicker().parse("monday", null, tmpDate);

            expectedDate.setDate(expectedDate.getDate() - 4); //set the expected date

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_friday_of_current_week() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //friday
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = getDatePicker().parse("friday", null, tmpDate);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_sunday_of_current_week() {

            var tmpDate = new $.telerik.datetime(2009, 10, 27); //friday
            var expectedDate = new Date(2009, 10, 27); //friday
            
            var returnedDate = getDatePicker().parse("sunday", null, tmpDate);

            expectedDate.setDate(expectedDate.getDate() + 2); //set the expected date

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_march_of_current_year() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = getDatePicker().parse("March", null, tmpDate);

            expectedDate.setMonth(expectedDate.getMonth() - 8);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_December_of_current_year() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday
            
            var returnedDate = getDatePicker().parse("december", null, tmpDate);

            expectedDate.setMonth(expectedDate.getMonth() + 1);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_November_of_current_year() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = getDatePicker().parse("november", null, tmpDate);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_friday() {

            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = getDatePicker().parse("next friday", null, tmpDate);

            expectedDate.setDate(expectedDate.getDate() + 7);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_last_friday() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = getDatePicker().parse("last friday", null, tmpDate);

            expectedDate.setDate(expectedDate.getDate() - 7);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_monday() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = getDatePicker().parse("next monday", null, tmpDate);

            expectedDate.setDate(expectedDate.getDate() + 3);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_last_monday() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = getDatePicker().parse("last monday", null, tmpDate);

            expectedDate.setDate(expectedDate.getDate() - 11);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_sunday() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = getDatePicker().parse("next sunday", null, tmpDate);

            expectedDate.setDate(expectedDate.getDate() + 9);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_November() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //November

            var returnedDate = getDatePicker().parse("next november", null, tmpDate);

            expectedDate.setMonth(expectedDate.getMonth() + 12);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_last_november() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //November

            var returnedDate = getDatePicker().parse("last november", null, tmpDate);

            expectedDate.setMonth(expectedDate.getMonth() - 12);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_february() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //November

            var returnedDate = getDatePicker().parse("next february", null, tmpDate);

            expectedDate.setMonth(expectedDate.getMonth() + 3);
            
            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_last_february() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //November

            var returnedDate = getDatePicker().parse("last february", null, tmpDate);

            expectedDate.setMonth(expectedDate.getMonth() - 21);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_december() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //November

            var returnedDate = getDatePicker().parse("next december", null, tmpDate);

            expectedDate.setMonth(expectedDate.getMonth() + 13);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        //short names
        function test_parseByToken_should_return_next_fri() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = getDatePicker().parse("next fri", null, tmpDate);

            expectedDate.setDate(expectedDate.getDate() + 7);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_last_fri() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = getDatePicker().parse("last fri", null, tmpDate);

            expectedDate.setDate(expectedDate.getDate() - 7);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_mon() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = getDatePicker().parse("next mon", null, tmpDate);

            expectedDate.setDate(expectedDate.getDate() + 3);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_last_mon() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = getDatePicker().parse("last mon", null, tmpDate);

            expectedDate.setDate(expectedDate.getDate() - 11);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_sun() {

            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = getDatePicker().parse("next sun", null, tmpDate);

            expectedDate.setDate(expectedDate.getDate() + 9);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_Nov() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //November

            var returnedDate = getDatePicker().parse("next Nov", null, tmpDate);

            expectedDate.setMonth(expectedDate.getMonth() + 12);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_last_Nov() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //November

            var returnedDate = getDatePicker().parse("last Nov", null, tmpDate);

            expectedDate.setMonth(expectedDate.getMonth() - 12);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_feb() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //November

            var returnedDate = getDatePicker().parse("next feb", null, tmpDate);

            expectedDate.setMonth(expectedDate.getMonth() + 3);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_last_feb() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //November

            var returnedDate = getDatePicker().parse("last feb", null, tmpDate);

            expectedDate.setMonth(expectedDate.getMonth() - 21);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_dec() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //November

            var returnedDate = getDatePicker().parse("next dec", null, tmpDate);

            expectedDate.setMonth(expectedDate.getMonth() + 13);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_year() {

            var tmpDate = new $.telerik.datetime(2009, 10, 27);
            var expectedDate = new Date(2009, 10, 27);

            var returnedDate = getDatePicker().parse("next year", null, tmpDate);

            expectedDate.setFullYear(expectedDate.getFullYear() + 1, expectedDate.getMonth(), expectedDate.getDate());

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_last_year() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27);
            var expectedDate = new Date(2009, 10, 27);

            var returnedDate = getDatePicker().parse("last year", null, tmpDate);

            expectedDate.setFullYear(expectedDate.getFullYear() - 1, expectedDate.getMonth(), expectedDate.getDate());

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_month() {

            var tmpDate = new $.telerik.datetime(2009, 10, 27);
            var expectedDate = new Date(2009, 10, 27);

            var returnedDate = getDatePicker().parse("next month", null, tmpDate);

            expectedDate.setFullYear(expectedDate.getFullYear(), expectedDate.getMonth() + 1, expectedDate.getDate());

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_last_month() {

            var tmpDate = new $.telerik.datetime(2009, 10, 27);
            var expectedDate = new Date(2009, 10, 27);

            var returnedDate = getDatePicker().parse("last month", null, tmpDate);

            expectedDate.setFullYear(expectedDate.getFullYear(), expectedDate.getMonth() - 1, expectedDate.getDate());

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_week() {

            var tmpDate = new $.telerik.datetime(2009, 10, 27);
            var expectedDate = new Date(2009, 10, 27);

            var returnedDate = getDatePicker().parse("next week", null, tmpDate);

            expectedDate.setFullYear(expectedDate.getFullYear(), expectedDate.getMonth(), expectedDate.getDate() + 7);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_last_week() {

            var tmpDate = new $.telerik.datetime(2009, 10, 27);
            var expectedDate = new Date(2009, 10, 27);

            var returnedDate = getDatePicker().parse("last week", null, tmpDate);

            expectedDate.setFullYear(expectedDate.getFullYear(), expectedDate.getMonth(), expectedDate.getDate() - 7);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_day() { //like tomorrow

            var tmpDate = new $.telerik.datetime(2009, 10, 27);
            var expectedDate = new Date(2009, 10, 27);

            var returnedDate = getDatePicker().parse("next day", null, tmpDate);

            expectedDate.setFullYear(expectedDate.getFullYear(), expectedDate.getMonth(), expectedDate.getDate() + 1);

            assertTrue(compareDates(expectedDate, returnedDate));
        }


        function test_parseByToken_should_return_last_day() { //like tomorrow

            var tmpDate = new $.telerik.datetime(2009, 10, 27);
            var expectedDate = new Date(2009, 10, 27);

            var returnedDate = getDatePicker().parse("last day", null, tmpDate);

            expectedDate.setFullYear(expectedDate.getFullYear(), expectedDate.getMonth(), expectedDate.getDate() - 1);

            assertTrue(compareDates(expectedDate, returnedDate));
        }
        
        function test_parseByToken_should_return_null_if_cannot_parse_first_token()
        {
            var returnedDate = getDatePicker().parse("undefined");
            assertNull(returnedDate);
        }

        function test_parseByToken_should_return_null_if_second_token_is_not_provided() {
            var returnedDate = getDatePicker().parse("next ");
            assertNull(returnedDate);
        }

        function test_parseByToken_should_return_null_if_second_token_is_not_correct() {
            var returnedDate = getDatePicker().parse("next undeffined");
            assertNull(returnedDate);
        }

        /////min and max date testing
        function test_parse_date_less_than_min_date_should_return_null() {

            var tmpDate = new $.telerik.datetime(1900, 9, 01);
            var result = getDatePicker().parse("last year", null, tmpDate);
            assertNull(result);
        }

        function test_parse_date_bigger_than_min_date_should_return_null() {

            var tmpDate = new $.telerik.datetime(2099, 9, 01);
            var result = getDatePicker().parse("next year", null, tmpDate);
            assertNull(result);
        }
       
    </script>
    
    
    
    <%= Html.Telerik().DatePicker()
            .Name("DatePicker")
            .MinDate(new DateTime(1900, 01, 01))
            .MaxDate(new DateTime(2100, 01, 01))
    %>
    
    <% Html.Telerik().ScriptRegistrar()
           .Scripts(scripts => scripts
               .Add("telerik.common.js")
               .Add("telerik.calendar.js")
               .Add("telerik.datepicker.js")); %>

</asp:Content>
