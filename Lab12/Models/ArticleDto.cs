using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Lab12.Models
{
    public class ArticleDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        [Required][Range(0.01, 999999999)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DisplayName("Price in $")]
        public double Price { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public IFormFile Image { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonIgnore]
        public Category Category { get; set; }

        public Article CastToArticle()
        {
            string fileName = null;
            if (Image != null)
            {
                string extension = Path.GetExtension(Image.FileName);
                fileName = Guid.NewGuid().ToString() + extension;
            }

            Article result = new Article
            {
                Id = Id,
                Name = Name,
                Price = Price,
                ImagePath = fileName,
                CategoryId = CategoryId,
            };

            return result;
        }
    }
}
