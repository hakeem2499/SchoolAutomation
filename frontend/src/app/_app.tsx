import type { AppProps } from "next/app";
import { ThemeProvider } from "../contexts/ThemeContext"; // Adjust the import path
import "../styles/globals.css"; // Your global styles

function MyApp({ Component, pageProps }: AppProps) {
    return (
        <ThemeProvider>
            <Component {...pageProps} />
        </ThemeProvider>
    );
}

export default MyApp;