<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="mobile_index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, minimum-scale=1, maximum-scale=1" />
    <title>手机站首页</title>
    <link href="../js/jqm/jquery.mobile-1.3.2.min.css" rel="stylesheet" />
    <script src="../js/jquery-1.9.1.min.js"></script>
    <script src="../js/jqm/jquery.mobile-1.3.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <!-- 页面开始 -->
    <div id="page1" data-role="page">
    <!-- 头部开始 -->
        <div data-role="header" data-theme="" >
            <h3>头部标题</h3>
        </div>
    <!-- 头部结束 -->
        <div data-role="content">
            中间内容
        </div>
    <!-- 底部开始 -->
        <div data-role="footer" data-theme="" >
            <h3>底部</h3>
        </div>
    <!-- 底部结束 -->
    </div>
    </form>
</body>
</html>
