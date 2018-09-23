Public Class Form1

    Private _transDAL As TransaksiDAL

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _transDAL = New TransaksiDAL()
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            Dim newTrans = New Transaksi With {
                .Nim = txtNim.Text,
                .IdJenis = CInt(txtIDJenis.Text),
                .Debet = CDec(txtDebet.Text),
                .Kredit = CDec(txtKredit.Text)}


            _transDAL.InsertTransaksiBL(newTrans)
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


            _transDAL.InsertTransaksiWithoutTS(newTrans)
            MessageBox.Show($"Data berhasil ditambah", "Keterangan")
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}", "Keterangan")
        End Try
    End Sub

    Private Sub btnGet_Click(sender As Object, e As EventArgs) Handles btnGet.Click
        Dim results = _transDAL.GetDataTransaksiDetail().ToList()
        dgvData.DataSource = results
    End Sub

    Private Sub btnGetDataDirect_Click(sender As Object, e As EventArgs) Handles btnGetDataDirect.Click
        Dim _db As New SampleAPIDbEntities
        Dim results = (From tr In _db.Transaksi.Include("JenisTransaksi").Include("Mahasiswa").AsNoTracking()
                       Select New With {
                          .TransaksiID = tr.TransaksiID,
                          .IdJenis = tr.IdJenis,
                          .NamaJenis = tr.JenisTransaksi.NamaJenis,
                          .Nim = tr.Nim,
                          .Nama = tr.Mahasiswa.Nama,
                          .CurrBalance = tr.CurrBalance,
                          .Debet = tr.Debet,
                          .Kredit = tr.Kredit}).ToList()

        dgvData.DataSource = results
    End Sub
End Class
