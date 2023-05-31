Public Class mainform
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        nav_user.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        nav_administrasi.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        transaksi_umum.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        transaksi_anggota.Show()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Me.Close()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub mainform_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        If lvl = "kasir" Then
            Button1.Enabled = True
            Button2.Enabled = False


        ElseIf lvl = "admin" Then
            Button3.Enabled = False
            Button4.Enabled = False
        Else

        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        deposit.Show()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        urutantransaksi.Show()
    End Sub
End Class