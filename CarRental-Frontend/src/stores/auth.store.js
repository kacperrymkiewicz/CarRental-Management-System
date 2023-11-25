import { defineStore } from 'pinia'
import axios from 'axios';

export const useAuthStore = defineStore('auth', {
  state: () => ({
    authToken: localStorage.getItem('authToken') || null,
  }),
  getters: {
    isAuthenticated: (state) => !!state.authToken,
  },
  actions: {
    setToken(token) {
      this.authToken = token;
      localStorage.setItem('authToken', token);
    },
    removeToken() {
      this.authToken = null;
      localStorage.removeItem('authToken');
    },
    async authenticateUser(emailAddress, password) {
      const responseStatus = { success: null, message: null }

      await axios.post('/Auth/Login', {
        emailAddress: emailAddress,
        password: password,
      })
      .then((response) => {
        console.log(response.data.data);
        this.setToken(response.data.data);
        responseStatus.success = true;
        responseStatus.message = 'Authentication successful'
      })
      .catch((error) => {
        responseStatus.success = false;
        if(error.code == 'ERR_NETWORK') {
          responseStatus.message = 'Authentication failed. Unexpected status code.'
        }
        else {
          responseStatus.message = 'Authentication failed. Wrong password.'
        }
      });

      return responseStatus;
    }
  }
})