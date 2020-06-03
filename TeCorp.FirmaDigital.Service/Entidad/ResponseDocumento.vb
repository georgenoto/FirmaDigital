Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Namespace Entidad

    Public Class ResponseDocumento
        Public filename As String = String.Empty
        Public status As Boolean = False
        Public signatureMethod As String = String.Empty
        Public code As Integer = 0
        Public message As String = String.Empty
        Public base64 As String = String.Empty

    End Class

End Namespace


