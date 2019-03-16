using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DynamicRoutine.Data;
using Microsoft.AspNetCore.Mvc;


namespace DynamicRoutine.Controllers
{
    public class HomeController : Controller
    {
        private readonly Data.AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var userId = Convert.ToInt32(User.Identity.Name);
            ViewBag.FullName = _context.Users.FirstOrDefault(c => c.Id.Equals(userId))?.FullName;
            return View();
        }
    }
}