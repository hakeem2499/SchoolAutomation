import type { Config } from "tailwindcss";

export default {
  content: [
    "./src/pages/**/*.{js,ts,jsx,tsx,mdx}",
    "./src/components/**/*.{js,ts,jsx,tsx,mdx}",
    "./src/app/**/*.{js,ts,jsx,tsx,mdx}",
    "./src/slices/**/*.{js,ts,jsx,tsx,mdx}",
  ],
  theme: {
    extend: {
      colors: {
        background: "var(--background)",
        foreground: "var(--foreground)",
        brand: "#ff8c00",
        brandWhite: "#f8f8f8",
        primary:"#0c0c0c",
        secondary:"#171717"
      },
      fontFamily: {
        sans: ["var(--font-dm-sans)"],
      },
      keyframes: {
        fadeInUp: {
					'0%': {
						opacity: '0',
						transform: 'translate3d(0, -20px, 0);'
					},
					'100%': {
						opacity: '1',
						transform: 'translateZ(0)'
					}
				},
      },
      animation:{
        fadeInUp: 'fadeInUp 0.5s ease-out forwards',
      }

    },
  },
  plugins: [require("@tailwindcss/typography")],
} satisfies Config;
