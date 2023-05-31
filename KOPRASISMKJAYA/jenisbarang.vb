Public Class jenisbarang
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Button1.Enabled = True
        Button4.Enabled = False
        autokode()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button1.Enabled = False
        Button4.Enabled = True
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Data Tidak Boleh Kosong")
            Exit Sub
        End If
        Dim simpan As String = "insert into jenis_barang values ('" & TextBox1.Text & "','" & TextBox2.Text & "')"
        jalankansql(simpan)
        tampil()
        MsgBox("Data Telah Di Simpan", vbInformation)
    End Sub

    Private Sub jenisbarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampil()
        autokode()
    End Sub
    Sub tampil()
        konek()
        DA = New SqlClient.SqlDataAdapter("select * from jenis_barang", conn)
        DS = New DataSet
        DA.Fill(DS, "jenis_barang")
        dgv1.DataSource = DS.Tables("jenis_barang")

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

    Sub autokode()
        conn.Close()
        conn.Open()
        cmd = New SqlClient.SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select max(kd_jenis_brg) as autokode from jenis_barang"
        DR = cmd.ExecuteReader
        DR.Read()
        If Not DR.HasRows Then
            TextBox1.Text = "JB001"
        Else
            Dim auto = Val(Microsoft.VisualBasic.Mid(DR.Item("autokode").ToString, 3, 6)) + 1
            TextBox1.Text = "JB" & auto & ""
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        MsgBox("Apakah Anda Yakin Ingin Menghapus ?", vbExclamation + vbYesNo)
        If vbYes Then
            Dim hapus As String = "delete from jenis_barang where kd_jenis_brg='" & TextBox1.Text & "' "
            jalankansql(hapus)
            MsgBox("Data Telah Di Hapus", vbInformation)
            tampil()
        Else
            Exit Sub
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Button1.Enabled = False
        Button4.Enabled = True
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Data Tidak Boleh Kosong")
            Exit Sub
        End If
        Dim ubah As String = "update jenis_barang set jenis_brg='" & TextBox2.Text & "' where kd_jenis_brg='" & TextBox1.Text & "' "
        jalankansql(ubah)
        tampil()
        MsgBox("Data Berhasil Di Update", vbInformation)
    End Sub

    Private Sub dgv1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv1.CellContentClick
        TextBox1.Text = dgv1.CurrentRow.Cells(0).Value.ToString
        TextBox2.Text = dgv1.CurrentRow.Cells(1).Value.ToString
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        konek()
        DA = New SqlClient.SqlDataAdapter("select * from unitkerja where unit_kerja like '%" & TextBox3.Text & "%' ", conn)
        DS = New DataSet
        DA.Fill(DS, "unitkerja")
        dgv1.DataSource = DS.Tables("unitkerja")
    End Sub
End Class