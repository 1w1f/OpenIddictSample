import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import fs from "fs";
import path from "path";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    port: 5555,
    https: {
      cert: fs.readFileSync(path.join(__dirname, "keys/cert.crt")),
      key: fs.readFileSync(path.join(__dirname, "keys/cert.key")),
    },
  },
});
