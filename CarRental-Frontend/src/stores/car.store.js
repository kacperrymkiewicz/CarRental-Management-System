import { defineStore } from 'pinia'
import axios from 'axios';

export const useCarStore = defineStore('car', {
  state: () => ({
    cars: [],
    responseStatus: {
      loading: false,
      success: false,
      error: null,
      statusCode: null
    },
  }),
  actions: {
    async fetchCars() {
      this.responseStatus.loading = true;
      this.responseStatus.success = false;
      this.responseStatus.error = null;
      this.responseStatus.statusCode = null;

      await axios.get('/Cars')
      .then((response) => {
        this.cars = response.data.data;
        this.responseStatus.success = true;
      })
      .catch((error) => {
        this.responseStatus.error = true;
        this.responseStatus.statusCode = error.response ? error.response.status : null;
      })
      .finally(() => {
        this.responseStatus.loading = false;
      });
    }
  }
})