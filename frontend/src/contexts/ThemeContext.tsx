"use client";
import { createContext, useContext, useState, useEffect, ReactNode } from "react";

// Define the type for the context value
type ThemeContextType = {
    theme: "light" | "dark";
    toggleTheme: () => void;
};

// Create the context with a default value
const ThemeContext = createContext<ThemeContextType>({
    theme: "dark",
    toggleTheme: () => {}, // Default empty function
});



interface Props {
    children: ReactNode;
}

export const ThemeProvider: React.FC<Props> = ({ children }) => {
    const [theme, setTheme] = useState<"light" | "dark">("dark");

    // Function to toggle the theme
    const toggleTheme = () => {
        setTheme((prevTheme) => (prevTheme === "light" ? "dark" : "light"));
    };

    useEffect(() => {
        const savedTheme = localStorage.getItem("theme") as "light" | "dark" | null;
        if (savedTheme) {
            setTheme(savedTheme);
        }
    }, []);

    // Update the data-theme attribute on the HTML element whenever the theme changes
    useEffect(() => {
        document.documentElement.setAttribute("data-theme", theme);
        localStorage.setItem("theme", theme);
    }, [theme]);

    // Provide the theme and toggle function to the context
    return (
        <ThemeContext.Provider value={{ theme, toggleTheme }}>
            {children}
        </ThemeContext.Provider>
    );
};

// Custom hook to use the theme context
export const useTheme = () => useContext(ThemeContext);