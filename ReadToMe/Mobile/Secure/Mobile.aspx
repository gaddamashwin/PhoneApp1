<%@ Page Title="" Language="VB" MasterPageFile="~/Mobile/Site.master" AutoEventWireup="false" CodeFile="Mobile.aspx.vb" Inherits="Mobile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div data-role="page" id="one">

	<div data-role="header">
		<h1>2Speech.us</h1>
	</div><!-- /header -->

	<div data-role="content" >	
		<ul data-role="listview" data-inset="true" data-theme="c" data-dividertheme="b">
			<li data-role="list-divider">Menu</li>
			<li><a href="http://2speech.us/Mobile/Secure/Default.aspx">Home</a></li>
            <li id='active2'><a href='http://2speech.us/Mobile/Secure/CollectInfo.aspx'>Covert To Speech</a></li>
            <li id='active3'><a href='http://2speech.us/Mobile/Secure/MyCollection.aspx'>My Collection</a></li>
            <li id='Li1'><a href='http://2speech.us/Mobile/Secure/Logout.aspx'>Logout</a></li>
			<li><a href="http://2speech.us/Mobile/Secure/About.aspx">About</a></li>
		</ul>
	</div><!-- /content -->
</div>
</asp:Content>

