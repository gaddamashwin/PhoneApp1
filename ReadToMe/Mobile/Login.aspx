<%@ Page Title="Log In" Language="VB" MasterPageFile="~/Mobile/Site.Master" AutoEventWireup="false"
    CodeFile="Login.aspx.vb" Inherits="Account_Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <meta name='description' content='Login or Signup' />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div data-role="page" id="one">
        <div data-role="header">
		    <h1>2Speech.us</h1>
	    </div><!-- /header -->
        <div data-role="content" >	
            <h2>
                Log In
            </h2>
            <p>
                Please enter your username and password.
                <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false">Register</asp:HyperLink> if you don't have an account.
            </p>
            <asp:Login ID="LoginUser" runat="server" EnableViewState="false"  RenderOuterTable="false">
                <LayoutTemplate>
                    <span class="failureNotification">
                        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="LoginUserValidationGroup"/>
                    <div>
                        <fieldset>
                            <h3>Account Information</h3>
                            <p>
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                                     CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                                     ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                                     CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                                     ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <!--<p>
                                <asp:CheckBox ID="RememberMe" runat="server"/>
                                <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Keep me logged in</asp:Label>
                            </p>-->
                        </fieldset>
                        <p>
                            <asp:Button ID="LoginButton" CssClass="button"  runat="server" CommandName="Login" Text="Log In" 
                                ValidationGroup="LoginUserValidationGroup" />
                        </p>
                    </div>
                </LayoutTemplate>
            </asp:Login>
    </div>
</div>
</asp:Content>