import { FC } from "react";
import { Content, isFilled } from "@prismicio/client";
import { PrismicText, SliceComponentProps } from "@prismicio/react";
import Bounded from "@/Components/Bounded";
import ButtonLink from "@/Components/ButtonLink";

/**
 * Props for `CtaSection`.
 */
export type CtaSectionProps = SliceComponentProps<Content.CtaSectionSlice>;

/**
 * Component for "CtaSection" Slices.
 */
const CtaSection: FC<CtaSectionProps> = ({ slice }) => {
  return (
    <Bounded
      data-slice-type={slice.slice_type}
      data-slice-variation={slice.variation}
    >
      <div className="flex py-6 flex-col items-center justify-center gap-4 md:gap-8">

        {isFilled.richText(slice.primary.cta_title) && (
          <h4 className="text-brand text-balance text-5xl md:text-7xl text-center font-semibold">
            <PrismicText field={slice.primary.cta_title} />
          </h4>
        )}

        {isFilled.richText(slice.primary.body) && (
          <p className="text-lg md:text-2xl  text-center font-medium ">
            <PrismicText field={slice.primary.body} />
          </p>
        )}

        {isFilled.link(slice.primary.link) && (
          <ButtonLink
            className="hero__button mt-8 "
            field={slice.primary.link}
          >
            {slice.primary.label}
          </ButtonLink>
        )}

        {isFilled.richText(slice.primary.description) && (
          <p className="text-lg md:text-2xl  text-gray-400 text-center font-medium">
            <PrismicText field={slice.primary.description} />
          </p>

        )}
      </div>
    </Bounded>
  );
};

export default CtaSection;
