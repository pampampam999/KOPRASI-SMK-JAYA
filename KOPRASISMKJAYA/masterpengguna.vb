Public Class masterpengguna
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button1.Enabled = True
        Button4.Enabled = False
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("Data Tidak Boleh Kosong", vbInformation)
            Exit Sub
        End If
        conn.Close()
        conn.Open()
        cmd = New SqlClient.SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from pengguna where nm_login='" & TextBox3.Text & "' "
        DR = cmd.ExecuteReader
        If DR.HasRows Then
            MsgBox("Nama Login Telah Di Gunakan", vbInformation)
            Exit Sub
        End If

        Dim simpan As String = "insert into pengguna (kd_pengguna,nm_pengguna,nm_login,pass_login,lvl) values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & ComboBox1.Text & "')"
        jalankansql(simpan)
        MsgBox("Data Berhasil Di Simpan", vbInformation)
        tampil()

    End Sub
    Sub autokode()
        conn.Close()
        conn.Open()
        cmd = New SqlClient.SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select max(kd_pengguna) as autokode from pengguna"
        DR = cmd.ExecuteReader
        DR.Read()
        If Not DR.HasRows Then
            TextBox1.Text = "A001"
        Else
            Dim auto = Val(Microsoft.VisualBasic.Mid(DR.Item("autokode").ToString, 2, 5)) + 1
            TextBox1.Text = "A" & auto & ""
        End If
    End Sub

    Private Sub masterpengguna_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampil()
        autokode()
    End Sub
    Sub tampil()
        konek()
        DA = New SqlClient.SqlDataAdapter("select * from pengguna", conn)
        DS = New DataSet
        DA.Fill(DS, "pengguna")
        dgv1.DataSource = DS.Tables("pengguna")

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

    Private Sub dgv1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv1.CellContentClick
        TextBox1.Text = dgv1.CurrentRow.Cells(0).Value.ToString
        TextBox2.Text = dgv1.CurrentRow.Cells(1).Value.ToString
        TextBox3.Text = dgv1.CurrentRow.Cells(2).Value.ToString
        TextBox4.Text = dgv1.CurrentRow.Cells(3).Value.ToString
        ComboBox1.Text = dgv1.CurrentRow.Cells(4).Value.ToString
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("Data Tidak Boleh Kosong", vbInformation)
            Exit Sub
        End If
        conn.Close()
        conn.Open()
        cmd = New SqlClient.SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * pengguna where nm_login='" & TextBox3.Text & "' "
        DR = cmd.ExecuteReader
        If DR.HasRows Then
            MsgBox("Nama Login Telah Di Gunakan", vbInformation)
            Exit Sub
        End If
        Dim ubah As String = "update pengguna set nm_pengguna='" & TextBox2.Text & "',nm_login='" & TextBox3.Text & "',pass_login='" & TextBox4.Text & "',lvl='" & ComboBox1.Text & "' where kd_pengguna='" & TextBox1.Text & "' "
        jalankansql(ubah)
        MsgBox("Data Berhasil Di Ubah", vbInformation)
        tampil()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        MsgBox("Apakah Anda Yakin Ingin Menghapus ?", vbExclamation + vbYesNo)
        If vbYes Then
            Dim hapus As String = "delete from pengguna where kd_pengguna='" & TextBox1.Text & "' "
            jalankansql(hapus)
            MsgBox("Data Telah Di Hapus", vbInformation)
            tampil()
        Else
            Exit Sub
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Button1.Enabled = True
        Button4.Enabled = False
        autokode()

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Me.Close()
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
End Class