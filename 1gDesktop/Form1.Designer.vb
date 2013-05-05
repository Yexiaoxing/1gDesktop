<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.pb_Fav = New System.Windows.Forms.PictureBox()
        Me.GlobalMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.获取连接IDToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.退出ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.pb_Play = New System.Windows.Forms.PictureBox()
        Me.pb_Next = New System.Windows.Forms.PictureBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pb_Share = New System.Windows.Forms.PictureBox()
        Me.pb_Search = New System.Windows.Forms.PictureBox()
        Me.Flash = New AxShockwaveFlashObjects.AxShockwaveFlash()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.mouseInside = New System.Windows.Forms.Timer(Me.components)
        CType(Me.pb_Fav, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GlobalMenu.SuspendLayout()
        CType(Me.pb_Play, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_Next, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_Share, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_Search, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Flash, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pb_Fav
        '
        Me.pb_Fav.BackColor = System.Drawing.Color.Transparent
        Me.pb_Fav.ContextMenuStrip = Me.GlobalMenu
        Me.pb_Fav.Image = Global._1gDesktop.My.Resources.Resources.Favorites1
        Me.pb_Fav.Location = New System.Drawing.Point(111, 3)
        Me.pb_Fav.Name = "pb_Fav"
        Me.pb_Fav.Size = New System.Drawing.Size(30, 30)
        Me.pb_Fav.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pb_Fav.TabIndex = 2
        Me.pb_Fav.TabStop = False
        Me.ToolTip1.SetToolTip(Me.pb_Fav, "收藏")
        Me.pb_Fav.Visible = False
        '
        'GlobalMenu
        '
        Me.GlobalMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.获取连接IDToolStripMenuItem, Me.退出ToolStripMenuItem})
        Me.GlobalMenu.Name = "GlobalMenu"
        Me.GlobalMenu.Size = New System.Drawing.Size(138, 48)
        '
        '获取连接IDToolStripMenuItem
        '
        Me.获取连接IDToolStripMenuItem.Name = "获取连接IDToolStripMenuItem"
        Me.获取连接IDToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.获取连接IDToolStripMenuItem.Text = "获取连接ID"
        '
        '退出ToolStripMenuItem
        '
        Me.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem"
        Me.退出ToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.退出ToolStripMenuItem.Text = "退出"
        '
        'pb_Play
        '
        Me.pb_Play.BackColor = System.Drawing.Color.Transparent
        Me.pb_Play.ContextMenuStrip = Me.GlobalMenu
        Me.pb_Play.Image = CType(resources.GetObject("pb_Play.Image"), System.Drawing.Image)
        Me.pb_Play.Location = New System.Drawing.Point(39, 3)
        Me.pb_Play.Name = "pb_Play"
        Me.pb_Play.Size = New System.Drawing.Size(30, 30)
        Me.pb_Play.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pb_Play.TabIndex = 0
        Me.pb_Play.TabStop = False
        Me.ToolTip1.SetToolTip(Me.pb_Play, "播放")
        Me.pb_Play.Visible = False
        '
        'pb_Next
        '
        Me.pb_Next.BackColor = System.Drawing.Color.Transparent
        Me.pb_Next.ContextMenuStrip = Me.GlobalMenu
        Me.pb_Next.Image = CType(resources.GetObject("pb_Next.Image"), System.Drawing.Image)
        Me.pb_Next.Location = New System.Drawing.Point(75, 3)
        Me.pb_Next.Name = "pb_Next"
        Me.pb_Next.Size = New System.Drawing.Size(30, 30)
        Me.pb_Next.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pb_Next.TabIndex = 1
        Me.pb_Next.TabStop = False
        Me.ToolTip1.SetToolTip(Me.pb_Next, "下一首")
        Me.pb_Next.Visible = False
        '
        'PictureBox5
        '
        Me.PictureBox5.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox5.ContextMenuStrip = Me.GlobalMenu
        Me.PictureBox5.Image = CType(resources.GetObject("PictureBox5.Image"), System.Drawing.Image)
        Me.PictureBox5.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(30, 30)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox5.TabIndex = 4
        Me.PictureBox5.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox5, "收藏")
        '
        'Label1
        '
        Me.Label1.AllowDrop = True
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.ContextMenuStrip = Me.GlobalMenu
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label1.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(183, 0)
        Me.Label1.MinimumSize = New System.Drawing.Size(20, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(3)
        Me.Label1.Size = New System.Drawing.Size(144, 38)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "欢迎使用亦歌助手"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.Label1, "当前歌曲")
        '
        'pb_Share
        '
        Me.pb_Share.BackColor = System.Drawing.Color.Transparent
        Me.pb_Share.ContextMenuStrip = Me.GlobalMenu
        Me.pb_Share.Image = Global._1gDesktop.My.Resources.Resources.Twitter
        Me.pb_Share.Location = New System.Drawing.Point(333, 3)
        Me.pb_Share.Name = "pb_Share"
        Me.pb_Share.Size = New System.Drawing.Size(30, 30)
        Me.pb_Share.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pb_Share.TabIndex = 5
        Me.pb_Share.TabStop = False
        Me.ToolTip1.SetToolTip(Me.pb_Share, "收藏")
        Me.pb_Share.Visible = False
        '
        'pb_Search
        '
        Me.pb_Search.BackColor = System.Drawing.Color.Transparent
        Me.pb_Search.ContextMenuStrip = Me.GlobalMenu
        Me.pb_Search.Image = Global._1gDesktop.My.Resources.Resources.Search1
        Me.pb_Search.Location = New System.Drawing.Point(147, 3)
        Me.pb_Search.Name = "pb_Search"
        Me.pb_Search.Size = New System.Drawing.Size(30, 30)
        Me.pb_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pb_Search.TabIndex = 6
        Me.pb_Search.TabStop = False
        Me.ToolTip1.SetToolTip(Me.pb_Search, "收藏")
        Me.pb_Search.Visible = False
        '
        'Flash
        '
        Me.Flash.Enabled = True
        Me.Flash.Location = New System.Drawing.Point(0, 0)
        Me.Flash.Name = "Flash"
        Me.Flash.OcxState = CType(resources.GetObject("Flash.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Flash.Size = New System.Drawing.Size(0, 0)
        Me.Flash.TabIndex = 1
        Me.Flash.Visible = False
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.ContextMenuStrip = Me.GlobalMenu
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "NotifyIcon1"
        Me.NotifyIcon1.Visible = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TableLayoutPanel1.ColumnCount = 7
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.pb_Search, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.pb_Share, 6, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.pb_Fav, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.PictureBox5, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.pb_Next, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 5, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.pb_Play, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(371, 43)
        Me.TableLayoutPanel1.TabIndex = 5
        '
        'mouseInside
        '
        Me.mouseInside.Enabled = True
        Me.mouseInside.Interval = 1000
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.SkyBlue
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(371, 43)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.Flash)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Location = New System.Drawing.Point(200, 200)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.Opacity = 0.8R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.TopMost = True
        CType(Me.pb_Fav, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GlobalMenu.ResumeLayout(False)
        CType(Me.pb_Play, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_Next, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_Share, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_Search, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Flash, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pb_Fav As System.Windows.Forms.PictureBox
    Friend WithEvents pb_Next As System.Windows.Forms.PictureBox
    Friend WithEvents pb_Play As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GlobalMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Flash As AxShockwaveFlashObjects.AxShockwaveFlash
    Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
    Friend WithEvents 退出ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents 获取连接IDToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pb_Share As System.Windows.Forms.PictureBox
    Friend WithEvents pb_Search As System.Windows.Forms.PictureBox
    Friend WithEvents mouseInside As System.Windows.Forms.Timer
End Class
