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
    /// Кол-во баллов, который студент получил за определенное задание
    /// </summary>
    public class PriceData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Id сессии, во время которой студент отвечал на вопросы
        /// </summary>
        [Required]
        public int SessionId { get; set; }

        public virtual Session Session { get; set; }

        public virtual ICollection<Student_Answer> Student_Answers { get; set; }

        /// <summary>
        /// Id пользователя
        /// </summary>
        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        /// <summary>
        /// Id задания
        /// </summary>
        [Required]
        public int TaskId { get; set; }

        public virtual Task Task { get; set; }

        /// <summary>
        /// Результирующий балл за задание
        /// </summary>
        [Required]
        [Range(0, 1)]
        public double Price { get; set; }
    }
}
