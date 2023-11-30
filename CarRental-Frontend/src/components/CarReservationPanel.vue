<script setup>
import IconCalendar from '@/components/icons/IconCalendar.vue'
import IconVehicle from '@/components/icons/IconVehicle.vue'
import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css'
import { useRouter } from 'vue-router';
import { ref } from 'vue';

const vehicleType = ref('Wszystkie');
let pickupDate = ref(new Date());
let pickupTime = ref(generateRentalInitalTime(new Date()));
let returnDate = ref(new Date());
let returnTime = ref(generateRentalInitalTime(new Date()));

const router = useRouter();

const format = 'dd/MM/yyyy';

const updateDate = () => {
  console.log(returnDate);
  console.log(pickupDate);
  if(returnDate <= pickupDate) returnDate.value = new Date(pickupDate.value);
}

const searchAvailableCars = () => {
  router.push({ 
    name: 'booking', 
    query: { 
      pickupDate: pickupDate.value.getFullYear() + '-' + (pickupDate.value.getMonth() + 1) + '-' + pickupDate.value.getDate(),
      pickupTime: pickupTime.value.hours + ":" + pickupTime.value.minutes,
      returnDate: returnDate.value.getFullYear() + '-' + (returnDate.value.getMonth() + 1) + '-' + returnDate.value.getDate(),
      returnTime: returnTime.value.hours + ":" + returnTime.value.minutes,
    }
  })
}

function generateRentalInitalTime(currentDate) {
  currentDate.setHours(currentDate.getHours() + 1);
  let currentMinutes = currentDate.getMinutes();
  let roundedMinutes = Math.ceil(currentMinutes / 15) * 15;
  currentDate.setMinutes(roundedMinutes);
  return {
    hours: currentDate.getHours(),
    minutes: currentDate.getMinutes()
  }
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
              <div class="col-lg-6 col-xl-5">
                <div class="form-group">
                  <label for="pickupDate"><IconCalendar class="car-reservation-icon"/> Pick-up</label>
                  <div class="rental-pickup-date mb-3 mb-lg-0">
                    <div class="row g-3">
                      <div class="col-lg-6 col-xl-7">
                        <VueDatePicker v-model="pickupDate" @update:model-value="updateDate()" input-class-name="datepicker-input" locale="pl" :disabled-week-days="[6, 0]" :enable-time-picker="false" :format="format" select-text="Zatwierdź" cancel-text="Anuluj" hide-input-icon auto-apply :min-date="new Date()" :clearable="false">{{ pickupDate }}</VueDatePicker>
                      </div>
                      <div class="col-lg-6 col-xl-5">
                        <VueDatePicker v-model="pickupTime" time-picker input-class-name="datepicker-input" locale="pl" minutes-increment="15" minutes-grid-increment="15" :min-time="{ hours: 8, minutes: 0 }" :max-time="{ hours: 22, minutes: 0 }" select-text="Zatwierdź" cancel-text="Anuluj" hide-input-icon auto-apply :clearable="false"></VueDatePicker>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="col-lg-6 col-xl-5">
                <div class="form-group">
                  <label for="returnDate"><IconCalendar class="car-reservation-icon"/> Drop-off</label>
                  <div class="rental-return-date">
                    <div class="row g-3">
                      <div class="col-lg-6 col-xl-7">
                        <VueDatePicker v-model="returnDate" input-class-name="datepicker-input" locale="pl" :disabled-week-days="[6, 0]" :enable-time-picker="false" :format="format" select-text="Zatwierdź" cancel-text="Anuluj" hide-input-icon auto-apply :min-date="pickupDate" :clearable="false">{{ pickupDate }}</VueDatePicker>
                      </div>
                      <div class="col-lg-6 col-xl-5">
                        <VueDatePicker v-model="returnTime" time-picker input-class-name="datepicker-input" locale="pl" minutes-increment="15" minutes-grid-increment="15" :min-time="{ hours: 8, minutes: 0 }" :max-time="{ hours: 22, minutes: 0 }" select-text="Zatwierdź" cancel-text="Anuluj" hide-input-icon auto-apply :clearable="false"></VueDatePicker>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="col-lg-12 col-xl-2 mt-4 mt-xl-0">
                <div class="rental-search-button">
                  <base-button @click.prevent="searchAvailableCars()" class="car-reservation-search-button" type="dark">Search</base-button>
                </div>
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
      width: 100%;
      height: 57px;
      margin: 0;
    }

    .car-reservation-search-button:hover {
      background-color: $dark-text-hover;
      border-color: $dark-text-hover;
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

    .rental-search-button {
      display: flex;
      align-items: flex-end;
      height: 100%;
    }
  }
</style>

<style lang="scss">
    .dp__theme_light {
    --dp-background-color: #f8f9fb;
    --dp-text-color: #2C3E50;
    --dp-hover-color: #f3f3f3;
    --dp-hover-text-color: #2C3E50;
    --dp-hover-icon-color: #959595;
    --dp-primary-color: #3f5ff2;
    --dp-primary-text-color: #f8f5f5;
    --dp-secondary-color: #c0c4cc;
    --dp-border-color: #f8f9fb;
    --dp-menu-border-color: #ddd;
    --dp-border-color-hover: #f8f9fb;
    --dp-disabled-color: #f6f6f6;
    --dp-scroll-bar-background: #f3f3f3;
    --dp-scroll-bar-color: #959595;
    --dp-success-color: #76d275;
    --dp-success-color-disabled: #a3d9b1;
    --dp-icon-color: #959595;
    --dp-danger-color: #ff6f60;
    --dp-highlight-color: rgba(25, 118, 210, 0.1);
    --dp-font-family: 'Inter', sans-serif;
    }

    .datepicker-input {
      background-color: white;
      font-size: .875rem;
      color: #ababab;//#6a6a6a
      border: 1px solid $dark-text;
      border-radius: 3px;
      font-weight: 400;
      padding-top: 1.0625rem;
      padding-bottom: 1.0625rem;
      padding-right: 1.875rem;
      padding-left: 1.25rem;
      margin-top: 3px;
      line-height: unset;
    }

    .datepicker-input:hover {
      border: 1px solid $dark-text;
    }
</style>