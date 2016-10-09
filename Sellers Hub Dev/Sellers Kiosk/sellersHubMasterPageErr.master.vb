
Partial Class sellersHubMasterPageErr
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim _dimclsGlobalFunctions As New clsGlobalFunctions

        ViewState("Fullname") = _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelFullname"))

        If ViewState("Fullname") <> "" Or Nothing Then
            spnWelcomeName.InnerHtml = "Welcome " & ViewState("Fullname")
        Else
            spnWelcomeName.InnerHtml = ""
        End If
    End Sub
End Class

