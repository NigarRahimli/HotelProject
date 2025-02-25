﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllTopRated
{
    public class PropertyWithHeartAndRateDto
    {
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public bool IsLiked { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public double Rate { get; set; }
        public string HostProfileImgUrl { get; set; }
        public int HostId { get; set; }
    }
}
