Imports MySql.Data
Imports MySql.Data.MySqlClient

Module DatabaseRetrieval
    Public Sub OpenConnection()
        myConn.ConnectionString = "server=" & ServerVars(0) & ";User id=" & ServerVars(1) & ";password=" & ServerVars(2) & ";database=" & ServerVars(3)
        'myConn.ConnectionString = "Server=" & ServerVars(0) & ";Database=" & ServerVars(3) & ";Uid=" & ServerVars(1) & ";Pwd=" & ServerVars(2)

        Try
            myConn.Open()
            isConnected = True
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        End Try
    End Sub

    Public Function RetrieveTableNames(ByVal table As String) As DataSet
        Dim TEMP_DataSet As New DataSet

        If isConnected Then
            Dim sqlStr As String = ""

            If table = "" Then
                sqlStr = "SELECT * FROM " & table
            Else
                sqlStr = "SELECT * FROM " & table
            End If

            Dim adp As MySqlDataAdapter = New MySqlDataAdapter(sqlStr, myConn)

            Try
                adp.Fill(TEMP_DataSet)
            Catch ex As MySqlException
                MsgBox("Error:" & ex.Message)
                LISA.Close()
            End Try

            adp.Dispose()
        End If

        Return TEMP_DataSet

        TEMP_DataSet.Dispose()
    End Function
End Module
