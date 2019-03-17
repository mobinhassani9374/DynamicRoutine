using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicRoutine.Entities
{
    public class UserRoutineField
    {
        public int Id { get; set; }

        public virtual User User { get; set; }

        public int UserId { get; set; }

        public int RoutineFieldId { get; set; }

        public RoutineField RoutineField { get; set; }
    }
}
