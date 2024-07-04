import React, { useState, useEffect } from 'react';
import FeaturedCard from '../card/FeaturedCard';
import { baseUrl } from '@/components/constant';

const FeaturedProperties = () => {
  const [properties, setProperties] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(`${baseUrl}/api/properties/featured/3`);
        const data = await response.json();
        setProperties(data);
      } catch (error) {
        console.error('Error fetching properties:', error);
      }
    };

    fetchData();
  }, []);

  return (
    <div className="featured pt-[100px] mx-auto md:w-full w-[279px]">
      <h1 className="font-bold text-[25px] text-[#484848] md:w-[412px] w-[279px] md:text-[36px]">
        Featured Properties on our Listing
      </h1>
      <div className="bg-[#484848] w-[140px] h-[6px] rounded-[3px] mt-[30px] mb-[60px]"></div>

      <div className="flex gap-[30px] flex-wrap content-center items-center mx-auto">
        {properties.map((property) => (
          <div key={property.propertyId} className="w-[300px] xl:w-[382px]">
            <FeaturedCard
              propertyImages={property.propertyImageDetails.map(image => `${baseUrl}${image.url}`)}
              isFavourite={property.isLiked}
              name={property.name}
              address={property.address}
              city={property.city}
              country={property.country}
              amenities={property.facilitiesDetails.map(facility => ({
                ...facility,
                iconUrl: `${baseUrl}${facility.iconUrl}`,
              }))}
              minPrice={property.minPrice}
              maxPrice={property.maxPrice}
            />
          </div>
        ))}
      </div>
    </div>
  );
};

export default FeaturedProperties;
