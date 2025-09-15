// DRAG AND DROP OPTIMIZED VERSION
console.log('Drag and Drop script loaded');

// Global variables
window.draggedIssueId = null;
window.dragDropInitialized = false;

// Optimized drag and drop setup with event delegation
window.setupDragAndDrop = () => {
    if (window.dragDropInitialized) {
        console.log('Drag and drop already initialized');
        return;
    }
    
    console.log('Setting up optimized drag and drop...');
    
    // Remove existing listeners to prevent duplicates
    document.removeEventListener('dragstart', handleDragStart);
    document.removeEventListener('dragend', handleDragEnd);
    document.removeEventListener('dragover', handleDragOver);
    document.removeEventListener('dragleave', handleDragLeave);
    document.removeEventListener('drop', handleDrop);
    
    // Add event delegation listeners
    document.addEventListener('dragstart', handleDragStart, true);
    document.addEventListener('dragend', handleDragEnd, true);
    document.addEventListener('dragover', handleDragOver, true);
    document.removeEventListener('dragleave', handleDragLeave, true);
    document.addEventListener('drop', handleDrop, true);
    
    window.dragDropInitialized = true;
    console.log('Drag and drop setup complete');
};

// Event handlers with event delegation
function handleDragStart(e) {
    if (!e.target.classList.contains('issue-card')) return;
    
    e.dataTransfer.effectAllowed = 'move';
    e.dataTransfer.setData('text/plain', ''); // Required for Firefox
    
    // Add visual feedback
    e.target.classList.add('dragging');
    
    // Store the dragged issue ID
    const issueId = e.target.getAttribute('data-issue-id');
    if (issueId) {
        window.draggedIssueId = issueId;
        console.log('Dragging issue:', issueId);
    }
}

function handleDragEnd(e) {
    if (!e.target.classList.contains('issue-card')) return;
    
    // Remove visual feedback
    e.target.classList.remove('dragging');
}

function handleDragOver(e) {
    if (!e.target.classList.contains('column-content')) return;
    
    e.preventDefault();
    e.dataTransfer.dropEffect = 'move';
    e.target.classList.add('drag-over');
}

function handleDragLeave(e) {
    if (!e.target.classList.contains('column-content')) return;
    
    if (!e.target.contains(e.relatedTarget)) {
        e.target.classList.remove('drag-over');
    }
}

// Debounced drop handler
let dropTimeout;
function handleDrop(e) {
    if (!e.target.classList.contains('column-content')) return;
    
    e.preventDefault();
    e.target.classList.remove('drag-over');
    
    // Clear previous timeout
    clearTimeout(dropTimeout);
    
    // Debounce the drop operation
    dropTimeout = setTimeout(() => {
        const newStatus = getStatusFromColumn(e.target);
        if (newStatus && window.draggedIssueId) {
            console.log('Dropping issue:', window.draggedIssueId, 'to status:', newStatus);
            
            try {
                if (window.DotNet) {
                    window.DotNet.invokeMethodAsync('Altinay.Blazor', 'OnDropLocal', newStatus);
                }
            } catch (err) {
                console.error('Error calling OnDropLocal:', err);
            }
        }
    }, 50); // 50ms debounce
}

// Helper function to get status from column
function getStatusFromColumn(zone) {
    const column = zone.closest('.kanban-column');
    const title = column.querySelector('.column-title span');
    
    if (title) {
        const text = title.textContent.trim();
        if (text.includes('TO DO')) return 'ToDo';
        if (text.includes('IN PROGRESS')) return 'InProgress';
        if (text.includes('IN REVIEW')) return 'InReview';
        if (text.includes('DONE')) return 'Done';
    }
    
    return null;
}

// Refresh function for DOM changes
window.refreshDragAndDrop = () => {
    console.log('Refreshing drag and drop...');
    window.dragDropInitialized = false;
    window.setupDragAndDrop();
};

window.setDraggedIssue = (issueId) => {
    window.draggedIssueId = issueId;
};

// Auto-initialize when DOM is ready
if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', () => {
        setTimeout(() => {
            window.setupDragAndDrop();
        }, 100);
    });
} else {
    // DOM already ready
    setTimeout(() => {
        window.setupDragAndDrop();
    }, 100);
}

// Fallback initialization
window.addEventListener('load', () => {
    if (!window.dragDropInitialized) {
        setTimeout(() => {
            window.setupDragAndDrop();
        }, 200);
    }
});