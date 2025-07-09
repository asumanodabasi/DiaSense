
let weightChart, bmiChart, glucoseChart, hbaChart,preChart;

function updateCharts(chartData) {
    const labels = chartData.map(x => new Date(x.createdAt).toLocaleDateString());
    const weightData = chartData.map(x => x.weight);
    const bmiData = chartData.map(x => x.bmi);
    const glucoseData = chartData.map(x => x.blood_glucose_level);
    const hbaData = chartData.map(x => x.HbA1c_level);
    const preData = chartData.map(x=>x.predict);

    if (weightChart && bmiChart && glucoseChart && hbaChart) {
        weightChart.data.labels = labels;
        weightChart.data.datasets[0].data = weightData;
        weightChart.update();

        bmiChart.data.labels = labels;
        bmiChart.data.datasets[0].data = bmiData;
        bmiChart.update();

        glucoseChart.data.labels = labels;
        glucoseChart.data.datasets[0].data = glucoseData;
        glucoseChart.update();

        hbaChart.data.labels = labels;
        hbaChart.data.datasets[0].data = hbaData;
        hbaChart.update();

        preChart.data.labels = labels;
        preChart.data.datasets[0].data = preData;
        preChart.update();
    }
}

function resetCanvas(id) {
    const oldCanvas = document.getElementById(id);
    const parent = oldCanvas.parentNode;
    oldCanvas.remove();
    const newCanvas = document.createElement("canvas");
    newCanvas.id = id;
    parent.appendChild(newCanvas);
}
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/diagnosisHub")
    .build();

connection.on("ReceiveChartUpdate", function (updatedData) {
    console.log("✔ Veri güncellemesi alındı SignalR ile!");

    const parsedData = typeof updatedData === "string" ? JSON.parse(updatedData) : updatedData;

    try {
        // Güncelleme yöntemi 1: Var olan grafiği güncelle
        updateCharts(parsedData);
    } catch (e) {
        console.error("🛠 updateCharts hatası:", e);

        // Alternatif olarak tamamen yeniden başlat
        resetCanvas("weightChart");
        resetCanvas("bmiChart");
        resetCanvas("glucoseChart");
        resetCanvas("hbaChart");
        initializeCharts(parsedData);
    }
    if (!Array.isArray(parsedData)) {
        console.error("❌ Beklenen dizi değil:", parsedData);
        return;
    }
});

connection.start().then(() => {
    console.log("✔ SignalR bağlantısı kuruldu!");
}).catch(err => console.error(err.toString()));