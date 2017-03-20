var connection = $.hubConnection();
var hubProxy = connection.createHubProxy('dashboardHub');

hubProxy.on('serverSendSomeData', function (data) {
    updateDashboard(data);
});

connection.start()
    .then(function () {
        return hubProxy.invoke('GetDashboard');
    })
    .done(function (data) {
        updateDashboard(data);
    });

function updateDashboard(data) {
    $('#refreshDateTime').text(data.RefreshDateTime);

    UpdateCpuLineChart(data);
    UpdateRamChart(data);
}

var canvasForCpuChart = document.getElementById("cpuChart");
var context2DLine = canvasForCpuChart.getContext("2d");
var lineChartConfig = {
    type: 'line',
    data: {
        datasets: [{
            label: 'cpu',
            data: []
        }]
    }
};
var lineChart = new Chart(context2DLine, lineChartConfig);
function UpdateCpuLineChart(data) {

    lineChartConfig.data.datasets[0].data.push(data.Cpu);
    lineChartConfig.data.labels.push(data.Tick);
    //Update the Line Chart    
    lineChart.update();
}

var context2Ddoughnut = document.getElementById("ramChart").getContext("2d");
var doughnutChartConfig = {
    type: 'doughnut',
    data: {
        labels: ['used','free'],
        datasets: [{
            label: 'Cpu',
            data: [0,100],
            backgroundColor: [
                "#FF6384",
                "#36A2EB"
            ]
        }]
    },
    options: {
        cutoutPercentage: 80,
    }
};
var doughnutChart = new Chart(context2Ddoughnut, doughnutChartConfig);
function UpdateRamChart(data) {

    doughnutChartConfig.data.datasets[0].data[0] = data.Cpu;
    doughnutChartConfig.data.datasets[0].data[1] = data.CpuAvail;
    doughnutChart.update();
}