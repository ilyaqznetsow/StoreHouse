using System;
using System.IO;
using Foundation;
using SQLite;
using StoreHouse.DAL;
using StoreHouse.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(DatabaseService))]

namespace StoreHouse.iOS
{
    public class DatabaseService : IDatabase
    {
        public DatabaseService()
        {
        }

        public SQLiteAsyncConnection CreateConnection()
        {
            var sqliteFilename = "StoreHouse.db";

            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            string path = Path.Combine(libFolder, sqliteFilename);

            // This is where we copy in the pre-created database
            if (!File.Exists(path))
            {
                var existingDb = NSBundle.MainBundle.PathForResource("StoreHouse", "db");
                File.Copy(existingDb, path);
            }

            var connection = new SQLiteAsyncConnection( path);

            // Return the database connection 
            return connection;
        }
    }
}
