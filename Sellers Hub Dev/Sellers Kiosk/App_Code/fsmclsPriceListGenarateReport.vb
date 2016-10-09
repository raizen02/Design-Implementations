'**************************************************************
'Programmer Name		: Nestor Garais Jr
'Date Created			: 2015.05.25
'Finished Date          : 
'Program Name           : fsmclsPriceListGenarateReport
'Program Description    : Global class to process then gererate price list
'Remarks                : May use in Portal also
'**************************************************************
'Updates Information    : Nestor Garais Jr  | JO#  
'Date Updated           : 2015-08-10
'Changes                : add paramter for page footer suppress, cangrow, height adjust programmatically
'Remarks                : Dev -  | Prod -  2016-05-10 
'***********************************************************************
'Updates Information    : Nestor Garais Jr  | JO#  
'Date Updated           : 2016-06-15
'Changes                : Remove page footer sub report and fields
'Remarks                : Dev -  2016-06-15 | Prod - 2016-06-16  
'***********************************************************************
'Updates Information    : Nestor Garais Jr  | JO#  
'Date Updated           : 2016-06-27
'Changes                : Use using block to dispose sqlconnection
'                         Add Implements IDisposable to dispose ReportDocument after used
'Remarks                : Dev -   2016-06-27 | Prod -   
'***********************************************************************
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Drawing
Imports System.Data
Imports System

Public Class fsmclsPriceListGenarateReport
    Implements IDisposable

    Private _prirpdPricelistReportDOc As ReportDocument
    Private _pristrErrorMessage As String = ""
    Private _priSqlDA As SqlDataAdapter
    Private _priDtsRptSource As DataSet
    Private _prisqlCommand As SqlCommand
    Private _priFont As Font

    Public Enum ReportType
        Standard = 1
        PreviewOnly = 2
    End Enum

    Public ReadOnly Property PriceListReportDocument() As ReportDocument
        Get
            Return _prirpdPricelistReportDOc
        End Get
    End Property

    Public ReadOnly Property PriceListErrorMessage() As String
        Get
            Return _pristrErrorMessage
        End Get
    End Property

    Public Function GenerateReport(ByVal _pstrConnectionString As String, _
                                   ByVal _prpdPRiceListDoc As ReportDocument, _
                                   ByVal _parIntMode As ReportType, ByVal _parstrProjectCode As String, _
                                   Optional ByVal _parstrPhaseCode As String = "", _
                                   Optional ByVal _parStatusAllocation As String = "", _
                                   Optional ByVal _parstrUsername As String = "", _
                                   Optional ByVal _parstrPriceListCode As String = "", _
                                   Optional ByVal _parstrXmlPricelistInventoryUnit As String = "", _
                                   Optional ByVal _parstrXmlPricelistInventoryUnitDetails As String = "", _
                                   Optional ByVal _parstrXmlPricelistInventoryUnitDetailsDiscount As String = "", _
                                   Optional ByVal _parstrXmlPricelistInventoryUnitDetailsDP As String = "", _
                                   Optional ByVal _parstrXmlPricelistInventoryUnitDetailsICR As String = "") As Boolean
        GenerateReport = True

        Using _dimSqlCon As New SqlClient.SqlConnection(_pstrConnectionString)
            _dimSqlCon.Open()
            _prisqlCommand = New SqlCommand
            _prirpdPricelistReportDOc = New ReportDocument

            Try
                With _prisqlCommand
                    .Connection = _dimSqlCon
                    ' .Transaction = _priSqlTransaction
                    .CommandText = "SP_fsmrptPriceList_V2"
                    .CommandType = CommandType.StoredProcedure
                    .CommandTimeout = 0

                    .Parameters.AddWithValue("@pintMode", _parIntMode)
                    .Parameters.AddWithValue("@pstrProjectCode", _parstrProjectCode)
                    .Parameters.AddWithValue("@pstrPhaseCode", _parstrPhaseCode)
                    .Parameters.AddWithValue("@pstrStatusAllocation", _parStatusAllocation)
                    .Parameters.AddWithValue("@pstrUserName", _parstrUsername)
                    .Parameters.AddWithValue("@pstrPriceListCode", _parstrPriceListCode)
                    .Parameters.AddWithValue("@pxmlPricelistInventoryUnit", _parstrXmlPricelistInventoryUnit)
                    .Parameters.AddWithValue("@pxmlPricelistInventoryUnitDetails", _parstrXmlPricelistInventoryUnitDetails)
                    .Parameters.AddWithValue("@pxmlPricelistInventoryUnitDetailsDiscount", _parstrXmlPricelistInventoryUnitDetailsDiscount)
                    .Parameters.AddWithValue("@pxmlPricelistInventoryUnitDetailsDP", _parstrXmlPricelistInventoryUnitDetailsDP)
                    .Parameters.AddWithValue("@pxmlPricelistInventoryUnitDetailsICR", _parstrXmlPricelistInventoryUnitDetailsICR)

                    _priSqlDA = New SqlDataAdapter(_prisqlCommand)
                    _priDtsRptSource = New DataSet
                    _priSqlDA.Fill(_priDtsRptSource)

                    ' Subreport first
                    _prpdPRiceListDoc.Subreports("SubRptReportFooterPromoNote").SetDataSource(_priDtsRptSource.Tables(5))
                    _prpdPRiceListDoc.SetDataSource(_priDtsRptSource.Tables(0).Copy)
                    _prpdPRiceListDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLegal
                    _prpdPRiceListDoc.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape

                    Dim _dimintRowNumber As Integer

                    '' Header properties
                    For Each _Row As DataRow In _priDtsRptSource.Tables(1).Rows 'TextObjectName  ,TextObjectValue   ,HeaderLevel ,Width ,Height  ,TopLocation   ,LeftLocation 
                        ' Try
                        ' _dimintRowNumber = _dimintRowNumber + 1
                        With CType(_prpdPRiceListDoc.ReportDefinition.Sections("PriceListHeaderSection").ReportObjects(_Row.Item("TextObjectName")), FieldObject)
                            .Width = _Row.Item("Width")
                            .Height = _Row.Item("Height")
                            .Top = _Row.Item("TopLocation")
                            .Left = _Row.Item("LeftLocation")
                            _priFont = New Font(_Row.Item("FontName").ToString, _Row.Item("FontSizeEM"), FontStyle.Bold, GraphicsUnit.Point)
                            .ApplyFont(_priFont)

                            .Border.BottomLineStyle = CrystalDecisions.Shared.LineStyle.SingleLine
                            .Border.TopLineStyle = CrystalDecisions.Shared.LineStyle.SingleLine
                            .Border.LeftLineStyle = CrystalDecisions.Shared.LineStyle.SingleLine
                            .Border.RightLineStyle = CrystalDecisions.Shared.LineStyle.SingleLine

                            .Border.BackgroundColor = Color.FromArgb(_Row.Item("BackgroundColor_R").ToString, _
                                                      _Row.Item("BackgroundColor_G").ToString, _
                                                      _Row.Item("BackgroundColor_B").ToString)
                            .Border.BorderColor = Color.FromName(_Row.Item("BorderColor").ToString)
                            .Color = Color.FromName(_Row.Item("TextColor").ToString)

                            .ObjectFormat.EnableSuppress = _Row.Item("Supress")
                            .ObjectFormat.EnableCanGrow = False
                            .ObjectFormat.HorizontalAlignment = _Row.Item("DetailsAlignment") ' CrystalDecisions.Shared.Alignment.RightAlign    ' CrystalDecisions.Shared.Alignment.HorizontalCenterAlign
                        End With
                    Next

                    ''Details Properties
                    _dimintRowNumber = 0

                    For Each _Row As DataRow In _priDtsRptSource.Tables(2).Rows  'TextObjectName  ,TextObjectValue   ,HeaderLevel ,Width ,Height  ,TopLocation   ,LeftLocation 
                        ' Try
                        _dimintRowNumber = _dimintRowNumber + 1

                        With CType(_prpdPRiceListDoc.ReportDefinition.Sections("PriceListDetailsSection").ReportObjects(_Row.Item("TextObjectName")), TextObject)
                            _priFont = New Font(_Row.Item("FontName").ToString, _Row.Item("FontSizeEM"), FontStyle.Regular, GraphicsUnit.Point)
                            .ApplyFont(_priFont)
                            .Width = _Row.Item("Width")
                            .Top = _Row.Item("TopLocation")
                            .Left = _Row.Item("LeftLocation")
                            .Height = _Row.Item("Height")
                            .ObjectFormat.EnableSuppress = _Row.Item("Supress")
                            .ObjectFormat.EnableCanGrow = False
                            .ObjectFormat.HorizontalAlignment = _Row.Item("DetailsAlignment")

                            ' .Border.BottomLineStyle = CrystalDecisions.Shared.LineStyle.SingleLine
                            ' .Border.TopLineStyle = CrystalDecisions.Shared.LineStyle.SingleLine

                            If _dimintRowNumber = 1 Then
                                .Border.LeftLineStyle = CrystalDecisions.Shared.LineStyle.SingleLine
                            End If

                            .Border.RightLineStyle = CrystalDecisions.Shared.LineStyle.SingleLine
                            .Border.BorderColor = Color.FromName(_Row.Item("BorderColor").ToString)
                            .Color = Color.FromName(_Row.Item("TextColor").ToString)
                        End With
                        'Catch ex As Exception
                        'Debug.Print(ex.Message)
                        'End Try
                    Next

                    ' Super SCript 
                    _dimintRowNumber = 0

                    For Each _Row As DataRow In _priDtsRptSource.Tables(3).Rows 'TextObjectName  ,TextObjectValue   ,HeaderLevel ,Width ,Height  ,TopLocation   ,LeftLocation 
                        'Try
                        _dimintRowNumber = _dimintRowNumber + 1

                        With CType(_prpdPRiceListDoc.ReportDefinition.Sections("PriceListDetailsSection").ReportObjects(_Row.Item("TextObjectName")), TextObject)
                            _priFont = New Font(_Row.Item("FontName").ToString, _Row.Item("FontSizeEM"), FontStyle.Bold, GraphicsUnit.Point)
                            .ApplyFont(_priFont)
                            .Width = _Row.Item("Width")
                            .Top = _Row.Item("TopLocation")
                            .Left = _Row.Item("LeftLocation")
                            .Height = _Row.Item("Height")

                            ' .Border.LeftLineStyle = CrystalDecisions.Shared.LineStyle.SingleLine
                            .Border.RightLineStyle = CrystalDecisions.Shared.LineStyle.SingleLine
                            .Border.BorderColor = Color.FromName(_Row.Item("BorderColor").ToString)

                            If _dimintRowNumber = 1 Then
                                .Border.LeftLineStyle = CrystalDecisions.Shared.LineStyle.SingleLine
                            End If

                            .ObjectFormat.EnableSuppress = _Row.Item("Supress")
                            .ObjectFormat.EnableCanGrow = False
                            .ObjectFormat.HorizontalAlignment = _Row.Item("DetailsAlignment")
                        End With

                        'Catch ex As Exception
                        'Debug.Print(ex.Message)
                        '  End Try
                    Next

                    'H_BrandLogoImage
                    With CType(_prpdPRiceListDoc.ReportDefinition.Sections("PriceListPageHeaderBrandSection").ReportObjects("H_BrandLogoImage"), BlobFieldObject)
                        .Top = _priDtsRptSource.Tables(4).Rows(0).Item("H_BrandLogoImage_Top")
                        .Left = _priDtsRptSource.Tables(4).Rows(0).Item("H_BrandLogoImage_Left")
                        .Width = _priDtsRptSource.Tables(4).Rows(0).Item("H_BrandLogoImage_Width")
                        .Height = _priDtsRptSource.Tables(4).Rows(0).Item("H_BrandLogoImage_Height")
                    End With

                    'H_TextObjBrandBGBar 
                    With CType(_prpdPRiceListDoc.ReportDefinition.Sections("PriceListPageHeaderBrandSection").ReportObjects("H_TextObjBrandBGBar"), TextObject)
                        .Top = _priDtsRptSource.Tables(4).Rows(0).Item("H_TextObjBrandBGBar_Top")
                        .Left = _priDtsRptSource.Tables(4).Rows(0).Item("H_TextObjBrandBGBar_Left")
                        .Width = _priDtsRptSource.Tables(4).Rows(0).Item("H_TextObjBrandBGBar_Width")
                        .Height = _priDtsRptSource.Tables(4).Rows(0).Item("H_TextObjBrandBGBar_Height")
                        .Border.BackgroundColor = Color.FromArgb(_priDtsRptSource.Tables(4).Rows(0).Item("BGColorRed"), _
                                                  _priDtsRptSource.Tables(4).Rows(0).Item("BGColorGreen"), _
                                                  _priDtsRptSource.Tables(4).Rows(0).Item("BGColorBlue"))
                    End With

                    'H_TextObjProjectBorder 
                    With CType(_prpdPRiceListDoc.ReportDefinition.Sections("PriceListPageHeaderProjectSection").ReportObjects("H_TextObjProjectBorder"), TextObject)
                        .Top = _priDtsRptSource.Tables(4).Rows(0).Item("H_TextObjProjectBorder_Top")
                        .Left = _priDtsRptSource.Tables(4).Rows(0).Item("H_TextObjProjectBorder_Left")
                        .Width = _priDtsRptSource.Tables(4).Rows(0).Item("H_TextObjProjectBorder_Width")
                        .Height = _priDtsRptSource.Tables(4).Rows(0).Item("H_TextObjProjectBorder_Height")
                    End With

                    'H_ProjectLogoImage
                    With CType(_prpdPRiceListDoc.ReportDefinition.Sections("PriceListPageHeaderProjectSection").ReportObjects("H_ProjectLogoImage"), BlobFieldObject)
                        .Top = _priDtsRptSource.Tables(4).Rows(0).Item("H_ProjectLogoImage_Top")
                        .Left = _priDtsRptSource.Tables(4).Rows(0).Item("H_ProjectLogoImage_Left")
                        .Width = _priDtsRptSource.Tables(4).Rows(0).Item("H_ProjectLogoImage_Width")
                        .Height = _priDtsRptSource.Tables(4).Rows(0).Item("H_ProjectLogoImage_Height")
                    End With

                    'H_ProjectName
                    With CType(_prpdPRiceListDoc.ReportDefinition.Sections("PriceListPageHeaderProjectSection").ReportObjects("H_ProjectName"), FieldObject)
                        .Top = _priDtsRptSource.Tables(4).Rows(0).Item("H_ProjectName_Top")
                        .Left = _priDtsRptSource.Tables(4).Rows(0).Item("H_ProjectName_Left")
                    End With

                    'H_TxbObjProjLabel
                    With CType(_prpdPRiceListDoc.ReportDefinition.Sections("PriceListPageHeaderProjectSection").ReportObjects("H_TxbObjProjLabel"), TextObject)
                        .Top = _priDtsRptSource.Tables(4).Rows(0).Item("H_ProjectName_Top")
                        .Left = _priDtsRptSource.Tables(4).Rows(0).Item("H_GeneralTxbStartLeft")
                    End With

                    'H_PhaseLocation
                    With CType(_prpdPRiceListDoc.ReportDefinition.Sections("PriceListPageHeaderProjectSection").ReportObjects("H_PhaseLocation"), FieldObject)
                        .Top = _priDtsRptSource.Tables(4).Rows(0).Item("H_PhaseLocation_Top")
                        .Left = _priDtsRptSource.Tables(4).Rows(0).Item("H_PhaseLocation_Left")
                    End With

                    'H_TxbObjLocation
                    With CType(_prpdPRiceListDoc.ReportDefinition.Sections("PriceListPageHeaderProjectSection").ReportObjects("H_TxbObjLocation"), TextObject)
                        .Top = _priDtsRptSource.Tables(4).Rows(0).Item("H_PhaseLocation_Top")
                        .Left = _priDtsRptSource.Tables(4).Rows(0).Item("H_GeneralTxbStartLeft")
                    End With

                    'H_PriceListAsOf
                    With CType(_prpdPRiceListDoc.ReportDefinition.Sections("PriceListPageHeaderProjectSection").ReportObjects("H_PriceListAsOf"), FieldObject)
                        .Top = _priDtsRptSource.Tables(4).Rows(0).Item("H_PriceListAsOf_Top")
                        .Left = _priDtsRptSource.Tables(4).Rows(0).Item("H_PriceListAsOf_Left")
                    End With

                    'H_TxbObjPriceAsOF
                    With CType(_prpdPRiceListDoc.ReportDefinition.Sections("PriceListPageHeaderProjectSection").ReportObjects("H_TxbObjPriceAsOF"), TextObject)
                        .Top = _priDtsRptSource.Tables(4).Rows(0).Item("H_PriceListAsOf_Top")
                        .Left = _priDtsRptSource.Tables(4).Rows(0).Item("H_GeneralTxbStartLeft")
                    End With

                    'H_AllocationRemarks
                    With CType(_prpdPRiceListDoc.ReportDefinition.Sections("PriceListPageHeaderProjectSection").ReportObjects("H_AllocationRemarks"), FieldObject)
                        .Top = _priDtsRptSource.Tables(4).Rows(0).Item("H_AllocationRemarks_Top")
                        .Left = _priDtsRptSource.Tables(4).Rows(0).Item("H_GeneralTxbStartLeft")
                    End With

                    'H_PromoRemarks
                    With CType(_prpdPRiceListDoc.ReportDefinition.Sections("PriceListPageHeaderProjectSection").ReportObjects("H_PromoRemarks"), FieldObject)
                        .Top = _priDtsRptSource.Tables(4).Rows(0).Item("H_PromoRemarks_Top")
                        .Left = _priDtsRptSource.Tables(4).Rows(0).Item("H_PromoRemarks_Left")
                    End With

                    'D_LinePhaseLevel()
                    With CType(_prpdPRiceListDoc.ReportDefinition.Sections("LineSectionPhaseLevel").ReportObjects("D_LinePhaseLevel"), LineObject)
                        .Left = _priDtsRptSource.Tables(4).Rows(0).Item("D_LinePhaseLevelLeftLocation")
                        .Right = _priDtsRptSource.Tables(4).Rows(0).Item("D_LinePhaseLevelMaxWidth")
                        .EnableExtendToBottomOfSection = True
                    End With

                    'D_LineBlkFloorLevel
                    With CType(_prpdPRiceListDoc.ReportDefinition.Sections("LineSectionBlkFloorLevel").ReportObjects("D_LineBlkFloorLevel"), LineObject)
                        .Left = _priDtsRptSource.Tables(4).Rows(0).Item("D_LineBlkFloorLevelLeftLocation")
                        .Right = _priDtsRptSource.Tables(4).Rows(0).Item("D_LineBlkFloorLevelMaxWidth")
                        .EnableExtendToBottomOfSection = True
                    End With

                    'D_LineUnitLevel
                    With CType(_prpdPRiceListDoc.ReportDefinition.Sections("LineSectionUnitLevel").ReportObjects("D_LineUnitLevel"), LineObject)
                        .Left = _priDtsRptSource.Tables(4).Rows(0).Item("D_LineUnitLevelLeftLocation")
                        .Right = _priDtsRptSource.Tables(4).Rows(0).Item("D_LineUnitLevelMaxWidth")
                        .EnableExtendToBottomOfSection = True
                    End With

                    'D_LineUnitTypeLevel
                    With CType(_prpdPRiceListDoc.ReportDefinition.Sections("LineSectionUnitTypeLevel").ReportObjects("D_LineUnitTypeLevel"), LineObject)
                        .Left = _priDtsRptSource.Tables(4).Rows(0).Item("D_LineUnitTypeLevelLeftLocation")
                        .Right = _priDtsRptSource.Tables(4).Rows(0).Item("D_LineUnitTypeLevelMaxWidth")
                        .EnableExtendToBottomOfSection = True
                    End With

                    ''H_TxbObjHeaderBorder
                    With CType(_prpdPRiceListDoc.ReportDefinition.Sections("PriceListHeaderSection").ReportObjects("H_TxbObjHeaderBorder"), TextObject)
                        .Top = _priDtsRptSource.Tables(4).Rows(0).Item("H_TxbObjHeaderBorder_Top")
                        .Left = _priDtsRptSource.Tables(4).Rows(0).Item("H_TxbObjHeaderBorder_Left")
                        .Width = _priDtsRptSource.Tables(4).Rows(0).Item("H_TxbObjHeaderBorder_Width")
                        .Height = _priDtsRptSource.Tables(4).Rows(0).Item("H_TxbObjHeaderBorder_Height")
                    End With

                    ''D_TextObjBGColor
                    With CType(_prpdPRiceListDoc.ReportDefinition.Sections("PriceListDetailsSectionBG").ReportObjects("D_TextObjBGColor"), TextObject)
                        .Top = _priDtsRptSource.Tables(4).Rows(0).Item("D_TxbObjAltBG_Top")
                        .Left = _priDtsRptSource.Tables(4).Rows(0).Item("D_TxbObjAltBG_Left")
                        .Width = _priDtsRptSource.Tables(4).Rows(0).Item("D_TxbObjAltBG_Width")
                        .Height = _priDtsRptSource.Tables(4).Rows(0).Item("D_TxbObjAltBG_Height")
                    End With

                    'RF_TxbObjNoteBorder
                    With CType(_prpdPRiceListDoc.ReportDefinition.Sections("GroupFooterSectionNote").ReportObjects("RF_TxbObjNoteBorder"), TextObject)
                        .Top = _priDtsRptSource.Tables(4).Rows(0).Item("RF_TxbObjNoteBorder_Top")
                        .Left = _priDtsRptSource.Tables(4).Rows(0).Item("RF_TxbObjNoteBorder_Left")
                        .Width = _priDtsRptSource.Tables(4).Rows(0).Item("RF_TxbObjNoteBorder_Width")
                        .Height = _priDtsRptSource.Tables(4).Rows(0).Item("RF_TxbObjNoteBorder_Height")
                    End With

                    Dim _dimstrNotYetDefine As String = ""

                    For Each _forrptObj As ReportObject In _prpdPRiceListDoc.ReportDefinition.Sections("PriceListDetailsSection").ReportObjects
                        If _priDtsRptSource.Tables(2).Select("TextObjectName = '" & _forrptObj.Name & "'").Length = 0 And _
                           _priDtsRptSource.Tables(3).Select("TextObjectName = '" & _forrptObj.Name & "'").Length = 0 Then
                            _dimstrNotYetDefine &= _forrptObj.Name & ";"
                        End If
                    Next

                    If _dimstrNotYetDefine <> "" Then
                        _pristrErrorMessage = "Error while generating report, some objects failed to format :" & _dimstrNotYetDefine

                        Return False
                    End If

                    'Object's properties that setup once only
                    With _prpdPRiceListDoc.ReportDefinition
                        .Sections("PriceListDetailsSection").Height = _priDtsRptSource.Tables(4).Rows(0).Item("D_DetaiSectionHeight")   ' --300 ' if occurs here, some of detail text were not apply its location, invalid object name
                        .Sections("PriceListHeaderSection").Height = _priDtsRptSource.Tables(4).Rows(0).Item("H_HeaderSectionHeight")
                        .Sections("PriceListDetailsSectionBG").Height = _priDtsRptSource.Tables(4).Rows(0).Item("D_DetaiSectionHeight")   ' --300
                        .Sections("PriceListPageHeaderBrandSection").Height = _priDtsRptSource.Tables(4).Rows(0).Item("H_PriceListPageHeaderBrandSection")   ' --940
                        .Sections("PriceListPageHeaderProjectSection").Height = _priDtsRptSource.Tables(4).Rows(0).Item("H_PriceListPageHeaderProjectSection")   ' --2000

                        .Sections("LineSectionPhaseLevel").Height = _priDtsRptSource.Tables(4).Rows(0).Item("D_DetaiSectionHeight")
                        .Sections("LineSectionBlkFloorLevel").Height = _priDtsRptSource.Tables(4).Rows(0).Item("D_DetaiSectionHeight")
                        .Sections("LineSectionUnitLevel").Height = _priDtsRptSource.Tables(4).Rows(0).Item("D_DetaiSectionHeight")
                        .Sections("LineSectionUnitTypeLevel").Height = _priDtsRptSource.Tables(4).Rows(0).Item("D_DetaiSectionHeight")

                        .Sections("ReportFooterSectionNote").Height = _priDtsRptSource.Tables(4).Rows(0).Item("RF_SectionHeight")
                    End With

                    _prirpdPricelistReportDOc = _prpdPRiceListDoc
                End With
            Catch ex As Exception
                _pristrErrorMessage = MyExceptionNotice

                Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
                GenerateReport = False
            Finally
                If _priSqlDA IsNot Nothing Then _priSqlDA.Dispose()
                If _priDtsRptSource IsNot Nothing Then _priDtsRptSource.Dispose()
                If _prisqlCommand IsNot Nothing Then _prisqlCommand.Dispose()
            End Try
        End Using
    End Function



#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                If _priSqlDA IsNot Nothing Then _priSqlDA.Dispose()
                If _priDtsRptSource IsNot Nothing Then _priDtsRptSource.Dispose()
                If _prisqlCommand IsNot Nothing Then _prisqlCommand.Dispose()
                If _priFont IsNot Nothing Then _priFont.Dispose()
                If _prirpdPricelistReportDOc IsNot Nothing Then _prirpdPricelistReportDOc.Dispose()

                If PriceListReportDocument IsNot Nothing Then PriceListReportDocument.Dispose()
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
