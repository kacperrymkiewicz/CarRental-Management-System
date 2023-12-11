<script setup>
import { ref } from 'vue';
import BookingModal from '@/components/BookingModal.vue';
import IconFuelType from '@/components/icons/IconFuelType.vue';
import IconTransmission from '@/components/icons/IconTransmission.vue';
import IconCarDoor from '@/components/icons/IconCarDoor.vue';
import { useI18n } from 'vue-i18n';
const props = defineProps(['car']);
const { t } = useI18n();

const bookingModalIsOpen = ref(false);
const bookingModalData = ref([]);
const openBookingModal = (bookingData) => {
  bookingModalData.value = bookingData;
  bookingModalIsOpen.value = true;
}
</script>

<template>
  <div class="car-booking-item">
    <div class="row">
      <div class="col-md-5">
        <div class="car-booking-image">
          <img class="img-fluid" :src="car.image" :alt="car.brand + ' ' + car.model">
        </div>
      </div>
      <div class="col-md-7">
        <div class="car-booking-parameters">
          <div class="car-booking-model">
            <h1>{{ car.brand }} {{ car.model }}</h1>
            <h2>{{ car.engine }} {{ car.productionYear }}</h2>
          </div>
          <div class="car-booking-specification">
            <div class="car-booking-fueltype">
              <span class="car-booking-icon"><IconFuelType/> {{ t('Car.Fuel') }}</span>
              <span>{{ t(`Car.FuelType.${car.fuelType}`) }}</span>
            </div>
            <div class="car-booking-transmission">
              <span class="car-booking-icon"><IconTransmission/> {{ t('Car.Transmission') }}</span>
              <span>{{ t(`Car.TransmissionType.${car.gearbox}`) }}</span>
            </div>
            <div class="car-booking-doors-number">
              <span class="car-booking-icon"><IconCarDoor/> {{ t('Car.Doors number') }}</span>
              <span>{{ t('Car.Doors', { doors: car.doorsNumber }) }}</span>
            </div>
          </div>
          <div class="car-booking-details">
            <router-link :to="{ name: 'car', params: { id: car.id } }">
              {{ t('Car.Show car details') }}
            </router-link>
          </div>
          <div class="car-booking-price">
            <h3>{{ car.price.toFixed(2) }} PLN</h3>
            <h5>{{ t('Car.Price', { days_number: 1 }, 1) }}</h5>
          </div>
          <base-button class="car-booking-button" @click="openBookingModal(car)" type="dark" :has-icon="true">{{ t('Booking.Book now') }}</base-button>
        </div>
      </div>
    </div>
    <booking-modal v-if="bookingModalIsOpen" :data="bookingModalData" @close-booking-modal="bookingModalIsOpen = false"></booking-modal>
  </div>
</template>

<style scoped lang="scss">
  .car-booking-item {
    padding: 15px;
    border: 1px solid #D1D9E2;
    border-radius: 15px;
    margin-bottom: 30px;

    & .car-booking-image {
      display: flex;
      align-items: center;
      justify-content: center;
      height: 100%;
    }

    & .car-booking-parameters {
      padding: 10px 35px;
      @media (min-width: 992px) {
        border-left: 1px solid #D1D9E2;
      }

      & .car-booking-specification {
        display: flex;
        justify-content: center;
        align-items: center;
        margin-bottom: 25px;

        & div {
          margin: 0 15px;

          & span:last-child {
            font-weight: 600;
          }
        }

        & .car-booking-icon {
          display: flex;
          justify-content: center;
          align-items: center;

          & svg {
            margin-right: 3px;
          }
        }
      }

      & .car-booking-model {
        text-align: left;

        & h1 {
          font-size: 1.85rem;
          font-weight: 700;
          margin-bottom: 3px;
        }

        & h2 {
          font-size: 1.1rem;
          margin-bottom: 30px;
        }
      }

      & .car-booking-details {
        text-align: right;
        margin-bottom: 5px;
      }

      & .car-booking-price {
        text-align: right;

        h3 {
          font-weight: 600;
          margin-bottom: 0px;
        }

        h5 {
          font-weight: 300;
          margin-top: 0px;
        }
      }

      & .car-booking-button {
        width: 100%;
        margin: 10px 0;
    }
    }
  }
</style>