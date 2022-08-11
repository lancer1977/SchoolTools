'Imports Microsoft.Office.Interop

'Public Class SpellParse
'    ReadOnly _myExcel As Excel.Application = New Excel.Application
'    ReadOnly _w As Excel.Workbook = _myExcel.Workbooks.Open("C:\Spells\spell_full.xlsx")

'    Private Sub SpellParse_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load

'    End Sub

'    Public Function ConvTime(ByVal code As String) As String
'        Select Case code
'            Case "1 standard action" : Return 10
'            Case Else : Return " "
'        End Select
'    End Function
'    Public Function ConvRange(ByVal code As String) As String
'        If InStr(code, "long") Then Return "Long"
'        If InStr(code, "medium") Then Return "Medium"
'        If InStr(code, "close") Then Return "Close"
'        Return StrConv(code, vbProperCase)
'    End Function
'End Class