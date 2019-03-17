using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicRoutine.Entities
{
    public class RoutineCustomAction
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int RoutineStepId { get; set; }

        public virtual RoutineStep RoutineStep { get; set; }
    }
}
