Public Class songItem
    Dim _Name As String
    Dim Singer As String
    Dim ID As String
    Sub New(sName, sSinger, sID)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        _Name = sName
        Singer = sSinger
        ID = sID
        songName.Text = sName
        songSinger.Text = sSinger
    End Sub

    Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
        Form1.Command("play", "#" + ID)
    End Sub
End Class
