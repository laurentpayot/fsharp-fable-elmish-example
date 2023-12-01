import { defineConfig } from 'vite'

// https://vitejs.dev/config/
export default defineConfig({
    root: './output/src', // all other paths will be relative to this directory
    server: {
        port: 5000,
        strictPort: true,
    },
    publicDir: '../../public',
    build: {
        outDir: '../../dist',
        emptyOutDir: true,
        target: 'es2022', // ES2022 allowed for terser v5.16+ https://github.com/vitejs/vite/pull/12197
        minify: 'terser',
        terserOptions: {
            format: {
                comments: false
            },
            // compress: {
            //     pure_getters: true,
            //     keep_fargs: false,
            //     unsafe: true,
            //     unsafe_arrows: true,
            //     unsafe_comps: true,
            //     unsafe_Function: true,
            //     unsafe_math: true,
            //     unsafe_symbols: true,
            //     unsafe_methods: true,
            //     unsafe_proto: true,
            //     unsafe_undefined: true,
            //     passes: 3,
            // },
            // mangle: {
            //     properties: 'keep_quoted', // effective but dangerous?
            // },
            mangle: true
        },
    },
    plugins: [],
    resolve: {
        alias: {
            'react': 'preact/compat',
            'react-dom': 'preact/compat',
        },
    }
})
