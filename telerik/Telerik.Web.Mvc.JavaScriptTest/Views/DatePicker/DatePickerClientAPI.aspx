<%@ Page Title="SingleExpandItem ClientAPI tests" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Client API Tests</h2>
    
   <script type="text/javascript">

       function getDatePicker() {
           return $('#DatePicker').data('tDatePicker');
       }

       function test_value_should_return_selected_date() {
           getDatePicker().show();

           var $calendar = getDatePicker().$calendar();

           var today = new Date();

           var days = $calendar.find('td:not(.t-other-month)');

           var day = $.grep(days, function(n) {
               return $('.t-link', n).html() == today.getDate();
           });

           $(day).click();

           assertTrue(isValidDate(today, getDatePicker().value()));
       }

       function isValidDate(date1, date2) {
           var isValid = true;

           if (date1.getFullYear() != date2.getFullYear())
               isValid = false;
           else if (date1.getMonth() != date2.getMonth())
               isValid = false;
           else if (date1.getDate() != date2.getDate())
               isValid = false;

           return isValid;
       }

       var isChanged;
       var isRaised;
       
       function test_clicking_toggle_button_should_raise_onOpen_event() {

           var button = $('#DatePicker > .t-icon');

           isRaised = false;

           button.trigger('click');

           assertTrue(isRaised);
       }
       
       function test_focusing_input_should_raise_onOpen_event() {

           var input = $('#DatePicker > .t-input');

           isRaised = false;

           input.focus();

           assertTrue(isRaised);
       }       

       function test_clicking_tab_should_raise_onClose() {

           getDatePicker().show();

           isRaised = false;
           var input = $('#DatePicker > .t-input');
           input.trigger({ type: "keydown", keyCode: 9});        

           assertTrue(isRaised);
       }

       function test_clicking_escape_should_raise_onClose() {

           getDatePicker().show();

           isRaised = false;
           
           var input = $('#DatePicker > .t-input');
           input.trigger({ type: "keydown", keyCode: 27 });

           assertTrue(isRaised);
       }


       function test_clicking_enter_should_raise_onClose() {

           getDatePicker().show();

           isRaised = false;

           var input = $('#DatePicker > .t-input');
           input.trigger({ type: "keydown", keyCode: 13 });

           assertTrue(isRaised);
       }

       //handlers
       function onLoad(sender, args) {
           isRaised = true;
       }

       function onChange(sender, args) {
           isChanged = true;
       }

       function onClose(sender, args) {
           isRaised = true;
       }

       function onOpen(sender, args) {
           isRaised = true;
       }
   </script>

 <%= Html.Telerik().DatePicker().Name("DatePicker")
                   .MinDate(new DateTime(1600, 1,1))
                   .MaxDate(new DateTime(2400, 1, 1))
                   .ClientEvents(events => events.OnLoad("onLoad")
                                                 .OnChange("onChange")
                                                 .OnClose("onClose")
                                                 .OnOpen("onOpen"))
                                                 
 %>
    
    <% Html.Telerik().ScriptRegistrar()
           .Scripts(scripts => scripts
               .Add("telerik.common.js")
               .Add("telerik.datepicker.js")
               .Add("telerik.calendar.js")); %>

</asp:Content>
<%--
       function test_clicking_enter_should_raise_onChange() {
           
           getDatePicker().togglePopup();

           isChanged = false;

           var input = $('#DatePicker > .t-input');
           
           input.trigger({ type: "keydown", keyCode: 13 });

           assertTrue(isChanged);
       }

       function test_clicking_day_should_raise_onChange() {

           getDatePicker().show();

           var $calendar = getDatePicker().$calendar();

           isChanged = false;

           var today = new Date();
           
           var days = $calendar.find('td:not(.t-other-month)');

           var day = $.grep(days, function(n) {
                return $('.t-link', n).html() == today.getDate();
            });
           
           $(day).click();
           
           assertTrue(isChanged);
       }
       
       function test_value_should_be_null_if_the_input_is_empty() {

           getDatePicker().show();
           var $calendar = getDatePicker().$calendar();

           var today = new Date();
           var days = $calendar.find('td:not(.t-other-month)');
           var day = $.grep(days, function(n) {
               return $('.t-link', n).html() == today.getDate();
           });
           
           $(day).click();
           debugger;

           getDatePicker().show();
           
           $('#DatePicker > .t-input').val('');
           $(document).click();
           
           assertTrue(getDatePicker().value() === null);
       }
--%>