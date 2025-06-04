<script setup lang="ts">
  import { Button } from '@/components/ui/button'
  import { Card, CardTitle, CardContent, CardDescription, CardAction } from '@/components/ui/card'
  import { Users } from 'lucide-vue-next';

</script>


<template>
    <Card class="p-6">
        <CardTitle class="flex items-center justify-between">
            <h1 class="text-2xl font-bold">Lag</h1>
            <Users color="red" :size="16" />
        </CardTitle>
      
        <CardDescription>
            This is a simple card component with a button.
        </CardDescription>
        
    </Card>
</template>