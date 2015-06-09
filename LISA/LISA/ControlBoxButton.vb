'This class was created to handle the late binding caused by changing the background image from the Maximize button during runtime and now it serves a purpose for solving my laziness problem
Imports System.Drawing
Imports System.Windows.Forms

Public MustInherit Class ControlBoxButton
    Inherits Button
    Private mIsDisabled As Boolean = False

    Sub New()
        Me.BackgroundImageLayout = ImageLayout.Center 'Set the ImageBackground to center
        Me.Anchor = AnchorStyles.Top Or AnchorStyles.Right 'Anchor the buttons on Top-Right,
        Me.BackColor = Color.Transparent
        Me.FlatStyle = Windows.Forms.FlatStyle.Flat
        Me.FlatAppearance.MouseOverBackColor = Color.Transparent
        Me.FlatAppearance.MouseDownBackColor = Color.Transparent
        Me.FlatAppearance.BorderColor = Color.White
        Me.Size = New Size(30, 30)
        Me.SetStyle(ControlStyles.Selectable, False)

        SetImage()
    End Sub

    'Did it in a better, perhaps, more efficient way (GUESS WHAT!? I CHANGED IT!! AGAIN!!)
    Public MustOverride Sub SetImage()

    'Change the image (should only be used together with a MouseHover event)
    Public MustOverride Sub SetHoverImage()

    Public Property isDisabled() As Boolean
        Get
            Return Me.mIsDisabled
        End Get
        Set(value As Boolean)
            Me.mIsDisabled = value
        End Set
    End Property

    Protected Sub Close(ByVal hide As Boolean)
        If hide Then
            Me.FindForm.Hide()
            Main.Show()
        Else
            Me.FindForm.Close()
        End If
    End Sub

    Protected Sub Minimize()
        Me.FindForm.WindowState = FormWindowState.Minimized
    End Sub

    Protected Sub Maximize()
        If Me.FindForm.WindowState = FormWindowState.Normal And Not Me.isDisabled Then
            Me.FindForm.WindowState = FormWindowState.Maximized
        ElseIf Not Me.isDisabled And Me.FindForm.WindowState = FormWindowState.Maximized Then
            Me.FindForm.WindowState = FormWindowState.Normal
        End If
    End Sub

    Public MustOverride Overloads Sub clicked(ByVal hide As Boolean)

    Public Shared Sub AddControlbox(ByVal frm As Form)
        Dim btnClose As New GenericControlboxButtonClose() With {.Name = "GenericControlBoxButtonClose", .Location = New Point(frm.ClientRectangle.Width - 29, -1)}
        Dim btnMinimize As New GenericControlboxButtonMinimize() With {.Name = "GenericControlboxButtonMinimize", .Location = New Point(frm.ClientRectangle.Width - 85, -1)}
        Dim btnMaximize As New GenericControlboxButtonMaximize() With {.Name = "GenericControlboxButtonMaximize", .Location = New Point(frm.ClientRectangle.Width - 57, -1)}

        AddHandler btnClose.Click, AddressOf FormDesignAndControl.ControlMouseClick
        AddHandler btnMinimize.Click, AddressOf FormDesignAndControl.ControlMouseClick
        AddHandler btnMaximize.Click, AddressOf FormDesignAndControl.ControlMouseClick
        AddHandler btnClose.MouseEnter, AddressOf FormDesignAndControl.ControlMouseEnter
        AddHandler btnMinimize.MouseEnter, AddressOf FormDesignAndControl.ControlMouseEnter
        AddHandler btnMaximize.MouseEnter, AddressOf FormDesignAndControl.ControlMouseEnter
        AddHandler btnClose.MouseLeave, AddressOf FormDesignAndControl.ControlMouseLeave
        AddHandler btnMinimize.MouseLeave, AddressOf FormDesignAndControl.ControlMouseLeave
        AddHandler btnMaximize.MouseLeave, AddressOf FormDesignAndControl.ControlMouseLeave

        frm.Controls.Add(btnClose)
        frm.Controls.Add(btnMinimize)
        frm.Controls.Add(btnMaximize)
    End Sub
End Class
