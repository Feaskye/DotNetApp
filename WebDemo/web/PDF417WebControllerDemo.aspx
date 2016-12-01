<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PDF417WebControllerDemo.aspx.cs" Inherits="PDF417WebControllerDemo" %>
<%@ Register Assembly="BarcodeLib.Barcode" Namespace="BarcodeLib.Barcode.PDF417" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PDF417 Web Form Demo - by BarcodeLib.com</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <cc1:PDF417WebForm ID="BarcodeWebForm1" Data="BarcodeLib.com" runat="server" Width=300 Height=300 />
    </div>
    </form>
</body>
</html>
