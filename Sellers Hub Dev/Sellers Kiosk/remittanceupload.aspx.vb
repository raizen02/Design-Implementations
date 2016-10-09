'**************************************************************
'Programmer Name		: Nestor S. Garais Jr
'Date Created			: 2014-01-17
'Finished Date          : 
'Program Name           : remittanceupload
'Program Description    : upload new remittance
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
'						* Nestor Garais Jr | 2015-08-26  | JO# JYYXXXXX
'						  DEV - 2015-08-26  PROD 2014-XX-XX
'							-  change EO datetime picker to textbox with datepicker css
'						* Nestor Garais Jr | 2016-01-28  | JO# JYYXXXXX
'						  DEV - 2016-01-28  PROD  
'							  - Use jquery ajax method for getting data from database
'                             - Remove server-side events
'                             - Use custom jquery fileupload
'                             - remove Ajax updatepanel  
'                       * Nestor Garais Jr | 2016-02-09  | JO# JYYXXXXX
'						  DEV - 2016-02-09  PROD  
'                             - Rename error message when converting image to filestream
'						* Nestor Garais Jr | 2016-05-13  | JO# JYYXXXXX
'						  DEV - 2016-05-13 PROD  2016-05-13
'                             -Fix: add validation for capturing blank username when session expired

'                       * Nestor Garais Jr | 2016-06-28 | JO# JYYXXXXX
'						  DEV - 2016-06-28   PROD  
'                         Fix warnings :
'                           CA2000 : Microsoft.Reliability : In method 'remittanceupload.DeleteData(String)', call System.IDisposable.Dispose on object '_dimclsRemittanceUpload' before all references to it are out of scope.
'                           CA2202 : Microsoft.Usage : Object '_dimsqlConnection' can be disposed more than once in method 'remittanceupload.DeleteData(String)'. To avoid generating a System.ObjectDisposedException you should not call Dispose more than one time on an object.: Lines: 418
'                           CA2000 : Microsoft.Reliability : In method 'remittanceupload.GetDataFromDB(Integer, String, String)', call System.IDisposable.Dispose on object '_dimclsRemittanceUpload' before all references to it are out of scope.
'                           CA2000 : Microsoft.Reliability : In method 'remittanceupload.GetDataFromDB(Integer, String, String)', call System.IDisposable.Dispose on object '_dimdtsResult' before all references to it are out of scope.
'                           CA2202 : Microsoft.Usage : Object '_dimsqlConnection' can be disposed more than once in method 'remittanceupload.GetDataFromDB(Integer, String, String)'. To avoid generating a System.ObjectDisposedException you should not call Dispose more than one time on an object.: Lines: 212, 216
'                           CA2000 : Microsoft.Reliability : In method 'remittanceupload.SaveData(String)', call System.IDisposable.Dispose on object '_dimDtsData' before all references to it are out of scope.
'                           CA2000 : Microsoft.Reliability : In method 'remittanceupload.SaveData(String)', call System.IDisposable.Dispose on object '_dimFileStream' before all references to it are out of scope.
'                           CA2000 : Microsoft.Reliability : In method 'remittanceupload.SaveData(String)', call System.IDisposable.Dispose on object '_dimXmlReader' before all references to it are out of scope.
'                           CA2000 : Microsoft.Reliability : In method 'remittanceupload.SaveData(String)', call System.IDisposable.Dispose on object '_dimclsRemittanceUpload' before all references to it are out of scope.
'                           CA2202 : Microsoft.Usage : Object '_dimFileStream' can be disposed more than once in method 'remittanceupload.SaveData(String)'. To avoid generating a System.ObjectDisposedException you should not call Dispose more than one time on an object.: Lines: 332, 334
'                           CA2202 : Microsoft.Usage : Object '_dimsqlConnection' can be disposed more than once in method 'remittanceupload.SaveData(String)'. To avoid generating a System.ObjectDisposedException you should not call Dispose more than one time on an object.: Lines: 354
'                                   Solution : Add try/catch and dispose all disposable objects in try/finally
'***********************************************************************

Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports FliAuthLib
Imports System.Collections.Generic
Imports System.Web.Script.Serialization
Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.IO

Partial Class remittanceupload
    'Inherits System.Web.UI.Page
    Inherits AuthenticatedPageBase

#Region "     Declaring Variables     "
    ' Your constant variables here
    Private Enum TransMode
        Mod01SelectExistingRemittedContract = 1
        Mod02SelectExistingRemittanceofContract = 2
        Mod03DeleteRemittance = 3
        Mod04InsertRemittance = 4
        Mod05InsertRemitImages = 5
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

        Public Sub New(ByVal _ErrNum As Integer, ByVal _ErrMsg As String)
            ErrorNumber = _ErrNum
            ErrorMessage = _ErrMsg
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
        'HttpContext.Current.Response.Write("Test")
        GetDataFromDB = ""

        Dim _dimSerial As New JavaScriptSerializer
        'Dim _dimObjectToSerialize As Object = Nothing
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

        Dim _dimclsRemittanceUpload As clsRemittanceUpload = Nothing
        Dim _dimdtsResult As New DataSet

        Try
            _dimclsRemittanceUpload = New clsRemittanceUpload

            Using _dimsqlConnection As New SqlConnection(MyMSSQLServer2000ConnectionString)
                _dimsqlConnection.Open()
                _dimclsRemittanceUpload.SQLConnection = _dimsqlConnection

                If _dimclsRemittanceUpload.ExeCuteAko(clsRemittanceUpload.ExecuteCommand.ExecuteDataAdapter, _
                                                      _dimstrUserName, _
                                                      Mode, _
                                                      ComCode, _
                                                      ConNum, _
                                                      "RemittanceReferenceCode", _
                                                      "ModeofPayment", _
                                                      "CompanyName", _
                                                      "BankName", , , , _
                                                      "FileName") = False Then

                    _dimErr.Add(New ErrorList(1, _dimclsRemittanceUpload.SQLMessage))
                    GetDataFromDB = _dimSerial.Serialize(_dimErr)

                    Exit Function
                End If

                With _dimclsRemittanceUpload.SQLDataAdapter
                    .Fill(_dimdtsResult)

                    If .SelectCommand.Parameters.Item("@pintErrorNumber").Value <> 8888 And _
                       .SelectCommand.Parameters.Item("@pintErrorNumber").Value IsNot Nothing Then
                        _dimErr.Add(New ErrorList(.SelectCommand.Parameters.Item("@pintErrorNumber").Value, .SelectCommand.Parameters.Item("@pstrErrorMessage").Value.ToString))
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
                'Sample  : _dimstrJSONstring
                '{
                '	"Table0": [{
                '		"AllocationCode": "0000000081",
                '		"AllocationName": "FISG Inventory"
                '	}, {
                '		"AllocationCode": "0000",
                '		"AllocationName": "test2"
                '	}],
                '	"Table1": [{
                '		"col1": "0000",
                '		"Col2": "test3"
                '	}]
                '}
            End Using
        Catch ex As Exception
            _dimErr.Add(New ErrorList(111, MyExceptionNotice))
            GetDataFromDB = _dimSerial.Serialize(_dimErr)

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
            Exit Function
        Finally
            If _dimclsRemittanceUpload IsNot Nothing Then _dimclsRemittanceUpload.Dispose()
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
        Dim _dimSerial As New JavaScriptSerializer
        Dim _dimErr As New List(Of ErrorList)

        ' Validate User Session
        Dim _dimstrUserName As String

        _dimstrUserName = SessionAPI.Username

        ' Validate User Session
        If _dimstrUserName Is Nothing Or _dimstrUserName = "" Then
            _dimErr.Add(New ErrorList(101, "Your session has expired. Please log in again"))

            Return _dimSerial.Serialize(_dimErr)
        End If
         
        ' Convert xml arguments to dataset
        Dim _dimXmlReader As StringReader = Nothing
        Dim _dimDtsData As DataSet = Nothing
        Dim _dimclsRemittanceUpload As clsRemittanceUpload = Nothing
        Dim _dimstrTempPath As String

        _dimstrTempPath = HttpContext.Current.Server.MapPath("./TempUploadFiles/")

        Try
            _dimclsRemittanceUpload = New clsRemittanceUpload
            _dimXmlReader = New StringReader(Data)
            _dimDtsData = New DataSet
            _dimDtsData.ReadXml(_dimXmlReader, XmlReadMode.Auto)

            Using _dimsqlConnection As New SqlConnection(MyMSSQLServer2000ConnectionString)
                _dimsqlConnection.Open()

                _dimclsRemittanceUpload.SQLConnection = _dimsqlConnection

                ' Apply transaction
                _dimclsRemittanceUpload.SQLTransaction = _dimsqlConnection.BeginTransaction

                ' Save Remittance and its images
                Dim _dimstrRemittanceReferenceCode As String = ""

                If _dimDtsData.Tables.Contains("RemitInfo") = True Then
                    For Each _dtrDataRemit As DataRow In _dimDtsData.Tables("RemitInfo").Rows
                        _dimstrRemittanceReferenceCode = ""
                        ' Save Remittance and get generated code

                        ' Insert Remittance
                        If _dimclsRemittanceUpload.ExeCuteAko(clsRemittanceUpload.ExecuteCommand.ExecuteNonQuery, _
                                                              _dimstrUserName, _
                                                              TransMode.Mod04InsertRemittance, _
                                                              _dtrDataRemit.Item("CompanyCode"), _
                                                              _dtrDataRemit.Item("ContractNumber"), , _
                                                              _dtrDataRemit.Item("ModePaymentCode"), _
                                                              _dtrDataRemit.Item("RemitCompanyName"), _
                                                              _dtrDataRemit.Item("BankCode"), _
                                                              _dtrDataRemit.Item("DatePaid"), _
                                                              _dtrDataRemit.Item("RemitAmount")) = False Then
                            _dimclsRemittanceUpload.SQLTransaction.Rollback()
                            _dimErr.Add(New ErrorList(1, _dimclsRemittanceUpload.SQLMessage))

                            Return _dimSerial.Serialize(_dimErr)
                        End If

                        _dimstrRemittanceReferenceCode = _dimclsRemittanceUpload.RemittanceReferenceCode

                        ' Save Images for Remittance RemitImages
                        If _dimDtsData.Tables.Contains("RemitImages") = True Then
                            ' 1 remittance has multiple images 
                            Dim _dimByte As Byte()
                            Dim _dimreader As BinaryReader = Nothing

                            For Each _dtrDataRemitImages As DataRow In _dimDtsData.Tables("RemitImages").Rows
                                '2016-02-09 : set to general error message
                                Try
                                    Using _dimFileStream As New FileStream(_dimstrTempPath + _dtrDataRemitImages.Item("savedname"), FileMode.Open)
                                        _dimreader = New BinaryReader(_dimFileStream)
                                        _dimByte = _dimreader.ReadBytes(_dimFileStream.Length)

                                        If _dimclsRemittanceUpload.ExeCuteAko(clsRemittanceUpload.ExecuteCommand.ExecuteNonQuery, _
                                                                              _dimstrUserName, _
                                                                              TransMode.Mod05InsertRemitImages, _
                                                                              _dtrDataRemit.Item("CompanyCode"), _
                                                                              _dtrDataRemit.Item("ContractNumber"), _
                                                                              _dimstrRemittanceReferenceCode, , , , , , _
                                                                              _dimByte, _
                                                                              _dtrDataRemitImages.Item("title")) = False Then
                                            _dimclsRemittanceUpload.SQLTransaction.Rollback()
                                            _dimErr.Add(New ErrorList(1, _dimclsRemittanceUpload.SQLMessage))

                                            Return _dimSerial.Serialize(_dimErr)
                                        End If
                                    End Using
                                Catch ex As Exception
                                    _dimclsRemittanceUpload.SQLTransaction.Rollback()
                                    _dimErr.Add(New ErrorList(1, MyExceptionNotice))

                                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
                                    Return _dimSerial.Serialize(_dimErr)
                                Finally
                                    _dimByte = Nothing
                                End Try
                            Next
                        End If
                    Next
                End If

                _dimclsRemittanceUpload.SQLTransaction.Commit()

                _dimErr.Add(New ErrorList(8888, "Remittance successfully added"))

                Return _dimSerial.Serialize(_dimErr)
            End Using
        Catch ex As Exception
            If _dimclsRemittanceUpload.SQLTransaction IsNot Nothing Then
                _dimclsRemittanceUpload.SQLTransaction.Rollback()
            End If

            _dimErr.Add(New ErrorList(2, MyExceptionNotice))

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
            Return _dimSerial.Serialize(_dimErr)
        Finally
            If _dimclsRemittanceUpload IsNot Nothing Then _dimclsRemittanceUpload.Dispose()
            If _dimXmlReader IsNot Nothing Then _dimXmlReader.Dispose()
            If _dimDtsData IsNot Nothing Then _dimDtsData.Dispose()

            _dimSerial = Nothing
            _dimErr = Nothing
        End Try
    End Function

    <WebMethod()> _
     <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
     Public Shared Function DeleteData(ByVal RemitID As String) As String
        Dim _dimSerial As New JavaScriptSerializer
        Dim _dimErr As New List(Of ErrorList)

         ' Validate User Session
        Dim _dimstrUserName As String

        _dimstrUserName = SessionAPI.Username

        ' Validate User Session
        If _dimstrUserName Is Nothing Or _dimstrUserName = "" Then
            _dimErr.Add(New ErrorList(101, "Your session has expired. Please log in again"))

            Return _dimSerial.Serialize(_dimErr)
        End If

       Dim _dimclsRemittanceUpload As New clsRemittanceUpload

        Try
            Using _dimsqlConnection As New SqlConnection(MyMSSQLServer2000ConnectionString)
                _dimsqlConnection.Open()
                _dimclsRemittanceUpload.SQLConnection = _dimsqlConnection

                ' Apply transaction
                _dimclsRemittanceUpload.SQLTransaction = _dimsqlConnection.BeginTransaction

                ' Delete Remittance
                If _dimclsRemittanceUpload.ExeCuteAko(clsRemittanceUpload.ExecuteCommand.ExecuteNonQuery, _
                                                      _dimstrUserName, _
                                                      TransMode.Mod03DeleteRemittance, , , _
                                                      RemitID) = False Then
                    _dimclsRemittanceUpload.SQLTransaction.Rollback()
                    _dimErr.Add(New ErrorList(1, _dimclsRemittanceUpload.SQLMessage))

                    Return _dimSerial.Serialize(_dimErr)
                End If

                _dimclsRemittanceUpload.SQLTransaction.Commit()

                _dimErr.Add(New ErrorList(8888, "Remittance successfully deleted"))

                ' Output JSON format
                Return _dimSerial.Serialize(_dimErr)
            End Using
        Catch ex As Exception
            _dimclsRemittanceUpload.SQLTransaction.Rollback()
            _dimErr.Add(New ErrorList(2, MyExceptionNotice))

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
            Return _dimSerial.Serialize(_dimErr)
        Finally
            If _dimclsRemittanceUpload IsNot Nothing Then _dimclsRemittanceUpload.Dispose()

            _dimSerial = Nothing
            _dimErr = Nothing
        End Try
    End Function

    ' This method is used to convert datatable to json string
    Public Shared Function ConvertDataTabletoString(ByVal _pdttInput As DataTable) As String
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim rows As New List(Of Dictionary(Of String, Object))()
        Dim row As Dictionary(Of String, Object)

        For Each dr As DataRow In _pdttInput.Rows
            row = New Dictionary(Of String, Object)()

            For Each col As DataColumn In _pdttInput.Columns
                row.Add(col.ColumnName, dr(col))
            Next

            rows.Add(row)
        Next

        Return serializer.Serialize(rows)
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

        If Not _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelRole")).Contains("|remittanceupload.aspx|") Then
            _dimclsGlobalFunctions = Nothing
            Response.Redirect("errorPage.aspx")

            Exit Sub
        End If

        _dimclsGlobalFunctions = Nothing
    End Sub
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
