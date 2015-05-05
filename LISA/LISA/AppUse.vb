Module AppUse
    Public Sub Init()
        Dim frm As New GenericForm("GenericForm_AppUseChoise", False)

        frm.Size = New Size(400, 300)

        Dim btn1 As New GenericButton With {.Name = "GenericButton_Management", .Text = "Management", .Location = New Point(25, 25), .Size = New Size(100, 40)}
        'Dim btn2 As New GenericButton With {.Name = "GenericButton_Registration", .Text = "Registration", .Location = New Point(50, 85), .Size = New Size(100, 40)}

        frm.Controls.Add(btn1)
        'frm.Controls.Add(btn2)

        frm.Show()
    End Sub
End Module
