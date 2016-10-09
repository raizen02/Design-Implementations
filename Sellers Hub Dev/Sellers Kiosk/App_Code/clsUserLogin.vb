'Updates Information	: Nestor Garais Jr | (JO# XXXXXXXX)
'Date Updated		    : 2016-06-23
'Changes			    : - Add Implements IDisposable,then collect all global variable for disposal
'                         - Add try/catch on every function then dispose all disposable object on try/finally
'Remarks			    : DEV - 2016-06-23 | PROD - 
'                                   Solution : Add try/catch and dispose all disposable objects in try/finally
'**************************************************************
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System

Public Class clsUserLogin
    Implements IDisposable

#Region "Declaring Variables"
    Private _priconConnection As SqlConnection
    Private _prisdaDataAdapter As SqlDataAdapter
    Private _pristrErrorMessage As String
#End Region

#Region "Enum"
    Public Enum ExecuteCommand
        ExecuteNonQuery = 1
        ExecuteReader = 2
        ExecuteDataAdapter = 3
    End Enum
#End Region

#Region "Properties and Methods"
    Public Property Connection() As SqlConnection
        Get
            Connection = _priconConnection
        End Get

        Set(ByVal value As SqlConnection)
            _priconConnection = value
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

    Public Property ErrorMessage() As String
        Get
            ErrorMessage = _pristrErrorMessage
        End Get

        Set(ByVal value As String)
            _pristrErrorMessage = value
        End Set
    End Property
#End Region

#Region "Data Access"
    Public Function fnUserLogin(ByVal _parenuCommandType As ExecuteCommand,
                                ByVal _parintMode As Integer,
                                ByVal _parstrUserName As String,
                                ByVal _parstrPassword As String,
                                Optional ByVal _parstrComputerName As String = "",
                                Optional ByVal _parstrIPAddress As String = "",
                                Optional ByVal _parstrApplicationCode As String = "",
                                Optional ByVal _parstrOldPassword As String = "") As Boolean
        Dim _dimcmdUserLogin As New SqlCommand

        Try
            With _dimcmdUserLogin
                .Connection = Connection
                .CommandText = "sp_ssmLogin"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0
                .Parameters.AddWithValue("@pintMode", _parintMode)
                .Parameters.AddWithValue("@pstrUserName", _parstrUserName)
                .Parameters.AddWithValue("@pstrPassword", Trim(_parstrPassword))
                .Parameters.AddWithValue("@pstrComputerName", Trim(_parstrComputerName))
                .Parameters.AddWithValue("@pstrIPAddress", Trim(_parstrIPAddress))
                .Parameters.AddWithValue("@pstrApplicationCode", Trim(_parstrApplicationCode))
                .Parameters.AddWithValue("@pstrOldPassword", Trim(_parstrOldPassword))

                .Parameters.Add("@pintErrorNumber", SqlDbType.Int, 4).Direction = ParameterDirection.Output
                .Parameters.Add("@pstrErrorMessage", SqlDbType.NVarChar, 200).Direction = ParameterDirection.Output

                If _parenuCommandType = ExecuteCommand.ExecuteNonQuery Then
                    .ExecuteNonQuery()

                    _pristrErrorMessage = .Parameters.Item("@pstrErrorMessage").Value.ToString

                    If .Parameters.Item("@pintErrorNumber").Value <> 8888 Then
                        Return False
                    End If
                ElseIf _parenuCommandType = ExecuteCommand.ExecuteDataAdapter Then
                    _prisdaDataAdapter = New SqlDataAdapter
                    _prisdaDataAdapter.SelectCommand = _dimcmdUserLogin
                End If
            End With

            Return True
        Catch ex As Exception
            _pristrErrorMessage = MyExceptionNotice

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)

            Return False
        Finally
            If _dimcmdUserLogin IsNot Nothing Then _dimcmdUserLogin.Dispose()
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
                If _prisdaDataAdapter IsNot Nothing Then _prisdaDataAdapter.Dispose()
                If _priconConnection IsNot Nothing Then _priconConnection.Dispose()

                If Connection IsNot Nothing Then Connection.Dispose()
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
