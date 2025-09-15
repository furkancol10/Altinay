// Drag and Drop functionality for Kanban Board
window.setupDragAndDrop = () => {
    console.log('Setting up drag and drop...');

    // Make all issue cards draggable
    const issueCards = document.querySelectorAll('.issue-card');
    issueCards.forEach(card => {
        card.draggable = true;

        card.addEventListener('dragstart', (e) => {
            e.dataTransfer.effectAllowed = 'move';
            e.dataTransfer.setData('text/plain', '');

            // Add visual feedback
            card.style.opacity = '0.5';
            card.style.transform = 'rotate(5deg)';
            
            // Store the dragged issue ID
            const issueId = card.getAttribute('data-issue-id');
            if (issueId) {
                window.draggedIssueId = issueId;
                console.log('Dragging issue:', issueId);
            }
        });

        card.addEventListener('dragend', (e) => {
            // Remove visual feedback
            card.style.opacity = '1';
            card.style.transform = 'none';
        });
    });

    // Setup drop zones
    const dropZones = document.querySelectorAll('.column-content');
    dropZones.forEach(zone => {
        zone.addEventListener('dragover', (e) => {
            e.preventDefault();
            e.dataTransfer.dropEffect = 'move';
            zone.classList.add('drag-over');
        });

        zone.addEventListener('dragleave', (e) => {
            // Only remove class if we're actually leaving the zone
            if (!zone.contains(e.relatedTarget)) {
                zone.classList.remove('drag-over');
            }
        });

        zone.addEventListener('drop', (e) => {
            e.preventDefault();
            zone.classList.remove('drag-over');

            // Get the new status from the column
            const newStatus = getStatusFromColumn(zone);
            if (newStatus && window.draggedIssueId) {
                console.log('Dropping issue:', window.draggedIssueId, 'to status:', newStatus);
                
                // Call the Blazor component's method directly
                try {
                    // Find the Blazor component and call its method
                    const component = document.querySelector('[data-blazor-component]');
                    if (component && component._blazorComponent) {
                        component._blazorComponent.OnDropLocalInstance(newStatus);
                    } else {
                        // Fallback: try to call via DotNet
                        if (window.DotNet) {
                            window.DotNet.invokeMethodAsync('Altinay.Blazor', 'OnDropLocal', newStatus);
                        }
                    }
                } catch (err) {
                    console.error('Error calling OnDropLocal:', err);
                }
            }
        });
    });
};

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

// Re-setup when new cards are added
window.refreshDragAndDrop = () => {
    setTimeout(() => {
        window.setupDragAndDrop();
    }, 100);
};

window.setDraggedIssue = (issueId) => {
    window.draggedIssueId = issueId;
};