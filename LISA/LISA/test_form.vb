Imports MySql.Data.MySqlClient

Public Class test_form
    Private WithEvents btn As New GenericButton With {.Location = New Point(35, 85), .Size = New Size(330, 50), .borderColor = Color.White, .hoverColor = Color.FromArgb(255, 211, 211, 211), .Tag = "REGISTER ONLY"}
    Private WithEvents btn1 As New GenericButton With {.Location = New Point(35, 150), .Size = New Size(330, 50), .borderColor = Color.White, .hoverColor = Color.FromArgb(255, 211, 211, 211), .Tag = "COMPLETE"}

    Public Sub New()
        InitializeComponent()
        Me.Size = New Size(400, 235)
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.Yellow
        Me.TransparencyKey = Color.Yellow
        Me.AllowTransparency = True
        Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)

        Dim lbl As New Label With {.Text = "PURPOSE", .BackColor = Color.FromArgb(255, 211, 211, 211), .TextAlign = ContentAlignment.MiddleCenter, .Font = New Font("Calibri", 24, FontStyle.Bold), .Width = 380, .ForeColor = Color.Pink, .Height = 50}
        lbl.Location = New Point(Me.ClientRectangle.Width \ 2 - lbl.Width \ 2, 25 - lbl.Height \ 2 - 1)

        Me.Controls.AddRange(New Control() {btn, btn1})
    End Sub

    Private Sub CreateControlBox(ByVal frm As Form)
        For i = 0 To 2
            Dim btn As New ControlBoxButton
            btn.Location = New Point(frm.ClientRectangle.Width - 30 * (i + 1) + 1, -1)
            btn.Name = "GenericControlBoxButton_" & i
            btn.SetImage(i)
            btn.Size = New Size(30, 30)
            btn.Tag = "1" & i

            AddHandler btn.Click, AddressOf ControlMouseClick
            AddHandler btn.MouseEnter, AddressOf ControlMouseEnter
            AddHandler btn.MouseLeave, AddressOf ControlMouseLeave
            frm.Controls.Add(btn)
        Next
    End Sub

    'Not needed now that dataset sees primary key
    'Private Sub dgv_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs)
    '    Dim dgv As DataGridView = DirectCast(sender, DataGridView)

    '    If Application.OpenForms.OfType(Of Form).Contains(Me) Then 'Check if the form has already loaded (It adds 3 rows when loading for some reason)
    '        Dim row As DataGridViewRow = dgv.Rows(e.RowIndex - 1)
    '        row.Cells(0).Value = previousIndex + 1
    '        previousIndex += 1
    '    End If
    'End Sub

    'Failed eID Code
    'Private Sub GetCardInformation()
    '    Try
    '        Dim eID As New EIDlib
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    End Try

    '    Dim lHandle As Integer

    '    Dim strCardNumber As String
    '    Dim strChipNumber As String
    '    Dim strBegVal As Object
    '    Dim strBegValDag, strBegValMaand, strBegValJaar As String
    '    Dim strEndVal As Object
    '    Dim strEndValDag, strEndValMaand, strEndValJaar As String
    '    Dim strIssuingMunicipality As String

    '    Dim strName As String
    '    Dim strFirstname1 As String
    '    Dim strBirthPlace As String
    '    Dim strBirthDate As String
    '    Dim strGender As String
    '    Dim strNationality As String
    '    Dim strNationalNumber As String

    '    Dim strStreet As String
    '    Dim strZIPCode As String
    '    Dim strMunicipality As String

    '    Dim RetStatus As RetStatus
    '    Dim MapColPicture As New MapCollection
    '    Dim MapColID As New MapCollection
    '    Dim MapColAddress As New MapCollection
    '    Dim CertifCheck As New CertifCheck

    '    Dim picture As Bitmap

    '    Try
    '        'RetStatus = eID.Init("", 0, 0, lHandle)
    '        If RetStatus.GetGeneral = 0 Then
    '            'RetStatus = eID.GetID(MapColID, CertifCheck)
    '            strChipNumber = MapColID.GetValue("ChipNumber").ToString
    '            strCardNumber = MapColID.GetValue("CardNumber").ToString
    '            strBegVal = MapColID.GetValue("BeginValidityDate")
    '            strEndVal = MapColID.GetValue("EndValidityDate")
    '            strIssuingMunicipality = MapColID.GetValue("IssuingMunicipality").ToString
    '            strName = MapColID.GetValue("Name").ToString
    '            strFirstname1 = MapColID.GetValue("FirstName1").ToString
    '            strBirthDate = MapColID.GetValue("BirthDate").ToString
    '            strBirthPlace = MapColID.GetValue("BirthPlace").ToString
    '            strGender = MapColID.GetValue("Gender").ToString
    '            strNationality = MapColID.GetValue("Nationality").ToString
    '            strNationalNumber = MapColID.GetValue("NationalNumber").ToString

    '            'strBegValDag = strBegVal.ToString.Substring(0, 2)
    '            'strBegValMaand = strBegVal.ToString.Substring(5, 2)

    '            MsgBox("Begin Value: " & strBegVal.ToString)
    '            MsgBox("End Value: " & strEndVal.ToString)
    '        End If

    '        'RetStatus = eID.GetAddress(MapColAddress, CertifCheck)

    '        strStreet = MapColAddress.GetValue("Street").ToString
    '        strZIPCode = MapColAddress.GetValue("ZIPCode").ToString
    '        strMunicipality = MapColAddress.GetValue("Municipality").ToString

    '        'RetStatus = eID.GetPicture(MapColPicture, CertifCheck)
    '        picture = DirectCast(MapColPicture.GetValue("Picture"), Bitmap)
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub test_form_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit

        e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(255, 211, 211, 211)), 0, 0, Me.ClientRectangle.Width, 50)
        e.Graphics.DrawRectangle(New Pen(Color.FromArgb(255, 211, 211, 211)), 0, 0, Me.ClientRectangle.Width - 1, Me.ClientRectangle.Height - 1)
        e.Graphics.DrawString("PURPOSE", New Font("Calibri", 24, FontStyle.Bold), New SolidBrush(Color.Yellow), New Point(134, 7))
    End Sub

    Private Sub Button1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn.MouseEnter, btn1.MouseEnter
        DirectCast(sender, GenericButton).borderColor = Color.FromArgb(255, 211, 211, 211)
        Me.Refresh()
    End Sub

    Private Sub Button1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn.MouseLeave, btn1.MouseLeave
        DirectCast(sender, GenericButton).borderColor = Color.White
    End Sub

    Private Sub btn_Paint(sender As Object, e As PaintEventArgs) Handles btn.Paint
        Dim btn As GenericButton = DirectCast(sender, GenericButton)
        Dim strSize As SizeF = e.Graphics.MeasureString(btn.Tag.ToString, btn.Font)

        e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit
        e.Graphics.DrawString(btn.Tag.ToString, btn.Font, New SolidBrush(Color.Yellow), New PointF(btn.Width \ 2 - CLng(strSize.Width) \ 2, 25 - CLng(strSize.Height) \ 2))
    End Sub

    Private Sub btn1_Paint(sender As Object, e As PaintEventArgs) Handles btn1.Paint
        Dim btn As GenericButton = DirectCast(sender, GenericButton)
        Dim strSize As SizeF = e.Graphics.MeasureString(DirectCast(sender, Button).Tag.ToString, DirectCast(sender, Button).Font)

        e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit
        e.Graphics.DrawString(DirectCast(sender, Button).Tag.ToString, DirectCast(sender, Button).Font, New SolidBrush(Color.Yellow), New PointF(btn.Width \ 2 - CLng(strSize.Width) \ 2, 25 - CLng(strSize.Height) \ 2))
    End Sub
End Class