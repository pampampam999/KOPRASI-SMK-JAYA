Public Class ubahpassword
    Private Sub ubahpassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.UseSystemPasswordChar = True
        TextBox2.UseSystemPasswordChar = True
        TextBox3.UseSystemPasswordChar = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Sub ubah()
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Semua Password Tidak Boleh Kosong")
            Exit Sub
        End If
        conn.Close()
        conn.Open()
        cmd = New SqlClient.SqlCommand("select * from pengguna where kd_pengguna='" & idpengguna & "' and pass_login='" & TextBox1.Text & "' ", conn)
        DR = cmd.ExecuteReader
        DR.Read()
        cmd.Dispose()
        If DR.HasRows Then
            If TextBox2.Text = TextBox3.Text Then
                conn.Close()
                conn.Open()
                cmd = New SqlClient.SqlCommand
                cmd.Connection = conn
                cmd.CommandType = CommandType.Text
                cmd.CommandText = "update pengguna set pass_login='" & TextBox2.Text & "' where kd_pengguna='" & idpengguna & "' "
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                MsgBox("Password Berhasil Di Ubah")
            Else
                MsgBox("Password Baru Tidak Sama")
            End If

        Else
            MsgBox("Password Lama Anda Salah")
        End If

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ubah()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyValue = Keys.Enter Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyValue = Keys.Enter Then
            TextBox3.Focus()
        End If
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyValue = Keys.Enter Then
            ubah()
        End If
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Me.Close()
    End Sub
End Class