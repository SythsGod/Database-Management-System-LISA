Public Class GenericCombobox
    Inherits ComboBox

    Public Sub New(ByVal name As String, ByVal lcation As Point, ByVal tag As Integer)
        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.Font = New Font(Main.Font.FontFamily, 17)
        Me.Location = lcation
        Me.Name = name
        Me.Tag = tag
        Me.Width = 300
    End Sub
End Class
