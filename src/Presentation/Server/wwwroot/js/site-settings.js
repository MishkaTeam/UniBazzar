// Site Settings Custom JavaScript

// Global variables
let siteSettingsDataTable;

// Initialize DataTable
function initializeDataTable() {
    if ($('#siteSettingsTable').length) {
        siteSettingsDataTable = $('#siteSettingsTable').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.24/i18n/Persian.json"
            },
            "pageLength": 25,
            "order": [[0, "asc"]],
            "responsive": true,
            "columnDefs": [
                {
                    "targets": -1, // Last column (Actions)
                    "orderable": false,
                    "searchable": false
                }
            ]
        });
    }
}

// Show loading state
function showLoading(button) {
    const originalText = button.innerHTML;
    button.innerHTML = '<i class="fas fa-spinner fa-spin"></i> در حال پردازش...';
    button.disabled = true;
    return originalText;
}

// Hide loading state
function hideLoading(button, originalText) {
    button.innerHTML = originalText;
    button.disabled = false;
}

// Show success message
function showSuccessMessage(message) {
    Swal.fire({
        icon: 'success',
        title: 'موفقیت',
        text: message,
        confirmButtonText: 'باشه'
    });
}

// Show error message
function showErrorMessage(message) {
    Swal.fire({
        icon: 'error',
        title: 'خطا',
        text: message,
        confirmButtonText: 'باشه'
    });
}

// Show confirmation dialog
function showConfirmation(message, callback) {
    Swal.fire({
        title: 'تایید',
        text: message,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'بله',
        cancelButtonText: 'خیر'
    }).then((result) => {
        if (result.isConfirmed) {
            callback();
        }
    });
}

// Validate form
function validateForm(formId) {
    const form = document.getElementById(formId);
    if (!form.checkValidity()) {
        form.reportValidity();
        return false;
    }
    return true;
}

// Format phone number
function formatPhoneNumber(input) {
    let value = input.value.replace(/\D/g, '');
    if (value.length > 0) {
        if (value.startsWith('0')) {
            value = value.substring(1);
        }
        if (value.length > 0 && !value.startsWith('9')) {
            value = '9' + value;
        }
    }
    input.value = value;
}

// Preview logo image
function previewLogo(input) {
    const file = input.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function(e) {
            const preview = document.getElementById('logoPreview');
            if (preview) {
                preview.src = e.target.result;
                preview.style.display = 'block';
            }
        };
        reader.readAsDataURL(file);
    }
}

// Auto-save form data to localStorage
function autoSaveForm(formId, key) {
    const form = document.getElementById(formId);
    if (form) {
        const formData = new FormData(form);
        const data = {};
        for (let [key, value] of formData.entries()) {
            data[key] = value;
        }
        localStorage.setItem(key, JSON.stringify(data));
    }
}

// Load form data from localStorage
function loadFormData(formId, key) {
    const form = document.getElementById(formId);
    if (form) {
        const data = localStorage.getItem(key);
        if (data) {
            const formData = JSON.parse(data);
            for (let key in formData) {
                const input = form.querySelector(`[name="${key}"]`);
                if (input) {
                    input.value = formData[key];
                }
            }
        }
    }
}

// Clear form data from localStorage
function clearFormData(key) {
    localStorage.removeItem(key);
}

// Export table to Excel
function exportToExcel(tableId, filename = 'site-settings') {
    const table = document.getElementById(tableId);
    if (table) {
        const wb = XLSX.utils.table_to_book(table, { sheet: "Site Settings" });
        XLSX.writeFile(wb, `${filename}.xlsx`);
    }
}

// Export table to PDF
function exportToPDF(tableId, filename = 'site-settings') {
    const table = document.getElementById(tableId);
    if (table) {
        const opt = {
            margin: 1,
            filename: `${filename}.pdf`,
            image: { type: 'jpeg', quality: 0.98 },
            html2canvas: { scale: 2 },
            jsPDF: { unit: 'in', format: 'letter', orientation: 'portrait' }
        };
        html2pdf().set(opt).from(table).save();
    }
}

// Search functionality
function searchSiteSettings(query) {
    if (siteSettingsDataTable) {
        siteSettingsDataTable.search(query).draw();
    }
}

// Filter by price list
function filterByPriceList(priceListId) {
    if (siteSettingsDataTable) {
        if (priceListId) {
            siteSettingsDataTable.column(5).search(priceListId).draw();
        } else {
            siteSettingsDataTable.column(5).search('').draw();
        }
    }
}

// Initialize tooltips
function initializeTooltips() {
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
}

// Initialize popovers
function initializePopovers() {
    const popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
    popoverTriggerList.map(function (popoverTriggerEl) {
        return new bootstrap.Popover(popoverTriggerEl);
    });
}

// Document ready function
$(document).ready(function() {
    // Initialize DataTable
    initializeDataTable();
    
    // Initialize tooltips and popovers
    initializeTooltips();
    initializePopovers();
    
    // Phone number formatting
    $('input[name="phoneNumber"]').on('input', function() {
        formatPhoneNumber(this);
    });
    
    // Logo preview
    $('input[name="logoFile"]').on('change', function() {
        previewLogo(this);
    });
    
    // Auto-save form data
    $('form').on('input', function() {
        const formId = this.id;
        const key = `siteSettings_${formId}`;
        autoSaveForm(formId, key);
    });
    
    // Load saved form data on page load
    $('form').each(function() {
        const formId = this.id;
        const key = `siteSettings_${formId}`;
        loadFormData(formId, key);
    });
    
    // Clear saved data on successful form submission
    $('form').on('submit', function() {
        const formId = this.id;
        const key = `siteSettings_${formId}`;
        clearFormData(key);
    });
    
    // Export buttons
    $('#exportExcel').on('click', function() {
        exportToExcel('siteSettingsTable', 'site-settings');
    });
    
    $('#exportPDF').on('click', function() {
        exportToPDF('siteSettingsTable', 'site-settings');
    });
    
    // Search functionality
    $('#searchInput').on('keyup', function() {
        searchSiteSettings(this.value);
    });
    
    // Price list filter
    $('#priceListFilter').on('change', function() {
        filterByPriceList(this.value);
    });
});

// Global error handler
window.addEventListener('error', function(e) {
    console.error('Global error:', e.error);
    showErrorMessage('خطای غیرمنتظره رخ داده است. لطفاً صفحه را مجدداً بارگذاری کنید.');
});

// Unhandled promise rejection handler
window.addEventListener('unhandledrejection', function(e) {
    console.error('Unhandled promise rejection:', e.reason);
    showErrorMessage('خطا در پردازش درخواست. لطفاً مجدداً تلاش کنید.');
});
