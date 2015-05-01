Imports MySql.Data.MySqlClient
Imports EIDLIBCTRLLib

Public Class test_form
    Private MouseIsDown As Boolean
    Private AlreadyLocked As Boolean
    Private setPoint As Point
    Private data As New DataSet("Test")
    Private previousIndex As Integer

    Public Sub New()
        InitializeComponent()
        'Me.Size = New Size(1280, 720)
        'Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterScreen
        GetServerVars()
        OpenConnection()
        data = RetrieveTableNames("Test_Tabel")
        previousIndex = data.Tables(0).Rows.Count



        Dim dgv As New DataGridView
        dgv.Name = "dgv1"
        dgv.DataSource = data.Tables(0)
        dgv.Location = New Point(20, 60)
        dgv.Size = New Size(1000, 620)
        'AddHandler dgv.RowsAdded, AddressOf dgv_RowsAdded
        Me.Controls.Add(dgv)

        Dim btn As New Button
        btn.Name = "btnUpload"
        btn.Size = New Size(100, 50)
        btn.Location = New Point(1050, 80)
        btn.Text = "Upload"
        AddHandler btn.Click, AddressOf Button_Upload
        Me.Controls.Add(btn)
    End Sub

    Private Sub Main_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        Dim conditions As Boolean
        conditions = e.X >= 0 And e.X <= Me.ClientRectangle.Width And e.Y >= 0 And e.Y <= 50

        MouseIsDown = conditions
    End Sub

    Private Function ThinkOfAGoodName(ByVal e As System.Windows.Forms.MouseEventArgs) As Point
        If Not AlreadyLocked Then
            Dim p As Point
            p = New Point(e.X, e.Y)
            AlreadyLocked = True
            Return p
        Else
            Return setPoint
        End If
    End Function

    Private Sub Main_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If MouseIsDown Then
            setPoint = ThinkOfAGoodName(e)
            Me.Location = New Point(Control.MousePosition.X - setPoint.X, Control.MousePosition.Y - setPoint.Y)
        End If
    End Sub

    Private Sub Main_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        MouseIsDown = False
        AlreadyLocked = False
    End Sub

    Private Sub Main_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim brush As New SolidBrush(Color.Coral)
        Dim pen As New Pen(Color.Black)
        Dim paper As Graphics

        paper = e.Graphics

        paper.FillRectangle(brush, 0, 0, Me.ClientRectangle.Width, 50)
        paper.DrawLine(pen, Me.ClientRectangle.Width \ 2, 0, Me.ClientRectangle.Width \ 2, Me.ClientRectangle.Height)
        paper.DrawLine(pen, 0, Me.ClientRectangle.Height \ 2, Me.ClientRectangle.Width, Me.ClientRectangle.Height \ 2)
    End Sub

    Private Sub Button_Upload(ByVal sender As Object, ByVal e As System.EventArgs)
        GetCardInformation()
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

    Private Sub GetCardInformation()
        Try
            Dim eID As New EIDlib
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        Dim lHandle As Integer

        Dim strCardNumber As String
        Dim strChipNumber As String
        Dim strBegVal As Object
        Dim strBegValDag, strBegValMaand, strBegValJaar As String
        Dim strEndVal As Object
        Dim strEndValDag, strEndValMaand, strEndValJaar As String
        Dim strIssuingMunicipality As String

        Dim strName As String
        Dim strFirstname1 As String
        Dim strBirthPlace As String
        Dim strBirthDate As String
        Dim strGender As String
        Dim strNationality As String
        Dim strNationalNumber As String

        Dim strStreet As String
        Dim strZIPCode As String
        Dim strMunicipality As String

        Dim RetStatus As RetStatus
        Dim MapColPicture As New MapCollection
        Dim MapColID As New MapCollection
        Dim MapColAddress As New MapCollection
        Dim CertifCheck As New CertifCheck

        Dim picture As Bitmap

        Try
            'RetStatus = eID.Init("", 0, 0, lHandle)
            If RetStatus.GetGeneral = 0 Then
                'RetStatus = eID.GetID(MapColID, CertifCheck)
                strChipNumber = MapColID.GetValue("ChipNumber").ToString
                strCardNumber = MapColID.GetValue("CardNumber").ToString
                strBegVal = MapColID.GetValue("BeginValidityDate")
                strEndVal = MapColID.GetValue("EndValidityDate")
                strIssuingMunicipality = MapColID.GetValue("IssuingMunicipality").ToString
                strName = MapColID.GetValue("Name").ToString
                strFirstname1 = MapColID.GetValue("FirstName1").ToString
                strBirthDate = MapColID.GetValue("BirthDate").ToString
                strBirthPlace = MapColID.GetValue("BirthPlace").ToString
                strGender = MapColID.GetValue("Gender").ToString
                strNationality = MapColID.GetValue("Nationality").ToString
                strNationalNumber = MapColID.GetValue("NationalNumber").ToString

                'strBegValDag = strBegVal.ToString.Substring(0, 2)
                'strBegValMaand = strBegVal.ToString.Substring(5, 2)

                MsgBox("Begin Value: " & strBegVal.ToString)
                MsgBox("End Value: " & strEndVal.ToString)
            End If

            'RetStatus = eID.GetAddress(MapColAddress, CertifCheck)

            strStreet = MapColAddress.GetValue("Street").ToString
            strZIPCode = MapColAddress.GetValue("ZIPCode").ToString
            strMunicipality = MapColAddress.GetValue("Municipality").ToString

            'RetStatus = eID.GetPicture(MapColPicture, CertifCheck)
            picture = DirectCast(MapColPicture.GetValue("Picture"), Bitmap)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class