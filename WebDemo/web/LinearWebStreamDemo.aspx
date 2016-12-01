<%@ Page Language="C#" AutoEventWireup="true" 
    CodeFile="LinearWebStreamDemo.aspx.cs" Inherits="WebStreamDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ASP.NET Barcode Generator Linear Demo</title>
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
                                <tr class="Odd">
                                    <td class="Tiny">
                                            Symbology
                                    </td> 
                                    <td>
                                        <asp:DropDownList ID="ddType" runat="server">
                                        </asp:DropDownList>
                                    </td>  
                                </tr> 
                               <tr class="Even">
                                   <td class="Tiny">
                                            Barcode Data
                                    </td>  
                                   <td>
                                       <asp:TextBox ID="tbData" runat="server" Text="ABC123"></asp:TextBox>
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
                                            Bar Cell Width
                                    </td>   
                                   <td>
                                       <asp:TextBox ID="tbBarWidth" runat="server" Text="1"></asp:TextBox>
                                      &nbsp;
                                   </td> 
                              </tr> 
                              <tr class="Odd">
                                   <td class="Tiny">
                                            Bar Cell Height
                                    </td>    
                                   <td>
                                       <asp:TextBox ID="tbBarHeight" runat="server" Text="80"></asp:TextBox>
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
                              <tr class="Even">
                                   <td class="Tiny">
                                            BearerBars Style
                                    </td>  
                                   <td>
                                    <asp:DropDownList ID="ctrBearerBars" runat="server">
                                       </asp:DropDownList>
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
                              <tr class="Even">
                                   <td class="Tiny">
                                           Top Margin
                                    </td>  
                                   <td>
                                    <asp:TextBox ID="tbTopMargin" runat="server" Text="10"></asp:TextBox>
                                   &nbsp;
                                   </td> 
                              </tr>
                              <tr>
                                    <td class="Tiny Category" colspan=10>
                                    <b>Text Style</b>
                                    </td>
                                </tr>
                              <tr class="Even">
                                   <td class="Tiny">
                                            Show Text
                                    </td>   
                                   <td>
                                       <asp:DropDownList ID="ddShowText" runat="server">
                                       </asp:DropDownList>
                                   </td> 
                              </tr>
                              <tr class="Odd">
                                   <td class="Tiny">
                                           Text Font Name
                                    </td>    
                                   <td>
                                       <asp:DropDownList ID="ddTextFontName" runat="server">
                                       </asp:DropDownList>
                                   </td> 
                              </tr>
                              <tr class="Even">
                                   <td class="Tiny">
                                         Text Font Size
                                    </td>   
                                   <td>
                                       <asp:DropDownList ID="ddTextFontSize" runat="server">
                                       </asp:DropDownList>
                                   </td> 
                              </tr>
                              <tr class="Odd">
                                   <td class="Tiny">
                                         Text Font Style
                                    </td>    
                                   <td>
                                       <asp:CheckBoxList ID="cblTextFontStyle" runat="server">
                                       </asp:CheckBoxList>
                                   </td> 
                              </tr>
                              <tr>
                                    <td class="Tiny Category" colspan=10>
                                    <b>Others</b>
                                    </td>
                                </tr>
                              <tr class="Odd">
                                   <td class="Tiny">
                                          Add Checksum
                                    </td>   
                                   <td>
                                       <asp:DropDownList ID="ddAddCheckSum" runat="server">
                                       </asp:DropDownList>
                                   </td> 
                              </tr>
                              <tr class="Odd">
                                   <td class="Tiny">
                                            Barcode Supplement Data <br />(Only valid for EAN8, 13, UPCA, UPCE with suppliment)
                                    </td>   
                                   <td>
                                       <asp:TextBox ID="tbSData" runat="server" Text=""></asp:TextBox>
                                   </td> 
                               </tr>  
                              
                              <tr class="Even">
                                   <td class="Tiny">
                                            Rotate
                                    </td>   
                                   <td>
                                       <asp:DropDownList ID="ddRotate" runat="server">
                                       </asp:DropDownList>
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
                                    <td class="Tiny Category" colspan=10>
                                    <b>Codabar</b>
                                    </td>
                                </tr>
                              <tr class="Even">
                                   <td class="Tiny">
                                            Codabar Start Char
                                    </td>   
                                   <td>
                                       <asp:DropDownList ID="ddCodabarStartChar" runat="server">
                                       </asp:DropDownList>
                                   </td> 
                              </tr>
                              <tr class="Odd">
                                    <td class="Tiny">
                                           Codabar Stop Char
                                    </td>  
                                   <td>
                                       <asp:DropDownList ID="ddCodabarStopChar" runat="server">
                                       </asp:DropDownList>
                                   </td> 
                              </tr>
                               <tr>
                                    <td class="Tiny Category" colspan=10>
                                    <b>UPCE</b>
                                    </td>
                                </tr>
                              <tr class="Even">
                                   <td class="Tiny">
                                            UPCE Number System
                                    </td>   
                                   <td>
                                       <asp:DropDownList ID="ddUPCENumber" runat="server">
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
