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
        background: "#000000",
        foreground: "var(--text-color)",
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
        spotlight: {
          "0%": {
            opacity: '0',
            transform: "translate(-72%, -62%) scale(0.5)",
          },
          "100%": {
            opacity: '1',
            transform: "translate(-50%,-40%) scale(1)",
          },
        },
      },
      animation:{
        fadeInUp: 'fadeInUp 0.5s ease-out forwards',
        spotlight: "spotlight 2s ease .75s 1 forwards",
      }

    },
  },
  plugins: [require("@tailwindcss/typography")],
} satisfies Config;
