using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PatientListApp1.Data
{
    public class Constants
    {
        public const string databaseFileName = "Patients.db3";

        public const SQLiteOpenFlags flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;

        public static string databasePath {
            get 
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, databaseFileName);
            }
        }
    }
}
