<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="js_ueditor_index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>ueditor demo</title>
    <script src="ueditor.config.js"></script>
    <script src="ueditor.all.min.js"></script>
    <script src="../jquery-1.9.1.min.js"></script>
    <script src="../base64.js"></script>
</head>
<body>
    <form id="form" runat="server" onsubmit="return onformSubmit();" method="post" >
    <div>
        <asp:HiddenField ID="hideContent" runat="server" Value="" />
        <textarea name="txtContent" id="myEditor"></textarea>
        <input id="Submit1" type="submit" value="submit" />
    </div>
    </form>
     <script type="text/javascript">
         var ue = UE.getEditor('myEditor');
         var textarea = $("textarea[name='txtContent']");
         var hideContent=$("#<%=hideContent.ClientID%>");
         ue.ready(function () {
             ue.setContent(utf8to16(base64decode(hideContent.val())));
         });

         //保存客户端处理
         function getEditor() {
             var editorContent = ue.getContent();
             if (ue.hasContents()) {
                 ue.setContent('');
                 editorContent = base64encode(utf16to8(editorContent));
                 textarea.val(editorContent);
                 return true;
             }
             alert('内容不能为空');
             return false;
         }
          
         function onformSubmit()
         {
             var res = true;
             res = getEditor();
             return res;
         }
    </script>
</body>
</html>
