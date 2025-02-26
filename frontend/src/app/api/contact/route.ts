import { NextResponse } from "next/server";
import { PrismaClient } from "@prisma/client";

const prisma = new PrismaClient();

// Handle POST requests
export async function POST(request: Request) {
  try {
    const { fullName, email, phone, companyName, message } = await request.json();

    // Save the form data to the database
    const contact = await prisma.contact.create({
      data: {
        fullName,
        email,
        phone,
        companyName,
        message,
      },
    });

    return NextResponse.json(
      { message: "Form submitted successfully!", contact },
      { status: 200 }
    );
  } catch (error) {
    console.error("Error submitting form:", error);
    return NextResponse.json(
      { message: "Failed to submit form." },
      { status: 500 }
    );
  }
}

// Handle GET requests (optional, for testing)
export async function GET() {
  return NextResponse.json(
    { message: "This is a GET request. Use POST to submit the form." },
    { status: 200 }
  );
}