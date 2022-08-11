Public Class Treeview
     Inherits Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.NodeTree = New System.Windows.Forms.TreeView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RemoveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DefinitionTitle = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.DefinitionLabel = New System.Windows.Forms.Label()

        Me.TextBox3 = New System.Windows.Forms.TextBox()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MyTab
        '
   
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.NodeTree)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DefinitionTitle)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBox2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DefinitionLabel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBox3)
        Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer1.Size = New System.Drawing.Size(284, 262)
        Me.SplitContainer1.SplitterDistance = 94
        Me.SplitContainer1.TabIndex = 0
        '
        'NodeTree
        '
        Me.NodeTree.ContextMenuStrip = Me.ContextMenuStrip1
        Me.NodeTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NodeTree.Location = New System.Drawing.Point(0, 0)
        Me.NodeTree.Name = "NodeTree"
        Me.NodeTree.Size = New System.Drawing.Size(94, 262)
        Me.NodeTree.TabIndex = 1
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemoveToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(118, 26)
        '
        'RemoveToolStripMenuItem
        '
        Me.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem"
        Me.RemoveToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.RemoveToolStripMenuItem.Text = "Remove"
        '
        'DefinitionTitle
        '
        Me.DefinitionTitle.AutoSize = True
        Me.DefinitionTitle.Location = New System.Drawing.Point(9, 5)
        Me.DefinitionTitle.Name = "DefinitionTitle"
        Me.DefinitionTitle.Size = New System.Drawing.Size(27, 13)
        Me.DefinitionTitle.TabIndex = 5
        Me.DefinitionTitle.Text = "Title"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(6, 18)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(141, 20)
        Me.TextBox2.TabIndex = 4
        '
        'DefinitionLabel
        '
        Me.DefinitionLabel.AutoSize = True
        Me.DefinitionLabel.Location = New System.Drawing.Point(9, 41)
        Me.DefinitionLabel.Name = "DefinitionLabel"
        Me.DefinitionLabel.Size = New System.Drawing.Size(51, 13)
        Me.DefinitionLabel.TabIndex = 7
        Me.DefinitionLabel.Text = "Definition"
        '
        'TextBox1
        '

        '
        'TextBox3
        '
        Me.TextBox3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TextBox3.Location = New System.Drawing.Point(3, 57)
        Me.TextBox3.Multiline = True
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(180, 202)
        Me.TextBox3.TabIndex = 6
        '
        'Treeview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Treeview"
        Me.Text = "Treeview"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents NodeTree As System.Windows.Forms.TreeView
    Friend Shadows WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RemoveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

    Friend WithEvents DefinitionTitle As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents DefinitionLabel As System.Windows.Forms.Label
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox



    Private Sub Treeview_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub Redraw()
        NodeTree.Nodes.Clear()
        For Each node In myList.DefinitionList
            NodeTree.Nodes.Add(node.Title)

        Next
        UpdateTextBox
    End Sub

    Private Sub NodeTree_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles NodeTree.AfterSelect
        UpdateTextBox()
    End Sub
    Private Sub UpdateTextBox()
        If NodeTree.SelectedNode IsNot Nothing Then
            SelectedIndex = NodeTree.SelectedNode.Index

            If NodeTree.SelectedNode.Index < myList.DefinitionList.Count Then
                currentCard = myList.DefinitionList(NodeTree.SelectedNode.Index)
                TextBox2.Text = myList.DefinitionList(NodeTree.SelectedNode.Index).Title
                TextBox3.Text = myList.DefinitionList(NodeTree.SelectedNode.Index).Definition
            Else
                currentCard = Nothing
                TextBox2.Text = ""
                TextBox3.Text = ""
            End If
        Else
            SelectedIndex = -1
            currentCard = Nothing
            TextBox2.Text = ""
            TextBox3.Text = ""
        End If

    End Sub

    Private Sub RemoveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveToolStripMenuItem.Click
        If ValidNode() Then
            If MsgBox("Really remove " & myList.DefinitionList(NodeTree.SelectedNode.Index).Title, vbYesNo) = vbYes Then
                myList.Remove(NodeTree.SelectedNode.Index)

            End If

        End If
  
    End Sub
    Public Function ValidNode() As Boolean
        If NodeTree.SelectedNode IsNot Nothing AndAlso NodeTree.SelectedNode.Index < myList.DefinitionList.Count Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub SplitContainer1_Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer1.Panel2.Paint

    End Sub
    '  Private CurrentIndex As Integer = -1

    Private Sub myDefinition_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        If currentCard IsNot Nothing Then
            currentCard.Title = TextBox2.Text
            Try
                If SelectedIndex >= 0 Then NodeTree.Nodes(SelectedIndex).Text = TextBox2.Text
            Catch ex As Exception

            End Try

        End If





    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        If currentCard IsNot Nothing Then currentCard.Definition = TextBox3.Text

    End Sub
    Dim SelectedIndex As Integer = -1
    Dim currentCard As Definitions

End Class