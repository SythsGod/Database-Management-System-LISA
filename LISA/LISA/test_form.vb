Imports MySql.Data.MySqlClient
Imports EIDLIBCTRLLib

Public Class test_form
    Private MouseIsDown As Boolean
    Private AlreadyLocked As Boolean
    Private setPoint As Point

    Public Sub New()
        InitializeComponent()
        Me.Size = New Size(1280, 720)
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.White

        GetServerVars()
        CreateControlBox(Me)
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

    Private Sub Button_Upload(ByVal sender As Object, ByVal e As System.EventArgs)
        'If Not ValidationTextboxes() Then Exit Sub
        ValidationTextboxes()

        Dim lln As New Leerling

        lln.Voornaam = Me.Controls("GenericTextbox0").Text
        lln.Naam = Me.Controls("GenericTextbox1").Text
        lln.Geslacht = Me.Controls("GenericCombobox2").Text
        lln.Geboortedatum = ConvertToDate(Me.Controls("GenericTextbox3").Text)
        'MsgBox(lln.Geboortedatum.ToString)
        lln.Geboorteplaats = DirectCast(Me.Controls("GenericTextbox4"), GenericTextbox).ToInteger
        lln.NationaliteitId = DirectCast(Me.Controls("GenericCombobox5"), ComboBox).SelectedIndex
        lln.Straatnaam = Me.Controls("GenericTextbox6").Text
        lln.HuisNummer = Me.Controls("GenericTextbox7").Text
        lln.BusNummer = Me.Controls("GenericTextbox8").Text
        lln.PostcodeId = DirectCast(Me.Controls("GenericTextbox9"), GenericTextbox).ToInteger
        lln.RijksRegNummer = Me.Controls("GenericTextbox10").Text
        lln.TelMobiel = Me.Controls("GenericTextbox11").Text
        lln.Email = Me.Controls("GenericTextbox12").Text
        lln.MoedertaalId = DirectCast(Me.Controls("GenericCombobox13"), ComboBox).SelectedIndex
        lln.SpreektaalId = DirectCast(Me.Controls("GenericCombobox14"), ComboBox).SelectedIndex
        lln.KlasId = DirectCast(Me.Controls("GenericCombobox15"), ComboBox).SelectedIndex
        lln.BroZus = DirectCast(Me.Controls("GenericCombobox16"), ComboBox).SelectedIndex
        lln.ZDPersoneel = 0
        lln.CorrGerAan = 0
        lln.OpmIvmVerblijfsAd = Me.Controls("GenericTextbox17").Text
        lln.Rapport = 0
        lln.Attest = 0
        lln.Buitenpas = 0
        lln.GodsKeuzeId = DirectCast(Me.Controls("GenericCombobox18"), ComboBox).SelectedIndex
        lln.OpmIvmTucht = Me.Controls("GenericTextbox19").Text
        lln.InschrijvingsStatus = 0
        lln.InschrDatum = CDate(String.Format("{0:yyyy/MM/dd}", Date.Now))
        'MsgBox(lln.InschrDatum)
        lln.InschrUur = Date.Now.TimeOfDay
        'MsgBox(lln.InschrUur.Hours & ":" & lln.InschrUur.Minutes & ":" & lln.InschrUur.Seconds)
        lln.StudieToelage = DirectCast(Me.Controls("GenericTextbox20"), GenericTextbox).ToInteger
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
        cmdInsert.Connection = myConn

        Dim iSqlStatus As Integer
        OpenConnection()
        iSqlStatus = cmdInsert.ExecuteNonQuery
        MsgBox(iSqlStatus)
    End Sub

    Public Function ValidationTextboxes() As Boolean
        Dim allValid As Boolean = True

        For Each txt In Me.Controls.OfType(Of TextBox)()
            If txt.Text = "" And Not txt.Name = "GenericTextbox8" And Not txt.Name = "GenericTextbox17" And Not txt.Name = "GenericTextbox19" Then
                DrawErrorBox(Me.CreateGraphics, txt, Color.FromArgb(255, 178, 0, 0))
                txt.Tag = "hadErrored"
                allValid = False
            ElseIf txt.Tag Is "hadErrored" Then
                DrawErrorBox(Me.CreateGraphics, txt, Me.BackColor)
                txt.Tag = Nothing
            End If
        Next

        Return allValid
    End Function

    Public Sub DrawErrorBox(ByVal graph As Graphics, ByVal txt As TextBox, ByVal color As Color)
        Dim brush As New SolidBrush(color)

        graph.FillRectangle(brush, New Rectangle(New Point(txt.Location.X - 2, txt.Location.Y - 2), New Size(txt.Size.Width + 4, txt.Size.Height + 4)))
    End Sub

    Public Function ConvertToDate(ByVal str As String) As String
        Dim year As String = str.Substring(0, 4)
        Dim month As String = str.Substring(4, 2)
        Dim day As String = str.Substring(6, 2)

        Return year & "-" & month & "-" & day
    End Function

    Private Sub Birthdate_Keypress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        Dim int As Integer = Asc(e.KeyChar)
        If Asc(e.KeyChar) < Asc("0") Or Asc(e.KeyChar) > Asc("9") Then
            e.Handled = True
        End If

        If Asc(e.KeyChar) = 8 Then
            e.Handled = False
        End If
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
End Class