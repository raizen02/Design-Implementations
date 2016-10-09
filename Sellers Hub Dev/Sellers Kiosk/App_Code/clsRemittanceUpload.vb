
'***********************************************************************
'Programmer Name        : Nestor S. Garais Jr
'Date Created           : 2014-01-20 
'Program Name           : clsRemittanceUpload 
'Program Description    :  Remittance Re-Upload
'Form Name              :   

'Updates Information	: Nestor Garais Jr | (JO# XXXXXXXX)
'Date Updated		    : 2016-06-23
'Changes			    : - Add Implements IDisposable,then collect all global variable for disposal
'                         - Add try/catch on every function then dispose all disposable object on try/finally
'Remarks			    : DEV - 2016-06-23 | PROD - 
'***********************************************************************

Imports System.Data.SqlClient
Imports System.Data
Imports System

Public Class clsRemittanceUpload
    Implements IDisposable
#Region "Declaring Variables"
    ' Your constant variables here
    Private _prisqlConnection As SqlConnection
    Private _prisqlTransaction As SqlTransaction
    Private _prisdaDataAdapter As SqlDataAdapter
    Private _prisqlDataReader As SqlDataReader
    Private _pristrSqlMessage As String = ""
    Private _pristrRemittanceReferenceCode As String = ""

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

    Public ReadOnly Property RemittanceReferenceCode() As String
        Get
            Return _pristrRemittanceReferenceCode
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
                               Optional ByVal _parstrCompanyCode As String = "", _
                               Optional ByVal _parstrContractNumber As String = "", _
                               Optional ByVal _parstrRemittanceReferenceCode As String = "", _
                               Optional ByVal _parstrModeofPayment As String = "", _
                               Optional ByVal _parstrCompanyName As String = "", _
                               Optional ByVal _parstrBankName As String = "", _
                               Optional ByVal _pardteRemittanceDate As Date = #12/31/9999#, _
                               Optional ByVal _parmonAmountPaid As Decimal = 0, _
                               Optional ByVal _parimgImageFile As Byte() = Nothing, _
                               Optional ByVal _parstrFileName As String = "") As Boolean
        Dim _dimcmdMaintenance As New SqlCommand

        Try
            With _dimcmdMaintenance
                .Connection = _prisqlConnection
                .Transaction = _prisqlTransaction
                .CommandText = "SP_selOnlineRemittanceUpload_v2"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                With .Parameters
                    .AddWithValue("@pintMode", _parintMode)
                    .AddWithValue("@pstrCompanyCode", _parstrCompanyCode)
                    .AddWithValue("@pstrContractNumber", _parstrContractNumber)
                    .AddWithValue("@pstrModeofPayment", _parstrModeofPayment)
                    .AddWithValue("@pstrCompanyName", _parstrCompanyName)
                    .AddWithValue("@pstrBankName", _parstrBankName)
                    .AddWithValue("@pdteRemittanceDate", _pardteRemittanceDate)
                    .AddWithValue("@pmonAmountPaid", _parmonAmountPaid)

                    If _parimgImageFile IsNot Nothing Then .AddWithValue("@pimgImageFile", _parimgImageFile)

                    .AddWithValue("@pstrFileName", _parstrFileName)

                    .Add("@pstrRemittanceReferenceCode", SqlDbType.VarChar, 20).Direction = ParameterDirection.InputOutput
                    .Item("@pstrRemittanceReferenceCode").Value = _parstrRemittanceReferenceCode

                    .AddWithValue("@pstrUsername", _pstrUsername)

                    .Add("@pintErrorNumber", SqlDbType.Int, 4).Value = 4444
                    .Item("@pintErrorNumber").Direction = ParameterDirection.Output
                    .Add("@pstrErrorMessage", SqlDbType.VarChar, 200).Value = " "
                    .Item("@pstrErrorMessage").Direction = ParameterDirection.Output
                End With

                If _parenuCommandType = ExecuteCommand.ExecuteNonQuery Then
                    .ExecuteNonQuery()

                    _pristrSqlMessage = .Parameters.Item("@pstrErrorMessage").Value.ToString

                    If .Parameters.Item("@pintErrorNumber").Value <> 8888 Then
                        Return False
                    End If

                    _pristrRemittanceReferenceCode = .Parameters.Item("@pstrRemittanceReferenceCode").Value.ToString
                ElseIf _parenuCommandType = ExecuteCommand.ExecuteDataAdapter Then
                    _prisdaDataAdapter = New SqlDataAdapter
                    _prisdaDataAdapter.SelectCommand = _dimcmdMaintenance
                ElseIf _parenuCommandType = ExecuteCommand.ExecuteReader Then
                    _prisqlDataReader = .ExecuteReader
                End If
            End With

            Return True
        Catch ex As Exception
            _pristrSqlMessage = MyExceptionNotice

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)

            Return False
        Finally
            _dimcmdMaintenance.Dispose()
            _dimcmdMaintenance = Nothing
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
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                If _prisqlConnection IsNot Nothing Then _prisqlConnection.Dispose()
                If _prisqlTransaction IsNot Nothing Then _prisqlTransaction.Dispose()
                If _prisdaDataAdapter IsNot Nothing Then _prisdaDataAdapter.Dispose()
                If _prisqlDataReader IsNot Nothing Then _prisqlDataReader.Close()

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



