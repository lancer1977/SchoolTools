Public Class DefinitionList
    Public Sub New()

    End Sub
    Public Sub Remove(ByVal definitionIndex As Integer)
        _definitionList.RemoveAt(definitionIndex)
       

    End Sub
    Public Sub Add(ByVal definition As Definitions)
        _definitionList.Add(definition)
 

    End Sub
    Public Sub AddDefinition()
        Dim newDef As New Definitions
        Add(newDef)

    End Sub
    Public Sub LoadList(ByRef sender As Form, Optional ByVal silent As Boolean = False)
        Dim filename As String = Application.LocalUserAppDataPath & ".\DB.txt"

        If System.IO.File.Exists(filename) = True Then

            sender.Cursor = Cursors.WaitCursor
            Dim objReader As New System.IO.StreamReader(filename)
            For Each item In objReader.ReadToEnd.Split(Chr(10))
                If item <> "" And InStr(item, "@") Then
                    Add(Definitions.AddDefinitionFromSavedString(item))
                End If
            Next
            objReader.Close()
            If silent = False Then MsgBox("Text ReadFrom File")
            sender.Cursor = Cursors.Default
        Else
            If silent = False Then MsgBox("File does not exist")
        End If
    End Sub

    Public Sub RandomFlashCard()
        Dim rand As New Random
        Dim cardNumber As Integer
        If _definitionList.Count > 0 Then
            cardNumber = (rand.Next(_definitionList.Count))
            _definitionList(cardNumber).Flashcard()

        End If
    End Sub
    Public Property DefinitionList() As List(Of Definitions)
        Get
            Return _definitionList
        End Get
        Set(ByVal value As List(Of Definitions))
            _definitionList = value
        End Set
    End Property

    Public Sub SaveList(Optional ByVal silent As Boolean = False)
        Dim strTemp As String = ""
        Dim filename As String = Application.LocalUserAppDataPath & ".\DB.txt"
        Dim objWriter As New System.IO.StreamWriter(filename, False)
        Dim stringout As String = ""

        For Each item In _definitionList
            Try
                Dim temp As String = item.SaveToString()
                stringout &= temp & vbNewLine
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        Next
        objWriter.Write(stringout)
        objWriter.Close()
        If silent = False Then MsgBox("Campaign saved")
    End Sub


    Private _definitionList As New List(Of Definitions)
End Class
