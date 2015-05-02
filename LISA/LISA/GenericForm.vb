﻿Public Class GenericForm
    Inherits Form

    Sub Init()
        Me.BackColor = Color.White
        Me.Name = ""
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Size = New Size(1280, 720)
        Me.StartPosition = FormStartPosition.CenterScreen
    End Sub
    Sub New(ByVal name As String)
        Init()
        Me.Name = name
        AddControlBox()
    End Sub

    Sub New(ByVal name As String, ByVal size As Size)
        Init()
        Me.Name = name
        Me.Size = size
        AddControlBox()
    End Sub

    Private Sub AddControlBox()
        For i = 0 To 2
            Dim btn As New ControlBoxButton

            btn.Location = New Point(Me.ClientRectangle.Width - 30 * (i + 1) + 1, -1)
            btn.Name = "GenericControlBoxButton" & i
            btn.SetImage(i)
            btn.Size = New Size(30, 30)
            btn.Tag = "1" & i

            AddHandler btn.Click, AddressOf FormDesignAndControl.ControlMouseClick
            AddHandler btn.MouseEnter, AddressOf FormDesignAndControl.ControlMouseEnter
            AddHandler btn.MouseLeave, AddressOf FormDesignAndControl.ControlMouseLeave

            Me.Controls.Add(btn)
        Next
    End Sub
End Class