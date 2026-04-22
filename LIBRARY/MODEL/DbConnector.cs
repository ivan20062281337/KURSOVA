
namespace LIBRARY.MODEL
{
    public static class DbConnector
    {
        private static string _connectionString;

        public static void Initialize(string user, string password)
        {
            _connectionString = $"Server=localhost;Database=library_system;Uid={user};Pwd={password};AllowPublicKeyRetrieval=True;SslMode=Preferred;";
        }

        public static string GetConnectionString() => _connectionString;
    }
}
