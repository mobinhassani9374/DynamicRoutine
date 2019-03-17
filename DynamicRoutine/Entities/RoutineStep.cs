using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicRoutine.Entities
{
    public class RoutineStep
    {
        public int Id { get; set; }

        public int FromStep { get; set; }

        public int? ToStep { get; set; }

        public RoutneAction Action { get; set; }

        public virtual Routine Routine { get; set; }

        public int RoutineId { get; set; }

        public virtual ICollection<RoutineCustomAction> CustomActions { get; set; }
    }

    public enum RoutneAction
    {
       [Display(Name ="تایید")]
        /// <summary>
        /// تایید
        /// </summary>
        Confirm = 1,

        /// <summary>
        /// رد
        /// </summary>
        [Display(Name = "رد")]
        Cancel = 2,

        /// <summary>
        /// نیاز به اصلاح
        /// </summary>
        [Display(Name = "نیاز به اصلاح")]
        Edit = 3,

        /// <summary>
        /// ارسال
        /// </summary>
        [Display(Name = "ارسال")]
        Send = 4,

        [Display(Name = "تایید و پایان")]
        ///تایید و پایان  
        ConfirmAndFinihs = 5
    }
}
