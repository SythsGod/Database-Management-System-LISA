Public Class GenericControlboxButtonClose
    Inherits ControlBoxButton

    Sub New()
        MyBase.New()
        SetImage()
    End Sub

    Public Overrides Sub SetImage()
        Me.BackgroundImage = Globals.imgClose(0)
    End Sub

    Public Overrides Sub SetHoverImage()
        Me.BackgroundImage = Globals.imgClose(1)
    End Sub

    Public Overrides Sub Clicked(ByVal hide As Boolean)
        MyBase.Close(hide)
    End Sub
End Class
