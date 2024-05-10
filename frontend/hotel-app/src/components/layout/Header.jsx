import Image from "next/image";
import React from "react";

function Header() {
  return (
    <div className="w-[1097px] mx-auto">
      <Image src="/images/logo.png" alt="logo" width={108} height={108} />
      <ul>
        <li>Find a Property</li>
        <li>Share Stories</li>
        <li>Rental Guides</li>
        <li>Download Mobile App</li>
      </ul>
      <div className="host">Become A Host</div>
      <div className="user"> </div>
    </div>
  );
}

export default Header;
