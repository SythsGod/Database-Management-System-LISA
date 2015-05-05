Public Class GenericButton
    Inherits Button

    Public Sub New()
        Me.Anchor = AnchorStyles.None 'Buttons will stay in place, even when the form is maximized
        Me.BackColor = Color.White 'Set the default backcolor
        Me.FlatStyle = Windows.Forms.FlatStyle.Flat 'Makes the button a flat control (Reason: Aesthetics)
        Me.FlatAppearance.BorderColor = Color.FromArgb(255, 57, 152, 214) 'Have the button border be "blue"
        Me.FlatAppearance.BorderSize = 2 'The border size
        Me.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 57, 152, 214) 'On button click, the button won't have a gray-ish background, but get the specified "blue"
        Me.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 57, 152, 214) 'On button hover, the button won't have the default backcolor, but get the specified "blue"
        Me.SetStyle(ControlStyles.Selectable, False) 'Button can't be focused (Reason: Aesthetics)
        Me.Size = New Size(420, 75) 'Set the default size
    End Sub

    Public WriteOnly Property borderColor() As Color
        Set(value As Color)
            Me.FlatAppearance.BorderColor = value
        End Set
    End Property

    Public WriteOnly Property hoverColor() As Color
        Set(value As Color)
            Me.FlatAppearance.MouseDownBackColor = value
            Me.FlatAppearance.MouseOverBackColor = value
        End Set
    End Property
End Class
