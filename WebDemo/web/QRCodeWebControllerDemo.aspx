<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QRCodeWebControllerDemo.aspx.cs" Inherits="QRCodeWebControllerDemo" %>
<%@ Register Assembly="BarcodeLib.Barcode" Namespace="BarcodeLib.Barcode.QRCode" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>QRCode Web Form Demo - by BarcodeLib.com</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <cc1:QRCodeWebForm ID="BarcodeWebForm1" Data="BarcodeLib.com" runat="server" ModuleSize=3 Width=300 Height=300 />
    </div>
    </form>
</body>
</html>
