import React, { useState, useEffect, useRef } from "react";

function Header() {
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const menuRef = useRef(null);

  useEffect(() => {
    function handleClickOutside(event) {
      if (menuRef.current && !menuRef.current.contains(event.target)) {
        setIsMenuOpen(false);
      }
    }

    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, []);

  return (
    <div className="bg-[#EFF0F2] py-[12px] ">
      <div className="font-montserrat mx-auto font-semibold flex justify-between px-[10px] gap-x-[5px]  sm:w-[620px] md:w-[728px] lg:w-[994px] xl:w-[1210px]">
        <a href="./" className="flex items-center text-center md:w-[88px] xl:w-[108px] xl:h-[43px] xl:mr-[140px]">
          <img
            src="/images/logo.png"
            alt="logo"
            className="w-[78px] h-[20px] md:w-[88px] md:h-[25px] xl:w-[140px] xl:h-[35px]"
          />
        </a>
        <ul className="text-[#484848] flex items-center gap-x-[15px] text-[12px]  md:gap-x-[18px] xl:gap-x-[20px] md:text-[14px] xl:text-[16px]">
          <li className="hidden md:block hover:font-bold hover:text-black"><a href="#">Find a Property</a></li>
          <li className="hidden md:block hover:font-bold hover:text-black"><a href="#">Share Stories</a></li>
          <li className="hidden md:block hover:font-bold hover:text-black"><a href="#">Rental Guides</a></li>
          <li className="hidden lg:block hover:font-bold hover:text-black"><a href="#">Download Mobile App</a></li>
        </ul>
        <a href="./login" className="host text-white text-center bg-[#484848] rounded-[23px] cursor-pointer hover:bg-black focus:ring focus:ring-slate-200 xl:block xl:w-[186px] xl:p-[12px] xl:text-[14px] lg:block lg:w-[170px] lg:p-[10px] lg:text-[13px] transition-all duration-300 hidden">
          Become A Host
        </a>
        <div ref={menuRef} className="user flex bg-white bg-red items-center px-[4px] rounded-[23px] gap-x-[10px] relative top-0" onClick={() => setIsMenuOpen(!isMenuOpen)}>
          <a href="#" className="menu">
            <img
              src="/icons/gg_menu.png"
              alt="menu"
              className="ml-[20px] w-[24px] h-[24px]"
            />
          </a>
          <a href="#">
            <img
              src="/icons/vector.png"
              alt="vector"
              className=" w-[36px] h-[36px]"
            />
          </a>
          <div className={`options text-end bg-white flex flex-col gap-y-[10px]  top-[30px] right-[10px] xl:top-[40px] xl:right-[10px] text-[#484848] font-medium  text-[14px] rounded-[6px] shadow lg:w-[180px] z-50 w-[190px] p-[15px] absolute transition-opacity duration-300 ${isMenuOpen ? 'opacity-100 visible ' : 'opacity-0 invisible'}`}>
            <a href="./signup" className="hover:text-black hover:font-bold">Sign up</a>
            <a href="./signin" className="hover:text-black hover:font-bold">Login</a>
            <a href="#" className="hover:text-black hover:font-bold">Help center</a>
            <a href="#" className="md:hidden border-t-[#d6d6d6] border-t-[1px] mt-[10px] pt-[10px] hover:text-black hover:font-bold">Find a Property</a>
            <a href="#" className="md:hidden hover:text-black hover:font-bold">Share Stories</a>
            <a href="#" className="md:hidden hover:text-black hover:font-bold">Rental Guides</a>
            <a href="#" className="lg:hidden hover:text-black hover:font-bold">Download Mobile App</a>
            <a href="./login" className="host text-[#484848] font-bold cursor-pointer hover:text-black focus:ring focus:ring-slate-200 lg:hidden  mt-[15px] ">
              Become A Host
            </a>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Header;
