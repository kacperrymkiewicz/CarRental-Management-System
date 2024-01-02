<script setup>
import { useUserStore } from '@/stores/user.store';
import { useRentalStore } from '@/stores/rental.store';
import { useToast, TYPE } from "vue-toastification";
import { useI18n } from 'vue-i18n';
const userStore = useUserStore();
const rentalStore = useRentalStore();
rentalStore.fetchRentals();

const toast = useToast();
const { t } = useI18n();

const setStatusIcon = (status) => {
  switch (status) {
    case 'Unconfirmed':
      return "#F8EE12";
    case 'Reserved':
      return "#209420";
    case 'Rented':
      return "#14c4cd";
    case 'Returned':
      return "#205594"
    case 'Unreturned':
      return "#ff9510"
    case 'Cancelled':
      return "#F84912";
    default:
      return "#FFF";
  }
};

const cancelReservation = async (rentalId) => {
  const responseStatus = await rentalStore.cancelReservation(rentalId);
  if (responseStatus.success) {
    rentalStore.fetchRentals();
  }
  toast(responseStatus.message, {
    type: responseStatus.success ? TYPE.SUCCESS : TYPE.ERROR,
    timeout: 2000
  });
}

const confirmReservation = async (rentalId) => {
  const responseStatus = await rentalStore.confirmReservation(rentalId);
  if (responseStatus.success) {
    rentalStore.fetchRentals();
  }
  toast(responseStatus.message, {
    type: responseStatus.success ? TYPE.SUCCESS : TYPE.ERROR,
    timeout: 2000
  });
}

const finishReservation = async (rentalId) => {
  const responseStatus = await rentalStore.finishReservation(rentalId);
  if (responseStatus.success) {
    rentalStore.fetchRentals();
  }
  toast(responseStatus.message, {
    type: responseStatus.success ? TYPE.SUCCESS : TYPE.ERROR,
    timeout: 2000
  });
}

const formatDate = (date) => {
  return date.getFullYear() + '-' + (date.getMonth() + 1).toString().padStart(2, '0') + '-' + 
         date.getDate().toString().padStart(2, '0') + ' ' + ("00" + date.getHours()).slice(-2) + ":" + ("00" + date.getMinutes()).slice(-2);  
}
</script>

<template>
  <section id="rentals-list">
    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <breadcrumbs>
            <router-link :to="{ name: 'panel-booking' }">Lista rezerwacji</router-link>
          </breadcrumbs>
          <welcome-message v-if="userStore.user" :name="userStore.user.firstname">
            <template v-slot:info>Oto lista rezerwacji dokonanych przez klientów</template>
          </welcome-message>
          <div class="wrapper d-flex flex-column">
            <div class="table-responsive d-flex flex-column">
              <table class="table">
                <thead>
                  <tr>
                    <th>{{ t('Booking.Pick-up') }}</th>
                    <th>{{ t('Booking.Drop-off') }}</th>
                    <th>{{ t('Account.Customer') }}</th>
                    <th>{{ t('Car.Car') }}</th>
                    <th>{{ t('Status') }}</th>
                    <th>{{ t('Actions') }}</th>
                  </tr>
                </thead>
                <tbody v-if="rentalStore.foundRentals">
                  <tr v-for="rental in rentalStore.rentals" :key="rental">
                    <td>
                      <span>
                        {{ formatDate(new Date(rental.pickupDate)) }}
                      </span>
                    </td>
                    <td>
                      <span>
                        {{ formatDate(new Date(rental.returnDate)) }}
                      </span>
                    </td>
                    <td>
                      {{ rental.customer.firstName }} {{ rental.customer.lastName }}
                    </td>
                    <td>
                      <span>
                        <router-link :to="{ name: 'car', params: { id: rental.car.id } }">
                          {{ rental.car.brand }} {{ rental.car.model }} {{ rental.car.productionYear }}, {{ rental.car.engine }}
                        </router-link>
                      </span>
                    </td>
                    <td>
                      <span>
                        <span class="status-icon" :style="{ backgroundColor: setStatusIcon(rental.status) }"></span>
                        <span class="status-text">{{ capitalizeFirstLetter(t(`Booking.Status.${rental.status}`)) }}</span>
                      </span>
                    </td>
                    <td>
                      <button @click="confirmReservation(rental.id)" :class="['green-button', {'disabled-green-button': rental.status == 'Returned' || rental.status == 'Reserved' || rental.status == 'Rented' || rental.status == 'Unreturned' }]">{{ t('Booking.Confirm') }}</button>
                      <button @click="cancelReservation(rental.id)" :class="['red-button', { 'disabled-red-button': rental.status == 'Rented' || rental.status == 'Returned' || rental.status == 'Unreturned' || rental.status == 'Cancelled' }]">{{ t('Booking.Cancel') }}</button>
                      <button @click="finishReservation(rental.id)" :class="['blue-button', { 'disabled-blue-button': rental.status == 'Returned' || rental.status == 'Cancelled' || rental.status == 'Unconfirmed' || rental.status == 'Reserved' }]">{{ t('Booking.Finish') }}</button>
                    </td>
                  </tr>
                </tbody>
                <tbody class="no-results" v-else>
                  <tr>
                    <td colspan="6">{{ t('Booking.No reservations') }}</td>
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
                    <th v-for="field in 4" :key="field"></th>
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