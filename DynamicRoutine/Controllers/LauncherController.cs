using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DynamicRoutine.Entities;
using DynamicRoutine.SSOT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DynamicRoutine.Controllers
{
    public class LauncherController : Controller
    {
        private readonly Data.AppDbContext _context;
        private readonly IDbConnection _connection;
        public LauncherController(Data.AppDbContext context,
            IDbConnection connection)
        {
            _context = context;
            _connection = connection;
        }
        public IActionResult Index(int id)
        {
            var models = _context.RoutineDashboards.Where(c => c.RoutineId.Equals(id)).ToList();
            ViewBag.Id = id;
            return View(models);
        }
        public IActionResult Manage(int id, DashboardType type, string dashboard = "")
        {
            var userId = Convert.ToInt32(User.Identity.Name);

            ViewBag.Id = id;
            ViewBag.Dashboard = dashboard;
            ViewBag.Type = type;

            var routine = _context.Routines.Include(c => c.Dashboards).FirstOrDefault(c => c.Id.Equals(id));

            var tblName = routine.TitleEn;

            var currentDashboard = routine.Dashboards.FirstOrDefault(c => c.TitleEn.Equals(dashboard));

            var steps = _context.RoutineSteps.Where(c => c.RoutineId.Equals(id)).ToList();

            ViewBag.Steps = steps;

            var query = "";

            if (type == DashboardType.Draft)
            {
                query = $"select * from {tblName} where RoutineIsFlown=0 and RoutineStep={currentDashboard.StartStep} ";

                if (currentDashboard.MultiUser)
                {
                    query += $"and {currentDashboard.TitleEn}UserIds like '%\"{userId}\"%'";
                }
                else
                {
                    query += $"and {currentDashboard.TitleEn}UserId={userId}";
                }
            }
            dynamic dd = null;

            if (!string.IsNullOrEmpty(query))
            {
                dd = _connection.Query<dynamic>(query);
            }


            return View(dd);
        }

        public IActionResult ChangeDash(int id,int recordId, DashboardType type, string dashboard = "", RoutneAction action = RoutneAction.Send)
        {
            var routine = _context.Routines.Include(c => c.Steps).FirstOrDefault(c => c.Id.Equals(id));

            return View();
        }
    }
}