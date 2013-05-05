<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class songItem
    Inherits System.Windows.Forms.UserControl

    'UserControl 重写 Dispose，以清理组件列表。
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
        Me.songName = New System.Windows.Forms.Label()
        Me.songSinger = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'songName
        '
        Me.songName.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.songName.Location = New System.Drawing.Point(3, 2)
        Me.songName.Name = "songName"
        Me.songName.Size = New System.Drawing.Size(234, 23)
        Me.songName.TabIndex = 0
        Me.songName.Text = "Label1"
        Me.songName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'songSinger
        '
        Me.songSinger.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.songSinger.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.songSinger.Location = New System.Drawing.Point(3, 23)
        Me.songSinger.Name = "songSinger"
        Me.songSinger.Size = New System.Drawing.Size(234, 21)
        Me.songSinger.TabIndex = 1
        Me.songSinger.Text = "Label1"
        Me.songSinger.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global._1gDesktop.My.Resources.Resources.play_up
        Me.PictureBox1.Location = New System.Drawing.Point(243, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(39, 40)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'songItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.songSinger)
        Me.Controls.Add(Me.songName)
        Me.Name = "songItem"
        Me.Size = New System.Drawing.Size(285, 52)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents songName As System.Windows.Forms.Label
    Friend WithEvents songSinger As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox

End Class
