<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataMatrixWebStreamDemo.aspx.cs" Inherits="DataMatrixWebStreamDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ASP.NET Data Matrix Generator Demo</title>
<style>
.Tiny {
color:#000000;
font-size:9px;
text-align: right; 
}
td {
    color:#000000;
    font-family:Verdana,Arial,Helvetica,sans-serif;
    font-size:11px;
   padding-left:5px;
  padding-right:5px;  
}

tr.Odd {
    background-color: #E8E9EB;
}
tr.Even {
    background-color: #F3F4F6;
}
td.Category {
    background-color: #D8D9DB;
   text-align: left; 
}
</style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hfFirst" Value="true" runat="server" />
        <asp:HiddenField ID="hfUrl" runat="server" />
       
    <table align=center width="600px" border="0" cellspacing="0" cellpadding="1" bordercolor="Gray">
		<tr>
			<td>
				<table border='0' width='100%' cellspacing='0' cellpadding='0' class="dataTable">
				
                                <tr>
                                    <td colspan=100 align=center >
                                            <asp:Image ID="barcodeImage" runat="server" />
                                    </td> 
                                </tr>
                                <tr>
                                    <td class="Tiny Category" colspan=10>
                                    <b>Compulsory</b>
                                    </td>
                                </tr>
                               <tr class="Even">
                                   <td class="Tiny">
                                            Barcode Data
                                    </td>  
                                   <td>
                                       <asp:TextBox ID="tbData" runat="server" Text="DataMatrix"></asp:TextBox>
                                   </td> 
                               </tr> 
                              <tr class="Even">
                                   <td class="Tiny">
                                            Data Matrix Encoding
                                    </td>  
                                   <td>
                                       <asp:DropDownList ID="ddEncoding" runat="server">
                                       </asp:DropDownList>
                                   </td> 
                               </tr>  
                              <tr class="Even">
                                   <td class="Tiny">
                                            Data Matrix Format
                                    </td>  
                                   <td>
                                       <asp:DropDownList ID="ddFormat" runat="server">
                                       </asp:DropDownList>
                                   </td> 
                               </tr>   
                              <tr class="Even">
                                   <td class="Tiny">
                                            Process Tilde
                                    </td>  
                                   <td>
                                       <asp:DropDownList ID="ddProcessTilde" runat="server">
                                       </asp:DropDownList>
                                   </td> 
                               </tr>    
                              <tr>
                                    <td class="Tiny Category" colspan=10>
                                    <b>Size</b>
                                    </td>
                                </tr>
                                <tr class="Odd">
                                   <td class="Tiny">
                                            Unit of Measure
                                    </td>   
                                   <td>
                                       <asp:DropDownList ID="ctrUOM" runat="server">
                                       </asp:DropDownList>
                                   </td> 
                              </tr> 
                              <tr class="Even">
                                   <td class="Tiny">
                                            Bar cell module size
                                    </td>   
                                   <td>
                                       <asp:TextBox ID="tbModuleSize" runat="server" Text="5"></asp:TextBox>
                                      &nbsp;
                                   </td> 
                              </tr>
                              <tr class="Even">
                                   <td class="Tiny">
                                            Resolution
                                    </td>  
                                   <td>
                                    <asp:TextBox ID="tbResolution" runat="server" Text="96"></asp:TextBox>
                                    &nbsp;(DPI) 
                                   </td> 
                              </tr>
                              <tr class="Odd">
                                   <td class="Tiny">
                                            Left Margin
                                    </td>   
                                   <td>
                                    <asp:TextBox ID="tbLeftMargin" runat="server" Text="10"></asp:TextBox>
                                   &nbsp;
                                   </td> 
                              </tr>
                              <tr class="Odd">
                                   <td class="Tiny">
                                            Right Margin
                                    </td>   
                                   <td>
                                    <asp:TextBox ID="tbRightMargin" runat="server" Text="10"></asp:TextBox>
                                   &nbsp;
                                   </td> 
                              </tr>
                              <tr class="Even">
                                   <td class="Tiny">
                                           Top Margin
                                    </td>  
                                   <td>
                                    <asp:TextBox ID="tbTopMargin" runat="server" Text="10"></asp:TextBox>
                                   &nbsp;
                                   </td> 
                              </tr>
                              <tr class="Even">
                                   <td class="Tiny">
                                           Bottom Margin
                                    </td>  
                                   <td>
                                    <asp:TextBox ID="tbBottomMargin" runat="server" Text="10"></asp:TextBox>
                                   &nbsp;
                                   </td> 
                              </tr>
                             
                              <tr>
                                    <td class="Tiny Category" colspan=10>
                                    <b>Others</b>
                                    </td>
                                </tr>
                              <tr class="Odd">
                                   <td class="Tiny">
                                            Image Format
                                    </td>  
                                   <td>
                                       <asp:DropDownList ID="ddImageFormat" runat="server">
                                       </asp:DropDownList>
                                   </td> 
                              </tr>
                              <tr>
                                    <td colspan="2" align=center>
                                   <br />
                                   <br /> 
                                        <asp:Button ID="Button1" runat="server" Text="Generate Barcode" />
                                        
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <a href="demo.html">Back to Demo Main</a>
                                    </td>
                                    
                              </tr>
                        </table>
                       </td>
                     </tr>
                   </table>   
    </form>
</body>
</html>
