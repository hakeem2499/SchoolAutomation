"use client";
import { asLink, Content, KeyTextField } from '@prismicio/client';
import Link from 'next/link';
import { usePathname } from 'next/navigation';
import React, { useState } from 'react';
import clsx from 'clsx';
import { PrismicNextLink } from '@prismicio/next';
import SideBar from './SideBar';
import Image from "next/image";


type Props = {
    settings: Content.SettingsDocument;
};

const NavBar: React.FC<Props> = ({ settings }) => {
    const [open, setOpen] = useState<boolean>(false);
    const [showServices, setShowServices] = useState<boolean>(false);
    const pathname = usePathname();

    const toggleOpen = () => setOpen(!open); // Toggle the open state
    const closeAllPopups = () => {
        setOpen(false);
        setShowServices(false);
        console.log('Popup closed');
    };

    // Helper function to render navigation links
    const renderNavLinks = (items: { label: KeyTextField; link_to_services?: any; link_to_company?: any }[], isServiceLink: boolean = false) => {
        return items.map((item) => (
            <PrismicNextLink
                className='block px-2 text-2xl'
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
        <nav className='md:py-4 px-4 ' aria-label='Main'>
            <div className="mx-auto z-50 flex max-w-8xl flex-col justify-between font-medium text-white md:flex-row md:items-center">
                {/* Logo and Mobile Menu Button */}
                <div className="flex items-center  justify-between">
                    <Link className="z-50" onClick={() => setOpen(false)} href="/">
                        <Image
                            src="/NdealNextBlack.svg"
                            alt="NdealLogo logo"
                            width={180}
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
                <div className={clsx(" fixed bottom-0 left-0 right-0 my-auto top-0 pt-14 z-40 md:hidden")}>
                    <div className="grid gap-8">
                        <SideBar className='bg-secondary text-white' showPopup={open} onClose={toggleOpen}>
                            {/* Services Section */}
                            <div>
                                <button onClick={() => setShowServices(true)} className="block text-2xl px-2 mt-10">
                                    Our Services
                                </button>
                                <SideBar className='bg-black text-brandWhite' showPopup={showServices}>
                                    {renderNavLinks(settings.data.our_services, true)}
                                </SideBar>
                            </div>

                            {/* Company Links */}
                            {renderNavLinks(settings.data.company)}
                        </SideBar>
                    </div>
                </div>
            </div>
        </nav>
    );
};

export default NavBar;