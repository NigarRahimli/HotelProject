import React from "react";
import Box from "@mui/material/Box";
import Paper from "@mui/material/Paper";
import { baseUrl } from "@/components/constant";

const FilterCard = ({ imgSrc, userSrc, isFavourite, name, location, minPrice, maxPrice, propertyImageDetails }) => {
  const [activeStep, setActiveStep] = React.useState(0);
  const maxSteps = propertyImageDetails.length;
  const defaultImage = "/images/property_avatar.jpg"; 

  React.useEffect(() => {
    const interval = setInterval(() => {
      setActiveStep((prevActiveStep) => (prevActiveStep + 1) % maxSteps);
    }, 5000);
    return () => clearInterval(interval);
  }, [maxSteps]);

  const handleStepChange = (step) => {
    setActiveStep(step);
  };

  const getImageSrc = (index) => {
    if (propertyImageDetails.length > 0) {
      return `${baseUrl}/${propertyImageDetails[index].url}`;
    }
    return defaultImage;
  };

  return (
    <div>
      <Box
        sx={{
          position: "relative",
          maxWidth: 382,
          width: 382, 
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
            width: "100%",
            bgcolor: "background.default",
          }}
        >
          <img
            src={getImageSrc(activeStep)}
            alt={`Image ${activeStep + 1}`}
            style={{ width: "100%", height: "100%", objectFit: "cover" }} // Adjust to cover the container
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
          {propertyImageDetails.map((_, index) => (
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
          className="cursor-pointer absolute top-[20px] right-[20px]"
          alt="Favourite Icon"
        />
        <p className="font-semibold text-[18px] text-[#9A9A9A] absolute left-[20px] bottom-[15px]">
          ${minPrice} - ${maxPrice} USD
        </p>
      </Box>

      <div>
        <h1 className="font-bold text-[#484848] text-[18px] mt-[25px]">{name}</h1>
        <p className="font-medium text-[#9A9A9A] text-[14px] py-[7px]">{location}</p>
      </div>
    </div>
  );
};

export default FilterCard;
