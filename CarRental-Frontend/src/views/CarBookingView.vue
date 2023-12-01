<script setup>
import CarBookingItem from '@/components/CarBookingItem.vue'
import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css'
import { ref } from 'vue';
import { useRoute } from 'vue-router';
import { useBookingStore } from '@/stores/booking.store';
const bookingStore = useBookingStore();

const route = useRoute();

const isLoading = ref(true);


const fetchCars = async () => {
  const apiResponse = await bookingStore.searchCars(route.query.pickupDate + ' ' + route.query.pickupTime, route.query.returnDate + ' ' + route.query.returnTime);
  if(apiResponse.success) {
    isLoading.value = false;
  }
}

fetchCars();
</script>

<template>
  <section id="car-booking">
    <div class="container">
      <div class="row">
        <div class="col-lg-3">
          <div class="car-booking-sidebar-options">
            <div class="car-booking-sidebar-reservation-date">
              <h3>Data rezerwacji</h3>
              <VueDatePicker v-model="pickupDate" @update:model-value="updateDate()" input-class-name="datepicker-input" locale="pl" :disabled-week-days="[6, 0]" :enable-time-picker="false" :format="format" select-text="Zatwierdź" cancel-text="Anuluj" hide-input-icon auto-apply :min-date="new Date()" :clearable="false">{{ pickupDate }}</VueDatePicker>
              <h6>Od {{ route.query.pickupDate }} {{ route.query.pickupTime }}</h6>
              <h6>Do {{ route.query.returnDate }} {{ route.query.returnTime }}</h6>
              <base-button class="car-booking-button" type="dark">Wyszukaj samochód</base-button>
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
      & .car-booking-sidebar-reservation-date {
        border: 1px solid $dark-text;
        border-radius: 15px;
        padding: 10px;

        h3 {
          font-size: 1.25rem;
          font-weight: 600;
          color: $dark-text;
          text-align: left;
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