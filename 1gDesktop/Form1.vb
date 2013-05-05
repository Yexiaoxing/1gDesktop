Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Threading
Imports System.Windows.Forms
Imports System.Xml

Public Class Form1
#Region "变量"
    Public song As Info
    Dim LRCShower As New LRC
    Friend mbox As New Messager

    Public NowBrushes As New ProcessColor With {.bg = Brushes.Black, .top = Brushes.Green}

    'Const GWL_EXSTYLE = (-20)
    'Const WS_EX_LAYERED = &H80000
    'Const WS_EX_TRANSPARENT As Long = &H20&
    'Private Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hwnd As Long, ByVal nIndex As Long) As Long
    'Private Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Long, ByVal nIndex As Long, ByVal dwNewLong As Long) As Long
#End Region

#Region "窗体过程"
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckForIllegalCrossThreadCalls = False
        LRCShower.Show()
        Flash.Movie = "http://www.1g1g.com/player/loader.swf"
    End Sub

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize, Me.Shown
        Dim p As New Thread(Sub()
                                RoundFormPainter.Paint(Me)
                            End Sub)
        p.Start()
    End Sub

    Private Sub Label1_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label1.SizeChanged
        TableLayoutPanel1.Height = Label1.Height
    End Sub
#End Region

#Region "窗体拖动"
    Private Sub windowDraging(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Label1.MouseDown, pb_Play.MouseDown, pb_Next.MouseDown, pb_Fav.MouseDown, Me.MouseDown, pb_Share.MouseDown, pb_Search.MouseDown
        ReleaseCapture()
        SendMessage(Handle.ToInt32, WM_SysCommand, SC_MOVE, 0)
    End Sub
#End Region

#Region "按钮事件"
    Private Sub NextSong() Handles pb_Next.MouseDown
        Command(ControlType._next)
    End Sub

    Private Sub FavSong() Handles pb_Fav.MouseDown
        Command(ControlType.fav)
    End Sub

    Private Sub PlayPauseSong() Handles pb_Play.MouseDown
        Command(ControlType.playPause)
    End Sub

    Private Sub Search() Handles pb_Search.MouseDown
        Dim sbox = New SearchBox()
        sbox.Show()
    End Sub

    Private Sub Share() Handles pb_Share.MouseDown
        Dim share = New sharevb(song)
        share.Show()
    End Sub

    Private Sub Open1g1g() Handles PictureBox5.Click
        Process.Start("http://www.1g1g.com/")
    End Sub
#End Region

#Region "图标变化"
    Private Sub Play_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pb_Play.MouseLeave
        If song.Status = State.playing Then
            pb_Play.Image = My.Resources.pause_up
        ElseIf song.Status = State.paused Then
            pb_Play.Image = My.Resources.play_up
        End If
    End Sub

    Private Sub Play_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pb_Play.MouseMove
        If song.Status = State.playing Then
            pb_Play.Image = My.Resources.pause_over
        ElseIf song.Status = State.paused Then
            pb_Play.Image = My.Resources.play_over
        End If
    End Sub

    Private Sub Next_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pb_Next.MouseLeave
        pb_Next.Image = My.Resources.next_up
    End Sub

    Private Sub Next_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pb_Next.MouseMove
        pb_Next.Image = My.Resources.next_over
    End Sub

    Private Sub pb_Play_MouseUp() Handles pb_Play.MouseUp
        If song.Status = State.playing Then
            pb_Play.Image = My.Resources.pause_up
        ElseIf song.Status = State.paused Then
            pb_Play.Image = My.Resources.play_up
        End If
    End Sub

    Private Sub pb_Next_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles pb_Next.MouseUp
        pb_Next.Image = My.Resources.next_up
    End Sub

    Private Sub pb_Play_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles pb_Play.MouseClick
        If song.Status = State.playing Then
            pb_Play.Image = My.Resources.pause_down
        ElseIf song.Status = State.paused Then
            pb_Play.Image = My.Resources.play_down
        End If
    End Sub

    Private Sub pb_Next_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles pb_Next.MouseClick
        pb_Next.Image = My.Resources.next_down
    End Sub
#End Region

#Region "Other"
    Private Sub 退出ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 退出ToolStripMenuItem.Click
        End
    End Sub
#End Region

#Region "Control"
    Private Sub Flash_FlashCall(ByVal sender As Object, ByVal e As AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEvent) Handles Flash.FlashCall
        Dim document As XDocument = XDocument.Parse(e.request)
        Dim innertext As String = ""
        For Each item In document.<invoke>.<arguments>.<string>
            innertext = item
        Next
        Dim node = document.<invoke>.<arguments>.<object>.<property>
        Select Case innertext
            'Case "connect"
            '    song.Connected = True
            '    Console.WriteLine("Changed to connected")
            'Case "disconnect"
            '    song.Connected = False
            '    Console.WriteLine("Changed to disconnected")
            Case "navigator.userAgent"
                song.Connected = True
                Console.WriteLine("Changed to connected")
            Case "colorStyle"
                For Each element In node
                    Select Case element.Attribute("id").Value
                        Case "type"
                            song.Color = element.Value
                        Case Else
                            Console.WriteLine("In " + innertext + " playerState, Showed a Unknown ID:" & element.Attribute("id").Value & " InnerText:" & element.Value)
                    End Select
                Next
                Select Case song.Color
                    Case "lightblue"
                        NowBrushes = (New ColorSet).lightBlue
                        Me.BackColor = Color.LightBlue
                    Case "red"
                        NowBrushes = (New ColorSet).red
                        Me.BackColor = Color.Red
                    Case "green"
                        NowBrushes = (New ColorSet).green
                        Me.BackColor = Color.Green
                    Case "black"
                        NowBrushes = (New ColorSet).black
                        Me.BackColor = Color.WhiteSmoke
                    Case "blue"
                        NowBrushes = (New ColorSet).blue
                        Me.BackColor = Color.Blue
                    Case "google"
                        NowBrushes = (New ColorSet).google
                        Me.BackColor = Color.LightCyan
                    Case "pink"
                        NowBrushes = (New ColorSet).pink
                        Me.BackColor = Color.Pink
                    Case Else
                        Console.WriteLine("Unknown Color:" & song.Color)
                End Select
            Case "playerState"
                For Each element In node
                    Select Case element.Attribute("id").Value
                        Case "state"
                            Select Case element.Value
                                Case "playing"
                                    song.Status = State.playing
                                    pb_Play.Image = My.Resources.pause_up
                                Case "paused"
                                    song.Status = State.paused
                                    pb_Play.Image = My.Resources.play_up
                                Case "loading"
                                    song.Status = State.loading
                                Case "stop"
                                    song.Status = State.stop
                                    pb_Play.Image = My.Resources.play_up
                            End Select
                        Case Else
                            Console.WriteLine("In " + innertext + " playerState, Showed a Unknown ID:" & element.Attribute("id").Value & " InnerText:" & element.Value)
                    End Select
                Next
            Case "playhead"
                Dim Time As New songTime
                For Each element In node
                    Select Case element.Attribute("id").Value
                        Case "currentTime"
                            Time.currentTime = element.Value
                        Case ("totalTime")
                            Time.totalTime = element.Value
                        Case Else
                            Console.WriteLine("In " + innertext + " playerState, Showed a Unknown ID:" & element.Attribute("id").Value & " InnerText:" & element.Value)
                    End Select
                Next
                song.Time = Time
                song.Connected = True
                LRCShower.ShowLrc(song.getLRCorInfo, song.Time.timeProcess, NowBrushes)
            Case "currentSongInfo"
                Dim TempInfo As New Info
                For Each element In node
                    Select Case element.Attribute("id").Value
                        Case "name"
                            TempInfo.songName = element.Value
                        Case "album"
                            TempInfo.songAlbum = element.Value
                        Case "singer"
                            TempInfo.songSinger = element.Value
                        Case "songId"
                            TempInfo.SongID = element.Value
                        Case Else
                            Console.WriteLine("In " + innertext + " playerState, Showed a Unknown ID:" & element.Attribute("id").Value & " InnerText:" & element.Value)
                    End Select
                Next
                If TempInfo.SongID = song.SongID Then
                    TempInfo = Nothing
                    Exit Sub
                End If
                song = TempInfo
                LRCShower.ShowLrc(song.getLRCorInfo, 1, NowBrushes)
                Label1.Text = song.getLRCorInfo
                mbox.ChangeLabel(song.getLRCorInfo)
            Case "volume"
                For Each element In node
                    Select Case element.Attribute("id").Value
                        Case "volume"
                            mbox.ChangeLabel("音量更改为：" + FormatPercent(element.Value, 0))
                        Case Else
                            Console.WriteLine("In " + innertext + " playerState, Showed a Unknown ID:" & element.Attribute("id").Value & " InnerText:" & element.Value)
                    End Select
                Next
            Case "playMode"
                For Each element In node
                    Select Case element.Attribute("id").Value
                        Case "mode"
                            Select Case element.Value
                                Case "normal"
                                    song.Mode = Mode.normal
                                Case "single"
                                    song.Mode = Mode.single
                                Case "fixed"
                                    song.Mode = Mode.fixed
                            End Select
                        Case Else
                            Console.WriteLine("In " + innertext + " playerState, Showed a Unknown ID:" & element.Attribute("id").Value & " InnerText:" & element.Value)
                    End Select
                Next
            Case "lyricSentence"
                ''not in use
            Case "lrc"
                For Each element In node
                    Select Case element.Attribute("id").Value
                        Case "lrc"
                            song.songLrc = element.Value
                        Case Else
                            Console.WriteLine("In " + innertext + " playerState, Showed a Unknown ID:" & element.Attribute("id").Value & " InnerText:" & element.Value)
                    End Select
                Next
            Case "display"
                For Each element In node
                    Select Case element.Attribute("id").Value
                        Case "text"
                            song.songNowLrc = element.Value
                        Case Else
                            Console.WriteLine("In " + innertext + " playerState, Showed a Unknown ID:" & element.Attribute("id").Value & " InnerText:" & element.Value)
                    End Select
                Next
                LRCShower.ShowLrc(song.getLRCorInfo, song.Time.timeProcess, NowBrushes)
            Case "message"
                For Each element In node
                    Select Case element.Attribute("id").Value
                        Case "text"
                            mbox.ChangeLabel(element.Value)
                        Case Else
                            Console.WriteLine("In " + innertext + " playerState, Showed a Unknown ID:" & element.Attribute("id").Value & " InnerText:" & element.Value)
                    End Select
                Next
            Case "userInfo"
                For Each element In node
                    Select Case element.Attribute("id").Value
                        Case "name"
                            If song.nowUser = element.Value Then
                                Exit Sub
                            End If
                            song.nowUser = element.Value
                        Case Else
                            Console.WriteLine("In " + innertext + " playerState, Showed a Unknown ID:" & element.Attribute("id").Value & " InnerText:" & element.Value)
                    End Select
                Next
                mbox.ChangeLabel("欢迎用户 " & song.nowUser)
                'Case "currentLinkInfo"
            Case Else
                Console.WriteLine("UnknownType:" & innertext)
        End Select
        Exit Sub

WriteIDUnknown:

    End Sub

    Private Function nodeXml(ByVal s As String) As XmlNodeList
        Dim document As New XmlDocument()
        document.LoadXml(s)
        Return document.GetElementsByTagName("arguments")
    End Function

#Region "Commands"
    ''' <summary>
    ''' 传入控制1gcommander的xml代码
    ''' </summary>
    ''' <param name="commandName">命令名</param>
    Public Sub Command(ByVal commandName As String)
        Flash.CallFunction(String.Format("<invoke name=""{0}"" returntype=""xml""><arguments><string>{1}</string></arguments></invoke>", "command", commandName))
        Console.WriteLine(String.Format("Command called: {0}", commandName))
    End Sub

    ''' <summary>
    ''' 传入控制1gcommander的xml代码
    ''' </summary>
    ''' <param name="commandName">命令名</param>
    ''' <param name="value" >命令参数</param>
    Public Sub Command(ByVal commandName As String, ByVal value As String)
        Flash.CallFunction(String.Format("<invoke name=""{0}"" returntype=""xml""><arguments><string>{1}</string><string>{2}</string></arguments></invoke>", "command", commandName, value))
        Console.WriteLine(String.Format("Command called: {0} {1}", commandName, value))
    End Sub

    ''' <summary>
    ''' 传入控制1gcommander的xml代码
    ''' </summary>
    ''' <param name="commandName">命令名</param>
    ''' <param name="value">第一个参数</param>
    ''' <param name="callBackInfo">第二个参数.此参数为Object，当给入callBackInfo时，回调方法playerEventDispatcher()的参数body中也会含有此callBackInfo属性，其值与command()方法给入的相同。调用者可利用此参数在command()与playerEventDispatcher()之间传递数据. Only for getStatus</param>
    Public Sub Command(ByVal commandName As String, ByVal value As String, ByVal callBackInfo As Object)
        Flash.CallFunction(String.Format("<invoke name=""{0}"" returntype=""xml""><arguments><string>{1}</string><string>{2}</string><string>{3}</string></arguments></invoke>", "command", commandName, value, callBackInfo))
        Console.WriteLine(String.Format("Command called: {0} {1} {2}", commandName, value, callBackInfo))
    End Sub
#End Region
#End Region

#Region "图标显消"
    Private Sub mouseInside_Tick(sender As System.Object, e As System.EventArgs) Handles mouseInside.Tick
        With System.Windows.Forms.Cursor.Position
            If Me.Location.X + Me.Width > .X And .X > Me.Location.X Then
                If Me.Location.Y + Me.Height > .Y And .Y > Me.Location.Y Then
                    If song.Connected Then
                        pb_Fav.Visible = True
                        pb_Next.Visible = True
                        pb_Play.Visible = True
                        pb_Search.Visible = True
                        pb_Share.Visible = True
                    End If
                Else
                    pb_Fav.Visible = False
                    pb_Next.Visible = False
                    pb_Play.Visible = False
                    pb_Search.Visible = False
                    pb_Share.Visible = False
                    Console.WriteLine("Y: Bottom:" + (Me.Location.Y + Me.Height).ToString + " Head:" + .Y.ToString + " Left:" + Me.Location.Y.ToString)
                End If
            Else
                pb_Fav.Visible = False
                pb_Next.Visible = False
                pb_Play.Visible = False
                pb_Search.Visible = False
                pb_Share.Visible = False
                Console.WriteLine("X: Right:" + (Me.Location.X + Me.Width).ToString + " Mouse:" + .X.ToString + " Left:" + Me.Location.X.ToString)
            End If
        End With

    End Sub
#End Region
End Class