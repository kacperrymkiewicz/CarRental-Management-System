import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap';
import './plugins/axios';

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'
import Toast from "vue-toastification";
import "vue-toastification/dist/index.css";

import BaseButton from '@/components/BaseButton.vue';

const toastConfig = {
  transition: "Vue-Toastification__slideBlurred",
  maxToasts: 3, 
  timeout: 2500,
  position: "bottom-right"
};

const app = createApp(App)

app.use(createPinia())
app.use(router)
app.use(Toast, toastConfig)
app.component('base-button', BaseButton)
app.mount('#app')
