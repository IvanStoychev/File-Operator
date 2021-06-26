using System;

namespace Backend.Option_models
{
    /// <summary>
    /// Holds data concerning connecting to the database.
    /// </summary>
    class DatabaseParameters
    {
        private static readonly Lazy<DatabaseParameters> lazy = new Lazy<DatabaseParameters>(() => new DatabaseParameters());

        /// <summary>
        /// Reference to the singleton instance of this class.
        /// </summary>
        internal static DatabaseParameters instance => lazy.Value;

        /// <summary>
        /// The address of the machine, hosting the SQL Server.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// The name of the database to use.
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// The SQL username to authenticate as.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The password of the SQL username.
        /// </summary>
        public string Password { get; set; }

        private DatabaseParameters() { }
    }
}
