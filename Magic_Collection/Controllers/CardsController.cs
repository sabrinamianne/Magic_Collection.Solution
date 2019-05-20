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
            List<string> allCards = new List<string>{};

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT image_url FROM cards ORDER BY name ASC;";

            MySqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                if(!rdr.IsDBNull(0))
                {
                    string imageUrl = rdr.GetString(0);
                    allCards.Add(imageUrl);
                }
            }

            conn.Close();
            if(conn!=null) conn.Dispose();

            return View(allCards);

        }

        [HttpPost("/cards/search")]
        public ActionResult Show(string search, string column)
        {
            List<string> images = DB.Search(search, column);

            return View(images);
        }


        [HttpGet("/cards/search")]
        public ActionResult Show(string test)
        {
            return View();
        }

        // [HttpPost]
        // public ActionResult Ajax(string input)
        // {
        //     Console.WriteLine("made it to the controller!");
        //     return RedirectToAction("Index");
        // }
    }   
}
