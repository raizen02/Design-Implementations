Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports Cti.Seller.Client.Entities
Imports System.Data

Public Class clsSearchFilterMgr
    Private _UserAllowedUnits As List(Of Project)
    Private _UserAllowedLocations As List(Of Location)
    Private _UserName As String
    Private Const strUserAllowedLocation As String = "UserAllowedLocation"
    Private Const strUserAllowedUnits As String = "UserAllowedUnits"
    Private Const intRetryCount As Integer = 5
    Dim _DataService As IDataService
    Public Sub New(username As String)
        ' GetUserAllowedUnits(filter)
        _UserName = username
        _DataService = New clsAvailabilitychart()

    End Sub
    Public ReadOnly Property UserAllowedUnits() As List(Of Project)
        Get
            If HttpContext.Current.Session(strUserAllowedUnits) Is Nothing Then
                Throw New ApplicationException("User records not properly Initialized")
            End If

            Return _UserAllowedUnits
        End Get
    End Property

    Private Sub GetUserAllowedUnits(filter As UnitSearchFilter)

    End Sub

    Public ReadOnly Property UserAllowedLocations() As List(Of Location)
        Get


            If HttpContext.Current.Session(strUserAllowedLocation) Is Nothing Then
                Throw New ApplicationException("User records not properly Initialized")
            End If

            Return _UserAllowedLocations
        End Get
    End Property
    Public Sub LoadUserProjects()
        'Load User Project in Separate Thread
        GetUserProjects()

    End Sub
    Private Sub GetUserProjects()
        Dim dt As DataTable = _DataService.GetUserProject(_UserName)
        'Convert to Entity
        _UserAllowedLocations = clsEntityHelper.ConvertDatatable2Location(dt)
        HttpContext.Current.Session("strUserAllowedLocation") = _UserAllowedLocations

    End Sub

End Class
