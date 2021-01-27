using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lab12.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        [Required][StringLength(40)]
        public string Name { get; set; }
        [Required][Range(0.01, 999999999)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DisplayName("Price in $")]
        public double Price { get; set; }
        public string ImagePath { get; set; }
        
        [ForeignKey("Category")]
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Category Category { get; set; }
    }
}
