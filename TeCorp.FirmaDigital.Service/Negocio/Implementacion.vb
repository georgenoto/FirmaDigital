Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports BS = TeCorp.FirmaDigital.DAL.Base.FuncionesWebApi
Imports BEnum = TeCorp.FirmaDigital.Service.Base
Imports BE = TeCorp.FirmaDigital.BE.Base

Imports BA = TeCorp.FirmaDigital.Service


Namespace Negocio
    Public Class Implementacion



#Region "Declaracion de variables"
        Private _ServidorWebApi As String
        Private _PDFV1 As String
        Private _PDFV2 As String

        Private _PathClienteSistema As String
        Private _UserClienteSistema As String
        Private _PasswordClienteSistema As String

        Private _RectangleWith As Integer
        Private _RectangleHeight As Integer
        Private _RectangleX As Integer
        Private _RectangleY As Integer
        Private objJSON As JObject

#End Region

#Region "Declaracion de propiedades"
        Private Property ServidorWebApi() As String
            Get
                Return _ServidorWebApi
            End Get
            Set(ByVal value As String)
                _ServidorWebApi = value
            End Set
        End Property

        Private Property PDFV1_URL() As String
            Get
                Return _PDFV1
            End Get
            Set(ByVal value As String)
                _PDFV1 = value
            End Set
        End Property

        Private Property PDFV2_URL() As String
            Get
                Return _PDFV2
            End Get
            Set(ByVal value As String)
                _PDFV2 = value
            End Set
        End Property

        Private Property PathClienteSistema() As String
            Get
                Return _PathClienteSistema
            End Get
            Set(ByVal value As String)
                _PathClienteSistema = value
            End Set
        End Property

        Private Property UserClienteSistema() As String
            Get
                Return _UserClienteSistema
            End Get
            Set(ByVal value As String)
                _UserClienteSistema = value
            End Set
        End Property

        Private Property PasswordClienteSistema() As String
            Get
                Return _PasswordClienteSistema
            End Get
            Set(ByVal value As String)
                _PasswordClienteSistema = value
            End Set
        End Property



        Private Property Rectangle_With() As Integer
            Get
                Return _RectangleWith
            End Get
            Set(ByVal value As Integer)
                _RectangleWith = value
            End Set
        End Property
        Private Property Rectangle_Height() As Integer
            Get
                Return _RectangleHeight
            End Get
            Set(ByVal value As Integer)
                _RectangleHeight = value
            End Set
        End Property
        Private Property Rectangle_X() As Integer
            Get
                Return _RectangleX
            End Get
            Set(ByVal value As Integer)
                _RectangleX = value
            End Set
        End Property
        Private Property Rectangle_Y() As Integer
            Get
                Return _RectangleY
            End Get
            Set(ByVal value As Integer)
                _RectangleY = value
            End Set
        End Property

#End Region

#Region "Metodos para leer el archivo de configuración Json"
        Private Function getValor(ByVal key As String) As String
            Return objJSON("ConfiguracionFirmaDigital").Item(key).ToString()
        End Function
#End Region

#Region "Metodos para firma digital "

        ''' <summary>
        ''' Firma un archivo PDF lo guarda en la ruta que se especifica y devuelve el archivo firmado
        ''' </summary>
        ''' <param name="BEDatosFirmaDigital"></param>
        ''' <returns></returns>
        Public Async Function PdfV1_AgregarFirmaDigital(ByVal BEDatosFirmaDigital As BA.Entidad.RequestDatosFirma) As Task(Of BA.Entidad.ResponseFirmaDigital)
            Dim BEResponseFirmaDigital As New BA.Entidad.ResponseFirmaDigital
            Dim BEFirmaDigital As New BE.Base.RequestFirmaDigital
            Dim strRequestJson As String = String.Empty
            Dim strResponseJson As String = String.Empty

            Dim strNombrePdf As String = String.Empty
            Dim strPdfBase64 As String = String.Empty
            Dim intValorX As Integer = 0
            Dim strMensajeValidacion As String = String.Empty
            Try

                If ValidarObjetoFirma(BEDatosFirmaDigital, strMensajeValidacion) Then
                    strNombrePdf = BEDatosFirmaDigital.filename
                    strPdfBase64 = BEDatosFirmaDigital.base64
                    intValorX = Rectangle_X

                    For Each objDetalleCertificado In BEDatosFirmaDigital.ColeccionDatosCertificado
                        BEFirmaDigital = CargarObjeto_RequestFirmaDigital(strNombrePdf, strPdfBase64, BEDatosFirmaDigital.passwordPdf, objDetalleCertificado, "PADES", intValorX)
                        AgregarPath(BEFirmaDigital)
                        strRequestJson = JsonConvert.SerializeObject(BEFirmaDigital, Formatting.Indented)
                        strResponseJson = Await BS.PostRequest(ServidorWebApi, PDFV1_URL, strRequestJson)
                        BEResponseFirmaDigital = JsonConvert.DeserializeObject(Of BA.Entidad.ResponseFirmaDigital)(strResponseJson)

                        If BEResponseFirmaDigital.status = 200 AndAlso BEResponseFirmaDigital.statusPhrase = "OK" Then
                            For Each objDocumento In BEResponseFirmaDigital.documentResponses
                                strPdfBase64 = objDocumento.base64
                            Next
                        End If
                        intValorX += Rectangle_With
                    Next

                Else
                    Throw New Exception(strMensajeValidacion)
                End If


            Catch ex As Exception
                BEResponseFirmaDigital.signedDocs = 0
                BEResponseFirmaDigital.unsignedDocs = 0
                BEResponseFirmaDigital.totalDocs = 0
                BEResponseFirmaDigital.summary = ""
                BEResponseFirmaDigital.messages = New List(Of String)
                BEResponseFirmaDigital.messages.Add(ex.Message)
                BEResponseFirmaDigital.code = 0
                BEResponseFirmaDigital.status = 400
                BEResponseFirmaDigital.statusPhrase = "BAD REQUEST"
                Return BEResponseFirmaDigital
            End Try

            Return BEResponseFirmaDigital
        End Function

        ''' <summary>
        ''' Firma un archivo PDF lo guarda y devuelve el archivo firmado
        ''' </summary>
        ''' <param name="BEDatosFirmaDigital"></param>
        ''' <returns></returns>
        Public Async Function PdfV2_AgregarFirmaDigital(ByVal BEDatosFirmaDigital As BA.Entidad.RequestDatosFirma) As Task(Of BA.Entidad.ResponseFirmaDigital)
            Dim BEResponseFirmaDigital As New BA.Entidad.ResponseFirmaDigital
            Dim BEFirmaDigital As New BE.Base.RequestFirmaDigital
            Dim strRequestJson As String = String.Empty
            Dim strResponseJson As String = String.Empty

            Dim strNombrePdf As String = String.Empty
            Dim strPdfBase64 As String = String.Empty
            Dim intValorX As Integer = 0
            Dim strMensajeValidacion As String = String.Empty
            Try

                If ValidarObjetoFirma(BEDatosFirmaDigital, strMensajeValidacion) Then
                    strNombrePdf = BEDatosFirmaDigital.filename
                    strPdfBase64 = BEDatosFirmaDigital.base64
                    intValorX = Rectangle_X

                    For Each objDetalleCertificado In BEDatosFirmaDigital.ColeccionDatosCertificado
                        BEFirmaDigital = CargarObjeto_RequestFirmaDigital(strNombrePdf, strPdfBase64, BEDatosFirmaDigital.passwordPdf, objDetalleCertificado, "PADES", intValorX)
                        strRequestJson = JsonConvert.SerializeObject(BEFirmaDigital, Formatting.Indented)
                        strResponseJson = Await BS.PostRequest(ServidorWebApi, PDFV1_URL, strRequestJson)
                        BEResponseFirmaDigital = JsonConvert.DeserializeObject(Of BA.Entidad.ResponseFirmaDigital)(strResponseJson)

                        If BEResponseFirmaDigital.status = 200 AndAlso BEResponseFirmaDigital.statusPhrase = "OK" Then
                            For Each objDocumento In BEResponseFirmaDigital.documentResponses
                                strPdfBase64 = objDocumento.base64
                            Next
                        End If
                        intValorX += Rectangle_With
                    Next

                Else
                    Throw New Exception(strMensajeValidacion)
                End If


            Catch ex As Exception
                BEResponseFirmaDigital.signedDocs = 0
                BEResponseFirmaDigital.unsignedDocs = 0
                BEResponseFirmaDigital.totalDocs = 0
                BEResponseFirmaDigital.summary = ""
                BEResponseFirmaDigital.messages = New List(Of String)
                BEResponseFirmaDigital.messages.Add(ex.Message)
                BEResponseFirmaDigital.code = 0
                BEResponseFirmaDigital.status = 400
                BEResponseFirmaDigital.statusPhrase = "BAD REQUEST"
                Return BEResponseFirmaDigital
            End Try

            Return BEResponseFirmaDigital
        End Function


        Public Function ConvertFileToBase64(ByVal fileName As String) As String
            Dim strResultado As String = String.Empty
            Try
                strResultado = Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName))
            Catch ex As Exception
                Return strResultado
            End Try
            Return strResultado
        End Function

#End Region

#Region "Metodos Auxiliarres"

        Private Function CargarObjeto_RequestFirmaDigital(ByVal strNombrePdf As String, ByVal strPdfBase64 As String, ByVal strPasswordPdf As String, ByVal objDetalleCertificado As BA.Entidad.RequestDatosCertificado, ByVal strSignatureMethod As String, ByVal ValorX As Integer) As BE.Base.RequestFirmaDigital
            Dim BEFirmaDigital As New BE.Base.RequestFirmaDigital
            Dim BEDocumento As BE.Base.RequestDocumento
            Dim BEResponseFirmaDigital As New BA.Entidad.ResponseFirmaDigital

            Try
                With BEFirmaDigital
                    .user = New BE.Base.RequestUser
                    .user.username = UserClienteSistema
                    .user.password = PasswordClienteSistema
                    .keystorePin = objDetalleCertificado.keystorePin
                    .certificateLabel = objDetalleCertificado.certificateLabel

                    .documents = New List(Of BE.Base.RequestDocumento)

                    BEDocumento = New BE.Base.RequestDocumento
                    BEDocumento.filename = strNombrePdf
                    BEDocumento.signatureMethod = strSignatureMethod
                    BEDocumento.pdfPassword = strPasswordPdf

                    BEDocumento.signatureDetails = New BE.Base.RequestSignatureDetails
                    BEDocumento.signatureDetails.signatureName = objDetalleCertificado.signatureName
                    BEDocumento.signatureDetails.reason = objDetalleCertificado.reason
                    BEDocumento.signatureDetails.location = objDetalleCertificado.location

                    BEDocumento.graphicSignature = New BE.Base.RequestGraphicSignature
                    BEDocumento.graphicSignature.pageIndex = 1

                    BEDocumento.graphicSignature.rectangle = New BE.Base.RequestRectangle
                    BEDocumento.graphicSignature.rectangle.x = ValorX
                    BEDocumento.graphicSignature.rectangle.y = Rectangle_Y
                    BEDocumento.graphicSignature.rectangle.width = Rectangle_With
                    BEDocumento.graphicSignature.rectangle.height = Rectangle_Height

                    BEDocumento.base64 = strPdfBase64
                    .documents.Add(BEDocumento)

                End With
            Catch ex As Exception
                Return Nothing
            End Try
            Return BEFirmaDigital
        End Function

        Private Function ValidarObjetoFirma(ByRef BEDatosFirmaDigital As BA.Entidad.RequestDatosFirma, ByRef strMensaje As String) As Boolean
            Dim bolResultado As Boolean = True
            Try
                If BEDatosFirmaDigital IsNot Nothing Then
                    With BEDatosFirmaDigital
                        If .filename Is Nothing OrElse .filename = "" Then
                            bolResultado = False
                            strMensaje = "El campo 'filename' es obligatorio."
                        End If
                        If .base64 Is Nothing OrElse .base64 = "" Then
                            bolResultado = False
                            strMensaje = "El campo 'base64' es oblogatorio."
                        End If
                        If .passwordPdf Is Nothing Then
                            .passwordPdf = ""
                        End If

                        If .ColeccionDatosCertificado IsNot Nothing OrElse .ColeccionDatosCertificado.Count > 0 Then
                            For Each objCertificado In .ColeccionDatosCertificado
                                If objCertificado.certificateLabel Is Nothing OrElse objCertificado.certificateLabel = "" Then
                                    bolResultado = False
                                    strMensaje = "El campo 'certificateLabel' del detalle del Certificado es obligatorio."
                                End If
                                If objCertificado.keystorePin Is Nothing OrElse objCertificado.keystorePin = "" Then
                                    bolResultado = False
                                    strMensaje = "El campo 'keystorePin' del detalle del Certificado es obligatorio."
                                End If

                                If objCertificado.signatureName Is Nothing Then
                                    objCertificado.signatureName = ""
                                End If
                                If objCertificado.reason Is Nothing Then
                                    objCertificado.reason = ""
                                End If
                                If objCertificado.location Is Nothing Then
                                    objCertificado.location = ""
                                End If

                            Next
                        Else
                            bolResultado = False
                            strMensaje = "Es necesario que la colección de datos de certificados contengan algún valor."
                        End If
                    End With


                Else
                    strMensaje = "El objeto 'RequestDatosFirma' no puede estar vacio, por favor cargue los datos necesarios"
                End If
            Catch ex As Exception
                Return False
            End Try
            Return bolResultado
        End Function

        Private Sub AgregarPath(ByRef objFirmaDigital As BE.Base.RequestFirmaDigital)
            Dim strRutaCompleta As String = String.Empty
            Dim strPath As String = String.Empty
            Try
                If objFirmaDigital IsNot Nothing Then
                    For Each objDoc In objFirmaDigital.documents
                        strRutaCompleta = PathClienteSistema + objDoc.filename
                        objDoc.filename = strRutaCompleta
                    Next
                End If
            Catch ex As Exception

            End Try
        End Sub


#End Region

#Region "Constructor"
        Sub New(ByVal SistemaCliente As BEnum.Enumerador.SistemaCliente)

            objJSON = JObject.Parse(Encoding.UTF8.GetString(My.Resources.ConfiguracionWebApi))
            ServidorWebApi = getValor("Server_URL")
            PDFV1_URL = getValor("PdfV1_URL")
            PDFV2_URL = getValor("PdfV2_URL")


            Select Case SistemaCliente
                Case BEnum.Enumerador.SistemaCliente.Elife2
                    UserClienteSistema = getValor("User_EL2")
                    PasswordClienteSistema = getValor("Password_EL2")
                    PathClienteSistema = getValor("Path_EL2")
                Case BEnum.Enumerador.SistemaCliente.Eproperty2
                    UserClienteSistema = getValor("User_EP2")
                    PasswordClienteSistema = getValor("Password_EP2")
                    PathClienteSistema = getValor("Path_EP2")
                Case BEnum.Enumerador.SistemaCliente.ESalud
                    UserClienteSistema = getValor("User_ESA")
                    PasswordClienteSistema = getValor("Password_ESA")
                    PathClienteSistema = getValor("Path_ESA")
            End Select

            Rectangle_With = getValor("Rectangle_With")
            Rectangle_Height = getValor("Rectangle_Height")
            Rectangle_X = getValor("Rectangle_X")
            Rectangle_Y = getValor("Rectangle_Y")

        End Sub
#End Region


    End Class



End Namespace

