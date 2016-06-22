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
    /// Тест по определенному предмету
    /// </summary>
    public class Test
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Название теста
        /// </summary>
        [Required]
        [MinLength(5)]
        [MaxLength(150)]
        public string Name { get; set; }

        /// <summary>
        /// Id предмета
        /// </summary>
        [Required]
        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }

        /// <summary>
        /// Id лектора, который проводит этот тест
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Макс балл за тест
        /// </summary>
        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }

        public virtual ICollection<TestLog> TestLogs { get; set; }
    }
}
