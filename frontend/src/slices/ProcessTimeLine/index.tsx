import { FC } from "react";
import { Content } from "@prismicio/client";
import { SliceComponentProps } from "@prismicio/react";

/**
 * Props for `ProcessTimeLine`.
 */
export type ProcessTimeLineProps =
  SliceComponentProps<Content.ProcessTimeLineSlice>;

/**
 * Component for "ProcessTimeLine" Slices.
 */
const ProcessTimeLine: FC<ProcessTimeLineProps> = ({ slice }) => {
  return (
    <section
      data-slice-type={slice.slice_type}
      data-slice-variation={slice.variation}
    >
      Placeholder component for process_time_line (variation: {slice.variation})
      Slices
    </section>
  );
};

export default ProcessTimeLine;
