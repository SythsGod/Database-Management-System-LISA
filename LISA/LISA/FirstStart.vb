Imports MySql.Data.MySqlClient

Public Class FirstStart
    Private WithEvents btn As New GenericButton With {.Location = New Point(35, 85), .Size = New Size(330, 50), .borderColor = Color.White, .hoverColor = Color.FromArgb(255, 211, 211, 211), .Tag = "REGISTER ONLY"}
    Private WithEvents btn1 As New GenericButton With {.Location = New Point(35, 150), .Size = New Size(330, 50), .borderColor = Color.White, .hoverColor = Color.FromArgb(255, 211, 211, 211), .Tag = "COMPLETE"}

    Public Sub New()
        InitializeComponent()
        Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        Me.AllowTransparency = True
        Me.BackColor = Color.FromArgb(255, Color.Gray)
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Size = New Size(400, 235)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.TransparencyKey = Color.Gray

        Me.Controls.AddRange(New Control() {btn, btn1})
    End Sub

    Private Sub frm_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit

        e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(255, 211, 211, 211)), 0, 0, Me.ClientRectangle.Width, 50)
        e.Graphics.DrawRectangle(New Pen(Color.FromArgb(255, 211, 211, 211)), 0, 0, Me.ClientRectangle.Width - 1, Me.ClientRectangle.Height - 1)
        e.Graphics.DrawString("PURPOSE", New Font("Calibri", 24, FontStyle.Bold), New SolidBrush(Color.Gray), New Point(134, 7))
    End Sub

    Private Sub _Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn.MouseEnter, btn1.MouseEnter
        DirectCast(sender, GenericButton).borderColor = Color.FromArgb(255, 211, 211, 211)
        Me.Refresh()
    End Sub

    Private Sub _Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn.MouseLeave, btn1.MouseLeave
        DirectCast(sender, GenericButton).borderColor = Color.White
    End Sub

    Private Sub btn_Paint(sender As Object, e As PaintEventArgs) Handles btn.Paint
        Dim btn As GenericButton = DirectCast(sender, GenericButton)
        Dim strSize As SizeF = e.Graphics.MeasureString(btn.Tag.ToString, btn.Font)

        e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit
        e.Graphics.DrawString(btn.Tag.ToString, btn.Font, New SolidBrush(Color.Gray), New PointF(btn.Width \ 2 - CLng(strSize.Width) \ 2, 25 - CLng(strSize.Height) \ 2))
    End Sub

    Private Sub btn1_Paint(sender As Object, e As PaintEventArgs) Handles btn1.Paint
        Dim btn As GenericButton = DirectCast(sender, GenericButton)
        Dim strSize As SizeF = e.Graphics.MeasureString(DirectCast(sender, Button).Tag.ToString, DirectCast(sender, Button).Font)

        e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit
        e.Graphics.DrawString(DirectCast(sender, Button).Tag.ToString, DirectCast(sender, Button).Font, New SolidBrush(Color.Gray), New PointF(btn.Width \ 2 - CLng(strSize.Width) \ 2, 25 - CLng(strSize.Height) \ 2))
    End Sub

    Private Sub btn_Click(sender As Object, e As EventArgs) Handles btn.Click
        SaveSetting(My.Application.Info.ProductName, "General", "Usage", "RegisterOnly")
        MsgBox("Setting saved. Please start again.", MsgBoxStyle.Information)
        Environment.Exit(0)
    End Sub

    Private Sub btn1_Click(sender As Object, e As EventArgs) Handles btn1.Click
        SaveSetting(My.Application.Info.ProductName, "General", "Usage", "Complete")
        MsgBox("Setting saved. Please start again.", MsgBoxStyle.Information)
        Environment.Exit(0)
    End Sub

    Private Function SettingExists() As Boolean
        Return New Boolean = (GetSetting(My.Application.Info.ProductName, "General", "Usage") = Nothing)
    End Function
End Class