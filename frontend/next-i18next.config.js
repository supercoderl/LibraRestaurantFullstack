/** @type {import('next-i18next').UserConfig} */
module.exports = {
    i18n: {
        defaultLocale: 'vi',
        locales: ['vi', 'de', 'en', 'fr', 'hi', 'jp', 'ko', 'th', 'zh'],
        localeDetection: false,
    },
    fallbackLng: 'vi',
    ns: ['common'],
    reloadOnPrerender: process.env.NODE_ENV === 'development',
}