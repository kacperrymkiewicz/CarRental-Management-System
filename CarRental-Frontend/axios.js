import axios from 'axios'

axios.defaults.baseURL = 'https://localhost:7238/api';
axios.defaults.headers.common['Authorization'] = 'bearer ' + localStorage.getItem('token');