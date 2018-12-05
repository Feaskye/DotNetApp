if (MARK == "") var MARK = "index";
$(function () {
    var cururl = location.href.toUpperCase();
    var i = 0;
    var count = $("#con #tags li").length;
    $("#con #tags li").each(function (index) {
        if ($(this).find("a").length > 0)
        {
            var url = $(this).find("a").attr("href").toUpperCase();
            if (url.length > 4 && cururl.toUpperCase().indexOf(url.toUpperCase()) > -1) {
                $(this).addClass("hover").siblings().removeClass("hover");
                return;
            }
        }
        i++;
    });
    if (i == count) {
        $("#con #tags li:eq(0)").addClass("hover");
    }
});