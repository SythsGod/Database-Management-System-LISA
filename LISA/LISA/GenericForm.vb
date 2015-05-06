Public Class GenericForm
    Inherits Form

    Private mHasBeenCreated As Boolean
    Private mHasControlbox As Boolean

    Private m_backcolor As Color
    Private m_opacity As Double
    Private m_transparent As Boolean
    Private m_transparentColor As Color

    Sub Init()
        Me.BackColor = Color.White
        Me.mHasBeenCreated = False
        Me.Name = ""
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Size = New Size(1280, 720)
        Me.StartPosition = FormStartPosition.CenterScreen

        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        SetStyle(ControlStyles.Opaque, False)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.UserPaint, True)
        UpdateStyles()

        AddCopyRightLabel(Me)
    End Sub
    Sub New(ByVal name As String, ByVal needsControlbox As Boolean)
        Init()
        Me.Name = name

        If needsControlbox Then
            AddControlBox(Me)
            Me.mHasControlbox = True
        Else
            Me.mHasControlbox = False
        End If
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

    Private Sub AddCopyRightLabel(ByVal frm As Form)
        Dim lbl As New Label With {.Text = "© 2014 - " & Date.Now.Year, .Name = "GenericLabel", .Font = New Font("Tahoma", 8)}
        lbl.Location = New Point(CInt(frm.ClientRectangle.Width / 2 - lbl.Width / 2), frm.ClientRectangle.Height - 20)
        frm.Controls.Add(lbl)
    End Sub
End Class