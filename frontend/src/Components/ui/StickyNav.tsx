"use client";
import { asLink, Content, isFilled, KeyTextField, LinkField } from '@prismicio/client';
import Link from 'next/link';
import { usePathname } from 'next/navigation';
import clsx from 'clsx';
import { PrismicNextLink } from '@prismicio/next';
import Image from "next/image";
import ButtonLink from '../ButtonLink';
import { motion, Variants } from 'framer-motion';
import React, { useState } from 'react';
import SideBar from '../SideBar';
import ThemeToggleSwitch from './ThemeSwitch';
import { useTheme } from '@/contexts/ThemeContext';

type Props = {
    settings: Content.SettingsDocument;
};

const StickyNav: React.FC<Props> = ({ settings }) => {
    const [open, setOpen] = useState<boolean>(false);
    const [showServices, setShowServices] = useState<boolean>(false);
    const [showSidebar, setShowSidebar] = useState<boolean>(false); // State for sidebar visibility
    const pathname = usePathname();
    const { theme } = useTheme();

    const toggleOpen = () => setOpen(true);
    const mobileToggleOpen = () => setOpen(!open);
    const closeAllPopups = () => {
        setOpen(false);
        setShowServices(false);
        setShowSidebar(false);
    };

    const toggleSidebar = () => {
        setShowSidebar(!showSidebar);
    };

    // Helper function to render navigation links
    const renderNavLinks = (
        items: { label: KeyTextField; link_to_services?: LinkField; link_to_company?: LinkField }[],
        isServiceLink: boolean = false
    ) => {
        return items.map((item) => (
            <PrismicNextLink
                className={clsx(
                    'block border-b md:border-none hover:text-brand transition-colors duration-200 border-slate-400/20 text-2xl md:text-lg'
                )}
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

    // Animation variants for the menu
    const menuVariants: Variants = {
        hidden: { height: "5.6rem" },
        visible: { height: "24rem" },
    };

    const menuStaggeredVariant = {
        hidden: { opacity: 0 },
        visible: { opacity: 1 }
    };

    // Animation variants for expanding rows
    const expandRowVariants: Variants = {
        hidden: { opacity: 0, y: 50 },
        visible: (i: number) => ({
            opacity: 1,
            y: 0,
            transition: { delay: i * 0.1, duration: 0.6 },
        }),
    };

    return (
        <>
            {/* Wrapper div to handle hover events for the entire navbar */}
            <div
                onMouseEnter={toggleOpen}
                onMouseLeave={closeAllPopups}
                className="fixed top-0 w-full z-50"
            >
                <motion.div
                    initial="hidden"
                    animate={open ? 'visible' : 'hidden'}
                    variants={menuVariants}
                    className={clsx("w-full px-2 md:p-4 text-inherit rounded-b-lg shadow-lg", theme === "dark" ? "bg-black" : "bg-brandWhite")}
                >
                    <div className={clsx("flex flex-col", open ? 'h-auto' : 'h-10')}>
                        {/* Top Row: Logo, Desktop Navigation, and Menu Button */}
                        <div className="flex justify-between items-center">
                            {/* Logo */}
                            <Link className="z-50" onClick={() => setOpen(false)} href="/">
                                <Image
                                    className={clsx(theme === 'light' ? 'invert' : '')}
                                    src="/NdealNextBlack.svg"
                                    alt="NdealLogo logo"
                                    width={150}
                                    height={38}
                                    priority
                                />
                                <span className="sr-only">Ndeal Homepage</span>
                            </Link>

                            {/* Desktop Navigation */}
                            <div className="hidden md:flex items-center space-x-5 text-center">
                                <button
                                    onMouseEnter={() => setShowServices(true)}
                                    
                                    className="text-lg inline-flex items-center gap-1"
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
                                {/* Company Links */}
                                <div onMouseEnter={() => setShowServices(false) } className="flex gap-8">
                                    {renderNavLinks(settings.data.company)}
                                </div>
                            </div>

                            {/* Work With Us Button and Mobile Menu Button */}
                            <div className="flex md:gap-2 items-center">
                                <ThemeToggleSwitch />
                                <ButtonLink
                                    className="hero__button hidden lg:flex"
                                    field={settings.data.work_with_us}
                                >
                                    {settings.data.work_with_us_label}
                                </ButtonLink>

                                <button
                                    className="block p-2 z-50 text-hidden text-3xl text-white md:hidden"
                                    onClick={mobileToggleOpen}
                                    aria-label={open ? "Close menu" : "Open menu"}
                                >
                                    <span className={clsx('burger text-black burger-3', open ? 'is-closed' : '')}></span>
                                </button>
                            </div>
                        </div>

                        {/* Expanded Mobile Navigation */}
                        <motion.div
                            className={clsx('md:hidden flex flex-col py-4 gap-6 transition-transform duration-500', open ? "translate-y-[0%]" : "translate-y-[-100%]")}
                            variants={{
                                visible: {
                                    transition: {
                                        staggerChildren: 0.4
                                    }
                                }
                            }}
                            animate={open ? 'visible' : 'hidden'}
                            initial="hidden">
                            <button
                                onClick={toggleSidebar}
                                className={clsx("text-2xl text-brand text-left", open ? "block" : "hidden")}
                            >
                                Our Services &rarr;
                            </button>
                            {renderNavLinks(settings.data.company).map((item, index) => (<motion.li key={index} variants={menuStaggeredVariant}>{item}</motion.li>))}
                        </motion.div>

                        {/* Expanded Content: Services or Work With Us Desktop Button */}
                        <motion.div className="flex-col hidden md:flex">
                            <div className="flex items-center mt-10">
                                {showServices ? (
                                    <motion.div
                                        variants={expandRowVariants}
                                        custom={1}
                                        className="pl-4 flex flex-col gap-4 mt-2"
                                    >
                                        {renderNavLinks(settings.data.our_services, true)}
                                    </motion.div>
                                ) : (
                                    <motion.div variants={expandRowVariants} custom={2}>
                                        <ButtonLink
                                            className="hero__button hidden lg:flex"
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
            </div>

            {/* Render the SideBar component */}
            <SideBar showPopup={showSidebar} onClose={toggleSidebar}>
                {renderNavLinks(settings.data.our_services, true)}
            </SideBar>
        </>
    );
};

export default StickyNav;