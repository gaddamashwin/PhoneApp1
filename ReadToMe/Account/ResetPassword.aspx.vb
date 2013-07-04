Imports System.Net.Mail
Imports System.Net

Partial Class Account_ResetPassword
    Inherits System.Web.UI.Page

    Protected Sub CreateUserButton_Click(sender As Object, e As EventArgs)
        Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim())
        If Captcha1.UserValidated Then
            Dim u = Membership.GetUser(Username.Text, False)
            Dim r = u.ResetPassword()
            SendEmail(Username.Text, u.Email, r)
            Response.Redirect("Login.aspx")
        Else
            ErrorMessage.Text = "Enter the characters as they are shown in the image."
        End If
    End Sub

    Private Sub SendEmail(username As String, email As String, password As String)
        Dim fromAddress = New MailAddress("2Speech.us@gmail.com", "2Speech.us")
        Dim toAddress = New MailAddress(email, username)
        Const fromPassword As String = "jagadeeshwar"
        Const subject As String = "Forgot Password Notification"
        Dim body As String = String.Format("Dear {0}, {1} Your password has been reset to: {2} {1} If you still have trouble logging into our site, please contact our Support Team. {1} We thank you for your support and look forward to serving you! {1} {1} 2Speech.us", username, Environment.NewLine, password)
        Dim smtp = New SmtpClient() With { _
                 .Host = "smtp.gmail.com", _
                .Port = 587, _
                .EnableSsl = True, _
                .DeliveryMethod = SmtpDeliveryMethod.Network, _
                .UseDefaultCredentials = False, _
                .Credentials = New NetworkCredential(fromAddress.Address, fromPassword) _
            }
            Using message = New MailMessage(fromAddress, toAddress) With { _
                .Subject = subject, _
                .Body = body _
            }
                smtp.Send(message)
            End Using
    End Sub
End Class
