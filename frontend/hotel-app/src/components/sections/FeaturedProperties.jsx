import React from 'react';
import FeaturedCard from '../card/FeaturedCard';


const FeaturedProperties = () => {
  return (
    <div className="featured pt-[100px] mx-auto md:w-full w-[279px]">
      <h1 className="font-bold text-[25px] text-[#484848] md:w-[412px] w-[279px] md:text-[36px] ">
        Featured Properties on our Listing
      </h1>
      <div className="bg-[#484848] w-[140px] h-[6px] rounded-[3px] mt-[30px] mb-[60px]"></div>

      <div className="flex gap-[30px] flex-wrap content-center items-center mx-auto">
        {[1, 2, 3, 4, 5].map((index) => (
          <div key={index} className="w-[300px] xl:w-[382px]">
            <FeaturedCard
              imgSrc="https://images.crowdspring.com/blog/wp-content/uploads/2017/08/23163415/pexels-binyamin-mellish-106399.jpg"
              userSrc="https://as2.ftcdn.net/v2/jpg/03/83/25/83/1000_F_383258331_D8imaEMl8Q3lf7EKU2Pi78Cn0R7KkW9o.jpg"
              isFavourite={false}
              name="Well Furnished Apartment"
              location="100 Smart Street, LA, USA"
              amenities={[
                {
                  id: 1,
                  iconUrl: "./icons/bed.png",
                  count: 3,
                },
                {
                  id: 2,
                  iconUrl: "./icons/bath.png",
                  count: 1,
                },
              ]}
            />
          </div>
        ))}
      </div>
    </div>
  );
};

export default FeaturedProperties;
