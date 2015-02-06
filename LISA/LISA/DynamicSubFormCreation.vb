Module DynamicSubFormCreation
    Private MouseIsDown As Boolean
    Private AlreadyLocked As Boolean
    Private SetPoint As Point
    Public Sub CreateSubForm(ByVal buttonTag As Integer, ByVal name As String)
        Dim form1 As New Form

        With form1
            .Size = New Size(1280, 720)
            .Name = "frm_" & TableNamesInDatabase.Rows(buttonTag)(1).ToString
            .FormBorderStyle = Windows.Forms.FormBorderStyle.None
            .Tag = buttonTag
            .StartPosition = FormStartPosition.CenterScreen
            .BackgroundImage = My.Resources.IMG_BG_BLUR
            .BackgroundImageLayout = ImageLayout.Center
        End With

        For i = 0 To 2
            Dim button1 As New ControlBoxButton
            With button1
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right
                .Name = "Generic" & i
                .Tag = "1" & i
                .Location = New Point(form1.ClientRectangle.Width - 45 * (i + 1), 5)
                .Size = New Size(40, 40)
                .FlatStyle = FlatStyle.Flat
                .SetImage = i
                .BackgroundImageLayout = ImageLayout.Center
            End With

            AddHandler button1.Click, AddressOf controlboxButton_Click
            AddHandler button1.Paint, AddressOf ButtonPaint
            form1.Controls.Add(button1)
        Next

        Dim label1 As New Label
        With label1
            If LangSwitch Then
                .Text = TableNamesInDatabase(buttonTag)(2).ToString
            Else
                .Text = TableNamesInDatabase(buttonTag)(3).ToString
            End If
            .BackColor = Color.Coral
            .TextAlign = ContentAlignment.MiddleLeft
            .Location = New Point(30, CInt((50 - label1.Height) / 2))
            .Name = "lbl" & name
            .Text = name & ":"
            .Width = 150
        End With
        form1.Controls.Add(label1)

        Dim dataGridView1 As New DataGridView
        With dataGridView1
            .Name = "DataGridView" & name
            .Location = New Point(20, 70)
            .Size = New Size(200 * AllTableInformation(buttonTag).Tables(0).Columns.Count, form1.Height - 90)
            .DataSource = AllTableInformation(buttonTag).Tables(0)
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With
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

        AddHandler form1.Paint, AddressOf form1_paint
        AddHandler form1.FormClosing, AddressOf form1_FormClosing
        AddHandler form1.MouseDown, AddressOf Form1_MouseDown
        AddHandler form1.MouseMove, AddressOf Form1_MouseMove
        AddHandler form1.MouseUp, AddressOf Form1_MouseUp
        form1.Show()
    End Sub

    Private Function ThinkOfAGoodName(ByVal e As System.Windows.Forms.MouseEventArgs) As Point
        If Not AlreadyLocked Then
            Dim p As Point
            p = New Point(e.X, e.Y)
            AlreadyLocked = True
            Return p
        Else
            Return SetPoint
        End If
    End Function

    Private Sub Form1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim conditions As Boolean
        conditions = e.X >= 0 And e.X <= DirectCast(sender, Form).ClientRectangle.Width And e.Y >= 0 And e.Y <= 50

        MouseIsDown = conditions
    End Sub

    Private Sub Form1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If MouseIsDown Then
            SetPoint = ThinkOfAGoodName(e)
            DirectCast(sender, Form).Location = New Point(Control.MousePosition.X - SetPoint.X, Control.MousePosition.Y - SetPoint.Y)
        End If
    End Sub

    Private Sub Form1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        MouseIsDown = False
        AlreadyLocked = False
    End Sub


    Private Sub form1_paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim brush As New SolidBrush(Color.Coral)
        Dim paper As Graphics

        paper = e.Graphics

        paper.FillRectangle(brush, 0, 0, DirectCast(sender, Form).ClientRectangle.Width, 50)
    End Sub

    Private Sub form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs)
        LISA.Show()
        DirectCast(sender, Form).Dispose()
    End Sub

    Private Sub controlboxButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim tag As Integer = CInt(DirectCast(sender, Button).Tag)
        Dim currForm As Form = DirectCast(sender, Button).FindForm

        Select Case tag
            Case 10 'Close
                currForm.Close()
            Case 11 'Maximize
                If currForm.WindowState = FormWindowState.Normal Then
                    currForm.WindowState = FormWindowState.Maximized
                    DirectCast(sender, ControlBoxButton).SetImage = 3
                Else
                    currForm.WindowState = FormWindowState.Normal
                    DirectCast(sender, ControlBoxButton).SetImage = 1
                End If
            Case 12 'Minimize
                currForm.WindowState = FormWindowState.Minimized
        End Select
    End Sub

    Private Sub ButtonPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim currForm As Form = DirectCast(sender, Button).FindForm
        'Remove the 'black given border' on a flat button by painting over it using the same background color as the button itself
        Dim button1 = DirectCast(sender, Button)
        Using P As New Pen(currForm.BackColor)
            e.Graphics.DrawRectangle(P, 1, 1, button1.Width - 3, button1.Height - 3)
        End Using
    End Sub
End Module
