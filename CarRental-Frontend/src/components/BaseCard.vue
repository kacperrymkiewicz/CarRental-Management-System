<template>
  <div class="container">
      <div class="outer-wrapper">
          <img class="info-icon" src="/frontend/images/icons/svg/base_card_info.svg">
          <div class="inner-wrapper">
              <p class="title"><slot name="title"></slot></p>
              <div class="content">
                  <slot name="content"></slot>
              </div>
          </div>
      </div>
  </div>
</template>

<style lang="scss" scoped>
div.container {
  @media (max-width: 992px) { 
      margin-bottom: 30px;
  }
  margin: 10px;

  div.outer-wrapper {
      display: flex;
      padding: 24px;
      background-color: #F8F9FB;
      border: 1.5px solid $secondary;
      border-radius: 5px;
      color: $secondary;
      position: relative;

      img.info-icon {
          position: absolute;
      }
      .title {
          font-weight: 500;
          font-size: 22px;
          line-height: 24px;
          letter-spacing: -0.01em;
          margin-bottom: 10px;
      }

      div.inner-wrapper {
          margin-left: 35px;
          text-align: left;

          div.content {
              :slotted(p){
                  font-size: 16px;
                  margin-bottom: 0px;
                  
                  span {
                      font-weight: 600;
                  }
              }
          }
      }

      .button {
          position: absolute;
          margin: 0;
          bottom: 20px;
          right: 20px;

      }
  }
}
</style>