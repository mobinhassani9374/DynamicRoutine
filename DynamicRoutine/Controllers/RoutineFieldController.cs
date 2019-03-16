using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DynamicRoutine.SSOT;
using Microsoft.AspNetCore.Mvc;

namespace DynamicRoutine.Controllers
{
    public class RoutineFieldController : Controller
    {
        private readonly Data.AppDbContext _context;
        public RoutineFieldController(Data.AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int id)
        {
            ViewBag.RoutineId = id;
            var model = _context.RoutineFields.Where(c => c.RoutineId.Equals(id)).ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(int routineId, string title, string Url, FieldType type, bool isRequide, string titleEn)
        {
            _context.RoutineFields.Add(new Entities.RoutineField
            {
                FieldTypeDes = type.ToString(),
                IsRequide = isRequide,
                RoutineId = routineId,
                Title = title,
                Type = type,
                Url = Url,
                TitleEn = titleEn
            });
            _context.SaveChanges();

            return RedirectToAction(nameof(Index), new { id = routineId });
        }

        public IActionResult Delete(int id)
        {
            var entity = _context.RoutineFields.FirstOrDefault(c => c.Id.Equals(id));
            if (entity != null)
            {
                _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index), new { id = entity.RoutineId });
        }
    }
}