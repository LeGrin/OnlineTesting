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
    /// Лог редактирования структуры и содержания теста
    /// </summary>
    public class TestLog
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
        /// Сообщение лога
        /// </summary>
        [Required]
        [MaxLength(300)]
        public string Message { get; set; }
    }
}
