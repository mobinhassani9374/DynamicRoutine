using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace DynamicRoutine.Controllers
{
    public class RoutineController : Controller
    {
        private readonly Data.AppDbContext _context;
        private readonly IDbConnection _connection;

        public RoutineController(Data.AppDbContext context,
            IDbConnection connection)
        {
            _context = context;
            _connection = connection;
        }
        public IActionResult Index()
        {
            var model = _context.Routines.Include(c => c.Dashboards).ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(string title, string titleEn)
        {
            _context.Routines.Add(new Entities.Routine
            {
                Title = title,
                TitleEn = titleEn
            });
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Save(int id)
        {
            var dashboards = _context.RoutineDashboards.Where(c => c.RoutineId.Equals(id)).ToList();

            var fields = _context.RoutineFields.Where(c => c.RoutineId.Equals(id)).ToList();

            var routine = _context.Routines.FirstOrDefault(c => c.Id.Equals(id));

            var query = Services.RoutineService.GetQuery_CreateTable(routine.TitleEn, dashboards, fields);

            _connection.Query(query);

            return RedirectToAction(nameof(Index));
        }
    }
}