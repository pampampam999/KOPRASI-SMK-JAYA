Public Class masteranggota
    Dim gambar As String
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Button1.Enabled = True
        Button4.Enabled = False
        autokode()
    End Sub
    Sub autokode()
        conn.Close()
        conn.Open()
        cmd = New SqlClient.SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select max(kd_anggota) as autokode from anggota"
        DR = cmd.ExecuteReader
        DR.Read()
        If Not DR.HasRows Then
            TextBox2.Text = "KA001"
        Else
            Dim auto = Val(Microsoft.VisualBasic.Mid(DR.Item("autokode").ToString, 3, 5)) + 1
            TextBox2.Text = "KA" & auto & ""
        End If
    End Sub


    Private Sub masteranggota_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampil()
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy-MM-dd"
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = "yyyy-MM-dd"
    End Sub
    Sub tampil()
        konek()
        DA = New SqlClient.SqlDataAdapter("select * from anggota", conn)
        DS = New DataSet
        DA.Fill(DS, "anggota")
        dgv1.DataSource = DS.Tables("anggota")

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
        Button1.Enabled = False
        Button4.Enabled = True
        If TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("Data Tidak Boleh Kosong")
            Exit Sub
        End If
        Dim simpan As String = "insert into anggota values ('" & TextBox2.Text & "','" & TextBox3.Text & "'," & TextBox4.Text & ",'" & TextBox5.Text & "','" & TextBox6.Text & "','" & DateTimePicker1.Text & "','" & ComboBox1.Text & "','" & TextBox7.Text & "','" & Today.ToString("yyyy-MM-dd") & "','" & Label11.Text & "',0,0) "
        jalankansql(simpan)
        tampil()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim ubah As String = "update anggota set kd_unit_kerja='" & TextBox3.Text & "',npp='" & TextBox4.Text & "',nm_anggota='" & TextBox5.Text & "',tmp_lahir='" & TextBox6.Text & "',tgl_lahir='" & DateTimePicker1.Text & "',jenis_kelamin='" & ComboBox1.Text & "',alamat='" & TextBox7.Text & "',tgl_jadi_anggota='" & DateTimePicker2.Text & "',pic='" & Label11.Text & "' where kd_anggota='" & TextBox2.Text & "' "
        jalankansql(ubah)
        tampil()
        MsgBox("Data Berhasil Di Ubah")
    End Sub

    Private Sub dgv1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv1.CellContentClick
        TextBox2.Text = dgv1.CurrentRow.Cells(0).Value.ToString
        TextBox3.Text = dgv1.CurrentRow.Cells(1).Value.ToString
        TextBox4.Text = dgv1.CurrentRow.Cells(2).Value.ToString
        TextBox5.Text = dgv1.CurrentRow.Cells(3).Value.ToString
        TextBox6.Text = dgv1.CurrentRow.Cells(4).Value.ToString
        DateTimePicker1.Text = dgv1.CurrentRow.Cells(5).Value.ToString
        ComboBox1.Text = dgv1.CurrentRow.Cells(6).Value.ToString
        TextBox7.Text = dgv1.CurrentRow.Cells(7).Value.ToString
        DateTimePicker2.Text = dgv1.CurrentRow.Cells(8).Value.ToString
        PictureBox1.ImageLocation = dgv1.CurrentRow.Cells(9).Value.ToString
        gambar = dgv1.CurrentRow.Cells(9).Value.ToString
        Label11.Text = gambar
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        MsgBox("Apakah Anda Yakin Ingin Menghapus", vbYesNo)
        If vbYes Then
            Dim hapus As String = "delete from anggota where kd_anggota='" & TextBox2.Text & "' "
            jalankansql(hapus)
            MsgBox("Data Telah Di Hapus", vbInformation)
            tampil()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        konek()
        DA = New SqlClient.SqlDataAdapter("select * from anggota where nm_anggota like '%" & TextBox1.Text & "%' ", conn)
        DS = New DataSet
        DA.Fill(DS, "anggota")
        dgv1.DataSource = DS.Tables("anggota")

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        OpenFileDialog1.Filter = "GAMBAR|*.img;*.jpg;*.jpeg"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            gambar = OpenFileDialog1.FileName
            PictureBox1.ImageLocation = gambar
            Label11.Text = gambar
        End If
    End Sub
End Class