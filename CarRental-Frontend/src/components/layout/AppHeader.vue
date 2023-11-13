<template>
  <header>
    <nav class="navbar navbar-expand-md bg-body-tertiary">
      <div class="container-fluid">
        <router-link to="/"><img src="/frontend/images/logo.png" class="nav-logo" alt="Logo"></router-link>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
          <ul class="navbar-nav me-auto mb-lg-0">
            <li class="nav-item">
              <router-link class="nav-link" active-class="active-logged-out" to="/">Strona główna</router-link>
            </li>
            <li class="nav-item"> 
              <router-link class="nav-link" active-class="active-logged-out" to="/umow-wizyte">Wynajmij samochód</router-link>
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