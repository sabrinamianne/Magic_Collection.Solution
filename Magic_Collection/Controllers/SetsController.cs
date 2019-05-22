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
    public class SetsController : Controller
    {
        [HttpGet("/sets")]
        public ActionResult Index()
        {
            List<string> allSets = DB.GetSetsList();
            return View(allSets);
        }
        
        [HttpGet("/sets/{setName}")]
        public ActionResult Show(string setName)
        {
            List<string> allCardImagesInSet = DB.GetSetImages(setName);
            return View(allCardImagesInSet);
        }
    }
}