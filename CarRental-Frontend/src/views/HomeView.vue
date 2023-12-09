<script setup>
import CarReservationPanel from '@/components/CarReservationPanel.vue'
import CarItem from '@/components/CarItem.vue'
import { useAuthStore } from '@/stores/auth.store';
import { useCarStore } from '@/stores/car.store';
import { useI18n } from 'vue-i18n';
const authStore = useAuthStore();
const carStore = useCarStore();
const {t} = useI18n();

carStore.fetchCars();
</script>

<template>
  <section id="home">
    <div class="container">
      <div class="row">
        <div class="col-md-8 offset-md-2">
          <h1>{{ t('Car rental') }}</h1>
          <p class="home-text">{{ t('Home.Introduction') }}</p>
          <div v-if="!authStore.isAuthenticated" class="home-buttons mt-3 mb-5">
            <router-link to="/logowanie"><base-button type="dark" class="mx-2">{{ t('Auth.Login') }}</base-button></router-link>
            <router-link to="/rejestracja"><base-button class="mx-2" style="background-color: #E6E9EC; border: 1px solid #D1D9E2; color: rgb(95, 109, 126);">{{ t('Auth.Register') }}</base-button></router-link>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-md-12">
          <img class="img-fluid" src="/frontend/images/homepage_vehicle.png" alt="Banner">
        </div>
      </div>
      <CarReservationPanel v-if="authStore.isAuthenticated"/>
    </div>
  </section>
  <section id="vehicles">
    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <h2 class="mt-5">{{ t('Home.Fleet') }}</h2>
          <p>{{ t('Home.Cars available for rent') }}</p>
          <div class="vehicles-fleet mt-5">
            <div class="row">
              <div v-for="car in carStore.cars" class="col-md-4">
                <CarItem :car="car"/>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<style lang="scss" scoped>
  h1 {
      margin: 60px 0 20px;
      font-weight: 700;
      font-weight: 700;
      font-size: 52px;
      line-height: 60px;
      text-align: center;
      letter-spacing: -0.01em;
      color: $button-dark;
  }

  h2 {
    font-weight: 700;
    font-size: 32px;
    line-height: 40px;
    letter-spacing: -0.02em;
    color: $button-dark;
  }

  p {
    font-size: 16px;
    color: $secondary;
  }

  p.home-text {
    font-size: 18px;
  }

  section#home {
    background-color: $primary;
    padding-bottom: 60px;
  }
</style>