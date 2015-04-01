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
    /// Ответ на вопрос
    /// </summary>
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Id задания, в котором есть этот вопрос
        /// </summary>
        [Required]
        public int TaskId { get; set; }

        public virtual Task Task { get; set; }

        /// <summary>
        /// Текст ответа
        /// </summary>
        [Required]
        public string Text { get; set; }

        /// <summary>
        /// Является ли ответ правильный
        /// </summary>
        [Required]
        public bool IsCorrect { get; set; }

        /// <summary>
        /// Цена ответа, -1 - 1
        /// </summary>
        [Required]
        [Range(-1.0, 1.0)]
        public double Price { get; set; }
    }
}
