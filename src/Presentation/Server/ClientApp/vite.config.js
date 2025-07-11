import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import { resolve } from 'path'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  build: {
    outDir: '../wwwroot/dist',
    emptyOutDir: true, 
    rollupOptions: {

      input: resolve(__dirname, 'src/main.js'),
      output: {

        entryFileNames: `assets/app.js`,
        chunkFileNames: `assets/app-chunk.js`,
        assetFileNames: `assets/[name].[ext]`
      }
    }
  }
})