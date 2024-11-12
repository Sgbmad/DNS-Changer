<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        cmbDNSSets = New ComboBox()
        txtPrimaryDNS = New TextBox()
        txtSecondaryDNS = New TextBox()
        btnAdd = New Button()
        btnUpdate = New Button()
        btnDelete = New Button()
        btnSet = New Button()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Icono = New NotifyIcon(components)
        ctMenu = New ContextMenuStrip(components)
        AbrirProgramaToolStripMenuItem = New ToolStripMenuItem()
        CerrarToolStripMenuItem = New ToolStripMenuItem()
        BorrarCachéToolStripMenuItem = New ToolStripMenuItem()
        tmHide = New Timer(components)
        ctMenu.SuspendLayout()
        SuspendLayout()
        ' 
        ' cmbDNSSets
        ' 
        cmbDNSSets.FormattingEnabled = True
        cmbDNSSets.Location = New Point(119, 19)
        cmbDNSSets.Name = "cmbDNSSets"
        cmbDNSSets.Size = New Size(136, 23)
        cmbDNSSets.TabIndex = 0
        ' 
        ' txtPrimaryDNS
        ' 
        txtPrimaryDNS.Location = New Point(119, 48)
        txtPrimaryDNS.Name = "txtPrimaryDNS"
        txtPrimaryDNS.Size = New Size(136, 23)
        txtPrimaryDNS.TabIndex = 1
        ' 
        ' txtSecondaryDNS
        ' 
        txtSecondaryDNS.Location = New Point(119, 76)
        txtSecondaryDNS.Name = "txtSecondaryDNS"
        txtSecondaryDNS.Size = New Size(136, 23)
        txtSecondaryDNS.TabIndex = 2
        ' 
        ' btnAdd
        ' 
        btnAdd.Location = New Point(268, 19)
        btnAdd.Name = "btnAdd"
        btnAdd.Size = New Size(96, 23)
        btnAdd.TabIndex = 3
        btnAdd.Text = "AÑADIR"
        btnAdd.UseVisualStyleBackColor = True
        ' 
        ' btnUpdate
        ' 
        btnUpdate.Location = New Point(268, 48)
        btnUpdate.Name = "btnUpdate"
        btnUpdate.Size = New Size(96, 23)
        btnUpdate.TabIndex = 4
        btnUpdate.Text = "GUARDAR"
        btnUpdate.UseVisualStyleBackColor = True
        ' 
        ' btnDelete
        ' 
        btnDelete.Location = New Point(268, 78)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(96, 23)
        btnDelete.TabIndex = 5
        btnDelete.Text = "BORRAR"
        btnDelete.UseVisualStyleBackColor = True
        ' 
        ' btnSet
        ' 
        btnSet.Location = New Point(370, 19)
        btnSet.Name = "btnSet"
        btnSet.Size = New Size(130, 82)
        btnSet.TabIndex = 6
        btnSet.Text = "ESTABLECER"
        btnSet.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(14, 19)
        Label1.Name = "Label1"
        Label1.Size = New Size(99, 15)
        Label1.TabIndex = 7
        Label1.Text = "NOMBRE DE DNS"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(74, 51)
        Label2.Name = "Label2"
        Label2.Size = New Size(39, 15)
        Label2.TabIndex = 8
        Label2.Text = "IPV4 1"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(74, 79)
        Label3.Name = "Label3"
        Label3.Size = New Size(39, 15)
        Label3.TabIndex = 9
        Label3.Text = "IPV4 2"
        ' 
        ' Icono
        ' 
        Icono.ContextMenuStrip = ctMenu
        Icono.Icon = CType(resources.GetObject("Icono.Icon"), Icon)
        Icono.Text = "DNS Changer"
        Icono.Visible = True
        ' 
        ' ctMenu
        ' 
        ctMenu.Items.AddRange(New ToolStripItem() {AbrirProgramaToolStripMenuItem, BorrarCachéToolStripMenuItem, CerrarToolStripMenuItem})
        ctMenu.Name = "ctMenu"
        ctMenu.Size = New Size(181, 92)
        ' 
        ' AbrirProgramaToolStripMenuItem
        ' 
        AbrirProgramaToolStripMenuItem.Name = "AbrirProgramaToolStripMenuItem"
        AbrirProgramaToolStripMenuItem.Size = New Size(180, 22)
        AbrirProgramaToolStripMenuItem.Text = "Abrir programa"
        ' 
        ' CerrarToolStripMenuItem
        ' 
        CerrarToolStripMenuItem.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point)
        CerrarToolStripMenuItem.Name = "CerrarToolStripMenuItem"
        CerrarToolStripMenuItem.Size = New Size(180, 22)
        CerrarToolStripMenuItem.Text = "Cerrar"
        ' 
        ' BorrarCachéToolStripMenuItem
        ' 
        BorrarCachéToolStripMenuItem.Name = "BorrarCachéToolStripMenuItem"
        BorrarCachéToolStripMenuItem.Size = New Size(180, 22)
        BorrarCachéToolStripMenuItem.Text = "Borrar caché"
        ' 
        ' tmHide
        ' 
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(512, 112)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(btnSet)
        Controls.Add(btnDelete)
        Controls.Add(btnUpdate)
        Controls.Add(btnAdd)
        Controls.Add(txtSecondaryDNS)
        Controls.Add(txtPrimaryDNS)
        Controls.Add(cmbDNSSets)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        Name = "Form1"
        StartPosition = FormStartPosition.CenterScreen
        Text = "DNS CHANGER"
        ctMenu.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents cmbDNSSets As ComboBox
    Friend WithEvents txtPrimaryDNS As TextBox
    Friend WithEvents txtSecondaryDNS As TextBox
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnSet As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Icono As NotifyIcon
    Friend WithEvents tmHide As Timer
    Friend WithEvents ctMenu As ContextMenuStrip
    Friend WithEvents AbrirProgramaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CerrarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BorrarCachéToolStripMenuItem As ToolStripMenuItem

End Class
