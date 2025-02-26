
import { PrismaClient } from "@prisma/client";
import { NextApiRequest, NextApiResponse } from "next";

const prisma = new PrismaClient();

export default async function handler(
  req: NextApiRequest,
  res: NextApiResponse
) {
  if (req.method === "POST") {
    const { fullName, email, phone, companyName, message } = req.body;

    try {
      const contact = await prisma.contact.create({
        data: {
          fullName,
          email,
          phone,
          companyName,
          message,
        },
      });
      res.status(200).json({ message: "Form submitted successfully!", contact });
    } catch (error) {
      console.error("Error submitting form:", error);
      res.status(500).json({ message: "Failed to submit form." });
    }
  } else {
    res.status(405).json({ message: "Method not allowed." });
  }
}