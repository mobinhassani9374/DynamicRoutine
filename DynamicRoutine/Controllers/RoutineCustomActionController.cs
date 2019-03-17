using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DynamicRoutine.Controllers
{
    public class RoutineCustomActionController : Controller
    {
        private readonly Data.AppDbContext _context;
        public RoutineCustomActionController(Data.AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int id)
        {
            ViewBag.StepId = id;
            var model = _context.RoutineCustomAction.Where(c => c.RoutineStepId.Equals(id)).ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(int stepId, string title)
        {
            _context.RoutineCustomAction.Add(new Entities.RoutineCustomAction
            {
                RoutineStepId = stepId,
                Title = title
            });
            _context.SaveChanges();

            return RedirectToAction(nameof(Index), new { id = stepId });
        }
        public IActionResult Delete(int id)
        {
            var entity = _context.RoutineCustomAction.FirstOrDefault(c => c.Id.Equals(id));
            if (entity != null)
            {
                _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index), new { id = entity.RoutineStepId });
        }
    }
}