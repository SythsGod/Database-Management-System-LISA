Public Class GenericControlboxButtonLanguage
    Inherits ControlBoxButton

    Sub New()
        MyBase.New()
        SetImage()
    End Sub

    Public Overloads Overrides Sub Clicked(hide As Boolean)

    End Sub

    Public Overrides Sub SetHoverImage()
        Me.BackgroundImage = Globals.imgLangSetting(1)
    End Sub

    Public Overrides Sub SetImage()
        Me.BackgroundImage = Globals.imgLangSetting(0)
    End Sub
End Class
