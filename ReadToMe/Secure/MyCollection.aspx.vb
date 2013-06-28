Imports System.Data
Imports System.Data.SqlClient
Imports NLog

Partial Class Secure_MyCollection
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not User.Identity.IsAuthenticated Then
                Response.Redirect("./../Account/Login.aspx")
            End If

            If Not IsPostBack Then
                bindGridView()
            End If
        Catch ex As Exception
            Dim mylogger As Logger = LogManager.GetCurrentClassLogger()
            mylogger.Error(ex.Message + ex.StackTrace)
        End Try
    End Sub

    Private Sub bindGridView()
        Dim cmd As New SqlCommand()
        Dim ada As New SqlClient.SqlDataAdapter
        Dim ds As New DataSet

        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("SpeechServices").ConnectionString)

            ' make sure we put the connection back to its previous state
            cmd.Connection = connection
            connection.Open()
            cmd.CommandText = "FileContentMyCollSelectAll"
            cmd.CommandType = Data.CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@UserID", User.Identity.Name)

            ada.SelectCommand = cmd
            ada.Fill(ds)
            connection.Close()
            cmd.Dispose()
        End Using

        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            GridView1.DataSource = ds.Tables(0)
            GridView1.DataBind()
        End If
    End Sub


    Protected Sub GridView1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            GridView1.PageIndex = e.NewPageIndex
            bindGridView()
        Catch ex As Exception
            Dim mylogger As Logger = LogManager.GetCurrentClassLogger()
            mylogger.Error(ex.Message + ex.StackTrace)
        End Try
    End Sub
End Class
