<script setup lang="ts">
    const title = window.document.title;
</script>

<template>
    <div class="container mx-auto">
        <div>
            <h1 class="text-4xl font-bold bg-gradient-to-r from-blue-600 to-purple-600 bg-clip-text text-transparent">
                {{ title }}
            </h1>
            <p className="text-gray-600 dark:text-gray-400 mt-2">
              Real-time monitoring of Kafka consumer groups
            </p>
        </div>
    </div>
</template>
