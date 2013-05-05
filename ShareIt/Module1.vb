Imports System.Text
Imports Settinger.Helper
Imports System.Web
Imports System.Net
Imports System.IO

Module Send
    ' singer name album id do(fav or playing)
    Dim songID As Integer

    Private apiKey As String = "015fa4ae548dfb7f041eee0afda9f27d"
    Private apiKeySecret As String = "7c199ffe1ebd4e92"

    Private miniblogUri As New Uri("http://api.douban.com/miniblog/saying")

    Private oAuth As New OAuthBase()
    Sub Sender_Fav(ByVal songSinger As String, ByVal songName As String, ByVal songAlbum As String, ByVal [ID] As Integer)
        Dim Message = MessageGet(songName, songSinger, songAlbum, "fav")
        songID = [ID]
        If Message.Enable = "True" Then
            For Each i In Message.Status
                RenJianMessage(i)
                SendSina(i)
                SendTwitter(i)
                SendDouban(i)
            Next
        End If
    End Sub

    Sub Sender_Playing(ByVal songSinger As String, ByVal songName As String, ByVal songAlbum As String, ByVal [ID] As Integer)
        Dim Message = MessageGet(songName, songSinger, songAlbum, "fav")
        songID = [ID]
        If Message.Enable = "True" Then
            For Each i In Message.Status
                RenJianMessage(i)
                SendSina(i)
                SendTwitter(i)
                SendDouban(i)
            Next
        End If
    End Sub

    Sub Sender_Mind(ByVal Messageing As String, ByVal [ID] As Integer)
        Dim Message = MessageGet("", "", "", "", Messageing)
        songID = [ID]
        If Message.Enable = "True" Then
            For Each i In Message.Status
                RenJianMessage(i)
                SendSina(i)
                SendTwitter(i)
                SendDouban(i)
            Next
        End If
    End Sub

    Public Sub RenJianMessage(ByVal i As String)
        Dim un = RenjianGet()
        If un.Password = "" Then
            Exit Sub
        End If
        Dim urls As String = "http://blog.1g1g.info/update.php?"
        Dim nv = "username=" & un.Username & "&password=" & un.Password & "&text=" & i & "&id=" & songID & "&source=亦歌"
        Dim webClient As System.Net.WebClient = New System.Net.WebClient()
        Dim dataurl = urls & nv
        Dim datas = Encoding.UTF8.GetBytes(dataurl)
        Dim result As String = webClient.DownloadString(ConvertByteArrayToString(datas))
    End Sub

    Public Sub SendTwitter(ByVal i As String)
        Dim un = TwitterGet()
        If un.OAuth_Token = "" Then
            Exit Sub
        End If
        Dim url = "http://blog.1g1g.info/t/1g.php?"
        Dim data = "t1=" & un.OAuth_Token & "&t2=" & un.Token_Secret & "&status=" & i & "&id=" & songID
        Dim webClient As System.Net.WebClient = New System.Net.WebClient()
        Dim dataurl = url & data
        Dim datas = Encoding.UTF8.GetBytes(dataurl)
        Dim result As String = webClient.DownloadString(ConvertByteArrayToString(datas))
    End Sub

    Public Sub SendSina(ByVal i As String)
        Dim un = SinaGet()
        If un.OAuth_Token = "" Then
            Exit Sub
        End If
        Dim url = "http://blog.1g1g.info/sina/1g.php?"
        Dim data = "oauth_token=" & un.OAuth_Token & "&oauth_token_secret=" & un.Token_Secret & "&status=" & i & "&id=" & songID
        Dim webClient As System.Net.WebClient = New System.Net.WebClient()
        Dim dataurl = url & data
        Dim datas = Encoding.UTF8.GetBytes(dataurl)
        Dim result As String = webClient.DownloadString(ConvertByteArrayToString(datas))
    End Sub

    Public Sub SendDouban(ByVal i As String)
        Dim un = DoubanGet()
        If un.OAuth_Token = "" Then
            Exit Sub
        End If

        Dim uri As Uri = miniblogUri
        Dim nonce As String = oAuth.GenerateNonce()
        Dim timeStamp As String = oAuth.GenerateTimeStamp()
        Dim normalizeUrl = "", normalizedRequestParameters = ""

        ' 签名
        Dim sig As String = oAuth.GenerateSignature(uri, apiKey, apiKeySecret, un.OAuth_Token, un.Token_Secret, "POST", _
         timeStamp, nonce, OAuthBase.SignatureTypes.HMACSHA1, normalizeUrl, normalizedRequestParameters)
        sig = Web.HttpUtility.UrlEncode(sig)

        '构造OAuth头部
        Dim oauthHeader As New StringBuilder()
        oauthHeader.AppendFormat("OAuth realm="""", oauth_consumer_key={0}, ", apiKey)
        oauthHeader.AppendFormat("oauth_nonce={0}, ", nonce)
        oauthHeader.AppendFormat("oauth_timestamp={0}, ", timeStamp)
        oauthHeader.AppendFormat("oauth_signature_method={0}, ", "HMAC-SHA1")
        oauthHeader.AppendFormat("oauth_version={0}, ", "1.0")
        oauthHeader.AppendFormat("oauth_signature={0}, ", sig)
        oauthHeader.AppendFormat("oauth_token={0}", un.OAuth_Token)

        '构造请求
        Dim requestBody As New StringBuilder("<?xml version='1.0' encoding='UTF-8'?>")
        requestBody.Append("<entry xmlns:ns0=""http://www.w3.org/2005/Atom"" xmlns:db=""http://www.douban.com/xmlns/"">")
        requestBody.Append("<content>" & i & " http://www.1g1g.com/s/" & songID & "</content>")
        requestBody.Append("</entry>")
        Dim encoding__1 As Encoding = Encoding.GetEncoding("utf-8")
        Dim data As Byte() = encoding__1.GetBytes(requestBody.ToString())

        ' Http Request的设置
        Dim request As HttpWebRequest = DirectCast(WebRequest.Create(uri), HttpWebRequest)
        request.Headers.Set("Authorization", oauthHeader.ToString())
        request.ContentType = "application/atom+xml"
        request.Method = "POST"
        request.ContentLength = data.Length
        Dim requestStream As Stream = request.GetRequestStream()
        requestStream.Write(data, 0, data.Length)
        requestStream.Close()
        Try
            Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
            Dim stream As New StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8)
            Dim responseBody As String = stream.ReadToEnd()
            stream.Close()
            response.Close()

        Catch e As WebException
            Dim stream As New StreamReader(e.Response.GetResponseStream(), System.Text.Encoding.UTF8)
            Dim responseBody As String = stream.ReadToEnd()
            stream.Close()

            Console.WriteLine("发送豆瓣广播失败，原因: " & responseBody)
            Console.WriteLine("OAUTH头部：" & oauthHeader.ToString)
        End Try
    End Sub

    Private Function ConvertByteArrayToString(ByVal byteArray As Byte()) As String
        Dim enc As Encoding = Encoding.UTF8
        Dim text As String = enc.GetString(byteArray)
        Return text
    End Function

    Function Split140(ByVal splittext As String) As String()
        Dim ReSplit() As String = New String() {""}
        If splittext.Length > 140 Then
            For i = 0 To splittext.Length / 140
                ReSplit(i) = splittext.Substring(140 * i, 140)
            Next
            Return ReSplit
        Else
            ReSplit(0) = New String(splittext)
            Return ReSplit
        End If
    End Function

#Region "获取账号资料"
    Function RenjianGet() As UN.Renjian
        Dim Un As UN.Renjian
        Dim Sets As New Renjian
        Un.Username = Sets.UserName
        Un.Password = Sets.PassWord
        Return Un
    End Function

    Function SinaGet() As UN.sina
        Dim Un As UN.sina
        Dim Sets As New Sina
        Un.OAuth_Token = Sets.Oauth_Token
        Un.Token_Secret = Sets.Token_Secret
        Return Un
    End Function

    Function DoubanGet() As UN.Douban
        Dim Un As UN.Douban
        Dim Sets As New Douban
        Un.OAuth_Token = Sets.Oauth_Token
        Un.Token_Secret = Sets.Token_Secret
        Return Un
    End Function

    Function TwitterGet() As UN.Twitter
        Dim Un As UN.Twitter
        Dim Sets As New Twitter
        Un.OAuth_Token = Sets.Oauth_Token
        Un.Token_Secret = Sets.Token_Secret
        Return Un
    End Function

    Function MessageGet(ByVal SongName, ByVal SongSinger, ByVal SongAlubm, ByVal [Do], Optional ByVal SendMessage = "") As UN
        Dim Un As UN
        Dim Sets As New Message
        Un.Enable = Sets.Enable
        Select Case [Do]
            Case "fav"
                Un.TmpS = String.Format(Sets.Fav, SongName, SongSinger, SongAlubm)
            Case "playing"
                Un.TmpS = String.Format(Sets.Playing, SongName, SongSinger, SongAlubm)
            Case Else
                Un.TmpS = SendMessage
        End Select
        Un.Status = Split140(Un.TmpS)
        Return Un
    End Function
#End Region
End Module

Structure UN
    Structure sina
        Dim OAuth_Token, Token_Secret As String
    End Structure
    Structure Renjian
        Dim Username, Password As String
    End Structure
    Structure Twitter
        Dim OAuth_Token, Token_Secret As String
    End Structure
    Structure Douban
        Dim OAuth_Token, Token_Secret As String
    End Structure
    Dim Enable As String
    Dim Status As String()
    Dim TmpS As String
End Structure