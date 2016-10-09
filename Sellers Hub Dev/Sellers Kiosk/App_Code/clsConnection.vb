'***********************************************************************
'               *   Nestor Garais Jr| 2016-06-23 | (JO# XXXXXXXX)
'                   DEV - 2016-06-23  | PROD  
'	                - Fix warning : 
'                       CA1001 : Microsoft.Design : Implement IDisposable on 'clsCommissReport' because it creates members of the following IDisposable types: 'SqlDataAdapter'. If 'clsCommissKiosk' has previously shipped, adding new members that implement IDisposable to this type is considered a breaking change to existing consumers.
'                     Solution: Implements IDisposable 
'***********************************************************************

Imports Microsoft.VisualBasic
Imports System.Data
Imports System

Public Class clsConnection
    Implements IDisposable
#Region "Declaring Variables"
    Private _priconMSSQLServer2000Connection As New SqlClient.SqlConnection
    Private _priconOleDbServerConnection As New OleDb.OleDbConnection
#End Region

#Region "Enum"
    Public Enum Database
        MSSQLServer2000 = 1
        OleDbServer = 2
    End Enum
#End Region

#Region "Properties and Methods"
    Public Property MSSQLServer2000Connection() As SqlClient.SqlConnection
        Get
            MSSQLServer2000Connection = _priconMSSQLServer2000Connection
        End Get

        Set(ByVal value As SqlClient.SqlConnection)
            _priconMSSQLServer2000Connection = value
        End Set
    End Property

    Public Property OleDbServerConnection() As OleDb.OleDbConnection
        Get
            OleDbServerConnection = _priconOleDbServerConnection
        End Get

        Set(ByVal value As OleDb.OleDbConnection)
            _priconOleDbServerConnection = value
        End Set
    End Property
#End Region

#Region "Data Access"
    Public Function OpenConnection(ByVal _parenuDatabaseType As Database, ByVal _parstrConnectionString As String) As Boolean
        Try
            Select Case _parenuDatabaseType
                Case Database.MSSQLServer2000
                    If MSSQLServer2000Connection.State = ConnectionState.Open Then MSSQLServer2000Connection.Close()
                    MSSQLServer2000Connection = New SqlClient.SqlConnection
                    MSSQLServer2000Connection.ConnectionString = _parstrConnectionString
                    MSSQLServer2000Connection.Open()

                Case Database.OleDbServer
                    If OleDbServerConnection.State = ConnectionState.Open Then MSSQLServer2000Connection.Close()
                    OleDbServerConnection = New OleDb.OleDbConnection
                    OleDbServerConnection.ConnectionString = _parstrConnectionString
                    OleDbServerConnection.Open()
            End Select

            Return True
        Catch ex As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)

            Return False
        End Try
    End Function

    Public Sub CloseConnection(ByVal _parenuDatabaseType As Database)
        'Select Case _parenuDatabaseType
        '    Case Database.MSSQLServer2000
        '        MSSQLServer2000Connection.Close()
        '        'MSSQLServer2000Connection = Nothing
        '    Case Database.OleDbServer
        '        OleDbServerConnection.Close()
        '        'OleDbServerConnection = Nothing
        'End Select
    End Sub
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                If _priconMSSQLServer2000Connection IsNot Nothing Then _priconMSSQLServer2000Connection.Dispose()
                If _priconOleDbServerConnection IsNot Nothing Then _priconOleDbServerConnection.Dispose()
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
