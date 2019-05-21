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
            // List<string> allCards = new List<string>{};

            // MySqlConnection conn = DB.Connection();
            // conn.Open();

            // MySqlCommand cmd = conn.CreateCommand();
            // cmd.CommandText = @"SELECT image_url FROM cards ORDER BY name ASC;";

            // MySqlDataReader rdr = cmd.ExecuteReader();
            // while(rdr.Read())
            // {
            //     if(!rdr.IsDBNull(0))
            //     {
            //         string imageUrl = rdr.GetString(0);
            //         allCards.Add(imageUrl);
            //     }
            // }

            // conn.Close();
            // if(conn!=null) conn.Dispose();

            ViewBag.Results = DB.AllCards();
            ViewBag.Search = " ";
            ViewBag.Column = "*";

            return View();

        }

        [HttpPost("/cards/search")]
        public ActionResult Show(string search, string column, int limit = 50)
        {
            ViewBag.Results = DB.Search(search, column, 1, limit);
            ViewBag.Search = search;
            ViewBag.Column = column;
            return View();
        }


        [HttpPost("/cards/search/{page}")]
        public ActionResult Show(string search, string column, int page = 1, int limit = 50)
        {

            ViewBag.Results = DB.Search(search, column, page, limit);
            ViewBag.Search = search;
            ViewBag.Column = column;
            ViewBag.Page = page;
            return View();
        }
    }   
}
