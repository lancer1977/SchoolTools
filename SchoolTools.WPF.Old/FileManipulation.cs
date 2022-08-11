using System;

using System.IO;

using System.Windows;


namespace PolyhydraGames.Code
{
    public static class FileManipulation
    {
        public static void LoadFile(NoteModelManager notes)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                FileName = "Document",
                DefaultExt = ".text",
                Filter = "Text documents (.txt)|*.txt"
            };

            // Show save file dialog box
            var result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                LoadFile(dlg.FileName, notes);

            }

        }

        public static void LoadFile(string name, NoteModelManager notes)
        {
            if (notes == null) throw new ArgumentNullException("notes");
            if (!File.Exists(name)) return;
            var text = "";
            using
                (var io = new StreamReader(name))
                text = io.ReadToEnd();

            notes.Load(NoteModel.CreateListFromJson(text));
            // Redraw();
            MessageBox.Show("Done loading file.");
        }

        public static void SaveFile(string name, NoteModelManager notes)
        {
            if (notes == null) throw new ArgumentNullException("notes");
            var data = NoteModel.SaveList(notes.List);
            using (var io = new StreamWriter(name, false))
            {
                io.Write(data);
            }
            MessageBox.Show("Done saving file.");
        }
        public static void SaveFile(NoteModelManager notes)
        {
            var dlg = new Microsoft.Win32.SaveFileDialog()
            {
                FileName = "Document",
                DefaultExt = ".text",
                Filter = "Text documents (.txt)|*.txt"
            };

            // Show save file dialog box
            var result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                SaveFile(dlg.FileName, notes);

            }

        }
    }
}
