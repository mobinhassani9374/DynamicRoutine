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

            if (type == DashboardType.Sent)
            {
                // داخل لاگ باشه و مخالف step جاری باشد
                var logs = _context.RoutineLog.Where(c => c.RoutineId.Equals(id) && c.UserId.Equals(userId)).ToList();

                var logEntityIds = logs.Select(c => c.EntityId).ToList();

                if (logEntityIds.Count > 0)
                {
                    var logEntityIdsQuery = "";
                    logEntityIds.ForEach(c =>
                    {
                        logEntityIdsQuery += $"{c},";
                    });
                    if (logEntityIdsQuery.EndsWith(","))
                    {
                        logEntityIdsQuery = logEntityIdsQuery.Remove(logEntityIdsQuery.Length - 1);
                    }


                    var currentStep = currentDashboard.StartStep;
                    query = $"select * from {tblName} where Id in({logEntityIdsQuery}) and RoutineStep!={currentStep}";
                }
            }

            if (type == DashboardType.New)
            {
                query = $"select * from {tblName} where RoutineIsFlown=1 and RoutineStep={currentDashboard.StartStep}";
                if (currentDashboard.MultiUser)
                {
                    query += $" and ({currentDashboard.TitleEn}UserIds like '%\"{userId}\"%' or {currentDashboard.TitleEn}UserIds is null)";
                }
                else
                {
                    query += $" and ({currentDashboard.TitleEn}UserId={userId} or {currentDashboard.TitleEn}UserId is null)";
                }

            }

            if (type == DashboardType.Done)
            {
                // داخل لاگ باشه و مخالف step جاری باشد
                var logs = _context.RoutineLog.Where(c => c.RoutineId.Equals(id) && c.UserId.Equals(userId)).ToList();

                var logEntityIds = logs.Select(c => c.EntityId).ToList();

                if (logEntityIds.Count > 0)
                {
                    var logEntityIdsQuery = "";
                    logEntityIds.ForEach(c =>
                    {
                        logEntityIdsQuery += $"{c},";
                    });
                    if (logEntityIdsQuery.EndsWith(","))
                    {
                        logEntityIdsQuery = logEntityIdsQuery.Remove(logEntityIdsQuery.Length - 1);
                    }


                    var currentStep = currentDashboard.StartStep;
                    query = $"select * from {tblName} where Id in({logEntityIdsQuery}) and RoutineIsDone=1";
                }
            }

            dynamic dd = null;

            if (!string.IsNullOrEmpty(query))
            {
                dd = _connection.Query<dynamic>(query);
            }

            var forms = _context.RoutineForms.Where(c => c.RoutineId.Equals(id)).ToList();
            ViewBag.Forms = forms;

            var lastLog = _context.RoutineLog.LastOrDefault(c => c.RoutineId.Equals(id));
            ViewBag.LastLog = lastLog;

            return View(dd);
        }

        public IActionResult ChangeDash(int id, DashboardType type, string dashboard = "", RoutneAction action = RoutneAction.Send, int recordId = 0)
        {
            var userId = Convert.ToInt32(User.Identity.Name);

            var routine = _context.Routines.Include(c => c.Steps).FirstOrDefault(c => c.Id.Equals(id));

            var tblName = routine.TitleEn;

            var record = _connection.Query<dynamic>($"select * from {tblName} where Id={recordId}").FirstOrDefault();

            var currentStep = record.RoutineStep;

            var toStep = routine.Steps.Where(c => c.Action == action && c.FromStep == currentStep).FirstOrDefault().ToStep;


            // log
            _context.RoutineLog.Add(new RoutineLog
            {
                Action = action,
                FromStep = currentStep,
                UserId = userId,
                RoutineId = id,
                ToStep = toStep,
                EntityId = recordId
            });
            _context.SaveChanges();

            var toStepStr = "null";
            if (toStep.HasValue)
            {
                toStepStr = toStep.Value.ToString();
            }

            // update current record
            var update_query = $"update {tblName} set RoutineStep={toStepStr} ,";

            if (!record.RoutineIsFlown)
            {
                update_query += " RoutineIsFlown=1 ,";
            }
            if (action == RoutneAction.ConfirmAndFinihs)
            {
                update_query += " RoutineIsDone=1 , RoutineIsSuccess=1 ,";
            }
            if (action == RoutneAction.Cancel)
            {
                update_query += " RoutineIsDone=1 , RoutineIsSuccess=0 ,";
            }

            if (update_query.EndsWith(","))
            {
                update_query = update_query.Remove(update_query.Length - 1);
            }

            update_query += $" where Id={recordId}";

            var update_query_result = _connection.Execute(update_query);

            return RedirectToAction(nameof(Manage), new
            {
                id = id,
                type = type,
                dashboard = dashboard
            });
        }
    }
}