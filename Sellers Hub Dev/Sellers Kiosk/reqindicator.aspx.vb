'**************************************************************
'Programmer Name		: Malvin V. Reyes
'Date Created			: 2014-11-13
'Finished Date          : 
'Program Name           : reqindicator
'Program Description    : Buyer's Requirements
'Stored Procedure       : 
'Updates Information
'						* Malvin Reyes | 2014-11-17 | JO# JYYXXXXX
'						  DEV - 2014-11-17 | PROD 2014-XX-XX
'							- Validate if the user have an access in reqindicator.aspx
'**************************************************************

Imports System.Windows.Forms
Imports FliAuthLib

Partial Class reqindicator
    'Inherits System.Web.UI.Page
    Inherits AuthenticatedPageBase

#Region "Declaring Variables"
    ' Your constant variables here
#End Region

#Region "     User Define Subs and Functions     "
#Region "Procedures"
    ' Place your Sub here
    Private Sub subErrorMessagePrompt(ByVal _parbolIsVisible As Boolean, _
                                      Optional ByVal _parstrErrorMsg As String = "Internal Error Occured.", _
                                      Optional ByVal _parstrAlert As String = "error")
        If _parbolIsVisible Then
            divErrorMsgBox.Visible = True

            divErrorMsgBox.Attributes.Remove("class")
            divErrorMsgBox.Attributes.Add("class", "alert alert-" & _parstrAlert)

            divErrorMsg.InnerHtml = _parstrErrorMsg
        Else
            divErrorMsgBox.Visible = False
        End If
    End Sub

    Private Sub subFillGenerate()
        If ddlApplicationType.SelectedValue = "Corporate" Then
            ddlContractType.Items.Item(5).Enabled = False

            If ddlContractType.SelectedIndex = 5 Then
                ddlContractType.SelectedIndex = 0
            End If
        Else
            ddlContractType.Items.Item(5).Enabled = True
        End If

        If ddlApplicationType.SelectedValue = "" Or ddlContractType.SelectedValue = "" Then
            btnGenerate.Enabled = False
        Else
            btnGenerate.Enabled = True
            btnGenerate.NavigateUrl = "BuyersRequirements/" & ddlApplicationType.SelectedValue & ddlContractType.SelectedValue & "/"
        End If
    End Sub
#End Region

#Region "Functions"
    ' Place your Functions here

#End Region
#End Region

#Region "     Events of Object     "
    ' Group the events of every control

#Region "Form"
    ' Events of Main form
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim _dimclsGlobalFunctions As New clsGlobalFunctions

        SessionAPI.RefreshSessions(CurrentUser)

        If Not _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelRole")).Contains("|reqindicator.aspx|") Then
            _dimclsGlobalFunctions = Nothing
            Response.Redirect("errorPage.aspx")

            Exit Sub
        Else
            If ddlContractType.SelectedValue = "" Then
                btnGenerate.Enabled = False
            Else
                btnGenerate.Enabled = True
            End If
        End If

        _dimclsGlobalFunctions = Nothing
    End Sub
#End Region

#Region "Button"
    ' Events in all Buttons

#End Region

#Region "Grid"
    ' Events in all Grid

#End Region

#Region "DropDownList"
    ' Events in all DropDownList
    Protected Sub ddlApplicationType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlApplicationType.SelectedIndexChanged
        Call subFillGenerate()
    End Sub

    Protected Sub ddlContractType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlContractType.SelectedIndexChanged
        Call subFillGenerate()
    End Sub
#End Region

    'etc...
#End Region
End Class
