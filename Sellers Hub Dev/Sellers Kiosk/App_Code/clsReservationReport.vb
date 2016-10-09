'Created By      : Malvin V. Reyes
'Updated Date    : July 22, 2010
'Description     : Crystal Report to PDF Loader 

Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System

Public Class clsReservationReport
    Implements IDisposable
#Region "Declaring Variables"
    Private _priintErrorNumber As Integer
    Private _pristrErrorMessage As String
    Private _priconConnection As SqlConnection
    Private _pritraTransaction As SqlTransaction
    Private _prisdaDataAdapter As SqlDataAdapter
#End Region

#Region "Enum"
    Public Enum ExecuteCommand
        ExecuteNonQuery = 1
        ExecuteReader = 2
        ExecuteDataAdapter = 3
    End Enum
#End Region

#Region "Properties and Methods"
    Public Property ErrorNumber() As Integer
        Get
            ErrorNumber = _priintErrorNumber
        End Get

        Set(ByVal value As Integer)
            _priintErrorNumber = value
        End Set
    End Property

    Public Property ErrorMessage() As String
        Get
            ErrorMessage = _pristrErrorMessage
        End Get

        Set(ByVal value As String)
            _pristrErrorMessage = value
        End Set
    End Property

    Public Property Connection() As SqlConnection
        Get
            Connection = _priconConnection
        End Get

        Set(ByVal value As SqlConnection)
            _priconConnection = value
        End Set
    End Property

    Public Property Transaction() As SqlTransaction
        Get
            Transaction = _pritraTransaction
        End Get

        Set(ByVal value As SqlTransaction)
            _pritraTransaction = value
        End Set
    End Property

    Public Property DataAdapter() As SqlDataAdapter
        Get
            DataAdapter = _prisdaDataAdapter
        End Get

        Set(ByVal value As SqlDataAdapter)
            _prisdaDataAdapter = value
        End Set
    End Property
#End Region

#Region "Data Access"
    Public Function ReportsInfo(ByVal _parenuCommandType As ExecuteCommand, _
                                ByVal _parintMode As Integer, _
                                ByVal _parstrUserName As String) As Boolean
        Dim _dimcmdReportsInfo As New SqlCommand

        Try
            With _dimcmdReportsInfo
                .Connection = Connection
                .Transaction = Transaction
                .CommandText = "sp_ppmrptOnlineReservationReport"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.AddWithValue("@pintMode", _parintMode)
                .Parameters.AddWithValue("@pstrUserName", _parstrUserName)
                .Parameters.Add("@pintErrorNumber", SqlDbType.SmallInt).Direction = ParameterDirection.Output
                .Parameters.Add("@pstrErrorMessage", SqlDbType.NVarChar, 200).Direction = ParameterDirection.Output

                If _parenuCommandType = ExecuteCommand.ExecuteNonQuery Then
                    .ExecuteNonQuery()

                    _priintErrorNumber = .Parameters.Item("@pintErrorNumber").Value
                    _pristrErrorMessage = .Parameters.Item("@pstrErrorMessage").Value.ToString

                    If .Parameters.Item("@pintErrorNumber").Value <> 8888 Then
                        Return False
                    End If
                ElseIf _parenuCommandType = ExecuteCommand.ExecuteDataAdapter Then
                    _prisdaDataAdapter = New SqlDataAdapter
                    _prisdaDataAdapter.SelectCommand = _dimcmdReportsInfo
                End If
            End With

            Return True
        Catch ex As Exception
            _pristrErrorMessage = MyExceptionNotice

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)

            Return False
        Finally
            _dimcmdReportsInfo.Dispose()
            _dimcmdReportsInfo = Nothing
        End Try
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                If _priconConnection IsNot Nothing Then _priconConnection.Dispose()
                If _pritraTransaction IsNot Nothing Then _pritraTransaction.Dispose()
                If _prisdaDataAdapter IsNot Nothing Then _prisdaDataAdapter.Dispose()

                If Connection IsNot Nothing Then Connection.Dispose()
                If Transaction IsNot Nothing Then Transaction.Dispose()
                If DataAdapter IsNot Nothing Then DataAdapter.Dispose()
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
