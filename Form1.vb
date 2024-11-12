Imports System.IO
Imports Newtonsoft.Json
Imports System.Net.NetworkInformation
Imports System.Diagnostics
Imports System.Net

Public Class Form1
    ' Estructura para almacenar cada conjunto de DNS
    Public Class DNSSet
        Public Property Name As String
        Public Property PrimaryDNS As String
        Public Property SecondaryDNS As String
    End Class

    ' Lista de conjuntos de DNS
    Private dnsSets As New List(Of DNSSet)
    Private dnsFilePath As String = "dns_sets.json"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "DNS CHANGER - " & Application.ProductVersion
        LoadDNSData()
        PopulateComboBox()
        SelectCurrentSystemDNS()
        Icono.BalloonTipText = cmbDNSSets.Text & " activo."
        Icono.BalloonTipIcon = ToolTipIcon.Info
        Icono.ShowBalloonTip(30000)
        Icono.Visible = True
        tmHide.Enabled = True
    End Sub

    ' Cargar datos de conjuntos de DNS desde el archivo JSON
    Private Sub LoadDNSData()
        If File.Exists(dnsFilePath) Then
            Dim jsonData As String = File.ReadAllText(dnsFilePath)
            dnsSets = JsonConvert.DeserializeObject(Of List(Of DNSSet))(jsonData)
        End If
    End Sub

    ' Guardar conjuntos de DNS en el archivo JSON
    Private Sub SaveDNSData()
        Dim jsonData As String = JsonConvert.SerializeObject(dnsSets, Formatting.Indented)
        File.WriteAllText(dnsFilePath, jsonData)
    End Sub

    ' Poblar ComboBox con nombres de conjuntos de DNS
    Private Sub PopulateComboBox()
        cmbDNSSets.Items.Clear()
        For Each dnsSet In dnsSets
            cmbDNSSets.Items.Add(dnsSet.Name)
        Next
    End Sub

    ' Seleccionar el conjunto de DNS actual del sistema en el ComboBox
    Private Sub SelectCurrentSystemDNS()
        Dim currentDnsServers As List(Of String) = GetCurrentSystemDNSServers()

        ' Buscar un conjunto en dnsSets que coincida con los DNS actuales (0 y 1 IPV6. 2 y 3 IPV4)
        Dim matchingSet = dnsSets.FirstOrDefault(Function(d) d.PrimaryDNS = currentDnsServers(2) AndAlso
                                                         d.SecondaryDNS = If(currentDnsServers.Count > 1, currentDnsServers(3), String.Empty))
        If matchingSet IsNot Nothing Then
            cmbDNSSets.SelectedItem = matchingSet.Name
            txtPrimaryDNS.Text = matchingSet.PrimaryDNS
            txtSecondaryDNS.Text = matchingSet.SecondaryDNS
        End If
    End Sub

    ' Obtener los servidores DNS actuales del sistema
    Private Function GetCurrentSystemDNSServers() As List(Of String)
        Dim dnsServers As New List(Of String)
        Dim networkConfig = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(Function(nic) nic.OperationalStatus = OperationalStatus.Up)

        If networkConfig IsNot Nothing Then
            Dim ipProps = networkConfig.GetIPProperties()
            For Each dnsAddr In ipProps.DnsAddresses
                dnsServers.Add(dnsAddr.ToString())
            Next
        End If

        Return dnsServers
    End Function

    ' Agregar nuevo conjunto de DNS
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim newDNS As New DNSSet With {
            .Name = InputBox("Nombre del conjunto de DNS:"),
            .PrimaryDNS = txtPrimaryDNS.Text,
            .SecondaryDNS = txtSecondaryDNS.Text
        }

        If Not String.IsNullOrEmpty(newDNS.Name) Then
            dnsSets.Add(newDNS)
            SaveDNSData()
            PopulateComboBox()
            cmbDNSSets.SelectedItem = newDNS.Name
        End If
    End Sub

    ' Actualizar conjunto de DNS seleccionado
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim selectedName As String = cmbDNSSets.SelectedItem?.ToString()
        If Not String.IsNullOrEmpty(selectedName) Then
            Dim dnsSet = dnsSets.FirstOrDefault(Function(d) d.Name = selectedName)
            If dnsSet IsNot Nothing Then
                dnsSet.PrimaryDNS = txtPrimaryDNS.Text
                dnsSet.SecondaryDNS = txtSecondaryDNS.Text
                SaveDNSData()
            End If
        End If
    End Sub

    ' Eliminar conjunto de DNS seleccionado
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim selectedName As String = cmbDNSSets.SelectedItem?.ToString()
        If Not String.IsNullOrEmpty(selectedName) Then
            dnsSets.RemoveAll(Function(d) d.Name = selectedName)
            SaveDNSData()
            PopulateComboBox()
            txtPrimaryDNS.Clear()
            txtSecondaryDNS.Clear()
        End If
    End Sub

    ' Cambiar el conjunto de DNS seleccionado en el ComboBox
    Private Sub cmbDNSSets_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDNSSets.SelectedIndexChanged
        Dim selectedName As String = cmbDNSSets.SelectedItem?.ToString()
        If Not String.IsNullOrEmpty(selectedName) Then
            Dim dnsSet = dnsSets.FirstOrDefault(Function(d) d.Name = selectedName)
            If dnsSet IsNot Nothing Then
                txtPrimaryDNS.Text = dnsSet.PrimaryDNS
                txtSecondaryDNS.Text = dnsSet.SecondaryDNS
            End If
        End If
    End Sub

    Private Sub btnSet_Click(sender As Object, e As EventArgs) Handles btnSet.Click
        Dim selectedDnsSet = dnsSets.FirstOrDefault(Function(d) d.Name = cmbDNSSets.SelectedItem?.ToString())
        If selectedDnsSet IsNot Nothing Then
            ' Establecer los DNS seleccionados
            SetDNSServers(New List(Of String) From {selectedDnsSet.PrimaryDNS, selectedDnsSet.SecondaryDNS})
            ' Flush DNS tras aplicar los cambios
            FlushDNS()
        End If
    End Sub

    ' Establecer los DNS seleccionados en el adaptador de red principal
    Private Sub SetDNSServers(dnsServers As List(Of String))
        Dim networkConfig = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(Function(nic) nic.OperationalStatus = OperationalStatus.Up)
        If networkConfig IsNot Nothing Then
            Dim processStartInfo As New ProcessStartInfo("netsh", $"interface ip set dns name=""{networkConfig.Name}"" source=static addr={dnsServers(0)}")
            processStartInfo.Verb = "runas"
            processStartInfo.CreateNoWindow = True
            processStartInfo.UseShellExecute = False

            Try
                ' Aplicar el primer DNS
                Process.Start(processStartInfo).WaitForExit()

                ' Agregar DNS adicionales (si existen)
                For i = 1 To dnsServers.Count - 1
                    Dim additionalDNSInfo As New ProcessStartInfo("netsh", $"interface ip add dns name=""{networkConfig.Name}"" addr={dnsServers(i)} index={i + 1}")
                    additionalDNSInfo.Verb = "runas"
                    additionalDNSInfo.CreateNoWindow = True
                    additionalDNSInfo.UseShellExecute = False
                    Process.Start(additionalDNSInfo).WaitForExit()
                Next
                Icono.BalloonTipText = cmbDNSSets.Text & " activo."
                Icono.BalloonTipIcon = ToolTipIcon.Info
                Icono.ShowBalloonTip(30000)

            Catch ex As Exception
                MessageBox.Show($"Error al aplicar DNS: {ex.Message}")
            End Try
        End If
    End Sub

    ' Método para ejecutar flush DNS
    Private Sub FlushDNS()
        Dim processStartInfo As New ProcessStartInfo("ipconfig", "/flushdns")
        processStartInfo.Verb = "runas"
        processStartInfo.CreateNoWindow = True
        processStartInfo.UseShellExecute = False

        Try
            Process.Start(processStartInfo).WaitForExit()
            Icono.BalloonTipText = "Caché borrada"
            Icono.BalloonTipIcon = ToolTipIcon.Info
            Icono.ShowBalloonTip(30000)
        Catch ex As Exception
            MessageBox.Show($"Error al ejecutar flush DNS: {ex.Message}")
        End Try
    End Sub

    Private Sub tmHide_Tick(sender As Object, e As EventArgs) Handles tmHide.Tick
        Me.Hide()
        tmHide.Enabled = False
    End Sub

    Private Sub AbrirProgramaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirProgramaToolStripMenuItem.Click
        Me.Show()
    End Sub

    Private Sub CerrarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CerrarToolStripMenuItem.Click
        End
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub Icono_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Icono.MouseDoubleClick
        Me.Show()
    End Sub

    Private Sub BorrarCachéToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BorrarCachéToolStripMenuItem.Click
        FlushDNS()
    End Sub
End Class
