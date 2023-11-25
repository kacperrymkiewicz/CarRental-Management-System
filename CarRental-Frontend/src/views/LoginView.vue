<script setup>
import { ref } from 'vue';
import { useToast, TYPE } from "vue-toastification";
import { useAuthStore } from '@/stores/auth.store';
const authStore = useAuthStore();

const emailAddress = ref('');
const password = ref('');

const toast = useToast();

const login = async () => {
  const authResponse = await authStore.authenticateUser(emailAddress.value, password.value);
  console.log(authResponse);
  toast(authResponse.message, {
    type: authResponse.success ? TYPE.SUCCESS : TYPE.ERROR,
    timeout: 2000
  });
}
</script>

<template>
  <section id="login">
    <div class="container">
      <div class="row d-flex justify-content-center">
        <div class="inner-container col-sm-10 col-md-8 col-lg-6">
          <h1>Logowanie</h1>
          <form @submit.prevent="login()">
            <div class="form-group d-flex flex-column align-items-center">
              <label for="email">Adres email</label>
              <input type="email" class="form-control" id="email" v-model="emailAddress">
            </div>

            <div class="form-group d-flex flex-column align-items-center">
              <label for="password">Hasło</label>
              <input type="password" class="form-control" id="password" v-model="password">
            </div>
            <base-button type="dark" :has-icon="true">Zaloguj się</base-button>
            <p>Nie masz konta? <router-link to="/rejestracja">Zarejestruj się</router-link></p>
          </form>
        </div>
      </div>
    </div>
  </section>
</template>

<style lang="scss" scoped>
.row {
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

div.inner-container {
  form {
    div.form-group {
      margin: 13px 0;

      label {
        font-weight: 500;
        font-size: 14px;
        line-height: 30px;
        letter-spacing: -0.001em;
        color: $secondary;
        align-self: flex-start;
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

  div+p {
    margin: 0;
    color: $button-red;
  }

}
</style>
  