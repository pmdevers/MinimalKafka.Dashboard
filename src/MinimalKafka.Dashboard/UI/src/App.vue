<script setup lang="ts">
  
  import NavBar from '@/components/NavBar.vue'
  import ConsumerInfo from '@/components/ConsumerInfo.vue'
  import ConsumerOverview from '@/components/ConsumerOverview.vue'
  import { data } from "@/store";

  
</script>

<template>

<NavBar />

<main class="container mx-auto mt-8">
  <ConsumerOverview />
</main>

</template>