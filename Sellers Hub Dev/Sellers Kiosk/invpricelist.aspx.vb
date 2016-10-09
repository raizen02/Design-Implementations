'**************************************************************
'Programmer Name		: William delos Reyes
'Date Created			: 2010-05-04
'Finished Date          : 
'Program Name           : selInventoryPriceList
'Program Description    : Generate report for price list
'Stored Procedure       : 
'Updates Information
'                       * Marc Erickson P. Legaspi | 2010-07-19 | JO# JYYXXXXX
'                           - Transferred the code from selPDFLoader Load Event 
'                             and put it in Sub named 'PopulatePriceList'
'
'                       * Malvin V. Reyes | 2010-09-08 | JO# JYYXXXXX
'                           - change the reportdocuments into _prirepMyReport and declare as private
'                           - dispose the _prirepMyReport on the page unload event.
'                         
'                       * Nestor Garais Jr | 2010-10-02 | JO# JYYXXXXX
'                           - change MyUsername to a declared parameter 
'                           - DeCrypt (Session("sesSelUser")) when using as username
'
'                       * Malvin V. Reyes | 2012-01-13 | JO# JYYXXXXX
'                           - Change the design for the portal proposal.
'
'                       * Malvin V. Reyes | 2013-10-07 | JO# JYYXXXXX
'                           - Include the Phase/Building, Scheme Type for the Pricelist Upload
'                           - Add parameter for the Phase/Building, Scheme Type in Stored Procedure
'                           - Change the logic of loading from Load Complete to Selected Index
'
'						* Malvin V. Reyes | 2013-10-29 | JO# JYYXXXXX
'                           - Include Phase/Building and Scheme Type
'                           - Scroll Down upon viewing of Pricelist
'
'						* Malvin V. Reyes | 2013-12-03 | JO# JYYXXXXX
'						  DEV 2012-12-03 | PROD 2014-02-19
'							- Fixed the flexible layout.

'						* Malvin Reyes | 2014-03-04 | JO# JYYXXXXX
'						  DEV - 2014-03-04 | PROD 2014-05-13
'							- Implement the SSO process.
'                           - Include the "Inherits AuthenticatedPageBase" 
'                           - Include the "SessionAPI.RefreshSessions(CurrentUser)"
'                           - Remove the Session("sesSelVerified") checking

'						* Malvin Reyes | 2014-04-28 | JO# JYYXXXXX
'						  DEV - 2014-04-28 | PROD 2014-05-13
'							- Change the Title for Google Analytics

'						* Malvin Reyes | 2014-08-26 | JO# J1403015
'						  DEV - 2014-08-26 | PROD 2014-XX-XX
'							- Disable the Generate button (btnProceed) upon loading
'                           - Validate the ddlSchemeType and enable/disable the button based on condition
'                           - Set the ddlSchemeType to AutoPostBack="True"

'						* Nestor Garais Jr| 2015-06-11 | JO#  
'						  DEV - 2015-06-11 | PROD 
'							- Add allocation status dropdown
'                           - No scheme type for standard report, set to invisible
'                           - Change SQL stored proc.. from SP_selrptPriceList to SP_selOnlinePriceList
'                           - use class fsmclsPriceListGenarateReport for generating standard price list

'						* Nestor Garais Jr| 2015-09-07 | JO#  
'						  DEV - 2015-09-07 | PROD 
'							- hide allocation if one item only
'                           - hide all div of price list fields if no unit available

'						* Nestor Garais Jr| 2016-05-12 | JO#  
'						  DEV - 2016-05-12 | PROD 
'							- Close and Dispose report documents after used...
'
'                       * Nestor Garais Jr | 2016-06-28 | JO# JYYXXXXX
'						  DEV - 2016-06-28   PROD  
'                         Fix warnings :
'                                CA2000 : Microsoft.Reliability : In method 'invpricelist.btnProceed_Click(Object, EventArgs)', call System.IDisposable.Dispose on object '_priclsGeneratePriceList' before all references to it are out of scope.
'                                CA2000 : Microsoft.Reliability : In method 'invpricelist.btnProceed_Click(Object, EventArgs)', call System.IDisposable.Dispose on object '_prirpdPriceList' before all references to it are out of scope.
'                                CA2202 : Microsoft.Usage : Object '_prirpdPriceList' can be disposed more than once in method 'invpricelist.btnProceed_Click(Object, EventArgs)'. To avoid generating a System.ObjectDisposedException you should not call Dispose more than one time on an object.: Lines: 345
'                                CA2000 : Microsoft.Reliability : In method 'invpricelist.subGetData(Integer, String)', call System.IDisposable.Dispose on object '_priclsConnection' before all references to it are out of scope.
'                                CA2000 : Microsoft.Reliability : In method 'invpricelist.subGetData(Integer, String)', call System.IDisposable.Dispose on object '_dimcmdSqlCommand' before all references to it are out of scope.
'                                CA2000 : Microsoft.Reliability : In method 'invpricelist.subGetData(Integer, String)', call System.IDisposable.Dispose on object '_pridtaSqlAdapter' before all references to it are out of scope.
'                                CA2000 : Microsoft.Reliability : In method 'invpricelist.subGetData(Integer, String)', call System.IDisposable.Dispose on object '_pridtsDataSet' before all references to it are out of scope.
'                                   Solution : Add try/catch and dispose all disposable objects in try/finally
'
'                       * Nestor Garais Jr | 2016-06-28 | JO# JYYXXXXX
'						  DEV - 2016-06-30   PROD  
'                             - Remove callback panel
'                             - Apply jquery ajax function and web method for populate object
'                             - Replace asp control with html objects
'                             - Convert asp event to javascript in markup
'***********************************************************

Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Web.Script.Serialization
Imports System.Web.Services
Imports System.Web.Script.Services 
Imports FliAuthLib

Partial Class invpricelist
    Inherits AuthenticatedPageBase

#Region "Declaring Variables"
    ' Your constant variables here 
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
#Region "Procedures"
    ' Place your Sub here
#End Region

#Region "Functions"
    ' Place your Functions here
    <WebMethod(enableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function GetDataFromDB(ByVal Mode As Integer, _
                                         ByVal Allocation As String, _
                                         ByVal Project As String, _
                                         ByVal Phase As String, _
                                         ByVal SchemeType As String) As String
        GetDataFromDB = ""

        Dim _dimSerial As New JavaScriptSerializer
        Dim _dimstrJSONstring As New StringBuilder
        Dim _dimErr As New List(Of ErrorList)
        Dim _dimstrUserName As String

        _dimstrUserName = SessionAPI.Username

        ' Validate User Session
        If _dimstrUserName Is Nothing Or _dimstrUserName = "" Then
            _dimErr.Add(New ErrorList(101, "Your session has expired. Please log in again"))
            GetDataFromDB = _dimSerial.Serialize(_dimErr)

            Exit Function
        End If

        Dim _dimdtsResult As DataSet = Nothing
        Dim _dimcmdSqlCommand As SqlCommand = Nothing

        Try
            _dimdtsResult = New DataSet
            _dimcmdSqlCommand = New SqlCommand

            Using _dimsqlConnection As New SqlConnection(MyMSSQLServer2000ConnectionString)
                _dimsqlConnection.Open()

                With _dimcmdSqlCommand
                    .Connection = _dimsqlConnection
                    .CommandText = "SP_selOnlinePriceList"
                    .CommandType = CommandType.StoredProcedure
                    .CommandTimeout = 0

                    With .Parameters
                        .AddWithValue("@pintMode", Mode)
                        .AddWithValue("@pstrProjectCode", Project)
                        .AddWithValue("@pstrPhaseBuildingCode", Phase)
                        .AddWithValue("@pstrStatusAllocationCode", Allocation)
                        .AddWithValue("@pstrSchemeType", SchemeType)
                        .AddWithValue("@pstrUsername", _dimstrUserName)

                        .Add("@pintErrorNumber", SqlDbType.Int).Direction = ParameterDirection.Output
                        .Add("@pstrErrorMessage", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output
                    End With
                End With

                Using _dimSqlDta As New SqlDataAdapter(_dimcmdSqlCommand)
                    _dimSqlDta.Fill(_dimdtsResult)

                    If _dimSqlDta.SelectCommand.Parameters.Item("@pintErrorNumber").Value <> 8888 And _
                       _dimSqlDta.SelectCommand.Parameters.Item("@pintErrorNumber").Value IsNot Nothing Then
                        _dimErr.Add(New ErrorList(_dimSqlDta.SelectCommand.Parameters.Item("@pintErrorNumber").Value, _
                                                  _dimSqlDta.SelectCommand.Parameters.Item("@pstrErrorMessage").Value.ToString))
                        GetDataFromDB = _dimSerial.Serialize(_dimErr)

                        Exit Function
                    End If

                    If Mode = 5 Then
                        If _dimdtsResult.Tables(0).Rows.Count > 0 Then
                            Dim _dimstrSessionID As String = "sesrptReport"
                            Dim _dimPriceListHtml As String = ""

                            HttpContext.Current.Session.Add(_dimstrSessionID, _dimdtsResult.Tables(0).Rows(0).Item("UploadFile"))

                            _dimPriceListHtml = "<div class=""alert alert-error"">" & _
                                                "Kindly note Price List Validity Date.<br />" & _
                                                "For information on Unit Availability, please " & _
                                                "check with your PDO." & _
                                                "</div>" & _
                                                "<iframe src='selPDFLoader.aspx?SessionID=" & _dimstrSessionID & _
                                                "&isByte=1#zoom=100' name=""finScheme"" width=""100%"" height=""500"" align=""center"" border=""0"" frameborder=""1"" scrolling=""no"" ></iframe>"

                            _dimErr.Add(New ErrorList(8888, _dimPriceListHtml))
                            GetDataFromDB = _dimSerial.Serialize(_dimErr)

                            Exit Function
                        Else
                            _dimErr.Add(New ErrorList(111, "No record found."))
                            GetDataFromDB = _dimSerial.Serialize(_dimErr)

                            Exit Function
                        End If
                    Else
                        ' Result all string datatype, error if byte
                        _dimstrJSONstring.Append("{")

                        For _tblIndex As Integer = 0 To _dimdtsResult.Tables.Count - 1
                            _dimstrJSONstring.Append("""" + _tblIndex.ToString + """:") 'eg. "1":
                            _dimstrJSONstring.Append(ConvertDataTabletoString(_dimdtsResult.Tables(_tblIndex)))

                            If _tblIndex <> _dimdtsResult.Tables.Count - 1 Then
                                _dimstrJSONstring.Append(",")
                            End If
                        Next

                        _dimstrJSONstring.Append("}")
                    End If
                End Using
            End Using
        Catch ex As Exception
            _dimErr.Add(New ErrorList(111, MyExceptionNotice))
            GetDataFromDB = _dimSerial.Serialize(_dimErr)

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)

            Exit Function
        Finally
            If _dimdtsResult IsNot Nothing Then _dimdtsResult.Dispose()
            If _dimcmdSqlCommand IsNot Nothing Then _dimcmdSqlCommand.Dispose()

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

    <WebMethod(enableSession:=True)> _
       <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function ShowPriceList(ByVal Allocation As String, _
                                         ByVal Project As String, _
                                         ByVal Phase As String) As String
        ShowPriceList = ""

        Dim _dimSerial As New JavaScriptSerializer
        Dim _dimstrJSONstring As New StringBuilder
        Dim _dimErr As New List(Of ErrorList)
        Dim _dimstrUserName As String

        _dimstrUserName = SessionAPI.Username

        ' Validate User Session
        If _dimstrUserName Is Nothing Or _dimstrUserName = "" Then
            _dimErr.Add(New ErrorList(101, "Your session has expired. Please log in again"))
            ShowPriceList = _dimSerial.Serialize(_dimErr)

            Exit Function
        End If

        Dim _priclsGeneratePriceList As New fsmclsPriceListGenarateReport
        Dim _prirpdPriceList As New ReportDocument

        Try
            Dim _dimstrReportPath As String = HttpContext.Current.Server.MapPath("Reports\fsmrptPriceList_V2.rpt")

            _prirpdPriceList.Load(_dimstrReportPath)

            ' New report viewer per status allocation 
            If _priclsGeneratePriceList.GenerateReport(MyMSSQLServer2000ConnectionString, _
                                                       _prirpdPriceList, _
                                                       fsmclsPriceListGenarateReport.ReportType.Standard, _
                                                       Project, _
                                                       Phase, _
                                                       Allocation, _
                                                       _dimstrUserName, "") = False Then
                _dimErr.Add(New ErrorList(14, _priclsGeneratePriceList.PriceListErrorMessage))
                ShowPriceList = _dimSerial.Serialize(_dimErr)

                Exit Function
            Else
                Dim _dimstrSessionID As String = "sesrptReport"
                Dim _dimPriceListHtml As String = ""

                HttpContext.Current.Session.Add(_dimstrSessionID, _priclsGeneratePriceList.PriceListReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat))

                _dimPriceListHtml = "<iframe src='selPDFLoader.aspx?SessionID=" & _dimstrSessionID & _
                                    "#zoom=100' name=""finScheme"" width=""100%"" height=""500"" align=""center"" border=""0"" frameborder=""1"" scrolling=""no"" ></iframe>"

                _dimErr.Add(New ErrorList(8888, _dimPriceListHtml))
                ShowPriceList = _dimSerial.Serialize(_dimErr)

                Exit Function
            End If
        Catch ex As Exception
            _dimErr.Add(New ErrorList(111, MyExceptionNotice))
            ShowPriceList = _dimSerial.Serialize(_dimErr)

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)

            Exit Function
        Finally
            If _priclsGeneratePriceList IsNot Nothing Then _priclsGeneratePriceList.Dispose()
            If _prirpdPriceList IsNot Nothing Then _prirpdPriceList.Dispose()

            _dimSerial = Nothing
            _dimstrJSONstring = Nothing
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
        'SessionAPI.RefreshSessions(CurrentUser)

        'If Not Page.IsPostBack Then
        '    Dim _dimclsGlobalFunctions As New clsGlobalFunctions

        '    If Not _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelRole")).Contains("|invpricelist.aspx|") Then
        '        Response.Redirect("errorPage.aspx")

        '        Exit Sub
        '    End If

        '    _dimclsGlobalFunctions = Nothing
        'End If
    End Sub

    'Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
    '    SqlConnection.ClearAllPools()
    'End Sub 'Page_Unload
#End Region

#Region "Button"

#End Region

#Region "Grid"
    ' Events in all Grid

#End Region

#Region "DropDownList"

#End Region

    'etc...
#End Region
End Class
