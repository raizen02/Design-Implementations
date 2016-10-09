
'***********************************************************************
'Programmer Name        : Nestor S. Garais Jr
'Date Created           : 2013-11-25 
'Program Name           : clsOnlineUnitHold 
'Program Description    : Online Hold with remit and upload file
'Form Name              :  

'Updates Information	: Nestor Garais Jr | (JO# XXXXXXXX)
'Date Updated		    : 2013-12-19
'Changes			    : add parameter UnitHoldSaveFREBAS_Status() @pintDaysToHold 
'Remarks			    : DEV - 2013-12-19 | PROD - YYYY-MM-DD


'Updates Information	: Nestor Garais Jr | (JO# XXXXXXXX)
'Date Updated		    : 2014-06-25
'Changes			    :  set @pstrErrorMessage length size to 2000
'Remarks			    : DEV - 2014-06-25| PROD - YYYY-MM-DD

'Updates Information	: Nestor Garais Jr | (JO# XXXXXXXX)
'Date Updated		    : 2015-07-13
'Changes			    : Remove sql parameter : pstrFinancingTypeCode, pstrAdjoiningUnit,pstrHouseModel,pstrSchemeType,pstrEAMNumber,pbitIsEAM
'Remarks			    : DEV - 2015-07-13 | PROD - 2016-05-10

'Updates Information	: Nestor Garais Jr | (JO# XXXXXXXX)
'Date Updated		    : 2016-06-23
'Changes			    : - Add Implements IDisposable,then collect all global variable for disposal
'                         - Add try/catch on every function then dispose all disposable object on try/finally
'Remarks			    : DEV - 2016-06-23 | PROD - 
'***********************************************************************

Imports System.Data.SqlClient
Imports System.Data
Imports System
'Imports System.String

Public Class clsUnitHold
    Implements IDisposable

#Region "Declaring Variables"
    ' Your constant variables here
    Private _prisqlConnection As SqlConnection
    Private _prisqlTransaction As SqlTransaction
    Private _prisdaDataAdapter As SqlDataAdapter
    Private _prisqlDataReader As SqlDataReader
    Private _pricmdMaintenance As SqlCommand

    Private _pristrSqlMessage As String = ""
    Private _pristrParCode As String = ""
    Private _pristrRemittanceReferenceCode As String = ""
    Private _pristrCompanyCode As String = ""
    Private _pristrContractNumber As String = ""

#Region "Enum"
    ' Place your Enum here
    Public Enum ExecuteCommand
        ExecuteNonQuery = 1
        ExecuteReader = 2
        ExecuteDataAdapter = 3
    End Enum
#End Region
#End Region

#Region "Properties and Methods"
#Region "Properties"
    ' Place your Properties here
    Public ReadOnly Property SQLMessage() As String
        Get
            Return _pristrSqlMessage
        End Get
    End Property

    Public Property SQLConnection() As SqlConnection
        Get
            Return _prisqlConnection
        End Get

        Set(ByVal value As SqlConnection)
            _prisqlConnection = value
        End Set
    End Property

    Public Property SQLTransaction() As SqlTransaction
        Get
            Return _prisqlTransaction
        End Get

        Set(ByVal value As SqlTransaction)
            _prisqlTransaction = value
        End Set
    End Property

    Public Property SQLDataAdapter() As SqlDataAdapter
        Get
            Return _prisdaDataAdapter
        End Get

        Set(ByVal value As SqlDataAdapter)
            _prisdaDataAdapter = value
        End Set
    End Property

    Public Property SQLDataReader() As SqlDataReader
        Get
            Return _prisqlDataReader
        End Get

        Set(ByVal value As SqlDataReader)
            _prisqlDataReader = value
        End Set
    End Property

    Public ReadOnly Property ParCode() As String
        Get
            Return _pristrParCode
        End Get
    End Property

    Public ReadOnly Property RemittanceReferenceCode() As String
        Get
            Return _pristrRemittanceReferenceCode
        End Get
    End Property

    Public ReadOnly Property CompanyCode() As String
        Get
            Return _pristrCompanyCode
        End Get
    End Property

    Public ReadOnly Property ContractNumber() As String
        Get
            Return _pristrContractNumber
        End Get
    End Property
#End Region

#Region "Methods"
    ' Place your Methods here

#End Region
#End Region

#Region "Data Access"
    ' Your Functions and Procedures for Data Access here.
    Public Function ExeCuteAko(ByVal _parenuCommandType As ExecuteCommand, _
                               ByVal _pstrUsername As String, _
                               ByVal _parintMode As Integer, _
                               Optional ByVal _parstrStatusAllocationCode As String = "", _
                               Optional ByVal _parstrLastName As String = "", _
                               Optional ByVal _parstrFirstName As String = "", _
                               Optional ByVal _parstrMiddleName As String = "", _
                               Optional ByVal _parstrProjectCode As String = "", _
                               Optional ByVal _parstrPhaseBuildingCode As String = "", _
                               Optional ByVal _parstrBlockFloorCluster As String = "", _
                               Optional ByVal _parstrLotUnitShareNumber As String = "", _
                               Optional ByVal _parstrParCode As String = "", _
                               Optional ByVal _parstrParType As String = "", _
                               Optional ByVal _parstrContactTypeCode As String = "", _
                               Optional ByVal _parstrContactDetails As String = "", _
                               Optional ByVal _parstrContractTypeCode As String = "", _
                               Optional ByVal _parstrSchemeType As String = "", _
                               Optional ByVal _parstrCompanyCode As String = "", _
                               Optional ByVal _parstrContractNumber As String = "", _
                               Optional ByVal _parstrReferenceObject As String = "", _
                               Optional ByVal _parstrSellerAgentCode As String = "", _
                               Optional ByVal _parintSellerPositionID As Integer = 0, _
                               Optional ByVal _parstrSellerName As String = "", _
                               Optional ByVal _parstrSellerNetworkStructure As String = "", _
                               Optional ByVal _parstrEmailAddress As String = "", _
                               Optional ByVal _pardteBirthDate As String = "", _
                               Optional ByVal _parstrSalesCountryCode As String = "", _
                               Optional ByVal _parstrSalesOfficeCode As String = "", _
                               Optional ByVal _parstrModeofPayment As String = "", _
                               Optional ByVal _parstrCompanyName As String = "", _
                               Optional ByVal _parstrBankName As String = "", _
                               Optional ByVal _pardteRemittanceDate As Date = #12/31/9999#, _
                               Optional ByVal _parmonAmountPaid As Decimal = 0, _
                               Optional ByVal _parstrRemittanceReferenceCode As String = "", _
                               Optional ByVal _parimgImageFile As Byte() = Nothing, _
                               Optional ByVal _parstrFileName As String = "", _
                               Optional ByVal _parstrPreferredDueDate As String = "", _
                               Optional ByVal _parstrDocumentType As String = "") As Boolean
        _pricmdMaintenance = New SqlCommand

        Try
            With _pricmdMaintenance
                .Connection = _prisqlConnection
                .Transaction = _prisqlTransaction
                .CommandText = "SP_selOnlineUnitHoldRemitAdvResv_V2"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                With .Parameters
                    .AddWithValue("@pintMode", _parintMode)
                    .AddWithValue("@pstrLastName", _parstrLastName)
                    .AddWithValue("@pstrFirstName", _parstrFirstName)
                    .AddWithValue("@pstrMiddleName", _parstrMiddleName)
                    .AddWithValue("@pstrProjectCode", _parstrProjectCode)
                    .AddWithValue("@pstrPhaseBuildingCode", _parstrPhaseBuildingCode)
                    .AddWithValue("@pstrBlockFloorCluster", _parstrBlockFloorCluster)
                    .AddWithValue("@pstrLotUnitShareNumber", _parstrLotUnitShareNumber)
                    .AddWithValue("@pstrParType", _parstrParType)
                    .AddWithValue("@pstrContactTypeCode", _parstrContactTypeCode)
                    .AddWithValue("@pstrContactDetails", _parstrContactDetails)
                    .AddWithValue("@pstrContractTypeCode", _parstrContractTypeCode)
                    .AddWithValue("@pstrReferenceObject", _parstrReferenceObject)
                    .AddWithValue("@pstrSellerAgentCode", _parstrSellerAgentCode)
                    .AddWithValue("@pintSellerPositionID", _parintSellerPositionID)
                    .AddWithValue("@pstrSellerName", _parstrSellerName)
                    .AddWithValue("@pstrSellerNetworkStructure", _parstrSellerNetworkStructure)
                    .AddWithValue("@pstrEmailAddress", _parstrEmailAddress)
                    .AddWithValue("@pdteBirthDate", _pardteBirthDate)
                    .AddWithValue("@pstrSchemeType", _parstrSchemeType)
                    .AddWithValue("@pstrSalesCountryCode", _parstrSalesCountryCode)
                    .AddWithValue("@pstrSalesOfficeCode", _parstrSalesOfficeCode)
                    .AddWithValue("@pstrModeofPayment", _parstrModeofPayment)
                    .AddWithValue("@pstrCompanyName", _parstrCompanyName)
                    .AddWithValue("@pstrBankName", _parstrBankName)
                    .AddWithValue("@pdteRemittanceDate", _pardteRemittanceDate)
                    .AddWithValue("@pmonAmountPaid", _parmonAmountPaid)

                    If _parimgImageFile IsNot Nothing Then .AddWithValue("@pimgImageFile", _parimgImageFile)

                    .AddWithValue("@pstrFileName", _parstrFileName)
                    .AddWithValue("@pstrPreferredDueDate", _parstrPreferredDueDate)
                    .AddWithValue("@pstrDocumentType", _parstrDocumentType)
                    .AddWithValue("@pstrStatusAllocationCode", _parstrStatusAllocationCode)

                    .Add("@pstrCompanyCode", SqlDbType.NChar, 4).Direction = ParameterDirection.InputOutput
                    .Item("@pstrCompanyCode").Value = _parstrCompanyCode
                    .Add("@pstrContractNumber", SqlDbType.VarChar, 30).Direction = ParameterDirection.InputOutput
                    .Item("@pstrContractNumber").Value = _parstrContractNumber
                    .Add("@pstrParCode", SqlDbType.VarChar, 20).Direction = ParameterDirection.InputOutput
                    .Item("@pstrParCode").Value = _parstrParCode
                    .Add("@pstrRemittanceReferenceCode", SqlDbType.VarChar, 20).Direction = ParameterDirection.InputOutput
                    .Item("@pstrRemittanceReferenceCode").Value = _parstrRemittanceReferenceCode
                    .AddWithValue("@pstrUsername", _pstrUsername)
                    .Add("@pintErrorNumber", SqlDbType.Int, 4).Value = 4444
                    .Item("@pintErrorNumber").Direction = ParameterDirection.Output
                    .Add("@pstrErrorMessage", SqlDbType.VarChar, 2000).Value = " "
                    .Item("@pstrErrorMessage").Direction = ParameterDirection.Output
                End With

                If _parenuCommandType = ExecuteCommand.ExecuteNonQuery Then
                    .ExecuteNonQuery()
                    _pristrSqlMessage = .Parameters.Item("@pstrErrorMessage").Value.ToString

                    If .Parameters.Item("@pintErrorNumber").Value <> 8888 Then
                        Return False
                    End If

                    _pristrRemittanceReferenceCode = .Parameters.Item("@pstrRemittanceReferenceCode").Value.ToString
                    _pristrParCode = .Parameters.Item("@pstrParCode").Value.ToString
                    _pristrContractNumber = .Parameters.Item("@pstrContractNumber").Value.ToString
                    _pristrCompanyCode = .Parameters.Item("@pstrCompanyCode").Value.ToString
                ElseIf _parenuCommandType = ExecuteCommand.ExecuteDataAdapter Then
                    _prisdaDataAdapter = New SqlDataAdapter
                    _prisdaDataAdapter.SelectCommand = _pricmdMaintenance
                ElseIf _parenuCommandType = ExecuteCommand.ExecuteReader Then
                    _prisqlDataReader = .ExecuteReader
                End If
            End With

            Return True
        Catch ex As Exception
            _pristrSqlMessage = MyExceptionNotice

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)

            Return False
        End Try
    End Function

    Public Function UnitHoldSaveFREBAS_Status(ByVal _parenuCommandType As ExecuteCommand, _
                                              ByVal _parintMode As Integer, _
                                              ByVal _parstrUserName As String, _
                                              ByVal _parstrSellerCode As String, _
                                              ByVal _parstrRemarks As String, _
                                              ByVal _parstrReferenceObject As String, _
                                              ByVal _pintDaysToHold As Integer) As Boolean
        Dim _dimcmdUnitHoldSaveFREBAS_Status As New SqlCommand

        Try
            With _dimcmdUnitHoldSaveFREBAS_Status
                .Connection = _prisqlConnection
                .Transaction = _prisqlTransaction
                .CommandText = "SP_immInventoryUnitStatusOnlineReservation"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.AddWithValue("@pintMode", _parintMode)
                .Parameters.AddWithValue("@pstrUserName", _parstrUserName)
                .Parameters.AddWithValue("@pstrSellerCode", Trim(_parstrSellerCode))
                .Parameters.AddWithValue("@pstrRemarks", Trim(_parstrRemarks))
                .Parameters.AddWithValue("@pstrReferenceObject", Trim(_parstrReferenceObject))
                .Parameters.AddWithValue("@pintDaysToHold", _pintDaysToHold)

                .Parameters.Add("@pintErrorNumber", SqlDbType.SmallInt).Direction = ParameterDirection.Output
                .Parameters.Add("@pstrErrorMessage", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output

                If _parenuCommandType = ExecuteCommand.ExecuteNonQuery Then
                    .ExecuteNonQuery()
                    _pristrSqlMessage = .Parameters.Item("@pstrErrorMessage").Value.ToString

                    If .Parameters.Item("@pintErrorNumber").Value <> 8888 Then
                        Return False
                    End If
                ElseIf _parenuCommandType = ExecuteCommand.ExecuteDataAdapter Then
                    _prisdaDataAdapter = New SqlDataAdapter
                    _prisdaDataAdapter.SelectCommand = _dimcmdUnitHoldSaveFREBAS_Status
                End If
            End With

            Return True
        Catch ex As Exception
            _pristrSqlMessage = MyExceptionNotice

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)

            Return False
        Finally
            _dimcmdUnitHoldSaveFREBAS_Status.Dispose()
            _dimcmdUnitHoldSaveFREBAS_Status = Nothing
        End Try
    End Function
#End Region

#Region "     User Define Subs and Functions     "
#Region "Procedures"
    ' Place your Sub here

#End Region

#Region "Functions"
    ' Place your Functions here

#End Region
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                If _prisqlConnection IsNot Nothing Then _prisqlConnection.Dispose()
                If _prisqlTransaction IsNot Nothing Then _prisqlTransaction.Dispose()
                If _prisdaDataAdapter IsNot Nothing Then _prisdaDataAdapter.Dispose()
                If _prisqlDataReader IsNot Nothing Then _prisqlDataReader.Close()
                If _pricmdMaintenance IsNot Nothing Then _pricmdMaintenance.Dispose()

                If SQLConnection IsNot Nothing Then SQLConnection.Dispose()
                If SQLTransaction IsNot Nothing Then SQLTransaction.Dispose()
                If SQLDataAdapter IsNot Nothing Then SQLDataAdapter.Dispose()
                If SQLDataReader IsNot Nothing Then SQLDataReader.Close()
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class



