import { FC } from "react";
import { Content } from "@prismicio/client";
import { SliceComponentProps } from "@prismicio/react";

/**
 * Props for `Approach`.
 */
export type ApproachProps = SliceComponentProps<Content.ApproachSlice>;

/**
 * Component for "Approach" Slices.
 */
const Approach: FC<ApproachProps> = ({ slice }) => {
  return (
    <section
      data-slice-type={slice.slice_type}
      data-slice-variation={slice.variation}
    >
      Placeholder component for approach (variation: {slice.variation}) Slices
    </section>
  );
};

export default Approach;
