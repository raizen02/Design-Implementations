'**************************************************************
'Programmer Name		: John Alexander M. Baltazar
'Date Created			: 2014.01.22
'Finished Date          : 2014.01.22
'Program Name           : clsSellerKioskResponsiveSlideMaintenance
'Program Description    : Handles Data Access for Seller Kiosk Responsive Slide Maintenance
'Stored Procedure       : sp_rsSlides
'Remarks                : DEV - 2014.01.22 | PROD - 2014.02.11
'**************************************************************
'Updates Information	: Nestor Garais Jr | (JO# XXXXXXXX)
'Date Updated		    : 2016-06-23
'Changes			    : - Add Implements IDisposable,then collect all global variable for disposal
'                         - Add try/catch on every function then dispose all disposable object on try/finally
'Remarks			    : DEV - 2016-06-23 | PROD - 
'***********************************************************************
Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class clsSliderMaintenance
    Implements IDisposable
#Region "   Declaring Variables   "
    ' Your constant variables here
    Private _priSqlConnection As SqlConnection
    Private _priSqlTransaction As SqlTransaction
    Private _prisdaDataAdapter As SqlDataAdapter
    Private _priintErrorNumber As Integer
    Private _pristrErrorMessage As String
    Private _pricmdMaintenance As SqlCommand

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
    Public Property DataAdapter() As SqlDataAdapter
        Get
            DataAdapter = _prisdaDataAdapter
        End Get

        Set(ByVal value As SqlDataAdapter)
            _prisdaDataAdapter = value
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
#End Region

#Region "   Data Access   "
    ' Your Functions and Procedures for Data Access here.
    Public Function fnExecuteCmd(ByVal _parenuCommandType As ExecuteCommand, _
                                 ByVal _parintmode As Integer, _
                                 ByVal _parstrUserName As String, _
                                 Optional ByRef _parintSlideId As Integer = -1, _
                                 Optional ByVal _parstrSlideName As String = "", _
                                 Optional ByRef _parstrSlideImageFileName As String = "", _
                                 Optional ByRef _parstrURL As String = "", _
                                 Optional ByRef _parintCaptionId As Integer = -1, _
                                 Optional ByRef _parstrCaptionImageFileName As String = "", _
                                 Optional ByVal _parintPositionLeft As Decimal = -1, _
                                 Optional ByVal _parintPositionTop As Decimal = -1, _
                                 Optional ByVal _parintWidth As Decimal = -1, _
                                 Optional ByVal _parintShowDelay As Integer = -1, _
                                 Optional ByVal _parintAnimationId As Integer = -1, _
                                 Optional ByVal _parintPosition As Integer = -1, _
                                 Optional ByVal _parblnShowSlide As Boolean = False, _
                                 Optional ByVal _parstrFileExt As String = "", _
                                 Optional ByVal _parstrSessionId As String = "") As Boolean
        Try
            _pricmdMaintenance = New SqlCommand

            With _pricmdMaintenance
                .Connection = _priSqlConnection
                .Transaction = _priSqlTransaction
                .CommandText = "sp_ppmSlides"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.AddWithValue("@pintMode", _parintmode)
                .Parameters.AddWithValue("@pstrUserName", _parstrUserName)

                .Parameters.Add("@pintSlideId", SqlDbType.Int).Direction = ParameterDirection.InputOutput
                .Parameters("@pintSlideId").Value = GetDbNull(_parintSlideId)

                .Parameters.Add("@pintCaptionId", SqlDbType.Int).Direction = ParameterDirection.InputOutput
                .Parameters("@pintCaptionId").Value = GetDbNull(_parintCaptionId)

                .Parameters.AddWithValue("@pstrSlideName", GetDbNull(_parstrSlideName))
                .Parameters.AddWithValue("@pstrSlideImageFileName", GetDbNull(_parstrSlideImageFileName))
                .Parameters.AddWithValue("@pstrURL", GetDbNull(_parstrURL))
                .Parameters.AddWithValue("@pstrCaptionImageFileName", GetDbNull(_parstrCaptionImageFileName))
                .Parameters.AddWithValue("@pintPositionLeft", GetDbNull(_parintPositionLeft))
                .Parameters.AddWithValue("@pintPositionTop", GetDbNull(_parintPositionTop))
                .Parameters.AddWithValue("@pintWidth", GetDbNull(_parintWidth))
                .Parameters.AddWithValue("@pintShowDelay", GetDbNull(_parintShowDelay))
                .Parameters.AddWithValue("@pintAnimationId", GetDbNull(_parintAnimationId))
                .Parameters.AddWithValue("@pintPosition", GetDbNull(_parintPosition))
                .Parameters.AddWithValue("@pstrSessionId", GetDbNull(_parstrSessionId))
                .Parameters.AddWithValue("@pblnShowSlide", GetDbNull(_parblnShowSlide))
                .Parameters.AddWithValue("@pstrFileExt", GetDbNull(_parstrFileExt))

                .Parameters.Add("@pintErrorNumber", SqlDbType.SmallInt).Direction = ParameterDirection.Output
                .Parameters.Add("@pstrErrorMessage", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output

                If _parenuCommandType = ExecuteCommand.ExecuteNonQuery Then
                    .ExecuteNonQuery()

                    _priintErrorNumber = .Parameters.Item("@pintErrorNumber").Value
                    _pristrErrorMessage = Trim(.Parameters.Item("@pstrErrorMessage").Value)

                    _parintSlideId = GetDbValue(.Parameters.Item("@pintSlideId").Value, GetType(Integer))
                    _parstrSlideImageFileName = GetDbValue(.Parameters.Item("@pstrSlideImageFileName").Value, GetType(String))
                    _parintCaptionId = GetDbValue(.Parameters.Item("@pintCaptionId").Value, GetType(Integer))
                    _parstrCaptionImageFileName = GetDbValue(.Parameters.Item("@pstrCaptionImageFileName").Value, GetType(String))

                    If _priintErrorNumber <> 8888 Then
                        Return False
                    End If
                ElseIf _parenuCommandType = ExecuteCommand.ExecuteDataAdapter Then
                    _prisdaDataAdapter = New SqlDataAdapter(_pricmdMaintenance)
                End If
            End With

            Return True
        Catch ex As Exception
            _pristrErrorMessage = MyExceptionNotice

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)

            Return False
            'Finally
        End Try

    End Function
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
                If _pricmdMaintenance IsNot Nothing Then _pricmdMaintenance.Dispose()

                If DataAdapter IsNot Nothing Then DataAdapter.Dispose()
                If SQLConnection IsNot Nothing Then SQLConnection.Dispose()
                If SQLTransaction IsNot Nothing Then SQLTransaction.Dispose()
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
