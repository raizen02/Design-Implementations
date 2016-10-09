Imports Microsoft.VisualBasic
Namespace Sellers.Entities
    Public Class Project
        Private _ProjectId As Int32
        Public Property ProjectId() As Int32
            Get
                Return _ProjectId
            End Get
            Set(ByVal value As Int32)
                _ProjectId = value
            End Set
        End Property
    End Class
End Namespace

