import { FC } from "react";
import { Content } from "@prismicio/client";
import { SliceComponentProps } from "@prismicio/react";

/**
 * Props for `Resources`.
 */
export type ResourcesProps = SliceComponentProps<Content.ResourcesSlice>;

/**
 * Component for "Resources" Slices.
 */
const Resources: FC<ResourcesProps> = ({ slice }) => {
  return (
    <section
      data-slice-type={slice.slice_type}
      data-slice-variation={slice.variation}
    >
      Placeholder component for resources (variation: {slice.variation}) Slices
    </section>
  );
};

export default Resources;
