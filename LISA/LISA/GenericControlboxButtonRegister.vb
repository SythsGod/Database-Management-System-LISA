Public Class GenericControlboxButtonRegister
    Inherits ControlBoxButton

    Sub New()
        MyBase.New()
    End Sub

    Public Overloads Overrides Sub Clicked(hide As Boolean)

    End Sub

    Public Overrides Sub SetHoverImage()
        Me.BackgroundImage = Globals.imgRegisterForm(1)
    End Sub

    Public Overrides Sub SetImage()
        Me.BackgroundImage = Globals.imgRegisterForm(0)
    End Sub
End Class
