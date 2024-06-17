import React from "react";

const ArticleCard = ({ imgSrc, title, category, link }) => {
  return (
    <div className="article-card w-[382px] h-[431px]">
     <a href="#"><img src={imgSrc} className="w-[382px] h-[340px] rounded-[12px] mb-[20px]" /></a> 
      <a href="#" className="font-bold text-[18px] text-[#484848] ">{title}</a>
      <p className="font-semibold text-[14px] text-[#9A9A9A] mt-[5px]">{category}</p>
    </div>
  );
};

export default ArticleCard;
