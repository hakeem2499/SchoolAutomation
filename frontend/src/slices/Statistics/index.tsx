import { FC } from "react";
import { Content } from "@prismicio/client";
import { SliceComponentProps } from "@prismicio/react";

/**
 * Props for `Statistics`.
 */
export type StatisticsProps = SliceComponentProps<Content.StatisticsSlice>;

/**
 * Component for "Statistics" Slices.
 */
const Statistics: FC<StatisticsProps> = ({ slice }) => {
  return (
    <section
      data-slice-type={slice.slice_type}
      data-slice-variation={slice.variation}
    >
      Placeholder component for statistics (variation: {slice.variation}) Slices
    </section>
  );
};

export default Statistics;
