import React from "react";

const StarCard = ({
  imgSrc,
  userSrc,
  isFavourite,
  name,
  address,
  city,
  country,
  star,
}) => {
  return (
    <div
      className="listed-card w-[279px] h-[340px] rounded-[8px] bg-center bg-no-repeat bg-origin-padding hover:bg-bottom transition-all duration-300 relative"
      style={{ backgroundImage: `url(${imgSrc})` }}
    >
      <img
        src={isFavourite ? "./icons/full_heart.png" : "./icons/empty_heart.png"}
        className="absolute right-[15px] top-[15px] cursor-pointer"
      />
      <div className="flex gap-x-[5px] absolute left-[15px] top-[15px]">
        {Array.from({ length: Math.floor(star) }, (_, index) => (
          <img key={index} src="./icons/star.png" />
        ))}
      </div>

      <div className="absolute flex flex-col bottom-0 left-0 p-[15px] w-full rounded-[2px] bg-[rgba(240,243,248,0.95)] z-10">
        <a href="#">
          <img src={userSrc} className="w-[70px] h-[70px] rounded-full" />
        </a>
        <a
          href="#"
          className="font-extrabold text-[17px] mt-[10px] text-[#484848]"
        >
          {name}
        </a>
        <a href="#" className="font-medium text-[13px] text-[#9A9A9A]">
          {address}, {city}, {country}
        </a>
      </div>
    </div>
  );
};

export default StarCard;
