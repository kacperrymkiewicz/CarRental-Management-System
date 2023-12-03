<script setup>
const props = defineProps({
  isCustomer: {
    type: Boolean,
    required: true
  }
})
</script>

<template>
  <div>
    <span> {{ isCustomer ? "Panel klienta" : "Panel pracownika" }} </span>
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