Imports TextToWavLib
Imports System.Net
Imports System.Data.SqlClient
Imports NLog

Partial Class Secure_CollectInfo
    Inherits System.Web.UI.Page

    Protected Sub btnGetfile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetfile.Click
        Try
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("SpeechServices").ConnectionString)
                Dim cmd As New SqlCommand()
                ' make sure we put the connection back to its previous state
                cmd.Connection = connection
                connection.Open()
                cmd.CommandText = "FileContentInsert"
                cmd.CommandType = Data.CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text)
                cmd.Parameters.AddWithValue("@Content", txtContent.Text)
                cmd.Parameters.AddWithValue("@url", Convert.ToString(txtURL.Text))
                cmd.Parameters.AddWithValue("@File", "")
                cmd.Parameters.AddWithValue("@UserID", User.Identity.Name)
                cmd.Parameters.AddWithValue("@SpeechRate", drpRate.Value)
                cmd.Parameters.AddWithValue("@VoiceID", DropDownList1.SelectedValue)
                cmd.ExecuteNonQuery()
                connection.Close()
                connection.Dispose()
            End Using

            drpRate.SelectedIndex = 3
            txtContent.Text = ""
            txtURL.Text = ""
            txtTitle.Text = ""
            lblMessage.Text = "Request has been sucessfully submitted. The speech file should be avaiable within 5 Minutes under MyCollections."
        Catch ex As Exception
            Dim mylogger As Logger = LogManager.GetCurrentClassLogger()
            mylogger.Error(ex.Message + ex.StackTrace)
        End Try

    End Sub

    Protected Sub btnGO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Try
            If txtURL.Text <> "" Then
                If Not (txtURL.Text.StartsWith("http://") OrElse txtURL.Text.StartsWith("https://")) Then txtURL.Text = "http://" + txtURL.Text
                'Create an instance of HTMLToText
                Dim Converter As New HtmlToText
                Dim httpreq As HttpWebRequest = WebRequest.Create(New Uri(txtURL.Text))
                httpreq.KeepAlive = False
                httpreq.Method = "Post"
                httpreq.ContentType = "text/html"
                httpreq.ContentLength = 0

                Dim iostream As IO.Stream = httpreq.GetResponse().GetResponseStream()
                Dim reader As New IO.StreamReader(iostream)
                Dim str As String = reader.ReadToEnd()
                str = Converter.Convert(str)
                txtContent.Text = Server.HtmlEncode(str).Substring(0, 8000)
            End If
        Catch ex As Exception
            Dim mylogger As Logger = LogManager.GetCurrentClassLogger()
            mylogger.Error(ex.Message + ex.StackTrace)
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not User.Identity.IsAuthenticated Then
                Response.Redirect("http://2speech.us/Mobile/Login.aspx")
            End If


            If Not IsPostBack Then
                Dim ds As New Data.DataSet
                Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("SpeechServices").ConnectionString)
                    Dim cmd As New SqlCommand
                    cmd.CommandText = "GetVoices"
                    cmd.CommandType = Data.CommandType.StoredProcedure

                    Dim ada As New SqlDataAdapter(cmd)
                    cmd.Connection = connection
                    connection.Open()
                    ada.Fill(ds)
                    connection.Close()
                    connection.Dispose()

                End Using

                If ds IsNot Nothing AndAlso ds.Tables IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                    DropDownList1.DataSource = ds.Tables(0)
                    DropDownList1.DataValueField = ds.Tables(0).Columns(0).ColumnName
                    DropDownList1.DataTextField = ds.Tables(0).Columns(1).ColumnName
                    DropDownList1.DataBind()
                End If

            End If
        Catch ex As Exception
            Dim mylogger As Logger = LogManager.GetCurrentClassLogger()
            mylogger.Error(ex.Message + ex.StackTrace)
        End Try
    End Sub
End Class
