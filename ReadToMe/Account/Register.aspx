<%@ Page Title="Register" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeFile="Register.aspx.vb" Inherits="Account_Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
<meta name='description' content='User registration page' />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:CreateUserWizard ID="RegisterUser" runat="server" EnableViewState="false">
        <LayoutTemplate>
            <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <WizardSteps>
            <asp:CreateUserWizardStep ID="RegisterUserWizardStep" runat="server">
                <ContentTemplate>
                    <h2>
                        Create a New Account
                    </h2>
                    <p>
                        Use the form below to create a new account.
                    </p>
                    <p>
                        Passwords are required to be a minimum of <%= Membership.MinRequiredPasswordLength %> characters in length.
                    </p>
                    <span class="failureNotification">
                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="RegisterUserValidationGroup"/>
                    <table>
                        <tr>
                            <td colspan="3">Account Information</td>
                            <tr>
                                <td>
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                </td>
                                <td>
                                <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox><a href="#" class="hintanchor" onmouseover="showhint('Please enter a username for your account login.', this, event, '150px')">[?]</a>
                                </td>
                                <td>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                                     CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td><asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label></td>
                                <td><asp:TextBox ID="Email" runat="server" CssClass="textEntry"></asp:TextBox><a href="#" class="hintanchor" onmouseover="showhint('Please enter a valid email address. This would be help to reset your password if you forget it.', this, event, '150px')">[?]</a></td>
                                <td><asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" 
                                     CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" 
                                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="Email" 
                                        CssClass="failureNotification" ErrorMessage="Invalid Email Format" ValidationGroup="RegisterUserValidationGroup"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td><asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label></td>
                                <td><asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox><a href="#" class="hintanchor" onmouseover="showhint('Please enter strong password.', this, event, '150px')">[?]</a></td>
                                <td><asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                                     CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td><asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label></td>
                                <td><asp:TextBox ID="ConfirmPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox><a href="#" class="hintanchor" onmouseover="showhint('Plese re-enter the password.', this, event, '150px')">[?]</a></td>
                                <td><asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" CssClass="failureNotification" Display="Dynamic" 
                                     ErrorMessage="Confirm Password is required." ID="ConfirmPasswordRequired" runat="server" 
                                     ToolTip="Confirm Password is required." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" 
                                     CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
                                     ValidationGroup="RegisterUserValidationGroup">*The Password and Confirmation Password must match.</asp:CompareValidator></td>
                            </tr>
                     
                        <tr>
                              <td colspan="3"><asp:Button ID="CreateUserButton"  class="button" runat="server" CommandName="MoveNext" Text="Create User" ValidationGroup="RegisterUserValidationGroup"/></td>
                        </tr>
                    </table>
                </ContentTemplate>
                <CustomNavigationTemplate>
                </CustomNavigationTemplate>
            </asp:CreateUserWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
</asp:Content>