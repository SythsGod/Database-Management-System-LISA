Module FailedEiDReader
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
    '        RetStatus = eID.Init("", 0, 0, lHandle)
    '        If RetStatus.GetGeneral = 0 Then
    '            RetStatus = eID.GetID(MapColID, CertifCheck)
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

    '            strBegValDag = strBegVal.ToString.Substring(0, 2)
    '            strBegValMaand = strBegVal.ToString.Substring(5, 2)

    '            MsgBox("Begin Value: " & strBegVal.ToString)
    '            MsgBox("End Value: " & strEndVal.ToString)
    '        End If

    '        RetStatus = eID.GetAddress(MapColAddress, CertifCheck)

    '        strStreet = MapColAddress.GetValue("Street").ToString
    '        strZIPCode = MapColAddress.GetValue("ZIPCode").ToString
    '        strMunicipality = MapColAddress.GetValue("Municipality").ToString

    '        RetStatus = eID.GetPicture(MapColPicture, CertifCheck)
    '        picture = DirectCast(MapColPicture.GetValue("Picture"), Bitmap)
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub
End Module
