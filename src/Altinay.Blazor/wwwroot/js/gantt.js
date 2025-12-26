// POINTER EVENT SİSTEMİ - KAYNAKTAN ENTEGRE
console.log('POINTER EVENT Sistemi Yükleniyor...');

// State management
window.ganttState = new Map();

// Options
window.ganttOptions = {
    pxPerDay: 1 // 1 pixel = 1 gün
};

// POINTER DOWN - KAYNAKTAN ENTEGRE
window.onPointerDown = function(e, container, dotnetRef, opts) {
    console.log('POINTER DOWN:', e.target);
    
    const bar = e.target.closest('.gantt-bar'); // Hangi bar?
    if (!bar) return;
    
    const isResizeLeft = e.target.classList.contains('gantt-resize-left') && e.offsetX < 10;
    const isResizeRight = e.target.classList.contains('gantt-resize-right') && e.offsetX > 10;
    
    const startX = e.clientX;                   // ilk x
    const startLeft = parseFloat(bar.style.left) || 0;  // barın ilk left'i
    const startWidth = parseFloat(bar.style.width) || 100; // barın ilk width'i
    const id = bar.getAttribute('data-id'); // hangi task?
    
    console.log('POINTER DOWN STATE:');
    console.log('Task ID:', id);
    console.log('Start X:', startX);
    console.log('Start Left:', startLeft);
    console.log('Start Width:', startWidth);
    console.log('Is Resize Left:', isResizeLeft);
    console.log('Is Resize Right:', isResizeRight);
    
    window.ganttState.set(container, {
        startX, startLeft, startWidth, id,
        resizing: isResizeLeft ? "left" : (isResizeRight ? "right" : null),
        moving: !isResizeLeft && !isResizeRight
    });
    
    // Visual feedback
    bar.classList.add('dragging');
    bar.style.opacity = '0.7';
};

// POINTER MOVE - KAYNAKTAN ENTEGRE
window.onPointerMove = function(e, container, dotnetRef, opts) {
    const ctx = window.ganttState.get(container);
    if (!ctx) return;
    
    const dx = e.clientX - ctx.startX;            // kaç px kaydı
    
    const bar = container.querySelector(`.gantt-bar[data-id="${ctx.id}"]`);
    if (!bar) return;
    
    console.log('POINTER MOVE:');
    console.log('DX:', dx);
    console.log('Moving:', ctx.moving);
    console.log('Resizing:', ctx.resizing);
    
    if (ctx.moving) {
        bar.style.left = `${ctx.startLeft + dx}px`; // tüm barı taşır
    } else if (ctx.resizing === "left") {
        bar.style.left = `${ctx.startLeft + dx}px`;
        bar.style.width = `${ctx.startWidth - dx}px`; // soldan kısalt/uzat
    } else if (ctx.resizing === "right") {
        bar.style.width = `${ctx.startWidth + dx}px`; // sağdan kısalt/uzat
    }
};

// POINTER UP - KAYNAKTAN ENTEGRE
window.onPointerUp = function(e, container, dotnetRef, opts) {
    const ctx = window.ganttState.get(container);
    if (!ctx) return;
    
    const dx = e.clientX - ctx.startX;
    const deltaDays = dx / opts.pxPerDay; // px → gün
    
    console.log('POINTER UP:');
    console.log('DX:', dx);
    console.log('Delta Days:', deltaDays);
    console.log('Moving:', ctx.moving);
    console.log('Resizing:', ctx.resizing);
    
    // Remove visual feedback
    const bar = container.querySelector(`.gantt-bar[data-id="${ctx.id}"]`);
    if (bar) {
        bar.classList.remove('dragging');
        bar.style.opacity = '';
    }
    
    // Call C# methods
    try {
        if (ctx.moving) {
            dotnetRef.invokeMethodAsync("OnJsDragFinished", ctx.id, deltaDays);
            console.log('Called OnJsDragFinished');
        } else if (ctx.resizing) {
            dotnetRef.invokeMethodAsync("OnJsResizeFinished", ctx.id, ctx.resizing, deltaDays);
            console.log('Called OnJsResizeFinished');
        }
    } catch (error) {
        console.log('Error calling C# method:', error);
    }
    
    window.ganttState.delete(container);
};

// INIT FUNCTION - KAYNAKTAN ENTEGRE
window.initGanttPointerEvents = function(container, dotnetRef, opts) {
    console.log('POINTER EVENT Sistemi başlatılıyor...');
    
    // Event'leri bağla
    container.addEventListener("pointerdown", function(e) {
        window.onPointerDown(e, container, dotnetRef, opts);
    });
    
    container.addEventListener("pointermove", function(e) {
        window.onPointerMove(e, container, dotnetRef, opts);
    });
    
    container.addEventListener("pointerup", function(e) {
        window.onPointerUp(e, container, dotnetRef, opts);
    });
    
    console.log('POINTER EVENT Sistemi hazır!');
};

// AltinayGantt namespace
window.AltinayGantt = {
    init: function(container, dotnetRef, opts) {
        window.initGanttPointerEvents(container, dotnetRef, opts);
    },
    dispose: function(container) {
        // Cleanup if needed
        window.ganttState.delete(container);
    }
};

// Initialize when page loads
document.addEventListener('DOMContentLoaded', function() {
    console.log('POINTER EVENT Sistemi yüklendi!');
});
