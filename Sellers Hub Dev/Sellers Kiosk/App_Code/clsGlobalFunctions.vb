Imports Microsoft.VisualBasic
Imports System.Diagnostics
Imports System.Web.UI

Public Class clsGlobalFunctions
    Function fnValidSql(ByVal _parstrValue As String) As String
        Dim _dimstrValue As String = ""
        Dim _dimintCounter As Integer

        For _dimintCounter = 1 To Len(_parstrValue)
            If Mid(_parstrValue, _dimintCounter, 1) = "'" Then
                _dimstrValue = _dimstrValue + "'"
            End If

            _dimstrValue = _dimstrValue + Mid(_parstrValue, _dimintCounter, 1)
        Next

        Return Trim(_dimstrValue)
    End Function

    Public Function fnEnCrypt(ByVal _parstrValue As String) As String
        Dim _dimintCodeCharCode, _dimintPasswordCharCode, _dimintChar, _dimintNewCharCode As Integer
        Dim _dimstrValue As String = ""
        Const _dimstrPassword As String = "06120914"

        For _dimintChar = 1 To Len(_parstrValue)
            _dimintCodeCharCode = Asc(Mid(_parstrValue, _dimintChar, 1))
            _dimintPasswordCharCode = Asc(Mid(_dimstrPassword, (_dimintChar Mod Len(_dimstrPassword) + 1), 1))

            _dimintNewCharCode = _dimintCodeCharCode + _dimintPasswordCharCode

            If _dimintNewCharCode > 255 Then _dimintNewCharCode = _dimintNewCharCode - 255

            _dimstrValue = _dimstrValue & Chr(_dimintNewCharCode)
        Next

        Return _dimstrValue
    End Function

    Function fnDeCrypt(ByVal _parstrValue As String) As String
        Dim _dimintCodeCharCode, _dimintPasswordCharCode, _dimintChar, _dimintOriginalCharCode As Integer
        Dim _dimstrValue As String = ""
        Const _dimstrPassword As String = "06120914"

        For _dimintChar = 1 To Len(_parstrValue)
            ' Get charactercodes of code and password
            _dimintCodeCharCode = Asc(Mid(_parstrValue, _dimintChar, 1))
            _dimintPasswordCharCode = Asc(Mid(_dimstrPassword, (_dimintChar Mod Len(_dimstrPassword) + 1), 1))

            _dimintOriginalCharCode = _dimintCodeCharCode - _dimintPasswordCharCode

            ' Charactercode must be => 1 and <= 255
            If _dimintOriginalCharCode < 1 Then _dimintOriginalCharCode = _dimintOriginalCharCode + 255

            _dimstrValue = _dimstrValue & Chr(_dimintOriginalCharCode)
        Next

        Return _dimstrValue
    End Function

    'Public Function fnGetPath(ByVal _parbolIsDefault As Boolean, _
    '                      ByVal _parstrServerName As String, _
    '                      ByVal _parstrFilename As String, _
    '                      ByVal _parstrHTTPS As String) As String
    '    If _parbolIsDefault = True Then
    '        _parstrFilename = _parstrFilename.ToLower
    '        _parstrFilename = _parstrFilename.Replace(MyDefaultFile.ToLower, MyDefaultInitialFile)
    '    End If

    '    If _parstrHTTPS <> "ON" Then
    '        'fnGetPath = "http://localhost:57304" & _parstrFilename          'DEV Test
    '        fnGetPath = "http://" & _parstrServerName & _parstrFilename     'UAT
    '        'fnGetPath = "https://" & _parstrServerName & _parstrFilename    'PROD
    '    Else
    '        fnGetPath = "https://" & _parstrServerName & _parstrFilename
    '    End If
    'End Function

    Public Sub ControlsFillValue(ByVal _parctrControl As Control, _
                                 ByVal _parstrControlString As String, _
                                 Optional ByVal _parstrValue As String = "")
        Dim _dimctrControl As Control

        Select Case _parstrControlString
            Case "TextBox"
                Dim _dimtxbTextBox As TextBox

                For Each _dimctrControl In _parctrControl.Controls
                    If TypeOf _dimctrControl Is TextBox Then
                        _dimtxbTextBox = _dimctrControl
                        _dimtxbTextBox.Text = _parstrValue
                    End If

                    If _dimctrControl.HasControls Then
                        ControlsFillValue(_dimctrControl, _parstrControlString, _parstrValue)
                    End If
                Next
            Case "HtmlGenericControl"
                Dim _dimhgcHtmlGenericControl As HtmlGenericControl

                For Each _dimctrControl In _parctrControl.Controls
                    If TypeOf _dimctrControl Is HtmlGenericControl Then
                        _dimhgcHtmlGenericControl = _dimctrControl
                        _dimhgcHtmlGenericControl.InnerHtml = _parstrValue
                    End If

                    If _dimctrControl.HasControls Then
                        ControlsFillValue(_dimctrControl, _parstrControlString, _parstrValue)
                    End If
                Next
        End Select
    End Sub

    Public Sub TextBoxControlsVisible(ByVal _parctrControl As Control, _
                                      ByVal _parbolReadOnly As Boolean)
        Dim _dimctrControl As Control
        Dim _dimtxbTextBox As TextBox

        For Each _dimctrControl In _parctrControl.Controls
            If TypeOf _dimctrControl Is TextBox Then
                _dimtxbTextBox = _dimctrControl
                _dimtxbTextBox.Visible = _parbolReadOnly
            End If

            If _dimctrControl.HasControls Then
                TextBoxControlsVisible(_dimctrControl, _parbolReadOnly)
            End If
        Next
    End Sub
End Class
