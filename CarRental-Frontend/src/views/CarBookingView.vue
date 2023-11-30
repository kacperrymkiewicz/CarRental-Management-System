<script setup>
  import { useRoute } from 'vue-router';
  const route = useRoute();
  console.log(route.query);
  // const props = defineProps(['pickupDate']);
  // console.log(props);
</script>

<template>
  <!-- <h1>{{ pickupDate }}</h1> -->
</template>