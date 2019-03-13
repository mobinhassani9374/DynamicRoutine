using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicRoutine.Entities
{
    public class RoutineDashboard
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string TitleEn { get; set; }

        public int StartStep { get; set; }


        public bool MultiUser { get; set; } = false;

        public virtual Routine Routine { get; set; }

        public int RoutineId { get; set; }
    }
}
