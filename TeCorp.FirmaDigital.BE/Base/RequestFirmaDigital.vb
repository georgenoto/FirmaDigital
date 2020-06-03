Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Namespace Base
    Public Class RequestFirmaDigital
        Public user As RequestUser = Nothing
        Public keystorePin As String = String.Empty
        Public certificateLabel As String = String.Empty
        Public documents As List(Of RequestDocumento)
    End Class

End Namespace
