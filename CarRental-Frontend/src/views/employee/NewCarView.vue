<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useToast, TYPE } from "vue-toastification";
import { useUserStore } from '@/stores/user.store';
const userStore = useUserStore();

const router = useRouter();
const toast = useToast();

const updateCar = async () => {

}
</script>

<template>
  <section id="car-form">
    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <breadcrumbs>
            <router-link :to="{ name: 'panel-cars' }">Zarządzanie flotą</router-link>
            <router-link :to="{ name: 'panel-cars-add' }">Dodaj nowy samochód</router-link>
          </breadcrumbs>
          <div class="d-flex flex-column align-items-center">
            <div class="col-md-6">
              <h1>Dodaj samochód</h1>
              <form @submit.prevent="updateProfile">
                <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="name">Marka</label>
                  <input type="text" class="form-control" id="name" v-model.trim="firstname">
                </div>

                <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="lastname">Model</label>
                  <input type="text" class="form-control" id="lastname" v-model.trim="lastname">
                </div>

                <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="email">Rodzaj nadwozia</label>
                  <select class="form-select">
                    <option>Sedan</option>
                    <option>SUV</option>
                  </select>
                </div>

                <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="tel">Numer rejestracyjny</label>
                  <input type="tel" class="form-control" id="tel" v-model.trim="phoneNumber">
                </div>

                <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="postal-code">Silnik</label>
                  <input type="text" class="form-control" id="postal-code" v-model.trim="zipCode">
                </div>

                <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="city">Rodzaj paliwa</label>
                  <select class="form-select">
                    <option>Benzyna</option>
                    <option>Diesel</option>
                  </select>
                </div>

                <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="house-number">Średnie spalanie paliwa</label>
                  <input type="number" class="form-control" id="house-number" v-model.trim="houseNumber">
                </div>

                <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="house-number">Skrzynia biegów</label>
                  <select class="form-select">
                    <option>Automatyczna</option>
                    <option>Manualna</option>
                  </select>
                </div>

                <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="street">Rok produkcji</label>
                  <input type="text" class="form-control" id="street" v-model.trim="street">
                </div>

                <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="house-number">Ilość drzwi</label>
                  <input type="text" class="form-control" id="house-number" v-model.trim="houseNumber">
                </div>

                <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="house-number">Ilość siedzeń</label>
                  <input type="text" class="form-control" id="house-number" v-model.trim="houseNumber">
                </div>

                <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="house-number">Pojemność bagażnika</label>
                  <input type="text" class="form-control" id="house-number" v-model.trim="houseNumber">
                </div>

                <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="house-number">Obraz</label>
                  <input type="text" class="form-control" id="house-number" v-model.trim="houseNumber">
                </div>

                <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="house-number">Cena</label>
                  <input type="text" class="form-control" id="house-number" v-model.trim="houseNumber">
                </div>

                <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="house-number">Status</label>
                  <select class="form-select">
                    <option>Aktywny</option>
                    <option>Nieaktywny</option>
                  </select>
                </div>

                <base-button type="dark">Dodaj</base-button>
                <router-link :to="{ name: 'panel-cars' }"><base-button type="light">Anuluj</base-button></router-link>
              </form>

            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<style lang="scss" scoped>
.col-md-6 {
  form {
    .form-group {
      margin: 13px 0;

      label {
        font-weight: 500;
        font-size: 14px;
        line-height: 30px;
        letter-spacing: -0.001em;
        color: $secondary;

      }

      input {
        box-sizing: border-box;
        border: 1px solid #5F6D7E;
        border-radius: 8px;
        display: flex;
        padding: 8px 18px;
        gap: 16px;
        background: #F8F9FB;

      }

      textarea {
        box-sizing: border-box;
        border: 1px solid #5F6D7E;
        border-radius: 8px;
        background: #F8F9FB;
        padding: 8px 18px;
        resize: none;

      }

      select {
        box-sizing: border-box;
        border: 1px solid #5F6D7E;
        border-radius: 8px;
        padding: 8px 18px;
        gap: 16px;
        background-color: #F8F9FB;
      }
    }

    .form-check {
      input {
        border-color: $secondary;

        &:checked {
          background-color: $teal;
          border-color: $secondary;
        }
      }

      label {
        margin-left: 10px;
        font-weight: 500;
        color: $secondary;

        span {

          a {
            color: $teal;
          }

        }
      }
    }

    p {
      color: $button-dark;
      font-style: normal;
      font-weight: 600;
      font-size: 15px;
      line-height: 22px;
      letter-spacing: -0.001em;
      color: $secondary;

      a {
        color: $teal;
      }

    }
  }

  h1 {
    margin: 60px 0;
    font-weight: 700;
    font-weight: 700;
    font-size: 52px;
    line-height: 60px;
    text-align: center;
    letter-spacing: -0.01em;
    color: $button-dark;
  }
}

.base-button {
  margin: 30px 10px;

  .span-icon {
    display: inline-block;
    transition: all 0.2s ease-in-out;
  }
}

.base-button:hover .span-icon {
  transform: translateX(5px);
}</style>