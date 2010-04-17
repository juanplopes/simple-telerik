<%@ Page Title="CollapseDelay Tests" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        CollapseDelay Tests</h2>
     <%= Html.Telerik().NumericTextBox()
           .Name("numerictextbox1")
           .MinValue(-10.2)
           .MaxValue(10000)
           .IncrementStep(1.44)
           .EmptyMessage("Enter text")
           %>
       <br />
     <%= Html.Telerik().NumericTextBox()
           .Name("numerictextbox")
           .MinValue(-10)
           .EmptyMessage("Enter text") %>

    <script type="text/javascript">

        function getInput(selector) {
            return $(selector || "#numerictextbox").data("tTextBox");
        }

        function test_on_load_should_create_text_input_appended_to_the_div() {
            assertTrue($('#numerictextbox1').find('.t-input').length == 2);
        }

        function test_on_load_should_create_text_input_with_waterMarkText_value_if_no_value() {
            assertTrue($('#numerictextbox1').find('.t-input:last').attr("value") == "Enter text");
        }

        function test_on_load_should_hide_original_input() {
            assertFalse($('#numerictextbox1').find('.t-input:first').is(':visible'))
        }

        function test_input_should_not_allow_entering_of_char() {
            var which = "65"; //'a'
            var isDefaultPrevent = false;
            var $input = $('#numerictextbox').find('> .t-input:last');

            $input.trigger({ type: "keypress",
                keyCode: which,
                preventDefault: function() {
                    isDefaultPrevent = true;
                }
            });

            assertTrue(isDefaultPrevent);
        }

        function test_input_should_allow_entering_digit() {
            var keyCode = "48"; //'0'
            var isDefaultPrevent = false;
            var $input = $('#numerictextbox').find('> .t-input:last');
            $input.trigger({ type: "keypress",
                keyCode: keyCode,
                preventDefault: function() {
                    isDefaultPrevent = true;
                }
            });
            assertFalse(isDefaultPrevent);
        }

        function test_input_should_increase_value_with_one_step_when_up_arrow_keyboard_is_clicked() {
            var keyCode = "38"; 
            var $input = $('#numerictextbox').find('> .t-input:last');

            getInput().value(null);
            $input.val("");
            
            $input.trigger({ type: "keydown",
                keyCode: keyCode
            });

            assertTrue(getInput().value() == 1);
        }

        function test_input_should_decrease_value_with_one_step_when_down_arrow_keyboard_is_clicked() {
            var keyCode = "40";
            var $input = $('#numerictextbox').find('> .t-input:last');

            getInput().value(null);
            $input.val("");

            $input.trigger({ type: "keydown",
                keyCode: keyCode
            });

            assertTrue(getInput().value() == -1);
        }


        function test_input_should_increase_value_with_one_step_when_up_arrow_is_clicked() {
            var $input = $('#numerictextbox').find('> .t-input:last');
            var $button = $('#numerictextbox').find('> .t-arrow-up');
            
            getInput().value(null);
            $input.val("");

            $button.trigger({ type: "mousedown",
                which: 1
            });

            assertTrue(getInput().value() == 1); 
        }

        function test_input_should_decrease_value_with_one_step_when_down_arrow_is_clicked() {
            var $input = $('#numerictextbox').find('> .t-input:last');
            var $button = $('#numerictextbox').find('> .t-arrow-down');

            getInput().value(null);
            $input.val("");

            $button.trigger({ type: "mousedown",
                which: 1
            });
            
            assertTrue(getInput().value() == -1);
        }

        function test_input_should_allow_entering_system_keys() {
            var keyCode = "0"; //'system keys'
            var isDefaultPrevent = false;
            var $input = $('#numerictextbox').find('> .t-input:last');
            $input.trigger({ type: "keypress",
                keyCode: keyCode,
                preventDefault: function() {
                    isDefaultPrevent = true;
                }
            });
            assertFalse(isDefaultPrevent);
        }

        function test_input_should_allow_minus_in_first_position() {
            
            var keyCode = "45";  // minus
            var isDefaultPrevent = false;
        
            var $input = $('#numerictextbox').find('> .t-input:last');

            $input.val('');

            $input.trigger({ type: "keypress",
                keyCode: keyCode,
                preventDefault: function() {
                    isDefaultPrevent = true;
                }
            });
            assertFalse(isDefaultPrevent);
        }

        function test_input_should_not_allow_minus_if_not_in_first_position() {
            var keyCode = "45";  // minus
            var isDefaultPrevent = false;

            var $input = $('#numerictextbox').find('> .t-input:last');

            $input.val('1');

            $input.trigger({ type: "keypress",
                keyCode: keyCode,
                preventDefault: function() {
                    isDefaultPrevent = true;
                }
            });

            assertTrue(isDefaultPrevent);
        }

        function test_input_should_allow_decimal_separator() {
            var keyCode = "190";  // '.'
            var isDefaultPrevent = false;
            var $input = $('#numerictextbox').find('> .t-input:last');

            $input.val('1');

            getInput().separator = '.'

            $input.trigger({ type: "keydown",
                keyCode: keyCode,
                preventDefault: function() {
                    isDefaultPrevent = true;
                }
            });

            assertFalse(isDefaultPrevent);
        }

        function test_input_should_not_allow_decimal_separator_if_input_is_empty() {
            var keyCode = "190";  // '.'
            var isDefaultPrevent = false;
            var $input = $('#numerictextbox').find('> .t-input:last');

            $input.val('');

            getInput().separator = '.'

            $input.trigger({ type: "keydown",
                keyCode: keyCode,
                preventDefault: function() {
                    isDefaultPrevent = true;
                }
            });

            assertTrue(isDefaultPrevent);
        }

        function test_input_should_not_allow_decimal_separator_if_it_is_already_entered() {
            var keyCode = "190";  // '.'
            var isDefaultPrevent = false;
            var $input = $('#numerictextbox').find('> .t-input:last');

            $input.val('1.');

            getInput().separator = '.'

            $input.trigger({ type: "keydown",
                keyCode: keyCode,
                preventDefault: function() {
                    isDefaultPrevent = true;
                }
            });

            assertTrue(isDefaultPrevent);
        }

        function test_if_change_input_value_manually_should_parse_entered_value_on_focus() {

            var input = getInput();

            input.value(null);
            
            var $input = $('#numerictextbox').find('> .t-input:last');
            $input.val('123');
            $input.focus();

            assertTrue(input.val == 123);
        }

        function test_value_method_should_set_val_property() {

            var input = getInput();

            input.value(123);

            assertTrue(input.val == 123);
        }

        function test_value_method_should_synq_both_inputs() {

            var input = getInput();

            input.value(123);
            var $inputs = $('#numerictextbox').find('> .t-input');
            
            assertTrue($inputs[0].value == '123');
            assertTrue($inputs[1].value == '123');
        }

        function test_if_input_value_is_changed_manually_should_be_able_to_parse_it_on_focus() {
            var input = getInput();

            input.value(123);
            var $input = $('#numerictextbox').find('> .t-input:last');
            $input.val('100').focus();

            assertTrue(input.value() == 100);
        }

        function test_if_input_value_is_changed_manually_should_be_able_to_parse_it_on_up_button() {
            var input = getInput();
            input.value(123);
            
            $('#numerictextbox').find('> .t-input:last').val('100');
            $('#numerictextbox').find('> .t-arrow-up')
                                .trigger({ type: "mousedown",
                                    which: 1
                                });
 
            assertTrue(input.value() == 101);
        }

        function test_if_input_value_is_changed_manually_should_be_able_to_parse_it_on_down_button() {
            var input = getInput();

            input.value(123);
            
            $('#numerictextbox').find('> .t-input:last').val('100');
            $('#numerictextbox').find('> .t-arrow-down')
                                .trigger({ type: "mousedown",
                                    which: 1
                                });

            assertTrue(input.value() == 99);
        }
    </script>

<% Html.Telerik().ScriptRegistrar().Globalization(true); %>

</asp:Content>