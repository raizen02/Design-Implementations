Imports System.Collections.Generic
Imports System.Data
Imports Cti.Seller.Client.Entities
Imports Microsoft.VisualBasic

Public Class clsEntityHelper
    Public Shared Function ConvertDatatable2Location(dt As DataTable) As List(Of Location)

        Dim result As New List(Of Location)
        For Each row As DataRow In dt.Rows
            Dim loc As New Location
            loc.CityMunicipality = row("uprLocation")
            result.Add(loc)
        Next row
        Return result

    End Function




End Class
