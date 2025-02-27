import { FC } from "react";
import { Content, isFilled } from "@prismicio/client";
import { PrismicRichText, PrismicText, SliceComponentProps } from "@prismicio/react";
import Bounded from "@/Components/Bounded";

/**
 * Props for `ShowCase`.
 */
export type ShowCaseProps = SliceComponentProps<Content.ShowCaseSlice>;

/**
 * Component for "ShowCase" Slices.
 */
const ShowCase: FC<ShowCaseProps> = ({ slice }) => {
  return (
    <Bounded
      data-slice-type={slice.slice_type}
      data-slice-variation={slice.variation}
    >
      {isFilled.richText(slice.primary.heading) && (
        <h1 className="text-balance text-4xl mt-[25%] md:mt-[15%] font-semibold text-brand md:text-7xl">
          <PrismicText field={slice.primary.heading} />
        </h1>
      )}

      {isFilled.richText(slice.primary.body) && (
        <div className=" mt-6 max-w-5xl text-balance font-medium text-lg md:text-2xl">
          <PrismicRichText field={slice.primary.body} />
        </div>
      )}
    </Bounded>
  );
};

export default ShowCase;
