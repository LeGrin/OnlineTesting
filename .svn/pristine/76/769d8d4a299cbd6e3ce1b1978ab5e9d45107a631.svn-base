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
    /// Предмет который читается у определенной группы
    /// </summary>
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Test> Tests { get; set; }

        public virtual ICollection<Group_Subject> Group_Subjects { get; set; }

        /// <summary>
        /// Id лектора, который читает данный предмет
        /// </summary>
        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
