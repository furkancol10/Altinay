// Toast Notification Sistemi
window.showToastNotification = (title, message, type = 'info') => {
    // Toast container oluştur
    let toastContainer = document.getElementById('toast-container');
    if (!toastContainer) {
        toastContainer = document.createElement('div');
        toastContainer.id = 'toast-container';
        toastContainer.style.cssText = `
            position: fixed;
            top: 20px;
            right: 20px;
            z-index: 9999;
            max-width: 400px;
        `;
        document.body.appendChild(toastContainer);
    }

    // Toast elementi oluştur
    const toast = document.createElement('div');
    toast.className = 'toast-notification';
    toast.style.cssText = `
        background: white;
        border-radius: 8px;
        box-shadow: 0 4px 12px rgba(0,0,0,0.15);
        margin-bottom: 10px;
        padding: 16px;
        border-left: 4px solid ${getTypeColor(type)};
        animation: slideInRight 0.3s ease-out;
        cursor: pointer;
        transition: all 0.3s ease;
    `;

    // Icon belirle
    const icon = getTypeIcon(type);
    
    // Toast içeriği
    toast.innerHTML = `
        <div style="display: flex; align-items: flex-start;">
            <div style="margin-right: 12px; font-size: 20px;">${icon}</div>
            <div style="flex: 1;">
                <div style="font-weight: 600; margin-bottom: 4px; color: #333;">${title}</div>
                <div style="color: #666; font-size: 14px;">${message}</div>
            </div>
            <button onclick="this.parentElement.parentElement.remove()" style="background: none; border: none; font-size: 18px; color: #999; cursor: pointer; margin-left: 8px;">×</button>
        </div>
    `;

    // Toast'a tıklanınca kapat
    toast.onclick = () => {
        toast.style.animation = 'slideOutRight 0.3s ease-in';
        setTimeout(() => toast.remove(), 300);
    };

    // Toast'ı ekle
    toastContainer.appendChild(toast);

    // 5 saniye sonra otomatik kapat
    setTimeout(() => {
        if (toast.parentElement) {
            toast.style.animation = 'slideOutRight 0.3s ease-in';
            setTimeout(() => toast.remove(), 300);
        }
    }, 5000);
};

function getTypeColor(type) {
    switch(type) {
        case 'welcome': return '#28a745';
        case 'daily-start': return '#ffc107';
        case 'daily-end': return '#17a2b8';
        case 'break': return '#28a745';
        case 'reminder': return '#007bff';
        case 'todo-overload': return '#dc3545';
        case 'deadline': return '#fd7e14';
        case 'team': return '#6f42c1';
        case 'comment': return '#17a2b8';
        default: return '#6c757d';
    }
}

function getTypeIcon(type) {
    switch(type) {
        case 'welcome': return '👋';
        case 'daily-start': return '🌅';
        case 'daily-end': return '🌆';
        case 'break': return '☕';
        case 'reminder': return '🔔';
        case 'todo-overload': return '⚠️';
        case 'deadline': return '⏰';
        case 'team': return '👥';
        case 'comment': return '💬';
        default: return '📢';
    }
}

// localStorage fonksiyonları
window.saveNotificationToStorage = (notification) => {
    try {
        // Mevcut bildirimleri al
        let notifications = JSON.parse(localStorage.getItem('globalNotifications') || '[]');
        
        // Yeni bildirimi ekle
        notifications.unshift(notification);
        
        // Son 50 bildirimi sakla
        if (notifications.length > 50) {
            notifications = notifications.slice(0, 50);
        }
        
        // localStorage'a kaydet
        localStorage.setItem('globalNotifications', JSON.stringify(notifications));
        
        // Tüm açık sayfalara bildirim gönder
        window.dispatchEvent(new CustomEvent('newNotification', { detail: notification }));
        
        console.log('Bildirim localStorage\'a kaydedildi:', notification);
    } catch (error) {
        console.error('Bildirim kaydetme hatası:', error);
    }
};

window.getNotificationsFromStorage = () => {
    try {
        return JSON.parse(localStorage.getItem('globalNotifications') || '[]');
    } catch (error) {
        console.error('Bildirim okuma hatası:', error);
        return [];
    }
};

// CSS animasyonları ekle
const style = document.createElement('style');
style.textContent = `
    @keyframes slideInRight {
        from {
            transform: translateX(100%);
            opacity: 0;
        }
        to {
            transform: translateX(0);
            opacity: 1;
        }
    }
    
    @keyframes slideOutRight {
        from {
            transform: translateX(0);
            opacity: 1;
        }
        to {
            transform: translateX(100%);
            opacity: 0;
        }
    }
`;
document.head.appendChild(style);
