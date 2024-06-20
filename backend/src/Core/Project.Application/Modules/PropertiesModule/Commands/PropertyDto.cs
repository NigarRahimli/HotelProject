using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.PropertiesModule.Commands
{
    public class PropertyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DescriptionId { get; set; }
    }
}
