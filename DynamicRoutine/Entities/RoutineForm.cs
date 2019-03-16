using DynamicRoutine.SSOT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicRoutine.Entities
{
    public class RoutineForm
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string FieldJson { get; set; }

        public FormType Type { get; set; }

        public virtual Routine Routine { get; set; }

        public int FromStep { get; set; }

        public RoutneAction Action { get; set; }

        public bool PreviousIsEdit { get; set; }

        public int RoutineId { get; set; }
    }
}
