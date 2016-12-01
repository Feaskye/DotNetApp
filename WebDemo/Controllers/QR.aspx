<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QR.aspx.cs" Inherits="WidgetS.QR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h4>二维码</h4>
        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Height="78px" Width="281px">奥特曼大小怪 bingo！</asp:TextBox>(不超过2K)
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="1.生成DR" />
        <br />
        <asp:Image ID="Image1" runat="server" />
        <br />
        <asp:Button ID="Button2" runat="server" Text="2.读取DR" onclick="Button2_Click" />
        <br />
        <asp:Literal ID="Literal1" runat="server"></asp:Literal><br />
        <h4>条形码</h4>
        编码：<asp:TextBox ID="barCode" runat="server" Width="500px">6923450657713</asp:TextBox><br />
        <img src="/QRimg/EAN13.jpg" style="float:right;" />
        <asp:Button ID="Button3" runat="server" Text="1.生成PDF 417 EAN-13" onclick="Button3_Click" />
        <br />
        <asp:Image ID="Image2" runat="server" /><br />

        <asp:Button ID="Button4" runat="server" Text="2.读取条形码" onclick="Button4_Click" />

        <br />
        <asp:Literal ID="Literal2" runat="server"></asp:Literal>

    </div>
    </form>
</body>
</html>
