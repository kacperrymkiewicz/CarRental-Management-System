<script setup>
const props = defineProps(['car']);
</script>

<template>
  <div class="car-item">
    <h4>{{ car.brand }} {{ car.model }}</h4>
    <base-button class="car-button">Szczegóły</base-button>
  </div>
</template>

<style scoped lang="scss">
  .car-button {
    background-color: #E6E9EC!important; 
    border: 1px solid #D1D9E2!important; 
    color: rgb(95, 109, 126)!important;
    font-weight: 600!important;
    width: 100%;
    margin: 5px 0;
  }
</style>