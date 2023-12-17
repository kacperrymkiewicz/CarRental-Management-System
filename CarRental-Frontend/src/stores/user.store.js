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
    userData: [],
    userRentals: []
  }),
  getters: {
    isCustomer: (state) => state.user.authorization == 'Customer',
    isEmployer: (state) => state.user.authorization == 'Employer',
    isAdministrator: (state) => state.user.authorization == 'Administrator',
    hasRentals: (state) => state.userRentals.length > 0
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
    async fetchUser() {
      await axios.get(`/Users/${this.user.id}`)
      .then((response) => {
        this.userData = response.data.data;
      });
    },
    async fetchUserRentals() {
      await axios.get(`/Users/${this.user.id}/Rentals`)
      .then((response) => {
        this.userRentals = response.data.data;
      });
    },
    async cancelReservation(id) {
      const responseStatus = { success: null, message: null }

      await axios.patch(`/Rentals/${id}/Cancel`)
      .then((response) => {
        responseStatus.success = response.data.success;
        responseStatus.message = 'Rezerwacja została odwołana pomyślnie';
      })
      .catch((error) => {
        responseStatus.success = false;
        responseStatus.message = error.response.data.message;
      });

      return responseStatus;
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
    },
    async updateProfile(firstname, lastname, emailAddress, phoneNumber, zipCode, city, street, houseNumber) {
      const responseStatus = { success: null, message: null }

      await axios.put('/Users', {
        id: this.user.id,
        firstName: firstname,
        lastName: lastname,
        emailAddress: emailAddress,
        address: {
          zipCode: zipCode, 
          city: city,
          street: street,
          houseNumber: houseNumber,
          phoneNumber: phoneNumber
        }
      })
      .then((response) => {
        responseStatus.success = response.data.success;
        responseStatus.message = 'Zmiany zostały zapisane'
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