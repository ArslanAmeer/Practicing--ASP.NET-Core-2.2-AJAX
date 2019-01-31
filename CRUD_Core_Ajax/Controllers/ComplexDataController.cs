using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Core_Ajax.Controllers
{
    public class ComplexDataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}