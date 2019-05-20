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

            return View(allSets);
        }
        
        [HttpGet("/sets/{setName}")]
        public ActionResult Show(string setName)
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

            ViewBag.AllIds = allIds;
            
            return View(allCardImagesInSet);
        }
    }
}