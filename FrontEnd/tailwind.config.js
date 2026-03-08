/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{vue,js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      fontFamily: {
        display: ['Cormorant Garamond', 'Georgia', 'serif'],
        body: ['Source Sans 3', 'system-ui', 'sans-serif'],
      },
      colors: {
        parchment: {
          50: '#fefdf9',
          100: '#fdf9ef',
          200: '#faf3e0',
          300: '#f5e6c8',
          400: '#edd9b0',
        },
        ink: {
          800: '#2c2a26',
          900: '#1a1917',
          950: '#0f0e0d',
        },
        accent: {
          amber: '#d4a574',
          gold: '#c9a227',
          warm: '#e8d5b7',
        },
      },
      boxShadow: {
        'book': '0 4px 6px -1px rgba(44, 42, 38, 0.08), 0 2px 4px -2px rgba(44, 42, 38, 0.06)',
        'book-hover': '0 10px 15px -3px rgba(44, 42, 38, 0.12), 0 4px 6px -4px rgba(44, 42, 38, 0.08)',
      },
    },
  },
  plugins: [],
}
