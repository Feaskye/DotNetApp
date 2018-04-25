<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery-1.9.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return SubmitEvent();" method="post">
    <div>
        <input id="key" name="search" type="text" value="" placeholder="请输入"  />
        <input id="Submit1" type="submit" value="submit" />
    </div>
    </form>
    <script type="text/javascript">
        // $("#key").val($("#key").val().replace(new RegExp(/</g), "$Start$").replace(new RegExp(/>/g), "$End$"));
        function SubmitEvent() {
            var key = $("#key").val();
            var reg = new RegExp("<(\s)*[^>]*?>");
            if (reg.test(key)) {
                $("#key").val(key.replace(new RegExp(/</g), "$Start$").replace(new RegExp(/>/g), "$End$").replace(/"|'/g, "\""));
            }
            return true;
        }
    </script>
</body>
</html>
