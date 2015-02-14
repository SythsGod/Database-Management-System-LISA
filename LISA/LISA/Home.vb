'Blue buttons:
'Hex: #3998d6 | Argb: 57,152,214

Imports System.Runtime.InteropServices

Public Class LISA

    'Globals
    Private MouseIsDown As Boolean = False
    Private AantButtons As Integer = 10
    Private SwitchRow As Boolean = False
    Private AlreadyLocked As Boolean
    Private setPoint As Point

    Public Sub New()
        InitializeComponent()
        Me.BackColor = Color.White 'Form's background color
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None 'Change to form to a flat control (Reason: Aesthetics)
        Me.Size = New Size(1280, 720) 'Form's size
        Me.StartPosition = FormStartPosition.CenterScreen 'StartPos of the form (Center of your screen)

        GetServerVars() 'Retrieve the server variables from the resources and assign them to variables
        OpenConnection() 'Open the database connection
        TableNamesInDatabase = RetrieveTableNames("vb_menu").Tables(0) 'Get the names of all the tables this application will need
        CreateButtons() 'Create all the buttons which need to be placed on the form
        CreateControlBox() 'Create the three 'ControlBox' controls (buttons)
        'AddTitleLabel() 'Add a title bar in the form of a label at the top of the form (might remove on main form)
        AddLanguagePicker() 'Add a language button to the bottom-right of the form
    End Sub

    'Handles the display of the progressbar on startup (might change to just show logo)
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

    '// Form Elements: ---------------------------------------------------------------------------------

    'Loop for all the buttons
    Private Sub CreateButtons()
        'Create X-amount of buttons according to this table
        For i = 0 To TableNamesInDatabase.Rows.Count - 1
            CreateButton(i)
        Next
    End Sub

    'Creation of a single button
    Private Sub CreateButton(ByVal i As Integer)
        Dim button1 As New FormButton

        With button1
            .Name = "btn_" & TableNamesInDatabase.Rows(i)(1).ToString 'Set the name
            .Tag = i 'Give the button a tag (Used later for decision making)
            .Text = TableNamesInDatabase.Rows(i)(2).ToString 'Set the text shown on the button

            'Switch the row so you create two columns and set the location of the button
            If SwitchRow Then
                .Location = New Point(680, 132 + ((75 + 20) * (i - 5)))
            Else
                .Location = New Point(180, 132 + ((75 + 20) * i))
            End If
        End With

        'Change the boolean to switch rows
        If i = 4 Then
            SwitchRow = True
        End If

        AddHandler button1.Click, AddressOf OnButtonClick 'Add button click event where it's decided what should happen (See event)

        Me.Controls.Add(button1) 'Add the button to the form
    End Sub

    'Create the custom ControlBox for the form
    Private Sub CreateControlBox()
        '-------> YAY FOR LESS CODE <--------

        For i = 0 To 2
            Dim button1 As New ControlBoxButton
            With button1
                .Name = "GenericControlBox_" & i
                .Tag = "1" & i
                .Location = New Point(Me.ClientRectangle.Width - 30 * (i + 1) + 1, -1)
                .Size = New Size(30, 30)
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

    'Title bar (might be removed)
    Private Sub AddTitleLabel()
        Dim label1 As New Label

        With label1
            .Name = "lblTitle"
            .Text = "Hoofd Menu:"
            .TextAlign = ContentAlignment.MiddleLeft
            .Location = New Point(30, CInt((26 - label1.Height) / 2))
            .BackColor = Color.Transparent
            .Tag = -1
        End With

        Me.Controls.Add(label1)
    End Sub
    
    'Think of a good name for this function (Job: Locks the mouse coords on MouseDown
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

    'Triggers whenever the mouse is held down or clicked
    Private Sub Main_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        Dim conditions As Boolean
        conditions = e.X >= 0 And e.X <= Me.ClientRectangle.Width And e.Y >= 0 And e.Y <= 50

        MouseIsDown = conditions
    End Sub

    'Triggers on mouse movement
    Private Sub Main_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If MouseIsDown Then
            setPoint = ThinkOfAGoodName(e) 'Again lock the coords
            Me.Location = New Point(Control.MousePosition.X - setPoint.X, Control.MousePosition.Y - setPoint.Y) 'Take your screen coords - your (locked) client coords, this way your mouse stays where it is and doesn't go to the point where the location of the form goes
        End If
    End Sub

    'Triggers when the mouse button is released
    Private Sub Main_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        'False some booleans (Yes, I know, "Verbs")
        MouseIsDown = False
        AlreadyLocked = False
    End Sub

    'Unused
    Private Sub Main_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim brush As New SolidBrush(Color.FromArgb(255, 57, 152, 214)) 'Coral: Color.FromArgb(255, 255, 127, 80)
        Dim paper As Graphics

        paper = e.Graphics

        'paper.FillRectangle(brush, 0, 0, Me.ClientRectangle.Width - 78, 26) 'I'm cheating here, don't tell anyone
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
        Dim button1 As New FormButton

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
            .FlatAppearance.BorderSize = 0
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