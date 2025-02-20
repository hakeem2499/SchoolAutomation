"use client";
import { asLink, Content, isFilled, KeyTextField, LinkField } from '@prismicio/client';
import Link from 'next/link';
import { usePathname } from 'next/navigation';
import clsx from 'clsx';
import { PrismicNextLink } from '@prismicio/next';
import Image from "next/image";
import ButtonLink from '../ButtonLink';
import { motion } from 'framer-motion';


import React, { useState } from 'react'
import Button from '../Button';

type Props = {
    settings: Content.SettingsDocument;
};

const StickyNav: React.FC<Props> = ({ settings }) => {

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
    const menuVariants = {
        hidden: { height: "5.6rem" },
        visible: { height: "29rem" },
        transition: { duration: 0.3 }
    }
    const expandRowVariants = {
        hidden: {
            opacity: 0, y: 50
        },
        visible: (i: any) => ({
            opacity: 1,
            y: 0,
            transiton: { delay: i * 0.1, duration: 0.6 }
        })
    }
    return (
        <motion.div className='fixed top-0 w-full px-2 md:p-8 bg-primary text-white rounded-lg shadow-lg' variants={menuVariants}
            initial="hidden"
            animate={open ? 'visible' : 'hidden'}
            whileHover="visible">
            <div className="flex flex-col">
                <div className="flex justify-between items-center">

                    <div className="flex items-center">
                        {/* insert Logo here */}
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
                    </div>
                    {/* insert the Navigation for desktop here  menu for mobile navigation */}
                    {/* desktop navs */}
                    <div className="hidden md:flex items-center space-x-5 text-center">
                        <button
                            onMouseEnter={() => setShowServices(true)}
                            onMouseLeave={() => setShowServices(false)}
                            className="text-lg"
                        >
                            Our Services
                        </button>
                        {/* Company Links */}
                        <div className="flex gap-8">
                            {renderNavLinks(settings.data.company)}
                        </div>
                    </div>
                    <div className='flex items-center'>
                        <ButtonLink
                            className="hero__button hidden lg:flex "
                            field={settings.data.work_with_us}
                        >
                            {settings.data.work_with_us_label}
                        </ButtonLink>
                        <button className="block p-2 z-50 text-hidden text-3xl text-white md:hidden" onClick={toggleOpen}>
                            <span className={clsx('burger burger-3', open ? 'is-closed' : '')}></span>
                        </button>
                    </div>
                </div>
                <motion.div className=" flex-col hidden md:flex">
                    <span className={clsx("h-px  bg-gray-300 w-full", open ? 'md:flex' : "hidden")}></span>
                    <div className="flex items-center mt-10">
                        {showServices ? (
                            <motion.div variants={expandRowVariants} custom={1} className="pl-4 flex flex-col gap-4 mt-2">
                                {renderNavLinks(settings.data.our_services, true)}
                            </motion.div>
                        ) : (
                            <motion.div variants={expandRowVariants} custom={2}>

                                <ButtonLink
                                    className="hero__button hidden lg:flex "
                                    field={settings.data.work_with_us}
                                >
                                    {settings.data.work_with_us_label}
                                </ButtonLink>
                            </motion.div>
                        )}
                    </div>
                </motion.div>
            </div>
        </motion.div>
    )
}

export default StickyNav;
