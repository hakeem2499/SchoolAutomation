"use client";
import { FC } from "react";
import { Content, isFilled } from "@prismicio/client";
import { PrismicRichText, PrismicText, SliceComponentProps } from "@prismicio/react";
import { CldVideoPlayer } from 'next-cloudinary';
import 'next-cloudinary/dist/cld-video-player.css';
import Bounded from "@/Components/Bounded";
import ButtonLink from "@/Components/ButtonLink";

/**
 * Props for `Hero`.
 */
export type HeroProps = SliceComponentProps<Content.HeroSlice>;

/**
 * Component for "Hero" Slices.
 */
const Hero: FC<HeroProps> = ({ slice }) => {
  return (
    <>
      {/* Video Background */}
      <div className="absolute z-[-50] h-fit md:h-full w-full">
        {/* Mobile Video */}
        <div className="block md:hidden">
          <CldVideoPlayer
            className="z-[-50]"
            loop={true}
            autoplay={true}
            muted={true}
            controls={false}
            width="1080"
            height="1920" // Portrait orientation for mobile
            src="mobile-video-public-id" // Replace with your mobile video's public ID
          />
        </div>

        {/* Desktop Video */}
        <div className="hidden md:block">
          <CldVideoPlayer
            className="z-[-50]"
            loop={true}
            autoplay={true}
            muted={true}
            controls={false}
            width="1920"
            height="1080" // Landscape orientation for desktop
            src="desktop-video-public-id" // Replace with your desktop video's public ID
          />
        </div>
      </div>

      {/* Hero Content */}
      <Bounded
        data-slice-type={slice.slice_type}
        data-slice-variation={slice.variation}
      >
        {isFilled.richText(slice.primary.heading) && (
          <h1 className="text-balance text-4xl font-medium text-brand md:text-7xl">
            <PrismicText field={slice.primary.heading} />
          </h1>
        )}

        {isFilled.richText(slice.primary.body) && (
          <div className="hero__body mx-auto mt-6 max-w-md text-balance text-slate-300 opacity-0">
            <PrismicRichText field={slice.primary.body} />
          </div>
        )}

        {isFilled.link(slice.primary.link_to_services) && (
          <ButtonLink
            className="hero__button mt-8 opacity-0"
            field={slice.primary.link_to_services}
          >
            {slice.primary.label}
          </ButtonLink>
        )}
      </Bounded>
    </>
  );
};

export default Hero;