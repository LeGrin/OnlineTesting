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
    /// Задание в тесте
    /// </summary>
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Id теста
        /// </summary>
        [Required]
        public int TestId { get; set; }

        public virtual Test Test { get; set; }

        /// <summary>
        /// Текст задания
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Тип задания
        /// </summary>
        [Required]
        public int TaskTypeId { get; set; }

        /// <summary>
        /// Цена вопроса, задается преподом
        /// </summary>
        [Range(1, double.MaxValue)]
        public double Price { get; set; }

        public virtual TaskType TaskType { get; set; }

        public virtual ICollection<PriceData> PricesData { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
