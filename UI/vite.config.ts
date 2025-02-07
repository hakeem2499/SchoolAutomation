import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import tailwindcss from '@tailwindcss/vite';
import Icons from 'unplugin-icons/vite';

// https://vite.dev/config/
export default defineConfig({
  plugins: [react(), tailwindcss(), Icons({
    compiler: 'jsx',
    autoInstall: true,
    jsx: 'react',


  }),],
})
