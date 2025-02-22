// Code generated by Slice Machine. DO NOT EDIT.

import type * as prismic from "@prismicio/client";

type Simplify<T> = { [KeyType in keyof T]: T[KeyType] };

type CaseStudyDocumentDataSlicesSlice = never;

/**
 * Content for Case Study documents
 */
interface CaseStudyDocumentData {
  /**
   * Slice Zone field in *Case Study*
   *
   * - **Field Type**: Slice Zone
   * - **Placeholder**: *None*
   * - **API ID Path**: case_study.slices[]
   * - **Tab**: Main
   * - **Documentation**: https://prismic.io/docs/field#slices
   */
  slices: prismic.SliceZone<CaseStudyDocumentDataSlicesSlice> /**
   * Meta Title field in *Case Study*
   *
   * - **Field Type**: Text
   * - **Placeholder**: A title of the page used for social media and search engines
   * - **API ID Path**: case_study.meta_title
   * - **Tab**: SEO & Metadata
   * - **Documentation**: https://prismic.io/docs/field#key-text
   */;
  meta_title: prismic.KeyTextField;

  /**
   * Meta Description field in *Case Study*
   *
   * - **Field Type**: Text
   * - **Placeholder**: A brief summary of the page
   * - **API ID Path**: case_study.meta_description
   * - **Tab**: SEO & Metadata
   * - **Documentation**: https://prismic.io/docs/field#key-text
   */
  meta_description: prismic.KeyTextField;

  /**
   * Meta Image field in *Case Study*
   *
   * - **Field Type**: Image
   * - **Placeholder**: *None*
   * - **API ID Path**: case_study.meta_image
   * - **Tab**: SEO & Metadata
   * - **Documentation**: https://prismic.io/docs/field#image
   */
  meta_image: prismic.ImageField<never>;
}

/**
 * Case Study document from Prismic
 *
 * - **API ID**: `case_study`
 * - **Repeatable**: `true`
 * - **Documentation**: https://prismic.io/docs/custom-types
 *
 * @typeParam Lang - Language API ID of the document.
 */
export type CaseStudyDocument<Lang extends string = string> =
  prismic.PrismicDocumentWithUID<
    Simplify<CaseStudyDocumentData>,
    "case_study",
    Lang
  >;

type PageDocumentDataSlicesSlice =
  | StatisticsSlice
  | ServicesSlice
  | ResourcesSlice
  | ProcessTimeLineSlice
  | CtaSectionSlice
  | CaseStudiesSlice
  | HeroSlice;

/**
 * Content for Page documents
 */
interface PageDocumentData {
  /**
   * Title field in *Page*
   *
   * - **Field Type**: Text
   * - **Placeholder**: *None*
   * - **API ID Path**: page.title
   * - **Tab**: Main
   * - **Documentation**: https://prismic.io/docs/field#key-text
   */
  title: prismic.KeyTextField;

  /**
   * Slice Zone field in *Page*
   *
   * - **Field Type**: Slice Zone
   * - **Placeholder**: *None*
   * - **API ID Path**: page.slices[]
   * - **Tab**: Main
   * - **Documentation**: https://prismic.io/docs/field#slices
   */
  slices: prismic.SliceZone<PageDocumentDataSlicesSlice> /**
   * Meta Title field in *Page*
   *
   * - **Field Type**: Text
   * - **Placeholder**: A title of the page used for social media and search engines
   * - **API ID Path**: page.meta_title
   * - **Tab**: SEO & Metadata
   * - **Documentation**: https://prismic.io/docs/field#key-text
   */;
  meta_title: prismic.KeyTextField;

  /**
   * Meta Description field in *Page*
   *
   * - **Field Type**: Text
   * - **Placeholder**: A brief summary of the page
   * - **API ID Path**: page.meta_description
   * - **Tab**: SEO & Metadata
   * - **Documentation**: https://prismic.io/docs/field#key-text
   */
  meta_description: prismic.KeyTextField;

  /**
   * Meta Image field in *Page*
   *
   * - **Field Type**: Image
   * - **Placeholder**: *None*
   * - **API ID Path**: page.meta_image
   * - **Tab**: SEO & Metadata
   * - **Documentation**: https://prismic.io/docs/field#image
   */
  meta_image: prismic.ImageField<never>;
}

/**
 * Page document from Prismic
 *
 * - **API ID**: `page`
 * - **Repeatable**: `true`
 * - **Documentation**: https://prismic.io/docs/custom-types
 *
 * @typeParam Lang - Language API ID of the document.
 */
export type PageDocument<Lang extends string = string> =
  prismic.PrismicDocumentWithUID<Simplify<PageDocumentData>, "page", Lang>;

type ResourceDocumentDataSlicesSlice = never;

/**
 * Content for Resource documents
 */
interface ResourceDocumentData {
  /**
   * Slice Zone field in *Resource*
   *
   * - **Field Type**: Slice Zone
   * - **Placeholder**: *None*
   * - **API ID Path**: resource.slices[]
   * - **Tab**: Main
   * - **Documentation**: https://prismic.io/docs/field#slices
   */
  slices: prismic.SliceZone<ResourceDocumentDataSlicesSlice> /**
   * Meta Title field in *Resource*
   *
   * - **Field Type**: Text
   * - **Placeholder**: A title of the page used for social media and search engines
   * - **API ID Path**: resource.meta_title
   * - **Tab**: SEO & Metadata
   * - **Documentation**: https://prismic.io/docs/field#key-text
   */;
  meta_title: prismic.KeyTextField;

  /**
   * Meta Description field in *Resource*
   *
   * - **Field Type**: Text
   * - **Placeholder**: A brief summary of the page
   * - **API ID Path**: resource.meta_description
   * - **Tab**: SEO & Metadata
   * - **Documentation**: https://prismic.io/docs/field#key-text
   */
  meta_description: prismic.KeyTextField;

  /**
   * Meta Image field in *Resource*
   *
   * - **Field Type**: Image
   * - **Placeholder**: *None*
   * - **API ID Path**: resource.meta_image
   * - **Tab**: SEO & Metadata
   * - **Documentation**: https://prismic.io/docs/field#image
   */
  meta_image: prismic.ImageField<never>;
}

/**
 * Resource document from Prismic
 *
 * - **API ID**: `resource`
 * - **Repeatable**: `true`
 * - **Documentation**: https://prismic.io/docs/custom-types
 *
 * @typeParam Lang - Language API ID of the document.
 */
export type ResourceDocument<Lang extends string = string> =
  prismic.PrismicDocumentWithUID<
    Simplify<ResourceDocumentData>,
    "resource",
    Lang
  >;

type ServiceDocumentDataSlicesSlice = never;

/**
 * Content for service documents
 */
interface ServiceDocumentData {
  /**
   * Service field in *service*
   *
   * - **Field Type**: Rich Text
   * - **Placeholder**: *None*
   * - **API ID Path**: service.service
   * - **Tab**: Main
   * - **Documentation**: https://prismic.io/docs/field#rich-text-title
   */
  service: prismic.RichTextField;

  /**
   * Description field in *service*
   *
   * - **Field Type**: Rich Text
   * - **Placeholder**: *None*
   * - **API ID Path**: service.description
   * - **Tab**: Main
   * - **Documentation**: https://prismic.io/docs/field#rich-text-title
   */
  description: prismic.RichTextField;

  /**
   * icon field in *service*
   *
   * - **Field Type**: Image
   * - **Placeholder**: *None*
   * - **API ID Path**: service.icon
   * - **Tab**: Main
   * - **Documentation**: https://prismic.io/docs/field#image
   */
  icon: prismic.ImageField<never>;

  /**
   * Slice Zone field in *service*
   *
   * - **Field Type**: Slice Zone
   * - **Placeholder**: *None*
   * - **API ID Path**: service.slices[]
   * - **Tab**: Main
   * - **Documentation**: https://prismic.io/docs/field#slices
   */
  slices: prismic.SliceZone<ServiceDocumentDataSlicesSlice> /**
   * Meta Title field in *service*
   *
   * - **Field Type**: Text
   * - **Placeholder**: A title of the page used for social media and search engines
   * - **API ID Path**: service.meta_title
   * - **Tab**: SEO & Metadata
   * - **Documentation**: https://prismic.io/docs/field#key-text
   */;
  meta_title: prismic.KeyTextField;

  /**
   * Meta Description field in *service*
   *
   * - **Field Type**: Text
   * - **Placeholder**: A brief summary of the page
   * - **API ID Path**: service.meta_description
   * - **Tab**: SEO & Metadata
   * - **Documentation**: https://prismic.io/docs/field#key-text
   */
  meta_description: prismic.KeyTextField;

  /**
   * Meta Image field in *service*
   *
   * - **Field Type**: Image
   * - **Placeholder**: *None*
   * - **API ID Path**: service.meta_image
   * - **Tab**: SEO & Metadata
   * - **Documentation**: https://prismic.io/docs/field#image
   */
  meta_image: prismic.ImageField<never>;
}

/**
 * service document from Prismic
 *
 * - **API ID**: `service`
 * - **Repeatable**: `true`
 * - **Documentation**: https://prismic.io/docs/custom-types
 *
 * @typeParam Lang - Language API ID of the document.
 */
export type ServiceDocument<Lang extends string = string> =
  prismic.PrismicDocumentWithUID<
    Simplify<ServiceDocumentData>,
    "service",
    Lang
  >;

/**
 * Item in *Settings → Our Services*
 */
export interface SettingsDocumentDataOurServicesItem {
  /**
   * label field in *Settings → Our Services*
   *
   * - **Field Type**: Text
   * - **Placeholder**: *None*
   * - **API ID Path**: settings.our_services[].label
   * - **Documentation**: https://prismic.io/docs/field#key-text
   */
  label: prismic.KeyTextField;

  /**
   * Link to services field in *Settings → Our Services*
   *
   * - **Field Type**: Link
   * - **Placeholder**: *None*
   * - **API ID Path**: settings.our_services[].link_to_services
   * - **Documentation**: https://prismic.io/docs/field#link-content-relationship
   */
  link_to_services: prismic.LinkField<
    string,
    string,
    unknown,
    prismic.FieldState,
    never
  >;
}

/**
 * Item in *Settings → Company*
 */
export interface SettingsDocumentDataCompanyItem {
  /**
   * label field in *Settings → Company*
   *
   * - **Field Type**: Text
   * - **Placeholder**: *None*
   * - **API ID Path**: settings.company[].label
   * - **Documentation**: https://prismic.io/docs/field#key-text
   */
  label: prismic.KeyTextField;

  /**
   * link to company field in *Settings → Company*
   *
   * - **Field Type**: Link
   * - **Placeholder**: *None*
   * - **API ID Path**: settings.company[].link_to_company
   * - **Documentation**: https://prismic.io/docs/field#link-content-relationship
   */
  link_to_company: prismic.LinkField<
    string,
    string,
    unknown,
    prismic.FieldState,
    never
  >;
}

/**
 * Item in *Settings → Policies*
 */
export interface SettingsDocumentDataPoliciesItem {
  /**
   * label field in *Settings → Policies*
   *
   * - **Field Type**: Text
   * - **Placeholder**: *None*
   * - **API ID Path**: settings.policies[].label
   * - **Documentation**: https://prismic.io/docs/field#key-text
   */
  label: prismic.KeyTextField;

  /**
   * link to policies field in *Settings → Policies*
   *
   * - **Field Type**: Link
   * - **Placeholder**: *None*
   * - **API ID Path**: settings.policies[].link_to_policies
   * - **Documentation**: https://prismic.io/docs/field#link-content-relationship
   */
  link_to_policies: prismic.LinkField<
    string,
    string,
    unknown,
    prismic.FieldState,
    never
  >;
}

/**
 * Item in *Settings → Contact Information*
 */
export interface SettingsDocumentDataContactInformationItem {
  /**
   * information field in *Settings → Contact Information*
   *
   * - **Field Type**: Rich Text
   * - **Placeholder**: *None*
   * - **API ID Path**: settings.contact_information[].information
   * - **Documentation**: https://prismic.io/docs/field#rich-text-title
   */
  information: prismic.RichTextField;

  /**
   * label field in *Settings → Contact Information*
   *
   * - **Field Type**: Text
   * - **Placeholder**: *None*
   * - **API ID Path**: settings.contact_information[].label
   * - **Documentation**: https://prismic.io/docs/field#key-text
   */
  label: prismic.KeyTextField;
}

/**
 * Content for Settings documents
 */
interface SettingsDocumentData {
  /**
   * Our Services field in *Settings*
   *
   * - **Field Type**: Group
   * - **Placeholder**: *None*
   * - **API ID Path**: settings.our_services[]
   * - **Tab**: Main
   * - **Documentation**: https://prismic.io/docs/field#group
   */
  our_services: prismic.GroupField<
    Simplify<SettingsDocumentDataOurServicesItem>
  >;

  /**
   * Company field in *Settings*
   *
   * - **Field Type**: Group
   * - **Placeholder**: *None*
   * - **API ID Path**: settings.company[]
   * - **Tab**: Main
   * - **Documentation**: https://prismic.io/docs/field#group
   */
  company: prismic.GroupField<Simplify<SettingsDocumentDataCompanyItem>>;

  /**
   * Policies field in *Settings*
   *
   * - **Field Type**: Group
   * - **Placeholder**: *None*
   * - **API ID Path**: settings.policies[]
   * - **Tab**: Main
   * - **Documentation**: https://prismic.io/docs/field#group
   */
  policies: prismic.GroupField<Simplify<SettingsDocumentDataPoliciesItem>>;

  /**
   * Contact Information field in *Settings*
   *
   * - **Field Type**: Group
   * - **Placeholder**: *None*
   * - **API ID Path**: settings.contact_information[]
   * - **Tab**: Main
   * - **Documentation**: https://prismic.io/docs/field#group
   */
  contact_information: prismic.GroupField<
    Simplify<SettingsDocumentDataContactInformationItem>
  >;

  /**
   * tagline field in *Settings*
   *
   * - **Field Type**: Rich Text
   * - **Placeholder**: *None*
   * - **API ID Path**: settings.tagline
   * - **Tab**: Main
   * - **Documentation**: https://prismic.io/docs/field#rich-text-title
   */
  tagline: prismic.RichTextField;

  /**
   * Work With Us field in *Settings*
   *
   * - **Field Type**: Link
   * - **Placeholder**: *None*
   * - **API ID Path**: settings.work_with_us
   * - **Tab**: Main
   * - **Documentation**: https://prismic.io/docs/field#link-content-relationship
   */
  work_with_us: prismic.LinkField<
    string,
    string,
    unknown,
    prismic.FieldState,
    never
  >;

  /**
   * Work With Us label field in *Settings*
   *
   * - **Field Type**: Text
   * - **Placeholder**: *None*
   * - **API ID Path**: settings.work_with_us_label
   * - **Tab**: Main
   * - **Documentation**: https://prismic.io/docs/field#key-text
   */
  work_with_us_label: prismic.KeyTextField;
}

/**
 * Settings document from Prismic
 *
 * - **API ID**: `settings`
 * - **Repeatable**: `false`
 * - **Documentation**: https://prismic.io/docs/custom-types
 *
 * @typeParam Lang - Language API ID of the document.
 */
export type SettingsDocument<Lang extends string = string> =
  prismic.PrismicDocumentWithoutUID<
    Simplify<SettingsDocumentData>,
    "settings",
    Lang
  >;

export type AllDocumentTypes =
  | CaseStudyDocument
  | PageDocument
  | ResourceDocument
  | ServiceDocument
  | SettingsDocument;

/**
 * Primary content in *CaseStudies → Default → Primary*
 */
export interface CaseStudiesSliceDefaultPrimary {
  /**
   * Heading field in *CaseStudies → Default → Primary*
   *
   * - **Field Type**: Title
   * - **Placeholder**: *None*
   * - **API ID Path**: case_studies.default.primary.heading
   * - **Documentation**: https://prismic.io/docs/field#rich-text-title
   */
  heading: prismic.TitleField;

  /**
   * Body field in *CaseStudies → Default → Primary*
   *
   * - **Field Type**: Rich Text
   * - **Placeholder**: *None*
   * - **API ID Path**: case_studies.default.primary.body
   * - **Documentation**: https://prismic.io/docs/field#rich-text-title
   */
  body: prismic.RichTextField;

  /**
   * CaseStudy field in *CaseStudies → Default → Primary*
   *
   * - **Field Type**: Content Relationship
   * - **Placeholder**: *None*
   * - **API ID Path**: case_studies.default.primary.case_study
   * - **Documentation**: https://prismic.io/docs/field#link-content-relationship
   */
  case_study: prismic.ContentRelationshipField<"case_study">;
}

/**
 * Default variation for CaseStudies Slice
 *
 * - **API ID**: `default`
 * - **Description**: Default
 * - **Documentation**: https://prismic.io/docs/slice
 */
export type CaseStudiesSliceDefault = prismic.SharedSliceVariation<
  "default",
  Simplify<CaseStudiesSliceDefaultPrimary>,
  never
>;

/**
 * Slice variation for *CaseStudies*
 */
type CaseStudiesSliceVariation = CaseStudiesSliceDefault;

/**
 * CaseStudies Shared Slice
 *
 * - **API ID**: `case_studies`
 * - **Description**: CaseStudies
 * - **Documentation**: https://prismic.io/docs/slice
 */
export type CaseStudiesSlice = prismic.SharedSlice<
  "case_studies",
  CaseStudiesSliceVariation
>;

/**
 * Primary content in *CtaSection → Default → Primary*
 */
export interface CtaSectionSliceDefaultPrimary {
  /**
   * Cta Title field in *CtaSection → Default → Primary*
   *
   * - **Field Type**: Title
   * - **Placeholder**: *None*
   * - **API ID Path**: cta_section.default.primary.cta_title
   * - **Documentation**: https://prismic.io/docs/field#rich-text-title
   */
  cta_title: prismic.TitleField;

  /**
   * Body field in *CtaSection → Default → Primary*
   *
   * - **Field Type**: Rich Text
   * - **Placeholder**: *None*
   * - **API ID Path**: cta_section.default.primary.body
   * - **Documentation**: https://prismic.io/docs/field#rich-text-title
   */
  body: prismic.RichTextField;

  /**
   * Link field in *CtaSection → Default → Primary*
   *
   * - **Field Type**: Link
   * - **Placeholder**: *None*
   * - **API ID Path**: cta_section.default.primary.link
   * - **Documentation**: https://prismic.io/docs/field#link-content-relationship
   */
  link: prismic.LinkField<string, string, unknown, prismic.FieldState, never>;

  /**
   * label field in *CtaSection → Default → Primary*
   *
   * - **Field Type**: Text
   * - **Placeholder**: *None*
   * - **API ID Path**: cta_section.default.primary.label
   * - **Documentation**: https://prismic.io/docs/field#key-text
   */
  label: prismic.KeyTextField;

  /**
   * description field in *CtaSection → Default → Primary*
   *
   * - **Field Type**: Rich Text
   * - **Placeholder**: *None*
   * - **API ID Path**: cta_section.default.primary.description
   * - **Documentation**: https://prismic.io/docs/field#rich-text-title
   */
  description: prismic.RichTextField;
}

/**
 * Default variation for CtaSection Slice
 *
 * - **API ID**: `default`
 * - **Description**: Default
 * - **Documentation**: https://prismic.io/docs/slice
 */
export type CtaSectionSliceDefault = prismic.SharedSliceVariation<
  "default",
  Simplify<CtaSectionSliceDefaultPrimary>,
  never
>;

/**
 * Slice variation for *CtaSection*
 */
type CtaSectionSliceVariation = CtaSectionSliceDefault;

/**
 * CtaSection Shared Slice
 *
 * - **API ID**: `cta_section`
 * - **Description**: CtaSection
 * - **Documentation**: https://prismic.io/docs/slice
 */
export type CtaSectionSlice = prismic.SharedSlice<
  "cta_section",
  CtaSectionSliceVariation
>;

/**
 * Primary content in *Hero → Default → Primary*
 */
export interface HeroSliceDefaultPrimary {
  /**
   * Heading field in *Hero → Default → Primary*
   *
   * - **Field Type**: Rich Text
   * - **Placeholder**: *None*
   * - **API ID Path**: hero.default.primary.heading
   * - **Documentation**: https://prismic.io/docs/field#rich-text-title
   */
  heading: prismic.RichTextField;

  /**
   * Body field in *Hero → Default → Primary*
   *
   * - **Field Type**: Rich Text
   * - **Placeholder**: *None*
   * - **API ID Path**: hero.default.primary.body
   * - **Documentation**: https://prismic.io/docs/field#rich-text-title
   */
  body: prismic.RichTextField;

  /**
   * link to services field in *Hero → Default → Primary*
   *
   * - **Field Type**: Link
   * - **Placeholder**: *None*
   * - **API ID Path**: hero.default.primary.link_to_services
   * - **Documentation**: https://prismic.io/docs/field#link-content-relationship
   */
  link_to_services: prismic.LinkField<
    string,
    string,
    unknown,
    prismic.FieldState,
    never
  >;

  /**
   * label field in *Hero → Default → Primary*
   *
   * - **Field Type**: Text
   * - **Placeholder**: *None*
   * - **API ID Path**: hero.default.primary.label
   * - **Documentation**: https://prismic.io/docs/field#key-text
   */
  label: prismic.KeyTextField;
}

/**
 * Default variation for Hero Slice
 *
 * - **API ID**: `default`
 * - **Description**: Default
 * - **Documentation**: https://prismic.io/docs/slice
 */
export type HeroSliceDefault = prismic.SharedSliceVariation<
  "default",
  Simplify<HeroSliceDefaultPrimary>,
  never
>;

/**
 * Slice variation for *Hero*
 */
type HeroSliceVariation = HeroSliceDefault;

/**
 * Hero Shared Slice
 *
 * - **API ID**: `hero`
 * - **Description**: Hero
 * - **Documentation**: https://prismic.io/docs/slice
 */
export type HeroSlice = prismic.SharedSlice<"hero", HeroSliceVariation>;

/**
 * Item in *ProcessTimeLine → Default → Primary → Process*
 */
export interface ProcessTimeLineSliceDefaultPrimaryProcessItem {
  /**
   * title field in *ProcessTimeLine → Default → Primary → Process*
   *
   * - **Field Type**: Rich Text
   * - **Placeholder**: *None*
   * - **API ID Path**: process_time_line.default.primary.process[].title
   * - **Documentation**: https://prismic.io/docs/field#rich-text-title
   */
  title: prismic.RichTextField;

  /**
   * image field in *ProcessTimeLine → Default → Primary → Process*
   *
   * - **Field Type**: Image
   * - **Placeholder**: *None*
   * - **API ID Path**: process_time_line.default.primary.process[].image
   * - **Documentation**: https://prismic.io/docs/field#image
   */
  image: prismic.ImageField<never>;

  /**
   * Body field in *ProcessTimeLine → Default → Primary → Process*
   *
   * - **Field Type**: Rich Text
   * - **Placeholder**: *None*
   * - **API ID Path**: process_time_line.default.primary.process[].body
   * - **Documentation**: https://prismic.io/docs/field#rich-text-title
   */
  body: prismic.RichTextField;

  /**
   * label field in *ProcessTimeLine → Default → Primary → Process*
   *
   * - **Field Type**: Text
   * - **Placeholder**: *None*
   * - **API ID Path**: process_time_line.default.primary.process[].label
   * - **Documentation**: https://prismic.io/docs/field#key-text
   */
  label: prismic.KeyTextField;
}

/**
 * Primary content in *ProcessTimeLine → Default → Primary*
 */
export interface ProcessTimeLineSliceDefaultPrimary {
  /**
   * Heading field in *ProcessTimeLine → Default → Primary*
   *
   * - **Field Type**: Rich Text
   * - **Placeholder**: *None*
   * - **API ID Path**: process_time_line.default.primary.heading
   * - **Documentation**: https://prismic.io/docs/field#rich-text-title
   */
  heading: prismic.RichTextField;

  /**
   * Process field in *ProcessTimeLine → Default → Primary*
   *
   * - **Field Type**: Group
   * - **Placeholder**: *None*
   * - **API ID Path**: process_time_line.default.primary.process[]
   * - **Documentation**: https://prismic.io/docs/field#group
   */
  process: prismic.GroupField<
    Simplify<ProcessTimeLineSliceDefaultPrimaryProcessItem>
  >;
}

/**
 * Default variation for ProcessTimeLine Slice
 *
 * - **API ID**: `default`
 * - **Description**: Default
 * - **Documentation**: https://prismic.io/docs/slice
 */
export type ProcessTimeLineSliceDefault = prismic.SharedSliceVariation<
  "default",
  Simplify<ProcessTimeLineSliceDefaultPrimary>,
  never
>;

/**
 * Slice variation for *ProcessTimeLine*
 */
type ProcessTimeLineSliceVariation = ProcessTimeLineSliceDefault;

/**
 * ProcessTimeLine Shared Slice
 *
 * - **API ID**: `process_time_line`
 * - **Description**: ProcessTimeLine
 * - **Documentation**: https://prismic.io/docs/slice
 */
export type ProcessTimeLineSlice = prismic.SharedSlice<
  "process_time_line",
  ProcessTimeLineSliceVariation
>;

/**
 * Primary content in *Resources → Default → Primary*
 */
export interface ResourcesSliceDefaultPrimary {
  /**
   * Heading field in *Resources → Default → Primary*
   *
   * - **Field Type**: Title
   * - **Placeholder**: *None*
   * - **API ID Path**: resources.default.primary.heading
   * - **Documentation**: https://prismic.io/docs/field#rich-text-title
   */
  heading: prismic.TitleField;

  /**
   * Link field in *Resources → Default → Primary*
   *
   * - **Field Type**: Link
   * - **Placeholder**: *None*
   * - **API ID Path**: resources.default.primary.link
   * - **Documentation**: https://prismic.io/docs/field#link-content-relationship
   */
  link: prismic.LinkField<string, string, unknown, prismic.FieldState, never>;

  /**
   * label field in *Resources → Default → Primary*
   *
   * - **Field Type**: Text
   * - **Placeholder**: *None*
   * - **API ID Path**: resources.default.primary.label
   * - **Documentation**: https://prismic.io/docs/field#key-text
   */
  label: prismic.KeyTextField;

  /**
   * Resource field in *Resources → Default → Primary*
   *
   * - **Field Type**: Content Relationship
   * - **Placeholder**: *None*
   * - **API ID Path**: resources.default.primary.resource
   * - **Documentation**: https://prismic.io/docs/field#link-content-relationship
   */
  resource: prismic.ContentRelationshipField<"resource">;
}

/**
 * Default variation for Resources Slice
 *
 * - **API ID**: `default`
 * - **Description**: Default
 * - **Documentation**: https://prismic.io/docs/slice
 */
export type ResourcesSliceDefault = prismic.SharedSliceVariation<
  "default",
  Simplify<ResourcesSliceDefaultPrimary>,
  never
>;

/**
 * Slice variation for *Resources*
 */
type ResourcesSliceVariation = ResourcesSliceDefault;

/**
 * Resources Shared Slice
 *
 * - **API ID**: `resources`
 * - **Description**: Resources
 * - **Documentation**: https://prismic.io/docs/slice
 */
export type ResourcesSlice = prismic.SharedSlice<
  "resources",
  ResourcesSliceVariation
>;

/**
 * Item in *Services → Default → Primary → Services*
 */
export interface ServicesSliceDefaultPrimaryServicesItem {
  /**
   * service field in *Services → Default → Primary → Services*
   *
   * - **Field Type**: Content Relationship
   * - **Placeholder**: *None*
   * - **API ID Path**: services.default.primary.services[].service
   * - **Documentation**: https://prismic.io/docs/field#link-content-relationship
   */
  service: prismic.ContentRelationshipField<"service">;
}

/**
 * Primary content in *Services → Default → Primary*
 */
export interface ServicesSliceDefaultPrimary {
  /**
   * Heading field in *Services → Default → Primary*
   *
   * - **Field Type**: Title
   * - **Placeholder**: *None*
   * - **API ID Path**: services.default.primary.heading
   * - **Documentation**: https://prismic.io/docs/field#rich-text-title
   */
  heading: prismic.TitleField;

  /**
   * Link field in *Services → Default → Primary*
   *
   * - **Field Type**: Link
   * - **Placeholder**: *None*
   * - **API ID Path**: services.default.primary.link
   * - **Documentation**: https://prismic.io/docs/field#link-content-relationship
   */
  link: prismic.LinkField<string, string, unknown, prismic.FieldState, never>;

  /**
   * label field in *Services → Default → Primary*
   *
   * - **Field Type**: Text
   * - **Placeholder**: *None*
   * - **API ID Path**: services.default.primary.label
   * - **Documentation**: https://prismic.io/docs/field#key-text
   */
  label: prismic.KeyTextField;

  /**
   * Services field in *Services → Default → Primary*
   *
   * - **Field Type**: Group
   * - **Placeholder**: *None*
   * - **API ID Path**: services.default.primary.services[]
   * - **Documentation**: https://prismic.io/docs/field#group
   */
  services: prismic.GroupField<
    Simplify<ServicesSliceDefaultPrimaryServicesItem>
  >;
}

/**
 * Default variation for Services Slice
 *
 * - **API ID**: `default`
 * - **Description**: Default
 * - **Documentation**: https://prismic.io/docs/slice
 */
export type ServicesSliceDefault = prismic.SharedSliceVariation<
  "default",
  Simplify<ServicesSliceDefaultPrimary>,
  never
>;

/**
 * Slice variation for *Services*
 */
type ServicesSliceVariation = ServicesSliceDefault;

/**
 * Services Shared Slice
 *
 * - **API ID**: `services`
 * - **Description**: Services
 * - **Documentation**: https://prismic.io/docs/slice
 */
export type ServicesSlice = prismic.SharedSlice<
  "services",
  ServicesSliceVariation
>;

/**
 * Item in *Statistics → Default → Primary → stats*
 */
export interface StatisticsSliceDefaultPrimaryStatsItem {
  /**
   * Body field in *Statistics → Default → Primary → stats*
   *
   * - **Field Type**: Rich Text
   * - **Placeholder**: *None*
   * - **API ID Path**: statistics.default.primary.stats[].body
   * - **Documentation**: https://prismic.io/docs/field#rich-text-title
   */
  body: prismic.RichTextField;

  /**
   * label field in *Statistics → Default → Primary → stats*
   *
   * - **Field Type**: Text
   * - **Placeholder**: *None*
   * - **API ID Path**: statistics.default.primary.stats[].label
   * - **Documentation**: https://prismic.io/docs/field#key-text
   */
  label: prismic.KeyTextField;
}

/**
 * Primary content in *Statistics → Default → Primary*
 */
export interface StatisticsSliceDefaultPrimary {
  /**
   * Heading field in *Statistics → Default → Primary*
   *
   * - **Field Type**: Title
   * - **Placeholder**: *None*
   * - **API ID Path**: statistics.default.primary.heading
   * - **Documentation**: https://prismic.io/docs/field#rich-text-title
   */
  heading: prismic.TitleField;

  /**
   * Body field in *Statistics → Default → Primary*
   *
   * - **Field Type**: Rich Text
   * - **Placeholder**: *None*
   * - **API ID Path**: statistics.default.primary.body
   * - **Documentation**: https://prismic.io/docs/field#rich-text-title
   */
  body: prismic.RichTextField;

  /**
   * stats field in *Statistics → Default → Primary*
   *
   * - **Field Type**: Group
   * - **Placeholder**: *None*
   * - **API ID Path**: statistics.default.primary.stats[]
   * - **Documentation**: https://prismic.io/docs/field#group
   */
  stats: prismic.GroupField<Simplify<StatisticsSliceDefaultPrimaryStatsItem>>;
}

/**
 * Default variation for Statistics Slice
 *
 * - **API ID**: `default`
 * - **Description**: Default
 * - **Documentation**: https://prismic.io/docs/slice
 */
export type StatisticsSliceDefault = prismic.SharedSliceVariation<
  "default",
  Simplify<StatisticsSliceDefaultPrimary>,
  never
>;

/**
 * Slice variation for *Statistics*
 */
type StatisticsSliceVariation = StatisticsSliceDefault;

/**
 * Statistics Shared Slice
 *
 * - **API ID**: `statistics`
 * - **Description**: Statistics
 * - **Documentation**: https://prismic.io/docs/slice
 */
export type StatisticsSlice = prismic.SharedSlice<
  "statistics",
  StatisticsSliceVariation
>;

declare module "@prismicio/client" {
  interface CreateClient {
    (
      repositoryNameOrEndpoint: string,
      options?: prismic.ClientConfig,
    ): prismic.Client<AllDocumentTypes>;
  }

  interface CreateWriteClient {
    (
      repositoryNameOrEndpoint: string,
      options: prismic.WriteClientConfig,
    ): prismic.WriteClient<AllDocumentTypes>;
  }

  interface CreateMigration {
    (): prismic.Migration<AllDocumentTypes>;
  }

  namespace Content {
    export type {
      CaseStudyDocument,
      CaseStudyDocumentData,
      CaseStudyDocumentDataSlicesSlice,
      PageDocument,
      PageDocumentData,
      PageDocumentDataSlicesSlice,
      ResourceDocument,
      ResourceDocumentData,
      ResourceDocumentDataSlicesSlice,
      ServiceDocument,
      ServiceDocumentData,
      ServiceDocumentDataSlicesSlice,
      SettingsDocument,
      SettingsDocumentData,
      SettingsDocumentDataOurServicesItem,
      SettingsDocumentDataCompanyItem,
      SettingsDocumentDataPoliciesItem,
      SettingsDocumentDataContactInformationItem,
      AllDocumentTypes,
      CaseStudiesSlice,
      CaseStudiesSliceDefaultPrimary,
      CaseStudiesSliceVariation,
      CaseStudiesSliceDefault,
      CtaSectionSlice,
      CtaSectionSliceDefaultPrimary,
      CtaSectionSliceVariation,
      CtaSectionSliceDefault,
      HeroSlice,
      HeroSliceDefaultPrimary,
      HeroSliceVariation,
      HeroSliceDefault,
      ProcessTimeLineSlice,
      ProcessTimeLineSliceDefaultPrimaryProcessItem,
      ProcessTimeLineSliceDefaultPrimary,
      ProcessTimeLineSliceVariation,
      ProcessTimeLineSliceDefault,
      ResourcesSlice,
      ResourcesSliceDefaultPrimary,
      ResourcesSliceVariation,
      ResourcesSliceDefault,
      ServicesSlice,
      ServicesSliceDefaultPrimaryServicesItem,
      ServicesSliceDefaultPrimary,
      ServicesSliceVariation,
      ServicesSliceDefault,
      StatisticsSlice,
      StatisticsSliceDefaultPrimaryStatsItem,
      StatisticsSliceDefaultPrimary,
      StatisticsSliceVariation,
      StatisticsSliceDefault,
    };
  }
}
