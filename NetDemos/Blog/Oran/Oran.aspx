<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Oran.aspx.cs" Inherits="Oran_Oran" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ChinaRegion:ChinaRegion ID="ChinaRegion1" runat="server" />  <div id="dvChina" runat="server"></div>
        <IndexNavigator:IndexNavigator ID="IndexNavigator1" runat="server" FirstPage="首页" PreviousPage="上一页" NextPage="下一页" PageIndex="1" LastPage="尾页" PageSize="10" />
    </div>
    </form>
</body>
</html>
