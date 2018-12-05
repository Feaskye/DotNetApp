$(function () {

    // navbar notification popups
    $(".notification-dropdown").each(function (index, el) {
        var $el = $(el);
        var $dialog = $el.find(".pop-dialog");
        var $trigger = $el.find(".trigger");

        $dialog.click(function (e) {
            e.stopPropagation()
        });
        $dialog.find(".close-icon").click(function (e) {
            e.preventDefault();
            $dialog.removeClass("is-visible");
            $trigger.removeClass("active");
        });
        $("body").click(function () {
            $dialog.removeClass("is-visible");
            $trigger.removeClass("active");
        });

        $trigger.click(function (e) {
            e.preventDefault();
            e.stopPropagation();

            // hide all other pop-dialogs
            $(".notification-dropdown .pop-dialog").removeClass("is-visible");
            $(".notification-dropdown .trigger").removeClass("active")

            $dialog.toggleClass("is-visible");
            if ($dialog.hasClass("is-visible")) {
                $(this).addClass("active");
            } else {
                $(this).removeClass("active");
            }
        });
    });


    // skin changer
    $(".skins-nav .skin").click(function (e) {
        e.preventDefault();
        if ($(this).hasClass("selected")) {
            return;
        }
        $(".skins-nav .skin").removeClass("selected");
        $(this).addClass("selected");

        if (!$("#skin-file").length) {
            $("head").append('<link rel="stylesheet" type="text/css" id="skin-file" href="">');
        }
        var $skin = $("#skin-file");
        if ($(this).attr("data-file")) {
            $skin.attr("href", $(this).data("file"));
        } else {
            $skin.attr("href", "");
        }

    });


    // sidebar menu dropdown toggle
    $("#dashboard-menu .dropdown-toggle").click(function (e) {
        e.preventDefault();
        var $item = $(this).parent();
        $item.toggleClass("active");
        if ($item.hasClass("active")) {
            $item.find(".submenu").slideDown("fast");
        } else {
            $item.find(".submenu").slideUp("fast");
        }
    });


    // mobile side-menu slide toggler
    var $menu = $("#sidebar-nav");
    $("body").click(function () {
        if ($(this).hasClass("menu")) {
            $(this).removeClass("menu");
        }
    });
    $menu.click(function (e) {
        e.stopPropagation();
    });
    $("#menu-toggler").click(function (e) {
        e.stopPropagation();
        $("body").toggleClass("menu");
    });
    $(window).resize(function () {
        $(this).width() > 769 && $("body.menu").removeClass("menu")
    })


    // build all tooltips from data-attributes
    $("[data-toggle='tooltip']").each(function (index, el) {
        $(el).tooltip({
            placement: $(this).data("placement") || 'top'
        });
    });


    // custom uiDropdown element, example can be seen in user-list.html on the 'Filter users' button
    var uiDropdown = new function () {
        var self;
        self = this;
        this.hideDialog = function ($el) {
            return $el.find(".dialog").hide().removeClass("is-visible");
        };
        this.showDialog = function ($el) {
            return $el.find(".dialog").show().addClass("is-visible");
        };
        return this.initialize = function () {
            $("html").click(function () {
                $(".ui-dropdown .head").removeClass("active");
                return self.hideDialog($(".ui-dropdown"));
            });
            $(".ui-dropdown .body").click(function (e) {
                return e.stopPropagation();
            });
            return $(".ui-dropdown").each(function (index, el) {
                return $(el).click(function (e) {
                    e.stopPropagation();
                    $(el).find(".head").toggleClass("active");
                    if ($(el).find(".head").hasClass("active")) {
                        return self.showDialog($(el));
                    } else {
                        return self.hideDialog($(el));
                    }
                });
            });
        };
    };

    // instantiate new uiDropdown from above to build the plugins
    new uiDropdown();


    // toggle all checkboxes from a table when header checkbox is clicked
    $(".table th input:checkbox").click(function () {
        $checks = $(this).closest(".table").find("tbody input:checkbox");
        if ($(this).is(":checked")) {
            $checks.prop("checked", true);
        } else {
            $checks.prop("checked", false);
        }
    });

    // quirk to fix dark skin sidebar menu because of B3 border-box
    if ($("#sidebar-nav").height() > $(".content").height()) {
        $("html").addClass("small");
    }

    var setulTag = function (s) {
        var kt = $("ul#dashboard-menu >li.active");
        if (kt.length > 0) {
            var arrowele = kt.find("div.pointer");
            if (arrowele.length > 0) {
                arrowele.addClass("display-close");
            }
        }
    };

    $("ul#dashboard-menu >li >a.dropdown-toggle").on({
        click: function () {
            var arrowleft = "icon-chevron-left", arrowdown = "icon-chevron-down";
            var tq = $(this).find("i:last");
            if (tq.hasClass(arrowdown)) {
                tq.removeClass(arrowdown).addClass(arrowleft);
            }
            else if (tq.hasClass(arrowleft)) {
                tq.removeClass(arrowleft).addClass(arrowdown);
            }
        }
    });

    $("ul.submenu >li,a.messageTag").on({
        click: function () {
            setulTag();
            var st = $(this).parents("li"), qlink = st.find("a.dashboard-menu-link");
            st.find("div.pointer").removeClass("display-close");
            if (!st.hasClass("active")) {
                st.addClass("active");
            }            
            if (qlink && !qlink.hasClass("side-cols-active")) {
                qlink.addClass("side-cols-active");
            }
        }
    });
});

jQuery.fn.extend({
    uploadPreview: function (opts) {
        var _self = this,
            _this = $(this);
        opts = jQuery.extend({
            Img: "picPreview",
            Width: 100,
            Height: 100,
            ImgType: ["gif", "jpeg", "jpg", "bmp", "png"],
            Callback: function () { }
        }, opts || {});
        _self.getObjectURL = function (file) {
            var url = null;
            if (window.createObjectURL != undefined) {
                url = window.createObjectURL(file)
            } else if (window.URL != undefined) {
                url = window.URL.createObjectURL(file)
            } else if (window.webkitURL != undefined) {
                url = window.webkitURL.createObjectURL(file)
            }
            return url
        };
        _this.change(function () {
            if (this.value) {
                if (!RegExp("\.(" + opts.ImgType.join("|") + ")$", "i").test(this.value.toLowerCase())) {
                    alert("选择文件错误,图片类型必须是" + opts.ImgType.join("，") + "中的一种");
                    this.value = "";
                    return false
                }
                if ($.browser.msie) {
                    try {
                        $("#" + opts.Img).attr('src', _self.getObjectURL(this.files[0]))
                    } catch (e) {
                        var src = "";
                        var obj = $("#" + opts.Img);
                        var div = obj.parent("div")[0];
                        _self.select();
                        if (top != self) {
                            window.parent.document.body.focus()
                        } else {
                            _self.blur()
                        }
                        src = document.selection.createRange().text;
                        document.selection.empty();
                        obj.hide();
                        obj.parent("div").css({
                            'filter': 'progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale)',
                            'width': opts.Width + 'px',
                            'height': opts.Height + 'px'
                        });
                        div.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = src
                    }
                } else {
                    $("#" + opts.Img).attr('src', _self.getObjectURL(this.files[0]))
                }
                opts.Callback()
            }
        })
    }
});