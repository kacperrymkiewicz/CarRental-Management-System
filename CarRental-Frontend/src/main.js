import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap'
import './plugins/axios'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'
import Toast from "vue-toastification"
import "vue-toastification/dist/index.css"
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate'

import { capitalizeMixin } from '@/mixins/capitalizeMixin'

import Breadcrumbs from '@/components/Breadcrumbs.vue'
import BaseButton from '@/components/BaseButton.vue'
import BaseCard from '@/components/BaseCard.vue'

const toastConfig = {
  transition: "Vue-Toastification__slideBlurred",
  maxToasts: 3, 
  timeout: 2500,
  position: "bottom-right"
}

const app = createApp(App)
const pinia = createPinia()

pinia.use(piniaPluginPersistedstate)

app.use(pinia)
app.use(router)
app.use(Toast, toastConfig)
app.mixin(capitalizeMixin)
app.component('breadcrumbs', Breadcrumbs)
app.component('base-button', BaseButton)
app.component('base-card', BaseCard)
app.mount('#app')
