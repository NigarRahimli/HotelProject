import React from "react";
import Box from "@mui/material/Box";
import Skeleton from "@mui/material/Skeleton";
import { keyframes } from "@emotion/react";

const shine = keyframes`
  0% {
    background-position: -200px 0;
  }
  100% {
    background-position: 200px 0;
  }
`;

const SkeletonCard = () => {
  return (
    <div className="w-[382px]">
      <Box
        sx={{
          position: "relative",
          maxWidth: 382,
          width: 382,
          height: 340,
          borderRadius: "12px",
          overflow: "hidden",
          mb: 2,
          animation: `${shine} 1.5s infinite linear`,
          background: `linear-gradient(90deg, #f0f0f0 25%, #e0e0e0 50%, #f0f0f0 75%)`,
          backgroundSize: "200% 100%",
        }}
      >
        <Skeleton variant="rectangular" width={382} height={340} />
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
          {[...Array(4)].map((_, index) => (
            <Box
              key={index}
              sx={{
                width: 10,
                height: 10,
                borderRadius: "50%",
                bgcolor: "white",
                mx: 0.5,
              }}
            />
          ))}
        </Box>
      </Box>
      <Skeleton
        variant="text"
        width="60%"
        height={30}
        sx={{
          animation: `${shine} 1.5s infinite linear`,
          background: `linear-gradient(90deg, #f0f0f0 25%, #e0e0e0 50%, #f0f0f0 75%)`,
          backgroundSize: "200% 100%",
        }}
      />
      <Skeleton
        variant="text"
        width="80%"
        height={20}
        sx={{
          animation: `${shine} 1.5s infinite linear`,
          background: `linear-gradient(90deg, #f0f0f0 25%, #e0e0e0 50%, #f0f0f0 75%)`,
          backgroundSize: "200% 100%",
        }}
      />
    </div>
  );
};

export default SkeletonCard;
