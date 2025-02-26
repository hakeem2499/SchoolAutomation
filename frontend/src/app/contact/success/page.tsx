"use client"
import Bounded from '@/Components/Bounded';
import { Spotlight } from '@/Components/ui/SpotLight';
import { useSearchParams } from 'next/navigation';
import React from 'react'

type Props = {}

const SuccessPage = (props: Props) => {
    const searchParams = useSearchParams();
    const Name = searchParams.get('name')?.toUpperCase();
    const success = searchParams.get('success');
    return (

        <Bounded className="lg:h-screen w-full mt-16  rounded-md flex md:items-center md:justify-center bg-black/[0.96] antialiased bg-grid-white/[0.02] relative overflow-hidden">
            <Spotlight />
            {(Name && success === 'true') &&

                <div className=" p-4 max-w-7xl  mx-auto relative z-10  w-full pt-20 md:pt-0">
                    <h1 className="text-4xl md:text-7xl font-bold text-center bg-clip-text text-transparent bg-gradient-to-b from-neutral-50 to-neutral-400 bg-opacity-50">
                        Thank You {Name}
                    </h1>
                    <p className="mt-4 font-medium text-base text-neutral-300 max-w-lg md:text-lg text-center mx-auto">
                        We've received your message and our Team will be in touch soon
                    </p>
                </div>
            }
        </Bounded>
    )
}

export default SuccessPage