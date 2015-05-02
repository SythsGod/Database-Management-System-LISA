Module RegisterForm
    Public Sub Init()
        Dim frm As New GenericForm("GenericForm_Register")
        Dim button1 As New GenericButton

        AddTextBoxes(frm)
    End Sub

    Public Sub AddTextBoxes(ByVal frm As GenericForm)
        For j = 0 To 6
            For i = 0 To 2
                Dim lcation As Point = New Point(90 + 400 * i, 70 + 75 * j)
                Dim c As Integer = i + (j * 3) 'Stores where the counter is at

                If needsDropDown(c) Then 'Create a new combobox
                    Dim cmb As New GenericCombobox("GenericCombobox" & c, lcation, c)
                    frm.Controls.Add(cmb)
                Else 'Create a new textbox
                    Dim txt As New GenericTextbox("GenericTextbox" & c, lcation)
                    frm.Controls.Add(txt)
                End If

                Dim lbl As New Label With {.Location = New Point(lcation.X, lcation.Y - 28), .TextAlign = ContentAlignment.BottomLeft, .Text = registerEntries(c) & ":", .Width = 300}
                frm.Controls.Add(lbl)
            Next
        Next
    End Sub
End Module
