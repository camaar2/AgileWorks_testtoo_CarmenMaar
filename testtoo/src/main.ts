import { createApp } from 'vue';
import App from './App.vue';
import {setApiUrl} from "@/model/api.ts";
import router from './router/router';
const getRuntimeConf = async () => {
    const runtimeConf = await fetch('/config/runtime-config.json');
    return await runtimeConf.json();
};

const app = createApp(App);

getRuntimeConf().then((json) => {
    setApiUrl(json.API_URL);

    
    app.use(router);
    

    app.mount('#app');
});