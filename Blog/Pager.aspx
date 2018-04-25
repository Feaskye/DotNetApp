<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pager.aspx.cs" Inherits="Pager" %>

<%--<%@ Register Assembly="Oran.WebControl.OranPager" Namespace="Oran.WebControl" TagPrefix="OranPager" %>--%>
<%--<%@ Register Assembly="OranPager" TagPrefix="OranPager" Namespace="Oran.WebControl" %>--%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
     .pager { border:solid 1px red; padding:5px;}
     .pager a { margin-right:5px;}
     .pager .num_btn { float:right;}
     .pager .oran_pg_cur { background:yellow;}
     .pager .goto_btn { background:blue;}
     .pager .goto_txt { background:green; margin-right:5px; width:30px;}
   </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1><%= Request.QueryString["page"] == null ? "1" : Request.QueryString["page"]%></h1>
    
    <!--*****************************************************-->
    <OranPager:OranPager runat="server" ID="oranPagerMain" 
     ShowGotoBox="true" 
     ShowPageIndexList="true" 
     PageSize="2"
     PagerTagName="span"
     PagerCssClass="num_btn"
     NumericButtonCount="10"
     CustomizeTextAlign="Right"
     CssClass="pager"
     PageListCssClass="page_list"
     GotoButtonText="跳转"
     GotoBoxButtonCssClass="goto_btn"
     GotoBoxTextBoxCssClass="goto_txt" EnableUrlRewrite="true" UrlRewritePattern="demo-{0}.shtml" />
    <!--*****************************************************-->
    <br /><br /><br /><br />
    </div>
    </form>
</body>
</html>
