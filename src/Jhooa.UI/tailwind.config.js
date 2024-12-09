/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./**/*.{razor,html,cshtml}", "./node_modules/flowbite/**/*.js"],
    theme: {
        extend: {
            colors: {
                'jhooa-teal': {
                    100: '#26969F',
                    200: '#005359',
                },
                'jhooa-peach': {
                    100: '#E38064'
                },
            },
        }
    },
    plugins: [
        require('flowbite/plugin')
    ],
}

