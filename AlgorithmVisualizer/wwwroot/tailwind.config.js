/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "../Components/**/*.{razor,html,cshtml}",
    "../Pages/**/*.{razor,html,cshtml}",
    "../Layout/**/*.{razor,html,cshtml}",
    "../App.razor",
    "../wwwroot/**/*.{html,js}"
  ],
  theme: {
    extend: {},
  },
  plugins: [
    require('@tailwindcss/forms'),
  ],
} 