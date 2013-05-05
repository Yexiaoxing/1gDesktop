Imports _1gDesktop.ColorSet
Public Class LRC
    Dim X, Y As Integer
    Private BP As Bitmap
    Dim FT As Font = New Font("微软雅黑", 40, FontStyle.Regular, GraphicsUnit.Pixel)
    Private SecondStringBP As Bitmap

    Dim MenberCenterLocation As Integer
    ''' <summary>
    ''' 显示歌词
    ''' </summary>
    ''' <param name="MusicText">歌曲语句</param>
    ''' <param name="_process">进度百分比</param>
    ''' <remarks></remarks>
    Public Sub ShowLrc(ByVal MusicText As String, ByVal _process As Double, ByVal b As ProcessColor)
        On Error Resume Next

        MenberCenterLocation = Me.Left + Me.Width / 2
        Dim TBP As New Bitmap(Me.Width, Me.Height)
        Dim gs As Graphics = Graphics.FromImage(TBP)
        Dim TextWH = gs.MeasureString(MusicText, Font)
        TBP.Dispose()
        gs.Dispose()
        Dim Wid As Integer = TextWH.Width
        Dim Hei As Integer = TextWH.Height
        TextWH = Nothing
        BP.Dispose()
        BP = New Bitmap(Wid, Hei) 'Me.Width, Me.Height)
        Me.Size = New Size(Wid, Hei)

        Using G As Graphics = Graphics.FromImage(BP)
            G.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            G.CompositingMode = Drawing2D.CompositingMode.SourceOver
            G.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit
            X = 0 : Y = 0

            For J As Integer = 1 To 5
                Using lg As New Drawing2D.LinearGradientBrush(New Point(0, 0), New Point(0, 1), Color.FromArgb(90 - 90 / 5 * J, 0, 0, 0), Color.FromArgb(100 - J * 20, 0, 0, 0))
                    G.DrawString(MusicText, FT, lg, X + J, Y + J)
                End Using
            Next
            For I As Integer = 1 To 3
                Using lg As New Drawing2D.LinearGradientBrush(New Point(0, 0), New Point(0, 1), Color.FromArgb(90 - 90 / 3 * I, 0, 0, 0), Color.FromArgb(90 - 90 / 3 * I, 0, 0, 0))
                    G.DrawString(MusicText, FT, lg, X - I, Y)
                    G.DrawString(MusicText, FT, lg, X - I, Y - I)
                    G.DrawString(MusicText, FT, lg, X, Y - I)
                    G.DrawString(MusicText, FT, lg, X + I, Y - I)
                    G.DrawString(MusicText, FT, lg, X + I, Y)
                    G.DrawString(MusicText, FT, lg, X + I, Y + I)
                    G.DrawString(MusicText, FT, lg, X, Y + I)
                    G.DrawString(MusicText, FT, lg, X - I, Y + I)
                End Using
            Next
            Using lg As New Drawing2D.LinearGradientBrush(New Point(X, Y), New Point(X, Y + FT.Height), Color.YellowGreen, Color.DarkGreen)
                G.DrawString(MusicText, FT, lg, X, Y)
            End Using
            'G.DrawImage(GetStringImage(MusicText), New Rectangle(0, 0, Me.Width, Me.Height), New Rectangle(0, 0, Me.Width, Me.Height), GraphicsUnit.Pixel) '* s
            G.FillRectangle(b.bg, New Rectangle(Me.Width / 2 - 100, Me.Height - 10, 200, 10))
            G.FillRectangle(b.top, New Rectangle(Me.Width / 2 - 100, Me.Height - 10, 200 * _process, 10))
        End Using
        Me.BackgroundImage = BP
        DrawBP(Me, BP, 255)
        SetLo()
    End Sub
    'Private Function GetStringImage(ByVal s As String) As Bitmap
    '    If SecondStringBP IsNot Nothing Then SecondStringBP.Dispose()
    '    SecondStringBP = New Bitmap(Me.Width, Me.Height)
    '    Using G As Graphics = Graphics.FromImage(SecondStringBP)
    '        G.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
    '        G.CompositingMode = Drawing2D.CompositingMode.SourceOver
    '        G.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit
    '        Using lg As New Drawing2D.LinearGradientBrush(New Point(X, Y), New Point(X, Y + FT.Height), Color.LightYellow, Color.Red)
    '            G.DrawString(s, FT, lg, X, Y)
    '        End Using
    '    End Using
    '    Return SecondStringBP
    'End Function

    Private Sub Form1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        ReleaseCapture()
        SendMessage(sender.Handle.ToInt32(), WM_SysCommand, SC_MOVE, 0)
    End Sub
    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H80000
            Return cp
        End Get
    End Property

    Private Sub LRC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Font = FT
        Dim p As New ColorSet
        ShowLrc("欢迎使用亦歌", 0, p.green)
        Me.Location = New Point((My.Computer.Screen.WorkingArea.Width - Me.Width) / 2, My.Computer.Screen.WorkingArea.Height - Me.Height)
    End Sub

    Sub SetLo()
        Dim xs As Integer
        xs = MenberCenterLocation - Me.Width / 2
        Me.Location = New Point(xs, Me.Top)
    End Sub

    Private Sub LRC_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Move
        MenberCenterLocation = Me.Left + Me.Width / 2
    End Sub

    Private Sub LRC_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        MenberCenterLocation = Me.Left + Me.Width / 2
    End Sub
End Class

