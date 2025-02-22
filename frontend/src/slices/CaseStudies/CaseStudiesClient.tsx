"use client"; // Mark this as a Client Component

import { FC, useRef, useState } from "react";
import { Content, isFilled } from "@prismicio/client";
import { PrismicText } from "@prismicio/react";
import Bounded from "@/Components/Bounded";
import ButtonLink from "@/Components/ButtonLink";
import { PrismicNextImage } from "@prismicio/next";
import styles from "./index.module.css";


interface CaseStudiesClientProps {
  slice: Content.CaseStudiesSlice;
  caseStudies: Content.CaseStudyDocument[] | undefined; // Allow undefined
}

const CaseStudiesClient: FC<CaseStudiesClientProps> = ({ slice, caseStudies }) => {
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

  // If caseStudies is undefined or empty, show a fallback message
  if (!caseStudies || caseStudies.length === 0) {
    return (
      <Bounded>
        <div className="text-center py-8">
          <p>No case studies found.</p>
        </div>
      </Bounded>
    );
  }

  return (
    <Bounded
      data-slice-type={slice.slice_type}
      data-slice-variation={slice.variation}
    >
      <div className="flex flex-col justify-between py-4 w-full items-center">
        {isFilled.richText(slice.primary.heading) && (
          <h2 className="hero__heading text-balance text-2xl font-semibold md:font-medium md:text-5xl">
            <PrismicText field={slice.primary.heading} />
          </h2>
        )}

        {isFilled.link(slice.primary.link_to_case_studies) && (
          <ButtonLink className="text-xs md:text-base" field={slice.primary.link_to_case_studies}>
            {slice.primary.link_label}
          </ButtonLink>
        )}
      </div>
      <div className="flex flex-col gap-4 items-center">
        <div
          className={styles.scroller}
          ref={scrollerRef}
          onScroll={handleScroll}
          aria-live="polite"
        >
          {caseStudies.map(
            (case_study) =>
              case_study && (
                <div key={case_study.id} className={styles.scrollerItem}>
                  <div className="w-full md:w-1/2">
                    <PrismicNextImage
                      className="rounded-lg invert "
                      field={case_study.data.image}
                    />
                  </div>
                  <div className="flex w-full md:w-1/2 flex-col">
                    <h4 className="text-balance text-brand text-2xl font-medium md:text-3xl">
                      <PrismicText field={case_study.data.heading} />
                    </h4>
                    <p className="font-medium">
                      <PrismicText field={case_study.data.description} />
                    </p>
                  </div>
                </div>
              )
          )}
        </div>
        <div className="flex justify-center gap-4 items-center">
          <button
            className={styles.scrollerButton}
            onClick={() => scrollBy(-scrollerRef.current!.clientWidth * 0.9)}
            disabled={isAtStart}
            aria-label="Previous"
          >
            Left
          </button>
          <button
            className=""
            onClick={() => scrollBy(scrollerRef.current!.clientWidth * 0.9)}
            disabled={isAtEnd}
            aria-label="Next"
          >
            Right
          </button>
        </div>
      </div>
    </Bounded>
  );
};

export default CaseStudiesClient;