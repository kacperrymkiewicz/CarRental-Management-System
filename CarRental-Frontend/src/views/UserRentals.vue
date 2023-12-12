<script setup>
import { useUserStore } from '@/stores/user.store';
const userStore = useUserStore();
userStore.fetchUserRentals();

const setStatus = (status) => {
  switch(status){
      case 'Confirmed':
            return "Potwierdzona"
      case 'Unconfirmed':
          return "Niepotwierdzona"
      case 'Reserved':
          return "Zarezerwowany"
      case 'Rented':
          return "Wynajęty"
      case 'Returned':
          return "Zakończony"
      case 'Unreturned':
          return "Niezwrócony"
      case 'Cancelled':
          return "Anulowany"
      default:
          return "Niezdefiniowany"
  }
};

const setStatusIcon = (status) => {
  switch(status) {
    case 'Reserved':
          return "#209420";

      case 'Confirmed':
          return "#209420";
      case 'Unconfirmed':
          return "#F8EE12"
      case 'Canceled':
          return "#F84912";
      case 'Finished':
          return "#205594"
      default:
          return "#FFF";
  }
};
</script>

<template>
  <section id="user-rentals-list">
      <div class="container">
          <div class="row">
              <div class="col-md-12">
                  <breadcrumbs :homepageNav="true">
                      <router-link :to="{ name: 'reservations' }">Moje rezerwacje</router-link>
                  </breadcrumbs>
                  <welcome-message v-if="userStore.user" :name="userStore.user.firstname">
                      <template v-slot:info>Oto lista wszystkich rezerwacji</template>
                  </welcome-message>
                  <div class="wrapper d-flex flex-column">
                      <div class="table-responsive d-flex flex-column">  
                          <table class="table">
                            <thead>
                                <tr>
                                    <th>Data odbioru</th>
                                    <th>Data zwrotu</th>
                                    <th>Samochód</th>
                                    <th>Status</th>
                                    <th>Akcje</th>
                                </tr>
                            </thead>
                            <tbody v-if="userStore.hasRentals">
                                <tr v-for="rental in userStore.userRentals" :key="rental">
                                  <td>
                                    <span>
                                        {{ new Date(rental.pickupDate).toLocaleDateString('pl', { weekday:"short", year:"numeric", month:"short", day:"numeric"}) }} {{ new Date(rental.pickupDate).toLocaleTimeString('pl', { hour: '2-digit', minute: '2-digit' }) }}
                                    </span>
                                  </td>
                                  <td>
                                    <span>
                                        {{ new Date(rental.returnDate).toLocaleDateString('pl', { weekday:"short", year:"numeric", month:"short", day:"numeric"}) }} {{ new Date(rental.returnDate).toLocaleTimeString('pl', { hour: '2-digit', minute: '2-digit' }) }}
                                    </span>
                                  </td>
                                  <td>
                                    <span>{{ rental.car.brand }} {{ rental.car.model }} {{ rental.car.productionYear }}, {{ rental.car.engine }}</span>
                                  </td>
                                  <td>
                                    <span>
                                      <span class="status-icon" :style="{backgroundColor: setStatusIcon(rental.status)}"></span>
                                      <span class="status-text">{{ capitalizeFirstLetter(setStatus(rental.status)) }}</span>
                                    </span>
                                  </td>
                                  <td>
                                    <!-- <button @click="confirmVisit(rental.id)" :class="['teal-button', {'disabled-teal-button': rental.status == 'Finished' || rental.status == 'Canceled' || rental.status == 'Confirmed'}]">Potwierdź</button> -->
                                    <button @click="cancelVisit(rental.id)" :class="['red-button', {'disabled-red-button': rental.status == 'Finished' || rental.status == 'Canceled'}]">Odwołaj</button>
                                  </td>
                                </tr>
                            </tbody>
                            <tbody class="no-results" v-else>
                                <tr>
                                    <td colspan="6">Brak rezerwacji</td>
                                </tr>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <th>
                                        <span class="tfoot-icon">
                                            <img src="/frontend/images/icons/svg/table_arrow_left.svg">
                                        </span>
                                        <span class="tfoot-text">Poprzednia strona</span></th>
                                    <th v-for="field in 3" :key="field"></th>  
                                    <th>
                                        <span class="tfoot-text">Następna strona</span>
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
            
            thead, tfoot, tbody {
                tr {
                    th, td {
                        text-align: left;
                    }
                }
            }
            thead, tfoot {
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

                            &.disabled-teal-button {
                                cursor: not-allowed;
                                background-color: $button-teal-hover !important;
                                pointer-events: none;
                            }
                            &.disabled-red-button {
                                cursor: not-allowed;
                                background-color: $button-red-hover !important; 
                                pointer-events: none;
                            }
                            &.blue-button {
                                background-color: $button-blue;

                                &:hover {
                                    background-color: $button-blue-hover;
                                }
                            }

                            &.teal-button {
                                background-color: $button-teal;

                                &:hover {
                                    background-color: $button-teal-hover;
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