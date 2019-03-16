using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicRoutine.Services
{
    public static class RoutineService
    {
        public static string GetQuery_CreateTable(string tblName, List<Entities.RoutineDashboard> dashboards,
            List<Entities.RoutineField> fields)
        {
            string query = $"create table {tblName}(";
            query += "Id int primary key identity,";
            query += "RoutineIsFlown bit not null,";
            query += "RoutineIsSuccess bit not null,";
            query += "RoutineIsDone bit not null,";
            query += "RoutineStep int  null,";
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

            fields.ForEach(c =>
            {
                switch (c.Type)
                {
                    case SSOT.FieldType.Text:
                        {
                            query += $"Field_{c.TitleEn} nvarchar(MAX) null,";

                            break;
                        }
                       
                    case SSOT.FieldType.Number:
                        {
                            query += $"Field_{c.TitleEn} bigint null,";
                            break;
                        }
                    case SSOT.FieldType.File:
                        {
                            query += $"Field_{c.TitleEn} nvarchar(MAX) null,";
                            break;
                        }
                    case SSOT.FieldType.TextArea:
                        {
                            query += $"Field_{c.TitleEn} nvarchar(MAX) null,";
                            break;
                        }
                    case SSOT.FieldType.DropDown:
                        {
                            query += $"Field_{c.TitleEn}_Drop_Text nvarchar(MAX) null,";
                            query += $"Field_{c.TitleEn}_Drop_Value int null,";
                            break;
                        }
                    case SSOT.FieldType.DateTime:
                        {
                            query += $"Field_{c.TitleEn} datetime null,";
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            });

            if (query.EndsWith(","))
            {
                query = query.Remove(query.Length - 1);
            }

            query += ")";
            return query;
        }
    }
}
