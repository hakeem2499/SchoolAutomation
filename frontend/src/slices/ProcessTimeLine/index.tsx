import { FC, JSX } from "react";
import { Content } from "@prismicio/client";
import { SliceComponentProps } from "@prismicio/react";
import Bounded from "@/Components/Bounded";
import ProcessTimeLineClient from "./ProcessTimeLineClient";

/**
 * Props for `ProcessTimeLine`.
 */
export type ProcessTimeLineProps =
  SliceComponentProps<Content.ProcessTimeLineSlice>;

/**
 * Component for "ProcessTimeLine" Slices.
 */
const ProcessTimeLine: FC<ProcessTimeLineProps> = ({ slice }: ProcessTimeLineProps): JSX.Element => {
  return (
    <Bounded
      data-slice-type={slice.slice_type}
      data-slice-variation={slice.variation}
    >
      <ProcessTimeLineClient slice={slice}/>
    </Bounded>
  );
};

export default ProcessTimeLine;
