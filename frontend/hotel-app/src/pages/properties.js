import React, { useState, useEffect, useRef } from "react";
import Layout from "./Layout";
import FilterCard from "@/components/card/FilterCard";
import PriceRangeSlider from "@/components/filter/PriceRangeSlider ";

function Index() {
  const [isSidebarVisible, setSidebarVisible] = useState(false);
  const sidebarRef = useRef(null);

  const toggleSidebar = () => {
    setSidebarVisible(!isSidebarVisible);
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

  return (
    <Layout>
      <div className="mx-auto sm:w-[620px] md:w-[728px] lg:w-[994px] text-[#484848] py-[50px] lg:py-[90px] md:py-[60px] px-[15px] lg:px-0 xl:w-[1210px]">
        <div className="nav">
          <div className="flex justify-between items-center pb-[45px] mx-auto w-[279px] sm:w-full">
            <ul className="font-semibold flex items-center content-center text-[14px] md:text-[16px] gap-x-[10px] md:gap-x-[20px]">
              <li className="relative cursor-pointer">
                <h1 className="after:content-[''] after:absolute after:left-0 after:right-1/2 after:-bottom-2 after:border-b-[3px] after:border-[#484848]">
                  Appartments
                </h1>
              </li>
              <li className="bg-[#C2C6CC] w-[6px] h-[6px] rounded-full md:block hidden"></li>
              <li className="relative cursor-pointer hover:text-black md:block hidden">
                <h1>Houses</h1>
              </li>
              <li className="bg-[#C2C6CC] w-[6px] h-[6px] rounded-full md:block hidden"></li>
              <li className="relative cursor-pointer hover:text-black md:block hidden">
                <h1>Villas</h1>
              </li>
              <li className="bg-[#C2C6CC] w-[6px] h-[6px] rounded-full md:block hidden"></li>
              <li className="relative cursor-pointer hover:text-black md:block hidden">
                <h1>Homestays</h1>
              </li>
              <li className="bg-[#C2C6CC] w-[6px] h-[6px] rounded-full md:block hidden"></li>
              <li className="relative cursor-pointer hover:text-black md:block hidden">
                <h1>More</h1>
              </li>
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
          <div className="w-[300px] xl:w-[382px]">
            <FilterCard
              imgSrc="https://images.crowdspring.com/blog/wp-content/uploads/2017/08/23163415/pexels-binyamin-mellish-106399.jpg"
              userSrc="https://as2.ftcdn.net/v2/jpg/03/83/25/83/1000_F_383258331_D8imaEMl8Q3lf7EKU2Pi78Cn0R7KkW9o.jpg"
              isFavourite="true"
              name="Well Furnished Apartment"
              location="100 Smart Street, LA, USA"
            />
          </div>
          <div className="w-[300px] xl:w-[382px]">
            <FilterCard
              imgSrc="https://images.crowdspring.com/blog/wp-content/uploads/2017/08/23163415/pexels-binyamin-mellish-106399.jpg"
              userSrc="https://as2.ftcdn.net/v2/jpg/03/83/25/83/1000_F_383258331_D8imaEMl8Q3lf7EKU2Pi78Cn0R7KkW9o.jpg"
              isFavourite="true"
              name="Well Furnished Apartment"
              location="100 Smart Street, LA, USA"
            />
            
          </div>
          <div className="w-[300px] xl:w-[382px]">
            <FilterCard
              imgSrc="https://images.crowdspring.com/blog/wp-content/uploads/2017/08/23163415/pexels-binyamin-mellish-106399.jpg"
              userSrc="https://as2.ftcdn.net/v2/jpg/03/83/25/83/1000_F_383258331_D8imaEMl8Q3lf7EKU2Pi78Cn0R7KkW9o.jpg"
              isFavourite="true"
              name="Well Furnished Apartment"
              location="100 Smart Street, LA, USA"
            />
            
          </div>
          <div className="w-[300px] xl:w-[382px]">
            <FilterCard
              imgSrc="https://images.crowdspring.com/blog/wp-content/uploads/2017/08/23163415/pexels-binyamin-mellish-106399.jpg"
              userSrc="https://as2.ftcdn.net/v2/jpg/03/83/25/83/1000_F_383258331_D8imaEMl8Q3lf7EKU2Pi78Cn0R7KkW9o.jpg"
              isFavourite="true"
              name="Well Furnished Apartment"
              location="100 Smart Street, LA, USA"
            />
            
          </div>
          <div className="w-[300px] xl:w-[382px]">
            <FilterCard
              imgSrc="https://images.crowdspring.com/blog/wp-content/uploads/2017/08/23163415/pexels-binyamin-mellish-106399.jpg"
              userSrc="https://as2.ftcdn.net/v2/jpg/03/83/25/83/1000_F_383258331_D8imaEMl8Q3lf7EKU2Pi78Cn0R7KkW9o.jpg"
              isFavourite="true"
              name="Well Furnished Apartment"
              location="100 Smart Street, LA, USA"
            />
            
          </div>
          
         
        </div>
        {isSidebarVisible && (
          <div className="fixed inset-0 bg-gray-900 bg-opacity-50 z-50">
            <div
              ref={sidebarRef}
              className="fixed right-0 top-0 h-full w-[300px] bg-white shadow-lg z-50"
            >
              <button
                className="absolute top-[15px] right-[15px]"
                onClick={toggleSidebar}
              >
                <img src="./icons/x.png" className="w-[35px] h-[35px] "/>
              </button>
              
              <div className="p-4">
  <h2 className="text-xl font-bold mb-4">Filters</h2>
  <div className="mb-4">
    <label htmlFor="location" className="block font-semibold mb-1">Location</label>
    <input type="text" id="location" className="w-full border border-gray-300 rounded px-3 py-2" placeholder="Enter location" />
  </div>
  <div className="mb-4">
    <label htmlFor="propertyType" className="block font-semibold mb-1">Property Type</label>
    <select id="propertyType" className="w-full border border-gray-300 rounded px-3 py-2">
      <option value="">Any</option>
      <option value="apartment">Apartment</option>
      <option value="house">House</option>
      <option value="villa">Villa</option>
    </select>
  </div>
  <div className="mb-4">
   <PriceRangeSlider/>
  </div>
  <button className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600">Apply Filters</button>
</div>

            </div>
          </div>
        )}
      </div>
    </Layout>
  );
}

export default Index;
