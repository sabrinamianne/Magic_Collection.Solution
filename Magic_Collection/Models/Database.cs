using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Magic_Collection;

namespace Magic_Collection.Models
{
    public class DB
    {
        public static MySqlConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            return conn;
        }
    }
}
