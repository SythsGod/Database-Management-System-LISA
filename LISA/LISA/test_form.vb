﻿Public Class test_form
    Private MouseIsDown As Boolean
    Private AlreadyLocked As Boolean
    Private setPoint As Point

    Public Sub New()
        InitializeComponent()
        Me.Size = New Size(1280, 720)
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterScreen
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
End Class