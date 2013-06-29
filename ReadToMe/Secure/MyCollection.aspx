<%@ Page Title="My Collection" Language="VB" MasterPageFile=".\..\Site.master" AutoEventWireup="false" CodeFile="MyCollection.aspx.vb" Inherits="Secure_MyCollection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <meta name='description' content='My Collection Speech items' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>
        My Collection Items
    </h2>
    <p>
        Here are all the audio file for the content you requested. You can download or listen to them on you phone, MP3 player or any other device. The files can be downloaded on the PC by right clicking on the link in the grid and click 'Save target as ...'.
    </p>
    <div style="text-align:center">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="2px" CellPadding="10"  AllowPaging="true" PageSize="10"
        GridLines="Horizontal" Height="90px" Width="60%">
        <AlternatingRowStyle BackColor="#F7F7F7" />
        <Columns>
            <asp:BoundField DataField="ContentTitle" HeaderText="Title" />
            <asp:BoundField DataField="CreatedDatetime" HeaderText="Created On" />
            <asp:HyperLinkField DataTextField="Download" DataNavigateUrlFields="Filepath" HeaderText="Download link" />
        </Columns>
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <HeaderStyle BackColor="#f47c20" Font-Bold="True" ForeColor="#F7F7F7" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <RowStyle BackColor="#fcd3a5" ForeColor="#4A3C8C" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <SortedAscendingCellStyle BackColor="#F4F4FD" />
        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
        <SortedDescendingCellStyle BackColor="#D8D8F0" />
        <SortedDescendingHeaderStyle BackColor="#3E3277" />
    </asp:GridView>
    </div>
</asp:Content>

