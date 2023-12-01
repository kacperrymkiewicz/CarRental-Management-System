import { defineStore } from 'pinia'
import axios from 'axios';

export const useBookingStore = defineStore('booking', {
  state: () => ({
    searchResult: [],
  }),
  getters: {
    carsFound: (state) => state.searchResult.length > 0,
    carsAmount: (state) => state.searchResult.length,
  },
  actions: {
    async searchCars(pickupDate, returnDate) {
      const responseStatus = { success: null, message: null }

      await axios.get('/Cars/Availability', {
        pickupDate: pickupDate,
        returnDate: returnDate,
      })
      .then((response) => {
        this.searchResult = response.data.data;
        responseStatus.success = true;
      })
      .catch(() => {
        responseStatus.success = false;
        responseStatus.message = 'Server error. Unexpected status code.'
      });

      return responseStatus;
    },
  },
})