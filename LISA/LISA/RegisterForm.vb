Imports MySql.Data.MySqlClient

Module RegisterForm
    Public Sub Init()
        Dim frm As New GenericRegisterForm

        RemoveHandler frm.Controls("GenericControlboxButton0").Click, AddressOf FormDesignAndControl.ControlMouseClick
        AddHandler frm.Controls("GenericControlboxButton0").Click, AddressOf OverriddenControlMouseClick
        AddHandler frm.FormClosed, AddressOf frm_FormClosed

        frm.Show()
    End Sub

    Private Sub frm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs)
        Environment.Exit(0)
    End Sub

    Private Sub OverriddenControlMouseClick(ByVal sender As Object, ByVal e As System.EventArgs)
        If DirectCast(sender, ControlBoxButton).Tag.ToString = "10" Then
            DirectCast(sender, ControlBoxButton).FindForm.Hide()
            Main.Show()
        End If
    End Sub
End Module
