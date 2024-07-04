import React, { useEffect, useState } from 'react';
import StarCard from '../card/StarCard';
import { baseUrl } from "@/components/constant";

const TopRatedProperties = () => {
  const [properties, setProperties] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchProperties = async () => {
      try {
        const response = await fetch(`${baseUrl}/api/properties/rated/4`);
        const data = await response.json();
        setProperties(data);
      } catch (error) {
        console.error('Error fetching properties:', error);
        setError('Error fetching top rated properties');
      } finally {
        setLoading(false);
      }
    };

    fetchProperties();
  }, []);

  return (
    <div className="top pt-[100px] mx-auto md:w-full w-[279px]">
      <h1 className="font-bold text-[25px] text-[#484848] md:w-[339px] w-[279px] md:text-[36px]">
        Top Rated Properties
      </h1>
      <div className="bg-[#484848] w-[140px] h-[6px] rounded-[3px] mt-[30px] mb-[60px]"></div>

      {loading ? (
        <div>Loading...</div>
      ) : error ? (
        <div>{error}</div>
      ) : (
        <div className="flex gap-[30px] flex-wrap content-center items-center mx-auto">
          {properties.map((property) => (
            <StarCard
              key={property.propertyId}
              imgSrc={`${baseUrl}/${property.url}`}
              userSrc={`${baseUrl}/${property.hostProfileImgUrl}`}
              isFavourite={property.isLiked}
              name={property.name}
              address={property.address}
              city={property.city}
              country={property.country}
              star={property.rate}
            />
          ))}
        </div>
      )}
    </div>
  );
};

export default TopRatedProperties;
