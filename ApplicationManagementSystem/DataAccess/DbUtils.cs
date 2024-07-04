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
        public static bool _isConnected;

        private static DbUtils? _instance = null;
        SqlConnection? _connection;

        
        public static DbUtils getInstance()
        {
            if (_instance == null)
            {
                _isConnected = true;
                _instance = new DbUtils(_user, _password);
            }
            return _instance;
        }

        private DbUtils(string username, string password)
        {
            _server = "THEDUYET";
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
            }
            catch (Exception ex)
            {

                _isConnected = false;
                MessageBox.Show($"Failed to connect to database! Reason: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            _instance = this;
        }


    //    private DbUtils()
    //    {
    //        _server = "THEDUYET";
    //        _databaseName = "QLTuyenDung";
    //        _user = "sa";
    //        _password = "Geborgenheit18";

    //        string connectionString = $"""
				//Server = {_server}; 
				//User ID = {_user}; 
				//Password={_password}; 
				//Database = {_databaseName}; 
				//TrustServerCertificate=True;
				//Connect Timeout=5
				//""";

    //        _connection = new SqlConnection(connectionString);

    //        try
    //        {
    //            _connection.Open();
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show($"Failed to connect to database! Reason: {ex.Message}",
    //                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    //        }

    //        _instance = this;
    //    }

        public SqlConnection? Connection
        {
            get
            {
                return _connection;
            }
        }
    }
}
