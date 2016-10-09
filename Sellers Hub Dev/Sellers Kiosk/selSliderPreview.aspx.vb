'						* Nestor Garais Jr | 2016-06-28 | JO# JYYXXXXX
'						  DEV -  2016-06-28 | PROD 
'                         Fix warnings :
'                               CA2000 : Microsoft.Reliability : In method 'selSliderPreview.Page_Load(Object, EventArgs)', call System.IDisposable.Dispose on object '_dimclsSlides' before all references to it are out of scope.	D:\PROJECTS PENDING\ORS\Sellers HUB-DEV\Sellers Kiosk\selSliderPreview.aspx.vb	23	D:\...\Sellers Kiosk\
'                                   Solution : Add try/catch and dispose all disposable objects in try/finally

Imports System.Data
Imports System.Data.SqlClient

Partial Class selSliderPreview
    Inherits System.Web.UI.Page

    Private _pridtCaptions As DataTable

    Private ReadOnly Property SessionId As String
        Get
            If Request.QueryString("spsid") Is Nothing Then
                Return ""
            End If

            Return Request.QueryString("spsid")
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            If SessionId = "" Then Exit Sub

            Dim _dimclsSlides As New clsSliderMaintenance

            Try
                _dimclsSlides.fnExecuteCmd(clsSliderMaintenance.ExecuteCommand.ExecuteDataAdapter, _
                                           3, _
                                           "", _
                                           _parstrSessionId:=SessionId)

                Using _dimdaData As SqlDataAdapter = _dimclsSlides.DataAdapter
                    _dimdaData.SelectCommand.Connection = New SqlConnection(MyMSSQLServer2000ConnectionString)

                    Using _dimdsSlideInfo As New DataSet
                        _dimdaData.Fill(_dimdsSlideInfo)

                        If _dimdsSlideInfo.Tables(0).Rows.Count = 0 Then
                            Exit Sub
                        End If

                        _pridtCaptions = _dimdsSlideInfo.Tables(1).Copy

                        rptSlides.DataSource = _dimdsSlideInfo.Tables(0)
                        rptSlides.DataBind()

                        rptSlidePage.DataSource = _dimdsSlideInfo.Tables(0)
                        rptSlidePage.DataBind()
                    End Using
                End Using

                pnlSession.Visible = False
            Catch ex As Exception
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
            Finally
                If _dimclsSlides IsNot Nothing Then _dimclsSlides.Dispose()
            End Try
        End If
    End Sub

    Protected Sub rptSlides_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptSlides.ItemDataBound
        Dim _dimrptCaption As Repeater = e.Item.FindControl("rptSlideCaption")

        _dimrptCaption.DataSource = _pridtCaptions
        _dimrptCaption.DataBind()
    End Sub
End Class
