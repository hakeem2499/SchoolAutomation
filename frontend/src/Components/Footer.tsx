"use Client";

import { asLink, Content, KeyTextField, LinkField } from '@prismicio/client';
import { PrismicNextLink } from '@prismicio/next';
import { usePathname } from 'next/navigation';
import React from 'react'

type Props = {
    settings: Content.SettingsDocument;
};

const Footer: React.FC<Props> = ({ settings }) => {
    const pathname = usePathname();

    // Helper function to render navigation links
    const renderNavLinks = (items: { label: KeyTextField; link_to_services?: LinkField; link_to_company?: LinkField }[], isServiceLink: boolean = false) => {
        return items.map((item) => (
            <PrismicNextLink
                className='block px-2 text-2xl'
                key={item.label}
                field={isServiceLink ? item.link_to_services : item.link_to_company}

                aria-current={
                    pathname.includes(asLink(isServiceLink ? item.link_to_services : item.link_to_company) as string)
                        ? "page"
                        : undefined
                }
            >
                {item.label}
            </PrismicNextLink>
        ));
    };
    return (
        <div>Footer</div>
    )
}