Imports System.Drawing.Text
Imports MySql.Data.MySqlClient

Module Globals
    Public LangSwitch As Boolean = False 'If false lang="Dutch" else lang="English"
    Public ServerVars(3) As String
    Public TableNamesInDatabase As New DataTable
    Public AllTableInformation As New Dictionary(Of Integer, DataSet)
    Public isConnected As Boolean
    Public myConn As New MySqlConnection

    Public Sub GetServerVars()
        'Change this. I HATE IT.
        ServerVars(0) = My.Resources.SERVER_VAR_0
        ServerVars(1) = My.Resources.SERVER_VAR_1
        ServerVars(2) = My.Resources.SERVER_VAR_2
        ServerVars(3) = My.Resources.SERVER_VAR_3
        'ServerVars(0) = "db4free.net"
        'ServerVars(1) = "SythsGod"
        'ServerVars(2) = "Sythsgod0911"
        'ServerVars(3) = "modlist"
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
