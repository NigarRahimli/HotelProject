using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.PropertyImagesModule.Query.PropertyImagesGetByPropertyIdQuery
{
    public class PropertyImageDetailsDto
    {
        public int Id { get; set; }
        public string Image { get; set; } 
        public string Url { get; set; }
    }
}
