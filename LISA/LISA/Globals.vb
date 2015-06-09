Imports System.Drawing.Text
Imports MySql.Data.MySqlClient

Module Globals

#Region "Global Variables" 'All globally used variables are declared here
    'App ID (Complete or Registration only)
    Public AppID As Integer

    'Server Connection Variables
    Public DB_IP As String
    Public DB_Username As String
    Public DB_Password As String
    Public DB_Database As String

    'All datasets or tables storing information from the database
    Public neededTables As New DataTable
    Public allInformation As New Dictionary(Of Integer, DataSet)
    Public registerTranslations As New DataTable

    'Button images
    Public imgClose() As Bitmap = {My.Resources.ButtonClose, My.Resources.ButtonCloseHover}
    Public imgMinimize() As Bitmap = {My.Resources.ButtonMinimize, My.Resources.ButtonMinimizeHover}
    Public imgMaximize() As Bitmap = {My.Resources.ButtonMaximize, My.Resources.ButtonMaximizeHover, My.Resources.ButtonMaximized, My.Resources.ButtonMaximizedHover}
    Public imgLangSetting() As Bitmap = {My.Resources.LangSetting, My.Resources.LangSettingHover}
    Public imgRegisterForm() As Bitmap = {My.Resources.RegisterForm, My.Resources.RegisterFormHover}

    'Unnamed (for now)
    Public language As Integer
    Public languages() As String = {"Nederlands", "English", "Français", "Español", "Deutsch", "中国", "日本人", "Português"} 'Array of all possible languages which have translation in the database
#End Region

    Public Sub Init()
        GetServerVars()
        SetConnectionString()
        GetNeededTables()
        GetPreviousLanguage()
    End Sub

    Public Sub GetTranslations()
        registerTranslations = DatabaseRetrieval.RetrieveTableNames("vb_registerform_entries").Tables(0)
    End Sub

    Public Sub GetNeededTables()
        neededTables = DatabaseRetrieval.RetrieveTableNames("vb_menu").Tables(0)
    End Sub
    Private Sub GetServerVars()
        DB_IP = My.Resources.DB_IP
        DB_Username = My.Resources.DB_Username
        DB_Password = My.Resources.DB_Password
        DB_Database = My.Resources.DB_Database
    End Sub

    Public Sub GetDataFromServer()
        Globals.GetNeededTables()
        SplashLoading.BarLong(neededTables.Rows.Count * 10 + 20)
        SplashLoading.ShowBar((10))

        Dim i As Integer
        While i <= neededTables.Rows.Count - 1
            allInformation(i) = New DataSet
            allInformation(i) = DatabaseRetrieval.RetrieveTableNames(neededTables.Rows(i)(1).ToString)
            SplashLoading.ShowBar((i + 1) * 10)
            i += 1
            Threading.Thread.Sleep(100)
        End While

        Globals.GetTranslations()
        SplashLoading.ShowBar((i + 2) * 10)
    End Sub
    Private Sub GetPreviousLanguage()
        If GetSetting(My.Application.Info.ProductName, "General", "Language Setting") <> Nothing Then
            language = CInt(GetSetting(My.Application.Info.ProductName, "General", "Language Setting"))
        Else
            'Create new registry entry if the application runs for the first time (Default language is Dutch)
            SaveSetting(My.Application.Info.ProductName, "General", "Language Setting", "2")
        End If
    End Sub

    Public Structure Leerling
        Public Voornaam As String
        Public Naam As String
        Public Geslacht As String
        Public Geboortedatum As String
        Public Geboorteplaats As Integer 'Postcode?
        Public NationaliteitId As Integer 'Corresponds with id from 'nationalities' table
        Public Straatnaam As String
        Public HuisNummer As String
        Public BusNummer As String
        Public PostcodeId As Integer
        Public RijksRegNummer As String
        Public TelMobiel As String
        Public Email As String
        Public MoedertaalId As Integer 'Corresponds with id from 'languages' table
        Public SpreektaalId As Integer 'Corresponds with id from 'languages' table
        Public KlasId As Integer 'Corresponds with id from 'classes' table
        Public BroZus As Integer '# Brothers And/Or Sisters
        Public ZDPersoneel As Integer '?
        Public CorrGerAan As Integer '?
        Public OpmIvmVerblijfsAd As String 'Use?
        Public Rapport As Integer 'Usage?
        Public Attest As Integer 'Usage?
        Public Buitenpas As Integer 'Boolean wise?
        Public GodsKeuzeId As Integer 'Corresponds with id from 'religions' table
        Public OpmIvmTucht As String
        Public InschrijvingsStatus As Integer 'Boolean wise?
        Public InschrDatum As Date
        Public InschrUur As String
        Public StudieToelage As Integer 'Boolean wise?
        Public VerBuitGezin As Integer '?
        Public OudRondtrekBevol As Integer '?
        Public kerCirBinschip As Integer '?
    End Structure

    Public Function GetMainForm() As Form 'Trick vb so I can pick the startup form dynamically (hweh)
        Dim usageSetting As String = GetSetting(My.Application.Info.ProductName, "General", "Usage") 'Get the preset usage setting
        Dim start As Form = Global.LISA.FirstStart

        If usageSetting <> Nothing Then
            'Setting exists
            Select Case usageSetting
                Case "RegisterOnly"
                    start = Global.LISA.GenericRegisterForm
                    AppID = 1
                Case "Complete"
                    start = Global.LISA.Main
                    AppID = 2
                Case Else
                    MsgBox("Error: Couldn't fetch Usage Setting!" & vbNewLine & "Contact computer wizard!.", MsgBoxStyle.Critical)
                    Environment.Exit(0)
            End Select
        End If

        Return start
    End Function
End Module

'Colors:
'   Blue Form Buttons: Hex - #3998d6 | Argb - 57,152,214