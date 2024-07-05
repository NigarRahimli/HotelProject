import React, { useState } from 'react';

const SearchForm = ({ kinds, activeKind, handleKindClick, capitalizeFirstLetter, handleSearch }) => {
  const [location, setLocation] = useState('');
  const [checkIn, setCheckIn] = useState('');
  const [checkOut, setCheckOut] = useState('');
  const [guests, setGuests] = useState(1);

  const handleSubmit = () => {
    const searchPayload = {
      checkInTime: checkIn,
      checkOutTime: checkOut,
      GuestNum: guests,
      KindId: activeKind,
      CityName: location,
    };
    handleSearch(searchPayload);
  };

  return (
    <div className="w-[100%] h-[600px] bg-center bg-[url('/images/hotel.avif')] bg-no-repeat bg-origin-padding bg-cover pt-[100px] md:pt-[380px] mx-auto">
      <div className="find text-[#101010] flex items-center content-center gap-x-[30px] mx-auto w-[300px] md:w-[690px] lg:w-[794px] md:pl-[30px]">
        <h1 className="font-bold text-[30px] md:text-[40px]">FIND</h1>
        <ul className="text-[14px] md:text-[16px] font-extrabold pt-[20px] sm:pt-0 flex flex-wrap items-center content-center gap-[10px]">
          {kinds.map((kind) => (
            <li
              key={kind.id}
              className={`relative ${activeKind === kind.id ? "after:content-[''] after:absolute after:left-0 after:right-1/2 after:-bottom-1 after:border-b-2 after:border-black" : ""}`}
              onClick={() => handleKindClick(kind.id)}
            >
              <h1 className={`${activeKind === kind.id ? "text-black" : "text-[#101010]"} cursor-pointer hover:text-black`}>
                {capitalizeFirstLetter(kind.name)}
              </h1>
            </li>
          ))}
        </ul>
      </div>
      <div className="search bg-white rounded-[35px] w-[300px] md:w-[690px] lg:w-[794px] mx-auto mt-[15px] font-semibold p-[10px] flex flex-col md:flex-row content-between gap-x-[10px] md:gap-x-[5px]">
        <div className="pl-[15px] py-[8px] location">
          <h2 className="text-[12px] text-[#0f0f0f]">Location</h2>
          <input
            placeholder="Which city do you prefer?"
            value={location}
            onChange={(e) => setLocation(e.target.value)}
            className="w-[223px] lg:w-[223px] md:w-[190px] outline-none text-[14px] text-[#484848] placeholder:text-[14px] placeholder:text-[#C2C6CC]"
          />
        </div>
        <div className="bar my-[10px] md:mt-[15px] bg-[#DDDDDD] h-[2px] w-full md:h-[30px] md:w-[1px]"></div>
        <div className="checkIn pl-[15px] md:pl-0 py-[8px]">
          <h2 className="text-[12px] text-[#0f0f0f]">Check In</h2>
          <input
            type="datetime-local"
            placeholder="Add Dates"
            value={checkIn}
            onChange={(e) => setCheckIn(e.target.value)}
            className="outline-none w-full text-[14px] text-[#484848] placeholder:text-[14px] placeholder:text-[#C2C6CC]"
          />
        </div>
        <div className="bar my-[10px] md:mt-[15px] bg-[#DDDDDD] h-[2px] w-full md:h-[30px] md:w-[1px]"></div>
        <div className="checkOut pl-[15px] md:pl-0 py-[8px]">
          <h2 className="text-[12px] text-[#0f0f0f]">Check Out</h2>
          <input
            type="datetime-local"
            placeholder="Add Dates"
            value={checkOut}
            onChange={(e) => setCheckOut(e.target.value)}
            className="outline-none w-full text-[14px] text-[#484848] placeholder:text-[14px] placeholder:text-[#C2C6CC]"
          />
        </div>
        <div className="bar my-[10px] md:mt-[15px] bg-[#DDDDDD] h-[2px] w-full md:h-[30px] md:w-[1px]"></div>
        <div className="guests pl-[15px] md:pl-0 py-[8px]">
          <h2 className="text-[12px] text-[#0f0f0f]">Guests</h2>
          <input
            type="number"
            min="1"
            placeholder="Add Guests"
            value={guests}
            onChange={(e) => setGuests(e.target.value)}
            className="outline-none w-full text-[14px] text-[#484848] placeholder:text-[14px] placeholder:text-[#C2C6CC]"
          />
        </div>
        <div
          className="button cursor-pointer ml-[230px] md:ml-0 hover:bg-black focus:ring focus:ring-slate-200 bg-[#484848] p-[15px] w-[54px] h-[54px] flex content-center items-center rounded-full"
          onClick={handleSubmit}
        >
          <img src="./icons/fe_search.png" alt="Search" />
        </div>
      </div>
    </div>
  );
};

export default SearchForm;
