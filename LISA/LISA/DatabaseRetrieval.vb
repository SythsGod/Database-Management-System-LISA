Imports MySql.Data
Imports MySql.Data.MySqlClient

Module DatabaseRetrieval
    Private myConn As New MySqlConnection

    Public ReadOnly Property getConn() As MySqlConnection
        Get
            Return myConn
        End Get
    End Property
    Public Sub SetConnectionString()
        myConn.ConnectionString = "server=" & DB_IP & ";User id=" & DB_Username & ";password=" & DB_Password & ";database=" & DB_Database & ";"
    End Sub
    Public Sub OpenConnection()
        myConn.Close() 'Close any (possible) open connection

        Try
            myConn.Open() 'Try to open a valid connection to the database, throw an error otherwise
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & "Show this message to your computer wizard! He'll use some magic to fix this!") 'Show the user the error
        End Try
    End Sub

    Public Function RetrieveTableNames(ByVal table As String) As DataSet
        Dim TEMP_DataSet As New DataSet(table)
        Dim sqlStr As String = "SELECT * FROM " & table

        OpenConnection()

        Dim adp As MySqlDataAdapter = New MySqlDataAdapter(sqlStr, myConn)

        Try
            adp.FillSchema(TEMP_DataSet, SchemaType.Source, table) 'Add primary key to dataset
            adp.Fill(TEMP_DataSet, table)
        Catch ex As MySqlException
            If ex.ErrorCode.ToString = "-2147467259" Then
                MsgBox("Timed out. Retrying...")
                RetrieveTableNames(table)
            Else
                MsgBox("error: " & ex.Message & vbNewLine & "contact your computer wizard! let him use his magical powers for once!")
                MsgBox(ex.ErrorCode.ToString)
                Stop
            End If
        End Try

        adp.Dispose()
        myConn.Close()

        Return TEMP_DataSet

        'Dispose of variables no longer used
        TEMP_DataSet.Dispose()
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

        'Dispose of variables no longer used
        dataAdapter.Dispose()
        objCommandBuilder.Dispose()
    End Function
End Module