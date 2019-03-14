using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicRoutine.Entities
{
    public class RoutineLog
    {
        public int Id { get; set; }

        public int FromStep { get; set; }

        public int ToStep { get; set; }

        public virtual Routine Routine { get; set; }

        public int RoutineId { get; set; }

        public int UserId { get; set; }

        public RoutneAction Action { get; set; }

        public int EntityId { get; set; }
    }
}
