Module FormDesignAndControl
    Public Sub ControlMouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        DirectCast(sender, ControlBoxButton).SetHoverImage()
    End Sub

    Public Sub ControlMouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        DirectCast(sender, ControlBoxButton).SetImage()
    End Sub

    Public Sub ControlMouseClick(ByVal sender As Object, ByVal e As System.EventArgs)
        DirectCast(sender, ControlBoxButton).clicked(False)
    End Sub
End Module
