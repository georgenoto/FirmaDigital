Imports System.Net.Http
Imports System.Threading.Tasks

Namespace Base
    Public Class FuncionesWebApi

        Public Shared Async Function GetRequest(serverUrl As String, requestUrl As String, Optional xmlOutputFormat As Boolean = False) As Task(Of String)
            Dim result As String = String.Empty
            Try
                Using client = New HttpClient()
                    client.BaseAddress = New Uri(serverUrl)
                    client.DefaultRequestHeaders.Accept.Clear()
                    If xmlOutputFormat Then
                        client.DefaultRequestHeaders.Accept.Add(New System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"))
                    Else
                        client.DefaultRequestHeaders.Accept.Add(New System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"))
                    End If
                    Dim response As System.Net.Http.HttpResponseMessage = Await client.GetAsync(requestUrl)
                    response.EnsureSuccessStatusCode()
                    result = response.Content.ReadAsStringAsync().Result
                End Using
            Catch ex As System.Net.Http.HttpRequestException
                'LogStatus(ex.Message.ToString)
                Throw
            End Try
            Return result
        End Function

        Public Shared Async Function PostRequest(ByVal serverUrl As String, ByVal requestUrl As String, ByVal json As String, Optional xmlOutputFormat As Boolean = False) As Task(Of String)
            Dim result As String = String.Empty
            Try
                Using client = New HttpClient()
                    client.BaseAddress = New Uri(serverUrl)
                    client.DefaultRequestHeaders.Accept.Clear()
                    If xmlOutputFormat Then
                        client.DefaultRequestHeaders.Accept.Add(New System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"))
                    Else
                        client.DefaultRequestHeaders.Accept.Add(New System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"))
                    End If

                    Dim response As System.Net.Http.HttpResponseMessage = Await client.PostAsync(requestUrl, New StringContent(json, System.Text.Encoding.UTF8, "application/json"))
                    response.EnsureSuccessStatusCode()
                    result = response.Content.ReadAsStringAsync().Result
                End Using
            Catch ex As System.Net.Http.HttpRequestException
                'LogStatus(ex.Message.ToString)
                Throw
            End Try
            Return result
        End Function

        Public Shared Async Function PostTokken(ByVal serverUrl As String, ByVal requestUrl As String, ByVal json As String, ByVal tokken As String, Optional xmlOutputFormat As Boolean = False) As Task(Of String)
            Dim result As String = String.Empty
            Try
                Using client = New HttpClient()
                    client.BaseAddress = New Uri(serverUrl)
                    client.DefaultRequestHeaders.Accept.Clear()
                    If xmlOutputFormat Then
                        client.DefaultRequestHeaders.Accept.Add(New System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"))
                    Else
                        client.DefaultRequestHeaders.Accept.Add(New System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"))
                    End If
                    client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", tokken)
                    Dim response As System.Net.Http.HttpResponseMessage = Await client.PostAsync(requestUrl, New StringContent(json, System.Text.Encoding.UTF8, "application/json"))
                    response.EnsureSuccessStatusCode()
                    result = response.Content.ReadAsStringAsync().Result
                End Using
            Catch ex As System.Net.Http.HttpRequestException
                'LogStatus(ex.Message.ToString)
                Throw
            End Try
            Return result
        End Function


    End Class
End Namespace

