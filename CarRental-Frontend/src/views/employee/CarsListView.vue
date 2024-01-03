<script setup>
import { useUserStore } from '@/stores/user.store';
import { useRentalStore } from '@/stores/rental.store';
import { useI18n } from 'vue-i18n';
const userStore = useUserStore();
const rentalStore = useRentalStore();
const { t } = useI18n();
rentalStore.fetchCars();
</script>

<template>
  <section id="cars-list">
    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <breadcrumbs>
            <router-link :to="{ name: 'panel-cars' }">Zarządzanie flotą</router-link>
          </breadcrumbs>
          <welcome-message v-if="userStore.user" :name="userStore.user.firstname">
            <template v-slot:info>Oto lista wszystkich samochodów</template>
          </welcome-message>
          <div class="btn-container d-flex justify-content-end">
            <router-link :to="{ name: 'panel-cars-add' }">
              <base-button type="dark" class="m-0 mw-auto">
                Dodaj nowy samochód
              </base-button>
            </router-link>
          </div>
          <div class="wrapper d-flex flex-column">
            <div class="table-responsive d-flex flex-column">
              <table class="table">
                <thead>
                  <tr>
                    <th>Samochód</th>
                    <th>Rodzaj nadwozia</th>
                    <th>Status</th>
                    <th>Akcje</th>
                  </tr>
                </thead>
                <tbody v-if="rentalStore.foundCars">
                  <tr v-for="car in rentalStore.cars" :key="car">
                    <td>
                      <span>
                        {{ car.brand }} {{ car.model }}, {{ car.productionYear }} {{ car.engine }} {{ car.hoursepower }}km
                      </span>
                    </td>
                    <td>
                      <span>
                        {{ car.type }}
                      </span>
                    </td>
                    <td>
                      {{ car.status ? "Aktywny" : "Nieaktywny" }}
                    </td>
                    <td>
                      <button class="green-button">Edytuj parametry</button>
                      <button class="blue-button">Edytuj wyposażenie</button>
                      <button class="red-button">Usuń samochód</button>
                    </td>
                  </tr>
                </tbody>
                <tbody class="no-results" v-else>
                  <tr>
                    <td colspan="6">Nie znaleziono samochodów</td>
                  </tr>
                </tbody>
                <tfoot>
                  <tr>
                    <th>
                      <span class="tfoot-icon">
                        <img src="/frontend/images/icons/svg/table_arrow_left.svg">
                      </span>
                      <span class="tfoot-text">{{ t('Previous page') }}</span>
                    </th>
                    <th v-for="field in 2" :key="field"></th>
                    <th>
                      <span class="tfoot-text">{{ t('Next page') }}</span>
                      <span class="tfoot-icon">
                        <img src="/frontend/images/icons/svg/table_arrow_right.svg">
                      </span>
                    </th>
                  </tr>
                </tfoot>
              </table>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<style scoped lang="scss">
div.wrapper {
  margin: 40px 0;

  div.search {
    width: 300px;
    align-self: flex-end;

    @media (max-width: 768px) {
      align-self: center;
    }

    div.input-group {
      input {
        background-image: url("@/assets/images/icons/svg/magnifying_glass.svg");
        background-repeat: no-repeat;
        background-position-x: 16px;
        background-position-y: 8px;
        background-color: $primary;
        border: 1px solid $secondary;
        padding-left: 48px;
        font-weight: 600;
        color: $secondary;

        &:focus {
          box-shadow: rgba(0, 0, 0, 0.2) 0px 2px 2px;
        }
      }
    }
  }

  div.table-responsive {
    border: 1px solid $button-light;
    border-radius: 10px;
    background-color: $primary;

    table {
      margin: 0;

      thead,
      tfoot,
      tbody {
        tr {

          th,
          td {
            text-align: left;
          }
        }
      }

      thead,
      tfoot {
        tr {
          &:hover {
            background-color: transparent;
          }

          th {
            padding: 24px;
            font-weight: 600;
            line-height: 18px;
            color: $secondary;

            &:last-child {
              width: 200px;
            }
          }
        }
      }

      thead {
        tr {
          border-radius: 0 15px 0 15px;
        }
      }

      tbody {
        tr {
          &:hover {
            background-color: $button-light-hover;
          }

          td {
            padding: 16px 24px;
            font-weight: 500;
            vertical-align: middle;
            font-size: 14px;
            white-space: nowrap;


            span {
              a {
                color: inherit;
              }

              span {
                vertical-align: middle;

                &:first-child {
                  margin-right: 16px;
                }

                &.status-icon {
                  background-color: setStatus;
                  width: 15px;
                  height: 15px;
                  display: inline-block;
                  box-shadow: inset 0px 4px 4px rgba(0, 0, 0, 0.25);
                  border-radius: 100%;
                  box-sizing: border-box;
                  margin-right: 10px;

                }
              }
            }

            button {
              border-radius: 5px;
              padding: 2px 8px;
              color: white;
              font-weight: 500;
              font-size: 13px;
              line-height: 18px;
              border: 0;
              margin-right: 20px;
              transition: all .2s ease-in-out;

              &.disabled-green-button {
                cursor: not-allowed;
                background-color: $button-green-hover !important;
                pointer-events: none;
              }

              &.disabled-red-button {
                cursor: not-allowed;
                background-color: $button-red-hover !important;
                pointer-events: none;
              }

              &.disabled-blue-button {
                cursor: not-allowed;
                background-color: rgb(32, 85, 148, 0.5) !important;
                pointer-events: none;
              }

              &.blue-button {
                background-color: rgb(32, 85, 148, 1);

                &:hover {
                  background-color: rgb(32, 85, 148, 0.5)
                }
              }

              &.green-button {
                background-color: $button-green;

                &:hover {
                  background-color: $button-green-hover;
                }
              }

              &.red-button {
                background-color: $button-red;

                &:hover {
                  background-color: $button-red-hover;
                }
              }
            }
          }
        }

        &.no-results {
          tr {
            td {
              text-align: center;
            }
          }
        }
      }

      tfoot {
        tr {
          th {
            border: 0;
            white-space: nowrap;

            span.tfoot-text {
              vertical-align: middle;
              margin: 0 6px;
            }

            &:last-child {
              text-align: right;
            }
          }
        }
      }
    }
  }
}
</style>