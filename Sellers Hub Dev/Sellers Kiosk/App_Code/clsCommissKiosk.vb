
'***********************************************************************
'Programmer Name        : Nestor Garais Jr.
'Date Created           : Aug. 1, 2012
'Program Name           : clsCommissionKiosk
'Program Description    : class / procedures for Commisssion Kiosk 
'Form Name              : comkCommissionKiosk
'
'               *   Nestor Garais Jr| 2012-08-24 | (JO# XXXXXXXX)
'                   DEV - 2012-08-24  | PROD - YYYY-MM-DD
'	                mode 1 : added @pstrSearchField : Column Name , @pstrSearchValue : keyword for searching 
'			                , @pintPageNo : Selected Page no.

'               *   Nestor Garais Jr| 2016-06-22 | (JO# XXXXXXXX)
'                   DEV - 2016-06-22  | PROD  
'	                - Fix warning : 
'                       CA1001 : Microsoft.Design : Implement IDisposable on 'clsCommissKiosk' because it creates members of the following IDisposable types: 'SqlDataAdapter'. If 'clsCommissKiosk' has previously shipped, adding new members that implement IDisposable to this type is considered a breaking change to existing consumers.
'                      Solution : Implements IDisposable ,
'                       CA2000 : Microsoft.Reliability : In method 'clsCommissKiosk.ExeCuteAko(clsCommissKiosk.ExecuteCommand, String, Integer, String, String, String, String, String, String, Integer, Integer)', call System.IDisposable.Dispose on object '_dimcmdMaintenance' before all references to it are out of scope.
'                     Solution : Add try catch then dispose _dimsqlCommand on finally
'***********************************************************************

Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class clsCommissKiosk
    Implements IDisposable

#Region "Declaring Variables"
    ' Your constant variables here
    Private _priSqlConnection As SqlConnection
    Private _priSqlTransaction As SqlTransaction
    Private _prisdaDataAdapter As SqlDataAdapter
    Private _priSqlDataReader As SqlDataReader
    Private _pristrSqlMessage As String = ""

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
            Return _priSqlConnection
        End Get

        Set(ByVal value As SqlConnection)
            _priSqlConnection = value
        End Set
    End Property
    Public Property SQLTransaction() As SqlTransaction
        Get
            Return _priSqlTransaction
        End Get

        Set(ByVal value As SqlTransaction)
            _priSqlTransaction = value
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
            Return _priSqlDataReader
        End Get

        Set(ByVal value As SqlDataReader)
            _priSqlDataReader = value
        End Set
    End Property
#End Region

#Region "Methods"
    ' Place your Methods here

#End Region
#End Region

#Region "Data Access"
    ' Your Functions and Procedures for Data Access here.
    Public Function ExeCuteAko(ByVal _parenuCommandType As ExecuteCommand, _
                               ByVal _parstrUserName As String, _
                               ByVal _parintMode As Integer, _
                               Optional ByVal _parstrCompanyCode As String = "", _
                               Optional ByVal _parstrContractNumber As String = "", _
                               Optional ByVal _parstrAgentCode As String = "", _
                               Optional ByVal _parstrVoucherNum As String = "", _
                               Optional ByVal _parstrSearchField As String = "", _
                               Optional ByVal _parstrSearchValue As String = "", _
                               Optional ByVal _parintPageNo As Integer = 1, _
                               Optional ByVal _parintMaxPageSize As Integer = 10) As Boolean
        ExeCuteAko = False

        Dim _dimcmdMaintenance As New SqlCommand

        Try
            With _dimcmdMaintenance
                .Connection = _priSqlConnection
                .Transaction = _priSqlTransaction
                .CommandText = "SP_portalCommissionKiosk"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                With .Parameters
                    .AddWithValue("@pintMode", _parintMode)
                    .AddWithValue("@pstrCompanyCode", _parstrCompanyCode)
                    .AddWithValue("@pstrContractNumber", _parstrContractNumber)
                    .AddWithValue("@pstrAgentCode", _parstrAgentCode)
                    .AddWithValue("@pstrVoucherNum", _parstrVoucherNum)

                    .AddWithValue("@pstrSearchField", _parstrSearchField)
                    .AddWithValue("@pstrSearchValue", _parstrSearchValue)
                    .AddWithValue("@pintPageNo", _parintPageNo)
                    .AddWithValue("@pintRecordNoToDisplay", _parintMaxPageSize)

                    .AddWithValue("@pstrUserName", _parstrUserName)
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
                ElseIf _parenuCommandType = ExecuteCommand.ExecuteDataAdapter Then
                    _prisdaDataAdapter = New SqlDataAdapter
                    _prisdaDataAdapter.SelectCommand = _dimcmdMaintenance
                ElseIf _parenuCommandType = ExecuteCommand.ExecuteReader Then
                    _priSqlDataReader = .ExecuteReader
                End If
            End With

            ExeCuteAko = True
        Catch ex As Exception
            _pristrSqlMessage = MyExceptionNotice

            ExeCuteAko = False

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
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
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                If _prisdaDataAdapter IsNot Nothing Then _prisdaDataAdapter.Dispose()
                If _priSqlDataReader IsNot Nothing Then _priSqlDataReader.Close()
                If _priSqlTransaction IsNot Nothing Then _priSqlTransaction.Dispose()
                If _priSqlConnection IsNot Nothing Then _priSqlConnection.Dispose()

                If SQLConnection IsNot Nothing Then SQLConnection.Dispose()
                If SQLTransaction IsNot Nothing Then SQLTransaction.Dispose()
                If SQLDataAdapter IsNot Nothing Then SQLDataAdapter.Dispose()
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



