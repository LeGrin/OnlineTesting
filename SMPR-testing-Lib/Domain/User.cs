using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SMPR_testing_Lib.Domain
{
    /// <summary>
    /// Пользователь - (студент, препод)
    /// </summary>
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Имя и фамилия пользователя
        /// </summary>
        [MinLength(7)]
        [MaxLength(50)]
        [Display(Name = "ФИО")]
        public string Name { get; set; }

        /// <summary>
        /// Логин использующийся для входа
        /// </summary>
        [Required]
        [MinLength(7)]
        [MaxLength(25)]
        [Display(Name = "Имя использующиеся для входа")]
        public string LoginName { get; set; }

        /// <summary>
        /// Id группы, в которую входит пользователь
        /// </summary>
        [Required]
        [HiddenInput(DisplayValue = false)]
        [Display(Name="Группа")]
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }

        public ICollection<PriceData> PricesData { get; set; }
    }
}
