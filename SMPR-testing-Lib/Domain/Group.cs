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
    /// Группа студентов
    /// </summary>
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual ICollection<Group_Subject> Group_Subjects { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }

        /// <summary>
        /// Имя группы
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
