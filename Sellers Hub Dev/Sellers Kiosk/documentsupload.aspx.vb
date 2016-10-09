'**************************************************************
'Programmer Name		: Nestor S. Garais Jr
'Date Created			: 2014-01-20
'Finished Date          : 2014-01-21
'Program Name           : upload new documents
'Program Description    : 
'Stored Procedure       : 
'Remarks			    : DEV - 2014-01-20  | PROD - 2014-02-19
'Updates Information

'						* Malvin Reyes | 2014-03-04 | JO# JYYXXXXX
'						  DEV - 2014-03-04 | PROD 2014-XX-XX
'							- Implement the SSO process.
'                           - Include the "Inherits AuthenticatedPageBase" 
'                           - Include the "SessionAPI.RefreshSessions(CurrentUser)"
'                           - Remove the Session("sesSelVerified") checking

'						* Nestor Garais Jr | 2014-03-05 | JO# JYYXXXXX
'						  DEV - 2014-03-05 | PROD  
'							- When user selected a account/unit... display buyer's name info
'                           - Hide company code column

'						* Malvin Reyes | 2014-04-28 | JO# JYYXXXXX
'						  DEV - 2014-04-28 | PROD 2014-XX-XX
'							- Change the Title for Google Analytics

'						* Nestor Garais Jr | 2014-05-08 | JO# JYYXXXXX
'						  DEV - 2014-05-08 | PROD 2014-XX-XX
'							- Replace EO Ajaxfileuploader to user control image upload( ucImageUpload ), due to error on loading when using Firefox browser

'						* Nestor Garais Jr | 2014-06-25 | JO# JYYXXXXX
'						  DEV - 2014-06-25 | PROD 2014-XX-XX
'							-  fixed scroll focus 

'						* Nestor Garais Jr | 2015-08-04  | JO# JYYXXXXX
'						  DEV - 2015-08-04 | PROD  
'							-  transfer loading of document type list upon reservation number is selected 
'                           - add function DataTableWithNewRow to insert new row for downdown list source

'						* Nestor Garais Jr | 2016-02-05  | JO# JYYXXXXX
'						  DEV - 2016-02-05  PROD  
'							  - Use jquery ajax method for getting data from database
'                             - Remove server-side events
'                             - Use custom jquery fileupload
'                             - remove Ajax updatepanel  

'						* Nestor Garais Jr | 2016-02-09  | JO# JYYXXXXX
'						  DEV - 2016-02-09  PROD  
'                             - Rename error message when converting image to filestream

'						* Nestor Garais Jr | 2016-05-13  | JO# JYYXXXXX
'						  DEV - 2016-05-13 PROD  2016-05-13
'                             -Fix: add validation for capturing blank username when session expired

'						* Nestor Garais Jr | 2016-06-28 | JO# JYYXXXXX
'						  DEV -  2016-06-28 | PROD  
'                         Fix warnings :
'                           CA2000 : Microsoft.Reliability : In method 'documentsupload.GetDataFromDB(Integer, String, String)', call System.IDisposable.Dispose on object '_dimdtsResult' before all references to it are out of scope.
'                           CA2000 : Microsoft.Reliability : In method 'documentsupload.SaveData(String)', call System.IDisposable.Dispose on object '_dimDtsData' before all references to it are out of scope.
'                           CA2000 : Microsoft.Reliability : In method 'documentsupload.SaveData(String)', call System.IDisposable.Dispose on object '_dimFileStream' before all references to it are out of scope.
'                           CA2000 : Microsoft.Reliability : In method 'documentsupload.SaveData(String)', call System.IDisposable.Dispose on object '_dimXmlReader' before all references to it are out of scope.
'                           CA2202 : Microsoft.Usage : Object '_dimFileStream' can be disposed more than once in method 'documentsupload.SaveData(String)'. To avoid generating a System.ObjectDisposedException you should not call Dispose more than one time on an object.: Lines: 294, 296
'                           CA2202 : Microsoft.Usage : Object '_dimsqlConnection' can be disposed more than once in method 'documentsupload.GetDataFromDB(Integer, String, String)'. To avoid generating a System.ObjectDisposedException you should not call Dispose more than one time on an object.: Lines: 209, 213
'                           CA2202 : Microsoft.Usage : Object '_dimsqlConnection' can be disposed more than once in method 'documentsupload.SaveData(String)'. To avoid generating a System.ObjectDisposedException you should not call Dispose more than one time on an object.: Lines: 323
'                                   Solution : Add try/catch and dispose all disposable objects in try/finally
'***********************************************************************

Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Web.Script.Serialization
Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.IO
Imports FliAuthLib

Partial Class documentsupload
    'Inherits System.Web.UI.Page
    Inherits AuthenticatedPageBase

#Region "     Declaring Variables     "
    ' Your constant variables here 

    Private Enum TransMode
        Mod01SelectExistingRemittedContract = 1
        Mod02SelectExistingDocumentofContract = 2
        Mod03DeleteDocument = 3
        Mod04InsertDocument = 4
        Mod05PushEMail = 5
    End Enum

    Public Class ErrorList
        Private _priStrNumber As String
        Private _pristrErrorMessage As String

        Public Property ErrorNumber() As String
            Get
                Return _priStrNumber
            End Get

            Set(ByVal value As String)
                _priStrNumber = value
            End Set
        End Property

        Public Property ErrorMessage() As String
            Get
                Return _pristrErrorMessage
            End Get

            Set(ByVal value As String)
                _pristrErrorMessage = value
            End Set
        End Property

        Sub New(ByVal _parintErrNum As Integer, _
                ByVal _parstrErrMsg As String)
            ErrorNumber = _parintErrNum
            ErrorMessage = _parstrErrMsg
        End Sub
    End Class
#End Region

#Region "     User Define Subs and Functions     "
#Region "   Procedures  "
#End Region

#Region "   Functions  "
    ' Place your Functions here

    'Partial Class GetData
    ' Inherits System.Web.UI.Page
    <WebMethod()> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function GetDataFromDB(ByVal Mode As Integer, _
                                         ByVal ComCode As String, _
                                         ByVal ConNum As String) As String
        GetDataFromDB = ""

        Dim _dimSerial As New JavaScriptSerializer
        Dim _dimstrJSONstring As New StringBuilder
        Dim _dimErr As New List(Of ErrorList)

        ' Validate User Session
        Dim _dimstrUserName As String
        _dimstrUserName = SessionAPI.Username

        ' Validate User Session
        If _dimstrUserName Is Nothing Or _dimstrUserName = "" Then
            _dimErr.Add(New ErrorList(101, "Your session has expired. Please log in again"))

            GetDataFromDB = _dimSerial.Serialize(_dimErr)
            Exit Function
        End If

        Dim _dimclsDocumentsUpload As clsDocumentsUpload = Nothing
        Dim _dimdtsResult As DataSet = Nothing

        Try
            _dimclsDocumentsUpload = New clsDocumentsUpload
            _dimdtsResult = New DataSet

            Using _dimsqlConnection As New SqlConnection(MyMSSQLServer2000ConnectionString)
                _dimsqlConnection.Open()

                _dimclsDocumentsUpload.SQLConnection = _dimsqlConnection

                If _dimclsDocumentsUpload.ExeCuteAko(clsDocumentsUpload.ExecuteCommand.ExecuteDataAdapter, _
                                                     _dimstrUserName, _
                                                     Mode, _
                                                     ComCode, _
                                                     ConNum) = False Then
                    _dimErr.Add(New ErrorList(1, _dimclsDocumentsUpload.SQLMessage))

                    GetDataFromDB = _dimSerial.Serialize(_dimErr)
                    Exit Function
                End If

                With _dimclsDocumentsUpload
                    .SQLDataAdapter.Fill(_dimdtsResult)

                    If .SQLDataAdapter.SelectCommand.Parameters.Item("@pintErrorNumber").Value <> 8888 And .SQLDataAdapter.SelectCommand.Parameters.Item("@pintErrorNumber").Value IsNot Nothing Then
                        _dimErr.Add(New ErrorList(.SQLDataAdapter.SelectCommand.Parameters.Item("@pintErrorNumber").Value, _
                                                  .SQLDataAdapter.SelectCommand.Parameters.Item("@pstrErrorMessage").Value.ToString))
                        GetDataFromDB = _dimSerial.Serialize(_dimErr)

                        Exit Function
                    End If
                End With

                _dimstrJSONstring.Append("{")

                For _tblIndex As Integer = 0 To _dimdtsResult.Tables.Count - 1
                    _dimstrJSONstring.Append("""" + _tblIndex.ToString + """:") 'eg. "1":
                    _dimstrJSONstring.Append(ConvertDataTabletoString(_dimdtsResult.Tables(_tblIndex)))

                    If _tblIndex <> _dimdtsResult.Tables.Count - 1 Then
                        _dimstrJSONstring.Append(",")
                    End If
                Next

                _dimstrJSONstring.Append("}")
            End Using
        Catch ex As Exception
            _dimErr.Add(New ErrorList(111, MyExceptionNotice))
            GetDataFromDB = _dimSerial.Serialize(_dimErr)

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
        Finally
            If _dimclsDocumentsUpload IsNot Nothing Then _dimclsDocumentsUpload.Dispose()
            If _dimdtsResult IsNot Nothing Then _dimdtsResult.Dispose()

            _dimSerial = Nothing
            _dimErr = Nothing
        End Try

        HttpContext.Current.Response.Clear()
        'HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*") 'for firefox bugs
        HttpContext.Current.Response.Write(_dimstrJSONstring.ToString)

        _dimstrJSONstring = Nothing

        HttpContext.Current.Response.Flush()
        HttpContext.Current.Response.End()
    End Function

    <WebMethod()> _
     <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function SaveData(ByVal Data As String) As String
        SaveData = ""

        Dim _dimSerial As New JavaScriptSerializer
        Dim _dimErr As New List(Of ErrorList)
        Dim _dimstrUserName As String

        _dimstrUserName = SessionAPI.Username

        ' Validate User Session
        If _dimstrUserName Is Nothing Or _dimstrUserName = "" Then
            _dimErr.Add(New ErrorList(101, "Your session has expired. Please log in again"))
            Return _dimSerial.Serialize(_dimErr)
        End If

        ' Convert xml arguments to dataset
        Dim _dimXmlReader As StringReader = Nothing
        Dim _dimdtsResult As DataSet = Nothing
        Dim _dimclsDocumentsUpload As clsDocumentsUpload = Nothing
        Dim _dimstrTempPath As String

        _dimstrTempPath = HttpContext.Current.Server.MapPath("./TempUploadFiles/")

        Try
            _dimclsDocumentsUpload = New clsDocumentsUpload

            _dimXmlReader = New StringReader(Data)
            _dimdtsResult = New DataSet
            _dimdtsResult.ReadXml(_dimXmlReader, XmlReadMode.Auto)

            Using _dimsqlConnection As New SqlConnection(MyMSSQLServer2000ConnectionString)
                _dimsqlConnection.Open()
                _dimclsDocumentsUpload.SQLConnection = _dimsqlConnection

                ' Save Documents and its images
                If _dimdtsResult.Tables.Contains("DocInfoImages") = True Then
                    ' Apply transaction
                    _dimclsDocumentsUpload.SQLTransaction = _dimsqlConnection.BeginTransaction

                    ' 1 Documents has multiple images  
                    Dim _dimByte As Byte()
                    Dim _dimreader As BinaryReader = Nothing

                    For Each _dtrDataDocsImages As DataRow In _dimdtsResult.Tables("DocInfoImages").Rows
                        '2016-02-09 : set to general error message
                        Try
                            Using _priFileStream As New FileStream(_dimstrTempPath + _dtrDataDocsImages.Item("savedname"), FileMode.Open)
                                _dimreader = New BinaryReader(_priFileStream)
                                _dimByte = _dimreader.ReadBytes(_priFileStream.Length)

                                If _dimclsDocumentsUpload.ExeCuteAko(clsDocumentsUpload.ExecuteCommand.ExecuteNonQuery, _
                                                                     _dimstrUserName, _
                                                                     TransMode.Mod04InsertDocument, _
                                                                     _dtrDataDocsImages.Item("CompanyCode"), _
                                                                     _dtrDataDocsImages.Item("ReservationNo"), _
                                                                     _dtrDataDocsImages.Item("DocumentTypeCode"), _
                                                                     _dimByte, _
                                                                     _dtrDataDocsImages.Item("title")) = False Then
                                    _dimclsDocumentsUpload.SQLTransaction.Rollback()

                                    _dimErr.Add(New ErrorList(1, _dimclsDocumentsUpload.SQLMessage))

                                    Return _dimSerial.Serialize(_dimErr)
                                End If
                            End Using
                        Catch ex As Exception
                            _dimclsDocumentsUpload.SQLTransaction.Rollback()

                            _dimErr.Add(New ErrorList(1, "Error while saving image : " & _dtrDataDocsImages.Item("title")))

                            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)

                            Return _dimSerial.Serialize(_dimErr)
                        Finally
                            _dimByte = Nothing
                        End Try
                    Next

                    _dimclsDocumentsUpload.SQLTransaction.Commit()

                    _dimErr.Add(New ErrorList(8888, "Documents successfully added"))

                    Try
                        ' Push email Status Notification (Skip if error occur)
                        _dimclsDocumentsUpload.ExeCuteAko(clsDocumentsUpload.ExecuteCommand.ExecuteNonQuery, _
                                                          _dimstrUserName, _
                                                          TransMode.Mod05PushEMail, _
                                                          _dimdtsResult.Tables("DocInfoImages").Rows(0).Item("CompanyCode"), _
                                                          _dimdtsResult.Tables("DocInfoImages").Rows(0).Item("ReservationNo"), _
                                                          _dimdtsResult.Tables("DocInfoImages").Rows(0).Item("DocumentTypeCode"))
                    Catch ex As Exception
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
                    End Try

                    Return _dimSerial.Serialize(_dimErr)
                End If
            End Using
        Catch ex As Exception
            If _dimclsDocumentsUpload.SQLTransaction IsNot Nothing Then
                _dimclsDocumentsUpload.SQLTransaction.Rollback()
            End If

            _dimErr.Add(New ErrorList(2, ex.Message))

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
            Return _dimSerial.Serialize(_dimErr)
        Finally
            If _dimclsDocumentsUpload IsNot Nothing Then _dimclsDocumentsUpload.Dispose()
            If _dimXmlReader IsNot Nothing Then _dimXmlReader.Dispose()
            If _dimdtsResult IsNot Nothing Then _dimdtsResult.Dispose()

            _dimSerial = Nothing
            _dimErr = Nothing
        End Try
    End Function

    ' This method is used to convert datatable to json string
    Public Shared Function ConvertDataTabletoString(ByVal _pardttInput As DataTable) As String
        Dim _dimSerializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim _dimlstRows As New List(Of Dictionary(Of String, Object))()
        Dim _dimdctRow As Dictionary(Of String, Object)

        For Each _dimdtrDataRow As DataRow In _pardttInput.Rows
            _dimdctRow = New Dictionary(Of String, Object)()

            For Each _dimdtcDataColumn As DataColumn In _pardttInput.Columns
                _dimdctRow.Add(_dimdtcDataColumn.ColumnName, _dimdtrDataRow(_dimdtcDataColumn))
            Next

            _dimlstRows.Add(_dimdctRow)
        Next

        Return _dimSerializer.Serialize(_dimlstRows)
    End Function


#End Region
#End Region

#Region "     Events of Object     "
    ' Group the events of every control

#Region "Form"
    ' Events of Main form
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Initialize error message box
        Page.Form.Attributes.Add("enctype", "multipart/form-data")

        Dim _dimclsGlobalFunctions As New clsGlobalFunctions

        SessionAPI.RefreshSessions(CurrentUser)

        If Not _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelRole")).Contains("|documentsupload.aspx|") Then
            Response.Redirect("errorPage.aspx")

            Exit Sub
        End If

        _dimclsGlobalFunctions = Nothing
    End Sub

    'Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
    '    SqlConnection.ClearAllPools()
    'End Sub 'Page_Unload
#End Region 'Form

#Region "Button"
    ' Events in all Buttons 
#End Region 'Button

#Region "Grid"
    ' Events in all Grid
#End Region 'Grid

#Region "Dropdown List"
    ' Events in all Dropdown List
#End Region 'Dropdown List

#Region "CheckBOx"
#End Region

    'etc...
#End Region
End Class
