

import Bounded from '@/Components/Bounded';
import { createClient } from '@/prismicio'
import { PrismicNextImage } from '@prismicio/next';
import { PrismicText, SliceZone } from '@prismicio/react';
import { notFound } from 'next/navigation';
import React from 'react'

type Params = {
    uid: string
}

export default async function Page({ params }: { params: Params }) {
    const client = createClient();
    const page = await client.getByUID("service", params.uid)
        .catch(() => notFound());

    return (
        <Bounded>
            <div className="relative grid place-items-center text-center">

                <h1 className="text-7xl font-medium">
                    <PrismicText field={page.data.service} />
                    <p className="text-lg text-yellow-500">Case Study</p>
                </h1>
                <p className="mb-4 mt-8 max-w-xl text-lg text-slate-300">
                    <PrismicText field={page.data.description} />
                </p>
                <PrismicNextImage
                    field={page.data.icon}
                    className="rounded-lg"
                    quality={100}
                />
            </div>
            <div className="mx-auto">
                <SliceZone slices={page.data.slices} components={components} />
            </div>
        </Bounded>
    )
}