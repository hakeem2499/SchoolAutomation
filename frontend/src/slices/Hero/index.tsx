"use client";
import { FC, useState, useEffect } from "react";
import { Content, isFilled } from "@prismicio/client";
import { PrismicRichText, PrismicText, SliceComponentProps } from "@prismicio/react";
import { CldVideoPlayer } from 'next-cloudinary';
import 'next-cloudinary/dist/cld-video-player.css';
import Bounded from "@/Components/Bounded";
import ButtonLink from "@/Components/ButtonLink";
import { SpotlightHero } from "@/Components/ui/SpotLightHero";

/**
 * Props for `Hero`.
 */
export type HeroProps = SliceComponentProps<Content.HeroSlice>;

/**
 * Component for "Hero" Slices.
 */
const Hero: FC<HeroProps> = ({ slice }) => {
  const [isVideoLoaded, setIsVideoLoaded] = useState(false);

  // Handle video load event
  const handleVideoLoad = () => {
    setIsVideoLoaded(true);
  };

  return (
    <>
      {/* Video Background */}
      <div className="absolute z-[-50] h-fit md:h-full w-full">
        {/* Fallback Image */}
        {!isVideoLoaded && (
          <div className="absolute inset-0 z-[-40]">
            <img
              src="/path/to/fallback-image.jpg" //  fallback image 
              alt="Fallback Image"
              className="w-full h-full object-cover"
            />
          </div>
        )}

        {/* Mobile Video */}
        <div className="block md:hidden">
          <CldVideoPlayer
            id="mobile"
            className="z-[-50] opacity-70"
            loop={true}
            autoplay={true}
            muted={true}
            controls={false}
            quality={100}
            width="1080"
            height="1920" // Portrait orientation for mobile
            src="HeroVideoMobileReal_brsnlr" //  mobile video's public ID
            onDataLoad={handleVideoLoad} // Trigger when video is loaded
          />
        </div>

        {/* Desktop Video */}
        {/* <div className="hidden md:block">
          <CldVideoPlayer
            id="medium"
            className="z-[-50] opacity-50"
            loop={true}
            autoplay={true}
            muted={true}
            controls={false}
            quality={100}
            width="1920"
            height="1080" // Landscape orientation for desktop
            src="HeroVideoMediumReal_y0lszw" //  desktop video's public ID
            onDataLoad={handleVideoLoad} // Trigger when video is loaded
          />
        </div> */}
        <SpotlightHero/>
      </div>

      {/* Hero Content */}
      <Bounded
        className="lg:min-h-screen"
        data-slice-type={slice.slice_type}
        data-slice-variation={slice.variation}
      >
        <div className="flex flex-col gap-4">
          {isFilled.richText(slice.primary.heading) && (
            <h1 className="text-balance text-4xl mt-[25%] md:mt-[15%] font-semibold text-brand md:text-7xl">
              <PrismicText field={slice.primary.heading} />
            </h1>
          )}

          {isFilled.richText(slice.primary.body) && (
            <div className="hero__body mt-6 max-w-5xl text-balance font-medium text-lg md:text-2xl">
              <PrismicRichText field={slice.primary.body} />
            </div>
          )}

          {isFilled.link(slice.primary.link_to_services) && (
            <ButtonLink
              className="hero__button mt-8"
              field={slice.primary.link_to_services}
            >
              {slice.primary.label}
            </ButtonLink>
          )}
        </div>
      </Bounded>
    </>
  );
};

export default Hero;