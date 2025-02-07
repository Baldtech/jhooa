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
                amsiProBlack: ["AmsiProBlack", "serif"],
                amsiSemiBold: ["AmsiSemiBold", "serif"],
                amsiBold: ["AmsiBold", "serif"],
                butler: ["Butler", "serif"],
                monetaBold: ["MonetaBold", "serif"],
                monetaRegular: ["MonetaRegular", "serif"],
            },
            colors: {
                'jhooa-teal': {
                    100: '#26969F',
                    200: '#005359',
                    500: '#3DA2AA',
                    600: '#001B1D',
                    900: '#002C2F'
                },
                'jhooa-brick': {
                    100: '#4D2E2E',
                    500: '#9A432A'
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
                },
                'jhooa-beige': {
                    100: '#D1A08D'
                }
            },
        }
    },
    plugins: [
        require('flowbite/plugin')
    ],
}

