"use strict";
(function () {

    $("#currency_conversion").on("submit", formSubmitHandler);

    function formSubmitHandler() {
        var currency_from = $("#currency_from").val();
        var currency_to = $("#currency_to").val();
        var currency_amount = $("#currency_amount").val();
        $.get("/api/convert/" + currency_amount + "/" + currency_from + "/to/" + currency_to)
            .done(function (data) {
                $("#currency_result").text(data);
                $("#currency_result").parent().removeClass("hide");
                $("#currency_error").addClass("hide");
            })
            .fail(function (error) {
                $("#currency_error").text(error.responseText);
                $("#currency_result").parent().addClass("hide");
                $("#currency_error").removeClass("hide");
            });
        return false;
    }
})();