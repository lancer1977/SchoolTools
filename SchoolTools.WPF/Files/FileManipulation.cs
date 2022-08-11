using System.IO; 
using System.Windows; 
using PolyhydraGames.Core.Global;
using PolyhydraGames.Core.IOC;
using PolyhydraGames.SchoolTools.Core.Notes;
using PolyhydraGames.SchoolTools.Core.Services;

namespace PolyhydraGames.SchoolTools.WPF.Files
{
    public static class FileManipulation
    {
        public static void LoadFile( )
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
                LoadFile(dlg.FileName);

            }

        }

        public static void LoadFile(string name )
        { 
            if (!File.Exists(name)) return;
            var text = "";
            using (var io = new StreamReader(name))
                text = io.ReadToEnd();
            var service = IOC.Get<INoteService>();
            service.Load(NoteModel.CreateItemsFromJson(text));
            // Redraw();
            MessageBox.Show("Done loading file.");
        }

        public static void SaveFile(string name )
        {
            var service = IOC.Get<INoteService>();
            var data = service.Items.ToJson();
            using (var io = new StreamWriter(name, false))
            {
                io.Write(data);
            }
            MessageBox.Show("Done saving file.");
        }
        public static void SaveFile( )
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
                SaveFile(dlg.FileName);

            }

        }
    }
}
