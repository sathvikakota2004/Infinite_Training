﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace DataAccess
{
    public class DBHandler
    {
        private readonly string connectionString;

        public DBHandler()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
