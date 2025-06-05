import path from 'node:path'
import tailwindcss from '@tailwindcss/vite'
import vue from '@vitejs/plugin-vue'

import vueDevTools from 'vite-plugin-vue-devtools'

import { defineConfig } from 'vite'


// https://vite.dev/config/
export default defineConfig({
  base: '/[RoutePrefix]',
  plugins: [
    vue(),
    vueDevTools(),
    tailwindcss(),
  ],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src'),
    },
  },
  build: {
    rollupOptions: {
      output: {
        entryFileNames: '[name].js',
        chunkFileNames: '[name].js',
        assetFileNames: '[name].[ext]'
      }
    }
  },
  server: {
    proxy: {
      '/[RoutePrefix]/dashboard.json': {
        target: 'http://localhost:5253/',
        changeOrigin: true,
        rewrite: (path) => {
          console.log(path) 
          var result = path.replace("[RoutePrefix]", 'kafka-dashboard')
          console.log(result);
          return result;
        }
      },
    },
  },
})
