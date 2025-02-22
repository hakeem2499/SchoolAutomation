// app/components/CaseStudiesServer.tsx
import { Content, isFilled } from "@prismicio/client";
import { createClient } from "@/prismicio";
import CaseStudiesClient from "./CaseStudiesClient";



const fetchCaseStudies = async (slice: Content.CaseStudiesSlice) => {
  const client = createClient();

  // Fetch all case studies
  const caseStudies = await Promise.all(
    slice.primary.casestudies.map(async (item) => {
      if (isFilled.contentRelationship(item.case_study)) {
        return await client.getByID<Content.CaseStudyDocument>(item.case_study.id);
      }
      return null;
    })
  );

  return caseStudies.filter((case_study) => case_study !== null) as Content.CaseStudyDocument[];
};

export default async function CaseStudiesServer({ slice }: { slice: Content.CaseStudiesSlice }) {
  const caseStudies = await fetchCaseStudies(slice);

  return <CaseStudiesClient slice={slice} caseStudies={caseStudies} />;
}