Imports MySql.Data.MySqlClient

Module DynamicSubFormCreation
    Private data As New DataSet
    Private changesWereMade As Boolean
    Public Sub CreateSubForm(ByVal buttonTag As Integer, ByVal name As String)
        data = AllTableInformation(buttonTag)

        Dim form1 As New GenericForm("GenericForm_" & buttonTag.ToString)

        Dim button1 As New GenericButton
        With button1
            .Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
            .Name = "GenericButton_Upload"
            .Tag = "15"
            .Size = New Size(100, 50)
            .FlatStyle = FlatStyle.Flat
            .Location = New Point(form1.ClientRectangle.Width - 120, form1.ClientRectangle.Height - 70)
            .Text = "Upload" 'Use language settings to determine the text
        End With
        AddHandler button1.Click, AddressOf btnUpload_Click
        form1.Controls.Add(button1)

        Dim label1 As New Label
        Dim label2 As New ShowAndHideLabel("GenericLabel_Actions")
        With label1
            .BackColor = Color.Transparent
            .TextAlign = ContentAlignment.MiddleLeft
            .Location = New Point(30, 28)
            .Name = "GenericLabel_" & name
            .Text = name & ":" 'Use language settings to determine the text
            .Width = 150
        End With
        label2.Location = New Point(20, form1.Height - 10 - label2.Height)
        form1.Controls.Add(label1)
        form1.Controls.Add(label2)

        Dim dataGridView1 As New DataGridView
        With dataGridView1
            .Anchor = AnchorStyles.Left Or AnchorStyles.Top
            .Name = "GenericDataGridView_Main"
            .Location = New Point(20, 70)
            .Size = New Size(form1.Width - 40, 520)
            .DataSource = data.Tables(0)
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With
        AddHandler dataGridView1.DataBindingComplete, AddressOf dataGridView1_DataBindingComplete
        AddHandler dataGridView1.CellValueChanged, AddressOf dataGridView1_CellValueChanged
        form1.Controls.Add(dataGridView1)

        'Add textboxes and buttons to handle the inserting of new records
        'For i = 0 To AllTableInformation(buttonTag).Tables(0).Columns.Count - 1
        '    Dim textbox1 As New TextBox
        '    With textbox1
        '        .Name = "txtInput" & i
        '        .Location = New Point(form1.ClientRectangle.Width - textbox1.Width - 20, 100 * (i + 1))
        '    End With
        '    form1.Controls.Add(textbox1)
        'Next 

        'AddHandler form1.Paint, AddressOf form1_Paint
        AddHandler form1.FormClosing, AddressOf form1_FormClosing
        AddHandler form1.Resize, AddressOf form1_Resize
        AddHandler form1.MouseUp, AddressOf MoveForms.MouseUp
        AddHandler form1.MouseMove, AddressOf MoveForms.MouseMove
        AddHandler form1.MouseDown, AddressOf MoveForms.MouseDown
        form1.Show()
    End Sub

    'Temporary measure for black border around form (For visibility with white background)
    'Private Sub form1_Paint(ByVal sender As Object, ByVal e As PaintEventArgs)
    '    Dim pen As Pen = New Pen(Color.Black)
    '    e.Graphics.DrawRectangle(pen, New Rectangle(0, 0, DirectCast(sender, Form).Width - 1, DirectCast(sender, Form).Height - 1))
    'End Sub

    Private Sub form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs)
        LISA.Show()
        DirectCast(sender, Form).Dispose()
    End Sub

    Private Sub form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dgv As DataGridView = CType(DirectCast(sender, Form).Controls("GenericDatagridView_Main"), DataGridView)
        Dim lbl As Label = CType(DirectCast(sender, Form).Controls("GenericLabel_Actions"), Label)

        If DirectCast(sender, Form).WindowState = FormWindowState.Normal Then
            dgv.Size = New Size(DirectCast(sender, Form).Width - 40, 520)
            lbl.Location = New Point(20, DirectCast(sender, Form).Height - 10 - lbl.Height)
        ElseIf DirectCast(sender, Form).WindowState = FormWindowState.Maximized Then
            dgv.Size = New Size(My.Computer.Screen.WorkingArea.Width - 40, My.Computer.Screen.WorkingArea.Height - 200)
            lbl.Location = New Point(20, My.Computer.Screen.WorkingArea.Height - lbl.Height + 10)
        End If
    End Sub

    'Unused (Remove later)
    'Private Sub ButtonPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
    '    Dim currForm As Form = DirectCast(sender, Button).FindForm
    '    'Remove the 'black given border' on a flat button by painting over it using the same background color as the button itself
    '    Dim button1 = DirectCast(sender, Button)
    '    Using P As New Pen(currForm.BackColor)
    '        e.Graphics.DrawRectangle(P, 1, 1, button1.Width - 3, button1.Height - 3)
    '    End Using
    'End Sub

    Private Sub dataGridView1_DataBindingComplete(ByVal sender As Object, ByVal e As System.EventArgs)
        If DirectCast(sender, DataGridView).Columns.Count > 0 Then
            'Hides first column with ID values
            DirectCast(sender, DataGridView).Columns(0).Visible = False
        End If
    End Sub

    Private Sub dataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        changesWereMade = True
    End Sub

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim label1 As ShowAndHideLabel = CType(DirectCast(sender, GenericButton).FindForm.Controls("GenericLabel_Actions"), ShowAndHideLabel)
        Dim dgv As DataGridView = CType(DirectCast(sender, GenericButton).FindForm.Controls("GenericDataGridView_Main"), DataGridView)

        If dgv.CurrentCell IsNot dgv.Item(1, 0) Then
            dgv.CurrentCell = dgv.Item(1, 0)
        Else
            dgv.CurrentCell = dgv.Item(1, 1)
        End If
        'Set focus on different cell so that the datagridview finishes the editing of records

        If Not changesWereMade Then
            label1.SetText("No changes detected. Upload canceled.", 3)
            Exit Sub
        End If

        'Upload stuff
        Dim table As String = TableNamesInDatabase(CInt(DirectCast(sender, GenericButton).FindForm.Tag))(1).ToString
        If Upload(data, table) Then
            label1.SetText("Uploaded to database.", 3)
        Else
            label1.SetText("Upload failed.", 5)
        End If
    End Sub
End Module
