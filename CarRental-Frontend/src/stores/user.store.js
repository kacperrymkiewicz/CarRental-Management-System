import { defineStore } from 'pinia'
import { useAuthStore } from '@/stores/auth.store';
import { jwtDecode } from 'jwt-decode';
import axios from 'axios';

export const useUserStore = defineStore('user', {
  state: () => ({
    user: {
      id: null,
      firstname: '',
      lastname: '',
      email: '',
      authorization: '',
    },
    userData: []
  }),
  getters: {
    isCustomer: (state) => state.user.authorization == 'Customer',
    isEmployer: (state) => state.user.authorization == 'Employer',
    isAdministrator: (state) => state.user.authorization == 'Administrator',
  },
  actions: {
    decodeToken() {
      const authStore = useAuthStore();

      if(authStore.isAuthenticated) {
        const decodedToken = jwtDecode(authStore.authToken);
        this.user.id = decodedToken.nameid;
        this.user.firstname = decodedToken.firstName;
        this.user.lastname = decodedToken.lastName;
        this.user.email = decodedToken.email;
        this.user.authorization = decodedToken.role;
        this.fetchUser(this.user.id);
      }
    },
    async fetchUser(userId) {
      await axios.get(`/Users/${userId}`)
      .then((response) => {
        this.userData = response.data.data;
      });
    },
    async changePassword(currentPassword, newPassword) {
      const responseStatus = { success: null, message: null }

      await axios.post('/Auth/ChangePassword', {
        id: this.user.id,
        currentPassword: currentPassword,
        newPassword: newPassword,
      })
      .then((response) => {
        responseStatus.success = response.data.success;
        responseStatus.message = response.data.message;
      })
      .catch((error) => {
        responseStatus.success = false;
        responseStatus.message = error.response.data.message;
      })

      return responseStatus;
    }
  },
  persist: true,
})