<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>index测试</title>
    <script src="js/jquery-1.9.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        index 测试页面<asp:FileUpload ID="FileUpload1" runat="server" />
        &nbsp;<asp:Button ID="btnUpLoad" runat="server" Text="上传" OnClick="btnUpLoad_Click" />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </div>
     <div id="dvbox">
         <img src="http://static.oschina.net/uploads/space/2014/0516/012728_nAh8_1168184.jpg" onclick="imageShow(this,'123','http://static.oschina.net/uploads/space/2014/0516/012728_nAh8_1168184.jpg')" />
     </div>

    <div id="tong">
        <img src="" />
    </div>
        <div id="tip"></div> 
   <script src="js/layer/layer.min.js"></script>
    <script type="text/javascript">
        var userAgent = navigator.userAgent.toLowerCase();
        // Figure out what browser is being used 
        jQuery.browser = {
            version: (userAgent.match(/.+(?:rv|it|ra|ie)[\/: ]([\d.]+)/) || [])[1],
            safari: /webkit/.test(userAgent),
            opera: /opera/.test(userAgent),
            msie: /msie/.test(userAgent) && !/opera/.test(userAgent),
            mozilla: /mozilla/.test(userAgent) && !/(compatible|webkit)/.test(userAgent),
            webkit: /webkit/.test(userAgent)
        };
        var browserTip = "你的浏览器名称是：";
        if ($.browser.msie) { //IE浏览器
            browserTip += "IE";
        }
        if ($.browser.mozilla) { //火狐浏览器
            browserTip += "Mozilla Firefox";
        }
        if ($.browser.webkit)
        {
            browserTip += "Chrome";
        }
        browserTip += " 版本号是：" + $.browser.version; //获取版本号
        $("#tip").html(browserTip);




        function imageShow(o, title, imgPath) {
            $("#tong img").attr({ "src": imgPath, "title": title });
            var i = $.layer({
                type: 1,
                title: false,
                fix: false,
                offset: ['200px', ''],
                area: ['600px', '360px'],
                page: { dom: '#tong' }
            });
        }

        //var jsontext = '{"firstname":"Jesper","surname":"Aaberg","phone":["555-0100","555-0120"]}';
        //var contact = JSON.parse(jsontext);
        //document.write(contact.surname + ", " + contact.firstname);
        //$(".tContent").each(function (i) { });

        /*
        var contact = new Object();
        contact.firstname = "Jesper";
        contact.surname = "Aaberg";
        contact.phone = ["555-0100", "555-0120"];
        contact.toJSON = function (key)
        {
            var replacement = new Object();
            for (var val in this)
            {
                if (typeof (this[val]) === 'string')
                    replacement[val] = this[val].toUpperCase();
                else
                    replacement[val] = this[val]
            }
            return replacement;
        };
        var jsonText = JSON.stringify(contact);
        document.write(jsonText);
        */

        (function ($, window, document, undefined) {
            jQuery.myPlugin = {
                myInfo: function (options) {
                    var defaults = { name: "wangrongfa", sex: "男", tel: "15768117351" };
                    var settiings = $.extend(defaults, options);
                    $("#dvbox").html(settiings.name);
                }
            };

            function isNullOrEmpty(param) {
                if (param == null || typeof (param) == 'undefined' || param === '' || param === 'null') {
                    return true;
                }
                return false;
            }

            
        })(jQuery, window, document);

        $.myPlugin.myInfo({ name: "xiaohan", sex: "男", tel: "15768117351" });
    </script>
    </form>
</body>
</html>
