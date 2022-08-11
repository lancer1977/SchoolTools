Imports System.Threading.Tasks

Public Enum SchoolToolsState
    Waiting
    TextAvailible
End Enum
Public Class SchoolTools
    Private MyState As SchoolToolsState

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        outputTextbox.Text = TextBox1.Text.Replace(vbNewLine, toTextbox.Text)

        Clipboard.SetText(outputTextbox.Text)
    End Sub

    


    Private Sub Button24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button24.Click
        TextBox1.Text = ""
        outputTextbox.Text = ""
    End Sub

   
    Public Sub StringFormatArray()
        Dim list As New List(Of String)
        For Each item In TextBox1.Text.Split(Environment.NewLine)
            If item = "" Or item = " " Then
                Continue For
            End If


            Dim splitItem = item.Split(",")


            outputTextbox.Text &= _
                     String.Format(txtStringFormat.Text, splitItem) & vbNewLine

        Next
    End Sub
    Public Sub StringFormatSingle()
        Dim list As New List(Of String)
        For Each item In TextBox1.Text.Split(Environment.NewLine)
            item = item.Replace(Chr(10), "")
            item = item.Replace(Chr(13), "")
            item = item.Trim

            outputTextbox.Text &= _
                     String.Format(txtStringFormat.Text, item) & vbNewLine

        Next
    End Sub


    Private Sub Button27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button27.Click
        My.Computer.Clipboard.SetText(outputTextbox.Text)

    End Sub

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        Dim file As New OpenFileDialog
        file.ShowDialog()
        If System.IO.File.Exists(file.FileName) Then
            Dim fileName As String = file.FileName
            TextBox1.Text = System.IO.File.ReadAllText(fileName)

        End If
    End Sub


    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

        Dim setText = New Task(Sub()
                                   For x As Integer = 1 To 100000

                                   Next
                                   'Clipboard.SetText(_outputTextbox.Text)
                               End Sub)
        If TextBox1.Text <> "" Then
            _outputTextbox.Text = TextBox1.Text.Replace(vbNewLine, " ").Replace("■", "")
            ' setText.Start()
            Speak(_outputTextbox.Text)
            MyState = SchoolToolsState.TextAvailible
        End If

    End Sub

    Private Sub SchoolTools_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If MyState = SchoolToolsState.TextAvailible Then
            Clipboard.SetText(_outputTextbox.Text)
            TextBox1.Clear()
            MyState = SchoolToolsState.Waiting
        End If

    End Sub
End Class