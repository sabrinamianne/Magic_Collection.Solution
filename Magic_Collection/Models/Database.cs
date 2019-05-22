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

        public static Dictionary<string, object>AllCards(int page = 1, int limit = 50)
        {
            List<string> images = new List<string>{};

            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();


            //Dont need this every search?
            cmd.CommandText = @"SELECT COUNT(*) FROM cards;";
            int totalResults = int.Parse(cmd.ExecuteScalar().ToString());
            int totalPages = totalResults / limit;

            int start = (page-1) * limit;
            int end = start + limit;
                        
            //DEBUG
            // Console.WriteLine("totalResults: " + totalResults);
            // Console.WriteLine("totalPages: " + totalPages);

            cmd.CommandText = @"SELECT image_url FROM cards;";
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
                {"totalPages", totalPages},
            };


            return results;
        }

        public static Dictionary<string, object>Search(string search, string column, int page = 1, int limit = 50)
        {
            List<string> images = new List<string>{};

            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();


            //Dont need this every search?
            cmd.CommandText = @"SELECT COUNT(*) FROM cards WHERE "+column+" LIKE '%"+search+"%';";
            int totalResults = int.Parse(cmd.ExecuteScalar().ToString());
            int totalPages = totalResults / limit;

            int start = (page-1) * limit;
            int end = start + limit;
                        
            //DEBUG
            // Console.WriteLine("totalResults: " + totalResults);
            // Console.WriteLine("totalPages: " + totalPages);

            cmd.CommandText = @"SELECT image_url FROM cards WHERE "+column+" LIKE '%"+search+"%' ORDER BY name ASC LIMIT "+start+", "+limit+";";
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
                {"totalPages", totalPages},
            };

            return results;
        }
    }
}
