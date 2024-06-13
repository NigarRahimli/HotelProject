import Image from "next/image";
import React from "react";

function Header() {
  return (
    <div className="bg-[#EFF0F2] py-[12px]">
      <div className="font-montserrat mx-auto font-semibold flex justify-between px-[10px] gap-x-[5px]  sm:w-[620px] md:w-[728px] lg:w-[994px] xl:w-[1210px]">
        <a href="./" className="flex items-center text-center md:w-[88px] xl:w-[108px] xl:h-[43px] xl:mr-[140px]">
          <img
            src="/images/logo.png"
            alt="logo"
            className="w-[78px] h-[20px] md:w-[88px] md:h-[25px] xl:w-[140px] xl:h-[35px]"
          />
        </a>
        <ul className="text-[#484848] flex items-center gap-x-[15px] text-[12px]  md:gap-x-[18px] xl:gap-x-[20px] md:text-[14px] xl:text-[16px]">
          <li className="hidden md:block"><a href="#">Find a Property</a></li>
          <li className="hidden md:block"><a href="#">Share Stories</a></li>
          <li className="hidden md:block"><a href="#">Rental Guides</a></li>
          <li className="hidden lg:block"><a href="#">Download Mobile App</a></li>
        </ul>
        <a href="./login" className="host text-white text-center bg-[#484848] rounded-[23px] cursor-pointer hover:bg-black focus:ring focus:ring-slate-200 xl:block xl:w-[186px] xl:p-[12px] xl:text-[14px] lg:block lg:w-[170px] lg:p-[10px] lg:text-[13px] hidden">
          Become A Host
        </a>
        <div className="user flex bg-white items-center px-[4px] rounded-[23px] gap-x-[10px]">
          <a href="#" >
            <img
              src="/icons/gg_menu.png"
              alt="menu"
              className="ml-[20px] w-[24px] h-[24px]"
            />
          </a>
          <a href="#" >
            <img
              src="/icons/vector.png"
              alt="vector"
              className=" w-[36px] h-[36px]"
            />
          </a>
        </div>
      </div>
    </div>
  );
}

export default Header;
