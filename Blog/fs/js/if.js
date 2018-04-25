if (MARK == "") var MARK = "index";
$(function () {
    var cururl = location.href.toUpperCase().replace("PRODUCTS", "");
    var i = 0;
    var count = $("#con #tags li").length;
    $("#con #tags li").each(function (index) {
        var url = $(this).find("a").attr("href").toUpperCase().replace(".SHTML", "").replace(".HTML", "").replace("PRODUCTS", "");
        if (url.length > 5 && cururl.toUpperCase().indexOf(url.toUpperCase()) > -1) {
            $(this).find("a").addClass("hover").parent("li").siblings().find("a").removeClass("hover");
            return;
        }
        i++;
    });
    if (i == count) {
        $("#con #tags li:eq(0)").addClass("hover");
    }
});