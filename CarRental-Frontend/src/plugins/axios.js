import axios from 'axios'

axios.defaults.baseURL = 'https://localhost:7233/api';
axios.defaults.headers.common['Authorization'] = `bearer ${JSON.parse(localStorage.getItem('auth')).authToken}`;