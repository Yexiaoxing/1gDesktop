Imports System.Xml
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Web
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.Collections.Generic

#Region "LRC"
Module LRCBase
    Public Const WM_SysCommand As Integer = &H112
    Public Const SC_MOVE As Integer = &HF012
    Public Const SC_NCLBUTTONDOWN = &HA1
    <DllImport("user32.dll", EntryPoint:="SendMessage")> _
    Public Function SendMessage(ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function
    <DllImport("user32.dll", EntryPoint:="ReleaseCapture")> _
    Public Function ReleaseCapture() As Integer
    End Function
    Public Sub DrawBP(ByVal Forma As Object, ByVal bitmap As Bitmap, ByVal opacity As Byte)
        If bitmap.PixelFormat <> PixelFormat.Format32bppArgb Then
            Throw New ApplicationException("The bitmap must be 32ppp with alpha-channel.")
        End If
        Dim screenDc As IntPtr = Win32.GetDC(IntPtr.Zero)
        Dim memDc As IntPtr = Win32.CreateCompatibleDC(screenDc)
        Dim hBitmap As IntPtr = IntPtr.Zero
        Dim oldBitmap As IntPtr = IntPtr.Zero

        Try
            hBitmap = bitmap.GetHbitmap(Color.FromArgb(0))
            oldBitmap = Win32.SelectObject(memDc, hBitmap)
            Dim size As New Win32.Size(bitmap.Width, bitmap.Height)
            Dim pointSource As New Win32.Point(0, 0)
            Dim topPos As New Win32.Point(Forma.Left, Forma.Top)
            Dim blend As New Win32.BLENDFUNCTION()
            blend.BlendOp = Win32.AC_SRC_OVER
            blend.BlendFlags = 0
            blend.SourceConstantAlpha = opacity
            blend.AlphaFormat = Win32.AC_SRC_ALPHA

            Win32.UpdateLayeredWindow(Forma.Handle, screenDc, topPos, size, memDc, pointSource, _
            0, blend, Win32.ULW_ALPHA)
        Finally
            Win32.ReleaseDC(IntPtr.Zero, screenDc)
            If hBitmap <> IntPtr.Zero Then
                Win32.SelectObject(memDc, oldBitmap)
                Win32.DeleteObject(hBitmap)
            End If
            Win32.DeleteDC(memDc)
        End Try
    End Sub
    Public Class Win32
        Public Enum Bool
            [False] = 0
            [True]
        End Enum


        <StructLayout(LayoutKind.Sequential)> _
        Public Structure Point
            Public x As Int32
            Public y As Int32

            Public Sub New(ByVal x As Int32, ByVal y As Int32)
                Me.x = x
                Me.y = y
            End Sub
        End Structure


        <StructLayout(LayoutKind.Sequential)> _
        Public Structure Size
            Public cx As Int32
            Public cy As Int32

            Public Sub New(ByVal cx As Int32, ByVal cy As Int32)
                Me.cx = cx
                Me.cy = cy
            End Sub
        End Structure


        <StructLayout(LayoutKind.Sequential, Pack:=1)> _
        Private Structure ARGB
            Public Blue As Byte
            Public Green As Byte
            Public Red As Byte
            Public Alpha As Byte
        End Structure


        <StructLayout(LayoutKind.Sequential, Pack:=1)> _
        Public Structure BLENDFUNCTION
            Public BlendOp As Byte
            Public BlendFlags As Byte
            Public SourceConstantAlpha As Byte
            Public AlphaFormat As Byte
        End Structure


        Public Const ULW_COLORKEY As Int32 = &H1
        Public Const ULW_ALPHA As Int32 = &H2
        Public Const ULW_OPAQUE As Int32 = &H4

        Public Const AC_SRC_OVER As Byte = &H0
        Public Const AC_SRC_ALPHA As Byte = &H1


        Public Declare Auto Function UpdateLayeredWindow Lib "user32.dll" (ByVal hwnd As IntPtr, ByVal hdcDst As IntPtr, ByRef pptDst As Point, ByRef psize As Size, ByVal hdcSrc As IntPtr, ByRef pprSrc As Point, _
        ByVal crKey As Int32, ByRef pblend As BLENDFUNCTION, ByVal dwFlags As Int32) As Bool

        Public Declare Auto Function GetDC Lib "user32.dll" (ByVal hWnd As IntPtr) As IntPtr

        <DllImport("user32.dll", ExactSpelling:=True)> _
        Public Shared Function ReleaseDC(ByVal hWnd As IntPtr, ByVal hDC As IntPtr) As Integer
        End Function

        Public Declare Auto Function CreateCompatibleDC Lib "gdi32.dll" (ByVal hDC As IntPtr) As IntPtr

        Public Declare Auto Function DeleteDC Lib "gdi32.dll" (ByVal hdc As IntPtr) As Bool

        <DllImport("gdi32.dll", ExactSpelling:=True)> _
        Public Shared Function SelectObject(ByVal hDC As IntPtr, ByVal hObject As IntPtr) As IntPtr
        End Function

        Public Declare Auto Function DeleteObject Lib "gdi32.dll" (ByVal hObject As IntPtr) As Bool
    End Class
End Module
#End Region
#Region "搜索"
Public Module Search
    Public Enum Encode
        UTF8
        GBK
    End Enum

    ''' <summary>
    ''' 搜索时返回的数据格式
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure songSearched
        ''' <summary>
        ''' 歌手名
        ''' </summary>
        Dim Singer As String
        ''' <summary>
        ''' 歌曲名
        ''' </summary>
        Dim songName As String
        ''' <summary>
        ''' 专辑
        ''' </summary>
        Dim Album As String
        ''' <summary>
        ''' 歌曲ID
        ''' </summary>
        Dim id
        ''' <summary>
        ''' 上传者
        ''' </summary>
        Dim uploadUser As String
    End Structure

    ''' <summary>
    ''' 在亦歌曲库中搜索，返回songSearched数组。
    ''' </summary>
    ''' <param name="Query">搜索关键字，必须。</param>
    ''' <param name="Encode">可选。编码，可选项UTF8;GBK，默认为UTF8</param>
    ''' <param name="Start">从第几首开始搜索。默认为0</param>
    ''' <param name="Number">返回的最大歌曲数量.默认为20</param>
    ''' <returns>一个songSearched数组，详见相应数组注释。</returns>
    ''' <remarks></remarks>
    Public Function Search(ByVal Query As String, Optional ByVal Encode As Encode = Encode.UTF8, Optional ByVal Start As Integer = 0, Optional ByVal Number As Integer = 20) As List(Of songSearched)
        Dim Searched As New List(Of songSearched)
        Dim req As HttpWebRequest
        If Encode = Encode.UTF8 Then
            req = WebRequest.Create(String.Format("http://search.1g1g.com/public/songs?encoding=utf8&query={0}&start={1}&number={2}", Query, Start, Number))
        Else 'If Encode = EncodeChoice.GBK Then
            req = WebRequest.Create(String.Format("http://search.1g1g.com/public/songs?query={0}&start={1}&number={2}", Query, Start, Number))
        End If
        Dim res As HttpWebResponse = req.GetResponse()
        Dim strm As StreamReader = New StreamReader(res.GetResponseStream(), Encoding.UTF8)
        Dim sline As String
        sline = strm.ReadToEnd()

        Dim xmlDoc As New XmlDocument()
        xmlDoc.LoadXml(sline)
        Dim xmlNode As System.Xml.XmlNode = xmlDoc.SelectSingleNode("//" + "songlist")

        For Each i As XmlNode In xmlNode.ChildNodes
            Dim nodes As New songSearched
            Dim xmlNodes As XmlNode
            xmlNodes = i.ChildNodes(0)
            nodes.id = xmlNodes.InnerText.ToString

            xmlNodes = i.ChildNodes(1)
            nodes.songName = xmlNodes.InnerText.ToString

            xmlNodes = i.ChildNodes(2)
            nodes.Singer = xmlNodes.InnerText.ToString

            xmlNodes = i.ChildNodes(3)
            nodes.Album = xmlNodes.InnerText.ToString

            xmlNodes = i.ChildNodes(4)
            nodes.uploadUser = xmlNodes.InnerText.ToString

            Searched.Add(nodes)
        Next
        Return Searched
    End Function
End Module
#End Region
#Region "状态信息"
Public Structure Info

    '''<summary>
    '''     歌曲名称
    '''</summary>
    Property songName() As String

    '''<summary>
    '''     歌手
    '''</summary>
    Property songSinger() As String

    '''<summary>
    '''     专辑
    '''</summary>
    Property songAlbum() As String

    '''<summary>
    '''     歌词
    '''</summary>
    Property songNowLrc() As String

    ''' <summary>
    '''     歌曲播放进度
    ''' </summary>
    Property Time As songTime

    '''<summary>
    '''     状态
    '''</summary>
    Property Status() As State

    '''<summary>
    '''     颜色
    '''</summary>
    Property Color() As String

    '''<summary>
    '''     音量
    '''</summary>
    Property Volume() As Double

    '''<summary>
    '''     歌曲ID
    '''</summary>
    Property SongID() As Integer

    '''<summary>
    '''     链接ID
    '''</summary>
    Property linkID() As Integer

    '''<summary>
    '''     完整歌词
    '''</summary>
    Property songLrc() As String

    '''<summary>
    '''     当前用户
    '''</summary>
    Property nowUser() As String

    ''' <summary>
    '''     播放模式
    ''' </summary>
    Property Mode As Mode

    ''' <summary>
    '''     连接状态
    ''' </summary>
    Property Connected As Boolean

    Function getLRCorInfo() As String
        If songNowLrc = "" Then
            If songName = "" Then
                Form1.Command(ControlType.getStatus, "currentSongInfo")
                Return "加载中"
            End If
            Return songName & " - " & songSinger
        Else
            Return songNowLrc
        End If
    End Function
End Structure
Public Structure songTime
    '''<summary>
    '''     当前时间
    '''</summary>
    Property currentTime

    ''' <summary>
    '''     总时间
    ''' </summary>
    Public totalTime

    Function timeProcess() As Double
        Dim pc As Double = Double.Parse(currentTime / totalTime)
        Return FormatNumber(pc, 2)
    End Function
End Structure
Public Enum Mode
    ''' <summary>
    ''' 全部
    ''' </summary>
    normal
    ''' <summary>
    ''' 单首
    ''' </summary>
    [single]
    ''' <summary>
    ''' 锁定
    ''' </summary>
    fixed
End Enum
Public Enum State
    ''' <summary>
    ''' 播放中
    ''' </summary>
    playing
    ''' <summary>
    ''' 暂停
    ''' </summary>
    paused
    ''' <summary>
    ''' 加载中
    ''' </summary>
    loading
    ''' <summary>
    ''' 停止
    ''' </summary>
    [stop]
End Enum
#End Region
#Region "控制类型"
Public Structure ControlType
    Shared playPause = "playPause"
    Shared _next = "next"
    Shared volumeOnOff = "volumeOnOff"
    Shared volumeUp = "volumeUp"
    Shared volumeDown = "volumeDown"
    Shared changeVolume = "changeVolume"
    Shared play = "play"
    Shared addToPlayList = "addToPlayList"
    Shared setSearchText = "setSearchText"
    Shared lockCurrent = "lockCurrent"
    Shared saveCurrent = "saveCurrent"
    Shared fav = "saveCurrent"
    Shared changeColorStyle = "changeColorStyle"
    Shared openLyricWindow = "openLyricWindow"
    Shared getStatus = "getStatus"
End Structure

Public Enum colorStyle
    lightblue
    red
    blue
    black
    google
End Enum
#End Region
#Region "Color Set"
Class ColorSet
    Public ReadOnly lightBlue As ProcessColor = New ProcessColor With {.bg = Brushes.Black, .top = Brushes.LightBlue}
    Public ReadOnly red As ProcessColor = New ProcessColor With {.bg = Brushes.Black, .top = Brushes.Red}
    Public ReadOnly blue As ProcessColor = New ProcessColor With {.bg = Brushes.Black, .top = Brushes.Blue}
    Public ReadOnly black As ProcessColor = New ProcessColor With {.bg = Brushes.Black, .top = Brushes.DarkBlue}
    Public ReadOnly google As ProcessColor = New ProcessColor With {.bg = Brushes.Black, .top = Brushes.White}
    Public ReadOnly pink As ProcessColor = New ProcessColor With {.bg = Brushes.Black, .top = Brushes.Pink}
    Public ReadOnly green As ProcessColor = New ProcessColor With {.bg = Brushes.Black, .top = Brushes.LawnGreen}
End Class

Public Class ProcessColor
    Property bg As Brush
    Property top As Brush
End Class
#End Region
#Region "微博 - stop"
'Class Send
'    ' singer name album id do(fav or playing)
'    Dim songID As Integer
'    Dim songSinger, songName, songAlbum As String
'    Dim Messageing As String
'    Private apiKey As String = "015fa4ae548dfb7f041eee0afda9f27d"
'    Private apiKeySecret As String = "7c199ffe1ebd4e92"

'    Private miniblogUri As New Uri("http://api.douban.com/miniblog/saying")

'    Private oAuth As New OAuthBase()
'    Sub New(ByVal Singer As String, ByVal [Name] As String, ByVal Album As String, ByVal ID As Integer)
'        songSinger = Singer
'        songName = [Name]
'        songAlbum = Album
'        songID = ID
'    End Sub

'    Sub New(ByVal Messageing_ As String, ByVal ID As Integer)
'        Messageing = Messageing_
'        songID = ID
'    End Sub
'    Sub Sender_Fav()
'        Dim Message = MessageGet(songName, songSinger, songAlbum, "fav")
'        If Message.Enable = "True" Then
'            For Each i In Message.Status
'                RenJianMessage(i)
'                SendSina(i)
'                SendTwitter(i)
'                SendDouban(i)
'            Next
'        Else
'            If MessageBox.Show("您尚未绑定微博，是否打开窗口绑定？", "提醒", MessageBoxButtons.OKCancel) = DialogResult.OK Then
'                Process.Start("Setting.exe")
'            End If
'        End If
'    End Sub

'    Sub Sender_Playing()
'        Dim Message = MessageGet(songName, songSinger, songAlbum, "fav")
'        If Message.Enable = "True" Then
'            For Each i In Message.Status
'                RenJianMessage(i)
'                SendSina(i)
'                SendTwitter(i)
'                SendDouban(i)
'            Next
'        Else
'            If MessageBox.Show("您尚未绑定微博，是否打开窗口绑定？", "提醒", MessageBoxButtons.OKCancel) = DialogResult.OK Then
'                Process.Start("Setting.exe")
'            End If
'        End If
'    End Sub

'    Sub Sender_Mind()
'        Dim Message = MessageGet("", "", "", "", Messageing)
'        If Message.Enable = "True" Then
'            For Each i In Message.Status
'                RenJianMessage(i)
'                SendSina(i)
'                SendTwitter(i)
'                SendDouban(i)
'            Next
'        End If
'    End Sub

'    Public Sub RenJianMessage(ByVal i As String)
'        Dim un = RenjianGet()
'        If un.Password = "" Then
'            Exit Sub
'        End If
'        Dim urls As String = "http://blog.1g1g.info/update.php?"
'        Dim nv = "username=" & un.Username & "&password=" & un.Password & "&text=" & i & "&id=" & songID & "&source=亦歌"
'        Dim webClient As System.Net.WebClient = New System.Net.WebClient()
'        Dim dataurl = urls & nv
'        Dim datas = Encoding.UTF8.GetBytes(dataurl)
'        Dim result As String = webClient.DownloadString(ConvertByteArrayToString(datas))
'        Console.WriteLine(dataurl)
'    End Sub

'    Public Sub SendTwitter(ByVal i As String)
'        Dim un = TwitterGet()
'        If un.OAuth_Token = "" Then
'            Exit Sub
'        End If
'        Dim url = "http://blog.1g1g.info/t/1g.php?"
'        Dim data = "t1=" & un.OAuth_Token & "&t2=" & un.Token_Secret & "&status=" & i & "&id=" & songID
'        Dim webClient As System.Net.WebClient = New System.Net.WebClient()
'        Dim dataurl = url & data
'        Dim datas = Encoding.UTF8.GetBytes(dataurl)
'        Dim result As String = webClient.DownloadString(ConvertByteArrayToString(datas))
'    End Sub

'    Public Sub SendSina(ByVal i As String)
'        Dim un = SinaGet()
'        If un.OAuth_Token = "" Then
'            Exit Sub
'        End If
'        Dim url = "http://blog.1g1g.info/sina/1g.php?"
'        Dim data = "oauth_token=" & un.OAuth_Token & "&oauth_token_secret=" & un.Token_Secret & "&status=" & i & "&id=" & songID
'        Dim webClient As System.Net.WebClient = New System.Net.WebClient()
'        Dim dataurl = url & data
'        Dim datas = Encoding.UTF8.GetBytes(dataurl)
'        Dim result As String = webClient.DownloadString(ConvertByteArrayToString(datas))
'    End Sub

'    Public Sub SendDouban(ByVal i As String)
'        Dim un = DoubanGet()
'        If un.OAuth_Token = "" Then
'            Exit Sub
'        End If

'        Dim uri As Uri = miniblogUri
'        Dim nonce As String = oAuth.GenerateNonce()
'        Dim timeStamp As String = oAuth.GenerateTimeStamp()
'        Dim normalizeUrl = "", normalizedRequestParameters = ""

'        ' 签名
'        Dim sig As String = oAuth.GenerateSignature(uri, apiKey, apiKeySecret, un.OAuth_Token, un.Token_Secret, "POST", _
'         timeStamp, nonce, OAuthBase.SignatureTypes.HMACSHA1, normalizeUrl, normalizedRequestParameters)
'        sig = Web.HttpUtility.UrlEncode(sig)

'        '构造OAuth头部
'        Dim oauthHeader As New StringBuilder()
'        oauthHeader.AppendFormat("OAuth realm="""", oauth_consumer_key={0}, ", apiKey)
'        oauthHeader.AppendFormat("oauth_nonce={0}, ", nonce)
'        oauthHeader.AppendFormat("oauth_timestamp={0}, ", timeStamp)
'        oauthHeader.AppendFormat("oauth_signature_method={0}, ", "HMAC-SHA1")
'        oauthHeader.AppendFormat("oauth_version={0}, ", "1.0")
'        oauthHeader.AppendFormat("oauth_signature={0}, ", sig)
'        oauthHeader.AppendFormat("oauth_token={0}", un.OAuth_Token)

'        '构造请求
'        Dim requestBody As New StringBuilder("<?xml version='1.0' encoding='UTF-8'?>")
'        requestBody.Append("<entry xmlns:ns0=""http://www.w3.org/2005/Atom"" xmlns:db=""http://www.douban.com/xmlns/"">")
'        requestBody.Append("<content>" & i & " http://www.1g1g.com/s/" & songID & "</content>")
'        requestBody.Append("</entry>")
'        Dim encoding__1 As Encoding = Encoding.GetEncoding("utf-8")
'        Dim data As Byte() = encoding__1.GetBytes(requestBody.ToString())

'        ' Http Request的设置
'        Dim request As HttpWebRequest = DirectCast(WebRequest.Create(uri), HttpWebRequest)
'        request.Headers.Set("Authorization", oauthHeader.ToString())
'        request.ContentType = "application/atom+xml"
'        request.Method = "POST"
'        request.ContentLength = data.Length
'        Dim requestStream As Stream = request.GetRequestStream()
'        requestStream.Write(data, 0, data.Length)
'        requestStream.Close()
'        Try
'            Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
'            Dim stream As New StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8)
'            Dim responseBody As String = stream.ReadToEnd()
'            stream.Close()
'            response.Close()

'        Catch e As WebException
'            Dim stream As New StreamReader(e.Response.GetResponseStream(), System.Text.Encoding.UTF8)
'            Dim responseBody As String = stream.ReadToEnd()
'            stream.Close()

'            Console.WriteLine("发送豆瓣广播失败，原因: " & responseBody)
'            Console.WriteLine("OAUTH头部：" & oauthHeader.ToString)
'        End Try
'    End Sub

'    Private Function ConvertByteArrayToString(ByVal byteArray As Byte()) As String
'        Dim enc As Encoding = Encoding.UTF8
'        Dim text As String = enc.GetString(byteArray)
'        Return text
'    End Function

'    Function Split140(ByVal splittext As String) As String()
'        Dim ReSplit() As String = New String() {""}
'        If splittext.Length > 140 Then
'            For i = 0 To splittext.Length / 140
'                ReSplit(i) = splittext.Substring(140 * i, 140)
'            Next
'            Return ReSplit
'        Else
'            ReSplit(0) = New String(splittext)
'            Return ReSplit
'        End If
'    End Function

'#Region "获取账号资料"
'    Function RenjianGet() As UN.Renjian
'        Dim Un As UN.Renjian
'        Dim Sets As New Renjian
'        Un.Username = Sets.UserName
'        Un.Password = Sets.PassWord
'        Return Un
'    End Function

'    Function SinaGet() As UN.sina
'        Dim Un As UN.sina
'        Dim Sets As New Sina
'        Un.OAuth_Token = Sets.Oauth_Token
'        Un.Token_Secret = Sets.Token_Secret
'        Return Un
'    End Function

'    Function DoubanGet() As UN.Douban
'        Dim Un As UN.Douban
'        Dim Sets As New Douban
'        Un.OAuth_Token = Sets.Oauth_Token
'        Un.Token_Secret = Sets.Token_Secret
'        Return Un
'    End Function

'    Function TwitterGet() As UN.Twitter
'        Dim Un As UN.Twitter
'        Dim Sets As New Twitter
'        Un.OAuth_Token = Sets.Oauth_Token
'        Un.Token_Secret = Sets.Token_Secret
'        Return Un
'    End Function

'    Function MessageGet(ByVal SongName, ByVal SongSinger, ByVal SongAlubm, ByVal [Do], Optional ByVal SendMessage = "") As UN
'        Dim Un As UN
'        Dim Sets As New Helper.Message
'        Un.Enable = Sets.Enable
'        Select Case [Do]
'            Case "fav"
'                Un.TmpS = String.Format(Sets.Fav, SongName, SongSinger, SongAlubm)
'            Case "playing"
'                Un.TmpS = String.Format(Sets.Playing, SongName, SongSinger, SongAlubm)
'            Case Else
'                Un.TmpS = SendMessage
'        End Select
'        Un.Status = Split140(Un.TmpS)
'        Return Un
'    End Function
'#End Region
'End Class

'Structure UN
'    Structure sina
'        Dim OAuth_Token, Token_Secret As String
'    End Structure
'    Structure Renjian
'        Dim Username, Password As String
'    End Structure
'    Structure Twitter
'        Dim OAuth_Token, Token_Secret As String
'    End Structure
'    Structure Douban
'        Dim OAuth_Token, Token_Secret As String
'    End Structure
'    Dim Enable As String
'    Dim Status As String()
'    Dim TmpS As String
'End Structure
#End Region
#Region "圆角窗体"
Public Module RoundFormPainter
    Public Sub Paint(ByVal sender As Object)
        On Error GoTo er
        Dim form As Form = sender
        Dim list As List(Of Point) = New List(Of Point)
        Dim width As Integer = form.Width
        Dim height As Integer = form.Height

        '左上
        list.Add(New Point(0, 5))
        list.Add(New Point(1, 5))
        list.Add(New Point(1, 3))
        list.Add(New Point(2, 3))
        list.Add(New Point(2, 2))
        list.Add(New Point(3, 2))
        list.Add(New Point(3, 1))
        list.Add(New Point(5, 1))
        list.Add(New Point(5, 0))
        '右上
        list.Add(New Point(width - 5, 0))
        list.Add(New Point(width - 5, 1))
        list.Add(New Point(width - 3, 1))
        list.Add(New Point(width - 3, 2))
        list.Add(New Point(width - 2, 2))
        list.Add(New Point(width - 2, 3))
        list.Add(New Point(width - 1, 3))
        list.Add(New Point(width - 1, 5))
        list.Add(New Point(width - 0, 5))
        '右下
        list.Add(New Point(width - 0, height - 5))
        list.Add(New Point(width - 1, height - 5))
        list.Add(New Point(width - 1, height - 3))
        list.Add(New Point(width - 2, height - 3))
        list.Add(New Point(width - 2, height - 2))
        list.Add(New Point(width - 3, height - 2))
        list.Add(New Point(width - 3, height - 1))
        list.Add(New Point(width - 5, height - 1))
        list.Add(New Point(width - 5, height - 0))
        '左下
        list.Add(New Point(5, height - 0))
        list.Add(New Point(5, height - 1))
        list.Add(New Point(3, height - 1))
        list.Add(New Point(3, height - 2))
        list.Add(New Point(2, height - 2))
        list.Add(New Point(2, height - 3))
        list.Add(New Point(1, height - 3))
        list.Add(New Point(1, height - 5))
        list.Add(New Point(0, height - 5))

        Dim points As Point() = list.ToArray()

        Dim shape = New GraphicsPath()
        shape.AddPolygon(points)

        form.CheckForIllegalCrossThreadCalls = False
        '将窗体的显示区域设为GraphicsPath的实例
        form.Region = New System.Drawing.Region(shape)
        Exit Sub
er:
        Resume
    End Sub
End Module
#End Region