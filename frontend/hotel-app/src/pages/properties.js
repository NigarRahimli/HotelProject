import React from "react";
import Layout from "./Layout";

import FilterCard from "@/components/card/FilterCard";

function index() {
  return (
    <Layout>
      <div className="mx-auto sm:w-[620px] md:w-[728px] lg:w-[994px] text-[#484848]  py-[50px] lg:py-[90px] md:py-[60px] px-[15px] lg:px-0  xl:w-[1210px]">
        <div className="nav">
          <div className="flex  justify-between  items-center  pb-[45px] mx-auto w-[279px] sm:w-full">
            <ul className=" font-semibold flex  items-center content-center text-[14px] md:text-[16px] gap-x-[10px] md:gap-x-[20px] ">
              <li className="relative cursor-pointer">
                <h1 className="after:content-[''] after:absolute after:left-0 after:right-1/2 after:-bottom-2 after:border-b-[3px] after:border-[#484848]">
                  Appartments
                </h1>
              </li>
              <li className="bg-[#C2C6CC] w-[6px] h-[6px] rounded-full md:block hidden"></li>
              <li className="relative cursor-pointer hover:text-black  md:block hidden">
                <h1>Houses</h1>
              </li>
              <li className="bg-[#C2C6CC] w-[6px] h-[6px] rounded-full  md:block hidden" ></li>
              <li className="relative cursor-pointer hover:text-black  md:block hidden">
                <h1>Villas</h1>
              </li>
              <li className="bg-[#C2C6CC] w-[6px] h-[6px] rounded-full  md:block hidden"></li>
              <li className="relative cursor-pointer hover:text-black  md:block hidden">
                <h1>Homestays</h1>
              </li>
              <li className="bg-[#C2C6CC] w-[6px] h-[6px] rounded-full  md:block hidden"></li>

              <li className="relative cursor-pointer hover:text-black  md:block hidden">
                <h1>More</h1>
              </li>
            </ul>
            <div className="flex p-[10px] items-center justify-center  gap-x-[5px] border-[1px] border-[#484848] w-[146px] rounded-[24px] hover:bg-[#EFF0F2]  hover:border-black">
              <img src="./icons/filter-icon.png" />
              <p className=" font-semibold text-[14px]">Filters</p>
            </div>
          </div>
        </div>
        <div className="flex gap-[30px] flex-wrap content-center items-center mx-auto sm:w-full w-[279px]">
          <div className="w-[300px]  xl:w-[382px]">
            <FilterCard
              imgSrc="https://images.crowdspring.com/blog/wp-content/uploads/2017/08/23163415/pexels-binyamin-mellish-106399.jpg"
              userSrc="https://as2.ftcdn.net/v2/jpg/03/83/25/83/1000_F_383258331_D8imaEMl8Q3lf7EKU2Pi78Cn0R7KkW9o.jpg"
              isFavourite="true"
              name="Well Furnished Apartment"
              location="100 Smart Street, LA, USA"
            />
          </div>
          <div className="w-[300px]  xl:w-[382px]">
            <FilterCard
              imgSrc="https://images.crowdspring.com/blog/wp-content/uploads/2017/08/23163415/pexels-binyamin-mellish-106399.jpg"
              userSrc="https://as2.ftcdn.net/v2/jpg/03/83/25/83/1000_F_383258331_D8imaEMl8Q3lf7EKU2Pi78Cn0R7KkW9o.jpg"
              isFavourite="true"
              name="Well Furnished Apartment"
              location="100 Smart Street, LA, USA"
            />
          </div>
          <div className="w-[300px]  xl:w-[382px]">
            <FilterCard
              imgSrc="https://images.crowdspring.com/blog/wp-content/uploads/2017/08/23163415/pexels-binyamin-mellish-106399.jpg"
              userSrc="https://as2.ftcdn.net/v2/jpg/03/83/25/83/1000_F_383258331_D8imaEMl8Q3lf7EKU2Pi78Cn0R7KkW9o.jpg"
              isFavourite="true"
              name="Well Furnished Apartment"
              location="100 Smart Street, LA, USA"
            />
          </div>
          <div className="w-[300px]  xl:w-[382px]">
            <FilterCard
              imgSrc="https://images.crowdspring.com/blog/wp-content/uploads/2017/08/23163415/pexels-binyamin-mellish-106399.jpg"
              userSrc="https://as2.ftcdn.net/v2/jpg/03/83/25/83/1000_F_383258331_D8imaEMl8Q3lf7EKU2Pi78Cn0R7KkW9o.jpg"
              isFavourite="true"
              name="Well Furnished Apartment"
              location="100 Smart Street, LA, USA"
            />
          </div>
        </div>
      </div>
    </Layout>
  );
}

export default index;
