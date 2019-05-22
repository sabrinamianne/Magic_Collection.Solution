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
        [HttpGet("/cards/{page}")]
        public ActionResult Index(int page)
        {
            ViewBag.Results = DB.AllCards(page);
            ViewBag.Images = ViewBag.Results["images"];
            ViewBag.TotalPages = ViewBag.Results["totalPages"];
            ViewBag.Page = page;

            return View();

        }
        
        [HttpPost("/cards/{page}")]
        public ActionResult Index(int page, int limit = 50)
        {
            ViewBag.Results = DB.AllCards(page);
            ViewBag.Images = ViewBag.Results["images"];
            ViewBag.TotalPages = ViewBag.Results["totalPages"];
            ViewBag.Page = page;

            return View();

        }

        [HttpPost("/cards/search/")]
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
