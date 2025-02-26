// src/components/ButtonLink.tsx

import { PrismicNextLink, PrismicNextLinkProps } from "@prismicio/next";
import clsx from "clsx";

export default function ButtonLink({
  className,
  ...restProps
}: PrismicNextLinkProps) {
  return (
    <PrismicNextLink
      className={clsx(
        "relative inline-flex h-fit w-fit rounded-md hover:border-primary border-inherit border-2   bg-brand px-4 py-3 md:px-8 text-brandWhite hover:text-primary hover:bg-brandWhite transition-all duration-300 outline-none   after:absolute after:inset-0 after:-z-10 after:animate-pulse after:rounded-full  focus:ring-2",
        className,
      )}
      {...restProps}
    />
  );
}