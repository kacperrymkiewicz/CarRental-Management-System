<script setup>
import IconCalendar from '@/components/icons/IconCalendar.vue'
import IconVehicle from '@/components/icons/IconVehicle.vue'
import { ref } from 'vue';
const vehicleType = ref('Wszystkie');
const pickupDate = ref('');
const pickupTime = ref('');
const returnDate = ref('');
const returnTime = ref('');


const searchAvailableCars = () => {

}
</script>

<template>
  <div class="row">
    <div class="col-md-10 offset-md-1">
      <div class="car-reservation-panel">
        <h2>Book a car</h2>
        <form @submit.prevent="searchAvailableCars()">
          <div class="car-reservation-top-panel">
            <div class="form-group">
              <label for="vehicleType"><IconVehicle class="car-reservation-icon"/> Select your car type</label>
              <select class="form-select" id="vehicleType" v-model="vehicleType">
                <option selected>Wszystkie</option>
                <option>Sedan</option>
                <option>SUV</option>
              </select>
            </div>
          </div>
          <div class="car-reservation-data-select">
            <div class="row">
              <div class="col-md-5">
                <div class="form-group">
                  <label for="pickupDate"><IconCalendar class="car-reservation-icon"/> Pick-up</label>
                  <div class="rental-pickup-date">
                    <div class="row g-3">
                      <div class="col-md-7">
                        <input type="date" class="form-control" id="pickupDate" v-model="pickupDate">
                      </div>
                      <div class="col-md-5">
                        <input type="time" class="form-control" id="pickupTime" v-model="pickupTime">
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="col-md-5">
                <div class="form-group">
                  <label for="returnDate"><IconCalendar class="car-reservation-icon"/> Drop-off</label>
                  <div class="rental-return-date">
                    <div class="row g-3">
                      <div class="col-md-7">
                        <input type="date" class="form-control" id="returnDate" v-model="returnDate">
                      </div>
                      <div class="col-md-5">
                        <input type="time" class="form-control" id="returnTime" v-model="returnTime">
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="col-md-2">
                <base-button class="car-reservation-search-button" type="dark">Search</base-button>
              </div>
            </div>

          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
  .car-reservation-panel {
    background-color: $dark-background;
    border-radius: 0.5rem;
    padding: 40px 45px 50px 55px;
    text-align: left;
    color: white;
    box-shadow: 0 10px 15px 0px rgb(0 0 0 / 30%);

    & .car-reservation-top-panel {
      margin-bottom: 1.25rem;
    }

    h2 {
      font-weight: 600;
      margin-bottom: 25px;
    }

    .car-reservation-search-button {
      background-color: $dark-text;
      border-color: $dark-text;
      margin: 0;
    }

    .car-reservation-icon {
      color: $dark-text;
      margin-right: 5px;
    }

    input.form-control, select.form-select {
      font-size: .875rem;
      color: #ababab;
      border: 1px solid $dark-text;
      border-radius: 3px;
      font-weight: 400;
      padding-top: 1.0625rem;
      padding-bottom: 1.0625rem;
      padding-right: 1.875rem;
      padding-left: 1.25rem;
      margin-top: 3px;
    }

    label {
      display: flex;
      align-items: center;
    }
  }
</style>