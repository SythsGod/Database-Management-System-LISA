Public Class Form1
    Private Hover As Boolean = False
    Public Sub New()
        InitializeComponent()
        btnTest.BackColor = Color.White
        btnTest.FlatAppearance.BorderSize = 0
        btnTest.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 57, 152, 214)
        btnTest.Text = "Test"
        'Size: 300, 50

        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.Purple
    End Sub

    Private Sub btnTest_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTest.MouseEnter
        Dim g As Graphics = DirectCast(sender, Button).CreateGraphics

        PaintChart(g) ', 0, 0, 0, 0, 0)
    End Sub

    Private Sub btnTest_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTest.MouseLeave
        'btnTest.BackColor = Color.White
    End Sub

    Private Sub PaintChart(ByVal g As Graphics) ', ByVal insolvent As Decimal, ByVal days30 As Decimal, ByVal days60 As Decimal, ByVal days90 As Decimal, ByVal current As Decimal)
        For i = 0 To 650
            g.FillEllipse(New SolidBrush(Color.FromArgb(255, 57, 152, 214)), btnTest.Width \ 2 - i \ 2, btnTest.Height \ 2 - i \ 2, i, i)
            Threading.Thread.Sleep(New System.TimeSpan(1))
        Next
    End Sub
End Class
