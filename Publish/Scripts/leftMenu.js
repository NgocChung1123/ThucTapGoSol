//menu action
$(document).ready(function () {

    $(".side-menu > li > div.label").click(function () {
        if ($(this).hasClass("active")) {
            $(this).siblings(":last").slideUp(300);
            $(this).removeClass("active");
        }
        else {
            $(".side-menu > li > div.label").removeClass("active");
            $(".side-menu > li > div.child-menu").slideUp(300);

            $(this).siblings(":last").slideDown(300);
            $(this).addClass("active");
        }
    });

    $("#btnOption").click(function () {
        var menu = $(".dropdown-menu");
        if (menu.is(":visible")) {
            menu.hide(300);
        }
        else {
            menu.show(300);
        }
        return false;
    });

    $('html').click(function () {
        var menu = $(".dropdown-menu");
        if (menu.is(":visible")) {
            menu.hide(300);
        }
    });

    $('.dropdown-menu').click(function (event) {
        event.stopPropagation();
    });

    var height = $(window).height();
    var leftHeight = $("#left").outerHeight();
    var rightHeight = $("#right").outerHeight();

    if (rightHeight < height - 163) {
        $("#left").height(height - 163);
    }
    else {
        if (leftHeight < rightHeight) {
            $("#left").height(rightHeight);
        }
    }

    var menu = $('#MenuID').val();
    $('#' + menu).children(":first").addClass('active');
    $('#' + menu + ' .child-menu').show();
});