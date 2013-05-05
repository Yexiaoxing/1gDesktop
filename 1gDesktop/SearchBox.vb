Imports System.Threading

Public Class SearchBox
    Dim songID As New List(Of String)
    Dim Query As String
    Sub New(Optional Query_ As String = "")
        InitializeComponent()
        CheckForIllegalCrossThreadCalls = False
        Query = Query_
        Me.Location = New Size(My.Computer.Screen.WorkingArea.Width / 2 - Me.Width / 2, My.Computer.Screen.WorkingArea.Height / 2 - Me.Height / 2)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.MouseDown
        Me.Close()
    End Sub


    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.MouseDown
        Query = TextBox1.Text
        Console.WriteLine(Query)
        FlowLayoutPanel1.Controls.Clear()
        Dim p As New Thread(Sub()
                                Dim SearchResults = Search.Search(Query)
                                For Each i In SearchResults
                                    Me.Invoke(New Action(Of songItem)(AddressOf flowADD), New songItem(i.songName, i.Singer, i.id))
                                Next
                            End Sub)
        p.Start()
    End Sub

    Sub flowADD(item As songItem)
        FlowLayoutPanel1.Controls.Add(item)
    End Sub

    Private Sub windowDraging(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Label1.MouseDown, Me.MouseDown, Panel2.MouseDown
        On Error Resume Next
        ReleaseCapture()
        SendMessage(Handle.ToInt32, WM_SysCommand, SC_MOVE, 0)
    End Sub

    Private Sub SearchBox_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        Dim p As New Thread(Sub()
                                RoundFormPainter.Paint(sender)
                            End Sub)
        p.Start()
    End Sub

    Private Sub FlowLayoutPanel1_Click(sender As Object, e As System.EventArgs) Handles FlowLayoutPanel1.Click
        FlowLayoutPanel1.Focus()
    End Sub

    Private Sub FlowLayoutPanel1_MouseEnter(sender As Object, e As System.EventArgs) Handles FlowLayoutPanel1.MouseEnter
        FlowLayoutPanel1.Focus()
    End Sub
End Class