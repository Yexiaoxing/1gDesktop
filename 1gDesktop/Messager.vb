Imports System.Threading

Public Class Messager

    Sub ChangeLabel(ByVal message As String)
        Label1.Text = message
        Dim TBP As New Bitmap(Me.Width, Me.Height)
        Dim gs As Graphics = Graphics.FromImage(TBP)
        Dim TextWH = gs.MeasureString(Label1.Text, Font)
        TBP.Dispose()
        gs.Dispose()
        Me.Size = New Size(TextWH.Width + 10, TextWH.Height + 10)

        Dim p As New Thread(Sub()
                                RoundFormPainter.Paint(Me)
                            End Sub)
        p.Start()
        Me.Show()
        Me.Location = New Size(My.Computer.Screen.WorkingArea.Width / 2 - Me.Width / 2, My.Computer.Screen.WorkingArea.Height - 160 - 80)
        Timer1.Stop()
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.Hide()
    End Sub
End Class