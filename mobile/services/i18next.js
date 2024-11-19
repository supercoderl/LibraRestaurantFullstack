import i18next from 'i18next'
import { initReactI18next } from 'react-i18next'

import de from '../locales/de.json'
import en from '../locales/en.json'
import fr from '../locales/fr.json'
import hi from '../locales/hi.json'
import jp from '../locales/jp.json'
import ko from '../locales/ko.json'
import sv from '../locales/sv.json'
import th from '../locales/th.json'
import vi from '../locales/vi.json'
import zh from '../locales/zh.json'

export const languageResources = {
  en: { translation: en },
  sv: { translation: sv },
  vi: { translation: vi },
  de: { translation: de },
  fr: { translation: fr },
  hi: { translation: hi },
  ja: { translation: jp },
  ko: { translation: ko },
  th: { translation: th },
  zh: { translation: zh },
}

i18next.use(initReactI18next).init({
  compatibilityJSON: 'v3',
  lng: 'vi',
  fallbackLng: 'vi',
  resources: languageResources,
})

export default i18next
