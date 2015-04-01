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
    /// Соответствие группы и предмета, который у этой группы читается
    /// </summary>
    public class Group_Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Id предмета
        /// </summary>
        [Required]
        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        /// <summary>
        /// Id группы
        /// </summary>
        [Required]
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }
    }
}
