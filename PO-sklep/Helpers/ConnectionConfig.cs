using System;

namespace PO_sklep.Helpers
{
    public class ConnectionConfig
    {
        public ConnectionConfig(string connectionString)
        {
            this.ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public string ConnectionString { get; }
    }
}
