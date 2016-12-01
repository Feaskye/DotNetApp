<%@ Page Language="C#" %>
<%@ Import Namespace="BarcodeLib.Barcode.ASP.NET" %>
<%
    LinearASPNETResponse.drawBarcode(Request, Response);
%>
