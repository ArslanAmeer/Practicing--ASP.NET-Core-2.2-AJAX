using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_Core_Ajax.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CRUD_Core_Ajax.Controllers
{
    public class ComplexDataController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ComplexDataController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddComplexData([FromBody] Owner owner)
        {
            try
            {
                using (_db)
                {
                    foreach (var t in owner.Cars)
                    {
                        _db.Entry(t.City).State = EntityState.Unchanged;
                    }

                    await _db.Owners.AddAsync(owner);
                    await _db.SaveChangesAsync();
                }
                return new JsonResult(new { success = true, responseText = "New Data added to Database Successfully!" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }


        public IActionResult AddListOfComplexData([FromBody] List<Owner> owner)
        {
            List<Owner> newList = owner;
            if (newList != null)
            {
                return new JsonResult(new { success = true });
            }
            return new JsonResult(new { success = false });
        }

        public List<City> GetCities()
        {
            using (_db)
            {
                return _db.Cities.ToList();
            }
        }

    }
}