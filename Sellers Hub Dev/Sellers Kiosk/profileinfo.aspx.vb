'**************************************************************
'Programmer Name		: Malvin V. Reyes
'Date Created			: 2012-01-11
'Finished Date          : 
'Program Name           : Profile Info
'Program Description    : 
'Stored Procedure       : 
'Updates Information
'						* Nestor Garais Jr | 2012-05-17 | JO# JYYXXXXX
'						  DEV - 2012-05-17 | PROD 2012-XX-XX
'							- apply new CSS for contact information

'                       * Malvin V. Reyes | 2013-11-29 | JO# JYYXXXXX
'                         DEV 2013-11-29 | PROD 2014-02-19
'                           - Fixed the flexible layout.

'						* Malvin Reyes | 2014-03-04 | JO# JYYXXXXX
'						  DEV - 2014-03-04 | PROD 2014-05-13
'							- Implement the SSO process.
'                           - Include the "Inherits AuthenticatedPageBase" 
'                           - Include the "SessionAPI.RefreshSessions(CurrentUser)"
'                           - Remove the Session("sesSelVerified") checking

'						* Malvin Reyes | 2014-04-28 | JO# JYYXXXXX
'						  DEV - 2014-04-28 | PROD 2014-05-13
'							- Change the Title for Google Analytics
'
'                       * Nestor Garais Jr | 2016-06-28 | JO# JYYXXXXX
'						  DEV - 2016-06-28   PROD  
'                         Fix warnings :
'                           CA2000 : Microsoft.Reliability : In method 'profileinfo.subPopulateSellersProfile(String)', call System.IDisposable.Dispose on object '_priclsConnection' before all references to it are out of scope.
'                           CA2000 : Microsoft.Reliability : In method 'profileinfo.subPopulateSellersProfile(String)', call System.IDisposable.Dispose on object '_priclsProfileInfo' before all references to it are out of scope.
'                           CA2000 : Microsoft.Reliability : In method 'profileinfo.subPopulateSellersProfile(String)', call System.IDisposable.Dispose on object '_pridtsDataSet' before all references to it are out of scope.
'                                   Solution : Add try/catch and dispose all disposable objects in try/finally
'**************************************************************

Imports System.Data.SqlClient
Imports System.Data
Imports FliAuthLib

Partial Class profileinfo
    'Inherits System.Web.UI.Page
    Inherits AuthenticatedPageBase

#Region "Declaring Variables"
    ' Your constant variables here
    Private _priclsConnection As clsConnection
    Private _priclsProfileInfo As clsProfileInfo
    Private _pridtsDataSet As DataSet
#End Region

#Region "     User Define Subs and Functions     "
#Region "Procedures"
    ' Place your Sub here
    Private Sub subPopulateSellersProfile(ByVal _parstrUserName As String)
        _priclsConnection = New clsConnection
        _priclsProfileInfo = New clsProfileInfo
        _pridtsDataSet = New DataSet

        Dim _dimstrPostalCode As String

        Try
            If _priclsConnection.OpenConnection(clsConnection.Database.MSSQLServer2000, MyMSSQLServer2000ConnectionString) = False Then
                divErrorMsgBox.Visible = True
                divErrorMsg.InnerHtml = MyExceptionNotice

                Exit Sub
            End If

            With _priclsProfileInfo
                Dim _dimintMode As Integer = 2 'Sellers Profile

                .Connection = _priclsConnection.MSSQLServer2000Connection

                If .fnUsersProfile(clsProfileInfo.ExecuteCommand.ExecuteDataAdapter, _dimintMode, _parstrUserName) = False Then
                    divErrorMsgBox.Visible = True
                    divErrorMsg.InnerHtml = .ErrorMessage

                    Exit Sub
                Else
                    .DataAdapter.Fill(_pridtsDataSet)   'Sellers Profile

                    For Each _dimdtbDataRow As DataRow In _pridtsDataSet.Tables(0).Rows
                        spnFullName.InnerHtml = "<b>" & _dimdtbDataRow("custName").ToString & "</b>"
                        spnDateBirth.InnerHtml = "<b>" & Format(_dimdtbDataRow("custBDate").ToString, "Short Date") & "</b>"
                        spnGender.InnerHtml = "<b>" & _dimdtbDataRow("custGender").ToString & "</b>"
                        spnMaritalStatus.InnerHtml = "<b>" & _dimdtbDataRow("custMaritalStatus").ToString & "</b>"
                    Next

                    If _pridtsDataSet.Tables(2).Rows.Count > 0 Then
                        divContactInfo.InnerHtml = vbCrLf & "                        <h4>"
                        divContactInfo.InnerHtml += vbCrLf & "                            Contacts</h4>"
                    End If

                    For Each _dimdtbDataRow As DataRow In _pridtsDataSet.Tables(2).Rows
                        divContactInfo.InnerHtml += vbCrLf & "                        <div class=""control-group"">"
                        divContactInfo.InnerHtml += vbCrLf & "                            <label class=""control-label"">"
                        divContactInfo.InnerHtml += vbCrLf & "                                " & _dimdtbDataRow("custContactDesc").ToString & "</label>"
                        divContactInfo.InnerHtml += vbCrLf & "                            <div class=""controls"">"
                        divContactInfo.InnerHtml += vbCrLf & "                                <span class=""input-xxlarge uneditable-input"">"
                        divContactInfo.InnerHtml += vbCrLf & "       " & _dimdtbDataRow("custContactDetails").ToString & "</span>"
                        divContactInfo.InnerHtml += vbCrLf & "                            </div>"
                        divContactInfo.InnerHtml += vbCrLf & "                        </div>"
                    Next

                    If _pridtsDataSet.Tables(3).Rows.Count > 0 Then
                        divAddressInfo.InnerHtml = vbCrLf & "                        <h4>"
                        divAddressInfo.InnerHtml += vbCrLf & "                            Address</h4>"
                    End If

                    If _pridtsDataSet.Tables(3).Rows.Count > 0 Then
                        For Each _dimdtbDataRow As DataRow In _pridtsDataSet.Tables(3).Rows
                            If Not (_dimdtbDataRow("custPostalCode")) Is DBNull.Value Then
                                _dimstrPostalCode = _dimdtbDataRow("custPostalCode").ToString
                            Else
                                _dimstrPostalCode = ""
                            End If

                            divAddressInfo.InnerHtml += vbCrLf & "                        <div class=""control-group"">"
                            divAddressInfo.InnerHtml += vbCrLf & "                            <label class=""control-label"">"
                            divAddressInfo.InnerHtml += vbCrLf & "                                " & _dimdtbDataRow("custAddType").ToString & "</label>"
                            divAddressInfo.InnerHtml += vbCrLf & "                            <div class=""controls"">"
                            divAddressInfo.InnerHtml += vbCrLf & "                                <span class=""input-xxlarge uneditable-input"">"
                            divAddressInfo.InnerHtml += vbCrLf & "       " & _dimdtbDataRow("custAddress").ToString & "</span>"
                            divAddressInfo.InnerHtml += vbCrLf & "                            </div>"
                            divAddressInfo.InnerHtml += vbCrLf & "                        </div>"
                        Next
                    End If
                End If
            End With
        Catch ex As Exception
            divErrorMsgBox.Visible = True
            divErrorMsg.InnerHtml = MyExceptionNotice

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
        Finally
            If _priclsConnection IsNot Nothing Then _priclsConnection.Dispose()
            If _pridtsDataSet IsNot Nothing Then _pridtsDataSet.Dispose()
            If _priclsProfileInfo IsNot Nothing Then _priclsProfileInfo.Dispose()
        End Try
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

        subPopulateSellersProfile(_dimclsGlobalFunctions.fnDeCrypt(Session("sesSelUser")))

        _dimclsGlobalFunctions = Nothing
    End Sub
#End Region

#Region "Button"
    ' Events in all Buttons

#End Region

#Region "Grid"
    ' Events in all Grid

#End Region

    'etc...
#End Region

End Class
