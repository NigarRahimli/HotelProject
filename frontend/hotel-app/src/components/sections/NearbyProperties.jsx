import React, { useEffect, useState } from "react";
import ListedCard from "@/components/card/ListedCard";
import { baseUrl } from "@/components/constant";

const NearbyProperties = () => {
  const [nearbyProperties, setNearbyProperties] = useState([]);
  const [nearbyLoading, setNearbyLoading] = useState(true);
  const [nearbyError, setNearbyError] = useState(null);

  useEffect(() => {
    const fetchNearbyProperties = async (latitude, longitude) => {
      try {
        const response = await fetch(`${baseUrl}/api/properties/nearby/4`, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            Latitude: latitude,
            Longitude: longitude,
            Take: 4,
          }),
        });
        const data = await response.json();
        setNearbyProperties(data);
      } catch (error) {
        setNearbyError("Error fetching nearby properties");
      } finally {
        setNearbyLoading(false);
      }
    };

    const getLocation = () => {
      if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
          (position) => {
            const { latitude, longitude } = position.coords;
            fetchNearbyProperties(latitude, longitude);
          },
          (error) => {
            console.error("Error getting location:", error);
            setNearbyError("Unable to retrieve location");
            setNearbyLoading(false);
          }
        );
      } else {
        setNearbyError("Geolocation is not supported by this browser.");
        setNearbyLoading(false);
      }
    };

    getLocation();
  }, []);

  return (
    <div className="nearby pt-[100px] mx-auto md:w-full w-[279px]">
      <h1 className="font-bold text-[25px] text-[#484848] md:w-[339px] w-[279px] md:text-[36px]">
        Property Near you
      </h1>
      <div className="bg-[#484848] w-[140px] h-[6px] rounded-[3px] mt-[30px] mb-[60px]"></div>

      {nearbyLoading ? (
        <div>Loading...</div>
      ) : nearbyError ? (
        <div>{nearbyError}</div>
      ) : (
        <div className="flex gap-[30px] flex-wrap content-center items-center mx-auto">
          {nearbyProperties.map((property) => (
            <ListedCard
              key={property.propertyId}
              imgSrc={`${baseUrl}/${property.url}`}
              userSrc={`${baseUrl}/${property.hostProfileImgUrl}`}
              isFavourite={property.isLiked}
              name={property.name}
              adress={property.address}
              city={property.city}
              country={property.country}
            />
          ))}
        </div>
      )}
    </div>
  );
};

export default NearbyProperties;
