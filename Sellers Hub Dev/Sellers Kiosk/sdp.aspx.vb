'**************************************************************
'Programmer Name		: Malvin V. Reyes
'Date Created			: 2010-09-03
'Finished Date          : 
'Program Name           : sdp
'Program Description    : Generate report for sdp
'Stored Procedure       : 
'Updates Information
'						* Malvin Reyes | 20xx-xx-xx | JO# J1Xxxxxx
'						  DEV - 20xx-xx-xx | PROD 20xx-XX-XX

'						* Nestor Garais Jr | 2016-06-28 | JO# JYYXXXXX
'						  DEV -  2016-06-28 | PROD  
'                         Fix warnings :
'                               CA2000 : Microsoft.Reliability : In method 'sdp.subGetData(Integer, String)', call System.IDisposable.Dispose on object '_priclsConnection' before all references to it are out of scope.
'                               CA2000 : Microsoft.Reliability : In method 'sdp.subGetData(Integer, String)', call System.IDisposable.Dispose on object '_pricmdSqlCommand' before all references to it are out of scope.
'                               CA2000 : Microsoft.Reliability : In method 'sdp.subGetData(Integer, String)', call System.IDisposable.Dispose on object '_pridtaSqlAdapter' before all references to it are out of scope.
'                               CA2000 : Microsoft.Reliability : In method 'sdp.subGetData(Integer, String)', call System.IDisposable.Dispose on object '_pridtsDataSet' before all references to it are out of scope.
'                               CA2000 : Microsoft.Reliability : In method 'sdp.subPopulateSDP(String, String, String)', call System.IDisposable.Dispose on object '_priclsConnection' before all references to it are out of scope.
'                               CA2000 : Microsoft.Reliability : In method 'sdp.subPopulateSDP(String, String, String)', call System.IDisposable.Dispose on object '_pricmdSqlCommand' before all references to it are out of scope.
'                               CA2000 : Microsoft.Reliability : In method 'sdp.subPopulateSDP(String, String, String)', call System.IDisposable.Dispose on object '_pridtaSqlAdapter' before all references to it are out of scope.
'                               CA2000 : Microsoft.Reliability : In method 'sdp.subPopulateSDP(String, String, String)', call System.IDisposable.Dispose on object '_pridtsDataSet' before all references to it are out of scope.
'                                   Solution : Add try/catch and dispose all disposable objects in try/finally
'**************************************************************

Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports FliAuthLib

Partial Class sdp
    'Inherits System.Web.UI.Page
    Inherits AuthenticatedPageBase

#Region "Declaring Variables"
    ' Your constant variables here
    Private _priclsConnection As clsConnection
    Private _pricmdSqlCommand As SqlCommand
    Private _pridtaSqlAdapter As SqlDataAdapter
    Private _pridtsDataSet As DataSet
#End Region

#Region "     User Define Subs and Functions     "
#Region "Procedures"
    ' Place your Sub here
    Private Sub subErrorMessagePrompt(ByVal _parbolIsVisible As Boolean, _
                                      Optional ByVal _parstrErrorMsg As String = "Internal Error Occured.", _
                                      Optional ByVal _parstrAlert As String = "error")
        If _parbolIsVisible Then
            divErrorMsgBox.Visible = True

            divErrorMsgBox.Attributes.Remove("class")
            divErrorMsgBox.Attributes.Add("class", "alert alert-" & _parstrAlert)

            divErrorMsg.InnerHtml = _parstrErrorMsg
        Else
            divErrorMsgBox.Visible = False
        End If
    End Sub

    Private Sub subPopulateSDP(ByVal _parstrProjectCode As String, _
                               ByVal _parstrPhaseBuildingCode As String, _
                               ByVal _parstrUserBy As String)
        Try
            _priclsConnection = New clsConnection
            _pricmdSqlCommand = New SqlCommand
            _pridtaSqlAdapter = New SqlDataAdapter
            _pridtsDataSet = New DataSet

            If _priclsConnection.OpenConnection(clsConnection.Database.MSSQLServer2000, MyMSSQLServer2000ConnectionString) = False Then
                subErrorMessagePrompt(True)

                Exit Sub
            End If

            With _pricmdSqlCommand
                .Connection = _priclsConnection.MSSQLServer2000Connection
                .CommandText = "SP_selrptSDP"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                With .Parameters
                    .AddWithValue("@pintMode", 3)
                    .AddWithValue("@pstrProjectCode", _parstrProjectCode)
                    .AddWithValue("@pstrPhaseBuildingCode", _parstrPhaseBuildingCode)
                    .AddWithValue("@pstrUsername", _parstrUserBy)

                    .Add("@pstrErrorMessage", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output
                End With

                _pridtaSqlAdapter.SelectCommand = _pricmdSqlCommand
                _pridtaSqlAdapter.Fill(_pridtsDataSet)

                If _pridtsDataSet.Tables(0).Rows.Count > 0 Then
                    Dim _dimstrSessionID As String = "sesrptReport"

                    Session.Add(_dimstrSessionID, _pridtsDataSet.Tables(0).Rows(0).Item(0))
                    divSDPReport.InnerHtml = "<iframe src='selPDFLoader.aspx?SessionID=" & _dimstrSessionID & _
                                             "&isByte=1#zoom=100' name=""sdp"" width=""100%"" height=""500"" align=""center"" border=""0"" frameborder=""1"" scrolling=""no"" </iframe>"
                Else
                    Session.Add("sesErrorMessage", "No record found.")
                    divSDPReport.InnerHtml = "<iframe src='selPDFLoader.aspx" & _
                                             "#zoom=100' name=""sdp"" width=""100%"" height=""150"" align=""center""  border=""0"" frameborder=""0""  scrolling=""no"" </iframe>"
                End If
            End With

            subErrorMessagePrompt(False)
        Catch ex As Exception
            subErrorMessagePrompt(True, MyExceptionNotice)

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
        Finally
            If _priclsConnection IsNot Nothing Then _priclsConnection.Dispose()
            If _pricmdSqlCommand IsNot Nothing Then _pricmdSqlCommand.Dispose()
            If _pridtaSqlAdapter IsNot Nothing Then _pridtaSqlAdapter.Dispose()
            If _pridtsDataSet IsNot Nothing Then _pridtsDataSet.Dispose()
        End Try
    End Sub

    Private Sub subGetData(ByVal _parintMode As Integer, ByVal _parstrUserBy As String)
        Try
            _priclsConnection = New clsConnection
            _pricmdSqlCommand = New SqlCommand
            _pridtaSqlAdapter = New SqlDataAdapter
            _pridtsDataSet = New DataSet

            Dim _dimstrProjectCode, _dimstrPhaseBuildingCode As String
            Dim _dimclsGlobalFunctions As New clsGlobalFunctions

            If ddlProjects.Items.Count > 0 Then
                _dimstrProjectCode = ddlProjects.SelectedValue
            Else
                _dimstrProjectCode = ""
            End If

            If ddlPhaseBuilding.Items.Count > 0 Then
                _dimstrPhaseBuildingCode = ddlPhaseBuilding.SelectedValue
            Else
                _dimstrPhaseBuildingCode = ""
            End If

            If _priclsConnection.OpenConnection(clsConnection.Database.MSSQLServer2000, MyMSSQLServer2000ConnectionString) = False Then
                subErrorMessagePrompt(True)

                Exit Sub
            End If

            With _pricmdSqlCommand
                .Connection = _priclsConnection.MSSQLServer2000Connection
                .CommandText = "SP_selrptSDP"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                With .Parameters
                    .AddWithValue("@pintMode", _parintMode)

                    .AddWithValue("@pstrProjectCode", _dimstrProjectCode)
                    .AddWithValue("@pstrPhaseBuildingCode", _dimstrPhaseBuildingCode)

                    .AddWithValue("@pstrUsername", _parstrUserBy)
                    .Add("@pstrErrorMessage", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output
                End With

                _pridtaSqlAdapter.SelectCommand = _pricmdSqlCommand

                If _parintMode = 1 Then
                    _pridtaSqlAdapter.Fill(_pridtsDataSet)

                    With ddlProjects
                        .DataTextField = "ProjectName"
                        .DataValueField = "ProjectCode"
                        .DataSource = _pridtsDataSet.Tables(0)
                        .DataBind()
                    End With
                ElseIf _parintMode = 2 Then     'Phase Building
                    _pridtaSqlAdapter.Fill(_pridtsDataSet)

                    With ddlPhaseBuilding
                        .DataTextField = "PhaseBuildingName"
                        .DataValueField = "PhaseBuildingCode"
                        .DataSource = _pridtsDataSet.Tables(0)
                        .DataBind()
                    End With
                End If
            End With

            subErrorMessagePrompt(False)
        Catch ex As Exception
            subErrorMessagePrompt(True, MyExceptionNotice)

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
        Finally
            If _priclsConnection IsNot Nothing Then _priclsConnection.Dispose()
            If _pricmdSqlCommand IsNot Nothing Then _pricmdSqlCommand.Dispose()
            If _pridtaSqlAdapter IsNot Nothing Then _pridtaSqlAdapter.Dispose()
            If _pridtsDataSet IsNot Nothing Then _pridtsDataSet.Dispose()
        End Try
    End Sub

    Private Sub subScroll()
        ScriptManager.RegisterStartupScript(updUpdatePanel, updUpdatePanel.GetType(), "meScroll", "var aTag = $(""a[name='aTagResult']""); $('html,body,window').animate({scrollTop: aTag.offset().top}, 1000);", True)
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
        Dim _dimclsGlobalFunctions As New clsGlobalFunctions

        SessionAPI.RefreshSessions(CurrentUser)

        If Not _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelRole")).Contains("|sdp.aspx|") Then
            _dimclsGlobalFunctions = Nothing
            Response.Redirect("errorPage.aspx")

            Exit Sub
        Else
            If Not Page.IsPostBack Then
                subGetData(1, _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelUser")))
            End If

            If ddlPhaseBuilding.SelectedValue = "" Then
                btnProceed.Enabled = False
            Else
                btnProceed.Enabled = True
            End If
        End If
        'End If

        _dimclsGlobalFunctions = Nothing
    End Sub
#End Region

#Region "Button"
    ' Events in all Buttons
    Protected Sub btnProceed_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProceed.Click
        Dim _dimclsGlobalFunctions As New clsGlobalFunctions

        divSDPReport.Visible = True

        subPopulateSDP(ddlProjects.SelectedValue, _
                       ddlPhaseBuilding.SelectedValue, _
                       _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelUser")))

        _dimclsGlobalFunctions = Nothing
        subScroll()
    End Sub
#End Region

#Region "Grid"
    ' Events in all Grid

#End Region

#Region "DropDownList"
    ' Events in all DropDownList
    Protected Sub ddlProjects_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProjects.SelectedIndexChanged
        divSDPReport.Visible = False
        divSDPReport.InnerHtml = ""

        Dim _dimclsGlobalFunctions As New clsGlobalFunctions

        subGetData(2, _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelUser")))

        _dimclsGlobalFunctions = Nothing
    End Sub

    Protected Sub ddlPhaseBuilding_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPhaseBuilding.SelectedIndexChanged
        divSDPReport.Visible = False
        divSDPReport.InnerHtml = ""

        If ddlPhaseBuilding.SelectedValue = "" Then
            btnProceed.Enabled = False
        Else
            btnProceed.Enabled = True
        End If
    End Sub
#End Region

    'etc...
#End Region
End Class
