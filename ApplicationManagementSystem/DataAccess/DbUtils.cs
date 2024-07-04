using Microsoft.Data.SqlClient;
using System.Windows;

namespace ApplicationManagementSystem.DataAccess
{
    public class DbUtils
    {
        private string _server;
        private string _databaseName;
        private string _user;
        private string _password;

        private static DbUtils? _instance = null;
        SqlConnection? _connection;

        public static DbUtils getInstance()
        {
            if (_instance == null)
            {
                _instance = new DbUtils();
            }
            return _instance;
        }

        private DbUtils()
        {
            _server = "MSI";
            _databaseName = "QLTuyenDung";
            _user = "sa";
            _password = "nvnh180803";

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
                MessageBox.Show($"Failed to connect to database! Reason: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            _instance = this;
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
