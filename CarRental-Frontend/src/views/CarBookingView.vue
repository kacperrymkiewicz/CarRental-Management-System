<script setup>
import CarBookingItem from '@/components/CarBookingItem.vue'
import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css'
import { ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useBookingStore } from '@/stores/booking.store';
const bookingStore = useBookingStore();

const route = useRoute();
const router = useRouter();
const format = 'dd/MM/yyyy';

const isLoading = ref(true);
const isError = ref(false);

bookingStore.syncQueryParams(route.query);

const fetchCars = async () => {
  const apiResponse = await bookingStore.searchCars(route.query.pickupDate + ' ' + route.query.pickupTime, route.query.returnDate + ' ' + route.query.returnTime);
  if(apiResponse.success) {
    isLoading.value = false;
    return;
  }
  isError.value = true;
}

const updateParams = () => {
  router.replace({ 
    name: 'booking', 
    query: bookingStore.buildQueryParams()
  })
  setTimeout(() => {
    fetchCars();
  }, 0);
}

const expectedParams = ['pickupDate', 'pickupTime', 'returnDate', 'returnTime', 'vehicleType'];
const paramsValidated = expectedParams.every(key => key in route.query);

if(paramsValidated) {
  fetchCars();
}
</script>

<template>
  <section id="car-booking">
    <div class="container">
      <div class="row">
        <div class="col-lg-3">
          <div class="car-booking-sidebar-options">
            <div class="car-booking-sidebar-reservation-date">
              <h5>Zmień datę rezerwacji</h5>
              <h6>Pick-up</h6>
              <VueDatePicker v-model="bookingStore.pickupDate" @update:model-value="bookingStore.updateDate()" input-class-name="sidebar-datepicker" locale="pl" :disabled-week-days="[6, 0]" :enable-time-picker="false" :format="format" select-text="Zatwierdź" cancel-text="Anuluj" hide-input-icon auto-apply :min-date="new Date()" :clearable="false">{{ pickupDate }}</VueDatePicker>
              <VueDatePicker v-model="bookingStore.pickupTime" time-picker input-class-name="sidebar-datepicker" locale="pl" minutes-increment="15" minutes-grid-increment="15" select-text="Zatwierdź" cancel-text="Anuluj" hide-input-icon auto-apply :clearable="false"></VueDatePicker>
              <h6>Drop-off</h6>
              <VueDatePicker v-model="bookingStore.returnDate" input-class-name="sidebar-datepicker" locale="pl" :disabled-week-days="[6, 0]" :enable-time-picker="false" :format="format" select-text="Zatwierdź" cancel-text="Anuluj" hide-input-icon auto-apply :min-date="bookingStore.pickupDate" :clearable="false">{{ pickupDate }}</VueDatePicker>
              <VueDatePicker v-model="bookingStore.returnTime" time-picker input-class-name="sidebar-datepicker" locale="pl" minutes-increment="15" minutes-grid-increment="15" select-text="Zatwierdź" cancel-text="Anuluj" hide-input-icon auto-apply :clearable="false"></VueDatePicker>
              <base-button @click.prevent="updateParams()" class="car-booking-button" type="dark">Wyszukaj samochód</base-button>
            </div>
          </div>
        </div>
        <div class="col-lg-9">
          <div v-if="!isLoading" class="car-booking-available-cars">
            <div v-if="!bookingStore.carsFound" class="car-booking-notfound">
              <h1>Nie znaleziono dostępnych pojazdów w terminie</h1>
            </div>
            <template v-else>
              <h5>Dostępne samochody: <span>{{ bookingStore.carsAmount }}</span></h5>
              <template v-for="car in bookingStore.searchResult">
                <CarBookingItem v-if="car.status" :car="car"/>
              </template>
            </template>
          </div>
          <div v-else-if="isLoading && !paramsValidated">
            <h3>Wybierz termin</h3>
          </div>
          <div v-else-if="isError">
            <h3>Wystąpił błąd</h3>
          </div>
          <div v-else>
            <h3>Trwa wyszukiwanie dostępnych pojazdów...</h3>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<style scoped lang="scss">
  section#car-booking {
    margin-top: 60px;

    & .car-booking-sidebar-options {
      margin-bottom: 35px;
      & .car-booking-sidebar-reservation-date {
        border: 1px solid #D1D9E2;
        border-radius: 15px;
        padding: 20px;

        h5 {
          font-size: 1.15rem;
          font-weight: 600;
          text-align: left;
        }

        h6 {
          margin-top: 15px;
          margin-bottom: 0;
        }

        & .car-booking-button {
          width: 100%;
          margin: 10px 0;
        }
      }
    }

    & .car-booking-available-cars {
      h5 {
        text-align: left;

        span {
          color: $dark-text;
        }
      }
    }
  }
</style>

<style lang="scss">
    .sidebar-datepicker {
      background-color: white;
      font-size: .875rem;
      color: #ababab;
      border: 1px solid #D1D9E2;
      border-radius: 15px;
      font-weight: 400;
      padding-top: 1.0125rem;
      padding-bottom: 1.0125rem;
      padding-right: 1.875rem;
      padding-left: 1.25rem;
      margin-top: 3px;
      margin-bottom: 3px;
      line-height: unset;
    }

    .sidebar-datepicker:hover {
      border: 1px solid $dark-text;
    }
</style>