using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.DescriptionModule.Queries
{
    public class DescriptionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Explanation { get; set; }
    }
}
