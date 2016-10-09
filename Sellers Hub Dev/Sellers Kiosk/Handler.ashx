'**********************************************************
'Programmer     : Nestor Garais Jr
'Date Created   : 7/3/2012
'Date Completed : 
'Program Name   : Handler.ashx
'Description    : Image handler 
'Updates        : 
'                       * Nestor Garais Jr | 2016-02-11 | JO# JYYXXXXX
'						  DEV - 2016-02-11   PROD  
'                             - Resize image fixed height to 50 then compute the aspect ration for width
'                             - Use function GetThumbnailImage for optimize loading in web browser

'                       * Nestor Garais Jr | 2016-06-27 | JO# JYYXXXXX
'						  DEV - 2016-06-27   PROD  
'                         Fix warnings :
'                            CA2000 : Microsoft.Reliability : In method 'Handler.ProcessRequest(HttpContext)', object '_dimDtImage' is not disposed along all exception paths. Call System.IDisposable.Dispose on object '_dimDtImage' before all references to it are out of scope.
'                            CA2000 : Microsoft.Reliability : In method 'Handler.ProcessRequest(HttpContext)', object '_dimdtaImage' is not disposed along all exception paths. Call System.IDisposable.Dispose on object '_dimdtaImage' before all references to it are out of scope.
'                            CA2000 : Microsoft.Reliability : In method 'Handler.ProcessRequest(HttpContext)', call System.IDisposable.Dispose on object '_dimImageMemoryStream' before all references to it are out of scope.
'                            CA2000 : Microsoft.Reliability : In method 'Handler.ProcessRequest(HttpContext)', call System.IDisposable.Dispose on object '_dimMemoryStream' before all references to it are out of scope.
'                            CA2202 : Microsoft.Usage : Object '_dimSqlConnection' can be disposed more than once in method 'Handler.ProcessRequest(HttpContext)'. To avoid generating a System.ObjectDisposedException you should not call Dispose more than one time on an object.: Lines: 60, 65
'                                 Solution : Add try/catch and dispose all disposable objects in try/finally

'                            CA2100 : Microsoft.Security : The query string passed to 'SqlCommand.CommandText.Set(String)' in 'Handler.ProcessRequest(HttpContext)' could contain the following variables 'context.Request.QueryString("ImgID")', 'context.Request.QueryString("ImgID")'. If any of these variables could come from user input, consider using a stored procedure or a parameterized SQL query instead of building the query with string concatenations.
'                            CA2100 : Microsoft.Security : The query string passed to 'SqlDataAdapter.New(String, SqlConnection)' in 'Handler.ProcessRequest(HttpContext)' could contain the following variables 'context.Request.QueryString("ImgID")', 'context.Request.QueryString("ImgID")'. If any of these variables could come from user input, consider using a stored procedure or a parameterized SQL query instead of building the query with string concatenations.
'                                   Solution : Add sp_selOnlineViewImage for security, remove commandtext
'                       Add validating of session for security purposes
'***********************************************************

<%@ WebHandler Language="VB" Class="Handler" %>

Imports System
Imports System.Web
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Drawing

Public Class Handler
    Inherits System.Web.UI.Page : Implements IHttpHandler, IRequiresSessionState

    Private _pridttImageList As DataTable
    Private _pridtaImageSql As SqlDataAdapter
    Private _primsMemoryStream As MemoryStream
                      
    Public Overrides Sub ProcessRequest(ByVal context As HttpContext) 'Implements IHttpHandler.ProcessRequest
        If context.Session("sesSelUser") Is Nothing Then
            Exit Sub
        End If
        
        If context.Session("sesSelUser") = "" Then
            Exit Sub
        End If
        
        Dim _dimIntDefaultImageWidth As Integer = 50
        Dim _dimIntDefaultImageHeight As Integer = 50
        
        Using _dimSqlConnection As New SqlConnection(MyMSSQLServer2000ConnectionString & ";Pooling=false")
            _dimSqlConnection.Open()
            
            Dim _dimNewImage As Image = Nothing
            Dim _dimNewByteImage() As Byte
                      
            Try
                _pridttImageList = New DataTable
                _pridtaImageSql = New SqlDataAdapter("sp_selOnlineViewImage", _dimSqlConnection)
                _pridtaImageSql.SelectCommand.CommandType = CommandType.StoredProcedure
                _pridtaImageSql.SelectCommand.CommandTimeout = 0
                
                _pridtaImageSql.SelectCommand.Parameters.AddWithValue("@pstrImageID", context.Request.QueryString("ImgID"))
                _pridtaImageSql.Fill(_pridttImageList)
                
                
                If _pridttImageList.Rows.Count > 0 Then
                    context.Response.Clear()
                    context.Response.ContentType = "image/jpeg"
                    
                    If context.Request.QueryString("ImgThumb") = "1" Then
                        'ThumbNail
                        _primsMemoryStream = New MemoryStream(DirectCast(_pridttImageList.Rows(0).Item("Images"), Byte()))
                        _dimNewImage = Image.FromStream(_primsMemoryStream)
                        
                        Dim _dimDecImageSizeRatio As Decimal = 0.05
                        
                        'Fixed to 70 pixel height, recompute width size by aspect ratio
                        _dimIntDefaultImageHeight = 70
                        _dimDecImageSizeRatio = _dimIntDefaultImageHeight / _dimNewImage.Height
                        _dimIntDefaultImageWidth = _dimDecImageSizeRatio * _dimNewImage.Width
                                         
                        'Resize image and lower the image quality for thumbnail purposes
                        _dimNewImage = _dimNewImage.GetThumbnailImage(_dimIntDefaultImageWidth, _dimIntDefaultImageHeight, New Drawing.Image.GetThumbnailImageAbort(AddressOf ThumbnailCallback), 0)
                       
                        ' Output image by stream
                        _dimNewImage.Save(context.Response.OutputStream, Imaging.ImageFormat.Jpeg)
                    Else
                        'display actutal size
                        context.Response.BinaryWrite(DirectCast(_pridttImageList.Rows(0).Item("Images"), Byte()))
                    End If
                End If
            Catch ex As Exception
                context.Response.Write("Error Image")
            Finally
                If _pridttImageList IsNot Nothing Then _pridttImageList.Dispose()
                If _pridtaImageSql IsNot Nothing Then _pridtaImageSql.Dispose()
                If _primsMemoryStream IsNot Nothing Then _primsMemoryStream.Dispose()
                If _dimNewImage IsNot Nothing Then _dimNewImage.Dispose()
                
                _dimNewByteImage = Nothing
            End Try
        End Using
    End Sub
 
    Public Overloads ReadOnly Property IsReusable() As Boolean 'Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
    
    Public Function ThumbnailCallback() As Boolean
        Return True
    End Function
End Class