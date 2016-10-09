'**************************************************************
'Programmer Name		: Malvin V. Reyes
'Date Created			: 2016-06-21
'Finished Date          : 
'Program Name           : Error Handler Page
'Program Description    : Error Handler Page
'Stored Procedure       : 
'Updates Information
'						* Malvin Reyes | 2016-06-21 | JO# J1Xxxxxx
'						  DEV - 20xx-xx-xx | PROD 20xx-XX-XX
'							- 
'**************************************************************

Partial Class errorPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Request.QueryString("aspxerrortype") = "401" Or Request.QueryString("aspxerrortype") = "403" Or Request.QueryString("aspxerrortype") Is Nothing Then
            Title = "Unauthorized Page | Sellers' HUB"
            divErrorMsg.InnerHtml = "Sorry, you are not authorized!"
            divError403.Visible = True
        ElseIf Request.QueryString("aspxerrortype") = "404" Then
            Title = "Page Not Found | Sellers' HUB"
            divErrorMsg.InnerHtml = "Oops... an unexpected event has just occurred"
            divError404.Visible = True
        Else
            Title = "Exception Notice | Sellers' HUB"
            divErrorMsg.InnerHtml = "Oops... an unexpected event has just occurred"
            divError500.Visible = True
        End If
    End Sub
End Class
