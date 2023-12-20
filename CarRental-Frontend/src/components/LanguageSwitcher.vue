<script setup>
import IconTranslate from '@/components/icons/IconTranslate.vue';
import { useI18n } from 'vue-i18n';
const { locale } = useI18n();

const languageLabels = {
  "en": "English",
  "pl": "Polski"
}

const switchLanguage = () => {
  locale.value = locale.value === 'pl' ? 'en' : 'pl';
  localStorage.setItem('lang', locale.value)
}
</script>

<template>
  <a @click.prevent="switchLanguage()" href="#">
    <li>
      <span>
        <IconTranslate/>
      </span>
      <template v-if="locale == 'pl'">
        {{ languageLabels.en }}
      </template>
      <template v-else>
        {{ languageLabels.pl }}
      </template>
    </li>
  </a>
</template>

<style lang="scss">
  a {
      text-decoration: none;
      font-weight: 500;
      color: $secondary;

    li {
      padding: 10px 0;
      border-radius: 10px;
      margin: 7px 0;

      &:hover {
        background-color: $button-light;
      }

      span {
        padding: 12px;
      }
    }
  }
</style>