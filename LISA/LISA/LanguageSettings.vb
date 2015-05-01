Module LanguageSettings
    Public Sub CreateForm()
        Dim form1 As New GenericForm("GenericForm_LanguageSettings", New Size(700, 400))

        CreateControlBox(form1)
        CreateFormButtons(form1)
        CreateLabel(form1)

        AddHandler form1.MouseUp, AddressOf MoveForms.MouseUp
        AddHandler form1.MouseMove, AddressOf MoveForms.MouseMove
        AddHandler form1.MouseDown, AddressOf MoveForms.MouseDown
        AddHandler form1.FormClosing, AddressOf MoveForms.Closing
        form1.Show()
    End Sub

    Private Sub CreateControlBox(ByVal frm As Form)
        For i = 0 To 2
            Dim btn As New ControlBoxButton
            btn.Location = New Point(frm.ClientRectangle.Width - 30 * (i + 1) + 1, -1)
            btn.Name = "GenericControlBoxButton_" & i
            btn.SetImage = i
            btn.Size = New Size(30, 30)
            btn.Tag = "1" & i

            If i = 1 Then
                btn.Disabled = True
            End If

            AddHandler btn.Click, AddressOf ControlMouseClick
            If i <> 1 Then
                AddHandler btn.MouseEnter, AddressOf ControlMouseEnter
            End If
            AddHandler btn.MouseLeave, AddressOf ControlMouseLeave
            frm.Controls.Add(btn)
        Next
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

        For Each btn As GenericButton In LISA.Controls.OfType(Of GenericButton)()
            If CInt(btn.Tag) < 10 Then
                btn.Text = TableNamesInDatabase(CInt(btn.Tag))(currentLang).ToString
            End If
        Next

        DirectCast(sender, GenericButton).FindForm.Close()
    End Sub
End Module
