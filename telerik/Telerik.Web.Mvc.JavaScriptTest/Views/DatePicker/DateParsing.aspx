<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTest.Customer>>" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTest" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<%
    string culture = Request.QueryString["culture"] ?? "en-US";
    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);

    Response.Write("<script type='text/javascript'>var culture='");
    Response.Write(culture);
    Response.Write("';</script>");
%>
    <h2>Date parsing</h2>
    
    <script type="text/javascript">
        function getDatePicker() {
            return $('#DatePicker').data('tDatePicker');
        }

        function isValidDate(year, month, day, date) {
            var isValid = true;
            
            if (year != date.year())
                isValid = false;
            else if (month != date.month() + 1)
                isValid = false;
            else if (day != date.date())
                isValid = false;
            
            return isValid;
        }
        
        //short date format
        function test_parse_Invariant_Language_date_format() { // Also: Persian

            var dateFormat = "MM/dd/yyyy";
            var result = getDatePicker().parse("12/23/2000", dateFormat);
            assertTrue(isValidDate(2000,12,23,result));
        }

        function test_parse_Arabic_date_format() { // Also : Galician, Divehi 

            var dateFormat = "dd/MM/yy";
            var result = getDatePicker().parse("10/10/99", dateFormat);
            assertTrue(isValidDate(1999,10,10,result));
        }

        function test_parse_Bulgarian_Language_date_format() { //

            var dateFormat = "d.M.yyyy 'г.'";
            var result = getDatePicker().parse("23.12.2000", dateFormat);
            assertTrue(isValidDate(2000,12,23,result));
        }

        function test_parse_Bulgarian_Language_with_literal_date_format() {

            var dateFormat = "d.M.yyyy 'г.'";
            var result = getDatePicker().parse("23.12.2000 г.", dateFormat);
            assertTrue(isValidDate(2000,12,23,result));
        }

        function test_parse_Catalan_Language_date_format() { // Also : Vietnamese, Arabic 

            var dateFormat = "dd/MM/yyyy";
            var result = getDatePicker().parse("23/12/2000", dateFormat);
            assertTrue(isValidDate(2000,12,23,result));
        }

        function test_parse_Chinese_Language_date_format() {

            var dateFormat = "yyyy/M/d";
            var result = getDatePicker().parse("2000/12/23", dateFormat);
            assertTrue(isValidDate(2000,12,23,result));
        }

        function test_parse_Czech_Language_date_format() {

            var dateFormat = "d.M.yyyy";
            var result = getDatePicker().parse("23.12.2000", dateFormat);
            assertTrue(isValidDate(2000, 12, 23, result));
        }

        function test_parse_Danish_Language_date_format() { // Also : Faroese, Hindi, Tamil, Marathi
            //Sanskrit, Konkani, Portuguese 

            var dateFormat = "dd-MM-yyyy";
            var result = getDatePicker().parse("23-12-2000", dateFormat);
            assertTrue(isValidDate(2000, 12, 23, result));
        }

        function test_parse_German_Language_date_format() { //also : Norwegian, Romanian, Russian, Turkish
            // Ukrainian, Belarusian, Armenian, Azeri, Macedonian, Georgian, Kazakh, Tatar, Italian,Norwegian
            //Azeri, Uzbek, Austria

            var dateFormat = "dd.MM.yyyy";
            var result = getDatePicker().parse("23.12.2000", dateFormat);
            assertTrue(isValidDate(2000, 12, 23, result));
        }

        function test_parse_Greek_Language_date_format() { // also : Portuguese, Thai, Hong Kong

            var dateFormat = "d/M/yyyy";
            var result = getDatePicker().parse("23/12/2000", dateFormat);
            assertTrue(isValidDate(2000, 12, 23, result));
        }

        function test_parse_English_Language_date_format() { // Also : Kiswahili 

            var dateFormat = "M/d/yyyy";
            var result = getDatePicker().parse("12/23/2000", dateFormat);
            assertTrue(isValidDate(2000, 12, 23, result));
        }

        function test_parse_Finnish_Language_date_format() { // also : Icelandic, Croatian, Slovenian
            // Serbian, Swedish, 

            var dateFormat = "d.M.yyyy";
            var result = getDatePicker().parse("23.12.2000", dateFormat);
            assertTrue(isValidDate(2000, 12, 23, result));
        }

        function test_parse_French_Language_date_format() { // also : Hebrew, Italian, Urdu, Indonesian
            //Malay, Syriac, (United Kingdom), Spanish (Mexico), ms-BN, Arabic 

            var dateFormat = "dd/MM/yyyy";
            var result = getDatePicker().parse("23/12/2000", dateFormat);
            assertTrue(isValidDate(2000, 12, 23, result));
        }

        function test_parse_Hungarian_Language_date_format() {

            var dateFormat = "yyyy. MM. dd.";
            var result = getDatePicker().parse("2000. 12. 23.", dateFormat);
            assertTrue(isValidDate(2000, 12, 23, result));
        }

        function test_parse_Japanese_Language_date_format() { // Also : Basque, Afrikaans 

            var dateFormat = "yyyy/MM/dd";
            var result = getDatePicker().parse("2000/12/23", dateFormat);
            assertTrue(isValidDate(2000, 12, 23, result));
        }

        function test_parse_Korean_Language_date_format() { // also : Polish, Albanian, Swedish

            var dateFormat = "yyyy-MM-dd";
            var result = getDatePicker().parse("2000-12-23", dateFormat);
            assertTrue(isValidDate(2000, 12, 23, result));
        }

        function test_parse_Dutch_Language_date_format() {

            var dateFormat = "d-M-yyyy";
            var result = getDatePicker().parse("23-2-2008", dateFormat);
            assertTrue(isValidDate(2008, 2, 23, result));
        }

        function test_parse_Norwegian_Language_date_format() {

            var dateFormat = "d-M-yyyy";
            var result = getDatePicker().parse("23-2-2008", dateFormat);
            assertTrue(isValidDate(2008, 2, 23, result));
        }

        function test_parse_Slovak_Language_date_format() {

            var dateFormat = "d. M. yyyy";
            var result = getDatePicker().parse("23. 2. 2008", dateFormat);
            assertTrue(isValidDate(2008, 2, 23, result));
        }

        function test_parse_Estonian_Language_date_format() {

            var dateFormat = "d.MM.yyyy";
            var result = getDatePicker().parse("23.12.2008", dateFormat);
            assertTrue(isValidDate(2008, 12, 23, result));
        }

        function test_parse_Latvian_Language_date_format() {

            var dateFormat = "yyyy.MM.dd.";
            var result = getDatePicker().parse("2008.12.23.", dateFormat);
            assertTrue(isValidDate(2008, 12, 23, result));
        }

        function test_parse_Lithuanian_Language_date_format() {

            var dateFormat = "yyyy.MM.dd";
            var result = getDatePicker().parse("2008.12.23", dateFormat);
            assertTrue(isValidDate(2008, 12, 23, result));
        }

        function test_parse_Kyrgyz_Language_date_format() {
            var dateFormat = "dd.MM.yy";
            var result = getDatePicker().parse("5.3.08", dateFormat);
            assertTrue(isValidDate(2008, 3, 5, result));
        }

        function test_parse_Uzbek_Language_date_format() {
            var dateFormat = "dd/MM yyyy";
            var result = getDatePicker().parse("5/3 2008", dateFormat);
            assertTrue(isValidDate(2008, 3, 5, result));
        }

        function test_parse_Punjabi_Language_date_format() { // Also : Gujarati, Telugu, Kannada  
            var dateFormat = "dd-MM-yy";
            var result = getDatePicker().parse("5-3-2008", dateFormat);
            assertTrue(isValidDate(2008, 3, 5, result));
        }

        function test_parse_Mongolian_Language_date_format() {
            var dateFormat = "yy.MM.dd";
            var result = getDatePicker().parse("99.12.25", dateFormat);
            assertTrue(isValidDate(1999, 12, 25, result));
        }

        function test_parse_Belgium_Language_date_format() { // Also Dutch(Belgium ), English (Australia)
            var dateFormat = "d/MM/yyyy";
            var result = getDatePicker().parse("05/10/2009", dateFormat);
            assertTrue(isValidDate(2009, 10, 5, result));
        }

        function test_parse_Yakut_Language_date_format() {
            var dateFormat = "MM.dd.yyyy";
            var result = getDatePicker().parse("3.5.2008", dateFormat);
            assertTrue(isValidDate(2008, 3, 5, result));
        }

        function test_parse_Croatian_Language_date_format() {
            var dateFormat = "d.M.yyyy.";
            var result = getDatePicker().parse("5.3.2008", dateFormat);
            assertTrue(isValidDate(2008, 3, 5, result));
        }

        //long date format.
        function test_parse_Invariant_Language_long_date_format() { // Also: Persian
            
            var dateFormat = "dddd, dd MMMM yyyy";
            var result = getDatePicker().parse("Saturday, 23 December 2000", dateFormat);
            assertTrue(isValidDate(2000, 12, 23, result));
        }

        function test_parse_Arabic_long_date_format() {

            var dateFormat = "dd/MMMM/yyyy";
            var result = getDatePicker().parse("10/September/2009", dateFormat);
            assertTrue(isValidDate(2009, 9, 10, result));
        }

        function test_parse_Bulgarian_long_date_format() {

            var dateFormat = "dd MMMM yyyy 'г.'";
            var result = getDatePicker().parse("10 September 2009 'г.'", dateFormat);
            assertTrue(isValidDate(2009, 9, 10, result));
        }

        function test_parse_Catalan_long_date_format() {
   
            var dateFormat = "dddd, d' / 'MMMM' / 'yyyy";
            var result = getDatePicker().parse("Saturday, 23 / December / 2000", dateFormat);
            assertTrue(isValidDate(2000, 12, 23, result));
        }

        function test_parse_Chinese_long_date_format() {

            var dateFormat = "yyyy'年'M'月'd'日'";
            var result = getDatePicker().parse("2009年9月10日", dateFormat);
            assertTrue(isValidDate(2009, 9, 10, result));
        }

        function test_parse_Czech_long_date_format() { // also: Danish, Norwegian

            var dateFormat = "d. MMMM yyyy";
            var result = getDatePicker().parse("10. September 2009", dateFormat);
            assertTrue(isValidDate(2009, 9, 10, result));
        }

        function test_parse_German_long_date_format() {

            var dateFormat = "dddd, d. MMMM yyyy";
            var result = getDatePicker().parse("Saturday, 23. December 2000", dateFormat);
            assertTrue(isValidDate(2000, 12, 23, result));
        }

        function test_parse_Greek_long_date_format() {

            var dateFormat = "dddd, d MMMM yyyy";
            var result = getDatePicker().parse("Saturday, 3 December 2000", dateFormat);
            assertTrue(isValidDate(2000, 12, 3, result));
        }

        function test_parse_English_long_date_format() {

            var dateFormat = "dddd, MMMM dd, yyyy";
            var result = getDatePicker().parse("Saturday, December 3, 2000", dateFormat);
            assertTrue(isValidDate(2000, 12, 3, result));
        }

        function test_parse_Finnish_long_date_format() { 

            var dateFormat = "d. MMMM'ta 'yyyy";
            var result = getDatePicker().parse("23. Decemberta 2000", dateFormat);
            assertTrue(isValidDate(2000, 12, 23, result));
        }

        function test_parse_France_long_date_format() { 

            var dateFormat = "dddd d MMMM yyyy";
            var result = getDatePicker().parse("Saturday 3 December 2000", dateFormat);
            assertTrue(isValidDate(2000, 12, 3, result));
        }

        function test_parse_Hebrew_long_date_format() { 

            var dateFormat = "dddd dd MMMM yyyy";
            var result = getDatePicker().parse("Saturday 3 December 2000", dateFormat);
            assertTrue(isValidDate(2000, 12, 3, result));
        }

        function test_parse_Hungarian_long_date_format() { 

            var dateFormat = "yyyy. MMMM d.";
            var result = getDatePicker().parse("2000. December 23.", dateFormat);
            assertTrue(isValidDate(2000, 12, 23, result));
        }

        function test_parse_Icelandic_long_date_format() {

            var dateFormat = "d. MMMM yyyy";
            var result = getDatePicker().parse("23. December 2000", dateFormat);
            assertTrue(isValidDate(2000, 12, 23, result));
        }

        function test_parse_Italian_long_date_format() { // Also Dutch

            var dateFormat = "dddd d MMMM yyyy";
            var result = getDatePicker().parse("Saturday 3 December 2000", dateFormat);
            assertTrue(isValidDate(2000, 12, 3, result));
        }

        function test_parse_Japanese_long_date_format() {

            var dateFormat = "yyyy'年'M'月'd'日'";
            var result = getDatePicker().parse("2000年12月3日", dateFormat);
            assertTrue(isValidDate(2000, 12, 3, result));
        }

        // Korean_long_date_format() { //Not supported!!!

        function test_parse_Norwegian_long_date_format() {

            var dateFormat = "dddd d MMMM yyyy";
            var result = getDatePicker().parse("Saturday 3 December 2000", dateFormat);
            assertTrue(isValidDate(2000, 12, 3, result));
        }

        function test_parse_Brazil_long_date_format() {
            
            var dateFormat = "dddd, d' de 'MMMM' de 'yyyy";
            var result = getDatePicker().parse("Saturday, 3 de December de 2000", dateFormat);
            assertTrue(isValidDate(2000, 12, 3, result));
        }

        function test_parse_Polish_long_date_format() { //Also Romanian, Thailand, Belarusian

            var dateFormat = "d MMMM yyyy";
            var result = getDatePicker().parse("3 December 2000", dateFormat);
            assertTrue(isValidDate(2000, 12, 3, result));
        }

        function test_parse_Russian_long_date_format() {

            var dateFormat = "d MMMM yyyy 'г.'";
            var result = getDatePicker().parse("3 December 2000 'г.'", dateFormat);
            assertTrue(isValidDate(2000, 12, 3, result));
        }

        function test_parse_Croatian_long_date_format() { //Also Slovak

            var dateFormat = "d. MMMM yyyy";
            var result = getDatePicker().parse("3. December 2000", dateFormat);
            assertTrue(isValidDate(2000, 12, 3, result));
        }

        function test_parse_Albanian_long_date_format() {

            var dateFormat = "yyyy-MM-dd";
            var result = getDatePicker().parse("2000-12-23", dateFormat);
            assertTrue(isValidDate(2000, 12, 23, result));
        }

        function test_parse_Swedish_long_date_format() {

            var dateFormat = "'den 'd MMMM yyyy";
            var result = getDatePicker().parse("den 23 December 2000", dateFormat);
            assertTrue(isValidDate(2000, 12, 23, result));
        }

        function test_parse_Ukrainian_long_date_format() {

            var dateFormat = "'den 'd MMMM yyyy' р.'";
            var result = getDatePicker().parse("den 23 December 2000", dateFormat);
            assertTrue(isValidDate(2000, 12, 23, result));
        }

//              Turkish_long_date_format() { // not supported because of date name(dddd) after number day (dd)
//            var dateFormat = "dd MMMM yyyy dddd";
        //            var result = getDatePicker().parse("3 December 2000 Saturday", dateFormat);
//            assertTrue(isValidDate(2000, 12, 3, result));
//        }

        function test_parse_Estonian_long_date_format() {

            var dateFormat = "d. MMMM yyyy'. a.'";
            var result = getDatePicker().parse("3. December 2000. a.", dateFormat);
            assertTrue(isValidDate(2000, 12, 3, result));
        }

        function test_parse_Latvian_long_date_format() {

            var dateFormat = "dddd, yyyy'. gada 'd. MMMM";
            var result = getDatePicker().parse("Saturday, 2000. gada 3. December", dateFormat);
            assertTrue(isValidDate(2000, 12, 3, result));
        }

        function test_parse_Lithuanian_long_date_format() {

            var dateFormat = "yyyy 'm.' MMMM d 'd.'";
            var result = getDatePicker().parse("2000 m. December 23 d.", dateFormat);
            assertTrue(isValidDate(2000, 12, 23, result));
        }

        function test_parse_Basque_long_date_format() {

            var dateFormat = "dddd, yyyy.'eko' MMMM'k 'd";
            var result = getDatePicker().parse("Saturday, 2000.eko Decemberk 3", dateFormat);
            assertTrue(isValidDate(2000, 12, 3, result));
        }

//        Georgian_long_date_format() { // not supported because of date name(dddd) after number day (dd)
//            
//            var dateFormat = "yyyy 'წლის' dd MM, dddd";
        //            var result = getDatePicker().parse("2000 წლის 3 12, Saturday", dateFormat);
//            assertTrue(isValidDate(2000, 12, 3, result));
//        }
        
        function test_parse_Tibetian_long_date_format() {
            
            var dateFormat = "yyyy'ལོའི་ཟླ' M'ཚེས' d";
            var result = getDatePicker().parse("2000ལོའི་ཟླ 12ཚེས 23", dateFormat);
            assertTrue(isValidDate(2000, 12, 23, result));
        }

        /////min and max date testing
        function test_parse_date_less_than_min_date_should_return_null() {
            
            var dateFormat = "MM/dd/yyyy";
            var result = getDatePicker().parse("01/23/1800", dateFormat);
            assertNull(result);
        }

        function test_parse_date_bigger_than_min_date_should_return_null() {

            var dateFormat = "MM/dd/yyyy";
            var result = getDatePicker().parse("01/23/2100", dateFormat);
            assertNull(result);
        }

        function test_parse_date_equal_to_min_should_parsed_correctly() {
            
            var dateFormat = "MM/dd/yyyy";

            var result = getDatePicker().parse("1/1/1900", dateFormat); //in FF new Date(1899, 11, 31) == 1.1.1900
            assertTrue(isValidDate(1900, 1, 1, result));
        }

        function test_parse_date_equal_to_max_should_parsed_correctly() {
        
            var dateFormat = "MM/dd/yyyy";

            var result = getDatePicker().parse("1/1/2100", dateFormat);
            assertTrue(isValidDate(2100, 1, 1, result));
        }
        
    </script>
    
    <%= Html.Telerik().DatePicker()
            .Name("DatePicker")
            .MinDate(new DateTime(1900, 01,01))
            .MaxDate(new DateTime(2100, 01, 01))
    %>
    
    <% Html.Telerik().ScriptRegistrar()
           .Scripts(scripts => scripts
               .Add("telerik.common.js")
               .Add("telerik.datepicker.js")
               .Add("telerik.calendar.js")); %>
</asp:Content>
