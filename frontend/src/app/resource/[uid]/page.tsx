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
 * Fetches the Resource page data from Prismic.
 */
const fetchResourcePage = async (uid: string) => {
    const client = createClient();
    return await client.getByUID('resource', uid).catch(() => notFound());
};

/**
 * Resource Page Component.
 */
export default async function Page({ params }: { params: Params }) {
    const page = await fetchResourcePage(params.uid);

    return (
        <Bounded>
            <div className="relative mt-16 grid place-items-center text-center">
                <p className="text-lg uppercase text-brand">Resources</p>
                <h1 className="text-7xl font-medium">
                    <PrismicText field={page.data.resource} />
                </h1>
                <p className="mb-4 mt-8 max-w-xl text-lg">
                    <PrismicText field={page.data.description} />
                </p>
                <PrismicNextImage
                    field={page.data.image}
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
 * Generates metadata for the Resource page.
 */
export async function generateMetadata({ params }: { params: Params }): Promise<Metadata> {
    const client = createClient();
    const page = await client.getByUID('resource', params.uid).catch(() => null);

    if (!page) {
        return {
            title: 'Resource Not Found',
            description: 'The requested Resource does not exist.',
        };
    }

    return {
        title: `${page.data.meta_title || asText(page.data.resource) + ' Resource'}`,
        description: page.data.meta_description,
    };
}

/**
 * Generates static paths for all Resource pages.
 */
export async function generateStaticParams() {
    const client = createClient();
    const pages = await client.getAllByType('resource');

    return pages.map((page) => ({ uid: page.uid }));
}