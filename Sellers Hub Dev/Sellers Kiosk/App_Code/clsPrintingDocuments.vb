'Programmer Name        : Malvin V. Reyes
'Date Created		    : September 4, 2010
'Program Name		    : clsPrintingDocuments
'Program Description	: Printing of Documents in International Website
'                         (with Callback functions and search facility)

'Updates Information	: Nestor Garais Jr | (JO# XXXXXXXX)
'Date Updated		    : 2016-06-23
'Changes			    : - Add Implements IDisposable,then collect all global variable for disposal
'                         - Add try/catch on every function then dispose all disposable object on try/finally
'Remarks			    : DEV - 2016-06-23 | PROD - 
'***********************************************************************
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System

Public Class clsPrintingDocuments
    Implements IDisposable

#Region "Declaring Variables"
    Private _priintErrorNumber As Integer
    Private _pristrErrorMessage As String
    Private _pristrReportDocuCode As String
    Private _priconConnection As SqlConnection
    Private _priconConnectionFREBAS As SqlConnection
    Private _pritraTransaction As SqlTransaction
    Private _pritraTransactionFREBAS As SqlTransaction
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

    Public Property ReportDocuCode() As String
        Get
            ReportDocuCode = _pristrReportDocuCode
        End Get

        Set(ByVal value As String)
            _pristrReportDocuCode = value
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

    Public Property ConnectionFREBAS() As SqlConnection
        Get
            ConnectionFREBAS = _priconConnectionFREBAS
        End Get

        Set(ByVal value As SqlConnection)
            _priconConnectionFREBAS = value
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

    Public Property TransactionFREBAS() As SqlTransaction
        Get
            TransactionFREBAS = _pritraTransactionFREBAS
        End Get

        Set(ByVal value As SqlTransaction)
            _pritraTransactionFREBAS = value
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
    Public Function PrintingDocuments(ByVal _parenuCommandType As ExecuteCommand,
                                      ByVal _parintMode As Integer,
                                      ByVal _parstrUserName As String,
                                      Optional ByVal _parstrCompanyCode As String = "",
                                      Optional ByVal _parstrContractNumber As String = "",
                                      Optional ByVal _parstrCustomerName As String = "",
                                      Optional ByVal _parstrReportFileName As String = "") As Boolean
        Dim _dimcmdPrintingDocuments As New SqlCommand

        Try
            With _dimcmdPrintingDocuments
                .Connection = Connection
                .Transaction = Transaction
                .CommandText = "sp_selPrintDocumentsNew"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.AddWithValue("@pintMode", _parintMode)
                .Parameters.AddWithValue("@pstrUserName", _parstrUserName)
                .Parameters.AddWithValue("@pstrCompanyCode", Trim(_parstrCompanyCode))
                .Parameters.AddWithValue("@pstrContractNumber", Trim(_parstrContractNumber))
                .Parameters.AddWithValue("@pstrCustomerName", Trim(_parstrCustomerName))
                .Parameters.AddWithValue("@pstrReportFileName", Trim(_parstrReportFileName))
                .Parameters.Add("@pstrReportDocuCode", SqlDbType.NVarChar, 4).Direction = ParameterDirection.Output
                .Parameters.Add("@pintErrorNumber", SqlDbType.SmallInt).Direction = ParameterDirection.Output
                .Parameters.Add("@pstrErrorMessage", SqlDbType.NVarChar, 200).Direction = ParameterDirection.Output

                If _parenuCommandType = ExecuteCommand.ExecuteNonQuery Then
                    .ExecuteNonQuery()

                    _pristrReportDocuCode = Trim(.Parameters.Item("@pstrReportDocuCode").Value)
                    _priintErrorNumber = .Parameters.Item("@pintErrorNumber").Value
                    _pristrErrorMessage = Trim(.Parameters.Item("@pstrErrorMessage").Value)

                    If .Parameters.Item("@pintErrorNumber").Value <> 8888 Then
                        Return False
                    End If
                ElseIf _parenuCommandType = ExecuteCommand.ExecuteDataAdapter Then
                    _prisdaDataAdapter = New SqlDataAdapter
                    _prisdaDataAdapter.SelectCommand = _dimcmdPrintingDocuments
                End If
            End With

            Return True
        Catch ex As Exception
            _pristrErrorMessage = MyExceptionNotice

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)

            Return False
        Finally
            _dimcmdPrintingDocuments.Dispose()
            _dimcmdPrintingDocuments = Nothing
        End Try
    End Function

    Public Function PrintingDocumentsSaveDispatch(ByVal _parenuCommandType As ExecuteCommand,
                                                  ByVal _parintMode As Integer,
                                                  ByVal _parstrUserName As String,
                                                  ByVal _parstrCompanyCode As String,
                                                  ByVal _parstrContractNumber As String,
                                                  ByVal _parstrReportDocuCode As String) As Boolean
        Dim _dimcmdPrintingDocumentsSaveDispatch As New SqlCommand

        Try
            With _dimcmdPrintingDocumentsSaveDispatch
                .Connection = ConnectionFREBAS
                .Transaction = TransactionFREBAS
                .CommandText = "SP_ssmINTLDocumentPrintDispatchedTagged"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.AddWithValue("@pintMode", _parintMode)
                .Parameters.AddWithValue("@pstrUserName", _parstrUserName)
                .Parameters.AddWithValue("@pstrCompanyCode", Trim(_parstrCompanyCode))
                .Parameters.AddWithValue("@pstrContractNumber", Trim(_parstrContractNumber))
                .Parameters.AddWithValue("@pstrReportDocuCode", Trim(_parstrReportDocuCode))
                .Parameters.Add("@pintErrorNumber", SqlDbType.SmallInt).Direction = ParameterDirection.Output
                .Parameters.Add("@pstrErrorMessage", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output

                If _parenuCommandType = ExecuteCommand.ExecuteNonQuery Then
                    .ExecuteNonQuery()

                    _priintErrorNumber = .Parameters.Item("@pintErrorNumber").Value
                    _pristrErrorMessage = .Parameters.Item("@pstrErrorMessage").Value.ToString

                    If .Parameters.Item("@pintErrorNumber").Value <> 8888 Then
                        Return False
                    End If
                ElseIf _parenuCommandType = ExecuteCommand.ExecuteDataAdapter Then
                    _prisdaDataAdapter = New SqlDataAdapter
                    _prisdaDataAdapter.SelectCommand = _dimcmdPrintingDocumentsSaveDispatch
                End If
            End With

            Return True
        Catch ex As Exception
            _pristrErrorMessage = MyExceptionNotice

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)

            Return False
        Finally
            _dimcmdPrintingDocumentsSaveDispatch.Dispose()
            _dimcmdPrintingDocumentsSaveDispatch = Nothing
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
                If _priconConnectionFREBAS IsNot Nothing Then _priconConnectionFREBAS.Dispose()
                If _pritraTransaction IsNot Nothing Then _pritraTransaction.Dispose()
                If _pritraTransactionFREBAS IsNot Nothing Then _pritraTransactionFREBAS.Dispose()
                If _prisdaDataAdapter IsNot Nothing Then _prisdaDataAdapter.Dispose()

                If Connection IsNot Nothing Then Connection.Dispose()
                If ConnectionFREBAS IsNot Nothing Then ConnectionFREBAS.Dispose()
                If Transaction IsNot Nothing Then Transaction.Dispose()
                If TransactionFREBAS IsNot Nothing Then TransactionFREBAS.Dispose()
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
