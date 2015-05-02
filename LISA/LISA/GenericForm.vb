Public Class GenericForm
    Inherits Form

    Private mHasBeenCreated As Boolean
    Sub Init()
        Me.BackColor = Color.White
        Me.mHasBeenCreated = False
        Me.Name = ""
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Size = New Size(1280, 720)
        Me.StartPosition = FormStartPosition.CenterScreen
    End Sub
    Sub New(ByVal name As String)
        Init()
        Me.Name = name
        AddControlBox(Me)
    End Sub

    Sub New(ByVal name As String, ByVal size As Size)
        Init()
        Me.Name = name
        Me.Size = size
        AddControlBox(Me)
    End Sub

    Public Property hasBeenCreated() As Boolean
        Get
            Return mHasBeenCreated
        End Get
        Set(value As Boolean)
            mHasBeenCreated = value
        End Set
    End Property

    Public Shared Sub AddControlBox(ByVal frm As Form)
        For i = 0 To 2
            Dim btn As New ControlBoxButton

            btn.Location = New Point(frm.ClientRectangle.Width - 30 * (i + 1) + 1, -1)
            btn.Name = "GenericControlBoxButton" & i
            btn.SetImage(i)
            btn.Size = New Size(30, 30)
            btn.Tag = "1" & i

            AddHandler btn.Click, AddressOf FormDesignAndControl.ControlMouseClick
            AddHandler btn.MouseEnter, AddressOf FormDesignAndControl.ControlMouseEnter
            AddHandler btn.MouseLeave, AddressOf FormDesignAndControl.ControlMouseLeave

            frm.Controls.Add(btn)
        Next
    End Sub
End Class