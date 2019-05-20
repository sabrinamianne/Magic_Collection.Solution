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

        public static List<string> Search(string search, string column)
        {
            List<string> images = new List<string>{};

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT image_url FROM cards WHERE "+column+" LIKE '%"+search+"%';";

            Console.WriteLine(cmd.CommandText);
            MySqlParameter searchName = new MySqlParameter("@searchName", search);
            cmd.Parameters.Add(searchName);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                if(!rdr.IsDBNull(0))
                {
                    string imageUrl = rdr.GetString(0);
                    images.Add(imageUrl);
                }
            }

            conn.Close();
            if(conn!=null) conn.Dispose();

            return images;
        }
    }
}
