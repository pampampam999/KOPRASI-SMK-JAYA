Public Class deposit
    Private Sub deposit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampil()
    End Sub
    Sub tampil()
        konek()
        DA = New SqlClient.SqlDataAdapter("select kd_anggota,nm_anggota,deposito from anggota ", conn)
        DT = New DataTable
        DA.Fill(DT)
        dgv1.DataSource = DT
    End Sub

    Private Sub dgv1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv1.CellContentClick
        TextBox1.Text = dgv1.CurrentRow.Cells(0).Value.ToString
        TextBox2.Text = dgv1.CurrentRow.Cells(1).Value.ToString
        TextBox3.Text = dgv1.CurrentRow.Cells(2).Value.ToString
        If TextBox3.Text = "" Then
            TextBox3.Text = 0

        End If
    End Sub
    Sub jalankansql(ByVal sql As String)
        conn.Close()
        conn.Open()
        cmd = New SqlClient.SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.Text
        cmd.CommandText = sql
        cmd.ExecuteNonQuery()
        cmd.Dispose()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox3.Text = "" Then
            TextBox3.Text = 0

        End If
        Dim text3 As Integer = TextBox3.Text
        Dim text4 As Integer = TextBox4.Text
        Dim topup As Integer = text3 + text4
        'MsgBox(topup)
        Dim update As String = "update anggota set deposito=" & topup & " where kd_anggota='" & TextBox1.Text & "'"
        jalankansql(update)
        MsgBox("Deposito berhasil Di Tambah")
        tampil()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox3.Text = "" Then
            TextBox3.Text = 0

        End If
        Dim text3 As Integer = TextBox3.Text

        Dim update As String = "update anggota set deposito=" & text3 & " where kd_anggota='" & TextBox1.Text & "'"
        jalankansql(update)
        MsgBox("Deposito berhasil Di Tambah")
        tampil()
    End Sub
End Class