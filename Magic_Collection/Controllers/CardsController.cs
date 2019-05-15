using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Magic_Collection.Models;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.Service;
using MtgApiManager.Lib.Utility;
using MySql.Data.MySqlClient;


namespace Magic_Collection.Controllers
{
    public class CardsController : Controller
    {
        [HttpGet("/cards")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost("/cards/search")]
        public ActionResult Show(string name)
        {
            List<string> images = new List<string>{};
            
            MySqlConnection conn = DB.Connection();
            conn.Open();
            
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT image_url FROM cards WHERE name LIKE '%"+name+"%' ORDER BY name ASC;";

            MySqlParameter searchName = new MySqlParameter("@searchName", name);
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

            return View(images);
        }

        
    }   
}