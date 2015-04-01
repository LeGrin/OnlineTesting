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
    /// Соответствие студента к ответу, который он дал
    /// </summary>
    public class Student_Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Id записи с результатом ответа
        /// </summary>
        [Required]
        public int PriceDataId { get; set; }

        public virtual PriceData PriceData { get; set; }
        
        /// <summary>
        /// Ответ, который дал студент
        /// </summary>
        [Required]
        public string GivenAnswer { get; set; }
    }
}
