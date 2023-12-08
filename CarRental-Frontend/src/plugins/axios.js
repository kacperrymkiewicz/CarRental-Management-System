import axios from 'axios'

axios.defaults.baseURL = 'https://localhost:7233/api';
axios.defaults.headers.common['Authorization'] = localStorage.getItem('auth') ? `bearer ${JSON.parse(localStorage.getItem('auth')).authToken}` : null;