<%@ Page Language="C#" %>
<%@ Import Namespace="BarcodeLib.Barcode.ASP.NET" %>
<%
    PDF417ASPNETResponse.drawBarcode(Request, Response);
%>

