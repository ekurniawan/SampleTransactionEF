
Public Class TransaksiDAL
    Public Function InsertTransaksi(_tran As Transaksi, _db As SampleAPIDbEntities) As Integer
        Try
            _db.Transaksi.Add(_tran)
            _db.SaveChanges()

            Dim transId = _tran.TransaksiID
            Return transId
        Catch ex As Exception
            Throw New Exception($"Error:{ex.Message}")
        End Try
    End Function

    Public Function GetBalanceByUser(_nim As String, _db As SampleAPIDbEntities) As Decimal
        Dim result = (From b In _db.ViewBalance
                      Where b.Nim = _nim
                      Select b).SingleOrDefault()
        Dim balance As Decimal = 0
        If Not IsNothing(result) Then
            balance = result.Balance.Value
        Else
            Throw New Exception("Data tidak nim tidak ditemukan")
        End If
        Return balance
    End Function

    Public Sub UpdateBalance(_transId As Integer, _currBal As Decimal,
                             _db As SampleAPIDbEntities)
        Dim result = (From t In _db.Transaksi
                      Where t.TransaksiID = _transId
                      Select t).SingleOrDefault()

        'If Not IsNothing(result) Then
        '    result.CurrBalance = _currBal
        '    _db.SaveChanges()
        'End If
        Throw New Exception("Kesalahan")
    End Sub

    Public Sub InsertTransaksiBL(_trans As Transaksi)
        Using _db As New SampleAPIDbEntities
            Using scope = _db.Database.BeginTransaction()
                Try
                    Dim _transId = InsertTransaksi(_trans, _db)
                    Dim _currBal = GetBalanceByUser(_trans.Nim, _db)
                    UpdateBalance(_transId, _currBal, _db)

                    scope.Commit()
                Catch ex As Exception
                    Throw New Exception($"{ex.Message}")
                End Try
            End Using
        End Using
    End Sub

    Public Sub InsertTransaksiWithoutTS(_trans As Transaksi)
        Using _db As New SampleAPIDbEntities
            Try
                Dim _transId = InsertTransaksi(_trans, _db)
                Dim _currBal = GetBalanceByUser(_trans.Nim, _db)
                UpdateBalance(_transId, _currBal, _db)
            Catch ex As Exception
                Throw New Exception($"{ex.Message}")
            End Try
        End Using
    End Sub
End Class
