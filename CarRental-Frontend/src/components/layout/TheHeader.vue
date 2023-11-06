<template>
  <header>
    <nav class="navbar navbar-expand-md bg-body-tertiary">
      <div class="container-fluid">
        <router-link to="/"><img src="../assets/images/logo.png" class="nav-logo" alt="Logo"></router-link>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="logged-in collapse navbar-collapse" id="navbarSupportedContent" v-if="user">
          <ul class="navbar-nav me-auto mb-lg-0" v-if="user.accountType == 'Doctor'">
            <li class="nav-item">
              <router-link class="nav-link" active-class="active-logged-in" to="/lekarz/pacjenci">Pacjenci</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" active-class="active-logged-in" to="/lekarz/kalendarz-wizyt">Kalendarz wizyt</router-link>
            </li>
          </ul>
          <ul class="navbar-nav me-auto mb-lg-0" v-else-if="user.accountType == 'Receptionist'">
            <li class="nav-item">
              <router-link class="nav-link" active-class="active-logged-in" to="/recepcja/wizyty">Wizyty</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" active-class="active-logged-in" to="/recepcja/pacjenci">Lista pacjentów</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" active-class="active-logged-in" to="/recepcja/lista-lekarzy">Lista lekarzy</router-link>
            </li>
          </ul>
          <ul class="navbar-nav me-auto mb-lg-0" v-else>
            <li class="nav-item">
              <router-link class="nav-link" active-class="active-logged-in" to="/">Strona główna</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" active-class="active-logged-in" to="/moje-wizyty">Wizyty</router-link>
            </li>
            <li class="nav-item"> 
              <router-link class="nav-link" active-class="active-logged-in" to="/recepty">Recepty</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" active-class="active-logged-in" to="/umow-wizyte">Umów wizytę</router-link>
            </li>
          </ul>
          <div class="overlay" v-if="isDropDownMenuOpen" @click="toggleDropDownMenu"></div>
          <div class="profile d-flex align-items-center" @click="toggleDropDownMenu">
            <svg class="profile-icon" width="40" height="40" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
              <rect width="40" height="40" rx="20" fill="#5F6D7E"/>
              <path d="M20 21.3334C21.8409 21.3334 23.3333 19.841 23.3333 18C23.3333 16.1591 21.8409 14.6667 20 14.6667C18.159 14.6667 16.6666 16.1591 16.6666 18C16.6666 19.841 18.159 21.3334 20 21.3334ZM20 21.3334C17.0544 21.3334 14.6666 23.1242 14.6666 25.3334M20 21.3334C22.9455 21.3334 25.3333 23.1242 25.3333 25.3334" stroke="white" stroke-width="1.5" stroke-linecap="round"/>
            </svg>
            <div>
              <p> {{ capitalizeFirstLetter(user.firstName) }} {{ capitalizeFirstLetter(user.lastName) }}</p>
              <p> {{ accountType }}</p>
            </div>
            <img class="profile-arrow" src="@/assets/images/icons/svg/drop_down_menu_arrow_down.svg">
            <div class="drop-down-menu" v-if="isDropDownMenuOpen">
              <ul>
                <router-link to="/profil" v-if="user.accountType=='Patient'">
                  <li>
                    <span>
                      <svg width="18" height="21" viewBox="0 0 18 21" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path d="M1.94 5.91249L7.94 2.16249C8.58854 1.75716 9.41146 1.75716 10.06 2.16249L16.06 5.91249C16.6448 6.27797 17 6.91891 17 7.60849V13.3915C17 14.0811 16.6448 14.722 16.06 15.0875L10.06 18.8375C9.41146 19.2428 8.58854 19.2428 7.94 18.8375L1.94 15.0875C1.35524 14.722 1 14.0811 1 13.3915V7.60849C1 6.91891 1.35524 6.27797 1.94 5.91249Z" stroke="#5F6D7E" stroke-width="2" stroke-linecap="round"/>
                      </svg>
                    </span>
                    Mój profil
                  </li>
                </router-link>
                <a href="#" @click="logout">
                  <li>
                    <span>
                      <svg width="18" height="21" viewBox="0 0 18 21" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path d="M1.94 5.91249L7.94 2.16249C8.58854 1.75716 9.41146 1.75716 10.06 2.16249L16.06 5.91249C16.6448 6.27797 17 6.91891 17 7.60849V13.3915C17 14.0811 16.6448 14.722 16.06 15.0875L10.06 18.8375C9.41146 19.2428 8.58854 19.2428 7.94 18.8375L1.94 15.0875C1.35524 14.722 1 14.0811 1 13.3915V7.60849C1 6.91891 1.35524 6.27797 1.94 5.91249Z" stroke="#5F6D7E" stroke-width="2" stroke-linecap="round"/>
                      </svg>
                    </span>
                    Wyloguj
                  </li>
                </a>
              </ul>
            </div>
          </div>
        </div>
        <div class="collapse navbar-collapse" id="navbarSupportedContent" v-else>
          <ul class="navbar-nav me-auto mb-lg-0">
            <li class="nav-item">
              <router-link class="nav-link" active-class="active-logged-out" to="/">Strona główna</router-link>
            </li>
            <li class="nav-item"> 
              <router-link class="nav-link" active-class="active-logged-out" to="/umow-wizyte">Umów wizytę</router-link>
            </li>
            <li class="d-md-none">
              <router-link class="nav-link" active-class="active-logged-out" to="/logowanie">Logowanie</router-link>
            </li>
            <li class="d-md-none">
              <router-link class="nav-link" active-class="active-logged-out" to="/rejestracja">Rejestracja</router-link>
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
<script>
import router from '@/router';


</script>

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
          font-weight: 500;
          
          &.active-logged-out {
            font-weight: 800;
            color: $secondary;
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
            &.active-logged-in {
              background-color: $button-light;
              font-weight: 500;
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