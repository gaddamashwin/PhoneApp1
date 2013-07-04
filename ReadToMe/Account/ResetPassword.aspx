<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="ResetPassword.aspx.vb" Inherits="Account_ResetPassword" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>
        Password Assistance
    </h2>
    <p>
        Enter the e-mail address associated with your 2Speech.us account, then click Continue. We'll email you a new password.
    </p>
     <span class="failureNotification">
        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
    </span>
    <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="RegisterUserValidationGroup"/>
    <table>
        <tr>
            <td><asp:Label ID="UsernameLabel" runat="server" AssociatedControlID="Username">Username:</asp:Label></td>
            <td><asp:TextBox ID="Username" runat="server" CssClass="textEntry"></asp:TextBox><a href="#" class="hintanchor" onmouseover="showhint('Please enter Username address associated with you account.', this, event, '150px')">[?]</a></td>
            <td><asp:RequiredFieldValidator ID="UsernameRequired" runat="server" ControlToValidate="Username" 
                    CssClass="failureNotification" ErrorMessage="Username is required." ToolTip="Username is required." 
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="Label1" runat="server" AssociatedControlID="Captcha1">Image:</asp:Label></td>
            <td>
                <cc1:CaptchaControl ID="Captcha1" runat="server"
                     CaptchaBackgroundNoise="Low" CaptchaLength="5"
                     CaptchaHeight="60" CaptchaWidth="200"
                     CaptchaLineNoise="None" CaptchaMinTimeout="5"
                     CaptchaMaxTimeout="240" FontColor = "#529E00" />
            </td>
        </tr>

        <tr>
            <td><asp:Label ID="lblCaptcha" runat="server" AssociatedControlID="txtCaptcha">Type characters:</asp:Label></td>
            <td>
                <asp:TextBox ID="txtCaptcha" runat="server" CssClass="textEntry"></asp:TextBox><a href="#" class="hintanchor" onmouseover="showhint('Please enter the characters as they are shown in the image.', this, event, '150px')">[?]</a>
            </td>
            <td><asp:RequiredFieldValidator ID="CaptchaRequiredField" runat="server" ControlToValidate="txtCaptcha" 
                    CssClass="failureNotification" ErrorMessage="Enter the characters as they are shown in the image." ToolTip="Enter the characters as they are shown in the image." 
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        
        <tr>
            <td colspan="3"><asp:Button ID="CreateUserButton" class="button" runat="server" Text="Continue" OnClick="CreateUserButton_Click" ValidationGroup="RegisterUserValidationGroup"/></td>
        </tr>
    </table>
</asp:Content>

