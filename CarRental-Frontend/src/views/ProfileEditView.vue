<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useToast, TYPE } from "vue-toastification";
import { useUserStore } from '@/stores/user.store';
const userStore = useUserStore();

const router = useRouter();
const toast = useToast();

const firstname = ref(userStore.userData.firstName);
const lastname = ref(userStore.userData.lastName);
const emailAddress = ref(userStore.userData.emailAddress);
const phoneNumber = ref(userStore.userData.address.phoneNumber);
const zipCode = ref(userStore.userData.address.zipCode);
const city = ref(userStore.userData.address.city);
const street = ref(userStore.userData.address.street);
const houseNumber = ref(userStore.userData.address.houseNumber);

const updateProfile = async () => {
  const responseStatus = await userStore.updateProfile(
    firstname.value,
    lastname.value,
    emailAddress.value,
    phoneNumber.value,
    zipCode.value,
    city.value,
    street.value,
    houseNumber.value
  )
  toast(responseStatus.message, {
      type: responseStatus.success ? TYPE.SUCCESS : TYPE.ERROR,
      timeout: 2000
  });
  if(responseStatus.success) {
    userStore.fetchUser();
    userStore.user.firstname = firstname.value;
    userStore.user.lastname = lastname.value;
    router.push({ name: 'profile' });
  }
}
</script>

<template>
  <section id="edit-profile">
    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <breadcrumbs :is-customer="true">
            <router-link to="/profil">Mój profil</router-link>
            <router-link to="/profil/edycja-profilu">Edycja profilu</router-link>
          </breadcrumbs>
          <welcome-message :name="userStore.user.firstname" icon-name="clipboard">
            <template v-slot:info>Tutaj możesz edytować swoje dane personalne</template>
            </welcome-message>
          <div class="d-flex flex-column align-items-center">
            <div class="col-md-6">
              <h1>Edycja profilu</h1>
              <form @submit.prevent="updateProfile">
                <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="name">Imię</label>
                  <input type="text" class="form-control" id="name" v-model.trim="firstname">
                </div>

                <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="lastname">Nazwisko</label>
                  <input type="text" class="form-control" id="lastname" v-model.trim="lastname">
                </div>

                <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="email">Adres email</label>
                  <input type="text" class="form-control" id="email" v-model.trim="emailAddress">
                </div>

                <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="tel">Telefon kontaktowy</label>
                  <input type="tel" class="form-control" id="tel" v-model.trim="phoneNumber">
                </div>

                <div class="form-group d-flex flex-column">
                  <label class="align-self-start" for="postal-code">Kod pocztowy</label>
                  <input type="text" class="form-control" id="postal-code" v-model.trim="zipCode"
                    placeholder="00-000">
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
                  <label class="align-self-start" for="house-number">Numer budynku</label>
                  <input type="text" class="form-control" id="house-number" v-model.trim="houseNumber">
                </div>

                <base-button type="dark">Zapisz zmiany</base-button>
                <router-link to="/profil"><base-button type="light">Anuluj</base-button></router-link>
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