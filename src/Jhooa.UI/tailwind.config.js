/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./**/*.{razor,html,cshtml}", "./node_modules/flowbite/**/*.js"],
    theme: {
        extend: {
            height: {
                'banner': '700px',
            },
            fontFamily: {
                butler: ["Butler", "serif"],
                text: ["AmsiPro", "sans-serif"],
                winter: ["WinterMinnie", "sans-serif"],
            },
            colors: {
                'jhooa-teal': {
                    100: '#26969F',
                    200: '#005359',
                },
                'jhooa-brick': {
                    100: '#4D2E2E',
                },
                'jhooa-peach': {
                    100: '#E38064'
                },
                'jhooa-gray': {
                    100: '#4B4B4B'
                }
            },
        }
    },
    plugins: [
        require('flowbite/plugin')
    ],
}

