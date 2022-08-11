using System;
using System.IO;
using PolyhydraGames.Core.Interfaces;

namespace PolyhydraGames.SchoolTools.WPF.Setup
{
    public class Storage : IStorageFolder
    {
        public string Get()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            var libraryPath = Path.Combine(documentsPath, "Library"); // Library folder
            return libraryPath;
        }
    }
}