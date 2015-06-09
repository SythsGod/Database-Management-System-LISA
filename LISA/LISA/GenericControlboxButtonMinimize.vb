Public Class GenericControlboxButtonMinimize
    Inherits ControlBoxButton

    Sub New()
        MyBase.New()
    End Sub
    Public Overrides Sub SetHoverImage()
        Me.BackgroundImage = Globals.imgMinimize(1)
    End Sub

    Public Overrides Sub SetImage()
        Me.BackgroundImage = Globals.imgMinimize(0)
    End Sub

    Public Overrides Sub Clicked(ByVal hide As Boolean)
        MyBase.Minimize()
    End Sub
End Class
