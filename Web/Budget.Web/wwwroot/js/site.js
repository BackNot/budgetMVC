$(document).ready(function() {

    let incomes = parseFloat($("#incomes").val());
    let expenses = parseFloat($("#expenses").val());
    
    // Load google charts
    google.charts.load('current', { 'packages': ['corechart'] });
    google.charts.setOnLoadCallback(drawChart);

    // Draw the chart and set the chart values
    function drawChart() {
    var data = google.visualization.arrayToDataTable([
        ['Money', 'Quantity'],
        ['Income', incomes],
        ['Expense', expenses]
    ]);

    // Optional; add a title and set the width and height of the chart
    var options = {
        'backgroundColor': 'transparent',
        'title': 'Status',
        'width': 550,
        'height': 400,
        'is3D': true
    };

    // Display the chart inside the <div> element with id="piechart"
    var chart = new google.visualization.PieChart(document.getElementById('piechart'));
    chart.draw(data, options);
}

    $("#add-income-submit").click(function () {
        let addModel = {};
        addModel.Date = $("#add-income-date").val();
        addModel.Amount = $("#add-income-amount").val();

        let jsonData = JSON.stringify(addModel);
        $.ajax("expenses/add", {
            method: "POST",
            contentType: "application/json",
            data:  jsonData ,
            success: function (data) {
                console.log(data);
            }
        });
    });
});