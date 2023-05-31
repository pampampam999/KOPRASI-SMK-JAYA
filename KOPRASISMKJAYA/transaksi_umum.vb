Public Class transaksi_umum
    Dim kdrinci As String
    Dim subtotal As Integer
    Dim total As Integer
    Dim stok As Integer
    Dim stokterbeli As Integer

    Dim stokakhir As Integer


    Private Sub transaksi_umum_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampil()
        TextBox8.Text = nmpengguna
        TextBox7.Text = Today.ToString("yyyy-MM-dd")
        autokode()
        labeltotal()
        If TextBox10.Text = "" Then
            TextBox10.Text = 0

        End If
        If TextBox9.Text = "" Then
            TextBox9.Text = 0

        End If
        TextBox10.Text = FormatCurrency(TextBox10.Text, 0)
        Label1.Text = FormatCurrency(Label1.Text, 0)

    End Sub
    Sub ambilstok()
        cmd = New SqlClient.SqlCommand("select stok from barang where kd_barang='" & TextBox1.Text & "' ", conn)
        stok = cmd.ExecuteScalar
        ' MsgBox(stok)
    End Sub
    Sub labeltotal()
        konek()
        cmd = New SqlClient.SqlCommand("select SUM(sub_total_umum) as total from penjualan_rinci_umum where status=0", conn)
        Dim DR = cmd.ExecuteScalar
        If DR.ToString = "" Then
            DR = 0
        End If
        total = DR
        Label1.Text = DR
    End Sub
    Sub autokode()
        conn.Close()
        conn.Open()
        cmd = New SqlClient.SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select max(kd_penjualan_umum) as autokode from penjualan_umum"
        DR = cmd.ExecuteReader
        DR.Read()
        If Not DR.HasRows Then
            TextBox5.Text = "N001"
        Else
            Dim auto = Val(Microsoft.VisualBasic.Mid(DR.Item("autokode").ToString, 2, 5)) + 1
            TextBox5.Text = "N" & auto & ""
        End If
    End Sub

    Sub tampil()
        konek()
        DA = New SqlClient.SqlDataAdapter("select * from view1 where status=0", conn)
        DS = New DataSet
        DA.Fill(DS, "view1")
        dgv1.DataSource = DS.Tables("view1")
        Me.dgv1.Columns(0).Visible = False
        Me.dgv1.Columns(1).Visible = False
        Me.dgv1.Columns(4).Visible = False
        Me.dgv1.Columns(8).Visible = False


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
        ambilstok()
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
            MsgBox("Data tidak boleh kosong")
            Exit Sub
        End If
        If TextBox4.Text > stok Then
            MsgBox("Barang Yang Di Minta Tidak Cukup")
            Exit Sub
        End If
        stokakhir = stok - TextBox4.Text

        subtotal = TextBox3.Text * TextBox4.Text

        Dim simpan As String = "insert into penjualan_rinci_umum (kd_barang,kd_pengguna,hrg_umum,jml_brg_umum,sub_total_umum) values ('" & TextBox1.Text & "','" & TextBox8.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & subtotal & "')"
        jalankansql(simpan)
        Dim update As String = "update barang set stok='" & stokakhir & "' where kd_barang='" & TextBox1.Text & "' "
        jalankansql(update)
        tampil()
        MsgBox("Data berhasil di simpan")
        labeltotal()
        Label1.Text = FormatCurrency(Label1.Text, 0)
        TextBox10.Text = TextBox9.Text - total
        bersih()
        TextBox2.Focus()
    End Sub
    Sub bersih()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        bersih()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ambilstok()
        stokakhir = stok + TextBox4.Text
        'MsgBox(stokakhir)
        Dim hapus As String = "delete from penjualan_rinci_umum where kd_rinci_umum='" & kdrinci & "' "
        jalankansql(hapus)
        Dim update As String = "update barang set stok='" & stokakhir & "' where kd_barang='" & TextBox1.Text & "' "
        jalankansql(update)
        tampil()
        MsgBox("Data Telah Di Hapus")
        autokode()
        Label1.Text = FormatCurrency(Label1.Text, 0)
        TextBox10.Text = TextBox9.Text - total
        labeltotal()
    End Sub

    Private Sub dgv1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv1.CellContentClick
        kdrinci = dgv1.CurrentRow.Cells(1).Value.ToString
        TextBox1.Text = dgv1.CurrentRow.Cells(2).Value.ToString
        TextBox2.Text = dgv1.CurrentRow.Cells(3).Value.ToString
        TextBox3.Text = dgv1.CurrentRow.Cells(5).Value.ToString
        TextBox4.Text = dgv1.CurrentRow.Cells(6).Value.ToString

    End Sub
    Sub tampil1(ByVal sql As String)
        konek()
        conn.Close()
        conn.Open()
        DA = New SqlClient.SqlDataAdapter("select * from barang where nm_barang like '%" & sql & "%' ", conn)
        DT = New DataTable
        DA.Fill(DT)
        dgv2.DataSource = DT
        If DT.Rows.Count > 0 Then
            dgv2.Visible = True
        Else
            dgv2.Visible = False
        End If

    End Sub
    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        tampil1(TextBox2.Text)
    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyValue = Keys.Enter Then
            dgv2.Focus()
        End If
    End Sub

    Private Sub dgv2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv2.CellContentClick

    End Sub

    Private Sub dgv2_KeyDown(sender As Object, e As KeyEventArgs) Handles dgv2.KeyDown
        If e.KeyValue = Keys.Enter Then
            TextBox1.Text = dgv2.CurrentRow.Cells("kd_barang").Value.ToString
            TextBox2.Text = dgv2.CurrentRow.Cells("nm_barang").Value.ToString
            TextBox3.Text = dgv2.CurrentRow.Cells("hrg_jual").Value.ToString
            TextBox4.Focus()
        End If
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub TextBox4_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyValue = Keys.Enter Then
            Button1.Focus()
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
    End Sub

    Private Sub dgv2_Leave(sender As Object, e As EventArgs) Handles dgv2.Leave
        dgv2.Visible = False
    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        If TextBox9.Text = "" Then
            TextBox9.Text = 0

        End If
        TextBox10.Text = TextBox9.Text - total
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub TextBox9_GotFocus(sender As Object, e As EventArgs) Handles TextBox9.GotFocus
        TextBox9.Text = FormatNumber(TextBox9.Text, 0)
    End Sub

    Private Sub TextBox9_Leave(sender As Object, e As EventArgs) Handles TextBox9.Leave
        TextBox9.Text = FormatCurrency(TextBox9.Text, 0)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim text9 As Integer = TextBox9.Text
        Dim text10 As Integer = TextBox10.Text
        TextBox9.Text = FormatNumber(TextBox9.Text, 0)

        Dim simpan As String = "insert into penjualan_umum values ('" & TextBox5.Text & "','" & Today.ToString("yyyy-MM-dd") & "','" & total & "')"
        jalankansql(simpan)
        Dim update As String = "update penjualan_rinci_umum set status=1,kd_penjualan_umum='" & TextBox5.Text & "' where status=0"
        jalankansql(update)
        tampil()
        autokode()
        MsgBox("Data berhasil di simpan")
        TextBox10.Text = TextBox9.Text - total
        labeltotal()
        TextBox2.Focus()
    End Sub

    Private Sub GroupBox3_Enter(sender As Object, e As EventArgs) Handles GroupBox3.Enter

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyValue = Keys.Enter Then
            TextBox2.Focus()
        End If
    End Sub
End Class