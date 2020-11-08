jQuery(function ($) {

    $(".sidebar-dropdown > a").click(function () {
        $(".sidebar-submenu").slideUp(200);
        if (
            $(this)
                .parent()
                .hasClass("active")
        ) {
            $(".sidebar-dropdown").removeClass("active");
            $(this)
                .parent()
                .removeClass("active");
        } else {
            $(".sidebar-dropdown").removeClass("active");
            $(this)
                .next(".sidebar-submenu")
                .slideDown(200);
            $(this)
                .parent()
                .addClass("active");
        }
    });


    function screenClass() {
        if ($(window).innerWidth() > 960) {
            $(".page-wrapper").addClass("toggled");
        } else {
            $(".page-wrapper").removeClass("toggled");
        }
    }

    // Fire.
    screenClass();

    // And recheck when window gets resized.
    $(window).bind('resize', function () {
        screenClass();
    });
   
    $("#close-sidebar").click(function () {
        $(".page-wrapper").removeClass("toggled");
    });
    $("#show-sidebar").click(function () {
        $(".page-wrapper").addClass("toggled");
    });




});