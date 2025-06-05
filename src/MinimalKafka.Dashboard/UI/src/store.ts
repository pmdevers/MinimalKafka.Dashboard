import { useFetch } from '@vueuse/core';

const absoluteUrl = new URL('dashboard.json', window.location.href).toString();
export const { data } = useFetch(absoluteUrl).get().json<IDashboardInfo>()

interface IDashboardInfo {
    name: string,
    consumers: Array<IConsumerInfo>
}

interface IConsumerInfo {
    topicName: string,
    groupId: string,
    clientId: string,
    keySchema: string,
    keyType: string,
    valueType: string,
    valueSchema: string
}