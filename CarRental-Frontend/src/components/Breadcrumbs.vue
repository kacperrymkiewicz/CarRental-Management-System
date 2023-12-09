<script setup>
const props = defineProps({
  isCustomer: {
    type: Boolean,
    required: false
  },
  homepageNav: {
    type: Boolean,
    required: false
  }
})
</script>

<template>
  <div>
    <span v-if="homepageNav"> Strona główna </span>
    <span v-else> {{ isCustomer ? "Panel klienta" : "Panel pracownika" }} </span>
    <slot></slot>
  </div>
</template>

<style lang="scss" scoped>
div {
  font-weight: 600;
  color: $secondary;
  text-align: left;
  margin-top: 30px;

  :slotted(a) {
    text-decoration: none;
    color: $secondary;

    &:hover {
      text-decoration: underline;
    }

    &::before {
      content: url("/frontend/images/icons/svg/breadcrumbs_arrow.svg");
      margin: 0 15px;
    }
  }
}
</style>