// Gantt Chart JavaScript Functions
let ganttChart = null;

/**
 * Script yükleme fonksiyonu
 */
window.loadScript = function(src) {
    return new Promise((resolve, reject) => {
        // Script zaten yüklenmiş mi kontrol et
        if (document.querySelector(`script[src="${src}"]`)) {
            resolve();
            return;
        }

        const script = document.createElement('script');
        script.src = src;
        script.onload = resolve;
        script.onerror = reject;
        document.head.appendChild(script);
    });
};

/**
 * Chart.js ile Gantt Chart'ı başlat
 */
window.initializeGanttChart = function(canvasId, chartData) {
    const ctx = document.getElementById(canvasId);
    if (!ctx) {
        console.error('Canvas element not found:', canvasId);
        return;
    }

    // Mevcut chart'ı temizle
    if (ganttChart) {
        ganttChart.destroy();
    }

    // Chart.js'i yükle
    if (typeof Chart === 'undefined') {
        loadChartJS().then(() => {
            createGanttChart(ctx, chartData);
        });
    } else {
        createGanttChart(ctx, chartData);
    }
};

/**
 * Chart.js kütüphanesini yükle
 */
function loadChartJS() {
    return new Promise((resolve, reject) => {
        if (typeof Chart !== 'undefined') {
            resolve();
            return;
        }

        // Chart.js zaten yüklenmiş, sadece adapter'ı kontrol et
        if (typeof Chart !== 'undefined' && Chart.adapters && Chart.adapters._adapters) {
            resolve();
            return;
        }

        // Adapter'ı yükle
        const timeScript = document.createElement('script');
        timeScript.src = 'https://cdn.jsdelivr.net/npm/chartjs-adapter-date-fns@3.0.0/dist/chartjs-adapter-date-fns.bundle.min.js';
        timeScript.onload = resolve;
        timeScript.onerror = reject;
        document.head.appendChild(timeScript);
    });
}

/**
 * Gantt Chart'ı oluştur
 */
function createGanttChart(ctx, chartData) {
    try {
        ganttChart = new Chart(ctx, {
            type: 'scatter',
            data: {
                datasets: chartData.data.datasets.map(dataset => ({
                    ...dataset,
                    data: dataset.data.map(point => ({
                        x: new Date(point.x),
                        y: point.y
                    }))
                }))
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                interaction: {
                    intersect: false,
                    mode: 'index'
                },
                scales: {
                    x: {
                        type: 'time',
                        time: {
                            unit: 'day',
                            displayFormats: {
                                day: 'MMM dd'
                            }
                        },
                        title: {
                            display: true,
                            text: 'Tarih',
                            font: {
                                size: 14,
                                weight: 'bold'
                            }
                        },
                        grid: {
                            color: 'rgba(0,0,0,0.1)'
                        }
                    },
                    y: {
                        display: false
                    }
                },
                plugins: {
                    legend: {
                        display: false
                    },
                    tooltip: {
                        backgroundColor: 'rgba(0,0,0,0.8)',
                        titleColor: 'white',
                        bodyColor: 'white',
                        borderColor: 'rgba(255,255,255,0.2)',
                        borderWidth: 1,
                        callbacks: {
                            title: function(context) {
                                return context[0].dataset.label;
                            },
                            label: function(context) {
                                const startDate = new Date(context.parsed.x);
                                const endDate = new Date(context.parsed.x + (24 * 60 * 60 * 1000 * 7)); // 7 gün varsayım
                                return [
                                    `Başlangıç: ${startDate.toLocaleDateString('tr-TR')}`,
                                    `Bitiş: ${endDate.toLocaleDateString('tr-TR')}`,
                                    `Durum: ${getStatusFromColor(context.dataset.borderColor)}`
                                ];
                            }
                        }
                    }
                },
                elements: {
                    point: {
                        radius: 0,
                        hoverRadius: 8
                    },
                    line: {
                        borderWidth: 3,
                        tension: 0
                    }
                }
            }
        });

        console.log('Gantt Chart initialized successfully');
    } catch (error) {
        console.error('Error creating Gantt Chart:', error);
    }
}

/**
 * Renk kodundan durum metnini al
 */
function getStatusFromColor(color) {
    const statusMap = {
        '#1976d2': 'To Do',
        '#f57c00': 'In Progress',
        '#7b1fa2': 'Review',
        '#388e3c': 'Done'
    };
    return statusMap[color] || 'Unknown';
}

/**
 * Chart'ı dışa aktar
 */
window.exportChart = function(canvasId) {
    const canvas = document.getElementById(canvasId);
    if (!canvas) {
        console.error('Canvas element not found:', canvasId);
        return;
    }

    // PNG olarak indir
    const link = document.createElement('a');
    link.download = 'gantt-chart.png';
    link.href = canvas.toDataURL();
    link.click();
};

/**
 * Chart'ı temizle
 */
window.destroyGanttChart = function() {
    if (ganttChart) {
        ganttChart.destroy();
        ganttChart = null;
    }
};

/**
 * Chart'ı güncelle
 */
window.updateGanttChart = function(canvasId, newData) {
    if (ganttChart) {
        ganttChart.data.datasets = newData.data.datasets.map(dataset => ({
            ...dataset,
            data: dataset.data.map(point => ({
                x: new Date(point.x),
                y: point.y
            }))
        }));
        ganttChart.update();
    } else {
        window.initializeGanttChart(canvasId, newData);
    }
};

/**
 * HTML Gantt Chart'ı göster (fallback)
 */
window.showHtmlGanttChart = function() {
    const canvas = document.getElementById('ganttChart');
    const htmlChart = document.getElementById('htmlGanttChart');
    
    if (canvas && htmlChart) {
        canvas.style.display = 'none';
        htmlChart.style.display = 'block';
        console.log('HTML Gantt Chart shown as fallback');
    }
};

/**
 * XML dosyasını indir
 */
window.downloadXmlFile = function(xmlContent, fileName) {
    const blob = new Blob([xmlContent], { type: 'application/xml' });
    const url = window.URL.createObjectURL(blob);
    const link = document.createElement('a');
    link.href = url;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    window.URL.revokeObjectURL(url);
    console.log(`XML file downloaded: ${fileName}`);
};

/**
 * XML dosya input'unu tetikle
 */
window.triggerXmlFileInput = function() {
    const input = document.createElement('input');
    input.type = 'file';
    input.accept = '.xml';
    input.style.display = 'none';
    
    input.onchange = function(event) {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function(e) {
                const xmlContent = e.target.result;
                // Blazor component'e XML içeriğini gönder
                if (window.DotNet && window.DotNet.invokeMethodAsync) {
                    window.DotNet.invokeMethodAsync('OnXmlFileUploaded', xmlContent);
                }
            };
            reader.readAsText(file);
        }
    };
    
    document.body.appendChild(input);
    input.click();
    document.body.removeChild(input);
};

/**
 * Drag & Drop için JavaScript helper fonksiyonları
 */

// Dragged task ID'sini sakla
window.setDraggedTask = function(taskId) {
    window.draggedTaskId = taskId;
    console.log('Dragged task set:', taskId);
};

// Timeline'ın başlangıç X pozisyonunu al
window.getTimelineStartX = function() {
    const timeline = document.querySelector('.timeline-body');
    if (timeline) {
        const rect = timeline.getBoundingClientRect();
        return rect.left;
    }
    return 0;
};

// Timeline'ın rect'ini al
window.getTimelineRect = function() {
    const timeline = document.querySelector('.timeline-body');
    if (timeline) {
        return timeline.getBoundingClientRect();
    }
    return null;
};

// Mouse pozisyonunu timeline pozisyonuna çevir
window.convertMouseToTimelinePosition = function(mouseX) {
    const timelineStart = window.getTimelineStartX();
    return mouseX - timelineStart;
};
