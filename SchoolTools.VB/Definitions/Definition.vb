Public Class Definitions
    Public Sub New()
        _title = InputBox("Term name?")
        _definition = InputBox(_title & " definition?")
    End Sub
    Public Sub New(ByVal title As String, ByVal definition As String)
        _title = title
        _definition = definition
    End Sub

    Public Shared Function AddDefinitionFromSavedString(ByVal item As String) As Definitions
        Dim definitionCode() As String = item.Split("@")
        Dim definition As New Definitions(definitionCode(0), definitionCode(1).Replace("<NEWLINE>", vbNewLine))
        Return definition

    End Function
    Public Sub Flashcard()
        MsgBox("What is the definition of " & _title, , _title)
        MsgBox(_definition, , _title)
    End Sub
    Public Function SaveToString()
        Return (_title & "@" & _definition).Replace(vbNewLine, "<NEWLINE>")

    End Function

    Public Property Title() As String
        Get
            Return _title
        End Get
        Set(ByVal value As String)
            _title = value
        End Set
    End Property

    Public Property Definition() As String
        Get
            Return _definition
        End Get
        Set(ByVal value As String)
            _definition = value
        End Set
    End Property


    Private _title As String
    Private _definition As String
End Class
