"use client"; // Required for interactivity in Next.js

import { Content } from "@prismicio/client";
import React, { useRef, useState, memo } from "react";

interface ScrollerProps {
  items: React.ReactNode[];
  
}

interface ScrollerItemProps {
  item: React.ReactNode;
}

const ScrollerItem: React.FC<ScrollerItemProps> = memo(({ item }) => {
  return <div className="scroller-item">{item}</div>;
});

const Scroller: React.FC<ScrollerProps> = ({ items }) => {
  const scrollerRef = useRef<HTMLDivElement>(null);
  const [isAtStart, setIsAtStart] = useState<boolean>(true);
  const [isAtEnd, setIsAtEnd] = useState<boolean>(false);
  


  // Handle scroll events
  const handleScroll = () => {
    if (scrollerRef.current) {
      const { scrollLeft, scrollWidth, clientWidth } = scrollerRef.current;
      setIsAtStart(scrollLeft === 0);
      setIsAtEnd(scrollLeft + clientWidth >= scrollWidth);
    }
  };

  // Scroll by a specific offset
  const scrollBy = (offset: number) => {
    if (scrollerRef.current) {
      scrollerRef.current.scrollBy({
        left: offset,
        behavior: "smooth",
      });
    }
  };

 

  return (
    <div className="scroller-container">
      {/* Previous Button */}
      <button
        className="scroller-button"
        onClick={() => scrollBy(-scrollerRef.current!.clientWidth * 0.9)}
        disabled={isAtStart}
        aria-label="Previous"
      >
        ←
      </button>

      {/* Scroller */}
      <div
        className="scroller scrollbar-hide"
        ref={scrollerRef}
        onScroll={handleScroll}
        aria-live="polite" // Announce changes to screen readers
      >
        {items.map((item, index) => (
          <ScrollerItem key={index} item={item} />
        ))}
      </div>

      {/* Next Button */}
      <button
        className="scroller-button"
        onClick={() => scrollBy(scrollerRef.current!.clientWidth * 0.9)}
        disabled={isAtEnd}
        aria-label="Next"
      >
        →
      </button>
    </div>
  );
};

export default Scroller;