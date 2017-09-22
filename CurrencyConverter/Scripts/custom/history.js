"use strict";
(function () {

    var graph = new Rickshaw.Graph({
        element: document.querySelector("#history_graph"),
        renderer: 'line',
        height: 220,
        series: [{
            color: "#008CBA",
            data: [{ x:0, y:0}]
         }]
    });
    var axes = new Rickshaw.Graph.Axis.Time({ graph: graph });
    var hoverDetail = new Rickshaw.Graph.HoverDetail({ graph: graph });

    $("#history_currency").on("change", currencyChangeHandler);

    function currencyChangeHandler(e) {
        var newCurrency = this.value;
        $("#history_currency").prop("disabled", true);
        $.get("/api/history/" + newCurrency)
            .done(function (data) {
                updateGraph(data, newCurrency);
                $("#history_error").addClass("hide");
                $("#history_graph").removeClass("hide");
                $("#history_currency").prop("disabled", false);
            })
            .fail(function (error) {
                $("#history_error").text(error.responseText);
                $("#history_error").removeClass("hide");
                $("#history_graph").addClass("hide");
                $("#history_currency").prop("disabled", false);
            });
    }

    function updateGraph(jsonResponse, newCurrency) {
        var mappedValues = mapHistory(jsonResponse);
        graph.series[0].data = mappedValues.result;
        graph.series[0].scale = d3.scale.linear().domain([mappedValues.minValue, mappedValues.maxValue]).nice();
        graph.series[0].name = newCurrency;
        graph.render();
        axes.render();
    }

    function mapHistory(jsonResponse) {
        var result = [];
        var minValue = Number.MAX_VALUE;
        var maxValue = Number.MIN_VALUE;
        for (var i = 0; i < jsonResponse.length; i++) {
            var unixTimestamp = Date.parse(jsonResponse[i].Timestamp + ".000Z");
            result[i] = {
                x: unixTimestamp / 1000,
                y: jsonResponse[i].Rate
            }
            minValue = minValue > jsonResponse[i].Rate ? jsonResponse[i].Rate : minValue;
            maxValue = maxValue < jsonResponse[i].Rate ? jsonResponse[i].Rate : maxValue;
        }
        return {
            result: result,
            minValue: minValue,
            maxValue: maxValue
        };
    }

})();