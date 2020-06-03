Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Namespace Entidad
    Public Class ResponseFirmaDigital
        Public signedDocs As Integer = 0
        Public unsignedDocs As Integer = 0
        Public totalDocs As Integer = 0
        Public summary As String = String.Empty
        Public messages As List(Of String)
        Public code As Integer = 0
        Public status As Integer = 0
        Public statusPhrase As String = ""
        Public documentResponses As List(Of ResponseDocumento)
    End Class
End Namespace

