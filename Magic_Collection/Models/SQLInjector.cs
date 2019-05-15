using Microsoft.AspNetCore.Mvc;
using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.Service;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;


namespace Magic_Collection.Models
{
    public class SQLInjector
    {
        static int pageNum = 1;

        public static void GetImageUrls()
        {
            CardService service = new CardService();
            var result = service.Where(x => x.Page, pageNum)
                                .Where(x => x.PageSize, 50)
                                .All();

            Console.WriteLine("Page: " + pageNum + " of " + result.PagingInfo.TotalPages);

            foreach(var card in result.Value)
            {
                MySqlConnection conn = DB.Connection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = @"UPDATE cards SET image_url = @url WHERE multiverseid = @cardMvi;";
                MySqlParameter url = new MySqlParameter("@url", card.ImageUrl.ToString());
                cmd.Parameters.Add(url);
                MySqlParameter mvid = new MySqlParameter("@cardMvi", card.MultiverseId);
                cmd.Parameters.Add(mvid);

                cmd.ExecuteNonQuery();
                
                conn.Close();
                if(conn != null) conn.Dispose();
            }

            pageNum++;
            if(pageNum > result.PagingInfo.TotalPages) Console.WriteLine("Done");
            else{ GetImageUrls(); }


        }
    }
}
