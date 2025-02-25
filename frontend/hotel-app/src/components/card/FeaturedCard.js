import Box from "@mui/material/Box";
import Paper from "@mui/material/Paper";
import React from "react";
import { baseUrl } from "@/components/constant";

const FeaturedCard = ({
  propertyImages,
  isFavourite,
  name,
  address,
  city,
  country,
  amenities,
  minPrice,
  maxPrice
}) => {
  const [activeStep, setActiveStep] = React.useState(0);
  const maxSteps = propertyImages.length;

  React.useEffect(() => {
    const interval = setInterval(() => {
      setActiveStep((prevActiveStep) => (prevActiveStep + 1) % maxSteps);
    }, 5000);
    return () => clearInterval(interval);
  }, [maxSteps]);

  const handleStepChange = (step) => {
    setActiveStep(step);
  };

  return (
    <div>
      <Box
        sx={{
          position: "relative",
          maxWidth: 382,
          flexGrow: 1,
          borderRadius: "12px",
          overflow: "hidden",
        }}
      >
        <Paper
          square
          elevation={0}
          sx={{
            display: "flex",
            alignItems: "center",
            justifyContent: "center",
            height: 340,
            maxWidth: 382,
            width: "100%",
            bgcolor: "background.default",
          }}
        >
          <img
            src={propertyImages[activeStep]}
            alt={`step ${activeStep + 1}`}
            style={{ width: "100%", height: "100%" }}
          />
        </Paper>
        <Box
          sx={{
            position: "absolute",
            bottom: 0,
            right: 0,
            display: "flex",
            justifyContent: "flex-end",
            alignItems: "center",
            height: 50,
            pr: 2,
            mt: 1,
          }}
        >
          {propertyImages.map((_, index) => (
            <Box
              key={index}
              onClick={() => handleStepChange(index)}
              sx={{
                width: 10,
                height: 10,
                borderRadius: "50%",
                bgcolor: index === activeStep ? "primary.main" : "white",
                mx: 0.5,
                transition: "width 0.5s, height 0.5s",
                cursor: "pointer",
                ...(index === activeStep && {
                  width: 15,
                  height: 15,
                }),
              }}
            />
          ))}
        </Box>

        <img
          src={isFavourite ? "./icons/full_heart.png" : "./icons/empty_heart.png"}
          className="absolute right-[15px] top-[15px] cursor-pointer"
        />
        <p className="font-semibold text-[18px] text-[#9A9A9A] absolute left-[20px] bottom-[15px]">
          ${minPrice} - ${maxPrice} USD
        </p>
      </Box>

      <div>
        <h1 className="font-bold text-[#484848] text-[18px] mt-[30px]">
          {name}
        </h1>
        <p className="font-medium text-[#9A9A9A] text-[14px] py-[7px]">
          {address}, {city}, {country}
        </p>
        <div className="amenities flex gap-x-[20px]">
          {amenities.map((amenity) => (
            <div key={amenity.id} className="flex items-center mr-[4px]">
              <img src={amenity.iconUrl} alt={amenity.name} className="mr-1" />
              <span className="font-semibold text-[#484848] text-[16px]">
                {amenity.count}
              </span>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default FeaturedCard;
