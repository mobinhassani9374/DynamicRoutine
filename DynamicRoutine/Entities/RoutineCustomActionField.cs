using DynamicRoutine.SSOT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicRoutine.Entities
{
    public class RoutineCustomActionField
    {
        public int Id { get; set; }

        public int FieldId { get; set; }

        public FieldOperation Operation { get; set; }

        public int RoutineCustomActionId { get; set; }

        public RoutineCustomAction RoutineCustomAction { get; set; }
    }
}
