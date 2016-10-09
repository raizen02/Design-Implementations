'**************************************************************
'Programmer Name		: Malvin V. Reyes
'Date Created			: 2014-04-21
'Finished Date          : 
'Program Name           : Promo and Events
'Program Description    : 
'Stored Procedure       : 
'Updates Information
'						* Malvin Reyes | 2014-04-28 | JO# JYYXXXXX
'						  DEV - 2014-04-28 | PROD 2014-05-13
'							- flexible title.
'                           - New Title | Category Title

'						* Nestor Garais Jr | 2016-06-29 | JO# JYYXXXXX
'						  DEV -  2016-06-29 | PROD  
'                         Fix warnings :
'                           CA2000 : Microsoft.Reliability : In method 'whatsnew.Page_Load(Object, EventArgs)', call System.IDisposable.Dispose on object '_priclsDashboardContents' before all references to it are out of scope.
'                                   Solution : Add try/catch and dispose all disposable objects in try/finally
'**************************************************************
Imports System.Data
Imports System.Data.SqlClient
Imports FliAuthLib

Partial Class whatsnew
    Inherits AuthenticatedPageBase

#Region "Declaring Variables"
    ' Your constant variables here
    Private _priclsDashboardContents As clsDashboardContents
#End Region

#Region "     User Define Subs and Functions     "
#Region "Procedures"
    ' Place your Sub here
    Private Sub subFillContent(ByVal _parArtID As Integer)
        If ViewState("vwsdttArticleList").Select("ArtId = " & _parArtID).Length > 0 Then
            Me.Title = ViewState("vwsdttArticleList").Select("ArtId = " & _parArtID)(0).Item("Title").ToString & " | " & ViewState("vwsstrTitle")

            lblTitleAnouncement.Text = ViewState("vwsdttArticleList").Select("ArtId = " & _parArtID)(0).Item("FullTitle").ToString
            divArticleContent.InnerHtml = ViewState("vwsdttArticleList").Select("ArtId = " & _parArtID)(0).Item("Content").ToString
        Else
            Response.Redirect("errorPage.aspx?aspxerrortype=404")

            Exit Sub
        End If
    End Sub
#End Region

#Region "Functions"
    ' Place your Functions here

#End Region
#End Region

#Region "     Events of Object     "
    ' Group the events of every control

#Region "Form"
    ' Events of Main form
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SessionAPI.RefreshSessions(CurrentUser)

        If Not Page.IsPostBack Then
            Dim _dimclsGlobalFunctions As New clsGlobalFunctions

            Try
                _priclsDashboardContents = New clsDashboardContents

                If IsNumeric(Request.QueryString("newsid")) = False Then
                    ViewState("vwsstrTitle") = ""

                    Response.Redirect("errorPage.aspx?aspxerrortype=404")
                    Exit Sub
                End If

                _priclsDashboardContents.fnDashboardContents(clsDashboardContents.ExecuteCommand.ExecuteDataAdapter, _
                                                             2, _
                                                             _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelUser")), _
                                                             Request.QueryString("newsid"))

                Using _dimdaDataAdapter As SqlDataAdapter = _priclsDashboardContents.DataAdapter
                    _dimdaDataAdapter.SelectCommand.Connection = New SqlConnection(MyMSSQLServer2000ConnectionString)

                    Using _dimdsContents As New DataSet
                        _dimdaDataAdapter.Fill(_dimdsContents)

                        ViewState("vwsstrTitle") = _dimdsContents.Tables(0).Rows(0).Item("CategoryDescription").ToString
                        ViewState("vwsdttArticleList") = _dimdsContents.Tables(1)
                    End Using

                    If _dimdaDataAdapter.SelectCommand.Connection.State = ConnectionState.Open Then
                        _dimdaDataAdapter.SelectCommand.Connection.Close()
                    End If
                End Using

                _priclsDashboardContents = Nothing

                rprWhatsNew.DataSource = ViewState("vwsdttArticleList")
                rprWhatsNew.DataBind()

                Call subFillContent(CInt(Request.QueryString("newsid")))
            Catch ex As Exception
                Throw ex
            Finally
                If _priclsDashboardContents IsNot Nothing Then _priclsDashboardContents.Dispose()
            End Try
        End If
    End Sub
#End Region

#Region "Button"
    ' Events in all Buttons
    Protected Sub btnLinkWhatsNew_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim _dimLinkButton As LinkButton = DirectCast(sender, LinkButton)
        Dim _dimArticleID As Integer = Int32.Parse(_dimLinkButton.CommandArgument)

        Call subFillContent(_dimArticleID)
    End Sub
#End Region

#Region "Grid"
    ' Events in all Grid

#End Region

    'etc...
#End Region
End Class
