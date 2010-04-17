<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%

    string culture = Request.QueryString["culture"] ?? "en-US";
    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);

    Response.Write("<script type='text/javascript'>var culture='");
    Response.Write(culture);
    Response.Write("';</script>");
%>
    <h2>
        DateFormatting</h2>

    <script type="text/javascript">
        var $t;

        function setUp() {
            $t = $.telerik;
        }

        function date(year, month, day, hour, minute, second, millisecond) {
            var d = new Date();
            d.setFullYear(year);
            d.setMonth(month - 1);
            d.setDate(day);
            d.setHours(hour ? hour : 0, minute ? minute : 0, second ? second : 0, millisecond ? millisecond : 0);
            return d;
        }

        function test_date_formatting_supports_short_date_format() {
            var d = date(2000, 1, 30);

            assertEquals(culture, '<%= (new DateTime(2000, 1, 30)).ToString("d")  %>', $t.formatString("{0:d}", d));
        }

        function test_date_formatting_supports_long_date_format() {
            var d = date(2000, 1, 30);

            assertEquals(culture,'<%= (new DateTime(2000, 1, 30)).ToString("D")  %>', $t.formatString("{0:D}", d));
        }

        function test_date_formatting_supports_full_date_long_time_format() {
            var d = date(2000, 1, 30, 13, 9, 9);

            assertEquals(culture,'<%= (new DateTime(2000, 1, 30, 13, 9, 9)).ToString("F")  %>', $t.formatString("{0:F}", d));
        }

        function test_date_formatting_supports_zero_padded_days() {
            var d = date(2000, 1, 1);

            assertEquals(culture,'<%= (new DateTime(2000, 1, 1)).ToString("M/dd/yyyy")  %>', $t.formatString("{0:M/dd/yyyy}", d));
        }

        function test_date_formatting_supports_zero_padded_months() {
            var d = date(2000, 1, 1);

            assertEquals(culture,'<%= (new DateTime(2000, 1, 1)).ToString("MM/dd/yyyy")  %>', $t.formatString("{0:MM/dd/yyyy}", d));
        }

        function test_date_formatting_supports_abbr_day_names() {
            var d = date(2000, 1, 1);

            assertEquals(culture,'<%= (new DateTime(2000, 1, 1)).ToString("MM/ddd/yyyy")  %>', $t.formatString("{0:MM/ddd/yyyy}", d));
        }

        function test_date_formatting_supports_day_names() {
            var d = date(2000, 1, 1);

            assertEquals(culture,'<%= (new DateTime(2000, 1, 1)).ToString("MM/dddd/yyyy")  %>', $t.formatString("{0:MM/dddd/yyyy}", d));
        }

        function test_date_formatting_supports_abbr_month_names() {
            var d = date(2000, 1, 1);
            assertEquals(culture,'<%= (new DateTime(2000, 1, 1)).ToString("MMM/dddd/yyyy")  %>', $t.formatString("{0:MMM/dddd/yyyy}", d));
        }

        function test_date_formatting_supports_month_names() {
            var d = date(2000, 1, 1);
            assertEquals(culture,'<%= (new DateTime(2000, 1, 1)).ToString("MMMM/dddd/yyyy")  %>', $t.formatString("{0:MMMM/dddd/yyyy}", d));
        }

        function test_date_formatting_supports_yy() {
            var d = date(2000, 1, 1);
            assertEquals(culture,'<%= (new DateTime(2000, 1, 1)).ToString("MMMM/dddd/yy")  %>', $t.formatString("{0:MMMM/dddd/yy}", d));
        }

        function test_date_formatting_supports_h_before_12() {
            var d = date(2000, 1, 1, 1);
            assertEquals(culture,'<%= (new DateTime(2000, 1, 1, 1, 0, 0)).ToString("(h)")  %>', $t.formatString("({0:h})", d));
        }

        function test_date_formatting_supports_h_after_12() {
            var d = date(2000, 1, 1, 13);
            assertEquals(culture,'<%= (new DateTime(2000, 1, 1, 13, 0, 0)).ToString("(h)")  %>', $t.formatString("({0:h})", d));
        }

        function test_date_formatting_supports_hh_before_12() {
            var d = date(2000, 1, 1, 1);
            assertEquals(culture,'<%= (new DateTime(2000, 1, 1, 1, 0, 0)).ToString("(hh)")  %>', $t.formatString("({0:hh})", d));
        }

        function test_date_formatting_supports_hh_after_12() {
            var d = date(2000, 1, 1, 13);
            assertEquals(culture,'<%= (new DateTime(2000, 1, 1, 13, 0, 0)).ToString("(hh)")  %>', $t.formatString("({0:hh})", d));
        }

        function test_date_formatting_supports_minutes() {
            var d = date(2000, 1, 1, 1);
            assertEquals(culture,'<%= (new DateTime(2000, 1, 1, 1, 0, 0)).ToString("(hh:m)")  %>', $t.formatString("({0:hh:m})", d));
        }

        function test_date_formatting_supports_zero_padded_minutes() {
            var d = date(2000, 1, 1, 1, 1);
            assertEquals(culture,'<%= (new DateTime(2000, 1, 1, 1, 1, 0)).ToString("(hh:mm)")  %>', $t.formatString("({0:hh:mm})", d));
        }

        function test_date_formatting_supports_seconds() {
            var d = date(2000, 1, 1, 1, 1, 1);
            assertEquals(culture,'<%= (new DateTime(2000, 1, 1, 1, 1, 1)).ToString("(hh:mm:s)")  %>', $t.formatString("({0:hh:mm:s})", d));
        }

        function test_date_formatting_supports_zero_padded_seconds() {
            var d = date(2000, 1, 1, 1, 1, 1);
            assertEquals(culture,'<%= (new DateTime(2000, 1, 1, 1, 1, 1)).ToString("(hh:mm:ss)")  %>', $t.formatString("({0:hh:mm:ss})", d));
        }

        function test_date_formatting_supports_tt_before_12() {
            var d = date(2000, 1, 1, 1, 1, 1);
            assertEquals(culture,'<%= (new DateTime(2000, 1, 1, 1, 1, 1)).ToString("(tt)")  %>', $t.formatString("({0:tt})", d));
        }

        function test_date_formatting_supports_tt_after_12() {
            var d = date(2000, 1, 1, 13, 1, 1);
            assertEquals(culture,'<%= (new DateTime(2000, 1, 1, 13, 1, 1)).ToString("(tt)")  %>', $t.formatString("({0:tt})", d));
        }

        function test_date_formatting_supports_f_more_than_99() {
            var d = date(2000, 1, 1, 1, 1, 1, 100);
            assertEquals(culture,'<%= (new DateTime(2000, 1, 1, 1, 1, 1, 100)).ToString("hh:mm:f")  %>', $t.formatString("{0:hh:mm:f}", d));
        }

        function test_date_formatting_supports_f_less_than_100() {
            var d = date(2000, 1, 1, 1, 1, 1, 99);
            assertEquals(culture,'<%= (new DateTime(2000, 1, 1, 1, 1, 1, 99)).ToString("hh:mm:f")  %>', $t.formatString("{0:hh:mm:f}", d));
        }

        function test_date_formatting_supports_ff() {
            var d = date(2000, 1, 1, 1, 1, 1, 129);
            assertEquals(culture,'<%= (new DateTime(2000, 1, 1, 1, 1, 1, 129)).ToString("hh:mm:ff")  %>', $t.formatString("{0:hh:mm:ff}", d));
        }

        function test_date_formatting_supports_fff() {
            var d = date(2000, 1, 1, 1, 1, 1, 129);
            assertEquals(culture,'<%= (new DateTime(2000, 1, 1, 1, 1, 1, 129)).ToString("(fff)")  %>', $t.formatString("({0:fff})", d));
        }

        function test_date_formatting_supports_H() {
            var d = date(2000, 1, 1, 1);
            assertEquals(culture,'<%= (new DateTime(2000, 1, 1, 1, 1, 0)).ToString("(H)")  %>', $t.formatString("({0:H})", d));
        }

        function test_date_formatting_supports_HH_less_than_10() {
            var d = date(2000, 1, 1, 9);
            assertEquals(culture,'<%= (new DateTime(2000, 1, 1, 9, 1, 0)).ToString("(HH)")  %>', $t.formatString("({0:HH})", d));
        }

        function test_date_formatting_supports_single_quote_literals() {
            var d = date(2000, 1, 1, 9);
            assertEquals(culture,'<%= (new DateTime(2000, 1, 1, 9, 1, 0)).ToString("(\'literal\')")  %>', $t.formatString("({0:'literal'})", d));
        }

        function test_date_formatting_supports_quote_literals() {
            var d = date(2000, 1, 1, 9);
            assertEquals(culture,'<%= (new DateTime(2000, 1, 1, 9, 1, 0)).ToString("(\"literal\")")  %>', $t.formatString("({0:\"literal\"})", d));
        }

        function test_date_formatting_supports_g_format() {
            var d = date(2000, 12, 30, 9, 1);
            assertEquals(culture,'<%= (new DateTime(2000, 12, 30, 9, 1, 0)).ToString("g") %>', $t.formatString("{0:g}", d));
        }

        function test_date_formatting_supports_G_format() {
            var d = date(2000, 12, 30, 9, 1);
            assertEquals(culture,'<%= (new DateTime(2000, 12, 30, 9, 1, 0)).ToString("G") %>', $t.formatString("{0:G}", d));
        }

        function test_date_formatting_supports_m_format() {
            var d = date(2000, 12, 30, 9, 1);
            assertEquals(culture,'<%= (new DateTime(2000, 12, 30, 9, 1, 0)).ToString("m") %>', $t.formatString("{0:m}", d));
        }

        function test_date_formatting_supports_M_format() {
            var d = date(2000, 12, 30, 9, 1);
            assertEquals(culture,'<%= (new DateTime(2000, 12, 30, 9, 1, 0)).ToString("M") %>', $t.formatString("{0:M}", d));
        }

        function test_date_formatting_supports_s_format() {
            var d = date(2000, 12, 30, 9, 1);
            assertEquals(culture,'<%= (new DateTime(2000, 12, 30, 9, 1, 0)).ToString("s") %>', $t.formatString("{0:s}", d));
        }
        
        function test_date_formatting_supports_u_format() {
            var d = date(2000, 12, 30, 9, 1);
            assertEquals(culture,'<%= (new DateTime(2000, 12, 30, 9, 1, 0)).ToString("u") %>', $t.formatString("{0:u}", d));
        }
        
        function test_date_formatting_supports_y_format() {
            var d = date(2000, 12, 30, 9, 1);
            assertEquals(culture,'<%= (new DateTime(2000, 12, 30, 9, 1, 0)).ToString("y") %>', $t.formatString("{0:y}", d));
        }
        
        function test_date_formatting_supports_Y_format() {
            var d = date(2000, 12, 30, 9, 1);
            assertEquals(culture,'<%= (new DateTime(2000, 12, 30, 9, 1, 0)).ToString("y") %>', $t.formatString("{0:Y}", d));
        }        
    
    
    </script>

    <ul>
        <li>Current culture:
            <%= System.Threading.Thread.CurrentThread.CurrentCulture.Name %></li>
        <li>Current UI culture:
            <%= System.Threading.Thread.CurrentThread.CurrentUICulture.Name %></li>
        <li>ShortDatePattern:
            <%= System.Globalization.DateTimeFormatInfo.CurrentInfo.ShortDatePattern %></li>
        <li>LongDatePattern:
            <%= System.Globalization.DateTimeFormatInfo.CurrentInfo.LongDatePattern%></li>
        <li>FullDateTimePattern:
            <%= System.Globalization.DateTimeFormatInfo.CurrentInfo.FullDateTimePattern%></li>
        <li>MonthDayPattern:
            <%= System.Globalization.DateTimeFormatInfo.CurrentInfo.MonthDayPattern%></li>
        <li>RFC1123:
            <%= System.Globalization.DateTimeFormatInfo.CurrentInfo.RFC1123Pattern%></li>
        <li>Sortable:
            <%= System.Globalization.DateTimeFormatInfo.CurrentInfo.SortableDateTimePattern%></li>
        <li>
            YearMonth:
            <%= System.Globalization.DateTimeFormatInfo.CurrentInfo.YearMonthPattern%>
        </li>
    </ul>
    <%
        Html.Telerik().ScriptRegistrar().Globalization(true)
                            .DefaultGroup(group => group
                                .Add("telerik.common.js")
                                .Add("telerik.calendar.js")
                                .Add("telerik.datepicker.js")
                                .Add("telerik.grid.js"));

    %>
</asp:Content>
