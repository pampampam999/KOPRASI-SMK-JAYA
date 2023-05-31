Public Class prosesanggota
    Dim kdanggota As String
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub prosesanggota_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampil()

    End Sub
    Sub tampil()
        konek()
        DA = New SqlClient.SqlDataAdapter("select kd_anggota,nm_anggota from anggota", conn)
        DS = New DataSet
        DA.Fill(DS, "anggota")
        dgv1.DataSource = DS.Tables("anggota")
    End Sub

    Private Sub dgv1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv1.CellContentClick

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        konek()
        DA = New SqlClient.SqlDataAdapter("select kd_anggota,nm_anggota from anggota where nm_anggota like '%" & TextBox2.Text & "%' ", conn)
        DS = New DataSet
        DA.Fill(DS, "anggota")
        dgv1.DataSource = DS.Tables("anggota")
    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyValue = Keys.Enter Then
            dgv1.Focus()
        End If
    End Sub

    Private Sub dgv1_KeyDown(sender As Object, e As KeyEventArgs) Handles dgv1.KeyDown
        If e.KeyValue = Keys.Enter Then

            kdanggota = dgv1.CurrentRow.Cells(0).Value.ToString
            TextBox2.Text = dgv1.CurrentRow.Cells(1).Value.ToString
        End If
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        If TextBox4.Text = "" Then
            TextBox4.Text = 0

        End If
        TextBox5.Text = TextBox4.Text - TextBox3.Text
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
        Dim text3 As Integer = TextBox3.Text
        Dim text4 As Integer = TextBox4.Text

        Dim simpan As String = "insert into penjualan_anggota  values ('" & TextBox1.Text & "','" & kdanggota & "','" & Today.ToString("yyyy-MM-dd") & "','" & TextBox3.Text & "')"
        jalankansql(simpan)
        Dim update As String = "update penjualan_rinci_anggota set status=1,kd_penjualan_anggota='" & kdanggota & "' where status=0"
        jalankansql(update)
        tampil()

        MsgBox("Data berhasil di simpan")
        transaksi_anggota.tampil()
        transaksi_anggota.autokode()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
    End Sub
End Class