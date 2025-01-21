/** @type {import('tailwindcss').Config} */

const defaultTheme = require("tailwindcss/defaultTheme");

module.exports = {
    content: ["./**/*.{razor,html,cshtml}", "./node_modules/flowbite/**/*.js"],
    theme: {
        extend: {
            height: {
                'banner': '700px',
            },
            fontFamily: {
                sans: ["AmsiPro", ...defaultTheme.fontFamily.sans],
                amsiProLight: ["AmsiProLight", "serif"],
                amsiSemiBold: ["AmsiSemiBold", "serif"],
                butler: ["Butler", "serif"],
            },
            colors: {
                'jhooa-teal': {
                    100: '#26969F',
                    200: '#005359',
                    500: '#3DA2AA',
                    900: '#002C2F'
                },
                'jhooa-brick': {
                    100: '#4D2E2E',
                },
                'jhooa-peach': {
                    100: '#E38064'
                },
                'jhooa-gray': {
                    100: '#4B4B4B'
                },
                'jhooa-admin': {
                    100: '#002C2F'
                },
                'jhooa-table': {
                    'even': '#FFFFFF1A',
                    'odd': '#002C2F'
                }
            },
        }
    },
    plugins: [
        require('flowbite/plugin')
    ],
}

