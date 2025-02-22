import { FC, JSX } from "react";
import { Content, isFilled } from "@prismicio/client";
import { PrismicRichText, PrismicText, SliceComponentProps } from "@prismicio/react";
import Bounded from "@/Components/Bounded";
import { PrismicNextImage, PrismicNextLink } from "@prismicio/next";
import ButtonLink from "@/Components/ButtonLink";
import { createClient } from "@/prismicio";

/**
 * Props for `Services`.
 */
export type ServicesProps = SliceComponentProps<Content.ServicesSlice>;

/**
 * Component for "Services" Slices.
 */
const Services: FC<ServicesProps> = async ({ slice }: ServicesProps): Promise<JSX.Element> => {
  const client = createClient();

  const services = await Promise.all(
    slice.items.map(async (item: Content.ServicesSliceDefaultPrimary) => {
      if (isFilled.contentRelationship(item.service)) {
        return await client.getByID<Content.ServiceDocument>(item.service.id)
      }
    })
  )
  console.log("🚀 ~ constServices:FC<ServicesProps>= ~ services:", services)

  return (
    <Bounded
      data-slice-type={slice.slice_type}
      data-slice-variation={slice.variation}
    >
      <div className="flex justify-between w-full items-center">

        {isFilled.richText(slice.primary.heading) && (
          <h2 className="hero__heading text-balance text-3xl font-medium  md:text-5xl">
            <PrismicText field={slice.primary.heading} />
          </h2>
        )}


        {isFilled.link(slice.primary.link) && (
          <ButtonLink
            className="hero__button mt-8 "
            field={slice.primary.link}
          >
            {slice.primary.label}
          </ButtonLink>
        )}
      </div>

      <div className="mt-10 grid md:grid-cols-2">
        {services.map((service, index) =>
          service && (
            <div key={service.id} className="flex flex-col md:p-2 lg:p-4 gap-4 md:gap-6 justify-center">
              <PrismicNextImage field={service.data.icon} />
              <PrismicRichText field={service.data.service} />
              <PrismicRichText field={service.data.description} />
              <PrismicNextLink
                document={service}
                className="after:absolute after:inset-0 hover:underline"
              >
                Develop <PrismicText field={service.data.service} /> case

              </PrismicNextLink>
            </div>
          ))}
      </div>
    </Bounded>
  );
};

export default Services;
