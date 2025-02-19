"use client";
import * as React from "react";
import Switch from "@mui/material/Switch";
import { useTheme } from "../../contexts/ThemeContext"; // Adjust the import path

export default function ThemeToggleSwitch() {
    const { theme, toggleTheme } = useTheme();

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        toggleTheme(); // Toggle the theme when the switch is toggled
    };

    return (
        <Switch
            color="default"
            checked={theme === "dark"} // Set the switch state based on the current theme
            onChange={handleChange}
            inputProps={{ "aria-label": "controlled" }}
            
        />
    );
}