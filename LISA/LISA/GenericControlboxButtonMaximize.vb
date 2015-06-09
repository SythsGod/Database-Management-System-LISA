Public Class GenericControlboxButtonMaximize
    Inherits ControlBoxButton

    Sub New()
        MyBase.New()
        SetImage()
    End Sub
    Public Overrides Sub SetHoverImage()
        If Me.FindForm.WindowState = FormWindowState.Normal Then
            Me.BackgroundImage = Globals.imgMaximize(1)
        Else
            Me.BackgroundImage = Globals.imgMaximize(3)
        End If
    End Sub

    Public Overrides Sub SetImage()
        If Not Me.FindForm Is Nothing Then
            If Me.FindForm.WindowState = FormWindowState.Normal Then
                Me.BackgroundImage = Globals.imgMaximize(0)
            Else
                Me.BackgroundImage = Globals.imgMaximize(2)
            End If
        Else
            Me.BackgroundImage = Globals.imgMaximize(0)
        End If
    End Sub

    Public Overrides Sub Clicked(ByVal hide As Boolean)
        MyBase.Maximize()
    End Sub
End Class
