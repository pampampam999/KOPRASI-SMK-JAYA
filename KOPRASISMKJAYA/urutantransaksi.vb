Public Class urutantransaksi
    Private Sub urutantransaksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampil()
    End Sub
    Sub tampil()
        konek()
        DA = New SqlClient.SqlDataAdapter("select * from view3 order by total desc", conn)
        DS = New DataSet
        DA.Fill(DS, "view3")
        DataGridView1.DataSource = DS.Tables("view3")
    End Sub
End Class