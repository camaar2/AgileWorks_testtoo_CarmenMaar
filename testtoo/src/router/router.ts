import { createRouter, createWebHistory } from 'vue-router';
import Inquiry from '@/views/Inquiry.vue';


const routes = [
    { path: '/', component: Inquiry }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;
