Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports Sellers.Entities

Public Class clsSearchFilterMgr
    Private _UserAllowedUnits As List(Of ProjectUnit)
    Private Const strUserAllowedUnits As String = "UserAllowedUnits"
    Public Sub New(filter As UnitSearchFilter)
        GetUserAllowedUnits(filter)
    End Sub
    Public ReadOnly Property UserAllowedUnits() As List(Of ProjectUnit)
        Get
            If HttpContext.Current.Session("UserAllowedUnits") Is Nothing Then
                Throw New ApplicationException("User records not properly Initialized")
            End If

            Return _UserAllowedUnits
        End Get
    End Property

    Private Sub GetUserAllowedUnits(filter As UnitSearchFilter)

    End Sub
End Class
