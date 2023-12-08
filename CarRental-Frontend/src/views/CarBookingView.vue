<script setup>
import CarBookingItem from '@/components/CarBookingItem.vue'
import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css'
import { ref } from 'vue';
import { useI18n } from 'vue-i18n';
import { useToast } from "vue-toastification";
import { useRoute, useRouter } from 'vue-router';
import { useBookingStore } from '@/stores/booking.store';
const bookingStore = useBookingStore();

const route = useRoute();
const router = useRouter();
const toast = useToast();
const format = 'dd/MM/yyyy';
const { t } = useI18n();

bookingStore.syncQueryParams(route.query);

const fetchCars = async () => {
  await bookingStore.searchCars(route.query.pickupDate + ' ' + route.query.pickupTime, route.query.returnDate + ' ' + route.query.returnTime);
  if(bookingStore.responseStatus.message) {
    toast.error(bookingStore.responseStatus.message);
  }
}

const updateParams = () => {
  router.replace({ 
    name: 'booking', 
    query: bookingStore.buildQueryParams()
  })
  setTimeout(() => {
    paramsValidated.value = expectedParams.every(key => key in route.query);
    fetchCars();
  }, 0);
}

const expectedParams = ['pickupDate', 'pickupTime', 'returnDate', 'returnTime', 'vehicleType'];
const paramsValidated = ref(expectedParams.every(key => key in route.query));

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
              <h5>{{ t('Booking.Change booking date') }}</h5>
              <h6>{{ t('Booking.Pick-up') }}</h6>
              <VueDatePicker v-model="bookingStore.pickupDate" @update:model-value="bookingStore.updateDate()" input-class-name="sidebar-datepicker" locale="pl" :disabled-week-days="[6, 0]" :enable-time-picker="false" :format="format" select-text="Zatwierdź" cancel-text="Anuluj" hide-input-icon auto-apply :min-date="new Date()" :clearable="false">{{ pickupDate }}</VueDatePicker>
              <VueDatePicker v-model="bookingStore.pickupTime" time-picker input-class-name="sidebar-datepicker" locale="pl" minutes-increment="15" minutes-grid-increment="15" select-text="Zatwierdź" cancel-text="Anuluj" hide-input-icon auto-apply :clearable="false"></VueDatePicker>
              <h6>{{ t('Booking.Drop-off') }}</h6>
              <VueDatePicker v-model="bookingStore.returnDate" input-class-name="sidebar-datepicker" locale="pl" :disabled-week-days="[6, 0]" :enable-time-picker="false" :format="format" select-text="Zatwierdź" cancel-text="Anuluj" hide-input-icon auto-apply :min-date="bookingStore.pickupDate" :clearable="false">{{ pickupDate }}</VueDatePicker>
              <VueDatePicker v-model="bookingStore.returnTime" time-picker input-class-name="sidebar-datepicker" locale="pl" minutes-increment="15" minutes-grid-increment="15" select-text="Zatwierdź" cancel-text="Anuluj" hide-input-icon auto-apply :clearable="false"></VueDatePicker>
              <base-button @click.prevent="updateParams()" class="car-booking-button" type="dark">{{ t('Booking.Search car') }}</base-button>
            </div>
          </div>
        </div>
        <div class="col-lg-9">
          <div v-if="bookingStore.responseStatus.success" class="car-booking-available-cars">
            <div v-if="!bookingStore.carsFound" class="car-booking-notfound">
              <h3>Nie znaleziono dostępnych pojazdów w podanym terminie</h3>
            </div>
            <template v-else>
              <h5>{{ t('Booking.Available cars') }}: <span>{{ bookingStore.carsAmount }}</span></h5>
              <template v-for="car in bookingStore.searchResult">
                <CarBookingItem v-if="car.status" :car="car"/>
              </template>
            </template>
          </div>
          <div v-else-if="!paramsValidated">
            <h3>Wybierz termin</h3>
          </div>
          <div v-else-if="bookingStore.responseStatus.error">
            <h3>Wystąpił błąd</h3>
          </div>
          <div v-else>
            <h3 class="mb-3">Trwa wyszukiwanie dostępnych pojazdów...</h3>
            <div class="spinner-border" role="status">
              <span class="visually-hidden">Loading...</span>
            </div>
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