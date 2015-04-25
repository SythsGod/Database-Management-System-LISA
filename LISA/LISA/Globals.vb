Imports System.Drawing.Text
Imports MySql.Data.MySqlClient

Module Globals
    Public LangSwitch As Boolean = False 'If false lang="Dutch" else lang="English"
    Public ServerVars(3) As String
    Public TableNamesInDatabase As New DataTable
    Public AllTableInformation As New Dictionary(Of Integer, DataSet)
    Public isConnected As Boolean
    Public myConn As New MySqlConnection
	
	Public tableNames_NL() As String = {"Talen", "Residentie", "Menu", "Menu App", "Onderwijstype", "Levensbeschouwing", "Rol", "Burgelijke Staat", "Nationaliteit", "Oudertype"}
    Public tableNames_EN() As String = {"Languages", "Address", "Menu", "Menu App", "Education Type", "Religion", "Role", "Marital Status", "Nationality", "Parent Type"}
    Public tableNames_FR() As String = {"Langues", "Résidence", "Menu", "Menu App", "Type de l'Education", "Religion", "rôle", "État Civil", "Nationalité", "Type parent"}
    Public tableNames_SP() As String = {"Idiomas", "Résidence", "Menú", "Menú App", "Tipo de Educación", "religión", "Papel", "Estado civil", "Nacionalidad", "Tipo de padres"}
    Public tableNames_GE() As String = {"Sprachen", "Wohnsitz", "Menü", "Menü App", "Bildung Art", "Philosophie", "Rolle", "Familienstand", "Nationalität", "ältere Typ"}
    Public tableNames_CH() As String = {"語言", "住所", "菜單", "菜單應用程序", "教育類型", "哲學", "角色", "婚姻狀況", "國籍", "老型號"}

    Public Sub GetServerVars()
        'Change this. I HATE IT.
        ServerVars(0) = My.Resources.SERVER_VAR_0
        ServerVars(1) = My.Resources.SERVER_VAR_1
        ServerVars(2) = My.Resources.SERVER_VAR_2
        ServerVars(3) = My.Resources.SERVER_VAR_3
    End Sub
End Module

'Tags:
'0 + i = buttons on main (<10)
'1 + i = Controlbox Buttons (<20)
'   10 = Close
'   11 = Maximize
'   12 = Minimize
'   13 = Maximize 2
'   14 = Language Picker
