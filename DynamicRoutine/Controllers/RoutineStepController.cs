using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DynamicRoutine.Entities;
using Microsoft.AspNetCore.Mvc;
using Dapper;

namespace DynamicRoutine.Controllers
{
    public class RoutineStepController : Controller
    {
        private readonly Data.AppDbContext _context;
        private readonly IDbConnection _connection;
        public RoutineStepController(Data.AppDbContext context,
            IDbConnection connection)
        {
            _context = context;
            _connection = connection;
        }
        public IActionResult Manage(int id)
        { 
            ViewBag.RoutineId = id;
            var dashboards = _context.RoutineDashboards.ToList();
            ViewBag.Dashboards = dashboards;
            ViewBag.RoutineSteps = _context.RoutineSteps.Where(c => c.RoutineId.Equals(id)).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Manage(int routineId, int from, int? to, RoutneAction action)
        {
            if(action== RoutneAction.ConfirmAndFinihs || action== RoutneAction.Cancel)
            {
                to = null;
            }
            _context.RoutineSteps.Add(new RoutineStep
            {
                Action = action,
                FromStep = from,
                ToStep = to,
                RoutineId = routineId
            });
            _context.SaveChanges();

            return RedirectToAction(nameof(Manage), new { id = routineId });
        }
        public IActionResult Delete(int id)
        {
            var entity = _context.RoutineSteps.FirstOrDefault(c => c.Id.Equals(id));
            if (entity != null)
            {
                _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Manage), new { id = entity.RoutineId });
        }
    }
}