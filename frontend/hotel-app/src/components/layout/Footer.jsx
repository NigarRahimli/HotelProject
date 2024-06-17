import Image from "next/image";
import React from "react";

function Footer() {
  return (
    <div className="font-montserrat bg-[#E8EAEC] pt-[12px]">
      <div className=" font-montserrat mx-auto font-semibold flex justify-between px-[10px] gap-x-[5px]  sm:w-[620px] md:w-[728px] lg:w-[994px] xl:w-[1210px] py-[33px]">
        <div className="news  mx-auto flex  flex-col justify-center gap-x-[68px] gap-y-[15px] md:flex-row">
          <div className="text text-[#484848]">
            <p className="font-bold text-[18px]">NEWSLETTER</p>
            <p className="font-medium text-[14px]">Stay Upto Date</p>
          </div>
          <div className="company flex items-center   content-center bg-white lg:w-[794px] rounded-[26px] lg:h-[52px]">
            <input
              type="email"
              placeholder="Your Email..."
              className="flex ml-[25px] pr-[15px] placeholder:text-[14px] lg:h-[27px] outline-none w-full"
            />
            <a
              href="#"
              className="flex items-center text-center content-center "
            >
           <div className="p-[12px] bg-[#9A9A9A] hover:bg-[#484848] rounded-full w-[52px] h-[52px] transition-all duration-300">
            <img
                src="./icons/send.png"
                alt="send"
                className="w-[28px] h-[28px] "
              />
            </div>
            </a>
          </div>
        </div>
      </div>
      <div>
        <div className=" bg-[#EFF0F2] pt-[86px]">
          <div className="cats flex flex-col text-center gap-y-[25px] lg:text-left lg:flex-row mx-auto content-center  pr-[10px] sm:w-[620px] md:w-[728px] lg:w-[994px] xl:w-[1210px]  pb-[180px] gap-x-[65px]">
            <div className="flex flex-col items-center  lg:items-start">
              <a href="#" className="flex items-center lg:items-start text-center hover:font-bold hover:text-black">
                <img
                  src="/images/logo.png"
                  alt="menu"
                  className="w-[140px] h-[35px] mb-[40px]"
                />
              </a>

              <p className="w-[382px]">
                Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
                eiusmod tempor incididunt ut labore et dolore magna aliqua.{" "}
              </p>
            </div>
            <div className="text-[#484848]">
              <p className="text-[18px] font-extrabold">COMPANY</p>
              <ul className="text-[15px] flex flex-col gap-y-[20px] pt-[30px] font-medium">
                <li> <a href="#" className="hover:font-bold hover:text-black">About us</a></li>
                <li><a href="#" className="hover:font-bold hover:text-black">Legal Information</a></li>
                <li><a href="#" className="hover:font-bold hover:text-black">Contact Us</a></li>
                <li><a href="#" className="hover:font-bold hover:text-black">Blogs</a></li>
              </ul>
            </div>
            <div className="text-[#484848]">
              <p className="text-[18px] font-extrabold">HELP CENTER</p>
              <ul className="text-[15px] flex flex-col gap-y-[20px] pt-[30px] font-medium">
                <li><a href="#" className="hover:font-bold hover:text-black">Find a Property</a></li>
                <li><a href="#" className="hover:font-bold hover:text-black">How To Host?</a></li>
                <li><a href="#" className="hover:font-bold hover:text-black">Why Us?</a></li>
                <li><a href="#" className="hover:font-bold hover:text-black">FAQs</a></li>
                <li><a href="#" className="hover:font-bold hover:text-black">Rental Guides</a></li>
              </ul>
            </div>
            <div className="text-[#484848]">
              <p className="text-[18px] font-extrabold">CONTACT INFO</p>
              <ul className="text-[15px] flex flex-col gap-y-[20px] pt-[30px] font-medium">
                <li>Phone: 1234567890</li>
                <li>Email: company@email.com</li>
                <li>Location: 100 Smart Street, LA, USA</li>
                <li className="flex lg:mx-0 mx-auto w-[171px] gap-x-[24px]">
                  <a href="#" className="hover:text-black"><img src="./images/fb.png" alt="" className="" /></a>   
                  <a href="#" className="hover:text-black"><img src="./images/twitter.png" alt="" /></a> 
                  <a href="#" className="hover:text-black"> <img src="./images/insta.png" alt="" /></a>
                  <a href="#" className="hover:text-black"><img src="./images/linkedin.png" alt="" /></a> 
                </li>
              </ul>
            </div>
          </div>
        </div>
        <div className="  bg-[#EFF0F2] border-[#E0E2E6]  border-t-[1px] ">
          <div className="copyright text-[#484848] text-[10px] lg:text-[15px] flex justify-between  px-[10px] mx-auto font-semibold  sm:w-[620px] md:w-[728px] lg:w-[994px] xl:w-[1210px]  py-[30px]">
            <p>© 2022 | All rights reserved</p>
            <p>Created with love by Nigar Rahimli</p>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Footer;
