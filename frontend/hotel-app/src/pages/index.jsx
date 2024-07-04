import React, { useEffect, useState } from "react";
import Layout from "./Layout";
import ListedCard from "@/components/card/ListedCard";
import FeaturedCard from "@/components/card/FeaturedCard";
import StarCard from "@/components/card/StarCard";
import ArticleCard from "@/components/card/ArticleCard";
import { baseUrl, colors } from "@/components/constant";
function Index() {
  const [kinds, setKinds] = useState([]);
  const [activeKind, setActiveKind] = useState(null); // State to track the active kind

  useEffect(() => {
    fetch(`${baseUrl}/api/kinds`)
      .then((response) => response.json())
      .then((data) => setKinds(data))
      .catch((error) => console.error("Error fetching kinds:", error));
  }, []);

  const handleKindClick = (id) => {
    setActiveKind(id);
  };

  const [latestProperties, setLatestProperties] = useState([]);
  const [latestLoading, setLatestLoading] = useState(true);
  const [latestError, setLatestError] = useState(null);

  useEffect(() => {
    const fetchLatestProperties = async () => {
      try {
        const response = await fetch(`${baseUrl}/api/properties/latest/4`);
        const data = await response.json();
        setLatestProperties(data);
      } catch (error) {
        setLatestError("Error fetching latest properties");
      } finally {
        setLatestLoading(false);
      }
    };

    fetchLatestProperties();
  }, []);

  const [properties, setProperties] = useState([]);
  const [nearbyLoading, setNearbyLoading] = useState(true);
  const [nearbyError, setNearbyError] = useState(null);

  useEffect(() => {
    const fetchProperties = async (latitude, longitude) => {
      try {
        const response = await fetch(`${baseUrl}/api/properties/nearby`, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            Latitude: latitude,
            Longitude: longitude,
            Take: 4,
          }),
        });
        const data = await response.json();
        setProperties(data);
      } catch (error) {
        setNearbyError("Error fetching nearby properties");
      } finally {
        setNearbyLoading(false);
      }
    };

    const getLocation = () => {
      if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
          (position) => {
            const { latitude, longitude } = position.coords;
            fetchProperties(latitude, longitude);
          },
          (error) => {
            console.error("Error getting location:", error);
            setNearbyError("Unable to retrieve location");
            setNearbyLoading(false);
          }
        );
      } else {
        setNearbyError("Geolocation is not supported by this browser.");
        setNearbyLoading(false);
      }
    };

    getLocation();
  }, []);

  return (
    <Layout>
      <div className="w-[100%] h-[600px] bg-center bg-[url('/images/hotel.avif')] bg-no-repeat bg-origin-padding bg-cover pt-[100px] md:pt-[380px] mx-auto">
        <div className="find text-[#101010] flex items-center content-center gap-x-[30px] mx-auto w-[300px] md:w-[690px] lg:w-[794px] md:pl-[30px] ">
          <h1 className="font-bold text-[30px] md:text-[40px] ">FIND</h1>
          <ul className=" text-[14px] md:text-[16px] font-extrabold pt-[20px] sm:pt-0 flex flex-wrap items-center content-center gap-[10px] ">
            {kinds.map((kind) => (
              <li
                key={kind.id}
                className={`relative ${
                  activeKind === kind.id
                    ? "after:content-[''] after:absolute after:left-0 after:right-1/2 after:-bottom-1 after:border-b-2 after:border-black"
                    : ""
                }`}
                onClick={() => handleKindClick(kind.id)}
              >
                <h1
                  className={`${
                    activeKind === kind.id ? "text-black" : "text-[#101010]"
                  } cursor-pointer hover:text-black`}
                >
                  {kind.name.toUpperCase()}
                </h1>
              </li>
            ))}
          </ul>
        </div>
        <div className="search bg-white rounded-[35px] w-[300px] md:w-[690px] lg:w-[794px] mx-auto mt-[15px] font-semibold p-[10px]  flex flex-col md:flex-row content-between gap-x-[10px] md:gap-x-[5px]">
          <div className="pl-[15px] py-[8px] location">
            <h2 className=" text-[12px] text-[#0f0f0f] ">Location</h2>
            <input
              placeholder="Which city do you prefer?"
              className=" w-[223px] lg:w-[223px]  md:w-[190px] outline-none text-[14px] text-[#484848] placeholder:text-[14px] placeholder:text-[#C2C6CC] "
            />
          </div>
          <div className="bar my-[10px] md:mt-[15px] bg-[#DDDDDD] h-[2px] w-full md:h-[30px] md:w-[1px]"></div>
          <div className="checkIn  pl-[15px] md:pl-0 py-[8px]">
            <h2 className=" text-[12px] text-[#0f0f0f]">Check In</h2>
            <input
              placeholder="Add Dates"
              className="outline-none w-full text-[14px] text-[#484848] placeholder:text-[14px] placeholder:text-[#C2C6CC] "
            />
          </div>
          <div className="bar my-[10px] md:mt-[15px] bg-[#DDDDDD] h-[2px] w-full md:h-[30px] md:w-[1px]"></div>
          <div className="checkOut  pl-[15px] md:pl-0 py-[8px]">
            <h2 className=" text-[12px] text-[#0f0f0f]">Check Out</h2>
            <input
              placeholder="Add Dates"
              className="outline-none w-full text-[14px] text-[#484848] placeholder:text-[14px] placeholder:text-[#C2C6CC] "
            />
          </div>
          <div className="bar my-[10px] md:mt-[15px] bg-[#DDDDDD] h-[1px] w-full md:h-[30px] md:w-[1px]"></div>
          <div className="guests  pl-[15px] md:pl-0  py-[8px]">
            <h2 className=" text-[12px] text-[#0f0f0f]">Guests</h2>
            <input
              placeholder="Add Guests"
              className="outline-none w-full text-[14px] text-[#484848] placeholder:text-[14px] placeholder:text-[#C2C6CC] "
            />
          </div>
          <div className="button cursor-pointer ml-[230px] md:ml-0 hover:bg-black focus:ring focus:ring-slate-200  bg-[#484848] p-[15px] w-[54px] h-[54px] flex content-center items-center rounded-full">
            <img src="./icons/fe_search.png" />
          </div>
        </div>
      </div>
      <div className="mx-auto sm:w-[620px] md:w-[728px] lg:w-[994px] xl:w-[1210px]">
      <div className="latest pt-[100px] mx-auto md:w-full w-[279px]">
          <h1 className="font-bold text-[25px] text-[#484848] md:w-[339px] w-[279px] md:text-[36px]">
            Latest on the Property Listing
          </h1>
          <div className="bg-[#484848] w-[140px] h-[6px] rounded-[3px] mt-[30px] mb-[60px]"></div>

          {latestLoading ? (
            <div>Loading...</div>
          ) : latestError ? (
            <div>{latestError}</div>
          ) : (
            <div className="flex gap-[30px] flex-wrap content-center items-center mx-auto">
              {latestProperties.map((property) => (
                <ListedCard
                  key={property.propertyId}
                  imgSrc={`${baseUrl}/${property.url}`}
                  userSrc={`${baseUrl}/${property.hostProfileImgUrl}`}
                  isFavourite={property.isLiked}
                  name={property.name}
                  adress={property.address}
                  city={property.city}
                  country={property.country}
                />
              ))}
            </div>
          )}
        </div>
    <div className="nearby pt-[100px] mx-auto md:w-full w-[279px]">
          <h1 className="font-bold text-[25px] text-[#484848] md:w-[339px] w-[279px] md:text-[36px]">
            Nearby Listed Properties
          </h1>
          <div className="bg-[#484848] w-[140px] h-[6px] rounded-[3px] mt-[30px] mb-[60px]"></div>

          {nearbyLoading ? (
            <div>Loading...</div>
          ) : nearbyError ? (
            <div>{nearbyError}</div>
          ) : (
            <div className="flex gap-[30px] flex-wrap content-center items-center mx-auto">
              {properties.map((property) => (
                <ListedCard
                  key={property.propertyId}
                  imgSrc={`${baseUrl}/${property.url}`}
                  userSrc={`${baseUrl}/${property.hostProfileImgUrl}`}
                  isFavourite={property.isLiked}
                  name={property.name}
                  adress={property.address}
                  city={property.city}
                  country={property.country}
                />
              ))}
            </div>
          )}
        </div>
        <div className="top pt-[100px] mx-auto md:w-full w-[279px]">
          <h1 className="font-bold text-[25px] text-[#484848] md:w-[339px] w-[279px] md:text-[36px] ">
            Top Rated Properties
          </h1>
          <div className="bg-[#484848] w-[140px] h-[6px] rounded-[3px] mt-[30px] mb-[60px]"></div>

          <div className="flex gap-[30px] flex-wrap content-center items-center mx-auto">
            <StarCard
              imgSrc="https://images.crowdspring.com/blog/wp-content/uploads/2017/08/23163415/pexels-binyamin-mellish-106399.jpg"
              userSrc="https://as2.ftcdn.net/v2/jpg/03/83/25/83/1000_F_383258331_D8imaEMl8Q3lf7EKU2Pi78Cn0R7KkW9o.jpg"
              isFavourite="true"
              name="Well Furnished Apartment"
              location="100 Smart Street, LA, USA"
            />

            <StarCard
              imgSrc="https://images.crowdspring.com/blog/wp-content/uploads/2017/08/23163415/pexels-binyamin-mellish-106399.jpg"
              userSrc="https://as2.ftcdn.net/v2/jpg/03/83/25/83/1000_F_383258331_D8imaEMl8Q3lf7EKU2Pi78Cn0R7KkW9o.jpg"
              isFavourite="true"
              name="Well Furnished Apartment"
              location="100 Smart Street, LA, USA"
            />
            <StarCard
              imgSrc="https://images.crowdspring.com/blog/wp-content/uploads/2017/08/23163415/pexels-binyamin-mellish-106399.jpg"
              userSrc="https://as2.ftcdn.net/v2/jpg/03/83/25/83/1000_F_383258331_D8imaEMl8Q3lf7EKU2Pi78Cn0R7KkW9o.jpg"
              isFavourite="true"
              name="Well Furnished Apartment"
              location="100 Smart Street, LA, USA"
            />
            <StarCard
              imgSrc="https://images.crowdspring.com/blog/wp-content/uploads/2017/08/23163415/pexels-binyamin-mellish-106399.jpg"
              userSrc="https://as2.ftcdn.net/v2/jpg/03/83/25/83/1000_F_383258331_D8imaEMl8Q3lf7EKU2Pi78Cn0R7KkW9o.jpg"
              isFavourite="true"
              name="Well Furnished Apartment"
              location="100 Smart Street, LA, USA"
            />
          </div>
        </div>

        <div className=" host h-[395px] w-[279px] md:w-full mt-[80px] rounded-[12px] flex flex-col gap-y-[30px] py-[80px] px-[50px] mx-auto md:mx-0 bg-center bg-[url('/images/blue.jpg')] bg-no-repeat bg-origin-padding hover:bg-bottom">
          <h1 className="font-bold text-[25px] text-[#101010] md:w-[320px] w-full md:text-[38px] mx-auto md:mx-0 ">
            Try Hosting With Us
          </h1>
          <p className="font-medium text-[16px] text-[#e2e0e0] ">
            Earn extra just by renting your property...
          </p>
          <a
            href="./login"
            className="host text-white text-center mt-[40px] mb-[30px] bg-[#484848] rounded-[23px] cursor-pointer hover:bg-black focus:ring focus:ring-slate-200 w-[186px] p-[12px] text-[14px] block "
          >
            Become A Host
          </a>
        </div>

        <div className="featured pt-[100px] mx-auto md:w-full w-[279px]">
          <h1 className="font-bold text-[25px] text-[#484848] md:w-[412px] w-[279px] md:text-[36px] ">
            Featured Properties on our Listing
          </h1>
          <div className="bg-[#484848] w-[140px] h-[6px] rounded-[3px] mt-[30px] mb-[60px]"></div>

          <div className="flex gap-[30px] flex-wrap content-center items-center mx-auto">
            <div className="w-[300px]  xl:w-[382px]">
              <FeaturedCard
                imgSrc="https://images.crowdspring.com/blog/wp-content/uploads/2017/08/23163415/pexels-binyamin-mellish-106399.jpg"
                userSrc="https://as2.ftcdn.net/v2/jpg/03/83/25/83/1000_F_383258331_D8imaEMl8Q3lf7EKU2Pi78Cn0R7KkW9o.jpg"
                isFavourite="true"
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
            <div className="w-[300px]  xl:w-[382px]">
              <FeaturedCard
                imgSrc="https://images.crowdspring.com/blog/wp-content/uploads/2017/08/23163415/pexels-binyamin-mellish-106399.jpg"
                userSrc="https://as2.ftcdn.net/v2/jpg/03/83/25/83/1000_F_383258331_D8imaEMl8Q3lf7EKU2Pi78Cn0R7KkW9o.jpg"
                isFavourite="true"
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
            <div className="w-[300px]  xl:w-[382px]">
              <FeaturedCard
                imgSrc="https://images.crowdspring.com/blog/wp-content/uploads/2017/08/23163415/pexels-binyamin-mellish-106399.jpg"
                userSrc="https://as2.ftcdn.net/v2/jpg/03/83/25/83/1000_F_383258331_D8imaEMl8Q3lf7EKU2Pi78Cn0R7KkW9o.jpg"
                isFavourite="true"
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
            <div className="w-[300px]  xl:w-[382px]">
              <FeaturedCard
                imgSrc="https://images.crowdspring.com/blog/wp-content/uploads/2017/08/23163415/pexels-binyamin-mellish-106399.jpg"
                userSrc="https://as2.ftcdn.net/v2/jpg/03/83/25/83/1000_F_383258331_D8imaEMl8Q3lf7EKU2Pi78Cn0R7KkW9o.jpg"
                isFavourite="true"
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
            <div className="w-[300px] xl:w-[382px]">
              <FeaturedCard
                imgSrc="https://images.crowdspring.com/blog/wp-content/uploads/2017/08/23163415/pexels-binyamin-mellish-106399.jpg"
                userSrc="https://as2.ftcdn.net/v2/jpg/03/83/25/83/1000_F_383258331_D8imaEMl8Q3lf7EKU2Pi78Cn0R7KkW9o.jpg"
                isFavourite="true"
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
          </div>
        </div>
        <div className="browse h-[395px] w-[279px] md:w-full mt-[80px] rounded-[12px] flex flex-col gap-y-[30px] py-[40px] px-[50px] mx-auto  md:mx-0 bg-center bg-[url('/images/violet.jpg')] bg-no-repeat bg-cover bg-origin-padding hover:bg-bottom">
          <h1 className="font-bold text-[25px] text-[#101010] md:w-[320px] w-full md:text-[38px] mx-auto md:mx-0 ">
            Browse For More Properties
          </h1>
          <p className="font-medium text-[16px] text-[#e2e0e0] ">
            Explore properties by their categories/types...
          </p>
          <a
            href="./login"
            className="host text-white text-center mt-[40px] mb-[30px] bg-[#484848] rounded-[23px] cursor-pointer hover:bg-black focus:ring focus:ring-slate-200 w-[186px] p-[12px] text-[14px] block "
          >
            Find A Property
          </a>
        </div>

        <div className="rental pt-[100px] mx-auto md:w-full w-[279px]">
          <h1 className="font-bold text-[25px] text-[#484848] md:w-[339px] w-[279px] md:text-[36px] ">
            Property Rental Guides & Tips
          </h1>
          <div className="bg-[#484848] w-[140px] h-[6px] rounded-[3px] mt-[30px] mb-[60px]"></div>

          <div className="flex gap-[30px] flex-wrap content-center items-center mx-auto">
            <ArticleCard
              imgSrc="https://images.crowdspring.com/blog/wp-content/uploads/2017/08/23163415/pexels-binyamin-mellish-106399.jpg"
              title="Choose the right property!"
              category="Economy"
              link="#"
            />
            <ArticleCard
              imgSrc="https://images.crowdspring.com/blog/wp-content/uploads/2017/08/23163415/pexels-binyamin-mellish-106399.jpg"
              title="Choose the right property!"
              category="Economy"
              link="#"
            />{" "}
            <ArticleCard
              imgSrc="https://images.crowdspring.com/blog/wp-content/uploads/2017/08/23163415/pexels-binyamin-mellish-106399.jpg"
              title="Choose the right property!"
              category="Economy"
              link="#"
            />
          </div>
          <a
            href="./login"
            className="host mx-auto text-white text-center mt-[40px] mb-[30px] bg-[#484848] rounded-[23px] cursor-pointer hover:bg-black focus:ring focus:ring-slate-200 xl:block xl:w-[186px] xl:p-[12px] xl:text-[14px] lg:block lg:w-[170px] lg:p-[10px] lg:text-[13px] transition-all duration-300 hidden"
          >
            View All Blogs
          </a>
        </div>

        <div className="discover flex flex-col md:flex-row gap-x-[150px] pt-[100px] pb-[80px] mx-auto md:w-full w-[279px]">
          <div>
            <h1 className="font-bold text-[25px] text-[#484848] md:w-[472px] w-[279px] md:text-[36px] ">
              Discover More About Property Rental
            </h1>
            <div className="bg-[#484848] w-[140px] h-[6px] rounded-[3px] mt-[30px] mb-[40px]"></div>
            <p className="font-regular text-[#9A9A9A] text-[16px] mb-[40px]">
              At vero eos et accusamus et iusto odio dignissimos ducimus qui
              blanditiis praesentium voluptatum deleniti atque corrupti quos
              dolores et quas molestias excepturi sint occaecati cupiditate non
              provident, similique sunt in culpa qui officia deserunt mollitia
              animi, id est laborum et dolorum fuga.
            </p>
            <div className="font-extrabold flex  text-[#484848] text-[15px] gap-[20px]">
              <a href="#" className=" hover:text-[#000000]">
                Ask A Question
              </a>
              <a href="#" className=" hover:text-[#000000]">
                Find A Property
              </a>
            </div>
            <a
              href="./login"
              className="host text-white text-center mt-[40px] mb-[30px] bg-[#484848] rounded-[23px] cursor-pointer hover:bg-black focus:ring focus:ring-slate-200 w-[186px] p-[12px] text-[14px] block  "
            >
              Discover More
            </a>
          </div>

          <img
            src="./images/hotel.jpg"
            className="w-[382px] h-[437px] hidden lg:block"
          />
        </div>
      </div>
    </Layout>
  );
}

export default Index;
