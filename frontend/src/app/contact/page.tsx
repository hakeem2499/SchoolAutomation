"use client";
import React, { SVGProps, useState } from "react";
import { useForm, SubmitHandler } from "react-hook-form";
import { motion, AnimatePresence } from "framer-motion";
import Bounded from "@/Components/Bounded";
import { CarouselControl } from "@/slices/Resources/ResourcesClient";
import { useRouter } from "next/navigation";




function Spinner(props: SVGProps<SVGSVGElement>) {
    return (
        <svg xmlns="http://www.w3.org/2000/svg" width="1em" height="1em" viewBox="0 0 24 24" {...props}><g><rect width="2" height="5" x="11" y="1" fill="currentColor" opacity=".14"></rect><rect width="2" height="5" x="11" y="1" fill="currentColor" opacity=".29" transform="rotate(30 12 12)"></rect><rect width="2" height="5" x="11" y="1" fill="currentColor" opacity=".43" transform="rotate(60 12 12)"></rect><rect width="2" height="5" x="11" y="1" fill="currentColor" opacity=".57" transform="rotate(90 12 12)"></rect><rect width="2" height="5" x="11" y="1" fill="currentColor" opacity=".71" transform="rotate(120 12 12)"></rect><rect width="2" height="5" x="11" y="1" fill="currentColor" opacity=".86" transform="rotate(150 12 12)"></rect><rect width="2" height="5" x="11" y="1" fill="currentColor" transform="rotate(180 12 12)"></rect><animateTransform attributeName="transform" calcMode="discrete" dur="0.75s" repeatCount="indefinite" type="rotate" values="0 12 12;30 12 12;60 12 12;90 12 12;120 12 12;150 12 12;180 12 12;210 12 12;240 12 12;270 12 12;300 12 12;330 12 12;360 12 12"></animateTransform></g></svg>
    )
}

type FormValues = {
    fullName: string;
    email: string;
    phone: string;
    companyName: string;
    message: string;
};

const ContactForm = () => {
    const [step, setStep] = useState<number>(1); // Track current step
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const router = useRouter(); // Move useRouter to the top level

    const {
        register,
        handleSubmit,
        formState: { errors },
        trigger,
        reset, // Add reset to clear form fields
    } = useForm<FormValues>();

    // Proceed to the next step
    const nextStep = async () => {
        let isValid = false;

        if (step === 1) {
            isValid = await trigger("fullName"); // Validate full name
        } else if (step === 2) {
            isValid = await trigger(["email", "phone"]); // Validate email and phone
        }

        if (isValid) {
            setStep((prev) => prev + 1);
        }
    };

    // Go back to the previous step
    const prevStep = () => {
        setStep((prev) => prev - 1);
    };

    // Handle form submission
    const onSubmit: SubmitHandler<FormValues> = async (data) => {
        const Name = data.fullName;
        let success: boolean = false;
        setIsLoading(true);

        try {
            const response = await fetch("/api/contact", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(data),
            });

            if (response.ok) {
                alert("Thank you for contacting us! We'll get back to you soon.");
                success = true;
                let url = `/contact/success?name=${Name}&success=${success.toString()}`;
                router.push(url);
                reset(); // Reset form fields
                setStep(1); // Reset to the first step
            } else {
                alert("Failed to submit the form. Please try again.");
            }
        } catch (error) {
            console.error("Error submitting form:", error);
            alert("An error occurred. Please try again.");
        } finally {
            setIsLoading(false); // Reset loading state
        }
    };

    return (
        <Bounded className="w-full min-h-screen mx-auto mt-24 p-6 rounded-lg shadow-lg">
            <h2 className="text-2xl font-bold mb-6 text-center">Contact Us</h2>
            <form onSubmit={handleSubmit(onSubmit)}>
                <AnimatePresence mode="wait">
                    {step === 1 && (
                        <motion.div
                            key="step1"
                            initial={{ opacity: 0, y: 50 }}
                            animate={{ opacity: 1, y: 0 }}
                            exit={{ opacity: 0, y: -50 }}
                            transition={{ duration: 0.3 }}
                            className="space-y-4 w-full"
                        >
                            <div>
                                <label htmlFor="fullName" className="block text-sm font-medium">
                                    Full Name
                                </label>
                                <input
                                    type="text"
                                    id="fullName"
                                    {...register("fullName", { required: "Full name is required" })}
                                />
                                {errors.fullName && (
                                    <p className="text-red-500 text-sm mt-1">{errors.fullName.message}</p>
                                )}
                            </div>
                            <CarouselControl
                                type="next"
                                handleClick={nextStep}
                                title="Go to Next"
                            />
                        </motion.div>
                    )}

                    {step === 2 && (
                        <motion.div
                            key="step2"
                            initial={{ opacity: 0, y: 50 }}
                            animate={{ opacity: 1, y: 0 }}
                            exit={{ opacity: 0, y: -50 }}
                            transition={{ duration: 0.3 }}
                            className="space-y-4"
                        >
                            <div>
                                <label htmlFor="email" className="block text-sm font-medium">
                                    Email
                                </label>
                                <input
                                    type="email"
                                    id="email"
                                    {...register("email", {
                                        required: "Email is required",
                                        pattern: {
                                            value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                                            message: "Invalid email address",
                                        },
                                    })}
                                />
                                {errors.email && (
                                    <p className="text-red-500 text-sm mt-1">{errors.email.message}</p>
                                )}
                            </div>
                            <div>
                                <label htmlFor="phone" className="block text-sm font-medium">
                                    Phone Number
                                </label>
                                <input
                                    type="tel"
                                    id="phone"
                                    {...register("phone", {
                                        required: "Phone number is required",
                                        pattern: {
                                            value: /^[0-9]+$/,
                                            message: "Invalid phone number",
                                        },
                                    })}
                                />
                                {errors.phone && (
                                    <p className="text-red-500 text-sm mt-1">{errors.phone.message}</p>
                                )}
                            </div>
                            <div className="flex justify-between">
                                <CarouselControl
                                    type="previous"
                                    handleClick={prevStep}
                                    title="Go to Previous"
                                />
                                <CarouselControl
                                    type="next"
                                    handleClick={nextStep}
                                    title="Go to Next"
                                />
                            </div>
                        </motion.div>
                    )}

                    {step === 3 && (
                        <motion.div
                            key="step3"
                            initial={{ opacity: 0, y: 50 }}
                            animate={{ opacity: 1, y: 0 }}
                            exit={{ opacity: 0, y: -50 }}
                            transition={{ duration: 0.3 }}
                            className="space-y-4"
                        >
                            <div>
                                <label htmlFor="companyName" className="block text-sm font-medium">
                                    Company Name
                                </label>
                                <input
                                    type="text"
                                    id="companyName"
                                    {...register("companyName", { required: "Company name is required" })}
                                />
                                {errors.companyName && (
                                    <p className="text-red-500 text-sm mt-1">{errors.companyName.message}</p>
                                )}
                            </div>
                            <div>
                                <label htmlFor="message" className="block text-sm font-medium">
                                    Share more about what we can do for you
                                </label>
                                <textarea
                                    id="message"
                                    {...register("message", { required: "Message is required" })}
                                    rows={4}
                                />
                                {errors.message && (
                                    <p className="text-red-500 text-sm mt-1">{errors.message.message}</p>
                                )}
                            </div>
                            <div className="flex justify-between">
                                <CarouselControl
                                    type="previous"
                                    handleClick={prevStep}
                                    title="Go to Previous"
                                />
                                <button
                                    type="submit"
                                    className="bg-brand text-white py-2 px-4 rounded-md opacity-75 hover:opacity-100 transition-opacity duration-200"
                                    disabled={isLoading}
                                    aria-disabled={isLoading}
                                    aria-busy={isLoading}
                                >
                                    {isLoading ? <span><Spinner/></span> : "Submit"}
                                </button>
                            </div>
                        </motion.div>
                    )}
                </AnimatePresence>
            </form>
        </Bounded>
    );
};

export default ContactForm;