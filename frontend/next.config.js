/** @type {import('next').NextConfig} */

const nextI18nextConfig = require('./next-i18next.config');

module.exports = {
  i18n: nextI18nextConfig.i18n,
  reactStrictMode: false,
  webpack(config) {
    config.module.rules.push({
      test: /\.svg$/,
      use: ['@svgr/webpack'],
    });
    
    return config;
  },
  images: {
    domains: [
      'image.flaticon.com',
      'images.pexels.com',
      'i.ibb.co',
      'modinatheme.com',
      'libra-novel.vercel.app',
      'res.cloudinary.com',
      'static.vecteezy.com',
      'www.pngkey.com',
      'lirp.cdn-website.com',
      'png.pngtree.com',
      'www.pngkey.com',
      'r2.erweima.ai',
      'pngimg.com',
      'encrypted-tbn0.gstatic.com',
      'www.freeiconspng.com',
      'www.pngplay.com',
      'rakuen.com.vn'],
  },
};
