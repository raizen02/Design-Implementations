'**************************************************************
'Programmer Name		: Malvin Reyes
'Date Created			: 2013-12-16
'Finished Date          : 
'Program Name           : Availability Chart
'Program Description    : 
'Stored Procedure       : 
'Updates Information
'						* Nestor Garais Jr | 2014-02-08 | JO# JYYXXXXX
'						  DEV - 2014-02-08 | PROD mm/dd/yyyy
'							- Tabpage 2 : Populate unit info and price list information

'						* Nestor Garais Jr | 2014-02-11 | JO# JYYXXXXX
'						  DEV - 2014-02-11 | PROD 2014-02-19
'							- Add room type filter from selected project, display only allowed room type
'                           - Added column Select CommandField to grdReseult ,then move index room type from 3 to 4
'                           - Populate unit details when unit is selected drop down list of units ddlInventoryUnit_SelectedIndexChanged, and when trigger btnInventoryUnitView_Click

'						* Malvin Reyes | 2014-03-03 | JO# JYYXXXXX
'						  DEV - 2014-03-03 | PROD 2014-05-13
'							- Implement the SSO process.
'                           - Include the "Inherits AuthenticatedPageBase" 
'                           - Include the "SessionAPI.RefreshSessions(CurrentUser)"
'                           - Remove the Session("sesSelVerified") checking

'						* Malvin Reyes | 2014-04-28 | JO# JYYXXXXX
'						  DEV - 2014-04-28 | PROD 2014-05-13
'							- Change the Title for Google Analytics

'						* Nestor Garais Jr | 2014-07-22 | JO# JYYXXXXX
'						  DEV - 2014-07-22 | PROD  2016-05-10
'							- Change the room type to market product subtype

'						* Nestor Garais Jr | 2015-09-18 | JO# JYYXXXXX
'						  DEV -  2015-09-18 | PROD  2016-05-10  
'							- Apply jquery ajax function for loading data
'                           - Replace asp control with html objects

'						* Nestor Garais Jr | 2016-04-26 | JO# JYYXXXXX
'						  DEV -  2016-04-26 | PROD  2016-05-10
'                           - Fix error subtype checkbox list.. unclickable due overlapping of checkbox with blank description ..

'						* Nestor Garais Jr | 2016-05-31 | JO# JYYXXXXX
'						  DEV -  2016-05-31 | PROD  2016-06-01
'                           - [Markup] Hide divResult when filter/dropdown changes
'                       
'						* Nestor Garais Jr | 2016-06-22 | JO# JYYXXXXX
'						  DEV -  2016-06-22 | PROD  
'                           - Fix Warning : CA2000 : Microsoft.Reliability : In method 'availabilitychart.GetData(ByRef Object, Integer, String, String, String, String, String, String)', call System.IDisposable.Dispose on object '_dimdttResult' before all references to it are out of scope.
'                              CA2000 : Microsoft.Reliability : In method 'availabilitychart.GetData(ByRef Object, Integer, String, String, String, String, String, String)', call System.IDisposable.Dispose on object '_dimdttResult' before all references to it are out of scope.
'                              CA2000 : Microsoft.Reliability : In method 'availabilitychart.GetData(ByRef Object, Integer, String, String, String, String, String, String)', object '_dimCmd' is not disposed along all exception paths. Call System.IDisposable.Dispose on object '_dimCmd' before all references to it are out of scope.
'                              CA2202 : Microsoft.Usage : Object '_dimsqlConnection' can be disposed more than once in method 'availabilitychart.GetData(ByRef Object, Integer, String, String, String, String, String, String)'. To avoid generating a System.ObjectDisposedException you should not call Dispose more than one time on an object.: Lines: 399, 401
'                                   Solution : Add try/catch and dispose all disposable objects in try/finally
'**************************************************************
Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Imports System.Web.Script.Serialization
Imports FliAuthLib
Imports System.Web.UI
Imports System.Web
Imports System

Partial Class availabilitychart
    Inherits Page

#Region "Declaring Variables"
    ' Your constant variables here
    Private Enum enumTransMode
        etmLocation = 1
        etmProject = 2
        etmPhase = 3
        etmBlock = 4
        etmInventoryUnit = 5
        etmProductSubType = 6
        etmResultGrid = 7
        etmTempComputation = 8
    End Enum

    Private Class _priclsDropDownDataSource
        Private _pristrCode As String
        Private _pristrDescription As String

        Property Code() As String
            Get
                Return _pristrCode
            End Get

            Set(ByVal value As String)
                _pristrCode = value
            End Set
        End Property

        Property Description() As String
            Get
                Return _pristrDescription
            End Get

            Set(ByVal value As String)
                _pristrDescription = value
            End Set
        End Property
    End Class

    Private Class _priclsErrorList
        Private _pristrNumber As String
        Private _pristrErrorMessage As String

        Property ErrorNumber() As String
            Get
                Return _pristrNumber
            End Get

            Set(ByVal value As String)
                _pristrNumber = value
            End Set
        End Property

        Property ErrorMessage() As String
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

    Private Class _priclsUnitList
        Private _pristrPhase As String
        Private _pristrBlock As String
        Private _pristrUnitName As String
        Private _pristrMarketProductType As String
        Private _pristrMarketProductSubType As String
        Private _pristrReferenceObject As String

        Property Phase() As String
            Get
                Return _pristrPhase
            End Get

            Set(ByVal value As String)
                _pristrPhase = value
            End Set
        End Property

        Property Block() As String
            Get
                Return _pristrBlock
            End Get

            Set(ByVal value As String)
                _pristrBlock = value
            End Set
        End Property

        Property UnitName() As String
            Get
                Return _pristrUnitName
            End Get

            Set(ByVal value As String)
                _pristrUnitName = value
            End Set
        End Property

        Property MarketProductType() As String
            Get
                Return _pristrMarketProductType
            End Get

            Set(ByVal value As String)
                _pristrMarketProductType = value
            End Set
        End Property

        Property MarketProductSubType() As String
            Get
                Return _pristrMarketProductSubType
            End Get

            Set(ByVal value As String)
                _pristrMarketProductSubType = value
            End Set
        End Property

        Property ReferenceObject() As String
            Get
                Return _pristrReferenceObject
            End Get

            Set(ByVal value As String)
                _pristrReferenceObject = value
            End Set
        End Property
    End Class

    Private Class _priclsUnitPricingDetails
        Private _pristrUnitInfo As String
        Private _pristrPricingInfo As String

        Property UnitInfo() As String
            Get
                Return _pristrUnitInfo
            End Get

            Set(ByVal value As String)
                _pristrUnitInfo = value
            End Set
        End Property

        Property PricingInfo() As String
            Get
                Return _pristrPricingInfo
            End Get

            Set(ByVal value As String)
                _pristrPricingInfo = value
            End Set
        End Property
    End Class
#End Region

#Region "     User Define Subs and Functions     "
#Region "Procedures"

#End Region

#Region "Functions"
    ' Place your Functions here

    'Partial Class GetData
    ' Inherits System.Web.UI.Page
    <System.Web.Services.WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function DataFromDB(ByVal Mode As Integer,
                                      ByVal Location As String,
                                      ByVal Project As String,
                                      ByVal Phase As String,
                                      ByVal Block As String,
                                      ByVal RefObj As String,
                                      ByVal SubType As String) As String
        DataFromDB = ""

        Dim _dimserSerial As New JavaScriptSerializer
        Dim _dimObjectToSerialize As Object = Nothing

        If fnbolGetData(_dimObjectToSerialize,
                        Mode,
                        Location,
                        Project,
                        Phase,
                        Block,
                        RefObj,
                        SubType) = False Then
            DataFromDB = _dimserSerial.Serialize(_dimObjectToSerialize)

            Exit Function
        End If

        HttpContext.Current.Response.Clear()
        'HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*") 'for firefox bugs
        HttpContext.Current.Response.Write(_dimserSerial.Serialize(_dimObjectToSerialize))

        _dimserSerial = Nothing
        _dimObjectToSerialize = Nothing

        HttpContext.Current.Response.Flush()
        HttpContext.Current.Response.End()
    End Function

    Private Shared Function fnbolGetData(ByRef _parobjOutputObj As Object, _
                                         ByVal _parintMode As Integer, _
                                         ByVal _parstrLocation As String, _
                                         ByVal _parstrProject As String, _
                                         ByVal _parstrPhase As String, _
                                         ByVal _parstrBlock As String, _
                                         ByVal _parstrRefObj As String, _
                                         ByVal _parstrSubType As String) As Boolean
        fnbolGetData = False

        Dim _dimlstErr As New List(Of _priclsErrorList)

        'If HttpContext.Current.Session("sesSelUser") Is Nothing Or HttpContext.Current.Session("sesSelUser") = "" Then
        '    _dimlstErr.Add(New _priclsErrorList(101, "Your session has expired. Please log in again"))
        '    _parobjOutputObj = _dimlstErr

        '    Exit Function
        'End If

        Dim _dimstrUserName As String
        Dim _dimclsGlobalFunctions As New clsGlobalFunctions

        _dimstrUserName = _dimclsGlobalFunctions.fnDeCrypt(HttpContext.Current.Session("sesSelUser"))

        Dim _dimcmdSqlCommand As SqlCommand = Nothing
        Dim _dimdttResult As DataTable = Nothing

        Try
            _dimcmdSqlCommand = New SqlCommand

            Using _dimsqlConnection As New SqlConnection(MyMSSQLServer2000ConnectionString)
                _dimsqlConnection.Open()

                _dimcmdSqlCommand.CommandText = "sp_AvailabilityChart_v2"
                _dimcmdSqlCommand.CommandType = CommandType.StoredProcedure
                _dimcmdSqlCommand.Connection = _dimsqlConnection
                _dimcmdSqlCommand.CommandTimeout = 0

                With _dimcmdSqlCommand.Parameters
                    .AddWithValue("@pintMode", _parintMode)
                    .AddWithValue("@pstrLocation", _parstrLocation)
                    .AddWithValue("@pstrProjectCode", _parstrProject)
                    .AddWithValue("@pstrPhaseCode", _parstrPhase)
                    .AddWithValue("@pstrBlockCode", _parstrBlock)
                    .AddWithValue("@pstrReferenceObjectCode", _parstrRefObj)
                    .AddWithValue("@pstrProductSubType", _parstrSubType)
                    .AddWithValue("@pstrUserName", "beerfs")
                    .Add("@pintErrorNumber", SqlDbType.SmallInt).Direction = ParameterDirection.Output
                    .Add("@pstrErrorMessage", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output
                End With

                Select Case _parintMode
                    '' DROPDOWN, Checkbox list , using serialize _priclsDropDownDataSource ArrayList
                    Case enumTransMode.etmLocation, _
                         enumTransMode.etmProject, _
                         enumTransMode.etmPhase, _
                         enumTransMode.etmBlock, _
                         enumTransMode.etmInventoryUnit, _
                         enumTransMode.etmProductSubType
                        Using _dtrDataReader As SqlDataReader = _dimcmdSqlCommand.ExecuteReader
                            Dim errorMsg As String = ""

                            If _dimcmdSqlCommand.Parameters.Item("@pintErrorNumber").Value <> 8888 And _dimcmdSqlCommand.Parameters.Item("@pintErrorNumber").Value IsNot Nothing Then
                                _dimlstErr.Add(New _priclsErrorList(_dimcmdSqlCommand.Parameters.Item("@pintErrorNumber").Value, _
                                                                    _dimcmdSqlCommand.Parameters.Item("@pstrErrorMessage").Value.ToString))
                                _parobjOutputObj = _dimlstErr

                                Exit Function
                            End If

                            Dim _dimlstList As New List(Of _priclsDropDownDataSource)
                            Dim _dimlstNewItem As _priclsDropDownDataSource

                            _dimlstNewItem = New _priclsDropDownDataSource
                            _dimlstNewItem.Code = ""
                            _dimlstNewItem.Description = ""
                            _dimlstList.Add(_dimlstNewItem)

                            While _dtrDataReader.Read
                                _dimlstNewItem = New _priclsDropDownDataSource
                                _dimlstNewItem.Code = _dtrDataReader.Item(0)
                                _dimlstNewItem.Description = _dtrDataReader.Item(1)
                                _dimlstList.Add(_dimlstNewItem)
                            End While

                            _parobjOutputObj = _dimlstList
                        End Using

                    Case enumTransMode.etmResultGrid 'Use Result Grid Source ArrayList
                        _dimdttResult = New DataTable

                        Using _dimdtaSqlDataAdapter As New SqlDataAdapter(_dimcmdSqlCommand)
                            _dimdtaSqlDataAdapter.Fill(_dimdttResult)
                        End Using

                        Dim _dimlstUnitList As New List(Of _priclsUnitList)

                        For Each _dtr As DataRow In _dimdttResult.Rows
                            Dim _dimclsItemUnitList As New _priclsUnitList

                            _dimclsItemUnitList.Phase = _dtr.Item("Phase")
                            _dimclsItemUnitList.Block = _dtr.Item("Block")
                            _dimclsItemUnitList.UnitName = _dtr.Item("UnitName")
                            _dimclsItemUnitList.MarketProductType = _dtr.Item("MarketProductType")
                            _dimclsItemUnitList.MarketProductSubType = _dtr.Item("MarketProductSubType")
                            _dimclsItemUnitList.ReferenceObject = _dtr.Item("ReferenceObject")
                            _dimlstUnitList.Add(_dimclsItemUnitList)
                        Next

                        _parobjOutputObj = _dimlstUnitList

                    Case enumTransMode.etmTempComputation
                        _dimdttResult = New DataTable

                        Using _dimdtaSqlDataAdapter As New SqlDataAdapter(_dimcmdSqlCommand)
                            _dimdtaSqlDataAdapter.Fill(_dimdttResult)
                        End Using

                        Dim _dimlstUnitPricingDetails As New List(Of _priclsUnitPricingDetails)

                        For Each _dtr As DataRow In _dimdttResult.Rows
                            Dim _dimclsItemUnitPricingDetails As New _priclsUnitPricingDetails

                            _dimclsItemUnitPricingDetails.UnitInfo = _dtr.Item("UnitInfo")
                            _dimclsItemUnitPricingDetails.PricingInfo = _dtr.Item("PricingInfo")
                            _dimlstUnitPricingDetails.Add(_dimclsItemUnitPricingDetails)
                        Next

                        _parobjOutputObj = _dimlstUnitPricingDetails
                End Select

                fnbolGetData = True
            End Using
        Catch ex As Exception
            _dimlstErr.Add(New _priclsErrorList(_dimcmdSqlCommand.Parameters.Item("@pintErrorNumber").Value, _
                                                MyExceptionNotice))
            _parobjOutputObj = _dimlstErr

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)

            fnbolGetData = False
        Finally
            If _dimdttResult IsNot Nothing Then _dimdttResult.Dispose()
            If _dimcmdSqlCommand IsNot Nothing Then _dimcmdSqlCommand.Dispose()
        End Try
    End Function
#End Region
#End Region

#Region "     Events of Object     "
    ' Group the events of every control
#Region "Form"
    ' Events of Main form
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        HttpContext.Current.Session("sesSelUser") = "ROBBYGE"
        'SessionAPI.RefreshSessions(CurrentUser)

        'Dim _dimclsGlobalFunctions As New clsGlobalFunctions

        'If Not _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelRole")).Contains("|availabilitychart.aspx|") Then
        '    _dimclsGlobalFunctions = Nothing
        '    Response.Redirect("errorPage.aspx")

        '    Exit Sub
        'End If

        '_dimclsGlobalFunctions = Nothing
    End Sub
#End Region
#End Region
End Class





