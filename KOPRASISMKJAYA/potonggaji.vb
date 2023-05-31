Public Class potonggaji
    Private Sub potonggaji_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampil()
    End Sub
    Sub tampil()
        konek()
        DA = New SqlClient.SqlDataAdapter("select kd_anggota,nm_anggota,gaji from anggota", conn)
        DS = New DataSet
        DA.Fill(DS, "anggota")
        dgv1.DataSource = DS.Tables("anggota")
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        konek()
        DA = New SqlClient.SqlDataAdapter("select kd_anggota,nm_anggota,gaji from anggota where nm_anggota like '%" & TextBox2.Text & "%' ", conn)
        DS = New DataSet
        DA.Fill(DS, "anggota")
        dgv1.DataSource = DS.Tables("anggota")
    End Sub
End Class