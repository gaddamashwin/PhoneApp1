<%@ Page Title="Collect Information" Language="VB" MasterPageFile="~/Mobile/Site.master" ValidateRequest="false" AutoEventWireup="false" CodeFile="CollectInfo.aspx.vb" Inherits="Secure_CollectInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
  <meta name='description' content='Collect the text for Speech' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div data-role="page" id="one">
    <div data-role="header">
		<h1>2Speech.us</h1>
	</div><!-- /header -->
    <div data-role="content" >	
    <h2>
        Collect Speech Content
    </h2>
    <p>
        Please enter the content that you would like to convert to speech. The content can be obtained from a website or from a text file. 
    </p>
            <table class="accountInfo" style="width:100%">
               <tr>
                    <td colspan="4">
                        Speech Content
                    </td>
               </tr>
               <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Title:" ></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="textEntry" MaxLength="100"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        <asp:RequiredFieldValidator CssClass="failureNotification" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please provide the title for content." ToolTip="Please provide the title for content." ControlToValidate="txtTitle" >* Required</asp:RequiredFieldValidator>
                    </td>
               </tr>
               <tr>
                    <td style="width:15%">
                        <asp:Label ID="Label4" runat="server" Text="Speech Rate:" ></asp:Label>
                    </td>
                    <td style="width:30%">
                        <select id="drpRate" runat="server">
                             <option value="1">Very Slow</option>
                             <option value="2">Slow</option>
                             <option value="3" selected="selected">Medium</option>
                             <option value="4">Fast</option>
                             <option value="5">Very Fast</option>
                        </select>
                    </td>
                    <td style="width:35%"></td>
                    <td style="width:15%"></td>
                </tr>
                 <tr>
                    <td style="width:15%">
                        <asp:Label ID="Label5" runat="server" Text="Voice:" ></asp:Label>
                    </td>
                    <td style="width:25%">
                        <asp:DropDownList ID="DropDownList1" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td style="width:40%"></td>
                    <td style="width:20%"></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="From online:" ></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtURL" runat="server" CssClass="textEntry" MaxLength="250"></asp:TextBox> 
                    </td>
                    <td colspan="2">
                        <asp:Button ID="btnGo" CausesValidation="false" CssClass="button" runat="server" Text="Go" Width="65px" />
                    </td>
                </tr>
                <tr>
                    <td colspan = "3">
                        <asp:textbox MaxLength="8000" style="resize:none;" Textmode="MultiLine"  id="txtContent" runat="server" width="93%" height="100px"></asp:textbox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator CssClass="failureNotification" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please provide the content." ToolTip="Please provide the content." ControlToValidate="txtContent" >* Required</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                          <asp:button id="btnGetfile" runat="server" CssClass="button" Text="Submit"></asp:button>
                    </td>
               </tr>
                </table>
            <asp:label ID="lblMessage" runat="server" Width="100%" Text=""></asp:label>
        	<p><a href="http://2speech.us/Mobile/Secure/Mobile.aspx">Back</a></p>
        </div>
</div>
</asp:Content>

