<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Mobile/Site.Master" AutoEventWireup="false"
    CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <meta name='description' content='Convert Text to speech' />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div data-role="page" id="one">
        <div data-role="header">
		    <h1>2Speech.us</h1>
	    </div><!-- /header -->
        <div data-role="content" >	
            <h2>
                WELCOME to 2SPEECH.US
            </h2>
            <p>
                2SPEECH.US lets you utilize your valuable time during commute or free time. Lets say you have an article or paper that your were supposed to read before you go work. Most of us don't find
                 the time to get to it and the reading gets postponed forever. 2SPEECH has a solution for this.
            </p>
            <h2>
                How does it work?
            </h2>
            <p>
               Copy the article or upload the text file or copy the link of website that you have to read and submit it. That is it! You will have an audio file which you can download or directly play on your smartphone. 
            </p>

            <p><a href="Mobile.aspx">Back</a></p>
        </div>
    </div>
</asp:Content>