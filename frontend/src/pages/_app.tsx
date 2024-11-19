import React from 'react';
import App, { AppContext, AppProps } from 'next/app';
import { GlobalStyles } from 'twin.macro';
import ThemeProvider from '../theme/theme-provider';
import ThemeStyles from '../theme/theme';
import 'tailwindcss/tailwind.css';
import '../global.css';
import store from 'src/redux/store';
import { Provider } from 'react-redux';
import 'react-toastify/dist/ReactToastify.css';
import '@fortawesome/fontawesome-svg-core/styles.css'

import { PersistGate } from 'redux-persist/integration/react';
import { persistStore } from 'redux-persist';
import { ToastContainer } from 'react-toastify';
import { ConfigProvider } from 'antd';
import 'dayjs/locale/vi';
import locale from 'antd/locale/vi_VN';
import localeData from 'dayjs/plugin/localeData';
import dayjs from 'dayjs';
import advancedFormat from 'dayjs/plugin/advancedFormat'
import customParseFormat from 'dayjs/plugin/customParseFormat'
import weekday from 'dayjs/plugin/weekday'
import weekOfYear from 'dayjs/plugin/weekOfYear'
import weekYear from 'dayjs/plugin/weekYear'

import 'swiper/css';
import 'swiper/css/navigation';
import 'swiper/css/pagination';
import 'swiper/css/scrollbar';
import { appWithTranslation } from 'next-i18next';
import DarkThemeToggler from '@/components/dark-theme-toggler';
import { SignalRProvider } from '@/context/signalRProvider';
import { Settings } from '@/components/settings';
import { GoogleOAuthProvider } from '@react-oauth/google';

let persistor = persistStore(store);

const MyApp = ({ Component, pageProps }: AppProps) => {
  dayjs.extend(customParseFormat);
  dayjs.extend(advancedFormat);
  dayjs.extend(weekday);
  dayjs.extend(weekOfYear);
  dayjs.extend(weekYear);
  dayjs.extend(localeData);
  dayjs.locale("vi_VN");

  return (
    <ConfigProvider locale={locale}>
      <GoogleOAuthProvider clientId={process.env.NEXT_PUBLIC_GOOGLE_CLIENT_ID || ""}>
        {/* <LanguageSelector /> */}
        <Provider store={store} stabilityCheck="never">
          <SignalRProvider>
            <GlobalStyles />
            <ThemeStyles />
            <ToastContainer position="top-center" />
            <ThemeProvider>
              <PersistGate loading={null} persistor={persistor}>
                <Component {...pageProps} />
                <Settings />
              </PersistGate>
              <DarkThemeToggler />
            </ThemeProvider>
          </SignalRProvider>
        </Provider>
      </GoogleOAuthProvider>
    </ConfigProvider>
  );
}

MyApp.getInitialProps = async ({ Component, ctx }: AppContext) => {
  let pageProps = {}

  if (Component.getInitialProps) {
    pageProps = await Component.getInitialProps(ctx)
  }

  return { pageProps }
}

export default appWithTranslation(MyApp)