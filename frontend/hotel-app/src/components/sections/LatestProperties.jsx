import React, { useEffect, useState } from "react";
import ListedCard from "@/components/card/ListedCard";
import { baseUrl } from "@/components/constant";

const LatestProperties = () => {
  const [latestProperties, setLatestProperties] = useState([]);
  const [latestLoading, setLatestLoading] = useState(true);
  const [latestError, setLatestError] = useState(null);

  useEffect(() => {
    const fetchLatestProperties = async () => {
      try {
        const response = await fetch(`${baseUrl}/api/properties/latest/4`);
        const data = await response.json();
        setLatestProperties(data);
      } catch (error) {
        setLatestError("Error fetching latest properties");
      } finally {
        setLatestLoading(false);
      }
    };

    fetchLatestProperties();
  }, []);

  return (
    <div className="latest pt-[100px] mx-auto md:w-full w-[279px]">
      <h1 className="font-bold text-[25px] text-[#484848] md:w-[339px] w-[279px] md:text-[36px]">
        Latest on the Property Listing
      </h1>
      <div className="bg-[#484848] w-[140px] h-[6px] rounded-[3px] mt-[30px] mb-[60px]"></div>

      {latestLoading ? (
        <div>Loading...</div>
      ) : latestError ? (
        <div>{latestError}</div>
      ) : (
        <div className="flex gap-[30px] flex-wrap content-center items-center mx-auto">
          {latestProperties.map((property) => (
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

export default LatestProperties;
