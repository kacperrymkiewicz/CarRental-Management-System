<script setup>
import { useToast, TYPE } from "vue-toastification";
import { useBookingStore } from '@/stores/booking.store';
import { useUserStore } from '@/stores/user.store';
import { useRoute, useRouter } from 'vue-router';
import { useI18n } from 'vue-i18n';
const emit = defineEmits(['closeBookingModal'])
const props = defineProps({
  modalActive: {
    type: Boolean,
    default: false
  },
  data: {},
});

const bookingStore = useBookingStore();
const userStore = useUserStore();
const route = useRoute();
const router = useRouter();
const toast = useToast();
const { t } = useI18n();

const addRental = async () => {
  const response = await bookingStore.addRental({
    pickupDate: route.query.pickupDate,
    pickupTime: route.query.pickupTime,
    returnDate: route.query.returnDate,
    returnTime: route.query.returnTime,
    carId: props.data.id,
    customerId: userStore.user.id
  });

  console.log(response);
  toast(response.message, {
    type: response.success ? TYPE.SUCCESS : TYPE.ERROR,
    timeout: 2000
  });

  emit('closeBookingModal');
  if(response.success) {
    router.push({ name: 'reservations' })
  }
}
</script>

<template>
  <teleport to="body">
    <div @click="emit('closeBookingModal')" v-show="modalActive" class="overlay"></div>
    <transition name="modal-animation">
      <dialog open v-if="modalActive">
        <div class="outer-wrapper">
          <img class="info-icon" src="/frontend/images/icons/svg/base_card_info.svg">
          <img class="close-button" @click="emit('closeBookingModal')" src="/frontend/images/icons/svg/base_modal_close.svg">
          <div class="inner-wrapper">
            <div class="container">
              <div class="row">
                <div class="col-md-12">
                  <p class="title mb-3">{{ t('Booking.Booking confirmation') }}</p>
                </div>
              </div>
              <div class="row">
                <div class="col-md-6">
                  <div class="content">
                    <p><span>{{ t('Car.Car') }}: </span> {{ data.brand }} {{ data.model }} {{ data.productionYear }}, {{ data.type }} {{ data.engine }} {{ data.horsepower }}km</p>
                    <p class="mb-3"><span>{{ t('Car.Transmission') }}: </span> {{ t(`Car.TransmissionType.${data.gearbox}`) }}</p>
                    <p><span>{{ t('Booking.Pick-up') }}: </span> {{ route.query.pickupDate }} {{ route.query.pickupTime }}</p>
                    <p><span>{{ t('Booking.Drop-off') }}: </span> {{ route.query.returnDate }} {{ route.query.returnTime }}</p>
                    <p><span>{{ t('Booking.Rental cost') }}: </span> {{ (bookingStore.rentalPeriod * data.price).toFixed(2) }} PLN</p>
                  </div>
                </div>
                <div class="col-md-6">
                  <div class="content">
                    <img class="img-fluid mt-4" :src="data.image">
                  </div>
                </div>
                <div class="modal-button">
                  <base-button class="mx-2" type="light" @click="emit('closeBookingModal')">{{ t('Cancel') }}</base-button>
                  <base-button class="mx-2" type="dark" @click="addRental">{{ t('Booking.Book a car') }}</base-button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </dialog>
    </transition>
  </teleport>
</template>

<style lang="scss" scoped>
.modal-animation-enter-active,
.modal-animation-leave-active {
  transition: all 0.3s ease;
}

.modal-animation-enter-from,
.modal-animation-leave-to {
  opacity: 0;
  transform: translate(-50%, -30%);
}

  div.overlay {
    position: fixed;
    top: 0;
    left: 0;
    height: 100vh;
    width: 100%;
    z-index: 1;
    background-color: rgba(0, 0, 0, 0.35);
  }

  dialog {
    width: 70%;
    border: 0;
    padding: 0;
    position: fixed;
    left: 50%;
    margin: 0;
    top: 50%;
    transform: translate(-50%, -50%);
    background-color: rgba(0, 0, 0, 0);
    z-index: 2;

    div.outer-wrapper {
      border: 1.5px solid $secondary;
      background-color: $base-modal-background;
      border-radius: 10px;
      filter: drop-shadow(0 0 30px #999);
      padding: 35px;
      position: relative;

      img.info-icon {
        position: absolute;
        top: 30px;
        left: 30px;
      }

      img.close-button {
        width: 30px;
        position: absolute;
        top: 30px;
        right: 30px;
        cursor: pointer;
      }

      div.inner-wrapper {
        padding: 25px;
        text-align: left;
        color: $secondary;

        p {
          margin-bottom: 0;
          font-size: 30px;

          &.title {
            font-weight: 700;
          }

          &.subtitle {
            text-transform: uppercase;
          }
        }


        div.content {
          p {
            margin: 5px 0;
            font-size: 20px;

            span {
              font-weight: 600;
            }
          }
        }

        .modal-button {
          margin-top: 30px;
          display: flex;
          justify-content: center;
        }
      }

    }
  }
</style>