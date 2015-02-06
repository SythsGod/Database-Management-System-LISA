'Blue buttons:
'Hex: #3998d6 | Argb: 57,152,214

Imports System.Runtime.InteropServices

Public Class LISA

    'Globals
    Private MouseIsDown As Boolean = False
    Private AantButtons As Integer = 10
    Private Switch As Boolean = False
    Private AlreadyLocked As Boolean
    Private setPoint As Point

    Public Sub New()
        InitializeComponent()
        Me.Size = New Size(1280, 720)
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.White
        'Me.DoubleBuffered = True
        'Me.BackgroundImage = My.Resources.IMG_BG_BLUR
        'Me.BackgroundImageLayout = ImageLayout.Stretch

        GetServerVars()
        OpenConnection()
        TableNamesInDatabase = RetrieveTableNames("vb_menu").Tables(0)
        CreateButtons()
        CreateControlBox()
        AddTitleLabel()
        AddLanguagePicker()
    End Sub

    'Unused
    Private Sub CacheFonts()
        'See; http://stackoverflow.com/questions/2928383/use-resource-font-directly-in-vb-net-c

        Dim fontBuffer As IntPtr
        Dim font() As Byte
        font = My.Resources.Sansation_Regular
        fontBuffer = Marshal.AllocCoTaskMem(font.Length)
        Marshal.Copy(font, 0, fontBuffer, font.Length)
    End Sub

    '---------------------------------------------------------------------------------------------------------------------------------------
    '----------------------------------------------FORM CREATION----------------------------------------------------------------------------
    '---------------------------------------------------------------------------------------------------------------------------------------

    Private Sub AddTitleLabel()
        Dim label1 As New Label

        With label1
            .Name = "lblTitle"
            .Text = "Hoofd Menu:"
            .TextAlign = ContentAlignment.MiddleLeft
            .Location = New Point(30, CInt((15 - label1.Height) / 2))
            .BackColor = Color.Transparent
            .Tag = -1
        End With

        Me.Controls.Add(label1)
    End Sub

    Private Sub ButtonPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        'Remove the 'black given border' on a flat button by painting over it using the same background color as the button itself
        Dim button1 = DirectCast(sender, Button)
        Using P As New Pen(Color.FromArgb(255, 57, 152, 214))
            e.Graphics.DrawRectangle(P, 1, 1, button1.Width - 3, button1.Height - 3)
        End Using
    End Sub

    Private Sub Main_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SplashLoading.BarLong(TableNamesInDatabase.Rows.Count * 10)
        Dim i As Integer = 0
        While i <= TableNamesInDatabase.Rows.Count - 1
            AllTableInformation(i) = New DataSet
            AllTableInformation(i) = DatabaseRetrieval.RetrieveTableNames(TableNamesInDatabase.Rows(i)(1).ToString)
            splashLoading.ShowBar((i + 1) * 10)
            i += 1
            Threading.Thread.Sleep(100)
        End While
    End Sub

    Private Function ThinkOfAGoodName(ByVal e As System.Windows.Forms.MouseEventArgs) As Point
        'First thing: Lock the position of your mouse (on the form) after mouse went down
        If Not AlreadyLocked Then
            Dim p As Point
            p = New Point(e.X, e.Y)
            AlreadyLocked = True
            Return p
            'Return said point
        Else
            Return setPoint
            'If the point had already been set we're just returning the previously set value
        End If
    End Function

    Private Sub Main_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        Dim conditions As Boolean
        conditions = e.X >= 0 And e.X <= Me.ClientRectangle.Width And e.Y >= 0 And e.Y <= 50

        MouseIsDown = conditions
    End Sub

    Private Sub Main_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If MouseIsDown Then
            setPoint = ThinkOfAGoodName(e) 'Again lock the coords
            Me.Location = New Point(Control.MousePosition.X - setPoint.X, Control.MousePosition.Y - setPoint.Y) 'Take your screen coords - your (locked) client coords, this way your mouse stays where it is and doesn't go to the point where the location of the form goes
        End If
    End Sub

    Private Sub Main_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        MouseIsDown = False
        AlreadyLocked = False
    End Sub

    Private Sub Main_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim brush As New SolidBrush(Color.FromArgb(255, 57, 152, 214)) 'Coral: Color.FromArgb(255, 255, 127, 80)
        Dim paper As Graphics

        'Draw a rectangle of lines around the form
        'Dim points() As Point = {New Point(1, 1), New Point(Me.Width - 1, 1), New Point(Me.Width - 1, Me.Height - 1), New Point(1, Me.Height - 1), New Point(1, 1)}

        paper = e.Graphics

        paper.FillRectangle(brush, 0, 0, Me.ClientRectangle.Width - 90, 26) 'I'm cheating here, don't tell anyone
    End Sub

    Private Sub CreateControlBox()
        'Dim button1, button2, button3 As New Button

        'With button1 'Close
        '    .Anchor = AnchorStyles.Top Or AnchorStyles.Right
        '    .Name = "btnClose"
        '    .Tag = 10
        '    .Size = New Size(40, 40)
        '    .Location = New Point(Me.ClientRectangle.Width - 45, 5)
        '    .FlatStyle = FlatStyle.Flat
        '    .BackgroundImage = My.Resources.Button_Close
        '    .BackgroundImageLayout = ImageLayout.Center
        'End With

        'AddHandler button1.Click, AddressOf OnButtonClick
        'AddHandler button1.Paint, AddressOf ButtonPaint
        'Me.Controls.Add(button1)

        'With button2 'Maximize
        '    .Anchor = AnchorStyles.Top Or AnchorStyles.Right
        '    .Name = "btnMaximize"
        '    .Tag = 11
        '    .Size = New Size(40, 40)
        '    .Location = New Point(Me.ClientRectangle.Width - 90, 5)
        '    .FlatStyle = FlatStyle.Flat
        '    .BackgroundImage = My.Resources.Button_Maximize_0
        '    .BackgroundImageLayout = ImageLayout.Center
        'End With

        'AddHandler button2.Click, AddressOf OnButtonClick
        'AddHandler button2.Paint, AddressOf ButtonPaint
        'Me.Controls.Add(button2)

        'With button3 'Minimize
        '    .Anchor = AnchorStyles.Top Or AnchorStyles.Right
        '    .Name = "btnMinimize"
        '    .Tag = 12
        '    .Size = New Size(40, 40)
        '    .Location = New Point(Me.ClientRectangle.Width - 135, 5)
        '    .FlatStyle = FlatStyle.Flat
        '    .BackgroundImage = My.Resources.Button_Minimize
        'End With

        'AddHandler button3.Click, AddressOf OnButtonClick
        'AddHandler button3.Paint, AddressOf ButtonPaint
        'Me.Controls.Add(button3)

        '-------> YAY FOR LESS CODE <--------

        For i = 0 To 2
            Dim button1 As New ControlBoxButton
            With button1
                .Name = "GenericControlBox_" & i
                .Tag = "1" & i
                .Location = New Point(Me.ClientRectangle.Width - 30 * (i + 1), 0)
                .BackColor = Color.Transparent
                .SetImage = i
            End With

            AddHandler button1.Click, AddressOf OnButtonClick
            AddHandler button1.MouseEnter, AddressOf ControlMouseEnter
            AddHandler button1.MouseLeave, AddressOf ControlMouseLeave
            'AddHandler button1.Paint, AddressOf ButtonPaint
            Me.Controls.Add(button1)
        Next
    End Sub

    Private Sub ControlMouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        DirectCast(sender, ControlBoxButton).SetHover = True
        DirectCast(sender, ControlBoxButton).SetHoverImage = CInt(DirectCast(sender, ControlBoxButton).Tag) - 10
    End Sub

    Private Sub ControlMouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        DirectCast(sender, ControlBoxButton).SetHover = False
        DirectCast(sender, ControlBoxButton).SetImage = CInt(DirectCast(sender, ControlBoxButton).Tag) - 10
    End Sub

    Private Sub AddLanguagePicker()
        Dim button1 As New Button

        With button1
            .Tag = 14
            .Name = "btnLanguageSwitch"
            .Text = "EN"
            .Location = New Point(Me.ClientRectangle.Width - 50, Me.ClientRectangle.Height - 50)
            .Size = New Size(50, 50)
            .BackColor = Color.White
            .Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderColor = Color.FromArgb(255, 57, 152, 214)
            .FlatAppearance.BorderSize = 2
            .FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 57, 152, 214)
        End With

        AddHandler button1.Click, AddressOf OnClickLanguageChange
        Me.Controls.Add(button1)
    End Sub

    '---------------------------------------------------------------------------------------------------------------------------------------
    '----------------------------------------------FORM CREATION----------------------------------------------------------------------------
    '---------------------------------------------------------------------------------------------------------------------------------------

    Private Sub OnClickLanguageChange(ByVal sender As Object, ByVal e As System.EventArgs)
        For Each btn In Me.Controls.OfType(Of Button)()
            If CInt(btn.Tag) < 10 And LangSwitch Then
                btn.Text = TableNamesInDatabase(CInt(btn.Tag))(2).ToString
            ElseIf CInt(btn.Tag) < 10 And Not LangSwitch Then
                btn.Text = CStr(TableNamesInDatabase(CInt(btn.Tag))(3))
            End If

            If btn.Name = "btnLanguageSwitch" And LangSwitch Then
                btn.Text = "EN"
            ElseIf btn.Name = "btnLanguageSwitch" And Not LangSwitch Then
                btn.Text = "NL"
            End If

            For Each lbl In Me.Controls.OfType(Of Label)()
                If CInt(lbl.Tag) = -1 And Not LangSwitch Then
                    lbl.Text = "Main Menu:"
                ElseIf CInt(lbl.Tag) = -1 And LangSwitch Then
                    lbl.Text = "Hoofd Menu:"
                End If
            Next
        Next

        LangSwitch = Not LangSwitch
    End Sub

    Private Sub CreateButtons()
        For i = 0 To TableNamesInDatabase.Rows.Count - 1
            CreateButton(i)
        Next
    End Sub

    Private Sub CreateButton(ByVal i As Integer)
        Dim button1 As New Button

        With button1
            .Anchor = AnchorStyles.None
            .BackColor = Color.White
            .Name = "btn_" & TableNamesInDatabase.Rows(i)(1).ToString
            .Text = TableNamesInDatabase.Rows(i)(2).ToString
            .Size = New Size(420, 75)
            .FlatAppearance.BorderColor = Color.FromArgb(255, 57, 152, 214)
            .FlatAppearance.BorderSize = 2
            .FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 57, 152, 214)
            If Switch Then
                .Location = New Point(680, 132 + ((75 + 20) * (i - 5)))
            Else
                .Location = New Point(180, 132 + ((75 + 20) * i))
            End If
            .Tag = i
            .FlatStyle = FlatStyle.Flat
            '.Font = New Font(CustomFonts.Families(0), 10)
        End With

        If i = 4 Then
            Switch = True
        End If

        AddHandler button1.Click, AddressOf OnButtonClick
        'AddHandler button1.Paint, AddressOf ButtonPaint

        Me.Controls.Add(button1)
    End Sub

    Private Sub OnButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim tag As Integer = CInt(DirectCast(sender, Button).Tag)

        If tag < 10 Then
            SwitchWindows(tag)
        Else
            Select Case tag
                Case 10
                    Me.Close()
                Case 11 Or 13
                    If Me.WindowState = FormWindowState.Normal Then
                        Me.WindowState = FormWindowState.Maximized
                        DirectCast(sender, ControlBoxButton).SetImage = 3
                        DirectCast(sender, ControlBoxButton).Tag = 13
                    Else
                        Me.WindowState = FormWindowState.Normal
                        DirectCast(sender, ControlBoxButton).SetImage = 1
                        DirectCast(sender, ControlBoxButton).Tag = 11
                    End If
                Case 12
                    Me.WindowState = FormWindowState.Minimized
            End Select
        End If
    End Sub

    Private Sub SwitchWindows(ByVal tag As Integer)
        Me.Hide()

        'TO-DO CHANGE THIS FFS (Because this is not how we do things)
        'I CHANGED IT! ^w^
        'I forgot what I changed ;^;

        Dim name As String
        If LangSwitch Then
            name = TableNamesInDatabase(tag)(3).ToString
        Else
            name = TableNamesInDatabase(tag)(2).ToString
        End If
        DynamicSubFormCreation.CreateSubForm(tag, name)
    End Sub
End Class