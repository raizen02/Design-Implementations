'**************************************************************
'Programmer Name		: Malvin Reyes
'Date Created			: 2014-01-06
'Finished Date          : 2014-01-13
'Program Name           : Dashboard of Sellers HUB
'Program Description    : 
'Stored Procedure       : 
'Updates Information
'						* John Alexander M. Baltazar | 2014-02-08 | JO# JYYXXXXX
'						  DEV - 2014.02.08 | PROD 2014-02-19
'							- Get contents form db

'						* Malvin Reyes | 2014-03-03 | JO# JYYXXXXX
'						  DEV - 2014-03-03 | PROD 2014-05-13
'							- Implement the SSO process.
'                           - Include the "Inherits AuthenticatedPageBase" 
'                           - Include the "SessionAPI.RefreshSessions(CurrentUser)"
'                           - Remove the Session("sesSelVerified") checking

'						* Malvin Reyes | 2014-03-17 | JO# JYYXXXXX
'						  DEV - 2014-03-17 | PROD 2014-05-13
'							- Change the oplan benta URL to http://www.filinvest.com.ph/sellershub/oplanbentatrenta
'                           - Change the URL of eCRM to http://ecrm.filinvest.com.ph/welcome.aspx

'						* Malvin Reyes | 2014-04-21 | JO# JYYXXXXX
'						  DEV - 2014-04-28 | PROD 2014-XX-XX
'							- Change the layout of Whats New, Buyer Promos, and Seller Promos
'							- Change the Title for Google Analytics

'						* Malvin Reyes | 2015-02-02 | JO# J1500291
'						  DEV - 2015-02-02 | PROD 2015-02-03
'							- Remove the oplan benta
'							- Change the Oplan Benta to More Fun 2015 without link.

'						* Malvin Reyes | 2016-03-21 | JO# SR-IT-2016-00822
'						  DEV - 2016-04-04 | PROD 2016-04-07
'							- Remove the More Fun 2015 
'							- Change the More Fun 2015 to 2016 I Own My Dream without link.

'						* Nestor Garais Jr | 2016-06-28 | JO# JYYXXXXX
'						  DEV -  2016-06-28 | PROD  
'                         Fix warnings :
'                               CA2000 : Microsoft.Reliability : In method 'whatsnew.Page_Load(Object, EventArgs)', call System.IDisposable.Dispose on object '_priclsDashboardContents' before all references to it are out of scope.
'                                   Solution : Add try/catch and dispose all disposable objects in try/finally
'**************************************************************
Imports System.Data
Imports System.Data.SqlClient
Imports FliAuthLib

Partial Class dashboard
    Inherits AuthenticatedPageBase

#Region "Declaring Variables"
    ' Your constant variables here
    Private _pridtSlideCaptions As DataTable
    Private _priclsDashboard As clsDashboardContents

#End Region

#Region "     User Define Subs and Functions     "
#Region "Procedures"
    ' Place your Sub here

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

        'Load UserProj
        Dim uiHelper As New clsSearchFilterMgr("BOBAV")
        uiHelper.LoadUserProjects()
        '-----------
        If Page.IsPostBack = True Then
            Exit Sub
        End If

        If Session("sesSelIsInternational") = "True" Then
            divIcons.InnerHtml = "                        <div class=""span4 feat"" style=""margin-bottom: 10px"">"
            divIcons.InnerHtml += "                            <a href=""ProjectCatalog/"" target=""_blank"">"
            divIcons.InnerHtml += "                                <img class=""center"" src=""img/ProjectCatalog.jpg"" /></a>"
            divIcons.InnerHtml += "                        </div>"
            divIcons.InnerHtml += "                        <div class=""span4 feat"" style=""margin-bottom: 10px"">"
            divIcons.InnerHtml += "                            <a href=""availabilitychart.aspx"">"
            divIcons.InnerHtml += "                                <img class=""center"" src=""img/AvailabilityChart.png"" /></a>"
            divIcons.InnerHtml += "                        </div>"
            divIcons.InnerHtml += "                        <div class=""span4 feat"" style=""margin-bottom: 10px"">"
            divIcons.InnerHtml += "                            <a href=""http://www.filinvestinternational.com/index.php?option=com_content&view=article&id=49:recruitment-video&catid=2:uncategorised"" target=""_blank"">"
            divIcons.InnerHtml += "                                <img class=""center"" src=""img/International2.jpg"" /></a>"
            divIcons.InnerHtml += "                        </div>"
        Else
            divIcons.InnerHtml = "                        <div class=""span4 feat"" style=""margin-bottom: 10px"">"
            divIcons.InnerHtml += "                            <a href=""http://ecrmqa.filinvest.com.ph/welcome.aspx"">"
            divIcons.InnerHtml += "                                <img class=""center"" src=""img/eCRM.png"" /></a>"
            divIcons.InnerHtml += "                        </div>"
            divIcons.InnerHtml += "                        <div class=""span4 feat"" style=""margin-bottom: 10px"">"
            divIcons.InnerHtml += "                            <a href=""availabilitychart.aspx"">"
            divIcons.InnerHtml += "                                <img class=""center"" src=""img/AvailabilityChart.png"" /></a>"
            divIcons.InnerHtml += "                        </div>"
            divIcons.InnerHtml += "                        <div class=""span4 feat"" style=""margin-bottom: 10px"">"
            divIcons.InnerHtml += "                            <a href=""SellerIncentive2016.aspx"">"
            divIcons.InnerHtml += "                                <img class=""center"" src=""Others/SellerIncentive2016/images/2016SIC_main.jpg"" /></a>"
            divIcons.InnerHtml += "                        </div>"
        End If

        _priclsDashboard = New clsDashboardContents

        If _priclsDashboard.fnDashboardContents(clsDashboardContents.ExecuteCommand.ExecuteDataAdapter, _
                                                1, _
                                                _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelUser"))) = False Then
            Exit Sub
        End If

        'Try
        '    Using _dimdaDataAdapter As SqlDataAdapter = _priclsDashboard.DataAdapter
        '        _dimdaDataAdapter.SelectCommand.Connection = New SqlConnection(MyMSSQLServer2000ConnectionString)
        '        _dimdaDataAdapter.SelectCommand.CommandTimeout = 0

        '        Using _dimdsContents As New DataSet
        '            _dimdaDataAdapter.Fill(_dimdsContents)

        '            _pridtSlideCaptions = _dimdsContents.Tables(1).Copy

        '            rptSlides.DataSource = _dimdsContents.Tables(0)
        '            rptSlides.DataBind()

        '            rptSlidePage.DataSource = _dimdsContents.Tables(0)
        '            rptSlidePage.DataBind()

        '            'Whats New
        '            If _dimdsContents.Tables(2).Rows.Count = 0 Then
        '                divWhatsNew1.Visible = False
        '                divWhatsNew2.Visible = False

        '                hrfCategoryID1.HRef = "#"
        '            Else
        '                divWhatsNew1.Visible = True
        '                divWhatsNew1.Attributes.Remove("class")

        '                If _dimdsContents.Tables(2).Rows.Count = 1 Then
        '                    divWhatsNew2.Visible = False
        '                    divWhatsNew1.Attributes.Add("class", "span12")
        '                Else
        '                    divWhatsNew2.Visible = True
        '                    divWhatsNew1.Attributes.Add("class", "span6")

        '                    imgWhatsNew2.Src = _dimdsContents.Tables(2).Rows(1).Item("Thumbnail").ToString
        '                    divWhatsNewTitle2.InnerHtml = _dimdsContents.Tables(2).Rows(1).Item("Title").ToString
        '                    parWhatsNew2.InnerHtml = CDate(_dimdsContents.Tables(2).Rows(1).Item("DateFrom")).ToString("MMMM dd, yyyy")
        '                    btnLinkWhatsNew2.NavigateUrl = "whatsnew.aspx?newsid=" & _dimdsContents.Tables(2).Rows(1).Item("ArtId").ToString
        '                End If

        '                imgWhatsNew1.Src = _dimdsContents.Tables(2).Rows(0).Item("Thumbnail").ToString
        '                divWhatsNewTitle1.InnerHtml = _dimdsContents.Tables(2).Rows(0).Item("Title").ToString
        '                parWhatsNew1.InnerHtml = CDate(_dimdsContents.Tables(2).Rows(0).Item("DateFrom")).ToString("MMMM dd, yyyy")
        '                btnLinkWhatsNew1.NavigateUrl = "whatsnew.aspx?newsid=" & _dimdsContents.Tables(2).Rows(0).Item("ArtId").ToString

        '                hplWhatsNewMore.NavigateUrl = "whatsnew.aspx?newsid=" & _dimdsContents.Tables(2).Rows(0).Item("ArtId").ToString
        '                hrfCategoryID1.HRef = "whatsnew.aspx?newsid=" & _dimdsContents.Tables(2).Rows(0).Item("ArtId").ToString
        '            End If

        '            lblCategoryID1.Text = _dimdsContents.Tables(6).Select("CategoryID = 1")(0).Item("CategoryTitle").ToString

        '            'Seller Promos
        '            rprSellerPromos.DataSource = _dimdsContents.Tables(3)
        '            rprSellerPromos.DataBind()
        '            lblCategoryID2.Text = _dimdsContents.Tables(6).Select("CategoryID = 2")(0).Item("CategoryTitle").ToString

        '            If _dimdsContents.Tables(3).Rows.Count <> 0 Then
        '                hplMoreSellersPromo.NavigateUrl = "whatsnew.aspx?newsid=" & _dimdsContents.Tables(3).Rows(0).Item("ArtId").ToString
        '                hrfCategoryID2.HRef = "whatsnew.aspx?newsid=" & _dimdsContents.Tables(3).Rows(0).Item("ArtId").ToString
        '            Else
        '                hrfCategoryID2.HRef = "#"
        '            End If

        '            'Buyer Promos
        '            rprBuyerPromos.DataSource = _dimdsContents.Tables(4)
        '            rprBuyerPromos.DataBind()
        '            lblCategoryID3.Text = _dimdsContents.Tables(6).Select("CategoryID = 3")(0).Item("CategoryTitle").ToString

        '            If _dimdsContents.Tables(4).Rows.Count <> 0 Then
        '                hplBuyersPromoMore.NavigateUrl = "whatsnew.aspx?newsid=" & _dimdsContents.Tables(4).Rows(0).Item("ArtId").ToString
        '                hrfCategoryID3.HRef = "whatsnew.aspx?newsid=" & _dimdsContents.Tables(4).Rows(0).Item("ArtId").ToString
        '            Else
        '                hrfCategoryID3.HRef = "#"
        '            End If

        '            Dim _dimdrCounts As DataRow = _dimdsContents.Tables(5).Rows(0)

        '            pnlNoWhatsNew.Visible = (_dimdrCounts.Item("WhatNewCount") = 0)
        '            hplWhatsNewMore.Visible = (_dimdrCounts.Item("WhatNewCount") > 2)

        '            pnlNoSellerPromo.Visible = (_dimdrCounts.Item("SellersPromoCount") = 0)
        '            hplMoreSellersPromo.Visible = (_dimdrCounts.Item("SellersPromoCount") > 3)

        '            pnlNoBuyerPromo.Visible = (_dimdrCounts.Item("BuyersPromoCount") = 0)
        '            hplBuyersPromoMore.Visible = (_dimdrCounts.Item("BuyersPromoCount") > 3)
        '        End Using

        '        If _dimdaDataAdapter.SelectCommand.Connection.State = ConnectionState.Open Then
        '            _dimdaDataAdapter.SelectCommand.Connection.Close()
        '        End If
        '    End Using
        'Catch ex As Exception
        '    Throw (ex)
        'Finally
        '    If _priclsDashboard IsNot Nothing Then _priclsDashboard.Dispose()
        'End Try
    End Sub
#End Region

#Region "Button"
    ' Events in all Buttons

#End Region

#Region "Grid"
    ' Events in all Grid

#End Region

#Region "Repeater"
    ' Events in all Grid
    Protected Sub rptSlides_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptSlides.ItemDataBound
        Dim _dimrptCaption As Repeater = e.Item.FindControl("rptSlideCaption")

        _pridtSlideCaptions.DefaultView.RowFilter = "[SlideId] = " & DirectCast(e.Item.DataItem, DataRowView).Item("Id")
        _dimrptCaption.DataSource = _pridtSlideCaptions.DefaultView
        _dimrptCaption.DataBind()
    End Sub
#End Region

    'etc...
#End Region
End Class
