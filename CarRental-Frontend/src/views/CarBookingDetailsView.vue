<script setup>
import IconFuelType from '@/components/icons/IconFuelType.vue'
import IconTransmission from '@/components/icons/IconTransmission.vue'
import IconCarDoor from '@/components/icons/IconCarDoor.vue'
import IconCarSeat from '@/components/icons/IconCarSeat.vue'
import IconCarTrunk from '@/components/icons/IconCarTrunk.vue'
import IconCarEngine from '@/components/icons/IconCarEngine.vue'
import IconCarCluster from '@/components/icons/IconCarCluster.vue'
import IconCarType from '@/components/icons/IconCarType.vue'
import IconCalendarAlternative from '@/components/icons/IconCalendarAlternative.vue'
import { useCarStore } from '@/stores/car.store';
import { useRoute, useRouter } from 'vue-router';
import { useI18n } from 'vue-i18n';
const route = useRoute();
const router = useRouter();
const { t } = useI18n();

const carStore = useCarStore();

const fetchCar = async () => {
  const carResponse = await carStore.fetchCar(route.params.id);
  if(carResponse.success) {
    const slug = `${carStore.car.brand}-${carStore.car.model}`;
    if(route.params.slug != slug) {
      router.replace({ 
        name: 'car', 
        params: {
          id: route.params.id,
          slug: slug
        }
      })
    }
    return;
  }
  router.replace({ name: 'home' });
}

fetchCar();
</script>

<template>
  <section id="car-booking-details">
    <div v-if="carStore.car" class="container">
      <div class="row">
        <div class="col-md-12">
          <breadcrumbs class="mb-5" :homepageNav="true">
            <router-link to="/samochody">Samochody</router-link>
            <router-link :to="route.params">{{ carStore.car.brand }} {{ carStore.car.model }}</router-link>
          </breadcrumbs>
        </div>
      </div>
      <div class="row g-5">
        <div class="col-md-7">
          <div class="car-image">
            <img class="img-fluid" :src="carStore.car.image" :alt="carStore.car.brand + ' ' + carStore.car.model">
          </div>
          <div class="car-equipment">
            <h5 class="mb-4">{{ t('Car.Equipment') }}</h5>
            <div class="row">
              <div class="col-md-4">
                <p>Klimatyzacja</p>
              </div>
              <div class="col-md-4">
                <p>Klimatyzacja</p>
              </div>
              <div class="col-md-4">
                <p>Klimatyzacja</p>
              </div>
              <div class="col-md-4">
                <p>Klimatyzacja</p>
              </div>
              <div class="col-md-4">
                <p>Klimatyzacja</p>
              </div>
              <div class="col-md-4">
                <p>Klimatyzacja</p>
              </div>
            </div>
          </div>
        </div>
        <div class="col-md-5">
          <div class="car-model">
            <h1>{{ carStore.car.brand }} {{ carStore.car.model }}</h1>
          </div>
          <div class="car-price">
            <h3>{{ carStore.car.price.toFixed(2) }} PLN</h3>
            <h5>{{ t('Car.Price', { days_number: 1 }, 1) }}</h5>
          </div>
          <div class="car-details">
            <h5 class="mb-3">{{ t('Car.Car overview') }}</h5>
            <table class="table">
              <tbody>
                <tr>
                  <td><IconCarType/> {{ t('Car.Body') }}</td>
                  <td>{{ carStore.car.type }}</td>
                </tr>
                <tr>
                  <td><IconFuelType/> {{ t('Car.Fuel') }}</td>
                  <td>{{ carStore.car.fuelType }}</td>
                </tr>
                <tr>
                  <td><IconCarCluster/> {{ t('Car.AvgFuelConsumption') }}</td>
                  <td>{{ carStore.car.avgFuelConsumption }}l/100km</td>
                </tr>
                <tr>
                  <td><IconCalendarAlternative/> {{ t('Car.Production year') }}</td>
                  <td>{{ carStore.car.productionYear }}</td>
                </tr>
                <tr>
                  <td><IconTransmission/> {{ t('Car.Transmission') }}</td>
                  <td>{{ t(`Car.TransmissionType.${carStore.car.gearbox}`) }}</td>
                </tr>
                <tr>
                  <td><IconCarEngine/> {{ t('Car.Engine') }}</td>
                  <td>{{ carStore.car.engine }} {{ carStore.car.horsepower }}km</td>
                </tr>
                <tr>
                  <td><IconCarDoor/> {{ t('Car.Doors number') }}</td>
                  <td>{{ carStore.car.doorsNumber }}</td>
                </tr>
                <tr>
                  <td><IconCarSeat/> {{ t('Car.Seats number') }}</td>
                  <td>{{ carStore.car.seatsNumber }}</td>
                </tr>
                <tr>
                  <td><IconCarTrunk/> {{ t('Car.Trunk capacity') }}</td>
                  <td>{{ carStore.car.trunkCapacity }}l</td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<style lang="scss">
  #car-booking-details {
    text-align: left;

    h1 {
      font-weight: 700;
    }

    h3 {
      font-weight: 600;
      margin: 0;
    }

    h5 { 
      font-weight: 300;
    }

    .car-image {
      text-align: center;
    }

    .car-details {
      border: 1px solid #D1D9E2;
      background-color: #fafbfd;
      border-radius: 15px;
      padding: 25px;

      .table {
        --bs-table-bg: #fafbfd;
        margin: 0;

        td {
          padding-left: 0;
          padding-right: 0;
        }

        tr:last-child {
          border-color: transparent;
        }
      }
    }

    .car-equipment {
      margin-top: 30px;
      padding-top: 15px;
      border-top: 1px solid #D1D9E2;
    }
  }
</style>