Imports Microsoft.VisualBasic

Public Class Block
    Private _BockId As Int32
    Public Property BlockId() As Int32
        Get
            Return _BockId
        End Get
        Set(ByVal value As Int32)
            _BockId = value
        End Set
    End Property

    Private _BlockName As String
    Public Property BlockName() As String
        Get
            Return _BlockName
        End Get
        Set(ByVal value As String)
            _BlockName = value
        End Set
    End Property

End Class
