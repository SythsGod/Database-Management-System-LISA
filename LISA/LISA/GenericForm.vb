Public Class GenericForm
    Inherits Form

    Sub New(ByVal name As String, ByVal needsControlbox As Boolean)
        Me.BackColor = Color.White
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Name = name
        Me.Size = New Size(1280, 720)
        Me.StartPosition = FormStartPosition.CenterScreen

        SetStyles()
        AddCopyRightLabel(Me)

        If needsControlbox Then ControlBoxButton.AddControlbox(Me)
    End Sub

    Private Sub SetStyles()
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        SetStyle(ControlStyles.Opaque, False)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.UserPaint, True)
        UpdateStyles()
    End Sub

    Private Sub AddCopyRightLabel(ByVal frm As Form)
        Dim lbl As New Label With {.Text = "© 2014 - " & Date.Now.Year, .Name = "GenericLabel", .Font = New Font("Tahoma", 8)}
        lbl.Location = New Point(CInt(frm.ClientRectangle.Width / 2 - lbl.Width / 2), frm.ClientRectangle.Height - 20)
        frm.Controls.Add(lbl)
    End Sub
End Class