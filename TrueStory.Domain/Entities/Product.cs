using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueStory.Domain.Entities;

public class Product
{
    public string Id { get; set; }
    public string Name { get; set; }
    public Dictionary<string, object> Data { get; set; }
}
