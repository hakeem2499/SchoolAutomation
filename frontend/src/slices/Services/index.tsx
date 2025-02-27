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
 * Fetches service documents based on the slice items.
 */
const fetchServices = async (slice: Content.ServicesSlice) => {
  const client = createClient();

  // Fetch all services in parallel
  const services = await Promise.all(
    slice.primary.services.map(async (item) => {
      if (isFilled.contentRelationship(item.service)) {
        return await client.getByID<Content.ServiceDocument>(item.service.id);
      }
      return null; // Return null for invalid items
    })
  );

  // Filter out null values and return only valid services
  return services.filter((service) => service !== null) as Content.ServiceDocument[];
};

/**
 * Component for "Services" Slices.
 */
const Services: FC<ServicesProps> = async ({ slice }: ServicesProps): Promise<JSX.Element> => {
  const services = await fetchServices(slice);
  console.log("🚀 ~ constServices:FC<ServicesProps>= ~ services:", services)

  return (

    <Bounded

      data-slice-type={slice.slice_type}
      data-slice-variation={slice.variation}
    >
      
      <div className="flex justify-between lg:mt-16 w-full items-center">
        {isFilled.richText(slice.primary.heading) && (
          <h2 className="hero__heading text-balance text-2xl font-semibold md:font-medium md:text-5xl">
            <PrismicText field={slice.primary.heading} />
          </h2>
        )}

        {isFilled.link(slice.primary.link) && (
          <ButtonLink className="text-xs md:text-base" field={slice.primary.link}>
            {slice.primary.label}
          </ButtonLink>
        )}
      </div>

      <div className="mt-10 grid md:gap-8 md:grid-cols-2">
        {services.map(
          (service) =>
            service && (
              <div key={service.id} className="flex bg-slate-950  text-white rounded-2xl flex-col p-4 lg:p-8 gap-4 md:gap-6 justify-center">
                <div>

                  <PrismicNextImage  className="rounded-lg  h-20 w-20" field={service.data.icon} />
                </div>
                <h4 className="text-balance text-brand text-2xl font-medium md:text-3xl">

                  <PrismicText field={service.data.service} />
                </h4>
                <p className="font-medium">

                  <PrismicText field={service.data.description} />
                </p>
                <PrismicNextLink
                  document={service}
                  className=" text-brand hover:underline"
                >
                   {service.data.label} 
                </PrismicNextLink>
              </div>
            )
        )}
      </div>
    </Bounded>
  );
};

export default Services;