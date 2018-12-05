<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LinearWebControllerDemo.aspx.cs" Inherits="WebControllerDemo" %>
<%@ Register Assembly="BarcodeLib.Barcode" Namespace="BarcodeLib.Barcode.Linear" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>WebControllerDemo</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <cc1:LinearWebForm ID="BarcodeWebForm1" Type=CODE128 Data="1234567890" runat="server" />
    </div>
    </form>
</body>
</html>
