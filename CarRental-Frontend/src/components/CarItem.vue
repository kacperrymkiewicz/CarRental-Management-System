<script setup>
import IconFuelType from '@/components/icons/IconFuelType.vue'
import IconTransmission from '@/components/icons/IconTransmission.vue'
import IconCarDoor from '@/components/icons/IconCarDoor.vue'
import { useI18n } from 'vue-i18n';
const props = defineProps(['car']);
const { t } = useI18n();
</script>

<template>
  <div class="car-item">
    <img class="img-fluid" :src="car.image" :alt="car.brand + ' ' + car.model">
    <div class="car-model">
      <h4>{{ car.brand }} {{ car.model }}</h4>
      <h6>{{ car.engine }} {{ car.productionYear }}</h6>
    </div>
    <div class="car-specification">
      <div class="car-booking-fueltype car-icon">
        <span class="car-booking-icon"><IconFuelType/></span>
        <span>{{ t(`Car.FuelType.${car.fuelType}`) }}</span>
      </div>
      <div class="car-booking-transmission car-icon">
        <span class="car-booking-icon"><IconTransmission/></span>
        <span>{{ t(`Car.TransmissionType.${car.gearbox}`) }}</span>
      </div>
      <div class="car-booking-doors-number car-icon">
        <span class="car-booking-icon"><IconCarDoor/></span>
        <span>{{ t('Car.Doors', { doors: car.doorsNumber }) }}</span>
      </div>
    </div>
    <router-link :to="{ name: 'car', params: { id: car.id } }">
      <base-button class="car-button">{{ t('Car.Details') }}</base-button>
    </router-link>
  </div>
</template>

<style scoped lang="scss">
  .car-item {
    background: #F8F9FB;
    border: 1px solid #D1D9E2;
    box-shadow: 0px 1px 2px rgba(16, 24, 40, 0.05);
    border-radius: 5px;
    padding: 25px;
    margin-bottom: 35px;

    .car-model {
      margin: 20px 0;
      
      h4 {
        font-weight: 700;
      }
    }
    .car-button {
      background-color: #E6E9EC!important; 
      border: 1px solid #D1D9E2!important; 
      color: rgb(95, 109, 126)!important;
      font-weight: 600!important;
      width: 100%;
      margin: 5px 0;
    }

    .car-specification {
      display: flex;
      justify-content: center;
      .car-icon {
        display: flex;
        flex-direction: column;
        margin: 10px 15px;
      }
    }
  }
</style>