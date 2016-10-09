Imports System.Data
Imports Microsoft.VisualBasic

Public Interface IDataService
    Function GetUserProject(UserName As String) As DataTable
End Interface
