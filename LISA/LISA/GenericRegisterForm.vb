Imports MySql.Data.MySqlClient

Public Class GenericRegisterForm
    Inherits GenericForm
    Public Sub New()
        MyBase.New("GenericForm_Register", True)
        InitializeComponent()

        Me.Icon = My.Resources.LisaIconFlatIco

        AddHandler Me.FormClosing, AddressOf frm_FormClosing
    End Sub

    Private Sub AddButtons()
        Dim btnUpload As New GenericButton() With {.Location = New Point(Me.Width - 120, Me.Height - 70), .Name = "GenericButton_Upload", .Size = New Size(100, 50), .Text = "Submit"}
        Dim btnClear As New GenericButton() With {.Location = New Point(Me.Width - 240, Me.Height - 70), .Name = "GenericButton_Clear", .Size = New Size(100, 50), .Text = "Clear"}

        AddHandler btnUpload.Click, AddressOf GenericButtonUpload_Click
        AddHandler btnClear.Click, AddressOf GenericButtonClear_Click
        Me.Controls.AddRange(New Control() {btnUpload, btnClear})
    End Sub

    Private Sub AddLabel()
        Dim lbl As New ShowAndHideLabel("GenericLabel_Actions")
        lbl.Location = New Point(90, Me.Height - 30)
        Me.Controls.Add(lbl)
    End Sub

    Private Sub AddTextBoxes()
        For j = 0 To 6
            For i = 0 To 2
                Dim lcation As Point = New Point(90 + 400 * i, 70 + 75 * j)
                Dim c As Integer = i + (j * 3) 'Stores where the counter is at

                If registerTranslations.Rows(c)(10).ToString = "1" Then 'Create a new combobox
                    Dim cmb As New GenericCombobox("GenericCombobox" & c, lcation, c)
                    Me.Controls.Add(cmb)
                Else 'Create a new textbox
                    Dim txt As New GenericTextbox("GenericTextbox" & c, lcation)
                    AddHandler txt.LostFocus, AddressOf ValidateTextbox
                    Me.Controls.Add(txt)
                End If

                Dim lbl As New Label With {.Location = New Point(lcation.X, lcation.Y - 28), .TextAlign = ContentAlignment.BottomLeft, .Text = registerTranslations.Rows(c)(language).ToString & ":", .Width = 300}
                Me.Controls.Add(lbl)
            Next
        Next

        For Each ctrl In Me.Controls.OfType(Of GenericCombobox)()
            Select Case ctrl.Tag.ToString
                Case "2"
                    ctrl.Items.AddRange(New String() {"M", "V"})
                    ctrl.SelectedIndex = 0
                Case "5"
                    For i = 0 To allInformation(4).Tables(0).Rows.Count - 1
                        ctrl.Items.Add(allInformation(4).Tables(0).Rows(i)(2).ToString)
                    Next

                    ctrl.SelectedIndex = 0
                Case "13"
                    For i = 0 To allInformation(2).Tables(0).Rows.Count - 1
                        ctrl.Items.Add(allInformation(2).Tables(0).Rows(i)(1).ToString)
                    Next

                    ctrl.SelectedIndex = 0
                Case "14"
                    For i = 0 To allInformation(2).Tables(0).Rows.Count - 1
                        ctrl.Items.Add(allInformation(2).Tables(0).Rows(i)(1).ToString)
                    Next

                    ctrl.SelectedIndex = 0
                Case "15"
                    For i = 0 To allInformation(10).Tables(0).Rows.Count - 1
                        ctrl.Items.Add(allInformation(10).Tables(0).Rows(i)(2).ToString)
                    Next
                    ctrl.SelectedIndex = 0
                Case "16"
                    ctrl.Items.AddRange(New String() {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"})
                    ctrl.SelectedIndex = 0
                Case "18"
                    For i = 0 To allInformation(8).Tables(0).Rows.Count - 1
                        ctrl.Items.Add(allInformation(8).Tables(0).Rows(i)(1).ToString)
                    Next

                    ctrl.SelectedIndex = 0
                Case "20"
                    ctrl.Items.AddRange(New String() {"Nee", "Ja"})
                    ctrl.SelectedIndex = 0
            End Select
        Next

        Me.Controls("GenericTextbox12").Font = New Font(Me.Font.FontFamily, 12) 'Make the 'Email' textbox have a smaller font, so larger emails fit
        AddHandler Me.Controls("GenericCombobox20").KeyPress, AddressOf NumbersOnly_Keypress
        AddHandler Me.Controls("GenericTextbox4").KeyPress, AddressOf NumbersOnly_Keypress
        AddHandler Me.Controls("GenericTextbox3").KeyPress, AddressOf NumbersOnly_Keypress

        'Hacks        
    End Sub

    Private Sub OverriddenControlMouseClick(ByVal sender As Object, ByVal e As System.EventArgs)
        DirectCast(sender, ControlBoxButton).clicked(True)
        Main.Show()
    End Sub

    Private Sub frm_FormClosing(ByVal sender As Object, ByVal e As Windows.Forms.FormClosingEventArgs)
        If Globals.AppID = 1 Then
            Environment.Exit(0)
        End If
    End Sub

    Private Sub NumbersOnly_Keypress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        Dim int As Integer = Asc(e.KeyChar)

        If int < Asc("0") Or int > Asc("9") Then 'Disallow anything but numbers
            e.Handled = True
        End If

        If int = 8 Then 'Allow backspace
            e.Handled = False
        End If
    End Sub

    Private Sub GenericButtonUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If ValidationTextboxes(CType(DirectCast(sender, GenericButton).FindForm, GenericForm)) Then
            Dim frm As GenericForm = CType(DirectCast(sender, GenericButton).FindForm, GenericForm) 'Retrieve current form in var
            Dim lln As New Leerling 'New structure var

            lln.Voornaam = frm.Controls("GenericTextbox0").Text
            lln.Naam = frm.Controls("GenericTextbox1").Text
            lln.Geslacht = frm.Controls("GenericCombobox2").Text
            lln.Geboortedatum = ConvertToDate(frm.Controls("GenericTextbox3").Text)
            lln.Geboorteplaats = DirectCast(frm.Controls("GenericTextbox4"), GenericTextbox).ToInteger
            lln.NationaliteitId = DirectCast(frm.Controls("GenericCombobox5"), ComboBox).SelectedIndex
            lln.Straatnaam = frm.Controls("GenericTextbox6").Text
            lln.HuisNummer = frm.Controls("GenericTextbox7").Text
            lln.BusNummer = frm.Controls("GenericTextbox8").Text
            lln.PostcodeId = DirectCast(frm.Controls("GenericTextbox9"), GenericTextbox).ToInteger
            lln.RijksRegNummer = frm.Controls("GenericTextbox10").Text
            lln.TelMobiel = frm.Controls("GenericTextbox11").Text
            lln.Email = frm.Controls("GenericTextbox12").Text
            lln.MoedertaalId = DirectCast(frm.Controls("GenericCombobox13"), ComboBox).SelectedIndex
            lln.SpreektaalId = DirectCast(frm.Controls("GenericCombobox14"), ComboBox).SelectedIndex
            lln.KlasId = DirectCast(frm.Controls("GenericCombobox15"), ComboBox).SelectedIndex
            lln.BroZus = DirectCast(frm.Controls("GenericCombobox16"), ComboBox).SelectedIndex
            lln.ZDPersoneel = 0
            lln.CorrGerAan = 0
            lln.OpmIvmVerblijfsAd = frm.Controls("GenericTextbox17").Text
            lln.Rapport = 0
            lln.Attest = 0
            lln.Buitenpas = 0
            lln.GodsKeuzeId = DirectCast(frm.Controls("GenericCombobox18"), ComboBox).SelectedIndex
            lln.OpmIvmTucht = frm.Controls("GenericTextbox19").Text
            lln.InschrijvingsStatus = 0
            lln.InschrDatum = CDate(String.Format("{0:yyyy/MM/dd}", Date.Now))
            lln.InschrUur = String.Format("{0:yyyy/MM/dd}", Date.Now) & " " & Now.TimeOfDay.ToString
            lln.StudieToelage = DirectCast(frm.Controls("GenericCombobox20"), GenericCombobox).SelectedIndex
            lln.VerBuitGezin = 0
            lln.OudRondtrekBevol = 0
            lln.kerCirBinschip = 0

            Dim cmdInsert As New MySqlCommand
            cmdInsert.Parameters.AddWithValue("@value1", lln.Voornaam)
            cmdInsert.Parameters.AddWithValue("@value2", lln.Naam)
            cmdInsert.Parameters.AddWithValue("@value3", lln.Geslacht)
            cmdInsert.Parameters.AddWithValue("@value4", lln.Geboortedatum)
            cmdInsert.Parameters.AddWithValue("@value5", lln.Geboorteplaats)
            cmdInsert.Parameters.AddWithValue("@value6", lln.NationaliteitId)
            cmdInsert.Parameters.AddWithValue("@value7", lln.Straatnaam)
            cmdInsert.Parameters.AddWithValue("@value8", lln.HuisNummer)
            cmdInsert.Parameters.AddWithValue("@value9", lln.BusNummer)
            cmdInsert.Parameters.AddWithValue("@value10", lln.PostcodeId)
            cmdInsert.Parameters.AddWithValue("@value11", lln.RijksRegNummer)
            cmdInsert.Parameters.AddWithValue("@value12", lln.TelMobiel)
            cmdInsert.Parameters.AddWithValue("@value13", lln.Email)
            cmdInsert.Parameters.AddWithValue("@value14", lln.MoedertaalId)
            cmdInsert.Parameters.AddWithValue("@value15", lln.SpreektaalId)
            cmdInsert.Parameters.AddWithValue("@value16", lln.KlasId)
            cmdInsert.Parameters.AddWithValue("@value17", lln.BroZus)
            cmdInsert.Parameters.AddWithValue("@value18", lln.ZDPersoneel)
            cmdInsert.Parameters.AddWithValue("@value19", lln.CorrGerAan)
            cmdInsert.Parameters.AddWithValue("@value20", lln.OpmIvmVerblijfsAd)
            cmdInsert.Parameters.AddWithValue("@value21", lln.Rapport)
            cmdInsert.Parameters.AddWithValue("@value22", lln.Attest)
            cmdInsert.Parameters.AddWithValue("@value23", lln.Buitenpas)
            cmdInsert.Parameters.AddWithValue("@value24", lln.GodsKeuzeId)
            cmdInsert.Parameters.AddWithValue("@value25", lln.OpmIvmTucht)
            cmdInsert.Parameters.AddWithValue("@value26", lln.InschrijvingsStatus)
            cmdInsert.Parameters.AddWithValue("@value27", lln.InschrDatum)
            cmdInsert.Parameters.AddWithValue("@value28", lln.InschrUur)
            cmdInsert.Parameters.AddWithValue("@value29", lln.StudieToelage)
            cmdInsert.Parameters.AddWithValue("@value30", lln.VerBuitGezin)
            cmdInsert.Parameters.AddWithValue("@value31", lln.OudRondtrekBevol)
            cmdInsert.Parameters.AddWithValue("@value32", lln.kerCirBinschip)

            Dim sqlString As String = "INSERT INTO lisa_leerling (Voornaam, Naam, Geslacht, Geboortedatum, Geboorteplaats, NationaliteitID, Straatnaam, Nummer, Busnummer, PostcodeID, RijksRegNummer, TelMobiel, Email, MoedertaalID, SpreektaalID, KlasID, BroZus, ZDPersoneel, CorrGerAan, OpmIvmVerblijfsAd, Rapport, Attest, Buitenpas, GodsKeuzeID, OpmIvmTucht, InschrijvingsStatus, inschrdatum, inschruur, StudieToelage, VerBuitGezin, OudRondtrekBevol, KerCirBinschip) VALUES (@value1, @value2, @value3, @value4, @Value5, @Value6, @Value7, @Value8, @Value9, @Value10, @Value11, @Value12, @Value13, @Value14, @Value15, @Value16, @Value17, @Value18, @Value19, @Value20, @Value21, @Value22, @Value23, @Value24, @Value25, @Value26, @Value27, @Value28, @Value29, @Value30, @Value31, @Value32);"
            cmdInsert.CommandText = sqlString
            cmdInsert.Connection = getConn()

            Dim iSqlStatus As Integer
            OpenConnection()
            iSqlStatus = cmdInsert.ExecuteNonQuery

            If iSqlStatus = 0 Then
                CType(DirectCast(sender, GenericButton).FindForm.Controls("GenericLabel_Actions"), ShowAndHideLabel).SetText("Error: Upload failed.", 5)
            Else
                CType(DirectCast(sender, GenericButton).FindForm.Controls("GenericLabel_Actions"), ShowAndHideLabel).SetText("Successfully submitted the information.", 3)
            End If
        End If
    End Sub

    Private Sub GenericButtonClear_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Controls("GenericTextbox0").Focus()
        For Each ctrl In Me.Controls.OfType(Of GenericTextbox)()
            ctrl.Clear()
            DrawErrorBox(Me.CreateGraphics, ctrl, Me.BackColor)
        Next
        For Each ctrl In Me.Controls.OfType(Of GenericCombobox)()
            ctrl.SelectedIndex = 0
        Next
        Me.Controls("GenericTextbox0").Focus()
    End Sub

    Private Sub ValidateTextbox(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim txt As GenericTextbox = DirectCast(sender, GenericTextbox) 'Temp var for textbox

        Dim NotRequired As Boolean = txt.Name <> "GenericTextbox8" And txt.Name <> "GenericTextbox17" And txt.Name <> "GenericTextbox19"

        If txt.Text = "" And NotRequired Then
            DrawErrorBox(txt.FindForm.CreateGraphics, txt, Color.FromArgb(255, 178, 0, 0))
        ElseIf NotRequired Then
            If txt.Name = "GenericTextbox3" Then 'Execute date check
                If DateCheck(txt) Then
                    DrawErrorBox(txt.FindForm.CreateGraphics, txt, Color.FromArgb(255, 0, 128, 0))
                Else
                    DrawErrorBox(txt.FindForm.CreateGraphics, txt, Color.FromArgb(255, 178, 0, 0))
                End If
            Else
                DrawErrorBox(txt.FindForm.CreateGraphics, txt, Color.FromArgb(255, 0, 128, 0))
            End If
        End If
    End Sub

    Private Function DateCheck(ByVal txt As GenericTextbox) As Boolean
        Dim year As Integer = CInt(txt.Text.Substring(0, 4))
        Dim month As Integer = CInt(txt.Text.Substring(4, 2))
        Dim day As Integer = CInt(txt.Text.Substring(6, 2))
        Dim time As String = year & month & day

        Dim valid As Boolean = year <= 2050 And year >= 1970 And month <= 12 And month >= 1 And day <= 31 And day >= 1

        If month = 4 Or month = 6 Or month = 9 Or month = 11 And day = 31 Then valid = False 'If the month is only 30 long and the day is 31th then you have a problem
        If month = 2 And day > 29 Then valid = False 'If it's february then there's no 29th, 30th or 31th (unless it's a leap year)
        If Not Date.IsLeapYear(year) And month = 2 And day = 29 Then valid = False 'If it's not a leap year then february 29th can't be

        Return valid
    End Function

    Private Function ValidationTextboxes(ByVal frm As GenericForm) As Boolean
        Dim allValid As Boolean = True

        For Each txt In frm.Controls.OfType(Of GenericTextbox)()
            If txt.Text = "" And txt.Name <> "GenericTextbox8" And txt.Name <> "GenericTextbox17" And txt.Name <> "GenericTextbox19" Then
                DrawErrorBox(frm.CreateGraphics, txt, Color.FromArgb(255, 178, 0, 0))
                txt.Tag = "hadErrored"
                allValid = False
            ElseIf txt.Name = "GenericTextbox3" Then
                If Not DateCheck(txt) Then
                    DrawErrorBox(frm.CreateGraphics, txt, Color.FromArgb(255, 178, 0, 0))
                    allValid = False
                End If
            ElseIf txt.Tag Is "hadErrored" Then
                DrawErrorBox(frm.CreateGraphics, txt, Color.FromArgb(255, 0, 128, 0))
                txt.Tag = Nothing
            End If
        Next

        frm.Controls("GenericTextbox0").Focus()

        Return allValid
    End Function

    Private Sub DrawErrorBox(ByVal graph As Graphics, ByVal txt As GenericTextbox, ByVal color As Color)
        graph.FillRectangle(New SolidBrush(color), New Rectangle(New Point(txt.Location.X - 2, txt.Location.Y - 2), New Size(txt.Size.Width + 4, txt.Size.Height + 4)))
    End Sub

    Private Function ConvertToDate(ByVal str As String) As String
        Dim year As String = str.Substring(0, 4)
        Dim month As String = str.Substring(4, 2)
        Dim day As String = str.Substring(6, 2)

        Return year & "-" & month & "-" & day
    End Function

    Private Sub GenericRegisterForm_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If AppID = 1 Then
            Globals.Init()
            Globals.GetDataFromServer()
        End If

        AddButtons()
        AddTextBoxes()
        AddLabel()

        If AppID <> 1 Then
            RemoveHandler Me.Controls("GenericControlBoxButtonClose").Click, AddressOf FormDesignAndControl.ControlMouseClick
            AddHandler Me.Controls("GenericControlBoxButtonClose").Click, AddressOf OverriddenControlMouseClick
        End If
    End Sub
End Class