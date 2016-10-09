
'						* Nestor Garais Jr | 2016-06-28 | JO# JYYXXXXX
'						  DEV   2016-06-28 | PROD   
'                         Fix warnings :
'                           CA2000 : Microsoft.Reliability : In method 'SessionAPI.RefreshSessions(WebUser, Boolean)', call System.IDisposable.Dispose on object '_dimclsUserLogin' before all references to it are out of scope.
'                                   Solution : Add try/catch and dispose all disposable objects in try/finally

Imports Microsoft.VisualBasic
Imports System.Web
Imports FliAuthLib.FliAuthService
Imports FliAuthLib

Public Class SessionAPI
    Private Shared _priclsGlobalFunctions As New clsGlobalFunctions

    Public Shared Property UserVerified() As Boolean
        Get
            If HttpContext.Current.Session("sesSelVerified") Is Nothing Then
                Return False
            End If

            Return HttpContext.Current.Session("sesSelVerified")
        End Get

        Set(ByVal value As Boolean)
            HttpContext.Current.Session("sesSelVerified") = value
        End Set
    End Property

    Public Shared Property SelIsInternational() As Boolean
        Get
            If HttpContext.Current.Session("sesSelIsInternational") Is Nothing Then
                Return False
            End If

            Return HttpContext.Current.Session("sesSelIsInternational")
        End Get

        Set(ByVal value As Boolean)
            HttpContext.Current.Session("sesSelIsInternational") = value
        End Set
    End Property

    Public Shared Property LoggedFromApp() As Boolean
        Get
            If HttpContext.Current.Session("LoggedFromApp") Is Nothing Then
                Return False
            End If

            Return HttpContext.Current.Session("LoggedFromApp")
        End Get

        Set(ByVal value As Boolean)
            HttpContext.Current.Session("LoggedFromApp") = value
        End Set
    End Property

    Public Shared Property SelSellerType() As String
        Get
            Return HttpContext.Current.Session("sesSelSellerType")
        End Get

        Set(ByVal value As String)
            HttpContext.Current.Session("sesSelSellerType") = value
        End Set
    End Property

    Public Shared Property Username() As String
        Get
            Return _priclsGlobalFunctions.fnDeCrypt(HttpContext.Current.Session("sesSelUser"))
        End Get

        Set(ByVal value As String)
            HttpContext.Current.Session("sesSelUser") = _priclsGlobalFunctions.fnEnCrypt(value)
        End Set
    End Property

    Public Shared Property FullName() As String
        Get
            Return _priclsGlobalFunctions.fnDeCrypt(HttpContext.Current.Session("sesSelFullname"))
        End Get

        Set(ByVal value As String)
            HttpContext.Current.Session("sesSelFullname") = _priclsGlobalFunctions.fnEnCrypt(value)
        End Set
    End Property

    Public Shared Property SelReport() As String
        Get
            Return HttpContext.Current.Session("sesSelReport")
        End Get

        Set(ByVal value As String)
            HttpContext.Current.Session("sesSelReport") = value
        End Set
    End Property

    Public Shared Property SelLastLogIn() As String
        Get
            Return HttpContext.Current.Session("sesSelLastLogIn")
        End Get

        Set(ByVal value As String)
            HttpContext.Current.Session("sesSelLastLogIn") = value
        End Set
    End Property

    Public Shared Property SelAgentCode() As String
        Get
            Return _priclsGlobalFunctions.fnDeCrypt(HttpContext.Current.Session("sesSelAgentCode"))
        End Get

        Set(ByVal value As String)
            HttpContext.Current.Session("sesSelAgentCode") = _priclsGlobalFunctions.fnEnCrypt(value)
        End Set
    End Property

    Public Shared Property SelRole() As String
        Get
            Return _priclsGlobalFunctions.fnDeCrypt(HttpContext.Current.Session("sesSelRole"))
        End Get

        Set(ByVal value As String)
            HttpContext.Current.Session("sesSelRole") = _priclsGlobalFunctions.fnEnCrypt(value)
        End Set
    End Property

    Public Shared Sub RefreshSessions(User As WebUser, Optional Forced As Boolean = False)
        'If SessionAPI.Username = Crypto.Decrypt(User.Username, Config.Crypto.Key) And Forced = False Then Exit Sub

        'Dim _dimclsUserLogin As clsUserLogin = Nothing

        'Try
        '    _dimclsUserLogin = New clsUserLogin

        '    If _dimclsUserLogin.fnUserLogin(clsUserLogin.ExecuteCommand.ExecuteDataAdapter, _
        '                                    3, _
        '                                    Crypto.Decrypt(User.Username, Config.Crypto.Key), _
        '                                    "", _
        '                                    HttpContext.Current.Request.ServerVariables("REMOTE_USER"), _
        '                                    HttpContext.Current.Request.ServerVariables("REMOTE_ADDR"), _
        '                                    AppConstants.AppCodes.A0002) = False Then
        '        Exit Sub
        '    End If

        '    Using _dimdsUserInfo As New System.Data.DataSet
        '        _dimclsUserLogin.DataAdapter.SelectCommand.Connection = New Data.SqlClient.SqlConnection(MyMSSQLServer2000ConnectionString)
        '        _dimclsUserLogin.DataAdapter.SelectCommand.CommandTimeout = 0
        '        _dimclsUserLogin.DataAdapter.Fill(_dimdsUserInfo)

        '        Dim _dimdrUser As Data.DataRow = _dimdsUserInfo.Tables(0).Rows(0)

        '        SessionAPI.UserVerified = True
        '        SessionAPI.Username = Crypto.Decrypt(User.Username, Config.Crypto.Key)
        '        SessionAPI.FullName = _dimdrUser.Item("usrFullName").ToString
        '        SessionAPI.SelReport = HttpContext.Current.Server.UrlDecode(_dimdrUser.Item("uapReport").ToString)
        '        SessionAPI.SelLastLogIn = HttpContext.Current.Server.UrlDecode(_dimdrUser.Item("usrLastLogIn").ToString)
        '        SessionAPI.SelAgentCode = _dimdrUser.Item("uapUserCode").ToString
        '        SessionAPI.SelIsInternational = _dimdrUser.Item("usrIsInternational")
        '        SessionAPI.SelSellerType = _dimdrUser.Item("usrSellerType")

        '        SessionAPI.LoggedFromApp = User.LoggedFromApp

        '        If _dimdsUserInfo.Tables(1).Rows.Count > 0 Then
        '            SessionAPI.SelRole = _dimdsUserInfo.Tables(1).Rows(0).Item("Menu").ToString
        '        End If
        '    End Using
        'Catch ex As Exception
        '    Throw ex
        'Finally
        '    If _dimclsUserLogin.DataAdapter.SelectCommand.Connection.State = Data.ConnectionState.Open Then
        '        _dimclsUserLogin.DataAdapter.SelectCommand.Connection.Close()
        '    End If

        '    If _dimclsUserLogin IsNot Nothing Then _dimclsUserLogin.Dispose()
        'End Try
    End Sub

    Public Shared Sub ClearSessions()
        UserVerified = False
        Username = Nothing
        FullName = Nothing
        SelReport = Nothing
        SelLastLogIn = Nothing
        SelAgentCode = Nothing
        SelRole = Nothing
        SelIsInternational = False
        SelSellerType = Nothing

        LoggedFromApp = False
    End Sub
End Class
