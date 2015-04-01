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
    /// Информация о том, что студент сдал тест(ответил на все вопросы)
    /// </summary>
    public class PassedTest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int TestId { get; set; }

        public virtual Test Test { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
