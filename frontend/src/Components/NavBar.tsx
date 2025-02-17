

import { asLink, Content } from '@prismicio/client'
import Link from 'next/link';
import { usePathname } from 'next/navigation';
import React, { JSX, useState } from 'react'
import Logo from './Logo';
import clsx from 'clsx';
import { PrismicNextLink } from '@prismicio/next';
import SideBar from './SideBar';

type Props = {
    settings: Content.SettingsDocument;
}

const NavBar: React.FC<Props> = ({ settings }): JSX.Element => {

    const [open, setOpen] = useState<boolean>(false);
    const [showServices, setShowServices] = useState<boolean>(false);
    const pathname = usePathname();
    const toggleOpen = () => setOpen(!open);
    const closeAllPopups = () => {
        setOpen(false);
        setShowServices(false);
    }



    return (
        <nav className='md:py-6 px-4 py-4' aria-label='Main'>
            <div className="mx-auto flex max-w-8xl flex-col justify-between py-2 font-medium text-white md:flex-row md:items-center">
                <div className="flex items-center justify-between">
                    <Link onClick={() => setOpen(false)} href="/" >
                        <Logo />
                        <span className="sr-only">Ndeal Homepage</span>
                    </Link>
                    <button className="block p-2 text-3xl text-white md:hidden" onClick={toggleOpen}>
                        <span className={clsx('burger hidden  burger-3', open ? 'is-closed' : '')}></span>
                    </button>
                </div>

                {/* MobileNavigation */}
                <div className={clsx("gap-4 fixed top-0 left-0 right-0 z-40 flex flex-col pl=4 pt-4 transition-transform duration-300 ease-in-out motion-reduce:transition-none w-full md:hidden", open ? 'translate-y-[50%]' : 'translate-y-0')}>
                    <div className="grid gap-4">
                        <div>
                            <button className="block px-2 mt-4">
                                Our Services
                            </button>
                            <SideBar showPopup={showServices}>

                                {settings.data.our_services.map((service) => {
                                    return (

                                        <PrismicNextLink
                                            className='block px-2 text-2xl  '
                                            key={service.label}
                                            field={service.link_to_services}
                                            onClick={() => setOpen(false)}>
                                            {service.label}
                                            aria-current={
                                                pathname.includes(asLink(service.link_to_services) as string)
                                                    ? "page"
                                                    : undefined
                                            }
                                        </PrismicNextLink>
                                    );
                                })}
                            </SideBar>
                        </div>
                        {settings.data.company.map((company) => {
                            return (
                                <PrismicNextLink
                                            className='block px-2 text-2xl  '
                                            key={company.label}
                                            field={company.link_to_company}
                                            onClick={() => setOpen(false)}>
                                            {company.label}
                                            aria-current={
                                                pathname.includes(asLink(company.link_to_company) as string)
                                                    ? "page"
                                                    : undefined
                                            }
                                        </PrismicNextLink>
                            )
                        }
                    </div>
                </div>
            </div>
        </nav>
    )
}

export default NavBar;