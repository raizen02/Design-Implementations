Imports Microsoft.VisualBasic
Namespace Sellers.Entities


    Public Class ProjectUnit
        Private _Location As String
        Public Property Location() As String
            Get
                Return _Location
            End Get
            Set(ByVal value As String)
                _Location = value
            End Set
        End Property
        Private _ProjectCode As String
        Public Property ProjectCode() As String
            Get
                Return _ProjectCode
            End Get
            Set(ByVal value As String)
                _ProjectCode = value
            End Set
        End Property

        Private _ProjectName As String
        Public Property ProjectName() As String
            Get
                Return _ProjectName
            End Get
            Set(ByVal value As String)
                _ProjectName = value
            End Set
        End Property

        Private _Phase As String
        Public Property Phase() As String
            Get
                Return _Phase
            End Get
            Set(ByVal value As String)
                _Phase = value
            End Set
        End Property

        Private _Block As String
        Public Property Block() As String
            Get
                Return _Block
            End Get
            Set(ByVal value As String)
                _Block = value
            End Set
        End Property
        Private _UnitName As String
        Public Property UnitName() As String
            Get
                Return _UnitName
            End Get
            Set(ByVal value As String)
                _UnitName = value
            End Set
        End Property

        Private _UnitId As Int32
        Public Property UnitId() As Int32
            Get
                Return _UnitId
            End Get
            Set(ByVal value As Int32)
                _UnitId = value
            End Set
        End Property
    End Class
End Namespace