import Vue from 'vue';
import Vuex, { StoreOptions } from 'vuex';
import { RootState } from '@/store/types'; 
import inquiryModule from '@/store/modules/inquiryStore'; 

Vue.use(Vuex);

const store: StoreOptions<RootState> = {
    modules: {
        inquiry: inquiryModule,
    },
};

export default new Vuex.Store<RootState>(store);