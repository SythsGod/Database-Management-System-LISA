Module FormDesignAndControl
    Public Sub ControlMouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        DirectCast(sender, ControlBoxButton).SetHover = True
        DirectCast(sender, ControlBoxButton).SetHoverImage = CInt(DirectCast(sender, ControlBoxButton).Tag) - 10
    End Sub

    Public Sub ControlMouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        DirectCast(sender, ControlBoxButton).SetHover = False
        DirectCast(sender, ControlBoxButton).SetImage = CInt(DirectCast(sender, ControlBoxButton).Tag) - 10
    End Sub
End Module
