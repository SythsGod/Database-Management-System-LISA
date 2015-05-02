Imports System.Drawing.Text
Imports MySql.Data.MySqlClient

Module Globals
    Public LangSwitch As Boolean = False 'If false lang="Dutch" else lang="English"
    Public ServerVars(3) As String
    Public TableNamesInDatabase As New DataTable
    Public AllTableInformation As New Dictionary(Of Integer, DataSet)
    Public isConnected As Boolean
    Public myConn As New MySqlConnection
    Public currentLang As Integer = 2 '= Dutch
    Public languages() As String = {"Nederlands", "English", "Français", "Español", "Deutsch", "中国", "", ""}
    Public registerEntries() As String = {"Voornaam", "Naam", "Geslacht", "Geboortedatum", "Geboorteplaats", "Nationaliteit", "Straatnaam", "Nummer", "Busnummer", "Postcode", "Rijksregister Nummer", "Telefoon/Mobiel", "Email", "Moedertaal", "Spreektaal", "Klas", "Broes/Zussen", "Opmerkingen verblijfsAd", "Godskeuze", "Opmerkingen tucht", "Studietoelage"}
    Public needsDropDown() As Boolean = {False, False, True, False, False, True, False, False, False, False, False, False, False, True, True, True, True, False, True, False, False}
    Public registerForm_ As New GenericForm("GenericForm_Register")

    Public Sub GetServerVars()
        'Change this. I HATE IT.
        ServerVars(0) = My.Resources.SERVER_VAR_0
        ServerVars(1) = My.Resources.SERVER_VAR_1
        ServerVars(2) = My.Resources.SERVER_VAR_2
        ServerVars(3) = My.Resources.SERVER_VAR_3
    End Sub

    Public Sub GetPreviousLanguage()
        If GetSetting(My.Application.Info.ProductName, "General", "Language Setting") <> Nothing Then
            currentLang = CInt(GetSetting(My.Application.Info.ProductName, "General", "Language Setting"))
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