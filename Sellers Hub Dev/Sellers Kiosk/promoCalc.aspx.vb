'**************************************************************
'Programmer Name		: Malvin Reyes
'Date Created			: 2014-10-13
'Finished Date          : 2014-10-14
'Program Name           : Spot DP Promo Calculator
'Program Description    : 
'Stored Procedure       : 
'Updates Information
'						* Malvin V. Reyes | 2014-10-20 | JYYXXXXX
'						  DEV - 2014-10-17 | PROD - 2014-XX-XX
'							- Include the picture of the promo for mechanics
'**************************************************************
Imports FliAuthLib

Partial Class promoCalc
    Inherits AuthenticatedPageBase

#Region "Declaring Variables"
    ' Your constant variables here

#End Region

#Region "     User Define Subs and Functions     "
#Region "Procedures"
    ' Place your Sub here
    Private Sub subCalculate()
        Dim _dimdblNumber As Double

        If Double.TryParse(txbTCP.Text, _dimdblNumber) Then
            If Double.TryParse(txbReservationFee.Text, _dimdblNumber) Then
                txbDiscount.Text = ((Convert.ToDouble(txbTCP.Text) * (0.04)) - Convert.ToDouble(txbReservationFee.Text)).ToString("n2")
            Else
                txbDiscount.Text = ((Convert.ToDouble(txbTCP.Text) * (0.04)) - 0.0).ToString("n2")
            End If

            txbYouSaved.Text = (Convert.ToDouble(txbTCP.Text) * (0.01)).ToString("n2")
            txbDPBalance.Text = ((Convert.ToDouble(txbTCP.Text) * (0.05)) / 6).ToString("n2")
            txbTCPBalance.Text = (Convert.ToDouble(txbTCP.Text) * (0.9)).ToString("n2")
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
        _dimclsGlobalFunctions = Nothing
    End Sub
#End Region

#Region "Button"
    ' Events in all Buttons
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        txbTCP.Text = ""
        txbDiscount.Text = ""
        txbYouSaved.Text = ""
        txbDPBalance.Text = ""
        txbTCPBalance.Text = ""

        divErrorMsg.Visible = False
    End Sub
#End Region

#Region "Text box"
    ' Events in all Text box
    Protected Sub txbTCP_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txbTCP.TextChanged
        Dim _dimdblNumber As Double

        If Double.TryParse(txbTCP.Text, _dimdblNumber) Then
            txbTCP.Text = String.Format("{0:n2}", _dimdblNumber)

            Call subCalculate()

            txbReservationFee.Focus()
        Else
            txbTCP.Text = ""
            txbReservationFee.Text = ""
            txbDiscount.Text = ""
            txbYouSaved.Text = ""
            txbDPBalance.Text = ""
            txbTCPBalance.Text = ""
        End If
    End Sub

    Protected Sub txbReservationFee_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txbReservationFee.TextChanged
        Dim _dimdblNumber As Double

        If Double.TryParse(txbReservationFee.Text, _dimdblNumber) Then
            txbReservationFee.Text = String.Format("{0:n2}", _dimdblNumber)

            Call subCalculate()

            btnReset.Focus()
        Else
            txbReservationFee.Text = ""
        End If
    End Sub
#End Region

    'etc...
#End Region
End Class
