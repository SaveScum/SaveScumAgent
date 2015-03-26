/*using System;
using System.Data.SQLite;

namespace AutoBackup.DatabaseTools
{
    internal class DataManager : IDisposable
    {
        private const string ConnectionString = "Data Source={0};Version=3;";
        private const int DatabaseSchemaVersion = 1;
        private static DataManager _instance;
        private SQLiteConnection _connection;
        private string _databaseFilename;

        private DataManager()
        {
        }

        public string DatabaseFilename
        {
            get { return _databaseFilename; }
            set
            {
                _databaseFilename = value;
                OpenConnection();
            }
        }

        public static DataManager Instance
        {
            get { return _instance ?? (_instance = new DataManager()); }
        }

        private void OpenConnection()
        {
            if (_connection != null) _connection.Shutdown();
            _connection = new SQLiteConnection(String.Format(ConnectionString, _databaseFilename));
            _connection.Open();
            ValidateSchemaVersion();
        }

        private void ValidateSchemaVersion()
        {
            var currentVersion = (int) GetSingleValue("PRAGMA user_version;");
            if (DatabaseSchemaVersion != currentVersion)
                throw new DatabaseVersionException(currentVersion, DatabaseSchemaVersion);
        }

        private Object GetSingleValue(string sql)
        {
            var cmd = new SQLiteCommand(sql, _connection);
            var reader = cmd.ExecuteReader();
            reader.Read();
            return reader.GetValue(0);
        }

        #region Idispose members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing != true) return;
            try
            {
                if (_connection != null) _connection.Close();
            }
            finally
            {
                if (_connection != null) _connection.Shutdown();
            }
        }

        #endregion
    }
}*/