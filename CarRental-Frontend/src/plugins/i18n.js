import { createI18n } from 'vue-i18n';
import PL from '@/locales/pl.json';
import EN from '@/locales/en.json';

const i18n = createI18n({
  legacy: false,
  locale: 'pl',
  fallbackLocale: 'en',
  messages: {
    en: EN,
    pl: PL
  }
});

export default i18n;