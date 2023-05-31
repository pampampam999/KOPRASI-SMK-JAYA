Public Class nav_user
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ubahpassword.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        masterpengguna.Show()
    End Sub

    Private Sub nav_user_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If lvl = "kasir" Then
            Button2.Enabled = False
        ElseIf lvl = "admin" Then
            Button2.Enabled = False
        End If
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Me.Close()
    End Sub
End Class