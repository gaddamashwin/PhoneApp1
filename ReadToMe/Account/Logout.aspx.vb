
Partial Class Account_Logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If User.Identity.IsAuthenticated Then
            Session.Abandon()
            FormsAuthentication.SignOut()
            Response.Redirect("./../Default.aspx")
        End If
    End Sub
End Class
