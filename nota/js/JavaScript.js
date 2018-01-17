< script >
    $(function () {
        var nav = $('#nav');
        $(window).scroll(function () {
            if ($(this).scrollTop() > 150) {
                nav.addClass("view");
            } else {
                nav.removeClass("view");
            }
        });
    });
	</script >
