import { defineStore } from 'pinia'
import { useAuthStore } from '@/stores/auth.store';
import { jwtDecode } from 'jwt-decode';

export const useUserStore = defineStore('user', {
  state: () => ({
    user: {
      id: null,
      firstname: '',
      lastname: '',
      email: '',
      authorization: '',
    },
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
      }
    },
  },
  persist: true,
})