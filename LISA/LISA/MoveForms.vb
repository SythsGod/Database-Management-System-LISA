Module MoveForms
    Private MouseIsDown As Boolean
    Private AlreadyLocked As Boolean
    Private SetPoint As Point
    Private Function LockOnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs) As Point 'Think of a good name for this function (Job: Locks the mouse coords on MouseDown)
        '1. Lock the position of your mouse (on the form) after mouse went down
        '2. Return said point
        '3. If the point had already been set we're just returning the previously set value!

        If Not AlreadyLocked Then
            Dim p As Point
            p = New Point(e.X, e.Y)
            AlreadyLocked = True
            Return p
        Else
            Return SetPoint
        End If
    End Function

    Public Sub MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        'Triggers whenever the mouse is held down or clicked
        Dim conditions As Boolean
        conditions = e.X >= 0 And e.X <= DirectCast(sender, Form).ClientRectangle.Width And e.Y >= 0 And e.Y <= 50

        MouseIsDown = conditions
    End Sub

    Public Sub MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        'Triggers on mouse movement
        If MouseIsDown Then
            SetPoint = LockOnMouseDown(e) 'Again lock the coords
            DirectCast(sender, Form).Location = New Point(Control.MousePosition.X - SetPoint.X, Control.MousePosition.Y - SetPoint.Y) 'Take your screen coords - your (locked) client coords, this way your mouse stays where it is and doesn't go to the point where the location of the form goes
        End If
    End Sub

    Public Sub MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        'Triggers when the mouse button is released
        '1. False some booleans (Yes, I know, "Verbs")
        MouseIsDown = False
        AlreadyLocked = False
    End Sub
    Public Sub Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs)
        Main.Show()
        DirectCast(sender, Form).Dispose()
    End Sub
End Module
