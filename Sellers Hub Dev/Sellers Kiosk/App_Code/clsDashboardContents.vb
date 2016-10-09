'**************************************************************
'Programmer Name		: John Alexander M. Baltazar
'Date Created			: 2014.02.07
'Finished Date          : 2014.02.07
'Program Name           : clsDashboardContents
'Program Description    : Handles Data Access for Dashboard contents
'Stored Procedure       : sp_rsSlides
'**************************************************************
'Updates Information	: Nestor Garais Jr | (JO# XXXXXXXX)
'Date Updated		    : 2016-06-23
'Changes			    : - Add Implements IDisposable,then collect all global variable for disposal
'                         - Add try/catch on every function then dispose all disposable object on try/finally
'Remarks			    : DEV - 2016-06-23 | PROD - 
'***********************************************************************

Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System

Public Class clsDashboardContents
    Implements IDisposable

#Region "   Declaring Variables   "
    ' Your constant variables here
    Private _priSqlConnection As SqlConnection
    Private _priSqlTransaction As SqlTransaction
    Private _prisdaDataAdapter As SqlDataAdapter
    Private _priintErrorNumber As Integer
    Private _pristrErrorMessage As String

#Region "Enum"
    ' Place your Enum here
    Public Enum ExecuteCommand
        ExecuteNonQuery = 1
        ExecuteReader = 2
        ExecuteDataAdapter = 3
    End Enum
#End Region

#End Region

#Region "   Properties   "
    ' Place your Properties here    
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

    Public Property SQLConnection() As SqlClient.SqlConnection
        Get
            Return _priSqlConnection
        End Get

        Set(ByVal value As SqlClient.SqlConnection)
            _priSqlConnection = value
        End Set
    End Property

    Public Property SQLTransaction() As SqlClient.SqlTransaction
        Get
            Return _priSqlTransaction
        End Get

        Set(ByVal value As SqlClient.SqlTransaction)
            _priSqlTransaction = value
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

#Region "   Data Access   "
    ' Your Functions and Procedures for Data Access here.
    Public Function fnDashboardContents(ByVal _parenuCommandType As ExecuteCommand, _
                                        ByVal _parintmode As Integer, _
                                        ByVal _parstrUserName As String, _
                                        Optional ByVal _parintArticleId As Integer = 0) As Boolean
        fnDashboardContents = False

        Dim _dimsqlCommand As New SqlCommand

        Try
            With _dimsqlCommand
                .Connection = _priSqlConnection
                .Transaction = _priSqlTransaction
                .CommandText = "sp_ppmDashboardContentNew"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.AddWithValue("@pintMode", _parintmode)
                .Parameters.AddWithValue("@pintArticleId", _parintArticleId)
                .Parameters.AddWithValue("@pstrUserName", _parstrUserName)

                .Parameters.Add("@pintErrorNumber", SqlDbType.SmallInt).Direction = ParameterDirection.Output
                .Parameters.Add("@pstrErrorMessage", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output

                If _parenuCommandType = ExecuteCommand.ExecuteNonQuery Then
                    .ExecuteNonQuery()

                    _priintErrorNumber = .Parameters.Item("@pintErrorNumber").Value
                    _pristrErrorMessage = Trim(.Parameters.Item("@pstrErrorMessage").Value)

                    If _priintErrorNumber <> 8888 Then
                        Return False
                    End If
                ElseIf _parenuCommandType = ExecuteCommand.ExecuteDataAdapter Then
                    _prisdaDataAdapter = New SqlDataAdapter(_dimsqlCommand)
                End If
            End With

            fnDashboardContents = True
        Catch ex As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)

            Return False
        Finally
            _dimsqlCommand.Dispose()
            _dimsqlCommand = Nothing
        End Try
    End Function

    'Public Function fnExecuteCmd( _
    '       ByVal _parenuCommandType As ExecuteCommand, _
    '       ByVal _parintmode As Integer, _
    '       ByVal _parstrUserName As String, _
    '       Optional ByVal _parintArticleId As Integer = 0 _
    '       ) As Boolean

    '    fnExecuteCmd = False
    '    Dim _dimsqlCommand As New SqlCommand

    '    Try
    '        With _dimsqlCommand
    '            .Connection = _priSqlConnection
    '            .Transaction = _priSqlTransaction
    '            .CommandText = "sp_ppmDashboardContent"
    '            .CommandType = CommandType.StoredProcedure
    '            .CommandTimeout = 0

    '            .Parameters.AddWithValue("@pintMode", _parintmode)
    '            .Parameters.AddWithValue("@pintArticleId", _parintArticleId)
    '            .Parameters.AddWithValue("@pstrUserName", _parstrUserName)

    '            .Parameters.Add("@pintErrorNumber", SqlDbType.SmallInt).Direction = ParameterDirection.Output
    '            .Parameters.Add("@pstrErrorMessage", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output

    '            If _parenuCommandType = ExecuteCommand.ExecuteNonQuery Then
    '                .ExecuteNonQuery()

    '                _priintErrorNumber = .Parameters.Item("@pintErrorNumber").Value
    '                _pristrErrorMessage = Trim(.Parameters.Item("@pstrErrorMessage").Value)

    '                If _priintErrorNumber <> 8888 Then
    '                    Return False
    '                End If
    '            ElseIf _parenuCommandType = ExecuteCommand.ExecuteDataAdapter Then
    '                _prisdaDataAdapter = New SqlDataAdapter(_dimsqlCommand)
    '            End If

    '        End With

    '        fnExecuteCmd = True
    '    Catch ex As Exception
    '        If _dimsqlCommand.Parameters.Item("@pstrErrorMessage") IsNot Nothing Then
    '            _pristrErrorMessage = _dimsqlCommand.Parameters.Item("@pstrErrorMessage").Value & vbCrLf
    '        End If

    '        _pristrErrorMessage &= "Details:" & vbCrLf & ex.Message.ToString

    '        Throw New Exception(_pristrErrorMessage)
    '    Finally
    '        _dimsqlCommand.Dispose()

    '    End Try

    'End Function
#End Region

#Region "   User Define Subs and Functions   "
#Region "Procedures"
    ' Place your Sub here

#End Region

#Region "Functions"
    ' Place your Functions here
    Private Function GetDbNull(ByVal Field As String) As Object
        If Field Is Nothing Then
            Return DBNull.Value
        End If

        If Field = "" Then
            Return DBNull.Value
        End If

        Return Field
    End Function

    Private Function GetDbNull(ByVal Field As Date) As Object
        If Field = #12:00:00 AM# Then
            Return DBNull.Value
        End If

        If Field = #12/31/9999# Then
            Return DBNull.Value
        End If

        Return Field
    End Function

    Private Function GetDbNull(ByVal Field As Integer) As Object
        If Field = -1 Then
            Return DBNull.Value
        End If

        Return Field
    End Function

    Private Function GetDbNull(ByVal Field As Byte()) As Object
        If Field.Length = 0 Then
            Return DBNull.Value
        End If

        Return Field
    End Function

    Private Function GetDbNull(ByVal Field As Object) As Object
        If Field Is Nothing Then
            Return DBNull.Value
        End If

        Return Field
    End Function

    Private Function GetDbValue(ByVal value As Object, ByVal DataType As Type) As Object
        If IsDBNull(value) = False Then
            Return value
        End If

        If DataType Is GetType(Integer) Then
            Return -1
        ElseIf DataType Is GetType(String) Then
            Return ""
        ElseIf DataType Is GetType(DateTime) Then
            Return #12/31/9999#
        End If

        Return Nothing
    End Function
#End Region
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                If _priSqlConnection IsNot Nothing Then _priSqlConnection.Dispose()
                If _priSqlTransaction IsNot Nothing Then _priSqlTransaction.Dispose()
                If _prisdaDataAdapter IsNot Nothing Then _prisdaDataAdapter.Dispose()

                If SQLConnection IsNot Nothing Then SQLConnection.Dispose()
                If SQLTransaction IsNot Nothing Then SQLTransaction.Dispose()
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
