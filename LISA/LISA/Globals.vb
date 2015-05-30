Imports System.Drawing.Text
Imports MySql.Data.MySqlClient

Module Globals

#Region "Global Variables" 'All globally used variables are declared here
    'Server Connection Variables
    Public DB_IP As String
    Public DB_Username As String
    Public DB_Password As String
    Public DB_Database As String

    'All datasets or tables storing information from the database
    Public neededTables As New DataTable
    Public allInformation As New Dictionary(Of Integer, DataSet)
    Public registerTranslations As New DataTable

    'Unnamed (for now)
    Public language As Integer
    Public languages() As String = {"Nederlands", "English", "Français", "Español", "Deutsch", "中国", "日本人", "Português"} 'Array of all possible languages which have translation in the database
#End Region

    Public Sub Init()
        GetServerVars()
        GetPreviousLanguage()
        SetConnectionString()
    End Sub
    Private Sub GetServerVars()
        DB_IP = My.Resources.DB_IP
        DB_Username = My.Resources.DB_Username
        DB_Password = My.Resources.DB_Password
        DB_Database = My.Resources.DB_Database
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
        Public InschrUur As TimeSpan
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
                Case "Complete"
                    start = Global.LISA.Main
                Case Else
                    MsgBox("Error: Couldn't fetch Usage Setting!" & vbNewLine & "Contact computer wizard!.", MsgBoxStyle.Critical)
                    Environment.Exit(0)
            End Select
        End If

        Return start
    End Function
End Module

'Tags:
'0 + i = buttons on main (<10)
'1 + i = Controlbox Buttons (<20)
'   10 = Close
'   11 = Maximize
'   12 = Minimize
'   13 = Maximize To Minimize
'   14 = Language Picker
'   15 = Register Form
'
'Colors:
'   Blue Form Buttons: Hex - #3998d6 | Argb - 57,152,214