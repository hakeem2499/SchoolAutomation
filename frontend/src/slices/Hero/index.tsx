"use client";
import { FC } from "react";
import { Content, isFilled } from "@prismicio/client";
import { PrismicRichText, PrismicText, SliceComponentProps } from "@prismicio/react";
import { CldVideoPlayer } from 'next-cloudinary';
import 'next-cloudinary/dist/cld-video-player.css';
import Bounded from "@/Components/Bounded";
import ControlledSwitches from "@/Components/ui/ThemeSwitch";

/**
 * Props for `Hero`.
 */
export type HeroProps = SliceComponentProps<Content.HeroSlice>;

/**
 * Component for "Hero" Slices.
 */
const Hero: FC<HeroProps> = ({ slice }) => {
  return (
    
    <Bounded
      data-slice-type={slice.slice_type}
      data-slice-variation={slice.variation}
    >
       {isFilled.richText(slice.primary.heading) && (
        <h1 className="  text-balance text-4xl  font-medium  md:text-7xl">
          <PrismicText field={slice.primary.heading} />
        </h1>
        
      )}
      <p className="text-5xl font-medium text-primary">
        are you with us
      </p>
      <ControlledSwitches/>
      </Bounded>
  );
};

export default Hero;
