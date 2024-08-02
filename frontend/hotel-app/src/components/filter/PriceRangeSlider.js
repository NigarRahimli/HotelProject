import React, { useState } from 'react';
import Slider from '@mui/material/Slider';
import Typography from '@mui/material/Typography';
import { styled } from '@mui/system';

const BlackSlider = styled(Slider)(({ theme }) => ({
  color: '#484848', 
  '& .MuiSlider-thumb': {
    '&:hover, &.Mui-focusVisible': {
      boxShadow: '0px 0px 0px 8px rgba(0, 0, 0, 0.16)', 
    },
  },
}));
const PriceRangeSlider = ({ onPriceChange }) => {
  const [value, setValue] = useState([0, 3000]); 

  const handleChange = (event, newValue) => {
    setValue(newValue);
    onPriceChange(newValue);
  };

  return (
    <div>
      <Typography id="priceRange" gutterBottom className="block font-semibold mb-1">
        Price Range
      </Typography>
      <BlackSlider
        value={value}
        onChange={handleChange}
        valueLabelDisplay="auto"
        aria-labelledby="priceRange"
        min={0}
        max={3000}
      />
      <Typography variant="subtitle1" gutterBottom>
        Min Price: {value[0]}
      </Typography>
      <Typography variant="subtitle1" gutterBottom>
        Max Price: {value[1]}
      </Typography>
    </div>
  );
};

export default PriceRangeSlider;
