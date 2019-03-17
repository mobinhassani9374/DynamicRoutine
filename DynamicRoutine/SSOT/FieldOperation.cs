using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DynamicRoutine.SSOT
{
    public enum FieldOperation
    {
        [Display(Name ="پرکردن")]
        Fill = 1,

        [Display(Name ="خواندنی")]
        Read = 2
    }
}
