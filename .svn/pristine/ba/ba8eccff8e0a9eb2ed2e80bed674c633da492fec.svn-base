using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPR_testing_Lib.Domain
{
    /// <summary>
    /// Тип задания - (один из многих, много из многих, текстовый, ...)
    /// </summary>
    public class TaskType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Название типа
        /// </summary>
        [Required]
        [MinLength(3)]
        [MaxLength(35)]
        public string Name { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
