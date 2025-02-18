"use client";
import { FC } from "react";
import { Content, isFilled } from "@prismicio/client";
import { PrismicText, SliceComponentProps } from "@prismicio/react";
import { CldVideoPlayer } from 'next-cloudinary';
import 'next-cloudinary/dist/cld-video-player.css';
import Bounded from "@/Components/Bounded";

/**
 * Props for `Hero`.
 */
export type HeroProps = SliceComponentProps<Content.HeroSlice>;

/**
 * Component for "Hero" Slices.
 */
const Hero: FC<HeroProps> = ({ slice }) => {
  return (
    <><div className="absolute z-[-50]  h-fit md:h-full w-full">
      {/* <CldVideoPlayer
        className="z-[-50]"
        loop={true}
        autoplay={true}
        key={Math.random()}
        muted={true}
        controls={false}
        width="1920"
        height="1080"
        src="herovideoformediumscreen_1_dm8ypk" /> */}
    </div>
    <Bounded
      data-slice-type={slice.slice_type}
      data-slice-variation={slice.variation}
    >
       {isFilled.richText(slice.primary.heading) && (
        <h1 className="hero__heading  text-balance text-5xl font-medium  md:text-7xl">
          <PrismicText field={slice.primary.heading} />
        </h1>
      )}
      </Bounded></>
  );
};

export default Hero;
