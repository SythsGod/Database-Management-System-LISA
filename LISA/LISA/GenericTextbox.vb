Public Class GenericTextbox
    Inherits TextBox
	'Rand
    Private fnt As Font = New Font(Me.Font.FontFamily, Me.FontHeight + 5, FontStyle.Regular)
    Private Sub Init()
        Me.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
        Me.Font = fnt
        Me.Location = New Point(150, 150)
        Me.Multiline = True
        Me.Name = "GenericTextbox"
        Me.Size = New Size(300, 35)
        Me.Width = 300
    End Sub
    Public Sub New(ByVal name As String)
        Init()
        Me.Name = name
    End Sub

    Public Sub New(ByVal name As String, ByVal loc As Point)
        Init()
        Me.Name = name
        Me.Location = loc
    End Sub

    Public Sub New(ByVal name As String, ByVal pSize As Size)
        Init()
        Me.Name = name
        Me.Size = pSize
    End Sub

    Public Sub New(ByVal name As String, ByVal pSize As Size, ByVal loc As Point)
        Init()
        Me.name = Name
        Me.size = pSize
        Me.location = loc
    End Sub

    Public Function ToInteger() As Integer
        Return CInt(Me.Text)
    End Function
End Class
