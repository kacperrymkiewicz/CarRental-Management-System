import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap';
import 'axios';

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'

import BaseButton from '@/components/BaseButton.vue';

const app = createApp(App)

app.use(createPinia())
app.use(router)
app.component('base-button', BaseButton)
app.mount('#app')
