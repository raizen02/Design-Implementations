'**************************************************************
'Programmer Name		: Nestor S. Garais Jr
'Date Created			: 2013-11-25
'Finished Date          : 20xx-xx-xx
'Program Name           : Unit Hold
'Program Description    : Online Hold with remit and upload file
'Stored Procedure       : 
'Updates Information
'						* Nestor Garais Jr | 2013-12-19 | JO# JYYXXXXX
'						  DEV - 2013-12-19 | PROD 2014-02-19
'							- add mode 21 : to get days of hold for saving status tagged to frebas server

'						* Nestor Garais Jr | 2014-01-29 | JO# JYYXXXXX
'						  DEV - 2014-01-29 | PROD 2014-02-19
'							- when saving unit hold , use Session("sesSelUser") instead of Session("sesSelagentcode")

'						* Nestor Garais Jr | 2014-01-30 | JO# JYYXXXXX
'						  DEV - 2014-01-30 | PROD 2014-02-19
'							- Apply smooth scrolling for  required validator error message 
'							- Replace DivFocus values to html anchor name...

'						* Nestor Garais Jr | 2014-02-03 | JO# JYYXXXXX
'						  DEV - 2014-02-03 | PROD 2014-02-19
'							- When saving frebas hold status, use Session("sesSelagentcode") as created by

'						* Malvin Reyes | 2014-03-03 | JO# JYYXXXXX
'						  DEV - 2014-03-03 | PROD 2014-05-13
'							- Implement the SSO process.
'                           - Include the "Inherits AuthenticatedPageBase" 
'                           - Include the "SessionAPI.RefreshSessions(CurrentUser)"
'                           - Remove the Session("sesSelVerified") checking

'						* Nestor Garais Jr | 2014-03-05 | JO# J1400485
'						  DEV - 2014-03-05 | PROD  2014-05-13
'							- Always Hide Eam details tab
'						* Nestor Garais Jr | 2014-03-05 | JO# J1400485
'						  DEV - 2014-03-11 | PROD  2014-05-13
'							- Always Hide Network structure code textbox 
'                           - Fix scrolling to div error message

'						* Malvin Reyes | 2014-04-28 | JO# JYYXXXXX
'						  DEV - 2014-04-28 | PROD 2014-05-13
'							- Change the Title for Google Analytics

'						* Nestor Garais Jr | 2014-05-08 | JO# JYYXXXXX
'						  DEV - 2014-05-08 | PROD 2014-XX-XX
'							- Replace EO Ajaxfileuploader to user control image upload( ucImageUpload ), due to error on loading when using Firefox browser

'						* Nestor Garais Jr | 2014-06-24 | JO# JYYXXXXX
'						  DEV - 2014-06-24| PROD 2014-XX-XX
'							- change prompt message upon holding from SQL 

'						* Nestor Garais Jr | 2014-10-15 | JO# J1403385
'						  DEV -  2014-10-15 | PROD 
'							For local user; 
'                               - add seller info based on login info
'                               - hide seller position
'                               - hide preferred due date
'						* Nestor Garais Jr | 2015-07-15 | JO#  
'						  DEV -  2015-07-15 | PROD 
'						  - add allocation selection
'                         - remove unused objects/field: EAM fields, financing type
'                         - set to one time saving of days of hold status BH in frebas
'                         - the visibility of fields depend allocation selected
'						* Nestor Garais Jr | 2015-11-10 | JO#  
'						  DEV -  2015-11-10 | PROD 
'                         - set btnSave always visible
'						* Nestor Garais Jr | 2015-11-10 | JO#  
'						  DEV -  2015-11-10 | PROD 
'                         - use webmethod SaveData for saving data to database
'						* Nestor Garais Jr | 2016-01-22 | JO#  
'						  DEV -   2016-01-22 | PROD 
'                         - clear field after add contact, remittance, doc and seller

'						* Nestor Garais Jr | 2016-02-09  | JO# JYYXXXXX
'						  DEV - 2016-02-09  PROD  
'                             - Rename error message when converting image to filestream
'						* Nestor Garais Jr | 2016-05-06  | JO# JYYXXXXX
'						  DEV - 2016-05-06  PROD  
'                             - Fix: Error getting blank seller name if not affiliated checked..

'						* Nestor Garais Jr | 2016-05-13  | JO# JYYXXXXX
'						  DEV - 2016-05-13 PROD 2016-05-13 
'                             -Fix: add validation for capturing blank username when session expired

'						* Nestor Garais Jr | 2016-05-17  | JO# JYYXXXXX
'						  DEV - 2016-05-17 PROD 2016-05-17
'                             - Fix on error not getting result message from UnitHoldSaveFREBAS_Status() saving unit status to FREBAS 
'                             - Hide divAllocation when save successful   

'						* Nestor Garais Jr | 2016-05-19  | JO# JYYXXXXX
'						  DEV - 2016-05-19 PROD 2016-05-19
'                             - Fix typographycal error on preffered due date   

'						* Nestor Garais Jr | 2016-06-29 | JO# JYYXXXXX
'						  DEV -  2016-06-29 | PROD  
'                         Fix warnings :
'                               CA2000 : Microsoft.Reliability : In method 'unithold.GetDataFromDB(Integer, String, String, String, String, String, String, String, String)', call System.IDisposable.Dispose on object '_dimclsUnitHold' before all references to it are out of scope.
'                               CA2000 : Microsoft.Reliability : In method 'unithold.GetDataFromDB(Integer, String, String, String, String, String, String, String, String)', call System.IDisposable.Dispose on object '_dimdtsResult' before all references to it are out of scope.
'                               CA2202 : Microsoft.Usage : Object '_dimsqlConnection' can be disposed more than once in method 'unithold.GetDataFromDB(Integer, String, String, String, String, String, String, String, String)'. To avoid generating a System.ObjectDisposedException you should not call Dispose more than one time on an object.: Lines: 330, 334
'                               CA2000 : Microsoft.Reliability : In method 'unithold.SaveData(String)', call System.IDisposable.Dispose on object '_dimDtsData' before all references to it are out of scope.
'                               CA2000 : Microsoft.Reliability : In method 'unithold.SaveData(String)', call System.IDisposable.Dispose on object '_dimDttDaysofHoldTable' before all references to it are out of scope.
'                               CA2000 : Microsoft.Reliability : In method 'unithold.SaveData(String)', call System.IDisposable.Dispose on object '_dimFileStream' before all references to it are out of scope.
'                               CA2000 : Microsoft.Reliability : In method 'unithold.SaveData(String)', call System.IDisposable.Dispose on object '_dimFileStream' before all references to it are out of scope.
'                               CA2000 : Microsoft.Reliability : In method 'unithold.SaveData(String)', call System.IDisposable.Dispose on object '_dimXmlReader' before all references to it are out of scope.
'                               CA2000 : Microsoft.Reliability : In method 'unithold.SaveData(String)', call System.IDisposable.Dispose on object '_dimclsUnitHold' before all references to it are out of scope.
'                               CA2000 : Microsoft.Reliability : In method 'unithold.SaveData(String)', call System.IDisposable.Dispose on object '_dimclsUnitHoldFREBAS' before all references to it are out of scope.
'                               CA2000 : Microsoft.Reliability : In method 'unithold.SaveData(String)', call System.IDisposable.Dispose on object '_dimsqlFREBASConnection' before all references to it are out of scope.
'                               CA2202 : Microsoft.Usage : Object '_dimFileStream' can be disposed more than once in method 'unithold.SaveData(String)'. To avoid generating a System.ObjectDisposedException you should not call Dispose more than one time on an object.: Lines: 600, 602
'                               CA2202 : Microsoft.Usage : Object '_dimFileStream' can be disposed more than once in method 'unithold.SaveData(String)'. To avoid generating a System.ObjectDisposedException you should not call Dispose more than one time on an object.: Lines: 646, 648
'                               CA2202 : Microsoft.Usage : Object '_dimsqlConnection' can be disposed more than once in method 'unithold.SaveData(String)'. To avoid generating a System.ObjectDisposedException you should not call Dispose more than one time on an object.: Lines: 757
'                                   Solution : Add try/catch and dispose all disposable objects in try/finally
'                                              Declare variable to top level /parent level of using block 
'***********************************************************************

Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Imports System.Web.Script.Serialization
Imports FliAuthLib
Imports System.IO

Partial Class unithold
    'Inherits System.Web.UI.Page
    Inherits AuthenticatedPageBase

#Region "     Declaring Variables     "
    ' Your constant variables here
    Private Enum TransMode
        Mode01UserAllocationRights = 1
        Mode02SelectDropdownlistsource = 2
        Mode03SearchPhaseBuildingList = 3
        Mode04SearchBlockList = 4
        Mode05SearchUnitList = 5
        Mode06SelectHouseModelInclusionSchemeTypelist = 6
        Mode07ContractTypeList = 7
        Mode08DisplayPromocodeTCPReservationFee = 8
        Mode09SelectSalesOffice = 9
        Mode10SavingUnitHold = 10
        Mode11SavingContactNumber = 11
        Mode12SavingSeller = 12
        Mode13SavingRemittance = 13
        Mode14SavingRemittanceImages = 14
        Mode15SavingDocuments = 15
        Mode16ValidationDocuments = 16
        Mode17PushemailSuccessHolding = 17
        Mode18DaysofHold = 18
        Mode19PromptuponSuccessfullyHold = 19
    End Enum
 
    Public Class DropDownDataSource
        Private _priStrCode As String
        Private _priDescription As String

        Public Property Code() As String
            Get
                Return _priStrCode
            End Get

            Set(ByVal value As String)
                _priStrCode = value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return _priDescription
            End Get

            Set(ByVal value As String)
                _priDescription = value
            End Set
        End Property
    End Class

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
#Region "Procedures"

#End Region

#Region "Functions"
    ' Place your Functions here

    'Partial Class GetData
    ' Inherits System.Web.UI.Page
    <WebMethod()> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function GetDataFromDB(ByVal Mode As Integer, _
                                         ByVal Allocation As String, _
                                         ByVal Project As String, _
                                         ByVal Phase As String, _
                                         ByVal Block As String, _
                                         ByVal RefObj As String, _
                                         ByVal SchemeType As String, _
                                         ByVal ContractType As String, _
                                         ByVal SalesCountry As String) As String
        'HttpContext.Current.Response.Write("Test")
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

        Dim _dimclsUnitHold As clsUnitHold = Nothing
        Dim _dimdtsResult As DataSet = Nothing

        Try
            _dimclsUnitHold = New clsUnitHold
            _dimdtsResult = New DataSet

            With _dimclsUnitHold
                Using _dimsqlConnection As New SqlConnection(MyMSSQLServer2000ConnectionString)
                    _dimsqlConnection.Open()

                    .SQLConnection = _dimsqlConnection

                    If .ExeCuteAko(clsUnitHold.ExecuteCommand.ExecuteDataAdapter, _
                                   _dimstrUserName, _
                                   Mode, _
                                   Allocation, _
                                   "LastName", _
                                   "FirstName", _
                                   "MiddleName", _
                                   Project, _
                                   Phase, _
                                   Block, _
                                   "LotUnitShareNumber", _
                                   "ParCode", _
                                   "ParType", _
                                   "ContactTypeCode", _
                                   "ContactDetails", _
                                   ContractType, _
                                   SchemeType, _
                                   "CompanyCode", _
                                   "ContractNumber", _
                                   RefObj, _
                                   "SellerAgentCode", _
                                   , _
                                   "SellerName", _
                                   "SellerNetworkStructure", _
                                   "EmailAddress", _
                                   , _
                                   SalesCountry, _
                                   "SalesOfficeCode", _
                                   "ModeofPayment", _
                                   "CompanyName", _
                                   "BankName", _
                                   , _
                                   , _
                                   "RemittanceReferenceCode", _
                                   , _
                                   "FileName", _
                                   "PreferredDueDate", _
                                   "DocumentType") = False Then
                        _dimErr.Add(New ErrorList(1, .SQLMessage))
                        GetDataFromDB = _dimSerial.Serialize(_dimErr)

                        Exit Function
                    End If

                    With .SQLDataAdapter
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
                End Using
            End With
        Catch ex As Exception
            _dimErr.Add(New ErrorList(111, MyExceptionNotice))
            GetDataFromDB = _dimSerial.Serialize(_dimErr)

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)

            Exit Function
        Finally
            If _dimclsUnitHold IsNot Nothing Then _dimclsUnitHold.Dispose()
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
        Dim _dimstrUserName As String

        ' Validate User Session
        _dimstrUserName = SessionAPI.Username

        ' Validate User Session
        If _dimstrUserName Is Nothing Or _dimstrUserName = "" Then
            _dimErr.Add(New ErrorList(101, "Your session has expired. Please log in again"))

            Return _dimSerial.Serialize(_dimErr)
        End If

        '// Tables : _dimDtsData
        '  //  BuyerUnitInfo (buyer info, unit info, sales location)
        '  //  ContactInfo
        '  //  RemitInfo
        '  //  RemitImages
        '  //  DocInfoImages
        '  //  SellerInfo
        '  //  AdjoiningUnit

        Dim _dimclsUnitHold As clsUnitHold = Nothing
        Dim _dimXmlReader As StringReader = Nothing
        Dim _dimstrTempPath As String
        Dim _dimreader As BinaryReader
        Dim _dimByte As Byte()
        Dim _dimclsUnitHoldFREBAS As clsUnitHold = Nothing
        Dim _dimDtsData As DataSet = Nothing
        Dim _dimDttDaysofHoldTable As DataTable = Nothing

        Dim _dimstrAllocation As String
        Dim _dimStrNewCompanyCode As String
        Dim _dimStrNewContractNumber As String
        Dim _dimStrNewBusinessPartnerCode As String
        Dim _dimintHoldDaytoHold As Integer = 7 ' Default 7
        Dim _dimstrRemittanceReferenceCode As String = ""

        _dimstrTempPath = HttpContext.Current.Server.MapPath("./TempUploadFiles/")

        Try
            _dimclsUnitHold = New clsUnitHold
            _dimXmlReader = New StringReader(Data)
            _dimDtsData = New DataSet
            ' Convert xml arguments to dataset
            _dimDtsData.ReadXml(_dimXmlReader, XmlReadMode.Auto)

            'Validate arguments value
            If _dimDtsData.Tables.Contains("BuyerUnitInfo") = False Then
                ' Error
                _dimErr.Add(New ErrorList(1, "Buyer and unit information is missing"))

                Return _dimSerial.Serialize(_dimErr)
            End If

            _dimclsUnitHoldFREBAS = New clsUnitHold
            _dimDttDaysofHoldTable = New DataTable

            Using _dimsqlConnection As New SqlConnection(MyMSSQLServer2000ConnectionString)
                _dimsqlConnection.Open()
                _dimclsUnitHold.SQLConnection = _dimsqlConnection

                _dimstrAllocation = _dimDtsData.Tables("BuyerUnitInfo").Rows(0).Item("Allocation")

                'Validation of Documents
                If _dimDtsData.Tables.Contains("DocInfoImages") = True Then
                    Dim _dimstrDocumentTypeCode As String = ""

                    For Each _dtrDataDocuImages As DataRow In _dimDtsData.Tables("DocInfoImages").Rows
                        _dimstrDocumentTypeCode &= _dtrDataDocuImages.Item("DocumentTypeCode") & "•"
                    Next

                    If _dimclsUnitHold.ExeCuteAko(clsUnitHold.ExecuteCommand.ExecuteNonQuery, _
                                                  _dimstrUserName, _
                                                  TransMode.Mode16ValidationDocuments, _
                                                  _dimstrAllocation, _
                                                  , , , , , , , , , , , , , , , , , , , , , , , , , , , , , , , , , _
                                                  _dimstrDocumentTypeCode) = False Then
                        _dimErr.Add(New ErrorList(1, _dimclsUnitHold.SQLMessage))

                        Return _dimSerial.Serialize(_dimErr)
                    End If
                End If

                ' Get Days of Hold              
                If _dimclsUnitHold.ExeCuteAko(clsUnitHold.ExecuteCommand.ExecuteDataAdapter, _
                                              _dimstrUserName, _
                                              TransMode.Mode18DaysofHold, _
                                              _dimstrAllocation) = False Then
                    _dimErr.Add(New ErrorList(1, _dimclsUnitHold.SQLMessage))

                    Return _dimSerial.Serialize(_dimErr)
                End If

                _dimclsUnitHold.SQLDataAdapter.Fill(_dimDttDaysofHoldTable)

                If _dimDttDaysofHoldTable.Rows.Count = 0 Then
                    _dimErr.Add(New ErrorList(1, "Error while getting days to hold of unit"))

                    Return _dimSerial.Serialize(_dimErr)
                Else
                    _dimintHoldDaytoHold = _dimDttDaysofHoldTable.Rows(0).Item("DaysToHold")
                End If

                ' Apply transaction
                _dimclsUnitHold.SQLTransaction = _dimsqlConnection.BeginTransaction

                ' Insert reservation 
                ' Save Busines partner
                ' Get Reservation Number 
                ' Update Unit status to hold  
                With _dimDtsData.Tables("BuyerUnitInfo").Rows(0)
                    If _dimclsUnitHold.ExeCuteAko(clsUnitHold.ExecuteCommand.ExecuteNonQuery, _
                                                  _dimstrUserName, _
                                                  TransMode.Mode10SavingUnitHold, _
                                                  .Item("Allocation"), _
                                                  .Item("LastName"), _
                                                  .Item("FirstName"), _
                                                  .Item("MiddleName"), _
                                                  .Item("Project"), _
                                                  .Item("Phase"), _
                                                  .Item("Block"), _
                                                  .Item("Unit"), , _
                                                  "INDV", , , _
                                                  .Item("PaymentOption"), _
                                                  .Item("SchemeType"), , , _
                                                  .Item("Unit"), , , , , _
                                                  .Item("EmailAddress"), _
                                                  .Item("BirthDay"), _
                                                  .Item("SalesCountry"), _
                                                  .Item("SalesOffice"), , , , , , , , , _
                                                  .Item("PreferredDueDate")) = False Then
                        _dimclsUnitHold.SQLTransaction.Rollback()
                        _dimErr.Add(New ErrorList(1, _dimclsUnitHold.SQLMessage))

                        Return _dimSerial.Serialize(_dimErr)
                    End If
                End With

                ' System generated code
                _dimStrNewCompanyCode = _dimclsUnitHold.CompanyCode
                _dimStrNewContractNumber = _dimclsUnitHold.ContractNumber
                _dimStrNewBusinessPartnerCode = _dimclsUnitHold.ParCode

                ' Save Seller information  
                If _dimDtsData.Tables.Contains("SellerInfo") = True Then
                    ' Multiple Row
                    For Each _dtrData As DataRow In _dimDtsData.Tables("SellerInfo").Rows
                        If _dimclsUnitHold.ExeCuteAko(clsUnitHold.ExecuteCommand.ExecuteNonQuery, _
                                                      _dimstrUserName, _
                                                      TransMode.Mode12SavingSeller, _
                                                      _dimstrAllocation, , , , , , , , _
                                                      _dimStrNewBusinessPartnerCode, , , , , , _
                                                      _dimStrNewCompanyCode, _
                                                      _dimStrNewContractNumber, , _
                                                      _dtrData.Item("AgentCode"), _
                                                      _dtrData.Item("SellerPositionCode"), _
                                                      _dtrData.Item("AgentName"), _
                                                      _dtrData.Item("NetworkStructure")) = False Then
                            _dimclsUnitHold.SQLTransaction.Rollback()
                            _dimErr.Add(New ErrorList(1, _dimclsUnitHold.SQLMessage))

                            Return _dimSerial.Serialize(_dimErr)
                        End If
                    Next
                Else
                    'If hidden/no table it is for local,still insert data but default user name as agent name
                    If _dimclsUnitHold.ExeCuteAko(clsUnitHold.ExecuteCommand.ExecuteNonQuery, _
                                                  _dimstrUserName, _
                                                  TransMode.Mode12SavingSeller, _
                                                  _dimstrAllocation, , , , , , , , _
                                                  _dimStrNewBusinessPartnerCode, , , , , , _
                                                  _dimStrNewCompanyCode, _
                                                  _dimStrNewContractNumber) = False Then
                        _dimclsUnitHold.SQLTransaction.Rollback()
                        _dimErr.Add(New ErrorList(1, _dimclsUnitHold.SQLMessage))

                        Return _dimSerial.Serialize(_dimErr)
                    End If
                End If

                ' Contact Number
                For Each _dtrData As DataRow In _dimDtsData.Tables("ContactInfo").Rows
                    If _dimclsUnitHold.ExeCuteAko(clsUnitHold.ExecuteCommand.ExecuteNonQuery, _
                                                  _dimstrUserName, _
                                                  TransMode.Mode11SavingContactNumber, _
                                                  _dimstrAllocation, , , , , , , , _
                                                  _dimStrNewBusinessPartnerCode, , _
                                                  _dtrData.Item("ContactTypeCode"), _
                                                  _dtrData.Item("ContactValue"), , , _
                                                  _dimStrNewCompanyCode, _
                                                  _dimStrNewContractNumber) = False Then
                        _dimclsUnitHold.SQLTransaction.Rollback()
                        _dimErr.Add(New ErrorList(1, _dimclsUnitHold.SQLMessage))

                        Return _dimSerial.Serialize(_dimErr)
                    End If
                Next

                ' Save Remittance and its images
                If _dimDtsData.Tables.Contains("RemitInfo") = True Then
                    For Each _dtrDataRemit As DataRow In _dimDtsData.Tables("RemitInfo").Rows
                        _dimstrRemittanceReferenceCode = ""

                        ' Save Remittance and get generated code
                        If _dimclsUnitHold.ExeCuteAko(clsUnitHold.ExecuteCommand.ExecuteNonQuery, _
                                                      _dimstrUserName, _
                                                      TransMode.Mode13SavingRemittance, _
                                                      _dimstrAllocation, , , , , , , , _
                                                      _dimStrNewBusinessPartnerCode, , , , , , _
                                                      _dimStrNewCompanyCode, _
                                                      _dimStrNewContractNumber, , , , , , , , , , _
                                                      _dtrDataRemit.Item("ModePaymentCode"), _
                                                      _dtrDataRemit.Item("RemitCompanyName"), _
                                                      _dtrDataRemit.Item("BankCode"), _
                                                      _dtrDataRemit.Item("DatePaid"), _
                                                      _dtrDataRemit.Item("RemitAmount"), ) = False Then
                            _dimclsUnitHold.SQLTransaction.Rollback()
                            _dimErr.Add(New ErrorList(1, _dimclsUnitHold.SQLMessage))

                            Return _dimSerial.Serialize(_dimErr)
                        End If

                        _dimstrRemittanceReferenceCode = _dimclsUnitHold.RemittanceReferenceCode

                        ' Save Images for Remittance RemitImages
                        If _dimDtsData.Tables.Contains("RemitImages") = True Then

                            ' 1 remittance has multiple images 
                            For Each _dtrDataRemitImages As DataRow In _dimDtsData.Tables("RemitImages").Select("RowID = " & _dtrDataRemit.Item("RowID"))
                                '2016-02-09 : set to general error message
                                Try
                                    Using _dimFileStream As New FileStream(_dimstrTempPath + _dtrDataRemitImages.Item("savedname"), FileMode.Open)
                                        _dimreader = New BinaryReader(_dimFileStream)
                                        _dimByte = _dimreader.ReadBytes(_dimFileStream.Length)

                                        If _dimclsUnitHold.ExeCuteAko(clsUnitHold.ExecuteCommand.ExecuteNonQuery, _
                                                                      _dimstrUserName, _
                                                                      TransMode.Mode14SavingRemittanceImages, _
                                                                      _dimstrAllocation, , , , , , , , _
                                                                      _dimStrNewBusinessPartnerCode, , , , , , _
                                                                      _dimStrNewCompanyCode, _
                                                                      _dimStrNewContractNumber, , , , , , , , , , , , , , , _
                                                                      _dimstrRemittanceReferenceCode, _
                                                                      _dimByte, _
                                                                      _dtrDataRemitImages.Item("title")) = False Then
                                            _dimclsUnitHold.SQLTransaction.Rollback()
                                            _dimErr.Add(New ErrorList(1, _dimclsUnitHold.SQLMessage))

                                            Return _dimSerial.Serialize(_dimErr)
                                        End If
                                    End Using
                                Catch ex As Exception
                                    _dimclsUnitHold.SQLTransaction.Rollback()
                                    _dimErr.Add(New ErrorList(1, "Error while saving image : " & _dtrDataRemitImages.Item("title")))

                                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex)

                                    Return _dimSerial.Serialize(_dimErr)
                                Finally
                                    _dimByte = Nothing
                                End Try
                            Next
                        End If
                    Next
                End If

                ' Save Documents
                If _dimDtsData.Tables.Contains("DocInfoImages") = True Then
                    ' 1 documents has multiple images 
                    For Each _dtrDataDocuImages As DataRow In _dimDtsData.Tables("DocInfoImages").Rows
                        '2016-02-09 : set to general error message
                        Try
                            Using _dimFileStream As New FileStream(_dimstrTempPath + _dtrDataDocuImages.Item("savedname"), FileMode.Open)
                                _dimreader = New BinaryReader(_dimFileStream)
                                _dimByte = _dimreader.ReadBytes(_dimFileStream.Length)

                                If _dimclsUnitHold.ExeCuteAko(clsUnitHold.ExecuteCommand.ExecuteNonQuery, _
                                                              _dimstrUserName, _
                                                              TransMode.Mode15SavingDocuments, _
                                                              _dimstrAllocation, , , , , , , , _
                                                              _dimStrNewBusinessPartnerCode, , , , , , _
                                                              _dimStrNewCompanyCode, _
                                                              _dimStrNewContractNumber, , , , , , , , , , , , , , , , _
                                                              _dimByte, _
                                                              _dtrDataDocuImages.Item("title"), , _
                                                              _dtrDataDocuImages.Item("DocumentTypeCode")) = False Then
                                    _dimclsUnitHold.SQLTransaction.Rollback()
                                    _dimErr.Add(New ErrorList(1, _dimclsUnitHold.SQLMessage))

                                    Return _dimSerial.Serialize(_dimErr)
                                End If
                            End Using
                        Catch ex As Exception
                            _dimclsUnitHold.SQLTransaction.Rollback()
                            _dimErr.Add(New ErrorList(1, "Error while saving image : " & _dtrDataDocuImages.Item("title")))

                            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)

                            Return _dimSerial.Serialize(_dimErr)
                        End Try
                    Next
                End If

                ' Save FREBAS Status
                Using _dimsqlFREBASConnection As New SqlConnection(MyMSSQLServer2000FREBASConnectionString)
                    _dimsqlFREBASConnection.Open()
                    _dimclsUnitHoldFREBAS.SQLConnection = _dimsqlFREBASConnection
                    _dimclsUnitHoldFREBAS.SQLTransaction = _dimsqlFREBASConnection.BeginTransaction

                    'use agent code saving in frebas status
                    Dim _dimAgentCode As String = SessionAPI.SelAgentCode '  _dimclsGlobalFunctions.fnDeCrypt(HttpContext.Current.Session("sesSelAgentCode"))
                    Dim _dimstrSellerFullName As String = SessionAPI.FullName ' _dimclsGlobalFunctions.fnDeCrypt(HttpContext.Current.Session("sesSelFullname"))

                    ' Save hold status of main unit   Mode 1 for unit hold
                    Dim _dimStatusRemarks As String
                    Dim _dimstrMainUnit As String

                    With _dimDtsData.Tables("BuyerUnitInfo").Rows(0)
                        _dimStatusRemarks = "Unit Hold tagged at Online Reservation by " & _dimstrSellerFullName & " for " & .Item("LastName").ToString.Trim.ToUpper & ", " & .Item("FirstName").ToString.Trim.ToUpper & " - BDAY : " & .Item("BirthDay").ToString
                        _dimstrMainUnit = .Item("Unit")
                    End With

                    ' Save main unit hold status in frebas
                    If _dimclsUnitHoldFREBAS.UnitHoldSaveFREBAS_Status(clsUnitHold.ExecuteCommand.ExecuteNonQuery, _
                                                                       1, _
                                                                       _dimstrSellerFullName, _
                                                                       _dimAgentCode, _
                                                                       _dimStatusRemarks, _
                                                                       _dimstrMainUnit, _
                                                                       _dimintHoldDaytoHold) = False Then
                        _dimclsUnitHold.SQLTransaction.Rollback()
                        _dimclsUnitHoldFREBAS.SQLTransaction.Rollback()
                        _dimErr.Add(New ErrorList(1, _dimclsUnitHoldFREBAS.SQLMessage))

                        Return _dimSerial.Serialize(_dimErr)
                    End If

                    ' Save hold status of adjoining unit   
                    If _dimDtsData.Tables.Contains("AdjoiningUnit") = True Then
                        For Each _dtrDataUnit As DataRow In _dimDtsData.Tables("AdjoiningUnit").Rows
                            If _dimclsUnitHoldFREBAS.UnitHoldSaveFREBAS_Status(clsUnitHold.ExecuteCommand.ExecuteNonQuery, _
                                                                               1, _
                                                                               _dimstrSellerFullName, _
                                                                               _dimAgentCode, _
                                                                               _dimStatusRemarks, _
                                                                               _dtrDataUnit.Item("ReferenceObject"), _
                                                                               _dimintHoldDaytoHold) = False Then
                                _dimclsUnitHold.SQLTransaction.Rollback()
                                _dimclsUnitHoldFREBAS.SQLTransaction.Rollback()
                                _dimErr.Add(New ErrorList(1, _dimclsUnitHoldFREBAS.SQLMessage))

                                Return _dimSerial.Serialize(_dimErr)
                            End If
                        Next
                    End If

                    _dimclsUnitHold.SQLTransaction.Commit()
                    _dimclsUnitHoldFREBAS.SQLTransaction.Commit()

                    'Push EMail 
                    Dim _dimblnIsErroremail As Boolean = False

                    ' Hold Status Notification
                    If _dimclsUnitHold.ExeCuteAko(clsUnitHold.ExecuteCommand.ExecuteNonQuery, _
                                                  _dimstrUserName, _
                                                  TransMode.Mode17PushemailSuccessHolding, _
                                                  _dimstrAllocation, , , , , , , , , , , , , , _
                                                  _dimStrNewCompanyCode, _
                                                  _dimStrNewContractNumber) = False Then
                        _dimblnIsErroremail = True
                    End If

                    Dim _dimstrFinalErrorMessage As String = ""

                    _dimstrFinalErrorMessage = "Successfully Held.<br/> Reservation No.: <b>" & _dimStrNewContractNumber & "</b>"

                    If _dimblnIsErroremail = True Then
                        _dimstrFinalErrorMessage = _dimstrFinalErrorMessage & "<br/><br/><b>Warning : Failed sending email notification</b>"
                    End If

                    ' Get success message, may varries due to allocation 
                    ' Prompt upon loading
                    '2014-06-24 : add new message upon holding
                    If _dimclsUnitHold.ExeCuteAko(clsUnitHold.ExecuteCommand.ExecuteNonQuery, _
                                                  _dimstrUserName, _
                                                  TransMode.Mode19PromptuponSuccessfullyHold, _
                                                  _dimstrAllocation) = False Then
                    End If

                    _dimstrFinalErrorMessage = _dimstrFinalErrorMessage & _dimclsUnitHold.SQLMessage
                    _dimErr.Add(New ErrorList(8888, _dimstrFinalErrorMessage))

                    Return _dimSerial.Serialize(_dimErr)
                End Using
            End Using
        Catch ex As Exception
            If _dimclsUnitHold.SQLTransaction IsNot Nothing Then
                _dimclsUnitHold.SQLTransaction.Rollback()
            End If

            _dimErr.Add(New ErrorList(2, MyExceptionNotice))

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)

            Return _dimSerial.Serialize(_dimErr)
        Finally
            If _dimclsUnitHold IsNot Nothing Then _dimclsUnitHold.Dispose()
            If _dimXmlReader IsNot Nothing Then _dimXmlReader.Dispose()
            If _dimDtsData IsNot Nothing Then _dimDtsData.Dispose()
            If _dimDttDaysofHoldTable IsNot Nothing Then _dimDttDaysofHoldTable.Dispose()
            If _dimclsUnitHoldFREBAS IsNot Nothing Then _dimclsUnitHoldFREBAS.Dispose()

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
        Page.Form.Attributes.Add("enctype", "multipart/form-data")
        Dim _dimclsGlobalFunctions As New clsGlobalFunctions

        SessionAPI.RefreshSessions(CurrentUser)

        If Not _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelRole")).Contains("|unithold.aspx|") Then
            _dimclsGlobalFunctions = Nothing
            Response.Redirect("errorPage.aspx")

            Exit Sub
        End If

        _dimclsGlobalFunctions = Nothing
    End Sub
#End Region
#End Region
End Class
