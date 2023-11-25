import { defineStore } from 'pinia'
import { useAuthStore } from '@/stores/auth.store';
import { jwtDecode } from 'jwt-decode';

export const useUserStore = defineStore('user', {
  state: () => ({
    userId: null,
    firstname: '',
    lastname: '',
    email: '',
    authorization: '',
  }),
  getters: {
    isCustomer: (state) => state.authorization == 'Customer',
    isEmployer: (state) => state.authorization == 'Employer',
    isAdministrator: (state) => state.authorization == 'Administrator',
  },
  actions: {
    decodeToken() {
      const authStore = useAuthStore();

      if(authStore.isAuthenticated) {
        const decodedToken = jwtDecode(authStore.authToken);
        this.userId = decodedToken.nameid;
        this.firstname = decodedToken.firstName;
        this.lastname = decodedToken.lastName;
        this.email = decodedToken.email;
        this.authorization = decodedToken.role;
        console.log(decodedToken);
      }
    },
  }
})