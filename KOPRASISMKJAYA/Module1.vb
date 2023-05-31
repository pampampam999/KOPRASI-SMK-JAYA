Imports System.Data.Sql
Imports System.Data.SqlClient
Module Module1
    Public conn As SqlConnection
    Public DA As SqlDataAdapter
    Public DS As DataSet
    Public DR As SqlDataReader
    Public DT As DataTable
    Public cmd As SqlCommand
    Public idpengguna As String
    Public nmpengguna As String
    Public lvl As String

    Sub konek()

        Try
            conn = New SqlConnection("Data Source=DESKTOP-1HMMK2Q\SQLEXPRESS;Initial Catalog=pos;Integrated Security=True")
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch ex As Exception
            MsgBox("Database Tidak Dapat Di Buka")
        End Try
    End Sub
End Module

