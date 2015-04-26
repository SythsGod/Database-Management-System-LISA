Imports MySql.Data.MySqlClient

Public Class test_form
    Private MouseIsDown As Boolean
    Private AlreadyLocked As Boolean
    Private setPoint As Point
    Private data As New DataSet("Test")
    Private previousIndex As Integer

    Public Sub New()
        InitializeComponent()
        Me.Size = New Size(1280, 720)
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
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
        AddHandler btn.Click, AddressOf btnUpload_Click
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

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'For i = 0 To data.Tables(0).Rows.Count - 1
        '    For j = 0 To data.Tables(0).Columns.Count - 1
        '        MsgBox(data.Tables(0).Rows.Item(i)(j).ToString)
        '    Next
        'Next

        Dim dataAdapter As New MySqlDataAdapter("SELECT * FROM Test_Tabel", myConn)
        dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey
        Dim objCommandBuilder As New MySqlCommandBuilder(dataAdapter)

        Try
            dataAdapter.Update(data, "Test_Tabel")
            MsgBox("Upload successful")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        dataAdapter.Dispose()
        objCommandBuilder.Dispose()
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
End Class