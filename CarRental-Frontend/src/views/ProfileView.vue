<script setup>
import IconProfile from '@/components/icons/IconProfile.vue'
import WelcomeMessage from '@/components/WelcomeMessage.vue'
import { useUserStore } from '@/stores/user.store';
const userStore = useUserStore();
</script>

<template>
  <section id="profile">
    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <breadcrumbs :is-customer="true">
            <router-link :to="{ name: 'profile' }">Mój profil</router-link>
          </breadcrumbs>
          <welcome-message :name="userStore.user.firstname">
            <template v-slot:info>Tutaj możesz zobaczyć oraz edytować swój profil</template>
          </welcome-message>
          <div class="d-flex flex-column align-items-center">
            <div class="col-md-12">
              <div class="wrapper d-flex flex-column">
                <div class="name d-flex flex-column align-items-center">
                  <icon-profile class="profile-icon" />
                  <span> {{ capitalizeFirstLetter(userStore.user.firstname) }} {{ capitalizeFirstLetter(userStore.user.lastname) }}</span>
                </div>
                <div class="buttons">
                  <router-link to="/profil/edycja-profilu"><base-button type="dark">Edytuj
                      profil</base-button></router-link>
                  <router-link to="/profil/edycja-hasla"><base-button type="dark">Zmień hasło</base-button></router-link>
                </div>
              </div>
              <div class="row base-cards-outer-wrapper">
                <div class="col-lg-6">
                  <base-card class="base-card">
                    <template v-slot:title>Dane personalne</template>
                    <template v-slot:content>
                      <p><span>Adres email: </span> {{ userStore.user.email }}</p>
                      <p><span>Telefon kontaktowy: </span> 123123123 </p>
                      <p><span>PESEL: </span> 123123123</p>
                      <p><span>Miejscowość: </span> test</p>
                      <p><span>Kod pocztowy: </span> test</p>
                      <p><span>Ulica i nr budynku: </span> test</p>
                    </template>
                  </base-card>
                </div>
                <div class="col-lg-6">
                  <div class="base-cards-inner-wrapper d-flex flex-column justify-content-between">
                    <base-card class="base-card-sm">
                      <template v-slot:title>Lorem, ipsum dolor.</template>
                      <template v-slot:content>
                        <p>Lorem ipsum dolor sit, amet consectetur adipisicing elit. Eligendi, nam!</p>
                      </template>
                    </base-card>
                    <base-card class="base-card-sm">
                      <template v-slot:title>Lorem, ipsum dolor.</template>
                      <template v-slot:content>
                        <p>Lorem ipsum dolor sit, amet consectetur adipisicing elit. Eligendi, nam!</p>
                      </template>
                    </base-card>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<style lang="scss" scoped>
#profile {
  @media (min-width: 992px) {
    padding-bottom: 40px;
  }
}

div.wrapper {

  div.name {
    .profile-icon {
      width: 90px;
    }

    span {
      font-weight: 700;
      font-size: 32px;
      line-height: 40px;
      letter-spacing: -0.02em;
      margin-top: 20px;
    }
  }

  div.buttons {
    display: flex;
    flex-direction: column;
    align-self: flex-end;
    margin: 24px 0;

    @media (max-width: 992px) {
      flex-direction: row;
      align-self: center;
    }

    button {
      margin: 12px;
    }
  }
}

div.base-cards-outer-wrapper {
  margin-top: 20px;

  .base-card>div:nth-child(1)>div:nth-child(2)>div:nth-child(2) {
    p {
      margin-bottom: 12px;
    }
  }

  .base-cards-inner-wrapper {
    height: 100%;
  }
}

@media (max-width: 992px) {

  .col-lg-6,
  .base-cards-inner-wrapper {

    div.base-card,
    .base-card-sm {
      margin: 20px 0;

    }
  }
}</style>