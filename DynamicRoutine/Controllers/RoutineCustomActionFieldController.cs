using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DynamicRoutine.SSOT;
using Microsoft.AspNetCore.Mvc;

namespace DynamicRoutine.Controllers
{
    public class RoutineCustomActionFieldController : Controller
    {
        private readonly Data.AppDbContext _context;
        public RoutineCustomActionFieldController(Data.AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int id)
        {
            var model = _context.RoutineCustomActionFields.Where(c => c.RoutineCustomActionId.Equals(id)).ToList();

            ViewBag.Id = id;

            var action = _context.RoutineCustomAction.FirstOrDefault(c => c.Id.Equals(id));

            var routineStep = _context.RoutineSteps.FirstOrDefault(c => c.Id.Equals(action.RoutineStepId));

            var fileds = _context.RoutineFields.Where(c => c.RoutineId.Equals(routineStep.RoutineId)).ToList();

            ViewBag.Fields = fileds;

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(int fieldId, FieldOperation operation, int routineCustomActionId)
        {
            _context.RoutineCustomActionFields.Add(new Entities.RoutineCustomActionField
            {
                FieldId = fieldId,
                Operation = operation,
                RoutineCustomActionId = routineCustomActionId

            });
            _context.SaveChanges();
            return RedirectToAction(nameof(Index), new { id = routineCustomActionId });
        }
        public IActionResult Delete(int id)
        {
            var entity = _context.RoutineCustomActionFields.FirstOrDefault(c => c.Id.Equals(id));
            if (entity != null)
            {
                _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index), new { id = entity.RoutineCustomActionId });
        }
    }
}