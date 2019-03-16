using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DynamicRoutine.SSOT
{
    public enum FormType
    {
        [Display(Name ="پرکردنی")]
        Fill = 1,

        [Display(Name ="خواندنی")]
        Read = 2
    }
}
