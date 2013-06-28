<%@ Page language="c#" Codebehind="WebForm1.aspx.cs" AutoEventWireup="false" Inherits="TextToWavWeb.WebForm1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:textbox id="TextBox1" style="Z-INDEX: 101; LEFT: 200px; POSITION: absolute; TOP: 72px" runat="server"
				Width="400px" Height="176px" TextMode="MultiLine"></asp:textbox><asp:button id="Button1" style="Z-INDEX: 102; LEFT: 208px; POSITION: absolute; TOP: 272px" runat="server"
				Width="200px" Height="32px" Text="Enter Some Text And Click"></asp:button>
			<asp:PlaceHolder id="PlaceHolder1" runat="server"></asp:PlaceHolder></form>
	</body>
</HTML>
