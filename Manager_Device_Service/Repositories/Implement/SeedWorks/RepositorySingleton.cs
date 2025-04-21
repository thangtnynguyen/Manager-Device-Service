using Microsoft.Data.SqlClient;
using System.Data;


namespace Manager_Device_Service.Repositories.Interface.ISeedWorks
{
    public class RepositorySingleton
    {

        private static string _connectionString = "Data Source=MSI\\SQLEXPRESS01;Initial Catalog=Manager-Device;Integrated Security=True;MultipleActiveResultSets=true;TrustServerCertificate=True;";
        private static readonly object s_lock = new object();
        private static volatile RepositorySingleton? instance;
        private static SqlConnection connection;
        public static RepositorySingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (s_lock)
                    {
                        if (instance == null)
                        {
                            instance = new RepositorySingleton();
                        }
                        return instance;
                    }
                }
                return instance;
            }
            private set { instance = value; }
        }

        private RepositorySingleton()
        {
            connection = new SqlConnection(_connectionString);
        }
        private void Open()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        private void Close()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public async Task<DataTable> ExecuteProcedureAsync(string procedure, Dictionary<string, object> parameters = null)
        {
            DataTable dt = new DataTable();
            Open();
            SqlCommand cmd = new SqlCommand(procedure, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (var parameter in parameters)
            {
                cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
            }
            SqlDataReader record = await cmd.ExecuteReaderAsync();
            dt.Load(record);
            Close();
            return dt;
        }
        public async Task<DataTable> ExecuteQueryAsync(string query)
        {
            DataTable dt = new DataTable();
            Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.CommandType = CommandType.Text;
            SqlDataReader record = await cmd.ExecuteReaderAsync();
            dt.Load(record);
            Close();
            return dt;
        }
        public Task<object> ExecuteScalarAsync(string query)
        {
            Task<object> result;
            Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.CommandType = CommandType.Text;
#pragma warning disable CS8619
            result = cmd.ExecuteScalarAsync();
#pragma warning restore CS8619
            Close();
            return result;
        }
    }
}
