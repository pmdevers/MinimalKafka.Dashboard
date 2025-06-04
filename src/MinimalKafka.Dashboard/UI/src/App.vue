<script setup lang="ts">
  import NavBar from '@/components/NavBar.vue'
  import ConsumerInfo from '@/components/ConsumerInfo.vue'
  import ConsumerOverview from '@/components/ConsumerOverview.vue'

  try{
    const absoluteUrl = new URL('dashboard.json', window.location.href);
    const response = await fetch(absoluteUrl, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    });
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    const data = await response.json();
    
    // Process the consumer data as needed
  } catch (error) {
    console.error('Error during component setup:', error);
  }
</script>

<template>

<NavBar />

<main class="container mx-auto mt-8">

  <div class="flex items-center justify-center gap-4">
      <ConsumerInfo />
      <ConsumerInfo />
      <ConsumerInfo />
      <ConsumerInfo />
  </div>



  <ConsumerOverview />

</main>

</template>