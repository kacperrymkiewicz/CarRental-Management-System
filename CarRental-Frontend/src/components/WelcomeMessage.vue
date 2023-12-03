<script setup>
const props = defineProps({
  name: {
    type: String,
    required: true
  }
});
</script>

<template>
  <div class="d-flex flex-column align-items-start">
    <h1 v-if="name">Witaj {{ capitalizeFirstLetter(this.name) }}</h1>
    <p class="info">
      <slot name="info"></slot>
    </p>
  </div>
</template>

<style lang="scss" scoped>
div {
  margin: 25px 0 50px;
  position: relative;

  h1 {
    font-size: 28px;
  }

  p.info {
    text-align: left;
  }
}
</style>