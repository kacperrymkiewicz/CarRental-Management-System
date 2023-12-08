<script setup>
import IconDropDownMenuArrowDown from '@/components/icons/IconDropdownMenuArrowDown.vue'
import LanguageSwitcher from '@/components/LanguageSwitcher.vue'
import { ref } from 'vue';
import { useI18n } from 'vue-i18n';
import { useRouter } from 'vue-router';
import { useToast } from "vue-toastification";
import { useAuthStore } from '@/stores/auth.store';
import { useUserStore } from '@/stores/user.store';
const authStore = useAuthStore();
const userStore = useUserStore();

const router = useRouter();
const toast = useToast();
const { t } = useI18n();

let isDropdownMenuOpen = ref(false);

const toggleDropdownMenu = () => {
  isDropdownMenuOpen.value = !isDropdownMenuOpen.value;
}

const logout = () => {
  userStore.$reset();
  authStore.$reset();
  router.push("/logowanie");
  toast("Wylogowano pomyślnie", {
    timeout: 2000
  });
}
</script>

<template>
  <header>
    <nav class="navbar navbar-expand-md bg-body-tertiary">
      <div class="container-fluid">
        <router-link to="/"><img src="/frontend/images/logo.png" class="nav-logo" alt="Logo"></router-link>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>

        <div v-if="authStore.isAuthenticated" class="logged-in collapse navbar-collapse" id="navbarSupportedContent">
          <ul v-if="userStore.isAdministrator" class="navbar-nav me-auto mb-lg-0">
            <li class="nav-item">
              <router-link class="nav-link" active-class="active-link" to="/samochody">Samochody</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" active-class="active-link" to="/klienci">Klienci</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" active-class="active-link" to="/rezerwacje">Rezerwacje</router-link>
            </li>
          </ul>

          <ul v-else-if="userStore.isEmployer" class="navbar-nav me-auto mb-lg-0">
            <li class="nav-item">
              <router-link class="nav-link" active-class="active-link" to="/klienci">Klienci</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" active-class="active-link" to="/rezerwacje">Rezerwacje</router-link>
            </li>
          </ul>

          <ul v-else class="navbar-nav me-auto mb-lg-0">
            <li class="nav-item">
              <router-link class="nav-link" active-class="active-link" to="/">Strona główna</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" active-class="active-link" to="/rezerwacja">Wynajmij samochód</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" active-class="active-link" :to="{ name: 'cars' }">Flota</router-link>
            </li>
            <li class="nav-item"> 
              <router-link class="nav-link" active-class="active-link" to="/rezerwacje">Moje rezerwacje</router-link>
            </li>
          </ul>
          
          <div v-if="isDropdownMenuOpen" @click="toggleDropdownMenu" class="overlay"></div>
          <div @click="toggleDropdownMenu" class="profile d-flex align-items-center">
            <svg class="profile-icon" width="40" height="40" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
              <rect width="40" height="40" rx="20" fill="#5F6D7E"/>
              <path d="M20 21.3334C21.8409 21.3334 23.3333 19.841 23.3333 18C23.3333 16.1591 21.8409 14.6667 20 14.6667C18.159 14.6667 16.6666 16.1591 16.6666 18C16.6666 19.841 18.159 21.3334 20 21.3334ZM20 21.3334C17.0544 21.3334 14.6666 23.1242 14.6666 25.3334M20 21.3334C22.9455 21.3334 25.3333 23.1242 25.3333 25.3334" stroke="white" stroke-width="1.5" stroke-linecap="round"/>
            </svg>
            <div>
              <p> {{ capitalizeFirstLetter(userStore.user.firstname) }} {{ capitalizeFirstLetter(userStore.user.lastname) }}</p>
              <p> {{ t(`Account.${userStore.user.authorization}`) }}</p>
            </div>
            <IconDropDownMenuArrowDown/>
            <div v-if="isDropdownMenuOpen" class="drop-down-menu">
              <ul>
                <router-link to="/profil" v-if="userStore.isCustomer">
                  <li>
                    <span>
                      <svg width="18" height="21" viewBox="0 0 18 21" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path d="M1.94 5.91249L7.94 2.16249C8.58854 1.75716 9.41146 1.75716 10.06 2.16249L16.06 5.91249C16.6448 6.27797 17 6.91891 17 7.60849V13.3915C17 14.0811 16.6448 14.722 16.06 15.0875L10.06 18.8375C9.41146 19.2428 8.58854 19.2428 7.94 18.8375L1.94 15.0875C1.35524 14.722 1 14.0811 1 13.3915V7.60849C1 6.91891 1.35524 6.27797 1.94 5.91249Z" stroke="#5F6D7E" stroke-width="2" stroke-linecap="round"/>
                      </svg>
                    </span>
                    {{ t('Account.My profile') }}
                  </li>
                </router-link>
                <a href="#" @click="logout">
                  <li>
                    <span>
                      <svg width="18" height="21" viewBox="0 0 18 21" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path d="M1.94 5.91249L7.94 2.16249C8.58854 1.75716 9.41146 1.75716 10.06 2.16249L16.06 5.91249C16.6448 6.27797 17 6.91891 17 7.60849V13.3915C17 14.0811 16.6448 14.722 16.06 15.0875L10.06 18.8375C9.41146 19.2428 8.58854 19.2428 7.94 18.8375L1.94 15.0875C1.35524 14.722 1 14.0811 1 13.3915V7.60849C1 6.91891 1.35524 6.27797 1.94 5.91249Z" stroke="#5F6D7E" stroke-width="2" stroke-linecap="round"/>
                      </svg>
                    </span>
                    {{ t('Account.Logout') }}
                  </li>
                </a>
                <hr>
                <LanguageSwitcher/>
              </ul>
            </div>
          </div>
        </div>

        <div class="collapse navbar-collapse" id="navbarSupportedContent" v-else>
          <ul class="navbar-nav me-auto mb-lg-0">
            <li class="nav-item">
              <router-link class="nav-link" active-class="active-link" :to="{ name: 'home' }">Strona główna</router-link>
            </li>
            <li class="nav-item"> 
              <router-link class="nav-link" active-class="active-link" :to="{ name: 'cars' }">Wynajmij samochód</router-link>
            </li>
            <li class="nav-item"> 
              <router-link class="nav-link" active-class="active-link" :to="{ name: 'cars' }">Flota</router-link>
            </li>
            <li class="d-md-none">
              <router-link class="nav-link" active-class="active-link" :to="{ name: 'login' }">Logowanie</router-link>
            </li>
            <li class="d-md-none">
              <router-link class="nav-link" active-class="active-link" to="/rejestracja">Rejestracja</router-link>
            </li>
          </ul>
          <div class="login-section d-none d-md-flex">
            <router-link class="nav-link register-button" active-class="active" to="/rejestracja">Rejestracja</router-link>
            <router-link class="nav-link login-button" active-class="active" to="/logowanie">
              <base-button type="dark">Logowanie</base-button>
            </router-link>
          </div>
        </div>
      </div>
    </nav>
  </header>
</template>

<style lang="scss" scoped>
div.overlay {
    position: fixed;
    top: 0;
    left: 0;
    height: 100vh;
    width: 100%;
    z-index: 1;
}
.nav-logo {
  width: 50px;
  margin-right: 20px;
}

nav {
  border-bottom: 1px solid #D1D9E2;
  background-color: $primary;
  color: $secondary;
  min-height: 90px;

  @media (min-width: 992px) {
    padding: 0 35px;
  }

  div {
    ul.navbar-nav {
      li.nav-item, li.d-md-none {

        @media (min-width: 992px) {
          margin: 0 10px;
        }

        .nav-link {
          padding: 9px 17px;
          border-radius: 5px;
          &.active-link {
            background-color: $button-light;
          }
        }
      }

      .dropdown-item:active {
        color: var(--bs-dropdown-link-hover-color);
        background-color: var(--bs-dropdown-link-hover-bg);
      }
      
    }

    div.login-section {
    align-items: center;
    
      .nav-link {
        margin: 0 10px;
        &.active {
          font-weight: 600;
          color: $secondary;
        }
      }
    }
    &.logged-in {
        ul.navbar-nav {
        li.nav-item {
          &:first-child {
            @media (min-width: 992px) {
              margin-left: 50px;
            }   
            
          }
          @media (min-width: 992px) {
            margin-left: 15px;
          }
          .nav-link {
            border-radius: 5px;
            &.active-link {
              background-color: $button-light;
            }
          }
        }
      }

      div.profile {
        position: relative;
        cursor: pointer;
        display: flex;

        @media (max-width: 768px) { 
            display: none !important;
        }

        div {
          text-align: left;
          margin: 0 10px;
          p {
            &:first-child {
              font-weight: 500;
            }
            margin: 0;
            font-size: 14px;
          }
        }
        img.profile-arrow {
          margin-left: 30px;
        }

        div.drop-down-menu {
          background-color: $primary;
          width: 230px;
          position: absolute;
          border-radius: 0px 0px 10px 10px;
          z-index: 1;
          top: 67px;
          right: -32px;
          border-bottom: 1px solid #D1D9E2;
          box-shadow: 0px 2px 6px rgba(0, 0, 0, 0.15);

          ul {
            list-style-type: none;
            width: 100%;
            padding: 15px 10px 15px 10px;
            margin: 0;
            
            a {
                text-decoration: none;
                font-weight: 500;
                color: $secondary;

              li {
                padding: 10px 0;
                border-radius: 10px;
                margin: 7px 0;

                &:hover {
                  background-color: $button-light;
                }

                span {
                  padding: 12px;
                }
              }
            }
          }
        }
      }
    }
  }
}
</style>