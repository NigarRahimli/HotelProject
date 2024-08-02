import React, { useState, useEffect, useRef } from "react";
import { useRouter } from "next/router"; // Import useRouter
import Layout from "./Layout";
import FilterCard from "@/components/card/FilterCard";
import PriceRangeSlider from "@/components/filter/PriceRangeSlider ";
import { baseUrl } from "@/components/constant";

function Properties() {
  const [isSidebarVisible, setSidebarVisible] = useState(false);
  const [kinds, setKinds] = useState([]);
  const [activeKindId, setActiveKindId] = useState(null);
  const [properties, setProperties] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null); // State for error message
  const sidebarRef = useRef(null);
  const router = useRouter();

  const toggleSidebar = () => {
    setSidebarVisible(!isSidebarVisible);
  };

  const capitalizeFirstLetter = (string) => {
    return string.charAt(0).toUpperCase() + string.slice(1).toLowerCase();
  };

  const handleClickOutside = (event) => {
    if (sidebarRef.current && !sidebarRef.current.contains(event.target)) {
      setSidebarVisible(false);
    }
  };

  useEffect(() => {
    if (isSidebarVisible) {
      document.addEventListener("mousedown", handleClickOutside);
    } else {
      document.removeEventListener("mousedown", handleClickOutside);
    }

    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, [isSidebarVisible]);

  useEffect(() => {
    const fetchKinds = async () => {
      try {
        const response = await fetch(`${baseUrl}/api/kinds`);
        const data = await response.json();
        setKinds(data);
      } catch (error) {
        console.error("Error fetching kinds:", error);
      }
    };

    fetchKinds();
  }, []);

  useEffect(() => {
    if (router.query.kindId) {
      setActiveKindId(parseInt(router.query.kindId, 10));
    }
  }, [router.query.kindId]);

  useEffect(() => {
    const fetchProperties = async () => {
      const { location, checkInDate, checkOutDate, guests, kindId } = router.query;
      if (!location || !checkInDate || !checkOutDate || !guests) {
        setLoading(false);
        return; 
      }
      
      try {
        const response = await fetch(
          `${baseUrl}/api/properties/1/size/3`,
          {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify({
              checkInTime: checkInDate,
              checkOutTime: checkOutDate,
              GuestNum: guests,
              KindId: kindId,
              CityName: location,
            }),
          }
        );

        if (response.status === 404) {
          setProperties([]);
          setError("Property not found with this filter");
        } else {
          const data = await response.json();
          setProperties(data.items);
          setError(null); // Clear error if data is found
        }
      } catch (error) {
        console.error("Error fetching properties:", error);
        setProperties([]);
        setError("An error occurred while fetching properties");
      }
      setLoading(false);
    };

    fetchProperties();
  }, [router.query.location, router.query.checkInDate, router.query.checkOutDate, router.query.guests, router.query.kindId]);

  const handleKindClick = (id) => {
    setActiveKindId(id);
    router.push({
      pathname: router.pathname,
      query: { ...router.query, kindId: id }
    });
  };

  return (
    <Layout>
      <div className="mx-auto sm:w-[620px] md:w-[728px] lg:w-[994px] text-[#484848] py-[50px] lg:py-[90px] md:py-[60px] px-[15px] lg:px-0 xl:w-[1210px]">
        <div className="nav">
          <div className="flex justify-between items-center pb-[45px] mx-auto w-[279px] sm:w-full">
            <ul className="kinds font-semibold flex items-center content-center text-[14px] md:text-[16px] gap-x-[10px] md:gap-x-[20px]">
              {kinds.map((kind) => (
                <li
                  key={kind.id}
                  className={`relative cursor-pointer ${kind.id === activeKindId ? 'border-b-2 border-[#484848]' : ''}`}
                  onClick={() => handleKindClick(kind.id)}
                >
                  <h1>
                    {capitalizeFirstLetter(kind.name)}
                  </h1>
                </li>
              ))}
            </ul>
            <div
              className="flex p-[10px] items-center justify-center gap-x-[5px] border-[1px] border-[#484848] w-[146px] rounded-[24px] hover:bg-[#EFF0F2] hover:border-black cursor-pointer"
              onClick={toggleSidebar}
            >
              <img src="./icons/filter-icon.png" alt="Filter Icon" />
              <p className="font-semibold text-[14px]">Filters</p>
            </div>
          </div>
        </div>
        <div className="flex gap-[30px] flex-wrap content-center items-center mx-auto sm:w-full w-[279px]">
          {loading ? (
            <p>Loading...</p>
          ) : error ? ( 
            <p>{error}</p>
          ) : properties.length > 0 ? (
            properties.map((property) => (
              <FilterCard
                key={property.id}
                imgSrc={property.mainImageUrl}
                userSrc={property.userImageUrl}
                isFavourite={property.isFavourite}
                name={property.name}
                location={property.location}
                minPrice={property.minPrice}
                maxPrice={property.maxPrice}
                propertyImageDetails={property.propertyImageDetails || []}
              />
            ))
          ) : (
            <p>No properties found</p>
          )}
        </div>
        {isSidebarVisible && (
          <div
            ref={sidebarRef}
            className="sidebar bg-white fixed top-0 bottom-0 right-0 w-full sm:w-[400px] border-l border-gray-200 shadow-xl z-50"
          >
            <div className="p-6">
              <h2 className="text-xl font-bold mb-4">Filter Options</h2>
              <PriceRangeSlider />
              {/* Add more filter options here */}
            </div>
          </div>
        )}
      </div>
    </Layout>
  );
}

export default Properties;
