'**********************************************************
'Programmer     : Nestor Garais Jr
'Date Created   : 2013-12-6
'Date Completed : 
'Program Name   : HandlerImage.ashx
'Description    : Image handler from viewstate source
'Updates        : how to use: img src="~/HandlerImage.ashx?imgname=TempFilename" 
'       
'                   * Nestor Garais Jr | 2016-06-27 | JO# JYYXXXXX
'					DEV - 2016-06-27   PROD  
'                   Fix warnings :
'                    CA2000 : Microsoft.Reliability : In method 'HandlerImage.GetImage(Byte())', call System.IDisposable.Dispose on object 'stream' before all references to it are out of scope.
'                    CA2000 : Microsoft.Reliability : In method 'HandlerImage.ProcessRequest(HttpContext)', call System.IDisposable.Dispose on object '_dimFileStream' before all references to it are out of scope.
'                    CA2202 : Microsoft.Usage : Object '_dimFileStream' can be disposed more than once in method 'HandlerImage.ProcessRequest(HttpContext)'. To avoid generating a System.ObjectDisposedException you should not call Dispose more than one time on an object.: Lines: 45, 48
'                                 Solution : Add try/catch and dispose all disposable objects in try/finally
'                    Optimize complex process on display image ,remove memorystream and byte variable 
'                   Add validating ofsession for security purposes
'***********************************************************

<%@ WebHandler Language="VB" Class="HandlerImage" %>

Imports System
Imports System.Web
Imports System.IO
Imports System.Drawing

Public Class HandlerImage : Implements IHttpHandler, IRequiresSessionState
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        If context.Session("sesSelUser") Is Nothing Then
            Exit Sub
        End If
        
        If context.Session("sesSelUser") = "" Then
            Exit Sub
        End If
        
        context.Response.Clear()

        If context.Request.QueryString.Count <> 0 Then
            'Get the stored image and write in the response.
            Dim _dimstrTempFileName As String = context.Server.UrlDecode(context.Request.QueryString("imgname"))

            Try
                Using _dimFileStream As New FileStream(_dimstrTempFileName, FileMode.Open)
                    Dim _dimImage As New Bitmap(_dimFileStream)

                    context.Response.ContentType = "image/jpeg"
                    _dimImage.Save(context.Response.OutputStream, Imaging.ImageFormat.Jpeg)
                End Using
            Catch ex As Exception
                context.Response.Write("Error Image")
            End Try
        End If
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
End Class