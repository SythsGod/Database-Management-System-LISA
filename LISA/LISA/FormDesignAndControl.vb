Module FormDesignAndControl
    Public Sub ControlMouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        DirectCast(sender, ControlBoxButton).SetHover = True
        DirectCast(sender, ControlBoxButton).SetHoverImage(CInt(DirectCast(sender, ControlBoxButton).Tag) - 10)
    End Sub

    Public Sub ControlMouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        DirectCast(sender, ControlBoxButton).SetHover = False
        DirectCast(sender, ControlBoxButton).SetImage(CInt(DirectCast(sender, ControlBoxButton).Tag) - 10)
    End Sub

    Public Sub ControlMouseClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As ControlBoxButton = DirectCast(sender, ControlBoxButton)

        Select Case CInt(DirectCast(sender, Button).Tag)
            Case 10 'Close
                DirectCast(sender, ControlBoxButton).FindForm.Close()
            Case 11 'Maximize
                btn.Maximize()
            Case 12 'Minimize
                btn.Minimize()
            Case 13 'Maximize to minimize
                btn.Maximize()
        End Select
    End Sub
End Module
