'Blue buttons:
'Hex: #3998d6 | Argb: 57,152,214

Imports System.Runtime.InteropServices

Public Class LISA
    'Globals
    Private SwitchRow As Boolean = False 'Switch for 2 rows of buttons

    Public Sub New()
        InitializeComponent()
        Me.BackColor = Color.White 'Form's background color
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None 'Change to form to a flat control (Reason: Aesthetics)
        Me.Size = New Size(1280, 720) 'Form's size
        Me.StartPosition = FormStartPosition.CenterScreen 'StartPos of the form (Center of your screen)

        GetPreviousLanguage()
        GetServerVars() 'Retrieve the server variables from the resources and assign them to variables
        OpenConnection() 'Open the database connection
        TableNamesInDatabase = RetrieveTableNames("vb_menu").Tables(0) 'Get the names of all the tables this application will need
        CreateButtons() 'Create all the buttons which need to be placed on the form
        CreateControlBox() 'Create the three 'ControlBox' controls (buttons)
        AddLanguageSettings() 'Add a language button to the bottom-right of the form

        AddHandler Me.MouseUp, AddressOf MoveForms.MouseUp
        AddHandler Me.MouseMove, AddressOf MoveForms.MouseMove
        AddHandler Me.MouseDown, AddressOf MoveForms.MouseDown
    End Sub

    Private Sub LISA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        MsgBox(currentLang)
    End Sub

    'Handles the display of the progressbar on startup (might change to just show logo)
    Private Sub Main_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SplashLoading.BarLong(TableNamesInDatabase.Rows.Count * 10)
        Dim i As Integer = 0
        While i <= TableNamesInDatabase.Rows.Count - 1
            AllTableInformation(i) = New DataSet
            AllTableInformation(i) = DatabaseRetrieval.RetrieveTableNames(TableNamesInDatabase.Rows(i)(1).ToString)
            SplashLoading.ShowBar((i + 1) * 10)
            i += 1
            Threading.Thread.Sleep(100)
        End While
    End Sub
    
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
            .Text = TableNamesInDatabase.Rows(i)(currentLang).ToString 'Set the text shown on the button

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

        AddHandler button1.Click, AddressOf ButtonForm_Click 'Add button click event where it's decided what should happen (See event)
        Me.Controls.Add(button1) 'Add the button to the form
    End Sub

    'Create the custom ControlBox for the form
    Private Sub CreateControlBox()
        '-------> YAY FOR LESS CODE <-------- What did I change? o.o

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

            AddHandler button1.Click, AddressOf ControlBoxButton_Click
            AddHandler button1.MouseEnter, AddressOf ControlMouseEnter
            AddHandler button1.MouseLeave, AddressOf ControlMouseLeave
            Me.Controls.Add(button1)
        Next
    End Sub

    'Title bar (might be removed)
    'Private Sub AddTitleLabel()
    '    Dim label1 As New Label

    '    With label1
    '        .Name = "lblTitle"
    '        .Text = "Hoofd Menu:"
    '        .TextAlign = ContentAlignment.MiddleLeft
    '        .Location = New Point(30, CInt((26 - label1.Height) / 2))
    '        .BackColor = Color.Transparent
    '        .Tag = -1
    '    End With

    '    Me.Controls.Add(label1)
    'End Sub

    'Unused
    'Private Sub Main_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    '    Dim brush As New SolidBrush(Color.FromArgb(255, 57, 152, 214)) 'Coral: Color.FromArgb(255, 255, 127, 80)
    '    Dim paper As Graphics

    '    paper = e.Graphics

    '    'paper.FillRectangle(brush, 0, 0, Me.ClientRectangle.Width - 78, 26) 'I'm cheating here, don't tell anyone
    'End Sub

    Private Sub AddLanguageSettings()
        Dim button1 As New ControlBoxButton

        With button1
            .Tag = 14
            .Name = "btnLanguageSwitch"
            .Location = New Point(Me.ClientRectangle.Width - 55, Me.ClientRectangle.Height - 55)
            .Size = New Size(50, 50)
            .Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
            .FlatAppearance.BorderSize = 0
            .BackgroundImage = My.Resources.Language_Settings_Icon__Inactive_
            .FlatAppearance.MouseDownBackColor = Color.Transparent
            .FlatAppearance.MouseOverBackColor = Color.Transparent
            .SetImage = 4
        End With

        AddHandler button1.MouseEnter, AddressOf ControlMouseEnter
        AddHandler button1.MouseLeave, AddressOf ControlMouseLeave
        AddHandler button1.Click, AddressOf ButtonLanguage_Click
        Me.Controls.Add(button1)
    End Sub

    Private Sub ButtonForm_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        SwitchWindows(CInt(DirectCast(sender, Button).Tag))
    End Sub

    Private Sub ControlBoxButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As ControlBoxButton = DirectCast(sender, ControlBoxButton)

        Select Case CInt(DirectCast(sender, ControlBoxButton).Tag)
            Case 10
                Me.Close()
            Case 11
                btn.Maximize()
            Case 12
                btn.Minimize()
            Case 13
                btn.Maximize()
        End Select
    End Sub

    Private Sub ButtonLanguage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'For Each btn In Me.Controls.OfType(Of Button)()
        '    If CInt(btn.Tag) < 10 And LangSwitch Then
        '        btn.Text = TableNamesInDatabase(CInt(btn.Tag))(2).ToString
        '    ElseIf CInt(btn.Tag) < 10 And Not LangSwitch Then
        '        btn.Text = CStr(TableNamesInDatabase(CInt(btn.Tag))(3))
        '    End If

        '    If btn.Name = "btnLanguageSwitch" And LangSwitch Then
        '        btn.Text = "EN"
        '    ElseIf btn.Name = "btnLanguageSwitch" And Not LangSwitch Then
        '        btn.Text = "NL"
        '    End If

        '    For Each lbl In Me.Controls.OfType(Of Label)()
        '        If CInt(lbl.Tag) = -1 And Not LangSwitch Then
        '            lbl.Text = "Main Menu:"
        '        ElseIf CInt(lbl.Tag) = -1 And LangSwitch Then
        '            lbl.Text = "Hoofd Menu:"
        '        End If
        '    Next
        'Next

        'LangSwitch = Not LangSwitch

        LanguageSettings.CreateForm()
    End Sub

    Private Sub SwitchWindows(ByVal tag As Integer)
        'TO-DO CHANGE THIS FFS (Because this is not how we do things)
        'I CHANGED IT! ^w^
        'I forgot what I changed ;^;
        'I'm not removing this part! >:D
        Me.Hide()

        Dim name As String
        name = TableNamesInDatabase(tag)(currentLang).ToString
        DynamicSubFormCreation.CreateSubForm(tag, name)
    End Sub
End Class