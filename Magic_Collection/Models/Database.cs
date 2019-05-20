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

        public static Dictionary<string, object>Search(string search, string column, int page = 0, int limit = 50)
        {
            List<string> images = new List<string>{};

            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"SELECT COUNT(*) FROM cards WHERE "+column+" LIKE '%"+search+"%' LIMIT "+limit+";";
            int totalResults = int.Parse(cmd.ExecuteScalar().ToString());
            int totalPages = totalResults / 50/*resultsPerPage */;
                        
            //DEBUG
            // Console.WriteLine("totalResults: " + totalResults);
            // Console.WriteLine("totalPages: " + totalPages);

            cmd.CommandText = @"SELECT image_url FROM cards WHERE "+column+" LIKE '%"+search+"%';";
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

            Dictionary<string, object> results = new Dictionary<string, object>{
                {"images", images},
                {"totalFound", totalResults},
                {"totalPages", totalPages}
            };

            return results;
        }
    }
}
