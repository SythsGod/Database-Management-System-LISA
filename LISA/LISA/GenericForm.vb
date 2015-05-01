Public Class GenericForm
    Inherits Form

    Sub New(ByVal name As String)
        Me.BackColor = Color.White
        Me.Name = name
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Size = New Size(1280, 720)
        Me.StartPosition = FormStartPosition.CenterScreen
    End Sub

    Sub New(ByVal name As String, ByVal size As Size)
        Me.BackColor = Color.White
        Me.Name = name
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Size = size
        Me.StartPosition = FormStartPosition.CenterScreen
    End Sub
End Class
