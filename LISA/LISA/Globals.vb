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
End Module

'Tags:
'0 + i = buttons on main (<10)
'1 + i = Controlbox Buttons (<20)
'   10 = Close
'   11 = Maximize
'   12 = Minimize
'   13 = Maximize To Minimize
'   14 = Language Picker
