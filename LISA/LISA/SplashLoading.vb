Public NotInheritable Class SplashLoading
    Public Delegate Sub SetProgressBarDelegate(ByVal max As Integer)
    Public Delegate Sub UpdateProgressBarDelegate(ByVal value As Integer)

    Public Sub BarLong(ByVal MemCount As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(New SetProgressBarDelegate(AddressOf BarLong), MemCount)
        Else
            Me.ProgressBar1.Maximum = MemCount
        End If
    End Sub

    Public Sub ShowBar(ByVal SoFar As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(New UpdateProgressBarDelegate(AddressOf ShowBar), SoFar)
        Else
            Me.ProgressBar1.Value = SoFar
        End If
    End Sub

    Public Sub New()
        InitializeComponent()
        Me.TransparencyKey = Me.BackColor
        'PictureBox1.BackgroundImage = My.Resources.Lisa_Icon_Style_2_No_BG
        'PictureBox1.BackgroundImageLayout = ImageLayout.Center Or ImageLayout.Stretch
        'PictureBox1.BackColor = Me.BackColor
    End Sub

    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint
        Dim r As New Rectangle(0, 0, 500, 500)
        e.Graphics.DrawImage(My.Resources.Lisa_Icon_Style_2_No_BG, r)
    End Sub
End Class
