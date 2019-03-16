using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicRoutine.Entities
{
    public class Routine
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string TitleEn { get; set; }

        public virtual ICollection<RoutineDashboard> Dashboards { get; set; }

        public virtual ICollection<RoutineStep> Steps { get; set; }

        public virtual ICollection<RoutineLog> Logs { get; set; }

        public virtual ICollection<RoutineField> Fields { get; set; }

        public virtual ICollection<RoutineForm> Forms { get; set; }
    }
}
