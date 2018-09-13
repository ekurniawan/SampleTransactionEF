Public Class Form1
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            Dim newTrans = New Transaksi With {
                .Nim = txtNim.Text,
                .IdJenis = CInt(txtIDJenis.Text),
                .Debet = CDec(txtDebet.Text),
                .Kredit = CDec(txtKredit.Text)}

            Dim _transDal As New TransaksiDAL()
            _transDal.InsertTransaksiBL(newTrans)
            MessageBox.Show($"Data berhasil ditambah", "Keterangan")
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}", "Keterangan")
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim newTrans = New Transaksi With {
                .Nim = txtNim.Text,
                .IdJenis = CInt(txtIDJenis.Text),
                .Debet = CDec(txtDebet.Text),
                .Kredit = CDec(txtKredit.Text)}

            Dim _transDal As New TransaksiDAL()
            _transDal.InsertTransaksiWithoutTS(newTrans)
            MessageBox.Show($"Data berhasil ditambah", "Keterangan")
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}", "Keterangan")
        End Try
    End Sub
End Class
