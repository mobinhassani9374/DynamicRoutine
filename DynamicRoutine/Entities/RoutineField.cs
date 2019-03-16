using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicRoutine.Entities
{
    public class RoutineField
    {
        public int Id { get; set; }

        /// <summary>
        /// عنوان فیلد
        /// </summary>
        [MaxLength(300)]
        public string Title { get; set; }

        public SSOT.FieldType Type { get; set; }

        [MaxLength(100)]
        public string FieldTypeDes { get; set; }

        [MaxLength(700)]
        public string Url { get; set; }

        public bool IsRequide { get; set; }


        public virtual Routine Routine { get; set; }

        public int RoutineId { get; set; }
    }
}
