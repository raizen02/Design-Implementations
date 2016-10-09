'**************************************************************
'Programmer Name		: Malvin Reyes
'Date Created			: 2010-10-04
'Finished Date          : 2010-10-04
'Program Name           : Contact Us
'Program Description    : 
'Stored Procedure       : 
'Updates Information
'						* Nestor Garais Jr | 2012-05-14 | JYYXXXXX
'						  DEV - 2012-05-14 | PROD - 2012-XX-XX
'							- apply new class for sendemail

'						* Malvin Reyes | 2012-10-16 | JYYXXXXX
'						  DEV - 2012-10-16 | PROD - 2012-10-16
'							- Change the recipient of email from inquiry@filinvestinternational.com
'                             to MyMailTo ("servicedesk@filinvestland.com.ph")
'							- Change the recipient of confirmation email from donotreply@filinvestinternational.com
'                             to MyMailFrom ("donotreply@filinvestland.com")

'						* Malvin Reyes | 2013-11-28 | JO# J1304070
'						  DEV - 2013-11-28 | PROD 2014-02-19
'							- Change the themes

'						* Malvin Reyes | 2014-03-04 | JO# JYYXXXXX
'						  DEV - 2014-03-04 | PROD 2014-05-13
'							- Implement the SSO process.
'                           - Include the "Inherits AuthenticatedPageBase" 
'                           - Include the "SessionAPI.RefreshSessions(CurrentUser)"
'                           - Remove the Session("sesSelVerified") checking

'						* Malvin Reyes | 2014-04-28 | JO# JYYXXXXX
'						  DEV - 2014-04-28 | PROD 2014-05-13
'							- Change the Title for Google Analytics

'						* Malvin Reyes | 2014-08-11 | JO# JYYXXXXX
'						  DEV - 2014-08-11 | PROD 2014-08-22
'							- Add the parameter _parstrReplyTo in ExeCuteAko

'Updates Information	: Nestor Garais Jr | (JO# XXXXXXXX)
'Date Updated		    : 2016-06-23
'Changes			    : - Add try/catch on every function and procedure then dispose all disposable object on try/finally
'Remarks			    : DEV - 2016-06-23 | PROD - 
'***********************************************************************

Imports FliAuthLib

Partial Class contactus
    'Inherits System.Web.UI.Page
    Inherits AuthenticatedPageBase

#Region "     Declaring Variables     "
    ' Your constant variables here

    Dim _dimclsConnection As clsConnection
    Dim _dimclsSendEmail As clsSendEmail
#End Region

#Region "     User Define Subs and Functions     "
#Region "Procedures"
    ' Place your Sub here
    Protected Sub clearFields()
        txbTelNum.Text = ""
        txbEmailAdd.Text = ""
        rdbOthers.Checked = True
        txbMessage.Text = ""
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

        If Not _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelRole")).Contains("|contactus.aspx|") Then
            Response.Redirect("errorPage.aspx")

            Exit Sub
        Else
            spnDate.InnerHtml = String.Format("{0:M/d/yyyy}", Now())
            spnCustomerName.InnerHtml = _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelFullname"))
        End If

        _dimclsGlobalFunctions = Nothing
    End Sub

    'Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
    '    System.Data.SqlClient.SqlConnection.ClearAllPools()
    'End Sub
#End Region

#Region "Button"
    ' Events in all Buttons
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Call clearFields()

        divErrorMsg.Visible = False
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        divErrorMsgBox.Attributes.Remove("class")
        divErrorMsgBox.Attributes.Add("class", "alert alert-error")

        Dim _dimstrMessage As String

        _dimclsConnection = New clsConnection

        Try
            If _dimclsConnection.OpenConnection(clsConnection.Database.MSSQLServer2000, MyMSSQLServer2000ConnectionString) = False Then
                divErrorMsgBox.Visible = True
                divErrorMsg.InnerHtml = "Internal Error Occured.<br />There was a problem in connecting to mail server"

                Exit Sub
            End If

            _dimclsSendEmail = New clsSendEmail

            With _dimclsSendEmail
                .SQLConnection = _dimclsConnection.MSSQLServer2000Connection

                Dim _dimstrQueries As String = ""

                If rdbAffiliation.Checked Then
                    _dimstrQueries = "Affiliation/Accreditation"
                ElseIf rdbCommRelated.Checked Then
                    _dimstrQueries = "Commission Related"
                ElseIf rdbDocuRelated.Checked Then
                    _dimstrQueries = "Documents Related"
                ElseIf rdbStatus.Checked Then
                    _dimstrQueries = "Status of Previous Request"
                ElseIf rdbPayments.Checked Then
                    _dimstrQueries = "Payments"
                ElseIf rdbOthers.Checked Then
                    _dimstrQueries = "Others"
                End If

                'Generate the Message
                _dimstrMessage = "<font face='Verdana' size='2' color='#01188C'><p align='justify'>"
                _dimstrMessage += "The following information was submitted through Seller's Request / Inquiry Form.<br/>"
                _dimstrMessage += "Submitted:" & Date.Now & "<br/>"
                _dimstrMessage += "------------------------------------------------------------</p>"
                _dimstrMessage += "<p align='justify'><b>Seller's Information</b><br/>"
                _dimstrMessage += "------------------------------------------------------------<br/>"
                _dimstrMessage += "  Seller's Name : " & spnCustomerName.InnerHtml.ToString & "<br/>"
                _dimstrMessage += "  Telephone Number : " & txbTelNum.Text & "<br/><br/>"
                _dimstrMessage += "<b>Query on " & _dimstrQueries & ".</b><br/>"
                _dimstrMessage += "------------------------------------------------------------<br/>"
                _dimstrMessage += "  Message: <br/><br/>"
                _dimstrMessage += txbMessage.Text & "</p></font>"

                If .ExeCuteAko(clsSendEmail.ExecuteCommand.ExecuteNonQuery, _
                               Session("sesSelFullname"), _
                               1, _
                               spnCustomerName.InnerHtml.ToString & "<" & txbEmailAdd.Text & ">", _
                               MyMailTo, _
                               "Seller's Inquiry / Follow-ups", _
                               _dimstrMessage, _
                               True, , , _
                               txbEmailAdd.Text) = False Then
                    divErrorMsgBox.Visible = True
                    divErrorMsg.InnerHtml = .SQLMessage

                    Exit Sub
                End If

                _dimstrMessage = "<p align='justify'><font face='Verdana' size='2' color='#01188C'>"
                _dimstrMessage += "<br/>Greetings!"
                _dimstrMessage += "</font></p><p align='justify'><font face='Verdana' size='2' color='#01188C'>Thank "
                _dimstrMessage += "you for contacting us. Our staff will attend to your query as soon as possible."
                _dimstrMessage += "</font></p><p align='justify'><font face='Verdana' size='2' color='#01188C'>"
                _dimstrMessage += "Your concerns are our highest priority.</font></p>"
                _dimstrMessage += "<p align='justify'><b><font face='Verdana' size='2' color='#000080'>"
                _dimstrMessage += "Filinvest Land, Inc.</font></b></p>"

                If .ExeCuteAko(clsSendEmail.ExecuteCommand.ExecuteNonQuery, _
                               Session("sesSelFullname"), _
                               1, _
                               MyMailFrom, _
                               txbEmailAdd.Text, _
                               "Filinvest [Seller's Inquiry / Follow-ups]", _
                               _dimstrMessage, _
                               True, , , _
                               MyMailTo) = False Then
                    divErrorMsgBox.Visible = True
                    divErrorMsg.InnerHtml = .SQLMessage

                    Exit Sub
                End If

                divErrorMsgBox.Attributes.Remove("class")
                divErrorMsgBox.Attributes.Add("class", "alert alert-success")

                divErrorMsgBox.Visible = True
                divErrorMsg.InnerHtml = "Thank you!<br/>Your query has been added."

                Call clearFields()
            End With
        Catch generalEx As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(generalEx)
        Finally
            If _dimclsConnection IsNot Nothing Then _dimclsConnection.Dispose()
            If _dimclsSendEmail IsNot Nothing Then _dimclsSendEmail.Dispose()
        End Try
    End Sub
#End Region

#Region "Grid"
    ' Events in all Grid

#End Region

#Region "Textbox"
    ' Events in all Textbox

#End Region

#Region "ComboBox"
    ' Events in all ComboBox

#End Region

#Region "CheckBox"
    ' Events in all CheckBox

#End Region

    'etc...
#End Region
End Class
