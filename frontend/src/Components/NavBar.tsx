"use client";
import { asLink, Content, isFilled, KeyTextField, LinkField } from '@prismicio/client';
import Link from 'next/link';
import { usePathname } from 'next/navigation';
import React, { useState } from 'react';
import clsx from 'clsx';
import { PrismicNextLink } from '@prismicio/next';
import SideBar from './SideBar';
import Image from "next/image";
import ButtonLink from './ButtonLink';
import {motion} from 'framer-motion';

type Props = {
    settings: Content.SettingsDocument;
};

const NavBar: React.FC<Props> = ({ settings }) => {
    const [open, setOpen] = useState<boolean>(false);
    const [showServices, setShowServices] = useState<boolean>(false);
    const pathname = usePathname();

    const toggleOpen = () => setOpen(!open);
    const closeAllPopups = () => {
        setOpen(false);
        setShowServices(false);
    };

    // Helper function to render navigation links
    const renderNavLinks = (items: { label: KeyTextField; link_to_services?: LinkField; link_to_company?: LinkField }[], isServiceLink: boolean = false) => {
        return items.map((item) => (
            <PrismicNextLink
                className='block border-b md:border-none hover:text-brand transition-colors duration-200  border-slate-400/20  text-2xl md:text-lg'
                key={item.label}
                field={isServiceLink ? item.link_to_services : item.link_to_company}
                onClick={closeAllPopups}
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
        <nav className='md:py-4 px-4' aria-label='Main'>
            <div className="mx-auto z-50 flex max-w-7xl flex-col justify-between font-medium text-white md:flex-row md:items-center">
                {/* Logo and Mobile Menu Button */}
                <div className="flex items-center   justify-between">
                    <Link className="z-50" onClick={() => setOpen(false)} href="/">
                        <Image

                            src="/NdealNextBlack.svg"
                            alt="NdealLogo logo"
                            width={150}
                            height={38}
                            priority
                        />
                        <span className="sr-only">Ndeal Homepage</span>
                    </Link>
                    <button className="block p-2 z-50 text-hidden text-3xl text-white md:hidden" onClick={toggleOpen}>
                        <span className={clsx('burger burger-3', open ? 'is-closed' : '')}></span>
                    </button>
                </div>

                {/* Mobile Navigation */}
                <div
                    className={clsx(
                        "fixed  left-0 right-0 top-0 z-40 flex flex-col items-end bg-primary pr-4 pt-14 transition-transform duration-300 ease-in-out motion-reduce:transition-none md:hidden",
                        open ? "translate-y-[0]" : "translate-y-[-100%]"
                    )}
                >
                    <div className="grid gap-8 w-full">
                        {/* Services Section */}
                        <div>
                            <button
                                onClick={() => setShowServices(!showServices)}
                                className="block text-2xl pl-4  mt-10"
                            >
                                Our Services
                            </button>
                            {showServices && (
                                <div className="pl-4 flex flex-col gap-4 mt-2">
                                    {renderNavLinks(settings.data.our_services, true)}
                                </div>
                            )}
                        </div>

                        {/* Company Links */}
                        <div className="pl-4 flex flex-col gap-8">
                            {renderNavLinks(settings.data.company)}
                        </div>
                    </div>
                </div>

                {/* Desktop Navigation */}
                <div className="hidden md:flex items-center text-white mx-auto gap-8">
                    {/* Services Dropdown */}
                    <div className="relative">
                        <button
                            onClick={() => setShowServices(!showServices)}
                            className="flex items-center text-lg gap-2"
                        >
                            <span>Our Services</span>
                            <svg
                                className={`w-4 h-4 transition-transform ${showServices ? "rotate-180" : ""}`}
                                viewBox="0 0 20 20"
                                fill="currentColor"
                            >
                                <path
                                    fillRule="evenodd"
                                    d="M5.293 7.293a1 1 0 011.414 0L10 10.586l3.293-3.293a1 1 0 111.414 1.414l-4 4a1 1 0 01-1.414 0l-4-4a1 1 0 010-1.414z"
                                    clipRule="evenodd"
                                />
                            </svg>
                        </button>

                        {/* Services Dropdown Menu */}
                        {showServices && (
                            <div className="absolute top-full flex flex-col gap-4 bg-brandWhite  left-0 mt-2 w-full min-w-[800px] p-8   text-black rounded-lg shadow-lg">
                                {renderNavLinks(settings.data.our_services, true)}
                            </div>
                        )}
                    </div>
                    {/* Company Links */}
                    <div className="flex gap-8">
                        {renderNavLinks(settings.data.company)}
                    </div>



                </div>
                <ButtonLink
                    className="hero__button hidden lg:flex "
                    field={settings.data.work_with_us}
                >
                    {settings.data.work_with_us_label}
                </ButtonLink>
            </div>
        </nav>
    );
};

export default NavBar;