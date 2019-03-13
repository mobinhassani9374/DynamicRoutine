using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DynamicRoutine.Controllers
{
    public class RoutineDashboardController : Controller
    {
        private readonly Data.AppDbContext _context;
        public RoutineDashboardController(Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            var model = _context.RoutineDashboards.Where(c => c.RoutineId.Equals(id)).ToList();
            return View(model);
        }
        public IActionResult Create(int id)
        {
            ViewBag.RoutineId = id;
            return View();
        }
        [HttpPost]
        public IActionResult Create(int routineId, string title, string titleEn, bool multiUser)
        {
            var startStep = 1000;

            var lastRecord = _context
                .RoutineDashboards
                .OrderByDescending(c => c.StartStep)
                .LastOrDefault(c => c.RoutineId.Equals(routineId));

            if (lastRecord != null)
            {
                startStep = lastRecord.StartStep + 1000;
            }

            _context.RoutineDashboards.Add(new Entities.RoutineDashboard
            {
                RoutineId = routineId,
                Title = title,
                TitleEn = titleEn,
                StartStep = startStep,
                MultiUser = multiUser
            });

            _context.SaveChanges();

            return RedirectPermanent("/Routine");
        }

        public IActionResult Delete(int id)
        {
            var entity = _context.RoutineDashboards.FirstOrDefault(c => c.Id.Equals(id));
            if (entity != null)
            {
                _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _context.SaveChanges();
            }
            return RedirectPermanent("/Routine");
        }
    }
}