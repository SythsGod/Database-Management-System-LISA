Public Class Main
    Public Sub New()
        InitializeComponent()
        Me.BackColor = Color.White
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Icon = My.Resources.LisaIconFlatIco
        Me.Name = "Form_Main"
        Me.Size = New Size(1280, 720)
        Me.StartPosition = FormStartPosition.CenterScreen

        Globals.Init()

        AddButtons(Me)
        AddLanguageButton(Me)
        AddRegisterButton(Me)
        AddCopyRightLabel(Me)

        ControlBoxButton.AddControlbox(Me)

        AddHandler Me.MouseUp, AddressOf MoveForms.MouseUp
        AddHandler Me.MouseMove, AddressOf MoveForms.MouseMove
        AddHandler Me.MouseDown, AddressOf MoveForms.MouseDown
        AddHandler Me.Load, AddressOf Main_Load
    End Sub

    Private Sub Main_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Globals.GetDataFromServer()
    End Sub

    Private Sub AddButtons(ByVal frm As Form)
        Dim f As Integer = 0
        Dim g As Integer = 0

        For i = 0 To neededTables.Rows.Count - 2
            Dim btn As New GenericButton

            btn.Location = New Point(180 + 500 * f, 132 + ((75 + 20) * (i - g)))
            btn.Name = "GenericButton_" & i
            btn.Tag = i
            btn.Text = neededTables.Rows(i)(language).ToString

            AddHandler btn.Click, AddressOf GenericButtonForm_Click
            frm.Controls.Add(btn)

            If i = 4 Then
                f = 1
                g = 5
            End If
        Next
    End Sub

    Private Sub AddLanguageButton(ByVal frm As Form)
        Dim btn As New GenericControlboxButtonLanguage()

        btn.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn.Location = New Point(frm.ClientRectangle.Width - 55, frm.ClientRectangle.Height - 55)
        btn.Name = "GenericButtonLanguageSettings"
        btn.Size = New Size(50, 50)

        AddHandler btn.Click, AddressOf GenericButtonLanguage_Click
        AddHandler btn.MouseEnter, AddressOf FormDesignAndControl.ControlMouseEnter
        AddHandler btn.MouseLeave, AddressOf FormDesignAndControl.ControlMouseLeave

        frm.Controls.Add(btn)
    End Sub

    Private Sub AddRegisterButton(ByVal frm As Form)
        Dim btn As New GenericControlboxButtonRegister()

        btn.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btn.Location = New Point(5, frm.ClientRectangle.Height - 55)
        btn.Name = "GenericButtonRegisterForm"
        btn.Size = New Size(50, 50)

        AddHandler btn.Click, AddressOf GenericButtonRegister_Click
        AddHandler btn.MouseEnter, AddressOf FormDesignAndControl.ControlMouseEnter
        AddHandler btn.MouseLeave, AddressOf FormDesignAndControl.ControlMouseLeave

        frm.Controls.Add(btn)
    End Sub

    Private Sub GenericButtonForm_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Hide()

        DynamicSubFormCreation.Init(CInt(DirectCast(sender, GenericButton).Tag), neededTables(CInt(DirectCast(sender, GenericButton).Tag))(language).ToString)
    End Sub

    Private Sub GenericButtonLanguage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        LanguageSettings.Init()
    End Sub

    Private Sub GenericButtonRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        DirectCast(sender, ControlBoxButton).FindForm.Hide()

        GenericRegisterForm.Show()
    End Sub

    Private Sub AddCopyRightLabel(ByVal frm As Form)
        Dim lbl As New Label With {.Text = "© 2014 - " & Date.Now.Year, .Name = "GenericLabel", .Font = New Font("Tahoma", 8)}
        lbl.Location = New Point(CInt(frm.ClientRectangle.Width / 2 - lbl.Width / 2), frm.ClientRectangle.Height - 20)
        frm.Controls.Add(lbl)
    End Sub
End Class