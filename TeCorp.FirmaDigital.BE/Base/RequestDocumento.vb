Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Namespace Base
    Public Class RequestDocumento
        Public base64 As String = String.Empty
        Public filename As String = String.Empty
        Public signatureMethod As String = String.Empty
        Public pdfPassword As String = String.Empty
        Public signatureDetails As RequestSignatureDetails = Nothing
        Public graphicSignature As RequestGraphicSignature = Nothing
    End Class
End Namespace

