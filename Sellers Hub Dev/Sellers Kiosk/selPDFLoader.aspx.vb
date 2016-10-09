'**************************************************************
'Programmer Name		: William Delos Reyes
'Date Created			: 20xx-xx-xx
'Finished Date          : 20xx-xx-xx
'Program Name           : PDF Loader
'Program Description    : 
'Stored Procedure       : 
'Updates Information
'						* Marc Erickson P. Legaspi | 2010-07-19 | JO# JYYXXXXX
'						  DEV - 2010-07-19 | PROD 2010-xx-xx
'							- Modified Code, made this applicable global to any form in Reservation

'						* Malvin Reyes | 2013-10-07 | JO# JYYXXXXX
'						  DEV - 2013-10-07 | PROD 2014-02-19
'							- Add parameter isByte for the PDF File saved in DB
'							- Filter if isByte = 1 direct write without stream convertion

'						* Alex Baltazar | 2014-08-26 | JO# JYYXXXXX
'						  DEV - 2014-08-27 | PROD 2014-02-19
'							- Adopt the PDF.js as the viewer of PDF
'							- Remove the unused imports
'                           - Include the error trapping and modify the pdfViewError in pdfjs/web/viewer.js (var moreInfoText = message + '\n';)

'						* Nestor Garais Jr| 2016-05-12 | JO#  
'						  DEV - 2016-05-12 | PROD 2016-05-12
'							- Close and Dispose stream after used...


'						* Nestor Garais Jr | 2016-06-28 | JO# JYYXXXXX
'						  DEV -  2016-06-28 | PROD  
'                         Fix warnings :
'                           CA2202 : Microsoft.Usage : Object '_dimStream' can be disposed more than once in method 'appSellersPage_selPDFLoader.Page_PreRender(Object, EventArgs)'. To avoid generating a System.ObjectDisposedException you should not call Dispose more than one time on an object.:
'                                   Solution : Remove _dimStream.Close() use only _dimStream.Dispose()

'**************************************************************

'Imports CrystalDecisions.Shared
'Imports CrystalDecisions.CrystalReports.Engine
'Imports System.Data
'Imports System.Data.SqlClient
Imports System.Windows.Forms

Partial Class appSellersPage_selPDFLoader
    Inherits System.Web.UI.Page

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        Dim _dimbyte() As Byte = Nothing
        Dim _dimStream As System.IO.Stream = Nothing
        Dim _dimstrSessionID As String = ""

        Try
            If Session("sesErrorMessage") IsNot Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "PdfJsError", String.Format("OpenPDFErr('{0}')", Session("sesErrorMessage").ToString), True)

                Exit Sub
            End If

            _dimstrSessionID = Request.QueryString("SessionID").ToString

            If Request.QueryString("isByte") = 1 Then
                _dimbyte = Session(_dimstrSessionID)
            Else
                _dimStream = Session(_dimstrSessionID)

                If _dimStream IsNot Nothing Then
                    ReDim _dimbyte(_dimStream.Length)
                    _dimStream.Read(_dimbyte, 0, CInt(_dimStream.Length))
                Else
                    ClientScript.RegisterStartupScript(Me.GetType(), "PdfJsError", String.Format("OpenPDFErr('{0}')", "File not loaded."), True)
                End If
            End If

            If _dimbyte IsNot Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "PdfJs", String.Format("OpenPDF('{0}', '')", Convert.ToBase64String(_dimbyte)), True)
            End If
        Catch ex As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)

            ClientScript.RegisterStartupScript(Me.GetType(), "PdfJsError", String.Format("OpenPDFErr('{0}', '')", ex.Message.Replace("'", "\'")), True)
        Finally
            Session.Remove(Request.QueryString("SessionID"))
            Session.Remove("sesErrorMessage")

            If _dimStream IsNot Nothing Then
                _dimStream.Dispose()
            End If

            _dimbyte = Nothing
        End Try
    End Sub
End Class
