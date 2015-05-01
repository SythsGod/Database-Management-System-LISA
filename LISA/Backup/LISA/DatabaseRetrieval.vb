Imports MySql.Data
Imports MySql.Data.MySqlClient

Module DatabaseRetrieval
    Public Sub OpenConnection()
        myConn.Close()
        myConn.ConnectionString = "server=" & ServerVars(0) & ";User id=" & ServerVars(1) & ";password=" & ServerVars(2) & ";database=" & ServerVars(3)
        'myConn.ConnectionString = "Server=" & ServerVars(0) & ";Database=" & ServerVars(3) & ";Uid=" & ServerVars(1) & ";Pwd=" & ServerVars(2)

        Try
            myConn.Open()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Function RetrieveTableNames(ByVal table As String) As DataSet
        Dim TEMP_DataSet As New DataSet(table)
        Dim sqlStr As String = ""

        If table = "" Then
            sqlStr = "SELECT * FROM " & table
        Else
            sqlStr = "SELECT * FROM " & table
        End If

        OpenConnection()

        Dim adp As MySqlDataAdapter = New MySqlDataAdapter(sqlStr, myConn)

        Try
            adp.FillSchema(TEMP_DataSet, SchemaType.Source, table) 'Add primary key to dataset
            adp.Fill(TEMP_DataSet, table)
        Catch ex As MySqlException
            MsgBox("Error:" & ex.Message)
            LISA.Close()
        End Try

        adp.Dispose()

        Return TEMP_DataSet

        TEMP_DataSet.Dispose()
        myConn.Close()
    End Function

    Public Function Upload(ByVal data As DataSet, ByVal table As String) As Boolean
        OpenConnection()

        Dim dataAdapter As New MySqlDataAdapter("SELECT * FROM " & table, myConn)
        dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey
        Dim objCommandBuilder As New MySqlCommandBuilder(dataAdapter)

        Try
            dataAdapter.Update(data, table)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        dataAdapter.Dispose()
        objCommandBuilder.Dispose()
    End Function
End Module