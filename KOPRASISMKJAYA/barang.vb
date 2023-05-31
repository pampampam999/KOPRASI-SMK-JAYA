Public Class barang
    Private Sub barang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampil()
        autokode()
    End Sub
    Sub autokode()
        conn.Close()
        conn.Open()
        cmd = New SqlClient.SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select max(kd_barang) as autokode from barang"
        DR = cmd.ExecuteReader
        DR.Read()
        If Not DR.HasRows Then
            TextBox2.Text = "KB1"
        Else
            Dim auto = Val(Microsoft.VisualBasic.Mid(DR.Item("autokode").ToString, 3, 5)) + 1
            TextBox2.Text = "KB" & auto & ""
        End If
    End Sub
    Sub tampil()
        konek()
        DA = New SqlClient.SqlDataAdapter("select * from barang", conn)
        DS = New DataSet
        DA.Fill(DS, "barang")
        dgv1.DataSource = DS.Tables("barang")

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

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        konek()
        DA = New SqlClient.SqlDataAdapter("select * from barang where nm_barang like '%" & TextBox1.Text & "%' ", conn)
        DS = New DataSet
        DA.Fill(DS, "barang")
        dgv1.DataSource = DS.Tables("barang")
    End Sub

    Private Sub dgv1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv1.CellContentClick
        TextBox2.Text = dgv1.CurrentRow.Cells(0).Value.ToString
        TextBox3.Text = dgv1.CurrentRow.Cells(2).Value.ToString
        TextBox4.Text = dgv1.CurrentRow.Cells(3).Value.ToString
        TextBox5.Text = dgv1.CurrentRow.Cells(4).Value.ToString
        TextBox6.Text = dgv1.CurrentRow.Cells(1).Value.ToString
        TextBox7.Text = dgv1.CurrentRow.Cells(5).Value.ToString
        TextBox8.Text = dgv1.CurrentRow.Cells(6).Value.ToString

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
    End Sub
    Sub clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox8.Clear()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Button1.Enabled = True
        Button4.Enabled = False

        clear()
        autokode()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button1.Enabled = False
        Button4.Enabled = True
        If TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Or TextBox8.Text = "" Then
            MsgBox("Data Tidak Boleh Kosong")
            Exit Sub
        End If
        Dim simpan As String = " insert into barang values ('" & TextBox2.Text & "','" & TextBox6.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "','" & TextBox7.Text & "','" & TextBox8.Text & "') "
        jalankansql(simpan)
        tampil()
        MsgBox("Data Telah Di Simpan")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        MsgBox("Apakah Anda Yakin Ingin Menghapus", vbYesNo)
        If vbYes Then
            Dim hapus As String = "delete from barang where kd_barang='" & TextBox2.Text & "' "
            jalankansql(hapus)
            MsgBox("Data Telah Di Hapus", vbInformation)
            tampil()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim ubah As String = "update barang set nm_barang='" & TextBox3.Text & "',hrg_beli='" & TextBox4.Text & "',hrg_jual='" & TextBox5.Text & "',kd_jenis_barang='" & TextBox6.Text & "',stok=" & TextBox7.Text & ",keterangan='" & TextBox8.Text & "' where kd_barang='" & TextBox2.Text & "' "
        jalankansql(ubah)
        tampil()
        MsgBox("Data Berhasil Di Ubah")
    End Sub
End Class