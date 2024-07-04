using Microsoft.Data.SqlClient;
using System.Windows;

namespace ApplicationManagementSystem.DataAccess
{
    public class DbUtils
    {
        private string _server;
        private string _databaseName;
        public static string _user;
        public static string _password;
        public static bool _isConnected = false;

        private static DbUtils? _instance = null;
        SqlConnection? _connection;


        public static DbUtils getInstance()
        {
            if (_instance == null || !_isConnected)
            {
                _instance = new DbUtils(_user, _password);
            }
            return _instance;
        }

        public static void closeConnection()
        {
            if (_instance != null)
            {
                _instance.Connection.Close();
            }
            _instance = null;
        }

        private DbUtils(string username, string password)
        {
            _server = "MSI";
            _databaseName = "QLTuyenDung";
            _user = username;
            _password = password;

            string connectionString = $"""
            Server = {_server}; 
            User ID = {_user}; 
            Password={_password}; 
            Database = {_databaseName}; 
            TrustServerCertificate=True;
            Connect Timeout=5
            """;

            _connection = new SqlConnection(connectionString);

            try
            {
                _connection.Open();
                _isConnected = true;
            }
            catch (Exception ex)
            {
                _isConnected = false;
                MessageBox.Show("Failed to connect to database!",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (_isConnected)
            {
                _instance = this;
            }
            else
            {
                closeConnection();
            }          
        }
        public SqlConnection? Connection
        {
            get
            {
                return _connection;
            }
        }
    }
}
