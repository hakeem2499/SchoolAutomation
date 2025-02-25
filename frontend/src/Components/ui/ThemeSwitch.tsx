"use client";
import * as React from "react";
import { useTheme } from "../../contexts/ThemeContext"; // Adjust the import path
import { motion, AnimatePresence } from "framer-motion"; // Import Framer Motion

function DarkMoon(props: React.SVGProps<SVGSVGElement>) {
    return (
        <svg xmlns="http://www.w3.org/2000/svg" width="1em" height="1em" viewBox="0 0 256 256" {...props}>
            <path fill="currentColor" d="M235.54 150.21a104.84 104.84 0 0 1-37 52.91A104 104 0 0 1 32 120a103.1 103.1 0 0 1 20.88-62.52a104.84 104.84 0 0 1 52.91-37a8 8 0 0 1 10 10a88.08 88.08 0 0 0 109.8 109.8a8 8 0 0 1 10 10Z"></path>
        </svg>
    );
}

function LightStar(props: React.SVGProps<SVGSVGElement>) {
    return (
        <svg xmlns="http://www.w3.org/2000/svg" width="1em" height="1em" viewBox="0 0 256 256" {...props}>
            <path fill="currentColor" d="M140 32v32a12 12 0 0 1-24 0V32a12 12 0 0 1 24 0m33.25 62.75a12 12 0 0 0 8.49-3.52l22.63-22.63a12 12 0 0 0-17-17l-22.6 22.66a12 12 0 0 0 8.48 20.49M224 116h-32a12 12 0 0 0 0 24h32a12 12 0 0 0 0-24m-42.26 48.77a12 12 0 1 0-17 17l22.63 22.63a12 12 0 0 0 17-17ZM128 180a12 12 0 0 0-12 12v32a12 12 0 0 0 24 0v-32a12 12 0 0 0-12-12m-53.74-15.23L51.63 187.4a12 12 0 0 0 17 17l22.63-22.63a12 12 0 1 0-17-17M76 128a12 12 0 0 0-12-12H32a12 12 0 0 0 0 24h32a12 12 0 0 0 12-12m-7.4-76.37a12 12 0 1 0-17 17l22.66 22.6a12 12 0 0 0 17-17Z"></path>
        </svg>
    );
}

export default function ThemeToggleSwitch() {
    const { theme, toggleTheme } = useTheme();

    return (
        <button
            className="bg-white p-1 z-50 rounded-full"
            onClick={toggleTheme}
            aria-label={theme === "dark" ? "Switch to light theme" : "Switch to dark theme"}
        >
            <AnimatePresence mode="wait">
                <motion.span
                    key={theme}
                    initial={{ opacity: 0 }}
                    animate={{ opacity: 1 }}
                    exit={{ opacity: 0 }}
                    transition={{ duration: 0.2 }}
                    className="rounded-full text-black"
                >
                    {theme === "dark" ? <LightStar /> : <DarkMoon />}
                </motion.span>
            </AnimatePresence>
        </button>
    );
}