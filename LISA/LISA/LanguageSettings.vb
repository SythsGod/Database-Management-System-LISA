Module LanguageSettings
    Public Sub Init()
        Dim frm As New GenericForm("GenericForm_LanguageSettings", False) With {.Size = New Size(700, 400)}

        CreateFormButtons(frm)
        CreateLabel(frm)

        'Unnecessary since there's no controlbox anymore
        'DirectCast(frm.Controls("GenericControlBoxButton1"), ControlBoxButton).Disabled = True
        'RemoveHandler DirectCast(frm.Controls("GenericControlBoxButton1"), ControlBoxButton).MouseEnter, AddressOf FormDesignAndControl.ControlMouseEnter
        'RemoveHandler DirectCast(frm.Controls("GenericControlBoxButton1"), ControlBoxButton).MouseLeave, AddressOf FormDesignAndControl.ControlMouseLeave

        AddHandler frm.MouseUp, AddressOf MoveForms.MouseUp
        AddHandler frm.MouseMove, AddressOf MoveForms.MouseMove
        AddHandler frm.MouseDown, AddressOf MoveForms.MouseDown
        AddHandler frm.FormClosing, AddressOf MoveForms.Closing
        AddHandler frm.LostFocus, AddressOf Frm_LostFocus
        AddHandler frm.Paint, AddressOf GenericForm_Paint
        frm.Show()
    End Sub

    Private Sub GenericForm_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        DirectCast(sender, GenericForm).CreateGraphics.DrawRectangle(New Pen(Color.Black), New Rectangle(New Point(0, 0), New Size(DirectCast(sender, GenericForm).Clientrectangle.Width - 1, DirectCast(sender, GenericForm).Clientrectangle.Height - 1)))
    End Sub

    Private Sub Frm_LostFocus(ByVal sender As Object, ByVal e As EventArgs)
        'Acts like ShowDialog()
        DirectCast(sender, GenericForm).Focus()
    End Sub

    Private Sub CreateFormButtons(ByVal frm As Form)
        Dim f As Integer = 0
        Dim x As Integer = 100

        For i = 0 To 7
            Dim btn As New GenericButton

            btn.Location = New Point(x, 60 + (75 * f))
            btn.Name = "GenericFormButton_" & i
            btn.Size = New Size(200, 50)
            btn.Tag = i
            btn.Text = languages(i)

            If i = 3 Then
                x = 400
                f = -1
            End If

            AddHandler btn.Click, AddressOf ButtonForm_Click
            frm.Controls.Add(btn)

            f += 1
        Next
    End Sub

    Private Sub CreateLabel(ByVal frm As Form)
        Dim lbl As New Label
        lbl.Width = 350
        lbl.Location = New Point(CInt(frm.ClientRectangle.Width / 2 - lbl.Width / 2), frm.ClientRectangle.Height - 55 + lbl.Height)
        lbl.Name = "GenericLabel_CurrentLang"
        lbl.Text = "Current Language: " & languages(currentLang - 2)
        lbl.TextAlign = ContentAlignment.MiddleCenter
        frm.Controls.Add(lbl)
    End Sub

    Private Sub ButtonForm_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        currentLang = CInt(DirectCast(sender, GenericButton).Tag) + 2
        SaveSetting(My.Application.Info.ProductName, "General", "Language Setting", currentLang.ToString)

        For Each btn As GenericButton In Main.Controls.OfType(Of GenericButton)()
            If CInt(btn.Tag) < 10 Then
                btn.Text = TableNamesInDatabase(CInt(btn.Tag))(currentLang).ToString
            End If
        Next

        DirectCast(sender, GenericButton).FindForm.Close()
    End Sub
End Module
