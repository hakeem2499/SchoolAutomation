import { FC, JSX } from "react";
import { Content, isFilled } from "@prismicio/client";
import { SliceComponentProps } from "@prismicio/react";
import Bounded from "@/Components/Bounded";
import { Carousel } from "./ResourcesClient";
import { createClient } from "@/prismicio";

/**
 * Props for `Resources`.
 */
export type ResourcesProps = SliceComponentProps<Content.ResourcesSlice>;

/**
 * Component for "Resources" Slices.
 */

// Fetches Resource Document
const fetchResources = async (slice: Content.ResourcesSlice) => {
  const client = createClient();
  const Resources = await Promise.all(
    slice.primary.resources.map(async (item) => {
      if (isFilled.contentRelationship(item.resource)) {
        return await client.getByID<Content.ResourceDocument>(item.resource.id);
      }
      return null;
    })
  );

  return Resources.filter((resource) => resource !== null) as Content.ResourceDocument[];
}
const Resources: FC<ResourcesProps> = async ({ slice } : { slice: Content.ResourcesSlice }) => {
  const resources = await fetchResources(slice);
  
  return (
    
      <Carousel slides={resources} />
    
  );
};

export default Resources;
