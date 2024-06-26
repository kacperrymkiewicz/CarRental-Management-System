import { defineStore } from 'pinia'
import axios from 'axios';

export const useBookingStore = defineStore('booking', {
  state: () => ({
    vehicleType: null,
    pickupDate: null,
    pickupTime: null,
    returnDate: null,
    returnTime: null,
    rentalPeriod: 0,
    searchResult: [],
    modal: {
      isActive: false,
      data: []
    },
    responseStatus: {
      loading: false,
      success: false,
      error: null,
      statusCode: null,
      message: null
    },
  }),
  getters: {
    carsFound: (state) => state.searchResult.length > 0,
    carsAmount: (state) => state.searchResult.length,
  },
  actions: {
    async searchCars(pickupDate, returnDate) {
      this.responseStatus.loading = true;
      this.responseStatus.success = false;
      this.responseStatus.error = null;
      this.responseStatus.statusCode = null;
      this.responseStatus.message = null;

      await axios.get('/Cars/Availability', {
        params: {
          pickupDate: pickupDate,
          returnDate: returnDate,
        }
      })
      .then((response) => {
        this.searchResult = response.data.data;
        this.responseStatus.success = response.data.success;
        if(!response.data.success) {
          this.responseStatus.error = true;
          this.responseStatus.message = response.data.message;
        }
        else {
          this.calculateRentalPeriod();
        }
      })
      .catch((error) => {
        this.responseStatus.error = true;
        this.responseStatus.statusCode = error.response ? error.response.status : null;
      })
      .finally(() => {
        this.responseStatus.loading = false;
      });
    },
    openBookingModal(bookingData) {
      this.modal.isActive = true;
      this.modal.data = bookingData;
    },
    async addRental(rentalData) {
      const responseStatus = { success: null, message: null }

      await axios.post('/Rentals', {
        pickupDate: rentalData.pickupDate + 'T' + rentalData.pickupTime,
        returnDate: rentalData.returnDate + 'T' + rentalData.returnTime,
        carId: rentalData.carId,
        customerId: rentalData.customerId
      })
      .then((response) => {
        if(response.data.success) {
          responseStatus.message = 'Samochód został zarezerwowany';
        }
        else {
          responseStatus.message = response.data.message;
        }
        responseStatus.success = response.data.success;
      })
      .catch((error) => {
        responseStatus.success = false;
        responseStatus.message = 'Nie udało się dokonać rezerwacji';
      })

      return responseStatus;
    },
    buildQueryParams() {
      return {
        vehicleType: this.vehicleType,
        pickupDate: this.pickupDate.getFullYear() + '-' + (this.pickupDate.getMonth() + 1).toString().padStart(2, '0') + '-' + this.pickupDate.getDate().toString().padStart(2, '0'),
        pickupTime: this.pickupTime.hours.toString().padStart(2, '0') + ":" + this.pickupTime.minutes.toString().padStart(2, '0'),
        returnDate: this.returnDate.getFullYear() + '-' + (this.returnDate.getMonth() + 1).toString().padStart(2, '0') + '-' + this.returnDate.getDate().toString().padStart(2, '0'),
        returnTime: this.returnTime.hours.toString().padStart(2, '0') + ":" + this.returnTime.minutes.toString().padStart(2, '0'),
      }
    },
    syncQueryParams(queryParams) {
      this.vehicleType = queryParams.vehicleType || 'Wszystkie';
      this.pickupDate = queryParams.pickupDate != null ? new Date(queryParams.pickupDate) : new Date();
      this.pickupTime = queryParams.pickupTime != null ? this.convertDatetimeToObject(this.parseTimeFromQuery(queryParams.pickupTime)) : this.generateRentalInitalTime(new Date());
      this.returnDate = queryParams.returnDate != null ? new Date(queryParams.returnDate) : new Date(this.pickupDate.getTime() + 24 * 60 * 60 * 1000);
      this.returnTime = queryParams.returnTime != null ? this.convertDatetimeToObject(this.parseTimeFromQuery(queryParams.returnTime)) : this.generateRentalInitalTime(new Date());
    },
    convertDatetimeToObject(datetime) {
      return {
        hours: datetime.getHours(),
        minutes: datetime.getMinutes()
      }
    },
    parseTimeFromQuery(timeString) {
      const [hoursStr, minutesStr] = timeString.split(':');
      const datetime = new Date();
      datetime.setHours(parseInt(hoursStr, 10));
      datetime.setMinutes(parseInt(minutesStr, 10));
      return datetime;
    },
    generateRentalInitalTime(currentDate) {
      currentDate.setHours(currentDate.getHours() + 1);
      let currentMinutes = currentDate.getMinutes();
      let roundedMinutes = Math.ceil(currentMinutes / 15) * 15;
      currentDate.setMinutes(roundedMinutes);
      return {
        hours: currentDate.getHours(),
        minutes: currentDate.getMinutes()
      }
    },
    updateDate() {
      if(this.returnDate <= this.pickupDate) {
        let newReturnDate = new Date(this.pickupDate);
        newReturnDate.setDate(newReturnDate.getDate() + 1);
        this.returnDate = newReturnDate;
      } 
    },
    calculateRentalPeriod() {
      let timeDiff = this.returnDate - this.pickupDate;
      this.rentalPeriod = Math.round((timeDiff / (1000 * 3600 * 24)));
    }
  }
})