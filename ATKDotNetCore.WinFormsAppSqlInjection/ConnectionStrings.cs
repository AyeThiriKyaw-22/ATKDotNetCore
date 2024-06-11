﻿using System.Data.SqlClient;

namespace ATKDotNetCore.WinFormsAppSqlInjection
{
    internal static class ConnectionStrings
    {
        public static SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "ATKDotNetCore",
            UserID = "sa",
            Password = "sa@123",
            TrustServerCertificate = true
        };
    }
}