Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        konek()
        TextBox2.UseSystemPasswordChar = True
    End Sub
    Sub login()
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Username Dan Password Tidak Boleh Kosong")
            Exit Sub
        End If
        conn.Close()
        conn.Open()
        cmd = New SqlClient.SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from pengguna where nm_login='" & TextBox1.Text & "' and pass_login='" & TextBox2.Text & "'  "
        DR = cmd.ExecuteReader
        DR.Read()

        If DR.HasRows Then
            idpengguna = DR.Item("kd_pengguna")
            nmpengguna = DR.Item("nm_pengguna")
            lvl = DR.Item("lvl")
            TextBox1.Clear()
            TextBox2.Clear()
            If lvl = "super" Then
                mainform.Show()
            ElseIf lvl = "admin" Then
                mainform.Show()
            Else
                mainform.Show()
            End If
        Else
            MsgBox("Username atau Password Salah")
        End If
        TextBox1.Clear()
        TextBox2.Clear()
        Me.Hide()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            TextBox2.UseSystemPasswordChar = False
        Else
            TextBox2.UseSystemPasswordChar = True
        End If
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
            login()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        login()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Me.Close()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
End Class
