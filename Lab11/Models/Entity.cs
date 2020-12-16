using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab11.Models
{
    public enum CoolEnum
    {
        [Display(Name = "Opcja A")]
        Option1,
        [Display(Name = "Opcja B")]
        Option2,
        [Display(Name = "Opcja C")]
        Option3
    }

    public class Entity
    {
        [Key]
        [Required]
        public int Key { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]{5,30}@[a-zA-Z0-9]{2,15}\.[a-zA-Z]{2,10}$", ErrorMessage = "Niepoprawny email")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Required]
        [MaxLength(40, ErrorMessage = "Name is too long")]
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{2}-[0-9]{3}$", ErrorMessage = "Niepoprawny kod pocztowy")]
        [Display(Name = "Kod pocztowy")]
        public string Postcode { get; set; }
        [EnumDataType(typeof(CoolEnum))]
        [Display(Name = "Fajny dropdown")]
        public CoolEnum EnumValue { get; set; }
    }
}
