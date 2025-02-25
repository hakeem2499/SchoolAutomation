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
 * Fetches the. page data from Prismic.
 */
const fetchCaseStudyPage = async (uid: string) => {
    const client = createClient();
    return await client.getByUID('case_study', uid).catch(() => notFound());
};

/**
 * CaseStudy Page Component.
 */
export default async function Page({ params }: { params: Params }) {
    const page = await fetchCaseStudyPage(params.uid);

    return (
        <Bounded>
            <div className="relative mt-16 grid place-items-center text-center">
                <p className="text-lg uppercase text-brand">Case Study</p>
                <h1 className="text-7xl font-medium">
                    <PrismicText field={page.data.heading} />
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
 * Generates metadata for the. page.
 */
export async function generateMetadata({ params }: { params: Params }): Promise<Metadata> {
    const client = createClient();
    const page = await client.getByUID('case_study', params.uid).catch(() => null);

    if (!page) {
        return {
            title: 'CaseStudy Not Found',
            description: 'The requested. does not exist.',
        };
    }

    return {
        title: `${page.data.meta_title || asText(page.data.heading) + '.'}`,
        description: page.data.meta_description,
    };
}

/**
 * Generates static paths for all. pages.
 */
export async function generateStaticParams() {
    const client = createClient();
    const pages = await client.getAllByType('case_study');

    return pages.map((page) => ({ uid: page.uid }));
}