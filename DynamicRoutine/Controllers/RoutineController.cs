using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dapper;
using DynamicRoutine.Entities;
using DynamicRoutine.SSOT;

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


        public IActionResult Form(int routineId, int fromStep, RoutneAction actionType, bool previousIsEdit)
        {
            var fields = _context.RoutineFields.Where(c => c.RoutineId.Equals(routineId)).ToList();

            ViewBag.Fields = fields;

            ViewBag.Action = actionType;
            ViewBag.RoutineId = routineId;
            ViewBag.FromStep = fromStep;
            ViewBag.PreviousIsEdit = previousIsEdit;

            var model = _context.RoutineForms.FirstOrDefault(c => c.RoutineId.Equals(routineId) && c.FromStep.Equals(fromStep) && c.Action.Equals(actionType) && c.PreviousIsEdit.Equals(previousIsEdit));


            return View(model);
        }
        [HttpPost]
        public IActionResult Form(int routineId, int fromStep, RoutneAction actionType, bool previousIsEdit, List<int> fields, FormType type, string title)
        {
            _context.RoutineForms.Add(new RoutineForm
            {
                Action = actionType,
                FromStep = fromStep,
                PreviousIsEdit = previousIsEdit,
                RoutineId = routineId,
                Title = title,
                Type = type,
                FieldJson = Newtonsoft.Json.JsonConvert.SerializeObject(fields.Select(c => c.ToString()).ToList())
            });

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}