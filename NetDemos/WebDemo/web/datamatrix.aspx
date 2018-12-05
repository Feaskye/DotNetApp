<%@ Page Language="C#" %>
<%@ Import Namespace="BarcodeLib.Barcode.ASP.NET" %>
<%
    DataMatrixASPNETResponse.drawBarcode(Request, Response);
%>
