Imports System.IO
Imports Newtonsoft.Json

Imports BS = TeCorp.FirmaDigital.Service.Negocio
Imports BE = TeCorp.FirmaDigital.Service.Entidad
Imports BB = TeCorp.FirmaDigital.Service.Base.Enumerador


Public Class Form1
    Private Sub AbrirDocumento(ByRef StrNombreArchivo As String, ByRef StrRutaCompleta As String)
        Dim open As New OpenFileDialog
        With open
            .Title = "Buscar pdf"
            .Filter = "|*.pdf"
            If .ShowDialog Then
                StrRutaCompleta = open.FileName
                StrNombreArchivo = open.SafeFileName
            End If
        End With
    End Sub
    Private Sub btnPdfV1_Click(sender As Object, e As EventArgs) Handles btnPdfV1.Click
        Dim BEImplementacion As New BS.Implementacion(BB.SistemaCliente.ESalud)
        Dim BEDatosFirma As New BE.RequestDatosFirma
        Dim BEDatosCertificado As BE.RequestDatosCertificado
        Dim BEResponseFirmaDigital As BE.ResponseFirmaDigital
        Dim strNombrePdf As String = String.Empty
        Dim strPathPdf As String = String.Empty
        Dim strDocumentoPdfBase64 As String = String.Empty

        AbrirDocumento(strNombrePdf, strPathPdf)

        With BEDatosFirma
            .filename = strNombrePdf
            .base64 = BEImplementacion.ConvertFileToBase64(strPathPdf)
            .passwordPdf = ""

            BEDatosCertificado = New BE.RequestDatosCertificado
            BEDatosCertificado.certificateLabel = "CERT_TEST_DIGICERT"
            BEDatosCertificado.keystorePin = "TestCert1234"
            BEDatosCertificado.reason = ""
            BEDatosCertificado.signatureName = ""
            BEDatosCertificado.location = ""
            .ColeccionDatosCertificado = New List(Of BE.RequestDatosCertificado)
            .ColeccionDatosCertificado.Add(BEDatosCertificado)

        End With
        BEResponseFirmaDigital = Task.Run(Function() BEImplementacion.PdfV1_AgregarFirmaDigital(BEDatosFirma)).Result

        If BEResponseFirmaDigital.status = 200 AndAlso BEResponseFirmaDigital.statusPhrase = "OK" Then
            MessageBox.Show("Firma OK")
        End If
        RichTextBox1.Text = JsonConvert.SerializeObject(BEResponseFirmaDigital, Formatting.Indented)
    End Sub

    Private Sub btnPdfV2_Click(sender As Object, e As EventArgs) Handles btnPdfV2.Click
        Dim BEImplementacion As New BS.Implementacion(BB.SistemaCliente.ESalud)
        Dim BEDatosFirma As New BE.RequestDatosFirma
        Dim BEDatosCertificado As BE.RequestDatosCertificado
        Dim BEResponseFirmaDigital As BE.ResponseFirmaDigital
        Dim strNombrePdf As String = String.Empty
        Dim strPathPdf As String = String.Empty
        Dim strDocumentoPdfBase64 As String = String.Empty

        AbrirDocumento(strNombrePdf, strPathPdf)

        With BEDatosFirma
            .filename = strNombrePdf
            .base64 = BEImplementacion.ConvertFileToBase64(strPathPdf)
            .passwordPdf = ""

            BEDatosCertificado = New BE.RequestDatosCertificado
            BEDatosCertificado.certificateLabel = "CERT_TEST_DIGICERT"
            BEDatosCertificado.keystorePin = "TestCert1234"
            BEDatosCertificado.reason = ""
            BEDatosCertificado.signatureName = ""
            BEDatosCertificado.location = ""
            .ColeccionDatosCertificado = New List(Of BE.RequestDatosCertificado)
            .ColeccionDatosCertificado.Add(BEDatosCertificado)

        End With
        BEResponseFirmaDigital = Task.Run(Function() BEImplementacion.PdfV2_AgregarFirmaDigital(BEDatosFirma)).Result

        If BEResponseFirmaDigital.status = 200 AndAlso BEResponseFirmaDigital.statusPhrase = "OK" Then

            For Each objDoc In BEResponseFirmaDigital.documentResponses
                If objDoc.base64 IsNot Nothing OrElse objDoc.base64 <> "" Then
                    Dim Base64Byte() As Byte = Convert.FromBase64String(objDoc.base64)
                    Dim obj As FileStream = File.Create("D:\Root.FirmaDigital.Cores\ESA\" + objDoc.filename)
                    obj.Write(Base64Byte, 0, Base64Byte.Length)
                    obj.Flush()
                    obj.Close()
                End If
            Next
            MessageBox.Show("Firma OK")
        End If
        RichTextBox1.Text = JsonConvert.SerializeObject(BEResponseFirmaDigital, Formatting.Indented)

    End Sub
End Class
