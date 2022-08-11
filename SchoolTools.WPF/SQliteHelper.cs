using System;
using System.IO;
using PolyhydraGames.SQLite.Interfaces;
using SQLite;

namespace SchoolTools.WPF
{
    public class SQliteHelper : ISQLiteFactory
    {
        public SQLiteConnection CreateReadonlyConnection(string filename)
        {
            string dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "database.db3");
            return new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadOnly);
        }

        public SQLiteConnection CreateConnection()
        {
            string dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "database.db3");
            return new SQLiteConnection(dbPath);
        }
    }
}