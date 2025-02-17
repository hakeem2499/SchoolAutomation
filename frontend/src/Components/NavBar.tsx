

import { Content } from '@prismicio/client'
import Link from 'next/link';
import { usePathname } from 'next/navigation';
import React, { JSX, useState } from 'react'
import Logo from './Logo';
import clsx from 'clsx';

type Props = {
    settings: Content.SettingsDocument;
}

const NavBar: React.FC<Props> = ({ settings }): JSX.Element => {

    const [open, setOpen] = useState<boolean>(false)
    const pathname = usePathname()
    const toggleOpen = () => setOpen(!open)


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
                <div className=" ">

                </div>
            </div>
        </nav>
    )
}

export default NavBar;