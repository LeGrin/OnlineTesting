using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPR_testing_Lib.Domain
{
    /// <summary>
    /// Сессия, во время которой студенты из определенной группы проходят тесты
    /// </summary>
    public class Session
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Id теста, который сдается в эту сессию
        /// </summary>
        [Required]
        public int TestId { get; set; }

        public virtual Test Test { get; set; }

        /// <summary>
        /// Дата начала сессии
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Дата окончания сессии
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Id группы
        /// </summary>
        [Required]
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }

        /// <summary>
        /// Вычислены ли максимальные баллы за вопросы
        /// </summary>
        [DefaultValue(false)]
        public bool IsCalculated { get; set; }

        public virtual ICollection<PriceData> PricesData { get; set; }
    }
}
