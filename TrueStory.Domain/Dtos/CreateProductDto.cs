using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueStory.Domain.Dtos
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Dictionary<string, object> Data { get; set; }
    }
}
