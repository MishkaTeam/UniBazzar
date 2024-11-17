(function ($) {
    // بررسی و تنظیم جهت برای فیلد متنی
    function setDirectionBasedOnLanguage(element) {
        const text = $(element).val().trim();
        if (text.length > 0) {
            if (/^[\u0600-\u06FF]/.test(text)) {
                $(element).css('direction', 'rtl');
            } else {
                $(element).css('direction', 'ltr');
            }
        } else {
            $(element).css('direction', '');
        }
    }

    // نظارت بر تمام فیلدهای متنی
    function applyAutoDirection() {
        $('input[type="text"], textarea').each(function () {
            $(this).on('input', function () {
                setDirectionBasedOnLanguage(this);
            });
        });
    }

    // اجرای اسکریپت برای عناصر جدید (مثلاً با Ajax اضافه شده)
    $(document).ready(applyAutoDirection);
    $(document).on('DOMNodeInserted', applyAutoDirection);
})(jQuery);
