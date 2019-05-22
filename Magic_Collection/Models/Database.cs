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

            cmd.CommandText = @"SELECT COUNT(*) FROM cards;";
            int totalResults = int.Parse(cmd.ExecuteScalar().ToString());
            int totalPages = totalResults / limit;

            int start = (page-1) * limit;
                        
            cmd.CommandText = @"SELECT image_url FROM cards ORDER BY name ASC LIMIT "+start+", "+limit+";";
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
                //{"totalFound", totalResults},
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
                // {"totalFound", totalResults},
                {"totalPages", totalPages},
            };

            return results;
        }


        public static void AddToCollection(string url)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"INSERT INTO collection (image_url) VALUES (@cardUrl);";
            MySqlParameter cardUrl = new MySqlParameter("@cardUrl", url);
            cmd.Parameters.Add(cardUrl);

            cmd.ExecuteNonQuery();
        }


        public static List<string> GetAllCollectionCards()
        {
            List<string> collectionUrls = new List<string>{};

            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"SELECT image_url FROM collection";
            
            MySqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                string url = rdr.GetString(0);
                collectionUrls.Add(url);
            }
            
            conn.Close();
            if(conn!=null) conn.Dispose();


            return collectionUrls;
        }

        public static List<string> GetSetsList()
        {
            List<string> allSets = new List<string>{};

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT setName FROM cards GROUP BY setName;";

            MySqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                string set = rdr.GetString(0);
                allSets.Add(set);
            }

            conn.Close();
            if(conn!=null) conn.Dispose();

            return allSets;
        }


        public static List<string> GetSetImages(string setName)
        {
            List<string> allCardImagesInSet = new List<string>{};
            List<int> allIds = new List<int>{};

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT image_url, id FROM cards WHERE setName = @setNameSearch ORDER BY name;";

            MySqlParameter setNameSearch = new MySqlParameter("@setNameSearch", setName);
            cmd.Parameters.Add(setNameSearch);

            MySqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                if(!rdr.IsDBNull(0))
                {
                    string image_url = rdr.GetString(0);
                    int id = rdr.GetInt32(1);
                    allCardImagesInSet.Add(image_url);
                    allIds.Add(id);
                }
            }

            conn.Close();
            if(conn!=null) conn.Dispose();

            return allCardImagesInSet;
        }
    }
}
