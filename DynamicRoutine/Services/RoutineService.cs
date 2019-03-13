using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicRoutine.Services
{
    public static class RoutineService
    {
        public static string GetQuery_CreateTable(string tblName, List<Entities.RoutineDashboard> dashboards)
        {
            string query = $"create table {tblName}(";
            query += "Id int primary key identity,";
            query += "RoutineIsFlown bit not null,";
            query += "RoutineIsSuccess bit not null,";
            query += "RoutineIsDone bit not null,";
            query += "RoutineStep int not null,";
            query += "RoutineFlownDate datetime null,";

            dashboards.ForEach(c =>
            {
                if (c.MultiUser)
                {
                    query += $"{c.TitleEn}UserIds nvarchar(MAX),";
                }
                else
                {
                    query += $"{c.TitleEn}UserId int null,";
                }
            });

            if (query.EndsWith(","))
            {
                query = query.Remove(query.Length-1);
            }

            query += ")";
            return query;
        }
    }
}
