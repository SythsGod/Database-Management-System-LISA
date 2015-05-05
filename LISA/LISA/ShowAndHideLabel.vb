Public Class ShowAndHideLabel
    Inherits Label
	'rand
    Private WithEvents Timer As New Timer
    Private mCounter As Integer
    Private mLimit As Integer
    Public Sub New(ByVal name As String)
        Me.BackColor = Color.Transparent
        Me.Location = New Point(50, 50)
        Me.Name = name
        Me.Text = ""
        Me.TextAlign = ContentAlignment.MiddleLeft
        Me.Width = 350

        Me.Timer.Enabled = False
        Me.Timer.Interval = 1000

        Me.mCounter = 0
    End Sub

    Public Sub SetText(ByVal text As String, ByVal time As Integer)
        Me.mLimit = time
        Me.Text = text
        Me.Timer.Start()
    End Sub

    Private Sub Timer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer.Tick
        If Me.mCounter >= Me.mLimit Then
            Me.Timer.Stop()
            Me.Text = ""
            Me.mCounter = 0
            Me.mLimit = 0
        Else
            mCounter += 1
        End If
    End Sub
End Class
