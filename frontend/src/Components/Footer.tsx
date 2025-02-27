import { asLink, Content, KeyTextField, LinkField } from '@prismicio/client';
import { PrismicNextLink } from '@prismicio/next';
import React from 'react';
import { createClient } from "@/prismicio";
import { PrismicRichText } from '@prismicio/react';

import ThemeToggleSwitch from './ui/ThemeSwitch';
import Bounded from './Bounded';

export async function Footer() {
    const client = createClient();
    const settings = await client.getSingle('settings');

    // Helper function to render navigation links
    const renderNavLinks = (items: { label: KeyTextField; link_to_services?: LinkField; link_to_company?: LinkField }[], isServiceLink: boolean = false) => {
        return items.map((item) => (
            <PrismicNextLink
                className='anchor-link block w-fit  text-sm md:text-lg'
                key={item.label}
                field={isServiceLink ? item.link_to_services : item.link_to_company}
            >
                {item.label}
            </PrismicNextLink>
        ));
    };

    return (
        <Bounded>


            <footer className="flex flex-col w-full mt-16 lg:mt-24 items-center gap-8 lg:gap-10 justify-between px-2  py-4">
                <div className=" text-3xl md:text-5xl px-4 ">
                    <div className=" top-20 left-0 rounded-full bg-slate-100 h-1 m-2 w-1/5"></div>
                    <em
                        className="text-transparent bg-clip-text not-italic bg-gradient-to-r from-brand to-violet-600 via-brand"
                    >Automate Today <br /> Know What's Next
                    </em
                    >
                </div>
                {/* Navigation Section */}
                <nav aria-label='footer' className="grid grid-cols-2 md:grid-cols-3 gap-8 w-full  max-w-6xl py-4 mx-auto md:px-2">
                    {/* Column 1: Company Links */}
                    <div className='flex flex-col md:text-sm text-xs gap-4'>
                        {renderNavLinks(settings.data.company)}
                    </div>

                    {/* Column 2: Service Links */}
                    <div className="flex flex-col gap-4">
                        {renderNavLinks(settings.data.our_services, true)}
                    </div>

                    {/* Column 3: Contact Information */}
                    <div className='flex flex-col  gap-4'>
                        <ul className="flex flex-col gap-4">
                            {settings.data.contact_information.map((item) => (
                                <li key={item.label}>
                                    <PrismicRichText field={item.information} />
                                </li>
                            ))}
                        </ul>

                    </div>
                </nav>

                {/* Policies Section */}
                <div className='flex w-full  justify-between md:px-10 px-2  glass-container rounded-none   md:items-center'>

                    <ul className="flex flex-col   md:gap-8 md:flex-row  md:text-lg text-xs gap-4   justify-center">
                        {settings.data.policies.map((item) => (
                            <li key={item.label}>
                                <PrismicNextLink
                                    field={item.link_to_policies}
                                    className="inline-flex items-center"
                                >
                                    {item.label}
                                </PrismicNextLink>
                            </li>
                        ))}
                    </ul>
                    {/* <ThemeToggleSwitch /> */}
                </div>
            </footer>
        </Bounded>
    );
}