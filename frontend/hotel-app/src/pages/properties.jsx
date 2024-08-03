import React, { useState, useEffect, useRef, useCallback } from "react";
import { useRouter } from "next/router";
import Layout from "./Layout";
import FilterCard from "@/components/card/FilterCard";
import PriceRangeSlider from "@/components/filter/PriceRangeSlider";
import SkeletonCard from "@/components/card/SkeletonCard";
import { baseUrl } from "@/components/constant";

function Properties() {
  const [isSidebarVisible, setSidebarVisible] = useState(false);
  const [kinds, setKinds] = useState([]);
  const [activeKindId, setActiveKindId] = useState(null);
  const [properties, setProperties] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [page, setPage] = useState(1);
  const [hasNext, setHasNext] = useState(true);
  const [checkInTime, setCheckInTime] = useState("");
  const [checkOutTime, setCheckOutTime] = useState("");
  const [guestNum, setGuestNum] = useState("");
  const [cityName, setCityName] = useState("");
  const sidebarRef = useRef(null);
  const footerRef = useRef(null);
  const observer = useRef();
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
    if (router.query.checkInTime) {
      setCheckInTime(router.query.checkInTime);
    }
    if (router.query.checkOutTime) {
      setCheckOutTime(router.query.checkOutTime);
    }
    if (router.query.guestNum) {
      setGuestNum(router.query.guestNum);
    }
    if (router.query.cityName) {
      setCityName(router.query.cityName);
    }
  }, [
    router.query.kindId,
    router.query.checkInTime,
    router.query.checkOutTime,
    router.query.guestNum,
    router.query.cityName,
  ]);

  const fetchProperties = useCallback(
    async (page) => {
      const {
        location,
        checkInDate,
        checkOutDate,
        guests,
        kindId,
        minPrice,
        maxPrice,
        checkInTime,
        checkOutTime,
        guestNum,
        cityName,
      } = router.query;
      if (!location || !checkInDate || !checkOutDate || !guests) {
        setLoading(false);
        return;
      }

      try {
        const response = await fetch(`${baseUrl}/api/properties/${page}/size/3`, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            checkInTime,
            checkOutTime,
            GuestNum: guestNum || guests,
            KindId: kindId,
            CityName: cityName || location,
            MinPrice: minPrice,
            MaxPrice: maxPrice,
          }),
        });

        if (response.status === 404) {
          setProperties([]);
          setError("Property not found with this filter");
        } else {
          const data = await response.json();
          setProperties((prev) => (page === 1 ? data.items : [...prev, ...data.items]));
          setHasNext(data.hasNext);
          setError(null);
        }
      } catch (error) {
        console.error("Error fetching properties:", error);
        setProperties([]);
        setError("An error occurred while fetching properties");
      }
      setLoading(false);
    },
    [router.query]
  );

  useEffect(() => {
    setPage(1);
    fetchProperties(1);
  }, [
    router.query.location,
    router.query.checkInDate,
    router.query.checkOutDate,
    router.query.guests,
    router.query.kindId,
    router.query.minPrice,
    router.query.maxPrice,
    router.query.checkInTime,
    router.query.checkOutTime,
    router.query.guestNum,
    router.query.cityName,
  ]);

  useEffect(() => {
    if (loading || !hasNext) return;

    const options = {
      root: null,
      rootMargin: "20px",
      threshold: 1.0,
    };

    observer.current = new IntersectionObserver((entries) => {
      if (entries[0].isIntersecting) {
        setPage((prev) => prev + 1);
      }
    }, options);

    if (footerRef.current) {
      observer.current.observe(footerRef.current);
    }

    return () => {
      if (observer.current && footerRef.current) {
        observer.current.unobserve(footerRef.current);
      }
    };
  }, [loading, hasNext]);

  useEffect(() => {
    if (page > 1) {
      fetchProperties(page);
    }
  }, [page]);

  const handleKindClick = (id) => {
    setActiveKindId(id);
    router.push({
      pathname: router.pathname,
      query: { ...router.query, kindId: id },
    });
  };

  const handlePriceChange = (newPriceRange) => {
    router.push({
      pathname: router.pathname,
      query: { ...router.query, minPrice: newPriceRange[0], maxPrice: newPriceRange[1] },
    });
  };

  const handleCheckInTimeChange = (event) => {
    setCheckInTime(event.target.value);
    router.push({
      pathname: router.pathname,
      query: { ...router.query, checkInTime: event.target.value },
    });
  };

  const handleCheckOutTimeChange = (event) => {
    setCheckOutTime(event.target.value);
    router.push({
      pathname: router.pathname,
      query: { ...router.query, checkOutTime: event.target.value },
    });
  };

  const handleGuestNumChange = (event) => {
    setGuestNum(event.target.value);
    router.push({
      pathname: router.pathname,
      query: { ...router.query, guestNum: event.target.value },
    });
  };

  const handleCityNameChange = (event) => {
    setCityName(event.target.value);
    router.push({
      pathname: router.pathname,
      query: { ...router.query, cityName: event.target.value },
    });
  };

  const minPrice = router.query.minPrice ? parseInt(router.query.minPrice, 10) : 0;
  const maxPrice = router.query.maxPrice ? parseInt(router.query.maxPrice, 10) : 3000;
  const today = new Date().toISOString().slice(0, 16); 
  const minCheckOutTime = checkInTime 
   ? new Date(new Date(checkInTime).getTime() + 24 * 60 * 60 * 1000).toISOString().slice(0, 16)
   : today;

  return (
    <Layout>
      <div className="mx-auto sm:w-[620px] md:w-[728px] lg:w-[994px] text-[#484848] py-[50px] lg:py-[90px] md:py-[60px] px-[15px] lg:px-0 xl:w-[1210px]">
        <div className="nav">
          <div className="flex justify-between items-center pb-[45px] mx-auto w-[279px] sm:w-full">
            <ul className="kinds font-semibold flex items-center content-center text-[14px] md:text-[16px] gap-x-[10px] md:gap-x-[20px]">
              {kinds.map((kind) => (
                <li
                  key={kind.id}
                  className={`relative cursor-pointer ${kind.id === activeKindId ? "border-b-2 border-[#484848]" : ""}`}
                  onClick={() => handleKindClick(kind.id)}
                >
                  <h1>{capitalizeFirstLetter(kind.name)}</h1>
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
          {loading && page === 1 ? (
            <>
              <SkeletonCard />
              <SkeletonCard />
              <SkeletonCard />
            </>
          ) : error ? (
            <p>{error}</p>
          ) :properties && properties.length > 0 ? (
            properties.map((property) => (
              <FilterCard
                key={property.propertyId}
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
        <div ref={footerRef} className="footer">
          {loading && page > 1 && (
            <>
              <SkeletonCard />
              <SkeletonCard />
              <SkeletonCard />
            </>
          )}
        </div>
        {isSidebarVisible && (
          <div
            ref={sidebarRef}
            className="sidebar bg-white fixed top-0 bottom-0 right-0 w-full sm:w-[400px] border-l border-gray-200 shadow-xl z-50"
          >
            <div className="p-6">
              <h2 className="text-xl font-bold mb-4">Filter Options</h2>
              <div>
                <label>Check-In Time:</label>
                <input
                  type="datetime-local"
                  min={today}
                  value={checkInTime}
                  onChange={handleCheckInTimeChange}
                  className="w-full p-2 border rounded"
                />
              </div>
              <div>
                <label>Check-Out Time:</label>
                <input
                  type="datetime-local"
                  min={minCheckOutTime}
                  value={checkOutTime}
                  onChange={handleCheckOutTimeChange}
                  className="w-full p-2 border rounded"
                />
              </div>
              <div>
                <label>Guest Number:</label>
                <input
                  type="number"
                  min={1}
                  value={guestNum}
                  onChange={handleGuestNumChange}
                  className="w-full p-2 border rounded"
                />
              </div>
              <div>
                <label>City Name:</label>
                <input
                  type="text"
                  value={cityName}
                  onChange={handleCityNameChange}
                  className="w-full p-2 border rounded"
                />
              </div>
              <PriceRangeSlider
                onPriceChange={handlePriceChange}
                minPrice={minPrice}
                maxPrice={maxPrice}
              />
            </div>
          </div>
        )}
      </div>
    </Layout>
  );
}

export default Properties;