    < script >
    $(function () {
        var nav = $('#nav');
        $(window).scroll(function () {
            if ($(this).scrollTop() > 150) {
                nav.addClass("menuFixo");
            } else {
                nav.removeClass("menuFixo");
            }
        });
    });
	</script >