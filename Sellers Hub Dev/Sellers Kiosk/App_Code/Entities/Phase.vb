Imports Microsoft.VisualBasic
Namespace Sellers.Entities

    Public Class Phase
        Private _PhaseId As Int32
        Public Property PhaseId() As Int32
            Get
                Return _PhaseId
            End Get
            Set(ByVal value As Int32)
                _PhaseId = value
            End Set
        End Property

        Private _PhaseName As String
        Public Property PhaseName() As String
            Get
                Return _PhaseName
            End Get
            Set(ByVal value As String)
                _PhaseName = value
            End Set
        End Property
    End Class

End Namespace