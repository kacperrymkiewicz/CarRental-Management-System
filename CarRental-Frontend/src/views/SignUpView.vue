<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useToast, TYPE } from "vue-toastification";

const firstName = ref('');
const lastName = ref('');
const emailAddress = ref('');
const password = ref('');
const repeatPassword = ref('');
const phoneNumber = ref('');
const postalCode = ref('');
const city = ref('');
const street = ref('');
const houseNumber = ref('');
const regulationsAccepted = ref(false); 

const router = useRouter();
const toast = useToast();

// const login = async () => {
//   const authResponse = await authStore.authenticateUser(emailAddress.value, password.value);
//   toast(authResponse.message, {
//     type: authResponse.success ? TYPE.SUCCESS : TYPE.ERROR,
//     timeout: 2000
//   });

//   if(authResponse.success) {
//     userStore.decodeToken();
//     redirectAfterSuccessfullLogin();
//   }
// }

const register = async () => {

}
</script>

<template>
  <section id="signup">
      <div class="container">
      <div class="row d-flex flex-column align-content-center">
          <div class="container-inner col-sm-10 col-md-8 col-lg-6">
          <h1>Rejestracja</h1>
          <form @submit.prevent="register()">
              <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="name">Imię</label>
                  <input type="text" class="form-control" id="name" v-model.trim="firstName">
              </div>

              <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="lastname">Nazwisko</label>
                  <input type="text" class="form-control" id="lastname" v-model.trim="lastName">
              </div>

              <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="email">Adres email</label>
                  <input type="text" class="form-control" id="email" v-model.trim="emailAddress">
              </div>

              <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="password">Hasło</label>
                  <input type="password" class="form-control" id="password" v-model.trim="password">
              </div>

              <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="repeat-pass">Powtórz hasło</label>
                  <input type="password" class="form-control" id="repeat-pass" v-model.trim="repeatPassword">
              </div>

              <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="tel">Telefon kontaktowy</label>
                  <input type="tel" class="form-control" id="tel" v-model.trim="phoneNumber">
              </div>

              <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="postal-code">Kod pocztowy</label>
                  <input type="text" class="form-control" id="postal-code" v-model.trim="postalCode" placeholder="00-000">
              </div>

              <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="city">Miejscowość</label>
                  <input type="text" class="form-control" id="city" v-model.trim="city">
              </div>

              <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="street">Ulica</label>
                  <input type="text" class="form-control" id="street" v-model.trim="street">
              </div>

              <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="house-number">Nr budynku</label>
                  <input type="text" class="form-control" id="house-number" v-model.trim="houseNumber">
              </div>
              
              <div class="form-check d-flex">
                  <input type="checkbox" class="form-check-input" id="regulamin" v-model="regulationsAccepted">
                  <label class="form-check-label" for="regulamin">Akceptuję <span><router-link class="align-self-start" to="#">regulamin</router-link></span></label>
              </div>

              <base-button type="dark" :has-icon="true">Zarejestruj się</base-button>
              <p>Masz już konto? <router-link to="/logowanie">Zaloguj się</router-link></p>
          </form>
          </div>
      </div>
      </div>
  </section>
</template>

<style lang="scss" scoped>
  div.row {
      p {
          color: $button-dark;
          font-style: normal;
          font-weight: 600;
          font-size: 15px;
          line-height: 22px;
          letter-spacing: -0.001em;
          color: $secondary;
          
          a {
              color: $dark-text;
          }
      }
  } 

  div.container-inner {
      form {
          div.form-group {
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
          }

          div.form-check {
              margin: 30px 0 10px;
              input {
                  border-color: $secondary;
                  &:checked {
                      background-color: $dark-text;
                      border-color: $secondary;
                  }
              }

              label {
                  margin-left: 10px;
                  font-weight: 500;
                  color: $secondary;

                  span {

                      a {
                          color: $dark-text;
                      }
                  }
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
      
      div + p {
          margin: 0;
          color: $button-red;
      }
  }
</style>
