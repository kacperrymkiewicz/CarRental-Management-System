import { defineStore } from 'pinia'
import axios from 'axios';

export const useRentalStore = defineStore('rental', {
  state: () => ({
    rentals: [],
    customers: [],
    cars: []
  }),
  getters: {
    foundRentals: (state) => state.rentals.length > 0,
    foundCustomers: (state) => state.customers.length > 0,
    foundCars: (state) => state.cars.length > 0
  },
  actions: {
    async fetchRentals() {
      await axios.get(`/Rentals`)
      .then((response) => {
        this.rentals = response.data.data;
      });
    },
    async fetchCustomers() {
      await axios.get(`/Users`)
      .then((response) => {
        this.customers = response.data.data;
      });
    },
    async fetchCars() {
      await axios.get(`/Cars`)
      .then((response) => {
        this.cars = response.data.data;
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
    async confirmReservation(id) {
      const responseStatus = { success: null, message: null }

      await axios.patch(`/Rentals/${id}/Confirm`)
      .then((response) => {
        responseStatus.success = response.data.success;
        responseStatus.message = 'Rezerwacja została potwierdzona';
      })
      .catch((error) => {
        responseStatus.success = false;
        responseStatus.message = error.response.data.message;
      });

      return responseStatus;
    },
    async finishReservation(id) {
      const responseStatus = { success: null, message: null }

      await axios.patch(`/Rentals/${id}/Finish`)
      .then((response) => {
        responseStatus.success = response.data.success;
        responseStatus.message = 'Rezerwacja została zakończona';
      })
      .catch((error) => {
        responseStatus.success = false;
        responseStatus.message = error.response.data.message;
      });

      return responseStatus;
    },
  },
})