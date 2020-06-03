<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.btnPdfV2 = New System.Windows.Forms.Button()
        Me.btnPdfV1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(45, 97)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(311, 289)
        Me.RichTextBox1.TabIndex = 5
        Me.RichTextBox1.Text = ""
        '
        'btnPdfV2
        '
        Me.btnPdfV2.Location = New System.Drawing.Point(281, 48)
        Me.btnPdfV2.Name = "btnPdfV2"
        Me.btnPdfV2.Size = New System.Drawing.Size(75, 23)
        Me.btnPdfV2.TabIndex = 4
        Me.btnPdfV2.Text = "PDF V2"
        Me.btnPdfV2.UseVisualStyleBackColor = True
        '
        'btnPdfV1
        '
        Me.btnPdfV1.Location = New System.Drawing.Point(45, 48)
        Me.btnPdfV1.Name = "btnPdfV1"
        Me.btnPdfV1.Size = New System.Drawing.Size(75, 23)
        Me.btnPdfV1.TabIndex = 3
        Me.btnPdfV1.Text = "PDF V1"
        Me.btnPdfV1.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(494, 450)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.btnPdfV2)
        Me.Controls.Add(Me.btnPdfV1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents btnPdfV2 As Button
    Friend WithEvents btnPdfV1 As Button
End Class
