<script setup>
const emit = defineEmits(['closeBookingModal'])
const props = defineProps({
  modalActive: {
    type: Boolean,
    default: false
  },
  data: {},
});
</script>

<template>
  <teleport to="body">
    <div @click="emit('closeBookingModal')" v-show="modalActive" class="overlay"></div>
    <transition name="modal-animation">
      <dialog open v-if="modalActive">
        <div class="outer-wrapper">
          <img class="info-icon" src="/frontend/images/icons/svg/base_card_info.svg">
          <img class="close-button" @click="emit('closeBookingModal')" src="/frontend/images/icons/svg/base_modal_close.svg">
          <div class="inner-wrapper">
            <div class="container">
              <div class="row">
                <div class="col-md-12">
                  <p class="title mb-3">Potwierdź rezerwację</p>
                </div>
              </div>
              <div class="row">
                <div class="col-md-6">
                  <p class="subtitle">Szczegóły rezerwacji</p>
                  <div class="content">
                    <p><span>Data odbioru: </span> 2023-02-02</p>
                    <p><span>Godzina odbioru: </span> 16:30</p>
                    <p><span>Data zwrotu: </span> 2023-04-30</p>
                    <p><span>Godzina zwrotu: </span> 17:15</p>
                  </div>
                </div>
                <div class="col-md-6">
                  <p class="subtitle">Samochód</p>
                  <div class="content">
                    <p><span>{{ data.brand }} {{ data.model }}</span></p>
                    <p><span>Godzina odbioru: </span> 16:30</p>
                    <p><span>Data zwrotu: </span> 2023-04-30</p>
                    <p><span>Godzina zwrotu: </span> 17:15</p>
                    <img class="img-fluid" :src="data.image">
                  </div>
                </div>
                <div class="modal-button">
                  <base-button class="mx-2" type="light" @click="emit('closeBookingModal')">Anuluj</base-button>
                  <base-button class="mx-2" type="dark">Dokonaj rezerwacji</base-button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </dialog>
    </transition>
  </teleport>
</template>

<style lang="scss" scoped>
.modal-animation-enter-active,
.modal-animation-leave-active {
  transition: all 0.3s ease;
}

.modal-animation-enter-from,
.modal-animation-leave-to {
  opacity: 0;
  transform: translate(-50%, -30%);
}

  div.overlay {
    position: fixed;
    top: 0;
    left: 0;
    height: 100vh;
    width: 100%;
    z-index: 1;
    background-color: rgba(0, 0, 0, 0.35);
  }

  dialog {
    width: 70%;
    border: 0;
    padding: 0;
    position: fixed;
    left: 50%;
    margin: 0;
    top: 50%;
    transform: translate(-50%, -50%);
    background-color: rgba(0, 0, 0, 0);
    z-index: 2;

    div.outer-wrapper {
      border: 1.5px solid $secondary;
      background-color: $base-modal-background;
      border-radius: 10px;
      filter: drop-shadow(0 0 30px #999);
      padding: 35px;
      position: relative;

      img.info-icon {
        position: absolute;
        top: 30px;
        left: 30px;
      }

      img.close-button {
        width: 30px;
        position: absolute;
        top: 30px;
        right: 30px;
        cursor: pointer;
      }

      div.inner-wrapper {
        padding: 25px;
        text-align: left;
        color: $secondary;

        p {
          margin-bottom: 0;
          font-size: 30px;

          &.title {
            font-weight: 700;
          }

          &.subtitle {
            text-transform: uppercase;
          }
        }


        div.content {
          p {
            margin: 5px 0;
            font-size: 20px;

            span {
              font-weight: 600;
            }
          }
        }

        .modal-button {
          display: flex;
          justify-content: center;
        }
      }

    }
  }
</style>