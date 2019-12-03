// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

$('.select-auto-complete').select2();
$('.select-multiple').select2();

$(document).ready(function(){
    $('a[type=submit]').click(function(){
        $(this).closest('form').submit();
    });
});

$(function () {
    $('[type="datetime-local"]')
        .attr({'type':'text', 'id':'datepicker'})
        .flatpickr({
            enableTime: true,
            minDate: "today",
            defaultDate: new Date(),
            time_24hr: true
        });
    
    if ($.validator !== undefined){
        $.validator.methods.range = function (value, element, param) {
            let globalizedValue = value.replace(",", ".");
            return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
        };
        $.validator.methods.number = function (value, element) {
            return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
        }
    }
});