<%@ Page Title="Collect Information" Language="VB" MasterPageFile=".\..\Site.master" ValidateRequest="false" AutoEventWireup="false" CodeFile="CollectInfo.aspx.vb" Inherits="Secure_CollectInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
  <meta name='description' content='Collect the text for Speech' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
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
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="textEntry" MaxLength="100"></asp:TextBox> <a href="#" class="hintanchor" onmouseover="showhint('Title for the content.', this, event, '150px')">[?]</a>
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
                        </select><a href="#" class="hintanchor" onmouseover="showhint('Speech Rate defines the rate at which the speach is read.', this, event, '150px')">[?]</a>
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
						<a href="#" class="hintanchor" onmouseover="showhint('To get the content from an external website please enter the complete URL.', this, event, '150px')">[?]</a>
                    </td>
                    <td colspan="2">
                        <asp:Button ID="btnGo" CausesValidation="false" CssClass="button" runat="server" Text="Go" Width="65px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="From text file:" ></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="85%" /><a href="#" class="hintanchor" onmouseover="showhint('To get the content from an text file browse the file on the PC.', this, event, '150px')">[?]</a>
                    </td>
                    <td colspan="2">
                        <asp:Button ID="btnUplaod" CausesValidation="false" CssClass="button" runat="server" Text="Upload" Width="165px" />
                    </td>
                </tr>
                <tr>
                    <td colspan = "3">
                        <asp:textbox MaxLength="8000" style="resize:none;" Textmode="MultiLine"  id="txtContent" runat="server" width="93%" height="100px"></asp:textbox>
                        <a href="#" class="hintanchor" onmouseover="showhint('Here is the content that you would like to convert 2 speech.', this, event, '150px')">[?]</a>    
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
</asp:Content>

