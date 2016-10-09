
Imports System.Web.Script.Services
Imports System.Collections.Generic
Imports System.Web.Script.Serialization

Partial Class Services
    Inherits System.Web.UI.Page


    <System.Web.Services.WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetCurrentTime(ByVal name As String) As String
        Dim x As New List(Of Project) From
        {New Project("Location1", "Phase1"),
             New Project("Location2", "Phase2"),
             New Project("Location3", "Phase3"),
             New Project("Location4", "Phase4")
        }

        Dim jsonSerialiser As New JavaScriptSerializer
        Dim result As New StringBuilder
        jsonSerialiser.Serialize(x, result)

        Return result.ToString
    End Function
    Private Class Project
        Public Sub New(Loc As String, Phase As String)
            _Location = Loc
            _Phase = Phase
        End Sub
        Private _Location As String
        Public Property Location() As String
            Get
                Return _Location
            End Get
            Set(ByVal value As String)
                _Location = value
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
    End Class

End Class
