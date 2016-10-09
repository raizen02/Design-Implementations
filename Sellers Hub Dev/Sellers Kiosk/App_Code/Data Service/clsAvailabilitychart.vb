Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports Cti.Seller.Data.Data_Repositories
Imports Microsoft.VisualBasic

Public Class clsAvailabilitychart
    Implements IDataService

    Public Function GetUserProject(UserName As String) As DataTable Implements IDataService.GetUserProject

        Dim sql As StringBuilder = New StringBuilder()
        sql.Append("SELECT uprLocation, uprProjectCode, uprProjectName, uprPhaseBuildingCode,")
        sql.Append(" uprPhaseBuildingName, uprAllocationCode, uprAllocationDesc, uprPricelistUpload")
        sql.Append(" FROM dbo.ssmUserApplication user")
        sql.Append(" INNER Join dbo.ssmUserProjects proj ON user.uapUserCode = proj.uprUserCode")
        sql.Append(" WHERE  user.uapUserName = @Username")
        'BOBAV'
        Dim sqlParams() As SqlParameter = {New SqlParameter("@Username", UserName)}
        Dim dt As DataTable = DaoHelperMSSQL.GetData(sql.ToString(), sqlParams)

        Return dt


    End Function

End Class
