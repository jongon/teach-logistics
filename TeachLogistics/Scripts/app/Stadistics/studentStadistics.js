$(function () {
    //Costo Total Con Promedio de Fondo - Gráfico Linea
    var totalCostLineData = {
        labels: periods,
        datasets: [
            {
                label: "Promedio Costo total",
                fillColor: "rgba(220,220,220,0.5)",
                strokeColor: "rgba(220,220,220,1)",
                pointColor: "rgba(220,220,220,1)",
                pointStrokeColor: "#fff",
                pointHighlightFill: "#fff",
                pointHighlightStroke: "rgba(220,220,220,1)",
                data: averageCostData
            },
            {
                label: "Costo total",
                fillColor: "rgba(26,179,148,0.5)",
                strokeColor: "rgba(26,179,148,0.7)",
                pointColor: "rgba(26,179,148,1)",
                pointStrokeColor: "#fff",
                pointHighlightFill: "#fff",
                pointHighlightStroke: "rgba(26,179,148,1)",
                data: totalCostData
            }
        ]
    };

    var totalCostLineOptions = {
        scaleShowGridLines: true,
        scaleGridLineColor: "rgba(0,0,0,.05)",
        scaleGridLineWidth: 1,
        bezierCurve: true,
        bezierCurveTension: 0.4,
        pointDot: true,
        pointDotRadius: 4,
        pointDotStrokeWidth: 1,
        pointHitDetectionRadius: 20,
        datasetStroke: true,
        datasetStrokeWidth: 2,
        datasetFill: true,
        responsive: true,
    };

    var ctx = document.getElementById("CostTotalLine").getContext("2d");
    var myNewChart = new Chart(ctx).Line(totalCostLineData, totalCostLineOptions);
    legend(document.getElementById("CostTotalLineLegend"), totalCostLineData);

    //Costo Total Con Promedio de Fondo - Gráfico Barra
    var totalCostBarData = {
        labels: periods,
        datasets: [
            {
                label: "Promedio Costo total",
                fillColor: "rgba(220,220,220,0.5)",
                strokeColor: "rgba(220,220,220,0.8)",
                highlightFill: "rgba(220,220,220,0.75)",
                highlightStroke: "rgba(220,220,220,1)",
                data: averageCostData
            },
            {
                label: "Costo total",
                fillColor: "rgba(26,179,148,0.5)",
                strokeColor: "rgba(26,179,148,0.8)",
                highlightFill: "rgba(26,179,148,0.75)",
                highlightStroke: "rgba(26,179,148,1)",
                data: totalCostData
            }
        ]
    };

    var totalCostBarOptions = {
        scaleBeginAtZero: true,
        scaleShowGridLines: true,
        scaleGridLineColor: "rgba(0,0,0,.05)",
        scaleGridLineWidth: 1,
        barShowStroke: true,
        barStrokeWidth: 2,
        barValueSpacing: 5,
        barDatasetSpacing: 1,
        responsive: true,
    }

    var ctx = document.getElementById("CostTotalBar").getContext("2d");
    var myNewChart = new Chart(ctx).Bar(totalCostBarData, totalCostBarOptions);
    legend(document.getElementById("CostTotalBarLegend"), totalCostLineData);

    ////Costo Inventario Total con promedio de fondo - Gráfico Linea
    var totalStockLineData = {
        labels: periods,
        datasets: [
            {
                label: "Promedio ",
                fillColor: "rgba(220,220,220,0.5)",
                strokeColor: "rgba(220,220,220,1)",
                pointColor: "rgba(220,220,220,1)",
                pointStrokeColor: "#fff",
                pointHighlightFill: "#fff",
                pointHighlightStroke: "rgba(220,220,220,1)",
                data: averageStockData
            },
            {
                label: "Example dataset",
                fillColor: "rgba(26,179,148,0.5)",
                strokeColor: "rgba(26,179,148,0.7)",
                pointColor: "rgba(26,179,148,1)",
                pointStrokeColor: "#fff",
                pointHighlightFill: "#fff",
                pointHighlightStroke: "rgba(26,179,148,1)",
                data: totalStockData
            }
        ]
    };

    var totalStockLineOptions = {
        scaleShowGridLines: true,
        scaleGridLineColor: "rgba(0,0,0,.05)",
        scaleGridLineWidth: 1,
        bezierCurve: true,
        bezierCurveTension: 0.4,
        pointDot: true,
        pointDotRadius: 4,
        pointDotStrokeWidth: 1,
        pointHitDetectionRadius: 20,
        datasetStroke: true,
        datasetStrokeWidth: 2,
        datasetFill: true,
        responsive: true,
    };

    var ctx = document.getElementById("StockLine").getContext("2d");
    var myNewChart = new Chart(ctx).Line(totalStockLineData, totalStockLineOptions);
    legend(document.getElementById("StockLineLegend"), totalCostLineData);

    ////Costo Inventario Total Con Promedio de Fondo - Gráfico barra
    var totalStockBarData = {
        labels: periods,
        datasets: [
            {
                label: "My First dataset",
                fillColor: "rgba(220,220,220,0.5)",
                strokeColor: "rgba(220,220,220,0.8)",
                highlightFill: "rgba(220,220,220,0.75)",
                highlightStroke: "rgba(220,220,220,1)",
                data: averageStockData
            },
            {
                label: "My Second dataset",
                fillColor: "rgba(26,179,148,0.5)",
                strokeColor: "rgba(26,179,148,0.8)",
                highlightFill: "rgba(26,179,148,0.75)",
                highlightStroke: "rgba(26,179,148,1)",
                data: totalStockData
            }
        ]
    };

    var totalStockBarOptions = {
        scaleBeginAtZero: true,
        scaleShowGridLines: true,
        scaleGridLineColor: "rgba(0,0,0,.05)",
        scaleGridLineWidth: 1,
        barShowStroke: true,
        barStrokeWidth: 2,
        barValueSpacing: 5,
        barDatasetSpacing: 1,
        responsive: true,
    }

    var ctx = document.getElementById("StockBar").getContext("2d");
    var myNewChart = new Chart(ctx).Bar(totalStockBarData, totalStockBarOptions);
    legend(document.getElementById("StockBarLegend"), totalCostLineData);

    //Demanda insatisfecha Total con promedio de fondo - Gráfico Linea
    var totalDemandLineData = {
        labels: periods,
        datasets: [
            {
                label: "Example dataset",
                fillColor: "rgba(220,220,220,0.5)",
                strokeColor: "rgba(220,220,220,1)",
                pointColor: "rgba(220,220,220,1)",
                pointStrokeColor: "#fff",
                pointHighlightFill: "#fff",
                pointHighlightStroke: "rgba(220,220,220,1)",
                data: averageDemandData
            },
            {
                label: "Example dataset",
                fillColor: "rgba(26,179,148,0.5)",
                strokeColor: "rgba(26,179,148,0.7)",
                pointColor: "rgba(26,179,148,1)",
                pointStrokeColor: "#fff",
                pointHighlightFill: "#fff",
                pointHighlightStroke: "rgba(26,179,148,1)",
                data: totalDemandData
            }
        ]
    };

    var totalDemandLineOptions = {
        scaleShowGridLines: true,
        scaleGridLineColor: "rgba(0,0,0,.05)",
        scaleGridLineWidth: 1,
        bezierCurve: true,
        bezierCurveTension: 0.4,
        pointDot: true,
        pointDotRadius: 4,
        pointDotStrokeWidth: 1,
        pointHitDetectionRadius: 20,
        datasetStroke: true,
        datasetStrokeWidth: 2,
        datasetFill: true,
        responsive: true,
    };

    var ctx = document.getElementById("DemandLine").getContext("2d");
    var myNewChart = new Chart(ctx).Line(totalDemandLineData, totalDemandLineOptions);
    legend(document.getElementById("DemandLineLegend"), totalCostLineData);

    //Demanda insatisfecha Total con promedio de fondo - Gráfico barra
    var totalDemandBarData = {
        labels: periods,
        datasets: [
            {
                label: "My First dataset",
                fillColor: "rgba(220,220,220,0.5)",
                strokeColor: "rgba(220,220,220,0.8)",
                highlightFill: "rgba(220,220,220,0.75)",
                highlightStroke: "rgba(220,220,220,1)",
                data: averageDemandData
            },
            {
                label: "My Second dataset",
                fillColor: "rgba(26,179,148,0.5)",
                strokeColor: "rgba(26,179,148,0.8)",
                highlightFill: "rgba(26,179,148,0.75)",
                highlightStroke: "rgba(26,179,148,1)",
                data: totalDemandData
            }
        ]
    };

    var totalDemandBarOptions = {
        scaleBeginAtZero: true,
        scaleShowGridLines: true,
        scaleGridLineColor: "rgba(0,0,0,.05)",
        scaleGridLineWidth: 1,
        barShowStroke: true,
        barStrokeWidth: 2,
        barValueSpacing: 5,
        barDatasetSpacing: 1,
        responsive: true,
    }

    var ctx = document.getElementById("DemandBar").getContext("2d");
    var myNewChart = new Chart(ctx).Bar(totalDemandBarData, totalDemandBarOptions);
    legend(document.getElementById("DemandBarLegend"), totalCostLineData);

    //Costo por ordenar Total con promedio de fondo - Gráfico Linea
    var totalOrderLineData = {
        labels: periods,
        datasets: [
            {
                label: "Example dataset",
                fillColor: "rgba(220,220,220,0.5)",
                strokeColor: "rgba(220,220,220,1)",
                pointColor: "rgba(220,220,220,1)",
                pointStrokeColor: "#fff",
                pointHighlightFill: "#fff",
                pointHighlightStroke: "rgba(220,220,220,1)",
                data: averageOrderData
            },
            {
                label: "Example dataset",
                fillColor: "rgba(26,179,148,0.5)",
                strokeColor: "rgba(26,179,148,0.7)",
                pointColor: "rgba(26,179,148,1)",
                pointStrokeColor: "#fff",
                pointHighlightFill: "#fff",
                pointHighlightStroke: "rgba(26,179,148,1)",
                data: totalOrderData
            }
        ]
    };

    var totalOrderLineOptions = {
        scaleShowGridLines: true,
        scaleGridLineColor: "rgba(0,0,0,.05)",
        scaleGridLineWidth: 1,
        bezierCurve: true,
        bezierCurveTension: 0.4,
        pointDot: true,
        pointDotRadius: 4,
        pointDotStrokeWidth: 1,
        pointHitDetectionRadius: 20,
        datasetStroke: true,
        datasetStrokeWidth: 2,
        datasetFill: true,
        responsive: true,
    };

    var ctx = document.getElementById("OrderLine").getContext("2d");
    var myNewChart = new Chart(ctx).Line(totalOrderLineData, totalOrderLineOptions);
    legend(document.getElementById("OrderLineLegend"), totalCostLineData);

    //Costo por ordenar Total con promedio de fondo - Gráfico barra
    var totalOrderBarData = {
        labels: periods,
        datasets: [
            {
                label: "My First dataset",
                fillColor: "rgba(220,220,220,0.5)",
                strokeColor: "rgba(220,220,220,0.8)",
                highlightFill: "rgba(220,220,220,0.75)",
                highlightStroke: "rgba(220,220,220,1)",
                data: averageOrderData
            },
            {
                label: "My Second dataset",
                fillColor: "rgba(26,179,148,0.5)",
                strokeColor: "rgba(26,179,148,0.8)",
                highlightFill: "rgba(26,179,148,0.75)",
                highlightStroke: "rgba(26,179,148,1)",
                data: totalOrderData
            }
        ]
    };

    var totalOrderBarOptions = {
        scaleBeginAtZero: true,
        scaleShowGridLines: true,
        scaleGridLineColor: "rgba(0,0,0,.05)",
        scaleGridLineWidth: 1,
        barShowStroke: true,
        barStrokeWidth: 2,
        barValueSpacing: 5,
        barDatasetSpacing: 1,
        responsive: true,
    }

    var ctx = document.getElementById("OrderBar").getContext("2d");
    var myNewChart = new Chart(ctx).Bar(totalOrderBarData, totalOrderBarOptions);
    legend(document.getElementById("OrderBarLegend"), totalCostLineData);
});