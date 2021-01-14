using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lab12.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required][StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }
        
        public List<Article> Articles { get; set; }
    }
}
