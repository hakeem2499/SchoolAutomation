import Bounded from '@/Components/Bounded';
import { createClient } from '@/prismicio';
import { components } from '@/slices';
import { asText } from '@prismicio/client';
import { PrismicNextImage } from '@prismicio/next';
import { PrismicText, SliceZone } from '@prismicio/react';
import { Metadata } from 'next';
import { notFound } from 'next/navigation';
import React from 'react';

type Params = { uid: string };

/**
 * Fetches the service page data from Prismic.
 */
const fetchServicePage = async (uid: string) => {
    const client = createClient();
    return await client.getByUID('service', uid).catch(() => notFound());
};

/**
 * Service Page Component.
 */
export default async function Page({ params }: { params: Params }) {
    const page = await fetchServicePage(params.uid);

    return (
        <Bounded>
            <div className=" mt-16 grid place-items-center text-center">
                <p className="text-lg uppercase text-brand">Services</p>
                <h1 className="text-5xl text-center text-balance font-medium">
                    <PrismicText field={page.data.service} />
                </h1>
                <p className="mb-4 mt-8 max-w-xl font-medium text-lg">
                    <PrismicText field={page.data.description} />
                </p>
                <PrismicNextImage
                    field={page.data.icon}
                    className="rounded-lg "
                    quality={100}
                />
            </div>
            <div className="mx-auto">
                <SliceZone slices={page.data.slices} components={components} />
            </div>
        </Bounded>
    );
}

/**
 * Generates metadata for the service page.
 */
export async function generateMetadata({ params }: { params: Params }): Promise<Metadata> {
    const client = createClient();
    const page = await client.getByUID('service', params.uid).catch(() => null);

    if (!page) {
        return {
            title: 'Service Not Found',
            description: 'The requested service does not exist.',
        };
    }

    return {
        title: `${page.data.meta_title || asText(page.data.service) + ' service'}`,
        description: page.data.meta_description,
    };
}

/**
 * Generates static paths for all service pages.
 */
export async function generateStaticParams() {
    const client = createClient();
    const pages = await client.getAllByType('service');

    return pages.map((page) => ({ uid: page.uid }));
}