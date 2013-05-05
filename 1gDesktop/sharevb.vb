Public Class sharevb
    Dim share As Info
    Sub New(shareinfo As Info)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        share = shareinfo
        shareName.Text = share.songName + " - " + share.songSinger
        TextBox1.Text = "http://www.1g1g.com/" + share.songSinger + "-" + share.songName
        TextBox2.Text = "♫ http://1g1g.com/#" + share.SongID.ToString
    End Sub

    Private Sub sina_Click(sender As System.Object, e As System.EventArgs) Handles sina.Click
        Process.Start("http://v.t.sina.com.cn/share/share.php?title=♫" + share.songName + "-" + share.songSinger + "%20%20%23亦歌%23" + "&url=http%3A%2F%2F1g1g%2Ecom%2Fs%2F" + share.SongID.ToString + "&appkey=365943804&sourceUrl=&content=utf-8&source=")
    End Sub

    Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
        Process.Start("http://v.t.qq.com/share/share.php?title=♫" + share.songName + "-" + share.songSinger + "%20%23亦歌%23" + "&url=http%3A%2F%2F1g1g%2Ecom%2Fs%2F" + share.SongID.ToString + "&appkey=1d3fd30ff1ec44ee9d6c0e14442fb1d9&pic=&site=http%3A%2F%2Fwww%2E1g1g%2Ecom%2F")
    End Sub

    Private Sub PictureBox2_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox2.Click
        Process.Start("http://twitter.com/intent/tweet?text=♫" + share.songName + "-" + share.songSinger + "%20%23亦歌%23" + "&url=http%3A%2F%2F1g1g.com%2Fs%2F" + share.SongID.ToString)
    End Sub
End Class