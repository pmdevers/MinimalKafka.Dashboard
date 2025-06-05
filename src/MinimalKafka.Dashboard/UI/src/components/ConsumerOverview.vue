<script lang="ts" setup>
import {
  Table,
  TableBody,
  TableCaption,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table'

import { data } from "@/store";

</script>

<template>
<div class="container mx-auto mt-8">


    <Table>
        <TableCaption>A list of your recent invoices.</TableCaption>
        <TableHeader>
        <TableRow>
            <TableHead class="w-[100px]">
            Topic Name
            </TableHead>
            <TableHead>Client Id</TableHead>
            <TableHead>Group Id</TableHead>
            <TableHead class="text-right">Key Type</TableHead>
            <TableHead class="text-right">Value Type</TableHead>
        </TableRow>
        </TableHeader>
        <TableBody>
        <TableRow v-for="(item, index) in data?.consumers" :key="index">
            <TableCell class="font-medium">{{ item.topicName }}</TableCell>
            <TableCell>{{ item.clientId }}</TableCell>
            <TableCell>{{ item.groupId }}</TableCell>
            <TableCell class="text-right">
              {{ item.keyType }}
            </TableCell>
            <TableCell class="text-right">
              {{ item.valueType }}
            </TableCell>
        </TableRow>
        </TableBody>
    </Table>

  </div>
</template>