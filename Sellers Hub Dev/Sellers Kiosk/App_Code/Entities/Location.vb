Imports Microsoft.VisualBasic
Namespace Sellers.Entities


    Public Class Location
        Private _LocationId As Int32
        Public Property LocationId() As Int32
            Get
                Return _LocationId
            End Get
            Set(ByVal value As Int32)
                _LocationId = value
            End Set
        End Property
        Private _LocationName As String
        Public Property LocationName() As String
            Get
                Return _LocationName
            End Get
            Set(ByVal value As String)
                _LocationName = value
            End Set
        End Property
    End Class
End Namespace