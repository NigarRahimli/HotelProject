﻿using Project.Application.Modules.FacilitiesModule.Queries;
using Project.Application.Modules.PropertyImagesModule.Query.PropertyImagesGetByPropertyIdQuery;

namespace Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllFeatured
{
    public class PropertyFilteredDto
    {
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public bool IsLiked { get; set; }
        public IEnumerable<PropertyImageDetailsDto> PropertyImageDetails { get; set; }
        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }

    }
}
