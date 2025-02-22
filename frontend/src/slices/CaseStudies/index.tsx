import { FC, JSX, useRef, useState } from "react";
import { Content, isFilled } from "@prismicio/client";
import { PrismicText, SliceComponentProps } from "@prismicio/react";
import Bounded from "@/Components/Bounded";
import { createClient } from "@/prismicio";
import Scroller from "@/Components/Scroller";
import ButtonLink from "@/Components/ButtonLink";

/**
 * Props for `CaseStudies`.
 */
export type CaseStudiesProps = SliceComponentProps<Content.CaseStudiesSlice>;



// function for fetching slice



const fetchCaseStudies = async (slice: Content.CaseStudiesSlice) => {
  const client = createClient();

  // fetch all caseStudies
  const caseStudies = await Promise.all(
    slice.primary.casestudies.map(async (item) => {
      if (isFilled.contentRelationship(item.case_study)) {
        return await client.getByID<Content.CaseStudyDocument>(item.case_study.id);
      }
      return null;
    })
  );

  return caseStudies.filter((case_study) => case_study !== null) as Content.CaseStudyDocument[];
};




/**
 * Component for "CaseStudies" Slices.
 */
const CaseStudies: FC<CaseStudiesProps> =async ({ slice }: CaseStudiesProps): Promise<JSX.Element> => {
  const caseStudies =await fetchCaseStudies(slice);
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
          <ButtonLink className="text-xs md:text-base" field={slice.primary.link}>
            {slice.primary.link_label}
          </ButtonLink>
        )}
      </div>
      <div>
        <div className="scroller scrollbar-hide"
          ref={scrollerRef}
          onScroll={handleScroll}
          aria-live="polite"
        >
          {
              caseStudies.map((case_study) => case_study && (
                <div className="flex w-full bg-slate-950">
                    <div>
                        
                    </div>
                </div>
              ))
          }

        </div>
        <div className="flex justify-center items-center">
          <button
            className=""
            onClick={() => scrollBy(-scrollerRef.current!.clientWidth * 0.9)}
            disabled={isAtStart}
            aria-label="Previous">
            Left
          </button>
          <button
            className=""
            onClick={() => scrollBy(scrollerRef.current!.clientWidth * 0.9)}
            disabled={isAtEnd}
            aria-label="Next">
            Right
          </button>
        </div>
      </div>
    </Bounded>
  );
};

export default CaseStudies;
