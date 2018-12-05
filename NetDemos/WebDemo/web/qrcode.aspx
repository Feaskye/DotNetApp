<%@ Page Language="C#" %>
<%@ Import Namespace="BarcodeLib.Barcode.ASP.NET" %>
<%
    QRCodeASPNETResponse.drawBarcode(Request, Response);
%>
