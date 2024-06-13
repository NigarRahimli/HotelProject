import Image from "next/image";
import React from "react";

function Footer() {
  return (
    <div className="font-montserrat bg-[#E8EAEC] pt-[12px]">
      <div className=" font-montserrat mx-auto font-semibold flex justify-between px-[10px] gap-x-[5px]  sm:w-[620px] md:w-[728px] lg:w-[994px] xl:w-[1210px] py-[33px]">
        <div className="news   flex justify-center gap-x-[68px]">
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
              <img
                src="./images/sendbtn.png"
                alt="menu"
                className="lg:w-[52px] lg:h-[52px]"
              />
            </a>
          </div>
        </div>
      </div>
      <div>
        <div className=" bg-[#EFF0F2] pt-[86px]">
          <div className="cats flex flex-col text-center gap-y-[25px] lg:text-left lg:flex-row mx-auto content-center  px-[10px] sm:w-[620px] md:w-[728px] lg:w-[994px] xl:w-[1210px]  pb-[180px] gap-x-[65px]">
            <div className="flex flex-col items-center  lg:items-start">
              <a href="#" className="flex items-center lg:items-start text-center ">
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
                <li> <a href="#">About us</a></li>
                <li><a href="#">Legal Information</a></li>
                <li><a href="#">Contact Us</a></li>
                <li><a href="#">Blogs</a></li>
              </ul>
            </div>
            <div className="text-[#484848]">
              <p className="text-[18px] font-extrabold">HELP CENTER</p>
              <ul className="text-[15px] flex flex-col gap-y-[20px] pt-[30px] font-medium">
                <li><a href="#">Find a Property</a></li>
                <li><a href="#">How To Host?</a></li>
                <li><a href="#">Why Us?</a></li>
                <li><a href="#">FAQs</a></li>
                <li><a href="#">Rental Guides</a></li>
              </ul>
            </div>
            <div className="text-[#484848]">
              <p className="text-[18px] font-extrabold">CONTACT INFO</p>
              <ul className="text-[15px] flex flex-col gap-y-[20px] pt-[30px] font-medium">
                <li>Phone: 1234567890</li>
                <li>Email: company@email.com</li>
                <li>Location: 100 Smart Street, LA, USA</li>
                <li className="flex lg:mx-0 mx-auto w-[171px] gap-x-[24px]">
               <a href="#"><img src="./images/fb.png" alt="" className="" /></a>   
                 <a href="#"><img src="./images/twitter.png" alt="" /></a> 
                 <a href="#"> <img src="./images/insta.png" alt="" /></a>
                 <a href="#"><img src="./images/linkedin.png" alt="" /></a> 
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
