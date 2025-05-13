using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueStory.Domain.Dtos
{
    public class GetProductsDto
    {
        public string name { get; set; }
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }

    public class GetProductByIdDto
    {
        public string id { get; set; }
    }
}
